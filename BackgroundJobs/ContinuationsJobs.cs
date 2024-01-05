using Hangfire;
using System.Diagnostics;

namespace HangfireWebDemo.BackgroundJobs
{
    public class ContinuationsJobs
    {
        public static void WriteWatermarkStatusJob(string jobId, string fileName)
        {
            BackgroundJob.ContinueJobWith(jobId, () => 
            Debug.WriteLine($"{fileName} resminin watermark işlemi tamamlandı..."));
        }
    }
}
