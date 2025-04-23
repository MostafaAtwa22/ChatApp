using ChatApplication.Core.Models;
using ChatApplication.Core.Models.Enums;
using ChatApplication.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChatApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoom(string name)
        {
            await _context.Chats.AddAsync(new Chat
            {
                Name = name,
                ChatType = ChatType.ROOM
            });

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> CreateMessage(int chatId, string message)
        {
            var msg = new Message
            {
                ChatId = chatId,
                Text = message,
                Name = "Default",
                TimeStamp = DateTime.UtcNow
            };

            await _context.Messages.AddAsync(msg);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Chat), new {id = chatId});
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Chat(int id)
        {
            var chat = await _context.Chats
                .Include(c => c.Messages)
                .FirstOrDefaultAsync(c => c.Id == id); 
            return View(chat);
        }
    }
}
