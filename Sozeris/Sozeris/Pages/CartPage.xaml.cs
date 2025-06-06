using Sozeris.ViewModels;

namespace Sozeris.Pages;

public partial class CartPage : ContentPage
{
    private readonly CartViewModel _viewModel;

    public CartPage(CartViewModel cartViewModel)
    {
        InitializeComponent();
        _viewModel = cartViewModel;
        BindingContext = _viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.LoadCart();
    }
}