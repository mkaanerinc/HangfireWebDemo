using System.Drawing;
using Hangfire;

namespace HangfireWebDemo.BackgroundJobs
{
    public class DelayedJobs
    {
        public static string AddWatermarkJob(string fileName, string watermarkText)
        {
            return BackgroundJob.Schedule(() => ApplyWatermark(fileName,watermarkText),
                TimeSpan.FromSeconds(20));
        }

        public static void ApplyWatermark(string fileName, string watermarkText)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/pictures",fileName);

            using (var bitmap = Bitmap.FromFile(path))
            {
                using (Bitmap tempBitmap = new (bitmap.Width,bitmap.Height))
                {
                    using (Graphics grp = Graphics.FromImage(tempBitmap))
                    {
                        grp.DrawImage(bitmap, 0, 0);

                        var font = new Font(FontFamily.GenericSansSerif,25,FontStyle.Bold);
                        var color = Color.FromArgb(255,0,0);
                        var brush = new SolidBrush(color);
                        var point = new Point(20,bitmap.Height-50);

                        grp.DrawString(watermarkText,font,brush,point);

                        tempBitmap.Save(Path.Combine(Directory.GetCurrentDirectory(), 
                            "wwwroot/pictures/watermarks",fileName));
                    }
                }
            }
        }
    }
}
