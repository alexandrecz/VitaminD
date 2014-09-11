using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using VitaminD.Resources;

namespace VitaminD.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            this.Items = new ObservableCollection<ItemViewModel>();
        }

        public ObservableCollection<ItemViewModel> Items { get; private set; }

        public bool IsDataLoaded
        {
            get;
            private set;
        }


        public void LoadData()
        {
            this.Items.Add(new ItemViewModel() { Id = 1, Title = AppResources.SunlightTimer });
            this.Items.Add(new ItemViewModel() { Id = 2, Title = AppResources.SunlightReminder });
            this.Items.Add(new ItemViewModel() { Id = 3, Title = AppResources.ReminderList });
            this.IsDataLoaded = true;
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
    }
}