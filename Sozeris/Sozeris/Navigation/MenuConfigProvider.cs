using Sozeris.Models.Config;
using Sozeris.Models.Enums;
using Sozeris.Pages;

namespace Sozeris.Navigation;

public class MenuConfigProvider
{
    public static IEnumerable<MenuItemConfig> GetPublicRoutes() => 
        new[]
        {
            new MenuItemConfig
            {
                Title = "Вход",
                PageType = typeof(LoginPage),
                Route = "login",
                Order = 99
            }
        };
    
    public static IEnumerable<MenuItemConfig> GetCommonMenuItems() =>
        new[]
        {
            new MenuItemConfig
            {
                Title = "Главная",
                PageType = typeof(HomePage),
                Route = "home",
                Order = 0
            },
            new MenuItemConfig
            {
                Title = "Профиль",
                PageType = typeof(UserProfilePage),
                Route = "profile",
                Order = 1
            }
        };

    public static IEnumerable<MenuItemConfig> GetAdminMenuItems() =>
        new[]
        {
            new MenuItemConfig
            {
                Title = "Регистрация пользователя",
                PageType = typeof(UserRegistrationPage),
                Route = "admin/userRegistration",
                Order = 2
            },
        };

    public static IEnumerable<MenuItemConfig> GetDeliveryMenuItems() =>
        new[]
        {
            new MenuItemConfig
            {
                Title = "Регистрация пользователя",
                PageType = typeof(UserRegistrationPage),
                Route = "delivery/userRegistration",
                Order = 2
            }
        };

    public static IEnumerable<MenuItemConfig> GetUserMenuItems() =>
        new[]
        {
            new MenuItemConfig
            {
                Title = "История доставок",
                PageType = typeof(UserSubscriptionPage),
                Route = "subscriptions",
                Order = 2
            },
            new MenuItemConfig
            {
                Title = "Каталог",
                PageType = typeof(ProductsCatalogPage),
                Route = "productsCatalog",
                Order = 3
            },
            new MenuItemConfig
            {
                Title = "Корзина",
                PageType = typeof(CartPage),
                Route = "cart",
                Order = 4
            }
        };

    public static IEnumerable<MenuItemConfig> GetMenuByRole(UserRole role)
    {
        var common = GetCommonMenuItems();

        return role switch
        {
            UserRole.Admin => common.Concat(GetAdminMenuItems()),
            UserRole.Delivery => common.Concat(GetDeliveryMenuItems()),
            UserRole.User => common.Concat(GetUserMenuItems()),
            _ => common
        };
    }
    
    /// <summary>
    /// Возвращает все уникальные маршруты (для регистрации).
    /// </summary>
    public static IEnumerable<MenuItemConfig> GetAllRoutes()
    {
        var all = GetCommonMenuItems()
            .Concat(GetAdminMenuItems())
            .Concat(GetDeliveryMenuItems())
            .Concat(GetUserMenuItems())
            .Concat(GetPublicRoutes());

        return all
            .GroupBy(x => x.Route)
            .Select(g => g.First());
    }
}