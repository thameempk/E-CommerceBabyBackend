using BabyBackend.Migrations;
using BabyBackend.Models;
using BabyBackend.Models.Dto;

namespace BabyBackend.Services.WhishListService
{
    public interface IWhishListServices
    {
         Task<bool> AddToWhishList(string token, int productId);
        Task RemoveWhishList(string token,int productId);
        Task<List<WhishListViewDto>> GetWhishLists(string token);
        Task<bool> isWishListExist(string token, int productId);
    }
}
