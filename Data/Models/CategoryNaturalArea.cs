namespace Data.Models;
public class CategoryNaturalArea
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public ICollection<NaturalArea> NaturalAreas { get; set; } = null!;
}