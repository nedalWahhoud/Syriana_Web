using System.Text.Json.Serialization;

namespace Syriana_Web.Components.Models
{
    public class CartItem
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; } = 0;
        // Ignore the products if they will convert to json
        [JsonIgnore]
        public Product Product { get; set; } = null!;
        [JsonIgnore]
        public string? TextMessage { get; set; }
    }
}
