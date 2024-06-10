using TravelAgency_EFCore.Models;
using TravelAgency_EFCore.ResponseModels;

namespace TravelAgency_EFCore.Repositories;

public interface IClientRepository
{
    Task<Client?> GetClientAsync(int id);
    Task<bool> CheckIfClientHasReservationsAsync(int id);
    Task DelClientAsync(Client client);
}