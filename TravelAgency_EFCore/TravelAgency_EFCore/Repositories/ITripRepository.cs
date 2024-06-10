using TravelAgency_EFCore.Models;
using TravelAgency_EFCore.ResponseModels;

namespace TravelAgency_EFCore.Repositories;

public interface ITripRepository
{
    Task<List<Trip>> GetTripsAsync(int pageNum, int pageSize);
    
    Task<int> GetTripsCountAsync();
}