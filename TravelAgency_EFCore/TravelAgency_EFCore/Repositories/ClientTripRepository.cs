using Microsoft.EntityFrameworkCore;
using TravelAgency_EFCore.Context;
using TravelAgency_EFCore.Models;
using TravelAgency_EFCore.RequestModels;

namespace TravelAgency_EFCore.Repositories;

public class ClientTripRepository : IClientTripRepository
{
    ApbdEfcContext _context;
    
    public ClientTripRepository(ApbdEfcContext context)
    {
        _context = context;
    }

    public async Task AddClientToTripAsync(ClientTripRequest request, int idTrip)
    {
        int clientId = await GetClientIdByPeselAsync(request.Pesel);
        
        // Add client to client_trip
        ClientTrip newClientTrip = new()
        {
            IdClient = clientId,
            IdTrip = idTrip,
            RegisteredAt = DateTime.Now,
            PaymentDate = request.PaymentDate
        };
        await _context.ClientTrips.AddAsync(newClientTrip);
        _context.SaveChangesAsync();
    }

    public async Task<bool> CheckIfClientExistsAsync(string pesel, int idTrip)
    { 
        return await _context.ClientTrips.AnyAsync(ct => ct.IdClient == _context.Clients.First(c => c.Pesel == pesel).IdClient && ct.IdTrip == idTrip);
    }
    
    public async Task<bool> ChceckIfSuchTripExistsAsync(int tripId)
    {
        return await _context.Trips.AnyAsync(t => t.IdTrip == tripId);
    }

    public async Task<bool> CheckIfTripInPastAsync(int tripId)
    {
        return await _context.Trips.AnyAsync(t => t.IdTrip == tripId && t.DateFrom < DateTime.Now);
    }

    public async Task<int> GetClientIdByPeselAsync(string pesel)
    {
        var client = await _context.Clients.FirstAsync(c => c.Pesel == pesel);
        return client.IdClient;
    }

    public async Task<bool> CheckIfClientAlreadyOnTripAsync(int clientId, int tripId)
    {
        return await _context.ClientTrips.AnyAsync(ct => ct.IdClient == clientId && ct.IdTrip == tripId);
    }
}