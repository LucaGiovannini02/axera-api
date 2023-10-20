namespace AxeraPJW.Class
{
    public class Resevations
    { 
        public int id { get; set; }
        public int id_meeting { get; set; }
        public int id_users { get; set; }
        public string note { get; set; }
        public bool payment_verified { get; set; }
        public bool retire_allow { get; set; }
    }
}
