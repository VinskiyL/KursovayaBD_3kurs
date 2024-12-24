using Quartz.Impl;
using Quartz;
using System.Threading.Tasks;

namespace Library
{
    internal class Scheduler
    {
        public static async Task Start()
        {
            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            await scheduler.Start();

            IJobDetail job = JobBuilder.Create<BookDebtorsJob>()
                .WithIdentity("BookDebtorsJob", "group1")
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("BookDebtorsTrigger", "group1")
                .StartNow()
                //.WithSchedule(CronScheduleBuilder.CronSchedule("0 0/5 * * * ?"))//5min
                .WithSchedule(CronScheduleBuilder.CronSchedule("0 0 12 ? * THU"))
                .Build();

            await scheduler.ScheduleJob(job, trigger);
        }
    }
}
