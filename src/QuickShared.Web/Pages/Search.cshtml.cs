using Mediator;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuickShared.Application.Features.GetFile;
using QuickShared.Application.Features.GetFilesByName;

namespace QuickShared.Web.Pages;

public class SearchModel(IMediator mediator) : PageModel
{
    private readonly IMediator _mediator = mediator;

    [BindProperty(SupportsGet = true)]
    public string? FileName { get; set; }

    public GetFilesByNameResponse? FilesFound { get; set; }

    public async Task OnGetAsync()
    {
        if (!string.IsNullOrEmpty(FileName))
        {
            var result = await _mediator.Send(new GetFilesByNameRequest(FileName));

            if (result.IsSuccess)
            {
                FilesFound = result.Value;
            }
        }
    }

    public async Task<IActionResult> OnGetDownloadAsync(Guid fileId)
    {
        var result = await _mediator.Send(new GetFileRequest(fileId));

        if (result.IsFailed)
        {
            return NotFound();
        }

        return File(result.Value.FileBytes, result.Value.ContentType, result.Value.FileName);
    }
}
