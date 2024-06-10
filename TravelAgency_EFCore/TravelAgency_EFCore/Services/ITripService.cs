using Microsoft.AspNetCore.Mvc;
using TravelAgency_EFCore.ResponseModels;

namespace TravelAgency_EFCore.Services;

public interface ITripService
{
    Task<TripsResponse> GetTripsAsync(int pageNum, int pageSize);
}