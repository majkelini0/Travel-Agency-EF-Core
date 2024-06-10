using Microsoft.AspNetCore.Mvc;

namespace TravelAgency_EFCore.Services;

public interface IClientService
{
    Task<IActionResult> DelClient(int id);
}