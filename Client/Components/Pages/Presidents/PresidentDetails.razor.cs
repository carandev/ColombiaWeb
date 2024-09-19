using Data.Models;
using Data.Services;
using Microsoft.AspNetCore.Components;

namespace Client.Components.Pages.Presidents;

/// <summary>
///     Vista de detalle del presidente.
/// </summary>
public partial class PresidentDetails
{
    /// <summary>
    ///     Servicio para consulta de presidentes en el API Colombia.
    /// </summary>
    [Inject] public PresidentService PresidentSrv { get; set; } = null!;

    /// <summary>
    ///     Obtiene la información del presidente.
    /// </summary>
    /// <returns>El modelo de presidente</returns>
    protected override async Task<President?> GetData()
    {
        return await PresidentSrv.GetPresidentById(EntityId);
    }
}