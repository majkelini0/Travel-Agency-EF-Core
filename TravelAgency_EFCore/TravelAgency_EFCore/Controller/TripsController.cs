using Microsoft.AspNetCore.Mvc;
using TravelAgency_EFCore.Context;
using TravelAgency_EFCore.Services;

namespace TravelAgency_EFCore.Controller;

[ApiController]
[Route("api")]
public class TripsController : ControllerBase
{
    private readonly ITripService _tripService;
    
    public TripsController(ITripService tripService)
    {
        _tripService = tripService;
    }
    
    
    [HttpGet]
    [Route("trips")]
    public async Task<IActionResult> GetTrips(int pageNum = 1, int pageSize = 10)
    {
        var result = await _tripService.GetTripsAsync(pageNum, pageSize);
        
        return Ok(result);
    }
}