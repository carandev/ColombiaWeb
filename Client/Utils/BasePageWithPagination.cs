using Data.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Client.Utils;

public abstract class BasePageWithPagination<T> : ComponentBase
{
    [Inject] public IJSRuntime JS { get; set; } = null!;
    [Inject] public NavigationManager NavMgr { get; set; } = null!;

    [SupplyParameterFromQuery] public int PageSize { get; set; }

    [SupplyParameterFromQuery] protected int PageNumber { get; set; }

    protected bool LoadingData { get; set; }

    protected abstract string PageRoute { get; }

    protected ModelWithPagination<T>? PaginatedModel { get; set; }

    protected override async Task OnInitializedAsync()
    {
        try
        {
            LoadingData = true;
            StateHasChanged();

            await GetData();

            LoadingData = false;
            StateHasChanged();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    protected abstract Task GetData();

    protected async Task NextPage()
    {
        if (PaginatedModel != null && PaginatedModel.PageCount > PageNumber)
        {
            PageNumber++;
            await GetData();
            await ScrollToUp();
            NavMgr.NavigateTo($"{PageRoute}?pageNumber={PageNumber}&pageSize={PageSize}");
        }
    }

    protected async Task PreviousPage()
    {
        if (PageNumber > 1)
        {
            PageNumber--;
            await GetData();
            await ScrollToUp();
            NavMgr.NavigateTo($"{PageRoute}?pageNumber={PageNumber}&pageSize={PageSize}");
        }
    }
    
   private async Task ScrollToUp()
   {
      await JS.InvokeVoidAsync("scroll", "0", "0");
   }
}