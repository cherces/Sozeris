namespace Sozeris.Server.Domain.Entities;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public decimal Price { get; set; }
    public byte[] Image { get; set; } = Array.Empty<byte>();
    public bool IsActive { get; set; } = true;
}