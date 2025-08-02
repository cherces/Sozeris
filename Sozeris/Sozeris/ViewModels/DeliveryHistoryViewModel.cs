using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Sozeris.Logic.Services;
using Sozeris.Logic.Services.Interfaces;
using Sozeris.Models.DTO;

namespace Sozeris.ViewModels;

public class DeliveryHistoryViewModel : INotifyPropertyChanged
{
    private readonly IDeliveryHistoryService _deliveryService;

    public ObservableCollection<DeliveryItemDTO> Deliveries { get; set; } = new();

    public DeliveryHistoryViewModel()
    {
        _deliveryService = new DeliveryHistoryService();
        LoadDeliveries();
    }

    private async void LoadDeliveries()
    {
        int userId = 1;
        var result = await _deliveryService.GetDeliveriesByUserIdAsync(userId);

        Deliveries.Clear();
        foreach (var delivery in result)
            Deliveries.Add(delivery);
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}