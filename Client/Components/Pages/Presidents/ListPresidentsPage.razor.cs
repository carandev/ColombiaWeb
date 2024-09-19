using Data.Models;
using Data.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;

namespace Client.Components.Pages.Presidents;

/// <summary>
///     Página con un listado de presidentes.
/// </summary>
public partial class ListPresidentsPage
{
    /// <summary>
    ///     Servicio que permite consultar los presidentes.
    /// </summary>
    [Inject] public PresidentService PresidentSrv { get; set; } = null!;

    /// <inheritdoc />
    protected override async ValueTask<GridItemsProviderResult<President>> GetData(
        GridItemsProviderRequest<President> request)
    {
        var pageNumber = request.StartIndex / 10 + 1;
        
        var paginatedModel = await PresidentSrv.GetPaginatedPresidents(pageNumber, request.Count);

        return GridItemsProviderResult.From(
            items: paginatedModel!.Data,
            totalItemCount: paginatedModel.TotalRecords);
    }
}