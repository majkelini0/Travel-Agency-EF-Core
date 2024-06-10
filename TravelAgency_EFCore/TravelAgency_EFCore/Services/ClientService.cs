using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TravelAgency_EFCore.Models;
using TravelAgency_EFCore.Repositories;

namespace TravelAgency_EFCore.Services;

public class ClientService : IClientService
{
    private readonly IClientRepository _clientRepository;

    public ClientService(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public async Task<IActionResult> DelClient(int id)
    {
        // check if client exists
        Client? client = await _clientRepository.GetClientAsync(id); 
        EnsureClientExists(client);
        
        // check if client has associated reservations
        if(await _clientRepository.CheckIfClientHasReservationsAsync(id))
        {
            return new BadRequestObjectResult("Client has reservations");
        }
        
        // delete client
        await _clientRepository.DelClientAsync(client);
        
        return new OkResult();
    }
    
    private static IActionResult? EnsureClientExists(Client client)
    {
        if(client == null)
        {
            return new NotFoundResult();
        }

        return null;
    }
}