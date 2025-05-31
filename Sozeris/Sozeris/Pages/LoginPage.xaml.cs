using Sozeris.Logic.Services.Interfaces;
using Sozeris.Models.Enums;
using Sozeris.ViewModels;

namespace Sozeris.Pages;

public partial class LoginPage
{ 
    public event EventHandler? LoginSucceeded;
    
    public LoginPage(LoginViewModel loginViewModel)
    {
        InitializeComponent();
        BindingContext = loginViewModel;
        
        loginViewModel.LoginSucceeded += (s, e) =>
        {
            LoginSucceeded?.Invoke(this, EventArgs.Empty);
        };
    }
}