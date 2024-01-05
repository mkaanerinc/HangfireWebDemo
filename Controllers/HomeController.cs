using HangfireWebDemo.BackgroundJobs;
using HangfireWebDemo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HangfireWebDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            RecurringJobs.ReportingJob();

            return View();
        }


        public IActionResult SignUp()
        {
            // User sign up the our site. After that our EmailSender Job execute.
            
            FireAndForgetJobs.EmailSendToUserJob("User Id","Sitemize hoþgeldiniz...");

            return View();
        }

        public IActionResult PictureSave()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PictureSave(IFormFile picture)
        {
            string newFileName = String.Empty;

            if (picture != null && picture.Length > 0)
            {
                newFileName = Guid.NewGuid().ToString() + Path.GetExtension(picture.FileName);

                var path = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/pictures",newFileName);

                using (var stream = new FileStream(path,FileMode.Create))
                {
                    await picture.CopyToAsync(stream);
                }

                string jobId = DelayedJobs.AddWatermarkJob(newFileName,"www.watermark.com");

                ContinuationsJobs.WriteWatermarkStatusJob(jobId,newFileName);
            }

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
