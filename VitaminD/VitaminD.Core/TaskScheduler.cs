using Microsoft.Phone.Scheduler;

namespace VitaminD.Core
{
    public class TaskScheduler : ScheduledTaskAgent
    {
        protected override void OnInvoke(ScheduledTask task)
        {
            Microsoft.Phone.Shell.ShellToast toast = new Microsoft.Phone.Shell.ShellToast();
            toast.Content = "TIME'S UP! GO AROUND";
            toast.Title = "VITAMIND";
            toast.NavigationUri = new System.Uri("MainPage.xaml", System.UriKind.Relative);
            toast.Show();       
            NotifyComplete();
        }

        protected override void OnCancel()
        {
            base.OnCancel();
        }

        private void GetNotice()
        {
           

        }

    }

}
