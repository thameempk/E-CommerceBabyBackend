using BabyBackend.Models;
using BabyBackend.Models.Dto;

namespace BabyBackend.Services.CartService
{
    public interface ICartServices
    {
        Task<List<CartViewDto>> GetCartItems(string token);
        Task AddToCart(string token, int productId);
        Task DeleteCart(string token, int productId);
        Task QuantityPlus(string token, int productId);
        Task QuantityMin(string token, int productId);
    }
}
