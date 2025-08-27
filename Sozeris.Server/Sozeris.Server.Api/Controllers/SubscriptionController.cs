using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sozeris.Server.Api.DTO.Subscription;
using Sozeris.Server.Api.Models.Common;
using Sozeris.Server.Domain.Entities;
using Sozeris.Server.Logic.Common;
using Sozeris.Server.Logic.Interfaces.Services;

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
    public async Task<ActionResult<ApiResponse<List<SubscriptionResponseDTO>>>> GetAllSubscriptions(CancellationToken ct)
    {
        var subscriptions = await _subscriptionService.GetAllSubscriptionsAsync(ct);
        var dto = _mapper.Map<List<SubscriptionResponseDTO>>(subscriptions);
        
        return Ok(ApiResponse<List<SubscriptionResponseDTO>>.Ok(dto));
    }

    [HttpGet("{subscriptionId:int}")]
    public async Task<ActionResult<ApiResponse<SubscriptionResponseDTO>>> GetSubscriptionByIdAsync(int subscriptionId, CancellationToken ct)
    {
        var result = await _subscriptionService.GetSubscriptionByIdAsync(subscriptionId, ct);

        return result.Match(
            onSuccess: subscription => _mapper.Map<SubscriptionResponseDTO>(subscription).ToApiResponse(this),
            onFailure: error => error.ToApiResponse<SubscriptionResponseDTO>(this)
        );
    }

    [HttpGet("user/{userId:int}")]
    public async Task<ActionResult<ApiResponse<List<SubscriptionResponseDTO>>>> GetSubscriptionsByUserIdAsync(int userId, CancellationToken ct)
    {
        var subscriptions = await _subscriptionService.GetSubscriptionsByUserIdAsync(userId, ct);
        
        var dto = _mapper.Map<List<SubscriptionResponseDTO>>(subscriptions);
        
        return Ok(ApiResponse<List<SubscriptionResponseDTO>>.Ok(dto));
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<SubscriptionResponseDTO>>> AddSubscriptionAsync([FromBody] SubscriptionCreateDTO subscriptionDto, CancellationToken ct)
    {
        var subscription = _mapper.Map<Subscription>(subscriptionDto);
        var result = await _subscriptionService.AddSubscriptionAsync(subscription, ct);

        return result.Match(
            onSuccess: created => _mapper.Map<SubscriptionResponseDTO>(created)
                    .ToCreatedAt(this, nameof(GetSubscriptionByIdAsync), HttpContext),
            onFailure: error => error.ToApiResponse<SubscriptionResponseDTO>(this)
        );
    }
}