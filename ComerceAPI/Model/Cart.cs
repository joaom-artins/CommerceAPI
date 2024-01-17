using System.Text.Json.Serialization;

namespace ComerceAPI.Model
{
    public class Cart
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        [JsonIgnore]
        public Client? Client { get; set; }
        [JsonIgnore]
        public List<Product> Products { get; set; }=new List<Product>();
    }
}
