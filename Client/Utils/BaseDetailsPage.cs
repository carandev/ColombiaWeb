using Microsoft.AspNetCore.Components;

namespace Client.Utils;

/// <summary>
///     Página base para los detalles de un modelo.
/// </summary>
/// <typeparam name="T">Modelo para mostrar los detalles</typeparam>
public abstract class BaseDetailsPage<T> : ComponentBase
{
    /// <summary>
    ///     Gestor de navegación.
    /// </summary>
    [Inject] public NavigationManager NavMgr { get; set; } = null!;
    
    /// <summary>
    ///     Identificador de la entidad.
    /// </summary>
    [Parameter] public int EntityId { get; set; }
    
    /// <summary>
    ///     Indica si está cargando información.
    /// </summary>
    protected bool LoadingData { get; set; }

    /// <summary>
    ///     Modelo que mostrara los detalles.
    /// </summary>
    protected T? Entity { get; set; }

    /// <inheritdoc />
    protected override async Task OnInitializedAsync()
    {
        try
        {
            LoadingData = true;
            StateHasChanged();

            Entity = await GetData();

            LoadingData = false;
            StateHasChanged();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    /// <summary>
    ///     Obtiene la información necesaria del modelo solicitado.
    /// </summary>
    protected abstract Task<T?> GetData();
}