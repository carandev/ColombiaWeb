using Data.Utils;
using Microsoft.AspNetCore.Components;

namespace Client.Components.Shared;

public partial class Pagination : ComponentBase
{
    [EditorRequired] [Parameter] public int PageNumber { get; set; }

    [EditorRequired] [Parameter] public int PageSize { get; set; }

    [EditorRequired] [Parameter] public int Total { get; set; }

    [EditorRequired] [Parameter] public int PageCount { get; set; }

    [EditorRequired] [Parameter] public EventCallback OnNextPage { get; set; }

    [EditorRequired] [Parameter] public EventCallback OnPreviousPage { get; set; }

    private async Task NextPage()
    {
        await OnNextPage.InvokeAsync();
    }

    private async Task PreviousPage()
    {
        await OnPreviousPage.InvokeAsync();
    }
}