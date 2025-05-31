using System.ComponentModel;
using System.Windows.Input;
using Sozeris.Logic.Services.Interfaces;
using Sozeris.Models;
using Sozeris.Models.Commons;

namespace Sozeris.ViewModels;

public class LoginViewModel : INotifyPropertyChanged
{
    private readonly IAuthService _authService;
    private readonly IUserSessionService _sessionService;
    private LoginModel _loginModel;
    private string _errorMessage = string.Empty;
    private bool _isBusy;

    public event PropertyChangedEventHandler PropertyChanged;
    
    public event EventHandler LoginSucceeded;
    
    public ICommand LoginCommand { get; }
    
    public LoginViewModel(IAuthService authService, IUserSessionService userSessionService)
    {
        _authService = authService;
        _sessionService = userSessionService;
        LoginCommand = new Command(async () => await OnLogin(), () => !IsBusy);
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
    
    public bool HasError => !string.IsNullOrEmpty(ErrorMessage);

    public bool IsBusy
    {
        get => _isBusy;
        set
        {
            _isBusy = value;
            OnPropertyChanged(nameof(IsBusy));
            (LoginCommand as Command)?.ChangeCanExecute();
        }
    }
    
    private async Task OnLogin()
    {
        if (string.IsNullOrWhiteSpace(LoginModel?.Username) || string.IsNullOrWhiteSpace(LoginModel?.Password))
        {
            ErrorMessage = "Имя пользователя и пароль не могут быть пустыми.";
            return;
        }

        IsBusy = true;
        ErrorMessage = string.Empty;

        try
        {
            JwtTokenModel tokens = await _authService.LoginAsync(LoginModel);

            if (string.IsNullOrWhiteSpace(tokens?.AccessToken))
            {
                ErrorMessage = "Не удалось получить токены.";
                return;
            }

            await _sessionService.SaveTokensAsync(tokens);

            LoginSucceeded?.Invoke(this, EventArgs.Empty);
        }
        catch (UnauthorizedAccessException ex)
        {
            ErrorMessage = "Неверные учетные данные.";
        }
        catch (Exception ex)
        {
            ErrorMessage = "Произошла ошибка: " + ex.Message;
        }
        finally
        {
            IsBusy = false;
        }
    }
    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}