using Sozeris.ViewModels;

namespace Sozeris.Pages;

public partial class LoginPage
{
    public LoginPage(LoginViewModel loginViewModel)
    {
        InitializeComponent();
        BindingContext = loginViewModel;
    }
}