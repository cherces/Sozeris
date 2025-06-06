using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Sozeris.Logic.Services;
using Sozeris.Logic.Services.Interfaces;
using Sozeris.Models.Entities;

namespace Sozeris.ViewModels;

public class ProductsCatalogViewModel : INotifyPropertyChanged
{
    private readonly IProductService _productService;
    private readonly ICartService _cartService;

    public ObservableCollection<Product> Products { get; set; } = new();

    private Product _selectedProduct;
    public Product SelectedProduct
    {
        get => _selectedProduct;
        set
        {
            _selectedProduct = value;
            OnPropertyChanged();
        }
    }

    private int _quantity = 1;
    public int Quantity
    {
        get => _quantity;
        set
        {
            if (value > 0)
            {
                _quantity = value;
                OnPropertyChanged();
            }
        }
    }

    private bool _isPopupVisible;
    public bool IsPopupVisible
    {
        get => _isPopupVisible;
        set
        {
            _isPopupVisible = value;
            OnPropertyChanged();
        }
    }
    
    private bool _isToastVisible;
    
    public bool IsToastVisible
    {
        get => _isToastVisible;
        set
        {
            _isToastVisible = value;
            OnPropertyChanged();
        }
    }

    private string _toastText;
    public string ToastText
    {
        get => _toastText;
        set
        {
            _toastText = value;
            OnPropertyChanged();
        }
    }

    public ICommand LoadCommand { get; }
    public ICommand AddCommand { get; }
    public ICommand ConfirmCommand { get; }
    public ICommand IncreaseCommand { get; }
    public ICommand DecreaseCommand { get; }

    public ProductsCatalogViewModel(IProductService productService, ICartService cartService)
    {
        _productService = productService;
        _cartService = cartService;
        
        LoadCommand = new Command(async () => await LoadProducts());
        AddCommand = new Command<Product>(ShowPopup);
        ConfirmCommand = new Command(ConfirmQuantity);
        IncreaseCommand = new Command(() => Quantity++);
        DecreaseCommand = new Command(() => Quantity--);

        LoadCommand.Execute(null);
    }

    private async Task LoadProducts()
    {
        var items = await _productService.GetAllProductsAsync();
        Products.Clear();
        foreach (var item in items)
            Products.Add(item);
    }

    private void ShowPopup(Product product)
    {
        SelectedProduct = product;
        Quantity = 1;
        IsPopupVisible = true;
    }
    
    public async Task ShowToastAsync(string message)
    {
        ToastText = message;
        IsToastVisible = true;

        await Task.Delay(500);

        IsToastVisible = false;
    }

    private async void ConfirmQuantity()
    {
        IsPopupVisible = false;
        _cartService.AddToCart(SelectedProduct, Quantity);
        await ShowToastAsync($"{Quantity} × {SelectedProduct.Name} добавлено в корзину");
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}