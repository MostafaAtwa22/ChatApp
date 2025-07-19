using ChatApp.Data;
using ChatApp.Enums;
using ChatApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ChatApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var chats = await _context.Chats
                .Include(c => c.ChatUsers)
                .Where(c => !c.ChatUsers.Any(u => u.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier)))
                .ToListAsync();
            return View(chats);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Chat(int id)
        {
            var chat = await _context.Chats
                .Include(c => c.Messages)
                .FirstOrDefaultAsync(c => c.Id == id);
            return View(chat);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMessage(int chatId, string message)
        {
            var Message = new Message
            {
                Content = message,
                ChatId = chatId,
                Name = User.Identity.Name,
                Timestamp = DateTime.Now
            };
            _context.Messages.Add(Message);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Chat), new {id = chatId});
        }

        [HttpPost]
        public async Task<IActionResult> CreateGroup(string name)
        {
            var chat = new Chat
            {
                Name = name,
                chatType = Enums.ChatType.Group
            };
            _context.Chats.Add(chat);

            var chatUser = new ChatUser
            {
                Chat = chat,
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!,
                Role = UserRole.Admin
            };
            _context.ChatUsers.Add(chatUser);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> JoinGroup(int id)
        {
            var chatUser = new ChatUser
            {
                ChatId = id,
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!,
                Role = UserRole.Admin
            };
            await _context.ChatUsers.AddAsync(chatUser);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Chat), new { id = id });
        }
    }
}
