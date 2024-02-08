using BabyBackend.Migrations;
using BabyBackend.Models;
using BabyBackend.Models.Dto;

namespace BabyBackend.Services.WhishListService
{
    public interface IWhishListServices
    {
         Task<bool> AddToWhishList(int userId, int productId);
        Task RemoveWhishList(int productId);
        Task<List<WhishListViewDto>> GetWhishLists(int userId);
    }
}
