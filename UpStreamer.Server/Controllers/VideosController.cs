using MediatR;
using Microsoft.AspNetCore.Mvc;
using UpStreamer.Server.Features.Videos.DTOs;
using UpStreamer.Server.Features.Videos.Handlers;

namespace UpStreamer.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VideosController(IMediator mediator) : ControllerBase
    {
        /// <summary>
        /// Get a paged list of the uploaded videos
        /// </summary>
        /// <returns>List of video metadata with pagination</returns>
        [HttpGet("list")]
        public async Task<string> GetPaged()
        {
            return await mediator.Send(new GetVideosQuery());
        }

        /// <summary>
        /// Get a video and its details by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Video details</returns>
        [HttpGet("{id}")]
        public string GetSingle(int id)
        {
            return $"hello {id}";
        }

        /// <summary>
        /// Create the video metadata
        /// </summary>
        /// <param name="request"></param>
        /// <returns>ID of the created video</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateVideoRequest request)
        {
            await mediator.Send(new CreateVideoCommand(request));
            return Ok();
        }
    }
}
