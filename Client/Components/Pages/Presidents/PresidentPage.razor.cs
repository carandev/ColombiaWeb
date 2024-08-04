using Data.Models;
using Data.Services;
using Data.Utils;
using Microsoft.AspNetCore.Components;

namespace Client.Components.Pages.Presidents;

public partial class PresidentPage : ComponentBase
{
    [Inject] public PresidentService PresidentSrv { get; set; } = null!;

    private ModelWithPagination<President>? PaginatedPresidents { get; set; }

    protected override async Task OnInitializedAsync()
    {
        try
        {
            PaginatedPresidents = await PresidentSrv.GetPaginatedPresidents();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }
}