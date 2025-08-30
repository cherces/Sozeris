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
public class SubscriptionsController : ControllerBase
{
    private readonly ISubscriptionService _subscriptionService;
    private readonly IMapper _mapper;
    
    public SubscriptionsController(ISubscriptionService subscriptionService, IMapper mapper)
    {
        _subscriptionService = subscriptionService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<List<SubscriptionResponseDto>>>> GetAllSubscriptions(CancellationToken ct)
    {
        var subscriptions = await _subscriptionService.GetAllSubscriptionsAsync(ct);
        var dto = _mapper.Map<List<SubscriptionResponseDto>>(subscriptions);
        
        return Ok(ApiResponse<List<SubscriptionResponseDto>>.Ok(dto));
    }

    [HttpGet("{subscriptionId:int}")]
    public async Task<ActionResult<ApiResponse<SubscriptionResponseDto>>> GetSubscriptionById(int subscriptionId, CancellationToken ct)
    {
        var result = await _subscriptionService.GetSubscriptionByIdAsync(subscriptionId, ct);
        
        if (result.IsSuccess)
            return _mapper.Map<SubscriptionResponseDto>(result.Value).ToApiResponse();

        return result.Error.ToApiResponse<SubscriptionResponseDto>(HttpContext);
    }

    [HttpGet("user/{userId:int}")]
    public async Task<ActionResult<ApiResponse<List<SubscriptionResponseDto>>>> GetSubscriptionsByUserId(int userId, CancellationToken ct)
    {
        var subscriptions = await _subscriptionService.GetSubscriptionsByUserIdAsync(userId, ct);
        
        var dto = _mapper.Map<List<SubscriptionResponseDto>>(subscriptions);
        
        return Ok(ApiResponse<List<SubscriptionResponseDto>>.Ok(dto));
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<SubscriptionResponseDto>>> AddSubscription([FromBody] SubscriptionCreateDto subscriptionDto, CancellationToken ct)
    {
        var subscription = _mapper.Map<Subscription>(subscriptionDto);
        var result = await _subscriptionService.AddSubscriptionAsync(subscription, ct);
        
        if (result.IsSuccess)
            return _mapper.Map<SubscriptionResponseDto>(result.Value).ToApiResponse();

        return result.Error.ToApiResponse<SubscriptionResponseDto>(HttpContext);
    }
    
    [HttpPut("{id:int}/toggleActive")]
    public async Task<ActionResult<ApiResponse>> ToggleSubscriptionActive(int id, CancellationToken ct)
    {
        var result = await _subscriptionService.ToggleSubscriptionActiveAsync(id, ct);

        if (result.IsSuccess)
            return ApiResponse.Ok();
        
        return result.Error.ToApiResponse(HttpContext);
    }
}