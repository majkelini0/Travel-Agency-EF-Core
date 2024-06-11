using Microsoft.AspNetCore.Mvc;
using TravelAgency_EFCore.RequestModels;

namespace TravelAgency_EFCore.Services;

public interface IClientTripService
{
    Task<IActionResult> AddClientToTripAsync(ClientTripRequest request, int idTrip);
}