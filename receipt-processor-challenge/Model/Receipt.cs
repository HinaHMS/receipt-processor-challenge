using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace receipt_processor_challenge.Models
{
    public class Receipt
    {
        [JsonPropertyName("retailer")]
        [Required]
        [RegularExpression("^[\\w\\s\\-&]+$")]
        public string Retailer { get; set; }

        [JsonPropertyName("purchaseDate")]
        [Required]
        [DataType(DataType.Date)]
        public string PurchaseDate { get; set; }

        [JsonPropertyName("purchaseTime")]
        [Required]
        [DataType(DataType.Time)]
        public string PurchaseTime { get; set; }

        [JsonPropertyName("items")]
        [Required]
        [MinLength(1)]
        public List<Item> Items { get; set; }

        [JsonPropertyName("total")]
        [Required]
        [RegularExpression("^\\d+\\.\\d{2}$")]
        public string Total { get; set; }
    }
}