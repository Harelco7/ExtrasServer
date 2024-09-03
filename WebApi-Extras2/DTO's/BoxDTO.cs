namespace WebApi_Extras2.DTO_s
{
    public class BoxDTO
    {
        public string? BoxName { get; set; }
        public int BoxId { get; set; }

        public string? Description { get; set; }

        public decimal? Price { get; set; }
        public decimal? Sale_Price { get; set; }

        public int? QuantityAvailable { get; set; }

        public int? BusinessID { get; set; }

        public string? AlergicType { get; set;}

    }
}
