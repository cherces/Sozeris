using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sozeris.ViewModels;

namespace Sozeris.Pages;

public partial class ProductsCatalogPage : ContentPage
{
    public ProductsCatalogPage(ProductsCatalogViewModel productsCatalogViewModel)
    {
        InitializeComponent();
        BindingContext = productsCatalogViewModel;
    }
}