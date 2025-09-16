
using Mediator;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuickShared.Application.Features.SaveFile;

namespace QuickShared.Web.Pages;

public class IndexModel(ILogger<IndexModel> logger, IMediator mediator) : PageModel
{
    private readonly ILogger<IndexModel> _logger = logger;
    private readonly IMediator _mediator = mediator;

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostUploadAsync(IFormFile file)
    {
        try
        {
            var response = await _mediator.Send(new SaveFileCommand(file));

            if (response.IsFailed)
            {
                string errors = string.Join(", ", response.Errors.Select(e => e.Message));

                _logger.LogError("File upload failed: {Errors}", errors);

                return new JsonResult(new { success = false, message = errors });
            }

            return new JsonResult(new { success = true, fileId = response.Value.FileId });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error uploading file");

            return new JsonResult(new { success = false, message = "Error uploading file" });
        }
    }
}

