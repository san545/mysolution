namespace CustomerWEBAPI.BlazorUI.Models
{
    public class Customer
    {
        public int cust_id { get; set; }
        public string cust_name { get; set; } = string.Empty;
        public string gst_no { get; set; } = string.Empty;
        public string mob_no { get; set; } = string.Empty;
        public string city { get; set; } = string.Empty;
        public string country { get; set; } = string.Empty;
        public string address { get; set; } = string.Empty;
    }
}
