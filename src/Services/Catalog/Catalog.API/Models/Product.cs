namespace Catalog.API.Models;

public class Product(
    Guid id,
    string name,
    string description,
    string imageFile,
    decimal price,
    List<string> categories)
{
    public Guid Id { get; set; } = id;
    public string Name { get; set; } = name;
    public string Description { get; set; } = description;
    public string ImageFile { get; set; } = imageFile;
    public decimal Price { get; set; } = price;
    public List<string> Categories { get; set; } = categories;
}
