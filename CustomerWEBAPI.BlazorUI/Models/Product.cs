namespace CustomerWEBAPI.BlazorUI.Models
{
    public class Product
    {
        public int prod_id { get; set; }
        public string prod_desc { get; set; } = string.Empty;
        public string prod_cd { get; set; } = string.Empty;
        public decimal Rate { get; set; }
    }
}
