namespace Data.Models;

public class Department
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int? CityCapitalId { get; set; }
    public int? Municipalities { get; set; }
    public float Surface { get; set; }
    public float? Population { get; set; }
    public string? PhonePrefix { get; set; }
    public int CountryId { get; set; }
    public virtual City CityCapital { get; set; } = null!;
    public virtual Country Country { get; set; } = null!;
    public ICollection<City> Cities { get; set; } = null!;
    public int? RegionId { get; set; }
    public virtual Region Region { get; set; } = null!;
    public ICollection<NaturalArea> NaturalAreas { get; set; } = null!;
    public ICollection<Map> Maps { get; set; } = null!;
    public virtual ICollection<IndigenousReservation> IndigenousReservations { get; set; } = null!;
    public virtual ICollection<Airport> Airports { get; set; } = null!;

}