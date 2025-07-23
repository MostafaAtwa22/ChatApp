using ChatApp.Data;
using ChatApp.Enums;
using ChatApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;

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

        public IActionResult Find()
        {
            var users = _context.Users
                .Where(u => u.Id != User.FindFirstValue(ClaimTypes.NameIdentifier))
                .ToList();
            return View(users);
        }


        public IActionResult Private()
        {
            var users = _context.Chats
                .Include(u => u.ChatUsers)
                .ThenInclude(u => u.User)
                .Where(u => u.chatType == ChatType.Private
                && u.ChatUsers.Any(y => y.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier)))
                .ToList();

            return View(users);
        }

        public async Task<IActionResult> CreatePrivateChat(string userId)
        {
            var chat = new Chat
            {
                chatType = ChatType.Private
            };
            chat.ChatUsers.Add(new ChatUser
            {
                UserId = userId
            });
            chat.ChatUsers.Add(new ChatUser
            {
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)
            });

            _context.Chats.Add(chat);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Chat), new {id = chat.Id});
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
