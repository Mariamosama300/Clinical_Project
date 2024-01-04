using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Microsoft.Xaml.Behaviors;
using Syncfusion.UI.Xaml.Scheduler;

namespace WpfScheduler
{
    public class SchedulerBehavior : Behavior<SfScheduler>
    {
        SfScheduler scheduler;
        protected override void OnAttached()
        {
            base.OnAttached();
            scheduler = this.AssociatedObject;
            scheduler.DaysViewSettings.SpecialTimeRegions.Add(new SpecialTimeRegion
            {
                StartTime = DateTime.Today.AddHours(13),
                EndTime = DateTime.Today.AddHours(14),
                Text = "Lunch",
                CanEdit = false,
                Background = Brushes.Black,
                Foreground = Brushes.White,
                CanMergeAdjacentRegions = true,
                RecurrenceRule = "FREQ=DAILY;INTERVAL=1"
            });
        }
        protected override void OnDetaching()
        {
            base.OnDetaching();
            this.scheduler = null;
        }
    }
}
