using BabyBackend.Models;
using BabyBackend.Models.Dto;

namespace BabyBackend.Services.CartService
{
    public interface ICartServices
    {
        List<CartViewDto> GetCartItems(int userId);
        void AddToCart(int userId, int productId);
        void DeleteCart(int userId, int productId);
        void QuantityPlus(int userId, int productId);
        void QuantityMin(int userId, int productId);
    }
}
