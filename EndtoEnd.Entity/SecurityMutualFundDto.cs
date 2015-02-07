using System;
using System.ComponentModel.DataAnnotations;

namespace EndtoEnd.Entity
{
    public class SecurityMutualFundDto
    {
        public int Id { get; set; }
        [StringLength(5, MinimumLength = 3,ErrorMessage = "Symbol field Maximum length is 5 and Minimum is 3")]
        [Required]
        public string Symbol { get; set; }
        [Required]
        public int MorningStarRating { get; set; }
        [StringLength(250)]
        [Required]
        public string Company { get; set; }
        [Required]
        [Range(typeof(Decimal), "-9999", "9999", ErrorMessage = "{0} must be a decimal/number between {1} and {2}.")]
        public decimal PercentChange { get; set; }
        [Range(typeof(Decimal), "-9999", "9999", ErrorMessage = "{0} must be a decimal/number between {1} and {2}.")]
        [Required]
        public decimal Shares { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss ttt}")]
        public DateTime RetrievalDateTime { get; set; }
    }
}
