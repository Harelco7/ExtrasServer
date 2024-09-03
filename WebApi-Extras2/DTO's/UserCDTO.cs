namespace WebApi_Extras2.DTO_s
{
    public class UserCDTO
    {
        //User Inputs
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

        public int? UserID { get; set; }



        //Customer Inputs
        public string? CustomerName { get; set; }
        public int? Age { get; set; }
        public string? Gender { get; set; }
        public int CustomerID { get; set; }


    }
}
