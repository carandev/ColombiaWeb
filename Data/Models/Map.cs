namespace Data.Models;
public class Map
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public int? DepartmentId { get; set; }
    public string[] UrlImages { get; set; } = null!;
    public string? UrlSource { get; set; }
    public virtual Department Department { get; set; } = null!;
}