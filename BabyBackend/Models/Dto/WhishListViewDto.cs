namespace BabyBackend.Models.Dto
{
    public class WhishListViewDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; }

        public string ProductDescription { get; set; }
        public decimal Price {  get; set; }
        public string Category {  get; set; }
    }
}
