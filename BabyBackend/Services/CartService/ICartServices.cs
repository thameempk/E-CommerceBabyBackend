using BabyBackend.Models;

namespace BabyBackend.Services.CartService
{
    public interface ICartServices
    {
        void AddToCart(int userId, int productId);
        void DeleteCart(int userId, int productId);
        void QuantityPlus(int userId, int productId);
        void QuantityMin(int userId, int productId);
    }
}
