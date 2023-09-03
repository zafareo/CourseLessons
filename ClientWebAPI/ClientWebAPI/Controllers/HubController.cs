using ClientWebAPI.HUB;
using ClientWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace ClientWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HubController : ControllerBase
    {

        private readonly IHubContext<ChatHub> _context;

        public HubController(IHubContext<ChatHub> context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> SendMessage(Message message)
        {
            await _context.Clients.All.SendAsync("ReceiveMessage", message);
            return Ok();
        }
    }
}
