using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Scheduler;

namespace VitaminD.Views
{
    public partial class ReminderView : PhoneApplicationPage
    {
        #region attributes

        private ApplicationBarIconButton back = new ApplicationBarIconButton();
        private ApplicationBarIconButton saveButton = new ApplicationBarIconButton();
        private IEnumerable<ScheduledNotification> notifications = ScheduledActionService.GetActions<ScheduledNotification>();

        private string title = "Reminder ";
        private string content = "TIME'S UP! It is time to take a bit of sunlight!";
        private RecurrenceInterval recurrence = RecurrenceInterval.None;

        #endregion

        #region constructor

        public ReminderView()
        {
            InitializeComponent();
            BuildApplicationBar();
        }

        #endregion

        #region methods

        private void BuildApplicationBar()
        {
            ApplicationBar = new ApplicationBar();
            ApplicationBar.Mode = ApplicationBarMode.Default;
            ApplicationBar.Opacity = 0.2;
            ApplicationBar.IsVisible = true;
            ApplicationBar.IsMenuEnabled = false;

            back.IconUri = new Uri("/Assets/back.png", UriKind.Relative);
            back.Text = "back";
            ApplicationBar.Buttons.Add(back);
            back.Click += (s, e) =>
            {
                NavigationService.GoBack();
            };

            saveButton.IconUri = new Uri("/Assets/save.png", UriKind.Relative);
            saveButton.Text = "save";

            ApplicationBar.Buttons.Add(saveButton);

            saveButton.Click += (s, e) =>
            {
                AddReminder();
            };
        }


        public void AddReminder()
        {
            // Get the begin time for the notification by combining the DatePicker
            // value and the TimePicker value.
            DateTime date = (DateTime)beginDatePicker.Value;
            DateTime time = (DateTime)beginTimePicker.Value;
            DateTime beginTime = date + time.TimeOfDay;

            // Make sure that the begin time has not already passed.
            if (beginTime < DateTime.Now)
            {
                MessageBox.Show("the begin date must be in the future.");
                return;
            }

            // Get the expiration time for the notification.
            date = (DateTime)expirationDatePicker.Value;
            time = (DateTime)expirationTimePicker.Value;
            DateTime expirationTime = date + time.TimeOfDay;

            // Make sure that the expiration time is after the begin time.
            if (expirationTime < beginTime)
            {
                MessageBox.Show("expiration time must be after the begin time.");
                return;
            }


            if (dailyRadioButton.IsChecked == true)
            {
                recurrence = RecurrenceInterval.Daily;
            }
            else if (weeklyRadioButton.IsChecked == true)
            {
                recurrence = RecurrenceInterval.Weekly;
            }
            else if (monthlyRadioButton.IsChecked == true)
            {
                recurrence = RecurrenceInterval.Monthly;
            }
            else if (endOfMonthRadioButton.IsChecked == true)
            {
                recurrence = RecurrenceInterval.EndOfMonth;
            }
            else if (yearlyRadioButton.IsChecked == true)
            {
                recurrence = RecurrenceInterval.Yearly;
            }

            Reminder reminder = new Reminder((notifications.Count<ScheduledNotification>() > 0 ? (notifications.Count<ScheduledNotification>() + 1).ToString() : "1"));
            reminder.Title = string.Format("{0}{1}", title, (notifications.Count<ScheduledNotification>() > 0 ? (notifications.Count<ScheduledNotification>() + 1).ToString() : "1"));
            reminder.Content = content;
            reminder.BeginTime = beginTime;
            reminder.ExpirationTime = expirationTime;
            reminder.RecurrenceType = recurrence;

            ScheduledActionService.Add(reminder);

            MessageBox.Show("reminder added!");
            NavigationService.GoBack();
        }

        #endregion

    }
}