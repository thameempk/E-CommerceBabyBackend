using BabyBackend.Models;
using BabyBackend.Models.Dto;

namespace BabyBackend.Services.CartService
{
    public interface ICartServices
    {
        Task<List<CartViewDto>> GetCartItems(int userId);
        Task AddToCart(int userId, int productId);
        Task DeleteCart(int userId, int productId);
        Task QuantityPlus(int userId, int productId);
        Task QuantityMin(int userId, int productId);
    }
}
