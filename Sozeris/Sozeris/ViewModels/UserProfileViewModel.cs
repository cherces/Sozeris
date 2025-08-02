using System.Collections.ObjectModel;
using System.Windows.Input;
using Sozeris.Models.Entities;
using Sozeris.Logic.Services.Interfaces;
using Sozeris.Logic.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Sozeris.Pages;

namespace Sozeris.ViewModels;

public class UserProfileViewModel : INotifyPropertyChanged
{
    private readonly IUserService _userService;
    private readonly ISubscriptionService _subscriptionService;

    public User User { get; set; }
    
    public ObservableCollection<Subscription> Subscriptions { get; set; } = new();
    
    private string _editableAddress;
    
    public string EditableAddress
    {
        get => _editableAddress; 
        set { _editableAddress = value; OnPropertyChanged(); }
    }
    
    private bool _isEditingAddress;
    public bool IsEditingAddress
    {
        get => _isEditingAddress;
        set { _isEditingAddress = value; OnPropertyChanged(); }
    }
    
    private bool _isSubscriptionHistoryVisible;
    public bool IsSubscriptionHistoryVisible
    {
        get => _isSubscriptionHistoryVisible;
        set { _isSubscriptionHistoryVisible = value; OnPropertyChanged(); }
    }

    public ICommand AddressTappedCommand { get; }
    public ICommand NavigateToSubscriptionPageCommand { get; }
    public ICommand CancelEditAddressCommand { get; }
    public ICommand SaveAddressCommand { get; }
    public ICommand LogoutCommand { get; }

    public UserProfileViewModel()
    {
        _userService = new UserService(); // Заглушка
        _subscriptionService = new SubscriptionService(); // Заглушка

        User = _userService.GetCurrentUser();
        IsEditingAddress = false;
        IsSubscriptionHistoryVisible = false;
        
        AddressTappedCommand = new Command(OnAddressTapped);
        NavigateToSubscriptionPageCommand = new Command(OnSubscriptionTapped);
        CancelEditAddressCommand = new Command(() => IsEditingAddress = false);
        SaveAddressCommand = new Command(() =>
        {
            User.Address = EditableAddress;
            IsEditingAddress = false;
        });
        LogoutCommand = new Command(OnLogout);
    }

    private void OnAddressTapped()
    {
        IsEditingAddress = true;
    }

    private async void OnSubscriptionTapped()
    {
        var userId = User.Id;
        var subs = await _subscriptionService.GetSubscriptionsByUserIdAsync(userId);
        await Shell.Current.Navigation.PushAsync(new UserSubscriptionPage(subs));
    }
    
    private void OnLogout()
    {
        App.Current.MainPage.DisplayAlert("Выход", "Вы успешно вышли", "ОК");
        // Здесь логика выхода
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}