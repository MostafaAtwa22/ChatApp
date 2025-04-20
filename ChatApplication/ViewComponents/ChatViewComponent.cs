using ChatApplication.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChatApplication.MVC.ViewComponents
{
    public class ChatViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public ChatViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> Invoke()
        {
            var chats = await _context.Chats.ToListAsync();
            return View(chats);
        }
    }
}
