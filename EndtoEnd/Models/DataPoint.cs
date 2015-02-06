using System.ComponentModel.DataAnnotations;

namespace EndtoEnd.Models
{
    public class DataPoint
    {
        [Key]
        public long Id { get; set; }

        public string Time { get; set; }
        public long JSTicks { get; set; }
        public decimal Value { get; set; } 
    }
}
