namespace Data.Models;

public class Country
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? StateCapital { get; set; }
    public float Surface { get; set; }
    public float Population { get; set; }
    public string?[] Languages { get; set; } = null!;
    public string? TimeZone { get; set; }
    public string? Currency { get; set; }
    public string? CurrencyCode { get; set; }
    public string? CurrencySymbol { get; set; }
    public string? ISOCode { get; set; }
    public string? InternetDomain { get; set; }
    public string? PhonePrefix { get; set; }
    public string? RadioPrefix { get; set; }
    public string? AircraftPrefix { get; set; }
    public string? SubRegion { get; set; }
    public string? Region { get; set; }
    public string[] Borders { get; set; } = null!;
    public string[] Flags { get; set; } = null!;

    public virtual ICollection<Department> Departments { get; set; } = null!;
    public virtual ICollection<President> Presidents { get; set; } = null!;
}
