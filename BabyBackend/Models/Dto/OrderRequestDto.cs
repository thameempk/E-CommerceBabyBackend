namespace BabyBackend.Models.Dto
{
    public class OrderRequestDto
    {
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhone {  get; set; }
        public string CustomerCity {  get; set; }
        public string HomeAddress {  get; set; }
        public string OrderString {  get; set; }
        public string TransactionId {  get; set; }

        public List<CartViewDto> CartViews { get; set; }

    }
}
