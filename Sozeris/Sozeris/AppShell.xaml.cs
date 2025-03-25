using System.Diagnostics;
using Sozeris.Pages;

namespace Sozeris;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        
        //SetInitialPage();
    }
    private void SetInitialPage()
    {
        //Debug.WriteLine($"Items count: {Items[0]}");
        if (Preferences.Get("IsAuthenticated", false))
            CurrentItem = Items.FirstOrDefault(item => item.Route == "IMPL_MainPage");
        else
            CurrentItem = Items.FirstOrDefault(item => item.Route == "IMPL_LoginPage");
    }
}