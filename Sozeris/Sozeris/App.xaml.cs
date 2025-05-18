using Sozeris.Logic.Services.Interfaces;
using Sozeris.Pages;

namespace Sozeris;

public partial class App : Application
{
    private readonly IServiceProvider _services;

    public App(IServiceProvider services)
    {
        InitializeComponent();
        _services = services;
    }
    protected override Window CreateWindow(IActivationState? activationState)
    {
        var window = new Window(new LoadingPage());

        _ = InitializeAppAsync(window);

        return window;
    }

    private async Task InitializeAppAsync(Window window)
    {
        var sessionService = _services.GetRequiredService<IUserSessionService>();

        bool isAuthenticated = await sessionService.IsAuthenticatedAsync();

        await MainThread.InvokeOnMainThreadAsync(async () =>
        {
            var shell = new AppShell();
            window.Page = shell;

            if (isAuthenticated)
                await shell.GoToAsync("//home");
            else
                await shell.GoToAsync("//login");
        });
    }
}