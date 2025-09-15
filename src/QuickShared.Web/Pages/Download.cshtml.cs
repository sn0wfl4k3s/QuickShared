using Mediator;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuickShared.Application.Features.GetFile;
using QuickShared.Application.Features.GetFileInfo;

namespace QuickShared.Web.Pages
{
    public class DownloadModel(IMediator mediator) : PageModel
    {
        private readonly IMediator _mediator = mediator;

        [BindProperty]
        public GetFileInfoResponse FileInfo { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var result = await _mediator.Send(new GetFileInfoRequest(id));

            if (result.IsFailed)
                return NotFound();

            FileInfo = result.Value;

            return Page();
        }

        public async Task<IActionResult> OnPostDownloadAsync(Guid fileId)
        {
            var result = await _mediator.Send(new GetFileRequest(fileId));

            if (result.IsFailed)
            {
                return NotFound();
            }

            return File(result.Value.FileBytes, result.Value.ContentType, result.Value.FileName);
        }
    }
}
