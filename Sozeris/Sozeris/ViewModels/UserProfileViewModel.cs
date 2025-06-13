using System.Windows.Input;
using Sozeris.Models.Entities;
using Sozeris.Logic.Services.Interfaces;
using Sozeris.Logic.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Sozeris.ViewModels;

public class UserProfileViewModel : INotifyPropertyChanged
{
    private readonly IUserService _userService;

    public User User { get; set; }
    
    private string _editableAddress;
    
    public string EditableAddress
    {
        get => _editableAddress;
        set
        {
            _editableAddress = value;
            OnPropertyChanged();
        }
    }
    
    private bool _isEditingAddress;
    public bool IsEditingAddress
    {
        get => _isEditingAddress;
        set
        {
            _isEditingAddress = value;
            OnPropertyChanged();
        }
    }

    public ICommand AddressTappedCommand { get; }
    public ICommand HistoryTappedCommand { get; }
    
    public ICommand CancelEditAddressCommand { get; }
    
    public ICommand SaveAddressCommand { get; }

    public UserProfileViewModel()
    {
        _userService = new UserService(); // Заглушка

        User = _userService.GetCurrentUser();
        IsEditingAddress = false; 
        
        AddressTappedCommand = new Command(OnAddressTapped);
        HistoryTappedCommand = new Command(OnHistoryTapped);
        CancelEditAddressCommand = new Command(() => IsEditingAddress = false);
        SaveAddressCommand = new Command(() =>
        {
            User.Address = EditableAddress;
            IsEditingAddress = false;
        });
    }

    private void OnAddressTapped()
    {
        IsEditingAddress = true;
        // Заглушка: переход на страницу адреса
        App.Current.MainPage.DisplayAlert("Адрес", "Переход на адрес доставки", "ОК");
    }

    private void OnHistoryTapped()
    {
        // Заглушка: переход на страницу истории покупок
        App.Current.MainPage.DisplayAlert("История", "Переход на историю покупок", "ОК");
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}