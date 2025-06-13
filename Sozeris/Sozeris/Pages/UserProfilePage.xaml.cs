using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sozeris.ViewModels;

namespace Sozeris.Pages;

public partial class UserProfilePage : ContentPage
{
    public UserProfilePage(UserProfileViewModel userProfileViewModel)
    {
        InitializeComponent();
        BindingContext = userProfileViewModel;
    }
}