using BabyBackend.Migrations;
using BabyBackend.Models;
using BabyBackend.Models.Dto;

namespace BabyBackend.Services.WhishListService
{
    public interface IWhishListServices
    {
        void AddToWhishList(int userId, int productId);
        void RemoveWhishList(int whishListId);
        List<WhishListViewDto> GetWhishLists(int userId);
    }
}
