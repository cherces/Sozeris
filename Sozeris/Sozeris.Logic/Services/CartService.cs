using Sozeris.Logic.Services.Interfaces;
using Sozeris.Models.Entities;

namespace Sozeris.Logic.Services;

public class CartService : ICartService
{
    public List<CartItem> CartItems { get; set; } = new();

    public void AddToCart(Product product, int quantity)
    {
        var existing = CartItems.FirstOrDefault(x => x.Product.Id == product.Id);
        if (existing != null)
        {
            existing.Quantity += quantity;
        }
        else
        {
            CartItems.Add(new CartItem
            {
                Product = product,
                Quantity = quantity
            });
        }
    }
    
    public void RemoveFromCart(CartItem item)
    {
        CartItems.Remove(item);
    }

    public void ClearCarts() => CartItems.Clear();
}