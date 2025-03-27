using System.ComponentModel.DataAnnotations;

namespace P7CreateRestApi.Model
{
    public class BidListDTO
    {

            [Required(ErrorMessage = "Account is required.")]
            public string Account { get; set; }

            [Required(ErrorMessage = "AccountType is required.")]
            public string BidType { get; set; }

            public double? BidQuantity { get; set; }
    }
}

