using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TravelAgency_EFCore.Services;

namespace TravelAgency_EFCore.Controller;

[ApiController]
[Route("api")]
public class ClientController : ControllerBase
{
    private readonly IClientService _clientService;
    
    public ClientController(IClientService clientService)
    {
        _clientService = clientService;
    }

    [HttpDelete]
    [Route("clients/{idClient:int}")]
    public async Task<IActionResult> DelClient(int idClient)
    {
        if (idClient < 0)
        {
            return BadRequest("Id cannot be less than 0");
        }
        
        var result = await _clientService.DelClient(idClient);

        return Ok(result);
    }
}