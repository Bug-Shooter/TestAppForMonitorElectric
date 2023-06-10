using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TestAppForMonitorElectric.Models
{
    public class CarModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; private set; } = Guid.Empty;

        [Required(AllowEmptyStrings = false, ErrorMessage = "Field 'Name' was Empty")]
        [MinLength(1)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Range(1,100000,ErrorMessage = "Parameter 'Weight' is not in allowed Range")]
        public int Weight { get; set; } = 0;

        [Required]
        [Range(1, 100000, ErrorMessage = "Parameter 'StartSellPrice' is not in allowed Range")]
        public int StartSellPrice { get; set; } = 0;

        [Required]
        public Guid ManufacturerID { get; set; }

        [JsonIgnore]
        public Manufacturer? Manufacturer { get; set; }
    }
}
