namespace Data.Models;
public class Radio
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public double Frequency { get; set; }
    public string Band { get; set; } = null!;
    public int CityId { get; set; }
    public virtual City City { get; set; } = null!;
    public Uri Url { get; set; } = null!;
    public string[] Streamers { get; set; } = null!;
}