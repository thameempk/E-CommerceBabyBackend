using BabyBackend.Migrations;
using BabyBackend.Models;
using BabyBackend.Models.Dto;

namespace BabyBackend.Services.WhishListService
{
    public interface IWhishListServices
    {
         Task AddToWhishList(int userId, int productId);
        Task RemoveWhishList(int whishListId);
        Task<List<WhishListViewDto>> GetWhishLists(int userId);
    }
}
