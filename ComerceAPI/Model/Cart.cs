namespace ComerceAPI.Model
{
    public class Cart
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public List<Product> Products { get; set; }=new List<Product>();
    }
}
