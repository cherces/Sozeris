using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sozeris.Pages;

public partial class HomePage : ContentPage
{
    public HomePage()
    {
        InitializeComponent();
    }
    
    private async void OnStartClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//productsCatalog");
    }
}