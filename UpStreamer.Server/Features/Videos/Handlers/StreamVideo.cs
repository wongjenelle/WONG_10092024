using Microsoft.AspNetCore.Mvc;

namespace UpStreamer.Server.Features.Video.Handlers
{
    public class StreamVideo : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
