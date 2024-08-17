using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;

namespace Client.Utils;

public abstract class BasePageWithPagination<T> : ComponentBase
{
    protected bool LoadingData { get; set; }

    protected abstract string PageRoute { get; }

    protected GridItemsProvider<T> DataProvider = null!;
    
    protected readonly PaginationState Pagination = new()
    {
            ItemsPerPage = 10
        };

    protected override Task OnInitializedAsync()
    {
        try
        {
            LoadingData = true;
            StateHasChanged();

            DataProvider = GetData;

            LoadingData = false;
            StateHasChanged();
            return Task.CompletedTask;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    protected abstract ValueTask<GridItemsProviderResult<T>> GetData(GridItemsProviderRequest<T> request);
}