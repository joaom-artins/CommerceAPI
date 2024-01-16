using System.Text.Json.Serialization;

namespace ComerceAPI.Model
{
    public class Client
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? CPF { get; set; }
        [JsonIgnore]
        public Cart? Cart { get; set; }
    }
}
