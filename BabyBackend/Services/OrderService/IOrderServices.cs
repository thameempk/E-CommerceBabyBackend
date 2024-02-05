using BabyBackend.Models;
using BabyBackend.Models.Dto;

namespace BabyBackend.Services.OrderService
{
    public interface IOrderServices
    {
        string OrderCreate(OrderDetailsDto orderDetails);
        List<OrderDetailsDto> Payment(string raz_pay_id, string raz_ord_id, string raz_pay_sig);

        void CreateOrder(int userId, List<CartViewDto> cartViews);
        
        


        

    }
}
