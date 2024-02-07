using BabyBackend.Models;
using BabyBackend.Models.Dto;

namespace BabyBackend.Services.OrderService
{
    public interface IOrderServices
    {
        Task<string> OrderCreate(long price);
        public List<OrderDetailsDto> Payment(RazorpayDto razorpay);

        Task<bool> CreateOrder(int userId, OrderRequestDto orderRequests);

        Task<List<OrderViewDto>> GetOrderDtails(int userId);

        Task<decimal> GetTotalRevenue();

        Task<List<OrderAdminViewDto>> GetOrderDetailAdmin();

        Task<OrderAdminDetailViewDto> GetOrderDetailsById(int orderId);

        Task<bool> UpdateOrder(int orderId, OrderAdminViewDto orderAdminView);


        
        


        

    }
}
