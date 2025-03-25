using Sozeris.Services;
using Sozeris.ViewModels;

namespace Sozeris.Pages;

public partial class LoginPage : ContentPage
{
    public LoginPage(LoginViewModel loginViewModel)
    {
        InitializeComponent();
        BindingContext = loginViewModel;
    }
}