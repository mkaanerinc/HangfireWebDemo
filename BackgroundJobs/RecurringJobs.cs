using Hangfire;
using System.Diagnostics;

namespace HangfireWebDemo.BackgroundJobs
{
    public class RecurringJobs
    {
        public static void ReportingJob()
        {
            RecurringJob.AddOrUpdate("given jobId", () => EmailReport(), Cron.Minutely);
        }

        public static void EmailReport()
        {
            Debug.WriteLine("Rapor, email olarak gönderilmiştir...");
        }
    }
}
