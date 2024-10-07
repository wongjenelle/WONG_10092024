using Microsoft.AspNetCore.Mvc;

namespace UpStreamer.Server.Features.Video.Handlers
{
    public class GetVideos : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
