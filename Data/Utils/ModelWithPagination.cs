namespace Data.Utils;

/// <summary>
///     Modelo para la paginación del API.
/// </summary>
/// <typeparam name="T">Modelo al que se le aplicará la paginación</typeparam>
public class ModelWithPagination<T>
{
    /// <summary>
    ///     Página en la que se encuentra.
    /// </summary>
    public int Page { get; set; }
    
    /// <summary>
    ///     Cantidad de elementos por página.
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    ///     Cantidad total de elementos consultados.
    /// </summary>
    public int TotalRecords { get; set; }

    /// <summary>
    ///     Cantidad total de páginas.
    /// </summary>
    public int PageCount { get; set; }

    /// <summary>
    ///     Listado de elementos.
    /// </summary>
    public List<T> Data { get; set; } = null!;
}