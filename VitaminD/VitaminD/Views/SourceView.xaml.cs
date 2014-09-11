using System;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Phone.Shell;

namespace VitaminD.Views
{
    public partial class SourceView : PhoneApplicationPage
    {
        private ImageSource imgSource;

        public SourceView()
        {
            InitializeComponent();
            BuildApplicationBar();
        }

        private void BuildApplicationBar()
        {
            ApplicationBar = new ApplicationBar();
            ApplicationBar.Mode = ApplicationBarMode.Default;
            ApplicationBar.Opacity = 0.2;
            ApplicationBar.IsVisible = true;
            ApplicationBar.IsMenuEnabled = false;

            ApplicationBarIconButton back = new ApplicationBarIconButton();
            back.IconUri = new Uri("/Assets/back.png", UriKind.Relative);
            back.Text = "back";
            ApplicationBar.Buttons.Add(back);
            back.Click += (s, e) =>
            {
                NavigationService.GoBack();
            };
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            if (NavigationContext.QueryString.ContainsKey("name"))
            {
                SetSource(NavigationContext.QueryString["name"]);
                StPlay.Begin();
            }           
        }

        private void SetSource(string name)
        {
            switch (name)
            {
                case "sun":
                    {
                        imgSource = new BitmapImage(new Uri("Assets/sunlight.jpg", UriKind.Relative));
                        TextSource.Text = VitaminD.Resources.AppResources.SourceSun;
                        ViewTitle.Text = VitaminD.Resources.AppResources.SunTitle;

                        break;
                    }
                case "sal":
                    {
                        imgSource = new BitmapImage(new Uri("Assets/sal.jpg", UriKind.Relative));
                        TextSource.Text = VitaminD.Resources.AppResources.SourceSal;
                        ViewTitle.Text = VitaminD.Resources.AppResources.SalTitle;
                        break;
                    }
                case "eggs":
                    {
                        imgSource = new BitmapImage(new Uri("Assets/eggs.jpg", UriKind.Relative));
                        TextSource.Text = VitaminD.Resources.AppResources.SourceEgg;
                        ViewTitle.Text = VitaminD.Resources.AppResources.EggTitle;
                        break;
                    }
                case "tun":
                    {
                        imgSource = new BitmapImage(new Uri("Assets/tuna.jpg", UriKind.Relative));
                        TextSource.Text = VitaminD.Resources.AppResources.SourceTun;
                        ViewTitle.Text = VitaminD.Resources.AppResources.TunTitle;
                        break;
                    }
                case "mus":
                    {
                        imgSource = new BitmapImage(new Uri("Assets/mush.jpg", UriKind.Relative));
                        TextSource.Text = VitaminD.Resources.AppResources.SourceMus;
                        ViewTitle.Text = VitaminD.Resources.AppResources.MusTitle;
                        break;
                    }
                case "cer":
                    {
                        imgSource = new BitmapImage(new Uri("Assets/cereal.jpg", UriKind.Relative));
                        TextSource.Text = VitaminD.Resources.AppResources.SourceCer;
                        ViewTitle.Text = VitaminD.Resources.AppResources.CerTitle;
                        break;
                    }
                case "mil":
                    {
                        imgSource = new BitmapImage(new Uri("Assets/milk.jpg", UriKind.Relative));
                        TextSource.Text = VitaminD.Resources.AppResources.SourceMilk;
                        ViewTitle.Text = VitaminD.Resources.AppResources.MilkTitle;
                        break;
                    }
                case "ric":
                    {
                        imgSource = new BitmapImage(new Uri("Assets/ricotta.jpg", UriKind.Relative));
                        TextSource.Text = VitaminD.Resources.AppResources.SourceRic;
                        ViewTitle.Text = VitaminD.Resources.AppResources.RicTitle;
                        break;
                    }
            }

            ImageSource.Source = imgSource;
        }

    }
}