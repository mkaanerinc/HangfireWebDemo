using Hangfire;
using HangfireWebDemo.Services;

namespace HangfireWebDemo.BackgroundJobs
{
    public class FireAndForgetJobs
    {
        public static void EmailSendToUserJob(string userId, string message)
        {
            BackgroundJob.Enqueue<IEmailSender>(x => x.Sender(userId,message));
        }
    }
}
