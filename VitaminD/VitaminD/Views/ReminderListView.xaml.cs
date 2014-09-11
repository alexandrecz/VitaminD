using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Scheduler;
using System.ComponentModel;

namespace VitaminD.Views
{
    public partial class ReminderListView : PhoneApplicationPage
    {
        #region Attributes

        private ApplicationBarIconButton deleteButton = new ApplicationBarIconButton();
        private ApplicationBarIconButton selectButton = new ApplicationBarIconButton();
        private ApplicationBarIconButton back = new ApplicationBarIconButton();
        private List<ScheduledNotification> notifications = ScheduledActionService.GetActions<ScheduledNotification>() as List<ScheduledNotification>;

        #endregion

        #region constructor

        public ReminderListView()
        {
            InitializeComponent();
            BuildApplicationBar();
            Long.ItemsSource = Notifications;
        }

        #endregion

        #region properties

        public List<ScheduledNotification> Notifications
        {
            get { return notifications; }
            set
            {
                if (notifications != null)
                {
                    notifications = value;
                    NotifyPropertyChanged("Notifications");
                }
            }
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

            selectButton.IconUri = new Uri("/Assets/ApplicationBar.Select.png", UriKind.Relative);
            selectButton.Text = "select";
            selectButton.IsEnabled = true;
            ApplicationBar.Buttons.Add(selectButton);

            selectButton.Click += (s, e) =>
            {
                Long.IsSelectionEnabled = !Long.IsSelectionEnabled;
            };

            deleteButton.IconUri = new Uri("/Assets/delete.png", UriKind.Relative);
            deleteButton.Text = "delete";
            deleteButton.IsEnabled = false;
            ApplicationBar.Buttons.Add(deleteButton);

            deleteButton.Click += (s, e) =>
            {
                DeleteReminder();
            };
        }


        public void DeleteReminder()
        {
            if (Long.SelectedItems.Count == 1)
            {
                if (MessageBox.Show("Do you want to remove this reminder?", "Confirmation", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    ScheduledNotification remind = Notifications.First(r => r.Name == ((ScheduledNotification)(Long.SelectedItems[0])).Name);
                    ScheduledActionService.Remove(remind.Name);
                    MessageBox.Show("Reminder removed");
                    NavigationService.GoBack();
                }
            }
            else
            {
                if (MessageBox.Show("Do you want to remove these reminders?", "Confirmation", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    foreach (ScheduledNotification item in Long.SelectedItems)
                    {
                        ScheduledNotification remind = Notifications.First(r => r.Name == item.Name);
                        ScheduledActionService.Remove(remind.Name);
                    }
                    MessageBox.Show("Reminders removed");
                    NavigationService.GoBack();
                }
            }
        }


        private void Long_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            deleteButton.IsEnabled = (Long.SelectedItems.Count > 0);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

    }
}