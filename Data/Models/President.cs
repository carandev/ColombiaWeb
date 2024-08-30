namespace Data.Models;

/// <summary>
///     Modelo de presidente.
/// </summary>
public class President
{
    /// <summary>
    ///     Identificador.
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    ///     Imagen.
    /// </summary>
    public string? Image { get; set; }
    
    /// <summary>
    ///     Nombre.
    /// </summary>
    public string? Name { get; set; }
    
    /// <summary>
    ///     Apellido.
    /// </summary>
    public string? LastName { get; set; }
    
    /// <summary>
    ///     Fecha de inicio del periodo de gobierno.
    /// </summary>
    public DateTime StartPeriodDate { get; set; }
    
    /// <summary>
    ///     Fecha de fin del periodo de gobierno.
    /// </summary>
    public DateTime? EndPeriodDate { get; set; }
    
    /// <summary>
    ///     Partido político.
    /// </summary>
    public string? PoliticalParty { get; set; }
    
    /// <summary>
    ///     Descripción.
    /// </summary>
    public string? Description { get; set; }
    
    /// <summary>
    ///     Identificador de la ciudad de nacimiento.
    /// </summary>
    public int CityId { get; set; }
    
    /// <summary>
    ///     Ciudad de nacimiento.
    /// </summary>
    public virtual City City { get; set; } = null!;
}