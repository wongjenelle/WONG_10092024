using Microsoft.AspNetCore.Mvc;

namespace UpStreamer.Server.Features.Videos.Handlers
{
    public class StreamVideo : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
