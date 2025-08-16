using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sozeris.Server.Api.DTO.Subscription;
using Sozeris.Server.Domain.Entities;
using Sozeris.Server.Domain.Interfaces.Services;

namespace Sozeris.Server.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SubscriptionController : ControllerBase
{
    private readonly ISubscriptionService _subscriptionService;
    private readonly IMapper _mapper;
    
    public SubscriptionController(ISubscriptionService subscriptionService, IMapper mapper)
    {
        _subscriptionService = subscriptionService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<SubscriptionResponseDTO>>> GetAllSubscriptions()
    {
        var subscriptions = await _subscriptionService.GetAllSubscriptionsAsync();
        
        return Ok(_mapper.Map<List<SubscriptionResponseDTO>>(subscriptions));
    }

    [HttpGet("{subscriptionId}")]
    public async Task<ActionResult<SubscriptionResponseDTO>> GetSubscriptionByIdAsync(int subscriptionId)
    {
        var subscription = await _subscriptionService.GetSubscriptionByIdAsync(subscriptionId);
        
        return subscription == null ? NotFound() : Ok(_mapper.Map<SubscriptionResponseDTO>(subscription));
    }

    [HttpGet("user/{userId}")]
    public async Task<ActionResult<List<SubscriptionResponseDTO>>> GetSubscriptionsByUserIdAsync(int userId)
    {
        var subscriptions = await _subscriptionService.GetSubscriptionsByUserIdAsync(userId);
        
        return Ok(_mapper.Map<List<SubscriptionResponseDTO>>(subscriptions));
    }

    [HttpPost]
    public async Task<ActionResult<SubscriptionCreateDTO>> AddSubscriptionAsync([FromBody] SubscriptionCreateDTO subscriptionDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        var subscription = _mapper.Map<Subscription>(subscriptionDto);
        var createdSubscription = await _subscriptionService.AddSubscriptionAsync(subscription);
        
        return CreatedAtAction(nameof(GetSubscriptionByIdAsync), new { subscriptionId = createdSubscription.Id }, createdSubscription);
    }
}