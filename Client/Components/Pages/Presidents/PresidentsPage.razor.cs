using Client.Utils;
using Data.Services;
using Microsoft.AspNetCore.Components;

namespace Client.Components.Pages.Presidents;

public partial class PresidentsPage
{
    [Inject] public PresidentService PresidentSrv { get; set; } = null!;

    protected override string PageRoute => PageRoutes.Presidents;

    protected override async Task GetData()
    {
        PaginatedModel = await PresidentSrv.GetPaginatedPresidents(PageNumber, PageSize);
    }
}