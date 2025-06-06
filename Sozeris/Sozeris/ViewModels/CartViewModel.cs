using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Sozeris.Logic.Services.Interfaces;
using Sozeris.Models.Entities;

namespace Sozeris.ViewModels;

public class CartViewModel : INotifyPropertyChanged
{
    private readonly ICartService _cartService;

    public ObservableCollection<CartItem> CartItems { get; set; } = new();

    private decimal _totalPrice;
    public decimal TotalPrice
    {
        get => _totalPrice;
        set
        {
            _totalPrice = value;
            OnPropertyChanged();
        }
    }

    public ICommand RemoveCommand { get; }
    public ICommand SubscribeCommand { get; }

    public CartViewModel(ICartService cartService)
    {
        _cartService = cartService;
        LoadCart();
        RemoveCommand = new Command<CartItem>(RemoveItem);
        SubscribeCommand = new Command(OnSubscribe);
    }
    
    private void OnSubscribe()
    {
        Application.Current.MainPage.DisplayAlert("Подписка", "Подписка успешно оформлена", "OK");
    }
    
    public bool HasItems => CartItems.Any();

    public void LoadCart()
    {
        CartItems.Clear();
        foreach (var item in _cartService.CartItems)
            CartItems.Add(item);

        CalculateTotal();
        OnPropertyChanged(nameof(HasItems));
    }

    private void RemoveItem(CartItem item)
    {
        _cartService.RemoveFromCart(item);
        CartItems.Remove(item);
        CalculateTotal();
        OnPropertyChanged(nameof(HasItems));
    }

    private void CalculateTotal()
    {
        TotalPrice = CartItems.Sum(x => x.TotalPrice);
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}