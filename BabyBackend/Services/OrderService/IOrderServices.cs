using BabyBackend.Models;
using BabyBackend.Models.Dto;

namespace BabyBackend.Services.OrderService
{
    public interface IOrderServices
    {
        string OrderCreate(long price);
        List<OrderDetailsDto> Payment(RazorpayDto razorpay);

        void CreateOrder(int userId, List<CartViewDto> cartViews);

        List<OrderViewDto> GetOrderDtails(int userId);

        decimal GetTotalRevenue();
        
        


        

    }
}
