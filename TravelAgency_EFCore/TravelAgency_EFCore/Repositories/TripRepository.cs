using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelAgency_EFCore.Context;
using TravelAgency_EFCore.Models;
using TravelAgency_EFCore.ResponseModels;

namespace TravelAgency_EFCore.Repositories;

public class TripRepository : ITripRepository
{
    private readonly ApbdEfcContext _context;
    
    public TripRepository(ApbdEfcContext context)
    {
        _context = context;
    }


    public async Task<List<Trip>> GetTripsAsync(int pageNum, int pageSize)
    {
        var result = await _context.Trips
            .Include(trip => trip.ClientTrips)
            .ThenInclude(clienttrip => clienttrip.IdClientNavigation)
            .Include(trip => trip.IdCountries)
            .Skip((pageNum - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return result;
    }

    public Task<int> GetTripsCountAsync()
    {
        var result = _context.Trips.CountAsync();

        return result;
    }
}