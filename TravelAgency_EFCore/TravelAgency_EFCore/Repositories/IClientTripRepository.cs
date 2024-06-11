using TravelAgency_EFCore.RequestModels;

namespace TravelAgency_EFCore.Repositories;

public interface IClientTripRepository
{
    Task AddClientToTripAsync(ClientTripRequest request, int idTrip);
    Task<bool> CheckIfClientExistsAsync(string pesel, int idTrip);
    Task<bool> ChceckIfSuchTripExistsAsync(int tripId);
    Task<bool> CheckIfTripInPastAsync(int tripId);
    Task<int> GetClientIdByPeselAsync(string pesel);
    Task<bool> CheckIfClientAlreadyOnTripAsync(int clientId, int tripId);
}