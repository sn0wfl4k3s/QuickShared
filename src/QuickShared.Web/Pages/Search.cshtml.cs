using Mediator;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuickShared.Application.Features.GetFilesByName;

namespace QuickShared.Web.Pages;

public class SearchModel : PageModel
{
    private readonly IMediator _mediator;

    public SearchModel(IMediator mediator)
    {
        _mediator = mediator;
    }

    [BindProperty(SupportsGet = true)]
    public string FileName { get; set; }

    public GetFilesByNameResponse FilesFound { get; set; }

    public async Task OnGetAsync()
    {
        if (!string.IsNullOrEmpty(FileName))
        {
            var result = await _mediator.Send(new GetFilesByNameRequest(FileName));
            if(result is not null)
            {
                FilesFound = result;
            }
        }
    }
}