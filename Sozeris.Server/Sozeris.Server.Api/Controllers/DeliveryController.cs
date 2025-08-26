using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sozeris.Server.Api.DTO.Delivery;
using Sozeris.Server.Api.Models.Common;
using Sozeris.Server.Logic.Common;
using Sozeris.Server.Logic.Interfaces.Services;

namespace Sozeris.Server.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DeliveryController : ControllerBase
{
    private readonly IDeliveryService _deliveryService;
    private readonly IMapper _mapper;

    public DeliveryController(IDeliveryService deliveryService, IMapper mapper)
    {
        _deliveryService = deliveryService;
        _mapper = mapper;
    }

    [HttpGet("today")]
    public async Task<ActionResult<ApiResponse<List<DeliveryForDayDTO>>>> GetDeliveriesForDay()
    {
        var deliveries = await _deliveryService.GetDeliveriesForDayAsync();
        var dto  = _mapper.Map<List<DeliveryForDayDTO>>(deliveries);
        
        return Ok(ApiResponse<List<DeliveryForDayDTO>>.Ok(dto));
    }
    
    [HttpPost("{subscriptionId}/status")]
    public async Task<ActionResult<ApiResponse>> MarkDelivery(int subscriptionId, [FromBody] DeliveryMarkRequestDTO request)
    {
        var result = await _deliveryService.MarkDeliveryAsync(subscriptionId, request.Status, request.Reason);

        return result.Match(
            onSuccess: () => this.ToApiResponse(),
            onFailure: error => error.ToApiResponse(this)
        );
    }
}