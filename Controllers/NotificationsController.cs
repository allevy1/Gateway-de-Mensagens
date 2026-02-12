using Microsoft.AspNetCore.Mvc;

namespace NotificationGateway.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotificationController : ControllerBase
{
    private readonly IBus _bus;

    public NotificationController(IBus bus)
    {
        _bus = bus;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] NotificationRequest request)
    {
        await _bus.Publish(request);

        return Ok("Sucesso!");
    }
}
