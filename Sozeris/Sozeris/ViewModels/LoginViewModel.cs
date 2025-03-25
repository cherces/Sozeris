using System.ComponentModel;
using System.Windows.Input;
using Sozeris.Models;
using Sozeris.Services;
using Sozeris.Services.Interfaces;

namespace Sozeris.ViewModels;

public class LoginViewModel : INotifyPropertyChanged
{
    private readonly IAuthService _authService;
    private LoginModel _loginModel;
    private string _errorMessage;

    public event PropertyChangedEventHandler PropertyChanged;
    public ICommand LoginCommand { get; }
    public LoginViewModel(IAuthService authService)
    {
        _authService = authService;
        _loginModel = new LoginModel();
        LoginCommand = new Command(OnLogin);
    }
    public LoginModel LoginModel
    {
        get => _loginModel;
        set
        {
            _loginModel = value;
            OnPropertyChanged(nameof(LoginModel));
        }
    }
    public string ErrorMessage
    {
        get => _errorMessage;
        set
        {
            _errorMessage = value;
            OnPropertyChanged(nameof(ErrorMessage));
        }
    }
    private async void OnLogin()
    {
        if (LoginModel == null || string.IsNullOrEmpty(LoginModel.Username) || string.IsNullOrEmpty(LoginModel.Password))
        {
            ErrorMessage = "Username and Password cannot be empty.";
            return;
        }
        
        try
        {
            await _authService.LoginAsync(loginModel: LoginModel);
            
            await Shell.Current.GoToAsync("//MainPage");
        }
        catch (UnauthorizedAccessException ex)
        {
            ErrorMessage = ex.Message;
        }
    }
    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}