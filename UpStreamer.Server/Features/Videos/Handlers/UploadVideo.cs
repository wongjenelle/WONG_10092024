using Microsoft.AspNetCore.Mvc;

namespace UpStreamer.Server.Features.Video.Handlers
{
    public class UploadVideo : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
