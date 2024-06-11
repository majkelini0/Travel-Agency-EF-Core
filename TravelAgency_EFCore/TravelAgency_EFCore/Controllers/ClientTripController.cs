using Microsoft.AspNetCore.Mvc;
using TravelAgency_EFCore.RequestModels;
using TravelAgency_EFCore.Services;

namespace TravelAgency_EFCore.Controller;

[ApiController]
[Route("api")]
public class ClientTripController : ControllerBase
{
    private readonly IClientTripService _clientTripService;
    
    public ClientTripController(IClientTripService clientTripService)
    {
        _clientTripService = clientTripService;
    }

    [HttpPost]
    [Route("trips/{idTrip:int}/clients")]
    public async Task<IActionResult> AddClientToTrip([FromBody] ClientTripRequest request, int idTrip)
    {
        var res =  await _clientTripService.AddClientToTripAsync(request, idTrip);

        return res;
    }
}