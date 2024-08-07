using Data.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Client.Utils;

public abstract class BasePageWithPagination<T> : ComponentBase
{
    [Inject] public IJSRuntime JS { get; set; } = null!;

    [SupplyParameterFromQuery] public int PageSize { get; set; }

    [SupplyParameterFromQuery] protected int PageNumber { get; set; }

    protected bool LoadingData { get; set; }

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
        }
    }

    protected async Task PreviousPage()
    {
        if (PageNumber > 1)
        {
            PageNumber--;
            await GetData();
            await ScrollToUp();
        }
    }
    
   private async Task ScrollToUp()
   {
      await JS.InvokeVoidAsync("scroll", "0", "0");
   }
}