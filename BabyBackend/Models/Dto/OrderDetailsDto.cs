using System.ComponentModel.DataAnnotations.Schema;

namespace BabyBackend.Models.Dto
{
    public class OrderDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile {  get; set; }

        public double TotalAmount {  get; set; }

        [NotMapped]

        public string TransactionId {  get; set; }

        [NotMapped]
        
        public string OrderId {  get; set; }
    }
}
