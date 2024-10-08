using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using UpStreamer.Server.Features.Videos.Handlers;

namespace UpStreamer.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VideosController(IMediator mediator) : ControllerBase
    {
        [HttpGet("list")]
        public async Task<string> GetPaged()
        {
            return await mediator.Send(new GetVideosQuery());
        }

        [HttpGet("{id}")]
        public string GetSingle(int id)
        {
            return $"hello {id}";
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload()
        {
            var file = Request.Form.Files;

            var result = await mediator.Send(new UploadVideoCommand(file));

            return Ok(result);
        }
    }
}
