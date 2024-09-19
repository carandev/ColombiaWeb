using Data.Models;
using Data.Services;
using Microsoft.AspNetCore.Components;

namespace Client.Components.Pages.Presidents;

/// <summary>
///     Vista de detalle del presidente.
/// </summary>
public partial class PresidentDetails : ComponentBase
{
    /// <summary>
    ///     Identificador del presidente.
    /// </summary>
    [Parameter]
    public int PresidentId { get; set; }
    
    /// <summary>
    ///     Servicio para consulta de presidentes en el API Colombia.
    /// </summary>
    [Inject] public PresidentService PresidentSrv { get; set; } = null!;

    /// <summary>
    ///     Presidente.
    /// </summary>
    private President? _president;
    
    protected override async Task OnInitializedAsync()
    {
        _president = await PresidentSrv.GetPresidentById(PresidentId);
    }
}