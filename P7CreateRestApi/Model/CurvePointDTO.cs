using System.ComponentModel.DataAnnotations;

namespace P7CreateRestApi.Model
{
    public class CurvePointDTO
    {
        [Required(ErrorMessage = "The curve Id is required.")]
        public byte? CurveId { get; set; }
        public DateTime? AsOfDate { get; set; }
        public double? CurvePointValue { get; set; }
    }
}
