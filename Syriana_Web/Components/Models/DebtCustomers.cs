using System.Text.Json.Serialization;

namespace Syriana_Web.Components.Models
{
    public class DebtCustomers
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public decimal Balance { get; set; }
    }
}
