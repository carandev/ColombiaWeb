namespace Data.Models;

public class Airport
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string IataCode { get; set; } = null!;
    public string OaciCode { get; set; } = null!;
    public string Type { get; set; } = null!;
    public int DeparmentId { get; set; }
    public virtual Department Department { get; set; } = null!;
    public int CityId { get; set; }
    public virtual City City { get; set; } = null!;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}