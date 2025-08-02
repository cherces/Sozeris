using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sozeris.Models.Entities;

namespace Sozeris.Pages;

public partial class UserSubscriptionPage : ContentPage
{
    public ObservableCollection<Subscription> Subscriptions { get; set; }

    public UserSubscriptionPage(IEnumerable<Subscription> subscriptions)
    {
        InitializeComponent();
        Subscriptions = new ObservableCollection<Subscription>(subscriptions);
        BindingContext = this;
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private async void OnDetailsClicked(object sender, EventArgs e)
    {
        if ((sender as Button)?.CommandParameter is Subscription subscription)
        {
            var details = string.Join("\n", subscription.Orders.Select(o =>
                $"{o.Product?.Name} - {o.Quantity} шт. - {o.Price} ₽"));

            await DisplayAlert($"Подписка №{subscription.Id}",
                $"Дата покупки: {subscription.PurchaseDate:dd.MM.yyyy}\n" +
                $"Период: {subscription.StartDate:dd.MM.yyyy} — {subscription.EndDate:dd.MM.yyyy}\n\n" +
                $"Заказы:\n{details}\n\nИтог: {subscription.Price} ₽", "ОК");
        }
    }
}