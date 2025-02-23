using AutoMapper;
using Sozeris.Server.Data.Repositories.Interfaces;
using Sozeris.Server.Logic.Services.Interfaces;
using Sozeris.Server.Models.DTO;
using Sozeris.Server.Models.Entities;

namespace Sozeris.Server.Logic.Services;

public class SubscriptionService : ISubscriptionService
{
    private readonly ISubscriptionRepository _subscriptionRepository;
    private readonly IMapper _mapper;

    public SubscriptionService(ISubscriptionRepository subscriptionRepository, IMapper mapper)
    {
        _subscriptionRepository = subscriptionRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<SubscriptionDTO>> GetAllSubscriptionsAsync()
    {
        var subscriptions = await _subscriptionRepository.GetAllSubscriptionsAsync();
        
        return _mapper.Map<IEnumerable<SubscriptionDTO>>(subscriptions);
    }

    public async Task<SubscriptionDTO?> GetSubscriptionByIdAsync(int subscriptionId)
    {
        var subscription = await _subscriptionRepository.GetSubscriptionByIdAsync(subscriptionId);
        
        return _mapper.Map<SubscriptionDTO>(subscription);
    }

    public async Task<IEnumerable<SubscriptionDTO>> GetSubscriptionsByUserIdAsync(int userId)
    {
        var subscriptions = await _subscriptionRepository.GetSubscriptionsByUserIdAsync(userId);
        
        return _mapper.Map<IEnumerable<SubscriptionDTO>>(subscriptions);
    }

    public async Task<SubscriptionDTO?> GetActiveSubscriptionByUserIdAsync(int userId)
    {
        var subscription = await _subscriptionRepository.GetActiveSubscriptionByUserIdAsync(userId);
        
        return _mapper.Map<SubscriptionDTO>(subscription);
    }

    public async Task<bool> RenewSubscriptionByIdAsync(int subscriptionId)
    {
        return await _subscriptionRepository.RenewSubscriptionByIdAsync(subscriptionId);
    }

    public async Task<bool> AddSubscriptionAsync(SubscriptionDTO subscriptionDto)
    {
        var subscription = _mapper.Map<Subscription>(subscriptionDto);
        
        return await _subscriptionRepository.AddSubscriptionAsync(subscription);
    }

    public async Task<bool> DeleteSubscriptionByIdAsync(int subscriptionId)
    {
        return await _subscriptionRepository.DeleteSubscriptionByIdAsync(subscriptionId);
    }
}