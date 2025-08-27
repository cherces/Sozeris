namespace Sozeris.Server.Domain.Entities;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public byte[] Image { get; set; }

    public void CloneFrom(Product product)
    {
        Name = product.Name;
        Price = product.Price;
        Image = product.Image;
    }
}