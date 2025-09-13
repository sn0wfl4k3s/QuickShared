using Mediator;
using Microsoft.AspNetCore.Mvc;
using QuickShared.Application.Features.GetFile;
using QuickShared.Application.Features.SaveFile;

namespace QuickShared.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ManageFileController(IMediator mediator) : Controller
{
    [HttpPost("Upload")]
    public async ValueTask<IActionResult> Upload([FromForm] SaveFileCommand command)
    {
        var result = await mediator.Send(command);

        if (result.IsFailed)
        {
            return BadRequest(result.Errors);
        }

        return Ok(result);
    }

    [HttpGet("Download/{fileId:guid}")]
    public async ValueTask<IActionResult> Download(Guid fileId)
    {
        var result = await mediator.Send(new GetFileRequest(fileId));

        if (result.IsFailed)
        {
            return NotFound(result.Errors);
        }

        return File(result.Value.FileBytes, result.Value.ContentType, result.Value.FileName);
    }
}
