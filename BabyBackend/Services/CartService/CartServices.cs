using BabyBackend.DbContexts;
using BabyBackend.Models;
using BabyBackend.Models.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Validations;

namespace BabyBackend.Services.CartService
{
    public class CartServices : ICartServices
    {
        private readonly BabyDbContext _dbContext;

        public CartServices(BabyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<CartViewDto> GetCartItems(int userId)
        {
            var user = _dbContext.Users.Include(u => u.cart).ThenInclude(c => c.cartItems).ThenInclude(ci => ci.product).FirstOrDefault(u => u.Id == userId);
            if(user != null)
            {
                var cartItems = user.cart.cartItems.Select(ci => new CartViewDto
                {
                    Id = ci.CartId,
                    ProductName = ci.product.ProductName,
                    Quantity = ci.Quantity
                }).ToList();

                return cartItems;
            }
            return new List<CartViewDto> ();
        }

        public void AddToCart(int userId, int productId)
        {
            var user = _dbContext.Users.Include(u => u.cart).ThenInclude(u => u.cartItems).FirstOrDefault(u => u.Id == userId);
            var product = _dbContext.products.FirstOrDefault(p => p.Id == productId);

            if(user != null &&  product != null)
            {
                if(user.cart == null)
                {
                    user.cart = new Cart
                    {
                        UserId = userId
                    };
                    
                }
          
            
            var itemExist = user.cart.cartItems.FirstOrDefault(ci => ci.ProductId == productId);

            if(itemExist != null)
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
            _dbContext.SaveChanges();
           }
        }
        public void DeleteCart(int userId, int productId)
        {
            var user = _dbContext.Users.Include(u => u.cart).ThenInclude(u => u.cartItems).FirstOrDefault(u => u.Id == userId);
            var product = _dbContext.products.FirstOrDefault(p => p.Id == productId);

            if(user != null && product != null)
            {
                var item = user.cart.cartItems.FirstOrDefault(ci => ci.ProductId == productId);
                _dbContext.cartItems.Remove(item);
            }
            _dbContext.SaveChanges();

        }
        public void QuantityPlus(int userId, int productId)
        {
            var user = _dbContext.Users.Include(u => u.cart).ThenInclude(u => u.cartItems).FirstOrDefault(u => u.Id ==userId);
            var product = _dbContext.products.FirstOrDefault(p => p.Id == productId);

            if(user != null && product !=null)
            {
                var item = user.cart.cartItems.FirstOrDefault(ci => ci.ProductId == productId);
                item.Quantity = item.Quantity + 1;
            }
            _dbContext.SaveChanges();
        }
        public void QuantityMin(int userId, int productId)
        {
            var user = _dbContext.Users.Include(u => u.cart).ThenInclude(u => u.cartItems).FirstOrDefault(u => u.Id == userId);
            var product = _dbContext.products.FirstOrDefault(p => p.Id == productId);

            if (user != null && product != null)
            {
                var item = user.cart.cartItems.FirstOrDefault(ci => ci.ProductId == productId);
                item.Quantity = item.Quantity - 1;
            }
            _dbContext.SaveChanges();
        }
    }
}
