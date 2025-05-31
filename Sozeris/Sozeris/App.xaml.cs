using System.Diagnostics;
using Sozeris.Logic.Services.Interfaces;
using Sozeris.Models.Enums;
using Sozeris.Pages;

namespace Sozeris;

public partial class App
{
    private readonly IServiceProvider _services;
    private Window _mainWindow;
    
    public App(IServiceProvider services)
    {
        InitializeComponent();
        _services = services;
    }
    
    protected override Window CreateWindow(IActivationState? activationState)
    {
        _mainWindow = new Window(new LoadingPage());

        _ = InitializeAppAsync(_mainWindow);

        return _mainWindow;
    }

    private async Task InitializeAppAsync(Window window)
    {
        var sessionService = _services.GetRequiredService<IUserSessionService>();

        var isAuthenticated = await sessionService.IsAuthenticatedAsync();
        isAuthenticated = true;

        if (isAuthenticated)
        {
            UserRole role = UserRole.User; //isAuthenticated
            //? await sessionService.GetRoleAsync() ?? UserRole.User
            //: (UserRole?)null;
            _mainWindow.Page = new AppShell(role);
        }
        else
        {
            var loginPage = _services.GetRequiredService<LoginPage>();
            loginPage.LoginSucceeded += OnLoginSucceeded;
            _mainWindow.Page = loginPage;
        }
    }
    
    private void OnLoginSucceeded(object? sender, EventArgs e)
    {
        if (_mainWindow == null) return;

        var sessionService = _services.GetRequiredService<IUserSessionService>();
        
        UserRole role = UserRole.User; //isAuthenticated
        //? await sessionService.GetRoleAsync() ?? UserRole.User
        //: (UserRole?)null;
        
        _mainWindow.Page = new AppShell(role);
    }
}