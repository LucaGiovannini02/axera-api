namespace AxeraPJW.Class
{
    public class Users
    {
        public int id { get; set; }
        public string fullname { get; set; }
        public int age { get; set; }    
        public string? parent { get; set; }
        public string mail { get; set; }
        public bool user_verified { get; set; }
        public bool newsletter_allow { get; set; }
        public string? allergies { get; set; }
        public bool waiver_allow { get; set; }
        public bool privacy_allow { get; set; }
        public string phone_number { get; set; }
       

    }
}
