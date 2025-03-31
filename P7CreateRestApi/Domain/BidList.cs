using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dot.Net.WebApi.Domain
{
    public class BidList
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BidListId { get; set; }

        public string Account { get; set; }

        public string BidType { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "BidQuantity must be greater than 0.")]
        public double? BidQuantity { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "AskQuantity must be greater than 0.")]
        public double? AskQuantity { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Bid must be greater than 0.")]
        public double? Bid { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Ask must be greater than 0.")]
        public double? Ask { get; set; }

        [StringLength(50, ErrorMessage = "Benchmark cannot be longer than 50 characters.")]
        public string? Benchmark { get; set; }
        
        public DateTime BidListDate { get; set; }

        public string? Commentary { get; set; }

        [StringLength(50, ErrorMessage = "BidSecurity cannot be longer than 50 characters.")]
        public string? BidSecurity { get; set; }

        [StringLength(50, ErrorMessage = "BidStatus cannot be longer than 50 characters.")]
        public string? BidStatus { get; set; }

        [StringLength(50, ErrorMessage = "Trader cannot be longer than 50 characters.")]
        public string? Trader { get; set; }

        public string? Book { get; set; }

        public string? CreationName { get; set; }

        public DateTime CreationDate { get; set; }

        public string? RevisionName { get; set; }

        [Compare("CreationDate", ErrorMessage = "Revision Date must be after Creation Date.")]
        public DateTime RevisionDate { get; set; }

        public string? DealName { get; set; }

        public string? DealType { get; set; }

        public string? SourceListId { get; set; }

        public string? Side { get; set; }

        public void Validate()
        {

        }
    }
}