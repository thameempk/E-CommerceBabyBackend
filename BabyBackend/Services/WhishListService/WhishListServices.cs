using AutoMapper;
using BabyBackend.Controllers;
using BabyBackend.DbContexts;
using BabyBackend.JwtVerification;
using BabyBackend.Migrations;
using BabyBackend.Models;
using BabyBackend.Models.Dto;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Razorpay.Api;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BabyBackend.Services.WhishListService
{
    public class WhishListServices : IWhishListServices
    {
        private readonly BabyDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly string HostUrl;
        private readonly IJwtServices _jwtServices;


        public WhishListServices(BabyDbContext dbContext, IMapper mapper, IConfiguration configuration, IJwtServices jwtServices)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _configuration = configuration;
            HostUrl = _configuration["HostUrl:url"];
            _jwtServices = jwtServices;

        }

        public async Task<bool> AddToWhishList(string token, int productId)
        {
            int userId = _jwtServices.GetUserIdFromToken(token);
            if (userId == null)
            {
                throw new Exception("user id not valid");
            }
            var wExist = _dbContext.whishLists.Include(w=>w.products).FirstOrDefault(w => w.ProductId == productId);
            if(wExist == null)
            {
                WhishListDto whishListDto = new WhishListDto
                {
                    userId = userId,
                    ProductId = productId
                };
                var wMapper = _mapper.Map<WhishList>(whishListDto);
                _dbContext.whishLists.Add(wMapper);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            _dbContext.whishLists.Remove(wExist);
            await _dbContext.SaveChangesAsync();
            return true;
            
        }

        public async Task RemoveWhishList(string token,int productId)
        {
            int userId = _jwtServices.GetUserIdFromToken(token);
            if (userId == null)
            {
                throw new Exception("user id not valid");
            }
            var wList = await _dbContext.whishLists.FirstOrDefaultAsync(p=>p.UserId==userId && p.ProductId == productId);
            if (wList != null)
            {
                _dbContext.whishLists.Remove(wList);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<WhishListViewDto>> GetWhishLists(string token)
        {
            int userId = _jwtServices.GetUserIdFromToken(token);
            if (userId == null)
            {
                throw new Exception("user id not valid");
            }
            var wList = await _dbContext.whishLists
                .Include(w => w.products)
                .ThenInclude(p => p.Category)
                .Where(u => u.UserId == userId)
                .ToListAsync();
            if(wList != null)
            {
                var wView = wList.Select(w => new WhishListViewDto
                {
                    Id = w.products.Id,
                    ProductName = w.products?.ProductName,
                    ProductDescription = w.products?.ProductDescription,
                    Price = w.products.Price,
                    Category = w.products?.Category.Name,
                    ProductImage = HostUrl + w.products.ProductImage

                }).ToList();

                return wView;
            }else
            {
                return new List<WhishListViewDto>();
            }
            
        }

        public async Task<bool> isWishListExist(string token, int productId)
        {
            int userId = _jwtServices.GetUserIdFromToken(token);
            if (userId == null)
            {
                throw new Exception("user id not valid");
            }
            var wList = await _dbContext.whishLists.FirstOrDefaultAsync(p => p.UserId == userId && p.ProductId == productId);
            if(wList == null)
            {
                return true;
            }
            return false;

        }
    }
}
