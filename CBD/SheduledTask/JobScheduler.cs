using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CBD.SheduledTask
{
    public class JobScheduler
    {
        public static void Start()
        {
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();

            IJobDetail job = JobBuilder.Create<EmailJob>().Build();

            ITrigger trigger = TriggerBuilder.Create()
                //.WithDailyTimeIntervalSchedule
                //  (s =>
                //     s.WithIntervalInMinutes(2)
                //    .OnEveryDay()
                //    .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(9, 57))
                //  )
                .Build();

            scheduler.ScheduleJob(job, trigger);
        }
    }
}