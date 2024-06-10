using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TravelAgency_EFCore.Repositories;
using TravelAgency_EFCore.ResponseModels;

namespace TravelAgency_EFCore.Services;

public class TripService : ITripService
{
    private readonly ITripRepository _tripRepository;
    
    public TripService(ITripRepository tripRepository)
    {
        _tripRepository = tripRepository;
    }


    public async Task<TripsResponse> GetTripsAsync(int pageNum, int pageSize)
    {
        pageNum = CheckPageNum(pageNum);
        pageSize = CheckPageSize(pageSize);
        
        int numOfTrips = await _tripRepository.GetTripsCountAsync();
        
        var trips = await _tripRepository.GetTripsAsync(pageNum, pageSize);

        var response = new TripsResponse
        {
            PageNum = pageNum,
            PageSize = pageSize,
            AllPages = (int) Math.Ceiling(numOfTrips / (double)pageSize),
            Trips = trips.Select(trip => new TripDTO
                {
                    Name = trip.Name,
                    Description = trip.Description,
                    DateFrom = trip.DateFrom,
                    DateTo = trip.DateTo,
                    MaxPeople = trip.MaxPeople,
                    
                    Countries = trip.IdCountries
                        .Where(country => country != null)
                        .Select(country => new CountryDTO
                    {
                        name = country.Name
                    }).ToList(),
                    
                    Clients = trip.ClientTrips
                        .Where(client => client.IdClientNavigation != null)
                        .Select(client => new ClientDTO
                    {
                        fname = client.IdClientNavigation.FirstName,
                        lname = client.IdClientNavigation.LastName
                    }).ToList()
                })
                .OrderByDescending(date => date.DateFrom)
                .ToList()
        };
        
        return response;
    }

    public static int CheckPageNum(int pageNum)
    {
        if (pageNum < 1)
        {
            return 1;
        }
        return pageNum;
    }
    
    public static int CheckPageSize(int pageSize)
    {
        if (pageSize < 1)
        {
            return 1;
        }
        return pageSize;
    }
}