using System.ComponentModel.DataAnnotations;

namespace Sozeris.Server.Models.Entities;

public class Product
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public decimal Price { get; set; }
}