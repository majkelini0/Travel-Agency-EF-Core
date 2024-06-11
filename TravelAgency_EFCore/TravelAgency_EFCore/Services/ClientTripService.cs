using Microsoft.AspNetCore.Mvc;
using TravelAgency_EFCore.Repositories;
using TravelAgency_EFCore.RequestModels;

namespace TravelAgency_EFCore.Services;

public class ClientTripService : IClientTripService
{
    IClientTripRepository _clientTripRepository;
    
    public ClientTripService(IClientTripRepository clientTripRepository)
    {
        _clientTripRepository = clientTripRepository;
    }

    public async Task<IActionResult> AddClientToTripAsync(ClientTripRequest request, int idTrip)
    {
        bool clientExists = await _clientTripRepository.CheckIfClientExistsAsync(request.Pesel, idTrip);
        if (clientExists)
        {
            return new BadRequestObjectResult("Client with this PESEL is already on this trip");
        }
        
        bool tripExists = await _clientTripRepository.ChceckIfSuchTripExistsAsync(idTrip);
        if (!tripExists)
        {
            return new BadRequestObjectResult("Trip with this id does not exists");
        }
        
        bool tripInPast = await _clientTripRepository.CheckIfTripInPastAsync(idTrip);
        if (tripInPast)
        {
            return new BadRequestObjectResult("Trip is already in the past");
        }
        
        int clientId = await _clientTripRepository.GetClientIdByPeselAsync(request.Pesel);
        bool clientAlreadyOnTrip = await _clientTripRepository.CheckIfClientAlreadyOnTripAsync(clientId, idTrip);

        if (clientAlreadyOnTrip)
        {
            return new BadRequestObjectResult("Client is already on this trip");
        }
        
        await _clientTripRepository.AddClientToTripAsync(request, idTrip);
        
        return new OkObjectResult("Client added to trip");
    }
}