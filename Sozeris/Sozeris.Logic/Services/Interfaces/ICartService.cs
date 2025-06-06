using Sozeris.Models.Entities;

namespace Sozeris.Logic.Services.Interfaces;

public interface ICartService
{
    List<CartItem> CartItems { get; }
    void AddToCart(Product product, int quantity);
    void RemoveFromCart(CartItem item);
    void ClearCarts();
}