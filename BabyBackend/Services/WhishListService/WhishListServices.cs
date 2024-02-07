using AutoMapper;
using BabyBackend.Controllers;
using BabyBackend.DbContexts;
using BabyBackend.Migrations;
using BabyBackend.Models;
using BabyBackend.Models.Dto;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task AddToWhishList(int userId, int productId)
        {
            WhishListDto whishListDto = new WhishListDto
            {
                userId = userId,
                ProductId = productId
            };
            var wMapper = _mapper.Map<WhishList>(whishListDto);
            _dbContext.whishLists.Add(wMapper);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveWhishList(int whishListId)
        {
            var wList = await _dbContext.whishLists.FindAsync(whishListId);
            if (wList != null)
            {
                _dbContext.whishLists.Remove(wList);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<WhishListViewDto>> GetWhishLists(int userId)
        {
            var wList = await _dbContext.whishLists
                .Include(w => w.products)
                .ThenInclude(p => p.Category)
                .Where(u => u.UserId == userId)
                .ToListAsync();

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
