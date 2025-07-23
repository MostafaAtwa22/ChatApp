using ChatApp.Data;
using ChatApp.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace ChatApp.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class ChatsController : Controller
    {
        private readonly IHubContext<ChatHub> _chat;

        public ChatsController(IHubContext<ChatHub> chat)
        {
            _chat = chat;
        }

        [HttpPost("[action]/{connectionId}/{groupId}")]
        public async Task<IActionResult> JoinGroup (string connectionId, int groupId)
        {
            await _chat.Groups.AddToGroupAsync(connectionId, groupId.ToString());
            return Ok();
        }

        [HttpPost("[action]/{connectionId}/{groupId}")]
        public async Task<IActionResult> LeaveGroup (
            string connectionId, 
            int groupId
            )
        {
            await _chat.Groups.RemoveFromGroupAsync(connectionId, groupId.ToString());
            return Ok();
        }
        [HttpPost("[action]")] 
        public async Task<IActionResult> SendMessage(
            int groupId,
            string message, 
            [FromServices] AppDbContext context)
        {
            var Message = new Models.Message
            {
                Content = message,
                ChatId = groupId,
                Name = User.Identity.Name,
                Timestamp = DateTime.Now
            };
            context.Messages.Add(Message);
            await context.SaveChangesAsync();

            await _chat.Clients.Group(groupId.ToString())
                .SendAsync("ReceiveMessage", new {
                    Content = Message.Content,
                    Name = Message.Name,
                    Timestamp = Message.Timestamp.ToString("dd/MM/yyyy hh:mm:ss")
                });
            return Ok();
        }
    }
}
