using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sozeris.Server.Api.DTO.Delivery;
using Sozeris.Server.Api.Models.Common;
using Sozeris.Server.Logic.Common;
using Sozeris.Server.Logic.Interfaces.Services;

namespace Sozeris.Server.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DeliveriesController : ControllerBase
{
    private readonly IDeliveryService _deliveryService;
    private readonly IMapper _mapper;

    public DeliveriesController(IDeliveryService deliveryService, IMapper mapper)
    {
        _deliveryService = deliveryService;
        _mapper = mapper;
    }

    [HttpGet("today")]
    public async Task<ActionResult<ApiResponse<List<DeliveryForDayDto>>>> GetDeliveriesToday(CancellationToken ct)
    {
        var deliveries = await _deliveryService.GetDeliveriesTodayAsync(ct);
        var dto  = _mapper.Map<List<DeliveryForDayDto>>(deliveries);
        
        return Ok(ApiResponse<List<DeliveryForDayDto>>.Ok(dto));
    }
    
    [HttpPost("{subscriptionId}/status")]
    public async Task<ActionResult<ApiResponse>> MarkDelivery(int subscriptionId, [FromBody] DeliveryMarkRequestDto dto, CancellationToken ct)
    {
        var result = await _deliveryService.MarkDeliveryAsync(subscriptionId, dto.Status, dto.Reason, ct);

        if (result.IsSuccess)
            return ApiResponse.Ok();

        return result.Error.ToApiResponse(HttpContext);
    }
}