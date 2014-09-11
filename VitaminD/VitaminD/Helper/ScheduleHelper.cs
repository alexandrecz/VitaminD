#define DEBUG_AGENT
using Microsoft.Phone.Scheduler;
using System;
using System.Windows;


namespace VitaminD.Helper
{
    public class ScheduleHelper
    {

        PeriodicTask periodicTask;
        ResourceIntensiveTask resourceIntensiveTask;

        string periodicTaskName = string.Empty;
        string resourceIntensiveTaskName = string.Empty;
        public bool agentsAreEnabled = true;

        public ScheduleHelper(string periodicTaskName)
        {
            this.periodicTaskName = periodicTaskName;
        }


        public void StartPeriodicAgent()
        {
            agentsAreEnabled = true;

            periodicTask = ScheduledActionService.Find(this.periodicTaskName) as PeriodicTask;
            if (periodicTask != null)
            {
                RemoveAgent(this.periodicTaskName);
            }

            periodicTask = new PeriodicTask(this.periodicTaskName);
            periodicTask.Description = "This demonstrates a periodic task.";

            try
            {
                ScheduledActionService.Add(periodicTask);
#if(DEBUG_AGENT)
                ScheduledActionService.LaunchForTest(periodicTask.Name, System.TimeSpan.FromSeconds(10));
#endif
            }
            catch (InvalidOperationException exception)
            {
                if (exception.Message.Contains("BNS Error: The action is disabled"))
                {
                    System.Windows.MessageBox.Show("Background agents for this application have been disabled by the user.");
                    agentsAreEnabled = false;
                    //PeriodicCheckBox.IsChecked = false;
                }

                if (exception.Message.Contains("BNS Error: The maximum number of ScheduledActions of this type have already been added."))
                {                    // No user action required. The system prompts the user when the hard limit of periodic tasks has been reached.

                }
                //PeriodicCheckBox.IsChecked = false;
            }
            catch (SchedulerServiceException)
            {
                // No user action required.
                //PeriodicCheckBox.IsChecked = false;
            }

        }


        private void StartResourceIntensiveAgent()
        {
            agentsAreEnabled = true;

            resourceIntensiveTask = ScheduledActionService.Find(resourceIntensiveTaskName) as ResourceIntensiveTask;

            // If the task already exists and background agents are enabled for the
            // application, you must remove the task and then add it again to update 
            // the schedule.
            if (resourceIntensiveTask != null)
            {
                RemoveAgent(resourceIntensiveTaskName);
            }

            resourceIntensiveTask = new ResourceIntensiveTask(resourceIntensiveTaskName);

            // The description is required for periodic agents. This is the string that the user
            // will see in the background services Settings page on the device.
            resourceIntensiveTask.Description = "This demonstrates a resource-intensive task.";

            // Place the call to Add in a try block in case the user has disabled agents.
            try
            {
                ScheduledActionService.Add(resourceIntensiveTask);
                //ResourceIntensiveStackPanel.DataContext = resourceIntensiveTask;

                // If debugging is enabled, use LaunchForTest to launch the agent in one minute.

#if(DEBUG_AGENT)
                ScheduledActionService.LaunchForTest(resourceIntensiveTask.Name, TimeSpan.FromSeconds(10));
#endif

            }
            catch (InvalidOperationException exception)
            {
                if (exception.Message.Contains("BNS Error: The action is disabled"))
                {
                    MessageBox.Show("Background agents for this application have been disabled by the user.");
                    agentsAreEnabled = false;

                }
                // ResourceIntensiveCheckBox.IsChecked = false;
            }
            catch (SchedulerServiceException)
            {
                // No user action required.
                //   ResourceIntensiveCheckBox.IsChecked = false;
            }
        }


        ///background
        private void RemoveAgent(string name)
        {
            try
            {
                ScheduledActionService.Remove(name);
            }
            catch (Exception)
            {
            }
        }
    }
}
