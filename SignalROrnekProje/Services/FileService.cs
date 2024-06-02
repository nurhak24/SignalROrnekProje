using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SignalROrnekProje.Models;
using System.Threading.Channels;

namespace SignalROrnekProje.Services
{
    public class FileService(AppDbContext context,IHttpContextAccessor httpContextAccessor,UserManager<IdentityUser> userManager,Channel<(string UserId, List<Product> products)> channel)
    {

        public async Task<bool> AddMessageToQueue()
        {

            var userId = userManager.GetUserId(httpContextAccessor!.HttpContext.User);

            var products = await context.Products.Where(x => x.UserId == userId).ToListAsync();

            return channel.Writer.TryWrite((userId, products));

        }

    }
}
