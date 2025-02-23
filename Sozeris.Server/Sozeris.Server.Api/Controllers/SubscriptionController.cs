using Microsoft.AspNetCore.Mvc;
using Sozeris.Server.Logic.Services.Interfaces;
using Sozeris.Server.Models.DTO;

namespace Sozeris.Server.Api.Controllers;

[Route("api/[controller]")]
public class SubscriptionController : ControllerBase
{
    private readonly ISubscriptionService _subscriptionService;

    public SubscriptionController(ISubscriptionService subscriptionService)
    {
        _subscriptionService = subscriptionService;
    }

    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<SubscriptionDTO>>> GetAllSubscriptions()
    {
        var subscriptions = await _subscriptionService.GetAllSubscriptionsAsync();
        
        return Ok(subscriptions);
    }

    [HttpGet("{subscriptionId}")]
    public async Task<ActionResult<SubscriptionDTO>> GetSubscriptionByIdAsync(int subscriptionId)
    {
        var subscription = await _subscriptionService.GetSubscriptionByIdAsync(subscriptionId);
        
        if (subscription == null) return NotFound();
        
        return Ok(subscription);
    }

    [HttpGet("user/{userId}")]
    public async Task<ActionResult<IEnumerable<SubscriptionDTO>>> GetSubscriptionsByUserIdAsync(int userId)
    {
        var subscriptions = await _subscriptionService.GetSubscriptionsByUserIdAsync(userId);
        
        return Ok(subscriptions);
    }

    [HttpGet("active/user/{userId}")]
    public async Task<ActionResult<SubscriptionDTO?>> GetActiveSubscriptionByUserIdAsync(int userId)
    {
        var subscription = await _subscriptionService.GetActiveSubscriptionByUserIdAsync(userId);
        
        if (subscription == null) return NotFound();
        
        return Ok(subscription);
    }

    [HttpPut("{subscriptionId}")]
    public async Task<ActionResult> RenewSubscriptionByIdAsync(int subscriptionId)
    {
        var success = await _subscriptionService.RenewSubscriptionByIdAsync(subscriptionId);

        if (!success) return BadRequest("Subscription renew failed.");
        
        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<SubscriptionDTO>> AddSubscriptionAsync([FromBody] SubscriptionDTO? subscriptionDto)
    {
        if (subscriptionDto is null) return BadRequest();
        
        var success = await _subscriptionService.AddSubscriptionAsync(subscriptionDto);
        
        if (!success) return BadRequest("Subscription creation failed.");
        
        return CreatedAtAction(nameof(GetSubscriptionByIdAsync), new { subscriptionId = subscriptionDto.Id }, subscriptionDto);
    }

    [HttpDelete("{subscriptionId}")]
    public async Task<ActionResult> DeleteSubscriptionByIdAsync(int subscriptionId)
    {
        var success = await _subscriptionService.DeleteSubscriptionByIdAsync(subscriptionId);
        
        if (!success) return BadRequest("Subscription deletion failed.");
        
        return NoContent();
    }
}