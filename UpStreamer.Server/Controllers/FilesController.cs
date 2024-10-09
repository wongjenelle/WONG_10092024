using MediatR;
using Microsoft.AspNetCore.Mvc;
using UpStreamer.Server.Features.Files.Handlers;

namespace UpStreamer.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilesController(IMediator mediator) : ControllerBase
    {
        /// <summary>
        /// Upload a file to the application server
        /// </summary>
        /// <returns></returns>
        [HttpPost, DisableRequestSizeLimit] //todo mitigate
        public async Task<IActionResult> Upload()
        {
            await mediator.Send(new UploadFileCommand(Request.Form.Files));
            return Ok();
        }
    }
}
