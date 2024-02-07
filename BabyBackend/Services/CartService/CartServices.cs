using BabyBackend.DbContexts;
using BabyBackend.Models;
using BabyBackend.Models.Dto;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BabyBackend.Services.CartService
{
    public class CartServices : ICartServices
    {
        private readonly BabyDbContext _dbContext;

        public CartServices(BabyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<CartViewDto>> GetCartItems(int userId)
        {
            var user = await _dbContext.Users
                .Include(u => u.cart)
                .ThenInclude(c => c.cartItems)
                .ThenInclude(ci => ci.product)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user != null)
            {
                var cartItems = user.cart.cartItems.Select(ci => new CartViewDto
                {
                    ProductId = ci.ProductId,
                    ProductName = ci.product.ProductName,
                    Quantity = ci.Quantity,
                    Price = ci.product.Price,
                    TotalAmount = ci.product.Price * ci.Quantity,
                    ProductImage = ci.product.ProductImage,
                }).ToList();

                return cartItems;
            }

            return new List<CartViewDto>();
        }

        public async Task AddToCart(int userId, int productId)
        {
            var user = await _dbContext.Users
                .Include(u => u.cart)
                .ThenInclude(u => u.cartItems)
                .FirstOrDefaultAsync(u => u.Id == userId);

            var product = await _dbContext.products.FirstOrDefaultAsync(p => p.Id == productId);

            if (user != null && product != null)
            {
                if (user.cart == null)
                {
                    user.cart = new Cart
                    {
                        UserId = userId,
                        cartItems = new List<CartItem>()
                    };
                    _dbContext.cart.Add(user.cart);
                    await _dbContext.SaveChangesAsync();
                }

                var itemExist = user.cart.cartItems.FirstOrDefault(ci => ci.ProductId == productId);

                if (itemExist != null)
                {
                    itemExist.Quantity = itemExist.Quantity + 1;
                }
                else
                {
                    var newCartItem = new CartItem
                    {
                        CartId = user.cart.Id,
                        ProductId = productId,
                        Quantity = 1
                    };

                    _dbContext.cartItems.Add(newCartItem);
                }

                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteCart(int userId, int productId)
        {
            var user = await _dbContext.Users
                .Include(u => u.cart)
                .ThenInclude(u => u.cartItems)
                .FirstOrDefaultAsync(u => u.Id == userId);

            var product = await _dbContext.products.FirstOrDefaultAsync(p => p.Id == productId);

            if (user != null && product != null)
            {
                var item = user.cart.cartItems.FirstOrDefault(ci => ci.ProductId == productId);
                if (item != null)
                {
                    _dbContext.cartItems.Remove(item);
                    await _dbContext.SaveChangesAsync();
                }
            }
        }

        public async Task QuantityPlus(int userId, int productId)
        {
            var user = await _dbContext.Users
                .Include(u => u.cart)
                .ThenInclude(u => u.cartItems)
                .FirstOrDefaultAsync(u => u.Id == userId);

            var product = await _dbContext.products.FirstOrDefaultAsync(p => p.Id == productId);

            if (user != null && product != null)
            {
                var item = user.cart.cartItems.FirstOrDefault(ci => ci.ProductId == productId);
                if (item != null)
                {
                    item.Quantity = item.Quantity + 1;
                    await _dbContext.SaveChangesAsync();
                }
            }
        }

        public async Task QuantityMin(int userId, int productId)
        {
            var user = await _dbContext.Users
                .Include(u => u.cart)
                .ThenInclude(u => u.cartItems)
                .FirstOrDefaultAsync(u => u.Id == userId);

            var product = await _dbContext.products.FirstOrDefaultAsync(p => p.Id == productId);

            if (user != null && product != null)
            {
                var item = user.cart.cartItems.FirstOrDefault(ci => ci.ProductId == productId);
                if (item != null)
                {
                    item.Quantity = item.Quantity - 1;
                    await _dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
