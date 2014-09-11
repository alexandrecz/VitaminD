#define DEBUG_AGENT
using System.Diagnostics;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Phone.Scheduler;

namespace VitaminD.Scheduler
{
    public class ScheduledAgent : ScheduledTaskAgent
    {
        /// <remarks>
        /// ScheduledAgent constructor, initializes the UnhandledException handler
        /// </remarks>
        static ScheduledAgent()
        {
            // Subscribe to the managed exception handler
            Deployment.Current.Dispatcher.BeginInvoke(delegate
            {
                Application.Current.UnhandledException += UnhandledException;
            });
        }

        /// Code to execute on Unhandled Exceptions
        private static void UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (Debugger.IsAttached)
            {
                // An unhandled exception has occurred; break into the debugger
                Debugger.Break();
            }
        }

        int count = 0;
        /// <summary>
        /// Agent that runs a scheduled task
        /// </summary>
        /// <param name="task">
        /// The invoked task
        /// </param>
        /// <remarks>
        /// This method is called when a periodic or resource intensive task is invoked
        /// </remarks>
        protected async override void OnInvoke(ScheduledTask task)
        {
            string message = "Time to Sunlight!";

            await System.Threading.Tasks.Task.Delay(1);


            count += 1;
            Microsoft.Phone.Shell.ShellToast toast = new Microsoft.Phone.Shell.ShellToast();
            toast.Content = message + count.ToString();
            toast.Title = "vitaminD";
            //toast.NavigationUri = new System.Uri("/Views/SourceView.xaml?name=sun&home=Main", System.UriKind.Relative);
            toast.Show();

            ////option tile lockscreen
            Microsoft.Phone.Shell.ShellTile tile = Microsoft.Phone.Shell.ShellTile.ActiveTiles.FirstOrDefault();
            var data = new Microsoft.Phone.Shell.StandardTileData();
            data.Count = count;
            data.BackContent = string.Format("TIME'S UP! Go out there and take a bit of sunlight");
            tile.Update(data);


#if(DEBUG_AGENT)
            ScheduledActionService.LaunchForTest(task.Name, System.TimeSpan.FromSeconds(10));
#endif
            NotifyComplete();
        }
    }
}