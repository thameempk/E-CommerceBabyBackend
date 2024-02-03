using AutoMapper;
using BabyBackend.Controllers;
using BabyBackend.DbContexts;
using BabyBackend.Migrations;
using BabyBackend.Models;
using BabyBackend.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace BabyBackend.Services.WhishListService
{
    public class WhishListServices : IWhishListServices
    {
        private readonly BabyDbContext _dbContext;
        private readonly IMapper _mapper;

        public WhishListServices(BabyDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void AddToWhishList(int userId, int productId)
        {
            WhishListDto whishListDto = new WhishListDto
            {
                userId = userId,
                ProductId = productId
            };
            var wMapper = _mapper.Map<WhishList>(whishListDto);
            _dbContext.whishLists.Add(wMapper);
            _dbContext.SaveChanges();
        }
        public void RemoveWhishList(int whishListId)
        {
            var wList = _dbContext.whishLists.Find(whishListId);
            _dbContext.whishLists.Remove(wList);
            _dbContext.SaveChanges();
        }
        public List<WhishListViewDto> GetWhishLists(int userId)
        {
            var wList = _dbContext.whishLists.Include(w => w.products).ThenInclude(p => p.Category).Where(u => u.UserId == userId).ToList();
            var wView = wList.Select(w => new WhishListViewDto
            {
                Id = w.Id,
                ProductName = w.products?.ProductName,
                ProductDescription = w.products?.ProductDescription,
                Price = w.products.Price,
                Category = w.products?.Category.Name
            }).ToList();

            return wView;

        }


    }
}
