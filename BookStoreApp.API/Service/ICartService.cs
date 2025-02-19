using BookStoreApp.API.Data;
using BookStoreApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApp.API.Service
{
    public interface ICartService
    {
        Task<Msg> AddCartItem(int bookId, string userId);
        Task<List<CartItem>> GetCartByUserId(string userId);
        Task<Msg> DecreaseAmount(int bookId, string userId);
        Task<Msg> DeleteCartItem(int bookId, string userId);
        Task<Msg> DeleteAll(string userId);
    }

    public class CartService : ICartService
    {
        private readonly BookstoreContext _context;

        public CartService(BookstoreContext context)
        {
            _context = context;
        }

        public async Task<Msg> AddCartItem(int bookId, string userId)
        {
            try
            {
                var existingItem = await _context.CartItems
                    .FirstOrDefaultAsync(ci => ci.BookId == bookId && ci.UserId == userId);

                if (existingItem != null)
                {
                    existingItem.Amount++;
                    await _context.SaveChangesAsync();
                    return Msg.SuccessMsg("Item quantity increased", existingItem);
                }

                var newItem = new CartItem
                {
                    UserId = userId,
                    BookId = bookId,
                    Amount = 1
                };

                _context.CartItems.Add(newItem);
                await _context.SaveChangesAsync();
                return Msg.SuccessMsg("Item added to cart", newItem);
            }
            catch (Exception ex)
            {
                return Msg.FailMsg($"Error adding item: {ex.Message}");
            }
        }

        public async Task<List<CartItem>> GetCartByUserId(string userId)
        {
            return await _context.CartItems
                .Include(ci => ci.Book)
                .Where(ci => ci.UserId == userId)
                .ToListAsync();
        }

        public async Task<Msg> DecreaseAmount(int bookId, string userId)
        {
            var item = await _context.CartItems
                .FirstOrDefaultAsync(ci => ci.BookId == bookId && ci.UserId == userId);

            if (item == null)
                return Msg.FailMsg("Item not found in cart");

            if (item.Amount > 1)
            {
                item.Amount--;
                await _context.SaveChangesAsync();
                return Msg.SuccessMsg("Quantity decreased", item);
            }

            _context.CartItems.Remove(item);
            await _context.SaveChangesAsync();
            return Msg.SuccessMsg("Item removed as quantity reached zero");
        }

        public async Task<Msg> DeleteCartItem(int bookId, string userId)
        {
            var item = await _context.CartItems
                .FirstOrDefaultAsync(ci => ci.BookId == bookId && ci.UserId == userId);

            if (item == null)
                return Msg.FailMsg("Item not found in cart");

            _context.CartItems.Remove(item);
            await _context.SaveChangesAsync();
            return Msg.SuccessMsg("Item removed from cart");
        }

        public async Task<Msg> DeleteAll(string userId)
        {
            var items = await _context.CartItems
                .Where(ci => ci.UserId == userId)
                .ToListAsync();

            _context.CartItems.RemoveRange(items);
            await _context.SaveChangesAsync();
            return Msg.SuccessMsg("All items removed from cart");
        }
    }
}
