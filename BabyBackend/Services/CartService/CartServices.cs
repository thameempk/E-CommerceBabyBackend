using BabyBackend.DbContexts;
using BabyBackend.JwtVerification;
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
        private readonly string HostUrl;
        private readonly IConfiguration _configuration;
        private readonly IJwtServices _jwtServices;

        public CartServices(BabyDbContext dbContext, IConfiguration configuration, IJwtServices jwtServices)
        {
            _dbContext = dbContext;
            _configuration = configuration;
            HostUrl = _configuration["HostUrl:url"];
            _jwtServices = jwtServices;
        }

        public async Task<List<CartViewDto>> GetCartItems(string token)
        {
            try
            {
                int userId = _jwtServices.GetUserIdFromToken(token);

                if (userId == null)
                {
                    throw new Exception("user id not valid");
                }

                var user = await _dbContext.cart
                    .Include(u => u.cartItems)
                    .ThenInclude(ci => ci.product)
                    .FirstOrDefaultAsync(u => u.UserId == userId);

                if (user != null)
                {
                    var cartItems = user.cartItems.Select(ci => new CartViewDto
                    {
                        ProductId = ci.ProductId,
                        ProductName = ci.product.ProductName,
                        Quantity = ci.Quantity,
                        Price = ci.product.Price,
                        TotalAmount = ci.product.Price * ci.Quantity,
                        ProductImage = HostUrl + ci.product.ProductImage,
                    }).ToList();

                    return cartItems;
                }
                return new List<CartViewDto>();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
            

          
        }

        public async Task AddToCart(string token, int productId)
        {
            int userId = _jwtServices.GetUserIdFromToken(token);
            if (userId == null)
            {
                throw new Exception("user id not valid");
            }
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

        public async Task DeleteCart(string token, int productId)
        {
            int userId = _jwtServices.GetUserIdFromToken(token);
            if (userId == null)
            {
                throw new Exception("user id not valid");
            }
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

        public async Task QuantityPlus(string token, int productId)
        {
            int userId = _jwtServices.GetUserIdFromToken(token);
            if (userId == null)
            {
                throw new Exception("user id not valid");
            }
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

        public async Task QuantityMin(string token, int productId)
        {
            int userId = _jwtServices.GetUserIdFromToken(token);
            if (userId == null)
            {
                throw new Exception("user id not valid");
            }
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
                     item.Quantity = item.Quantity >= 1 ? item.Quantity - 1 : item.Quantity;
                    if(item.Quantity == 0)
                    {
                        _dbContext.cartItems.Remove(item);
                        await _dbContext.SaveChangesAsync();
                    }
                    await _dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
