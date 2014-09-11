using Microsoft.Phone.Controls;
using Microsoft.Phone.Scheduler;
using Microsoft.Phone.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Navigation;
using System.Windows.Threading;
using VitaminD.Resources;

namespace VitaminD.Views
{
    public partial class TimerView : PhoneApplicationPage
    {
        #region attributes

        private ApplicationBarIconButton back = new ApplicationBarIconButton();
        private ApplicationBarIconButton help = new ApplicationBarIconButton();
        private int startTime = 0;
        private DispatcherTimer newTimer;
        private int tic = 0;
        private int ticM = 0;
        private int ticH = 0;
        private bool isRunning = false;
        private bool isHelp = false;
        private string actionLabel = AppResources.ButtonActionStartText;

        private string hour = "00";
        private string min = "00";
        private string sec = "00";
        private bool isAlarmActived;

        #endregion

        public TimerView()
        {
            InitializeComponent();
            BuildApplicationBar();
            ActionLabel.Content = AppResources.ButtonActionStartText;
            Sec.Text = sec;
            Min.Text = min;
            Hour.Text = hour;
        }

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
                RemoveAlarm();
                NavigationService.GoBack();
            };

            help.IconUri = new Uri("/Assets/help.png", UriKind.Relative);
            help.Text = "help";
            ApplicationBar.Buttons.Add(help);
            help.Click += (s, e) =>
            {
                if (!isHelp)
                {
                    StHelp.Begin();
                    isHelp = !isHelp;
                }
                else
                {
                    StHelpClose.Begin();
                    isHelp = !isHelp;
                }
            };
        }

        #region methods

        public void Start()
        {
            PhoneApplicationService.Current.ApplicationIdleDetectionMode = IdleDetectionMode.Disabled;
            if (startTime == 0)
            {
                startTime = DateTime.Now.Minute;
            }
            
            if (!isRunning)
            {
                this.newTimer = new DispatcherTimer();
                this.newTimer.Interval = TimeSpan.FromSeconds(1);
                this.newTimer.Tick += (o, s) =>
                {
                    this.tic += ((DispatcherTimer)(o)).Interval.Seconds;

                    if (this.tic == 60)
                    {
                        this.tic = 0;
                        sec = "00";
                        Min.Text = (ticM == 60 ? ticM = 0 : ticM += 1).ToString();

                        if (this.ticM == 20 && ticH == 0)
                        {
                            StartAlarm();
                        }

                        if (this.ticM == 60)
                        {
                            Sec.Text = "0";
                            this.ticM = 0;
                            Min.Text = "00";
                            Hour.Text = (ticH == 60 ? ticH = 0 : ticH += 1).ToString();
                        }

                        Min.Text = Min.Text.Length == 1 ? "0" + Min.Text : Min.Text;
                        Hour.Text = Hour.Text.Length == 1 ? "0" + Hour.Text : Hour.Text;
                    }
                    else
                    {
                        Sec.Text = this.tic.ToString();
                    }
                    Sec.Text = Sec.Text.Length == 1 ? "0" + Sec.Text : Sec.Text;

                    ActionLabel.Content = AppResources.ButtonActionPauseText;
                };

                newTimer.Start();
                isRunning = true;
            }
            else
            {
                Pause();
            }
        }

        public void Pause()
        {
            if (isRunning)
            {
                newTimer.Stop();
                isRunning = false;
                ActionLabel.Content = AppResources.ButtonActionResumeText;
            }
            else
            {
                Start();
            }
        }

        private void StartAlarm()
        {
            #region alarmOld
            //RemoveAlarm();

            //Alarm alarm = new Alarm("Sunlight")
            //{
            //    Content = AppResources.SunlightExposure,
            //    BeginTime = DateTime.Now.AddMinutes(1),
            //    RecurrenceType = RecurrenceInterval.None
            //};

            ////alarm.Sound = new Uri("/Ringtones/Ring01.wma", UriKind.Relative);
            //ScheduledActionService.Add(alarm);
            #endregion

            ApplicationBar.IsVisible = false;
            StAlarm.Begin();
            media.Play();
            media.MediaEnded += (s, e) =>
            {
                media.Play();
            };
        }

        private void StopAlarm()
        {
            StAlarmClose.Begin();
            media.Stop();
            Reset();
            ApplicationBar.IsVisible = true;
        }

        private void Reset()
        {
            //RemoveAlarm();
            newTimer.Stop();
            isRunning = false;
            ActionLabel.Content = AppResources.ButtonActionStartText;
            Sec.Text = "00";
            Min.Text = "00";
            Hour.Text = "00";
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Start();
        }

        private void CloseAlarm_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            StopAlarm();
        }

        private void RemoveAlarm()
        {
            List<ScheduledNotification> notifications = ScheduledActionService.GetActions<ScheduledNotification>() as List<ScheduledNotification>;
            if (notifications.Count > 0)
            {
                ScheduledNotification al = notifications.First(r => r.Name == "Sunlight");
                ScheduledActionService.Remove(al.Name);
            }
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            //   RemoveAlarm();
            Reset();
            base.OnBackKeyPress(e);
        }


        #endregion
    }
}