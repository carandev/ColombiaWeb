using System.Net.Http.Json;
using Data.Models;
using Data.Utils;

namespace Data.Services;

/// <summary>
///     Servicio para gestionar las peticiones la API sobre presidentes.
/// </summary>
public class PresidentService(HttpClient httpClient)
{
    public async Task<ModelWithPagination<President>?> GetPaginatedPresidents()
    {
        var query = $"{SharedValues.ApiColombiaPresidentEndpoint}/pagedList?Page=1&PageSize=10";

        var response = await httpClient.GetAsync(query);

        if (response.IsSuccessStatusCode)
        {
            var presidents = await response.Content.ReadFromJsonAsync<ModelWithPagination<President>>();
            
            return presidents;
        }

        var message = await response.Content.ReadAsStringAsync();

        throw new ApplicationException($"Ocurrió un error al consultar los presidentes: {message}");
    }

    public async Task<President?> GetPresidentById(int presidentId)
    {
        var query = $"{SharedValues.ApiColombiaPresidentEndpoint}/{presidentId}";

        var response = await httpClient.GetAsync(query);

        if (response.IsSuccessStatusCode)
        {
            var president = await response.Content.ReadFromJsonAsync<President>();

            return president;
        }

        var message = await response.Content.ReadAsStringAsync();
        
        throw new ApplicationException($"Ocurrió un error al consultar el presidente: {message}");
    }
}