using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;

namespace Client.Utils;

/// <summary>
///     Página base con paginación.
/// </summary>
/// <typeparam name="T">Modelo que se va a paginar</typeparam>
public abstract class BasePageWithPagination<T> : ComponentBase
{
    /// <summary>
    ///     Indica si está cargando información.
    /// </summary>
    protected bool LoadingData { get; set; }

    /// <summary>
    ///     Proveedor de elementos para la tabla con paginación.
    /// </summary>
    protected GridItemsProvider<T> DataProvider = null!;

    /// <summary>
    ///     Estado de la paginación, indicando la cantidad de elementos por página.
    /// </summary>
    protected readonly PaginationState Pagination = new()
    {
        ItemsPerPage = 10
    };

    /// <inheritdoc />
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

    /// <summary>
    ///     Obtiene la información necesaria para alimentar la tabla con paginación.
    /// </summary>
    /// <param name="request">Solicitud enviada por la tabla indicando la página y otros datos</param>
    /// <returns>El resultado de la petición</returns>
    protected abstract ValueTask<GridItemsProviderResult<T>> GetData(GridItemsProviderRequest<T> request);
}