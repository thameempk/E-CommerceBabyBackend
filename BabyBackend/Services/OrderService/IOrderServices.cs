using BabyBackend.Models;
using BabyBackend.Models.Dto;

namespace BabyBackend.Services.OrderService
{
    public interface IOrderServices
    {
        Task<string> OrderCreate(long price);
        public bool Payment(RazorpayDto razorpay);

        Task<bool> CreateOrder(string token, OrderRequestDto orderRequests);

        Task<List<OrderViewDto>> GetOrderDtails(string token);

        Task<decimal> GetTotalRevenue();

        Task<List<OrderAdminViewDto>> GetOrderDetailAdmin();

        Task<OrderAdminDetailViewDto> GetOrderDetailsById(int orderId);

        Task<bool> UpdateOrder(int orderId, UpdateOrderDto updateOrder);
        Task<List<OrderViewDto>> adminUserOrder(int userId);

        Task<int> GetTotalOrders();
        Task<int> TodayOrders();

        Task<decimal> TodayRevenue();









    }
}
