using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelAgency_EFCore.Context;
using TravelAgency_EFCore.Models;

namespace TravelAgency_EFCore.Repositories;

public class ClientRepository : IClientRepository
{
    private readonly ApbdEfcContext _context;
    
    public ClientRepository(ApbdEfcContext context)
    {
        _context = context;
    }
    
    public async Task DelClientAsync(Client client)
    {
        _context.Clients.Remove(client);
        await _context.SaveChangesAsync();
    }

    public async Task<Client?> GetClientAsync(int id)
    {
        var client = await _context.Clients.FirstOrDefaultAsync(cl => cl.IdClient == id);

        return client;
    }
    
    public async Task<bool> CheckIfClientHasReservationsAsync(int id)
    {
        var exists = await _context.ClientTrips.AnyAsync(client_trip => client_trip.IdClient == id);

        return exists;
    }
}