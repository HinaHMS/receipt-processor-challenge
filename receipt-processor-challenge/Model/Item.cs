using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace receipt_processor_challenge.Models
{
    public class Item
    {
        [JsonPropertyName("shortDescription")]
        [Required]
        [RegularExpression("^[\\w\\s\\-]+$")]
        public string ShortDescription { get; set; }

        [JsonPropertyName("price")]
        [Required]
        [RegularExpression("^\\d+\\.\\d{2}$")]
        public string Price { get; set; }
    }
}