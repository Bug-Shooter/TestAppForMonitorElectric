using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TestAppForMonitorElectric.Models
{
    public class Manufacturer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; private set; } = Guid.Empty;

        [Required(AllowEmptyStrings = false, ErrorMessage = "Field 'Name' was Empty")]
        [MinLength(1)]
        public string Name { get; set; } = string.Empty;

        [Required(AllowEmptyStrings = false, ErrorMessage = "Field 'Counrty' was Empty")]
        [MinLength(1)]
        public string Counrty { get; set; } = string.Empty;

        [JsonIgnore]
        public List<CarModel> CarModels { get; set; } = new List<CarModel>();
    }
}
