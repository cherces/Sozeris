using System.Diagnostics;
using Sozeris.Models.Enums;
using Sozeris.Navigation;
using Sozeris.Pages;

namespace Sozeris;

public partial class AppShell
{
    public AppShell()
    {
        InitializeComponent();
        RegisterRoutes();
    }
    
    public AppShell(UserRole role) : this()
    {
        ConfigureMenuByRole(role);
    }
    
    private void RegisterRoutes()
    {
        foreach (var route in MenuConfigProvider.GetAllRoutes())
        {
            Routing.RegisterRoute(route.Route, route.PageType);
        }
    }
    
    public void ConfigureMenuByRole(UserRole role)
    {
        Items.Clear();

        var menuItems = MenuConfigProvider
            .GetMenuByRole(role)
            .OrderBy(item => item.Order);

        foreach (var config in menuItems)
        {
            var shellContent = new ShellContent
            {
                Title = config.Title,
                ContentTemplate = new DataTemplate(config.PageType),
                Route = config.Route,
            };

            Items.Add(shellContent);
        }
    }
}