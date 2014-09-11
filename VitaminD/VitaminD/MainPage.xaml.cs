
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using System;


namespace VitaminD
{

    public partial class MainPage : PhoneApplicationPage
    {

        public MainPage()
        {
            InitializeComponent();
            DataContext = App.ViewModel;
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }
        }

        private void RateMe_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Microsoft.Phone.Tasks.MarketplaceReviewTask mk = new Microsoft.Phone.Tasks.MarketplaceReviewTask();
            mk.Show();
        }

        private void EmailMe_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Microsoft.Phone.Tasks.EmailComposeTask emailComposeTask = new Microsoft.Phone.Tasks.EmailComposeTask();
            emailComposeTask.To = "alexandrecz@live.com";
            emailComposeTask.Subject = "VitaminD - Feddback";
            emailComposeTask.Body = "Leave your comments here...";
            emailComposeTask.Show();
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/SourceView.xaml?name=" + ((System.Windows.FrameworkElement)(sender)).Name, UriKind.Relative));
        }

        private void MainMenu_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (MainMenu.SelectedItem != null)
            {
                switch ((MainMenu.SelectedItem as ViewModels.ItemViewModel).Id)
                {
                    case 1:
                        {
                            NavigationService.Navigate(new System.Uri("/Views/TimerView.xaml", System.UriKind.Relative));
                            break;
                        }
                    case 2:
                        {
                            NavigationService.Navigate(new System.Uri("/Views/ReminderView.xaml", System.UriKind.Relative));
                            break;
                        }
                    case 3:
                        {
                            NavigationService.Navigate(new System.Uri("/Views/ReminderListView.xaml", System.UriKind.Relative));
                            break;
                        }
                }
            }
            MainMenu.SelectedItem = null;
        }
    }
}