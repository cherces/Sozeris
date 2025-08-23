using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sozeris.Server.Api.DTO.Subscription;
using Sozeris.Server.Api.Models.Common;
using Sozeris.Server.Domain.Entities;
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
    public async Task<ActionResult<ApiResponse<List<SubscriptionResponseDTO>>>> GetAllSubscriptions()
    {
        var subscriptions = await _subscriptionService.GetAllSubscriptionsAsync();
        var dto = _mapper.Map<List<SubscriptionResponseDTO>>(subscriptions);
        
        return Ok(ApiResponse<List<SubscriptionResponseDTO>>.Ok(dto));
    }

    [HttpGet("{subscriptionId}")]
    public async Task<ActionResult<ApiResponse<SubscriptionResponseDTO>>> GetSubscriptionByIdAsync(int subscriptionId)
    {
        var result = await _subscriptionService.GetSubscriptionByIdAsync(subscriptionId);
        if (!result.Success) return NotFound(ApiResponse<object>.Fail(new Exception(result.ErrorMessage)));
        
        var dto = _mapper.Map<SubscriptionResponseDTO>(result.Value);
        
        return Ok(ApiResponse<SubscriptionResponseDTO>.Ok(dto));
    }

    [HttpGet("user/{userId}")]
    public async Task<ActionResult<ApiResponse<List<SubscriptionResponseDTO>>>> GetSubscriptionsByUserIdAsync(int userId)
    {
        var subscriptions = await _subscriptionService.GetSubscriptionsByUserIdAsync(userId);
        
        var dto = _mapper.Map<List<SubscriptionResponseDTO>>(subscriptions);
        
        return Ok(ApiResponse<List<SubscriptionResponseDTO>>.Ok(dto));
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<SubscriptionCreateDTO>>> AddSubscriptionAsync([FromBody] SubscriptionCreateDTO subscriptionDto)
    {
        if (!ModelState.IsValid) return BadRequest(ApiResponse<object>.Fail(ModelState));
        
        var subscription = _mapper.Map<Subscription>(subscriptionDto);
        var result = await _subscriptionService.AddSubscriptionAsync(subscription);
        if (!result.Success) return BadRequest(ApiResponse<object>.Fail(new Exception(result.ErrorMessage)));
        
        var response = _mapper.Map<SubscriptionResponseDTO>(result.Value);
        
        return CreatedAtAction(nameof(GetSubscriptionByIdAsync), new { subscriptionId = result.Value.Id }, response);
    }
}