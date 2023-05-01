using SalesWebSeller.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace SalesWebSeller.Models
{
    public class SalesRecord
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }

        [Required]
        [Range(0.0, 30000.0, ErrorMessage = "{0} must be from {1} to {2}")]
        [DisplayFormat(DataFormatString = "${0:F2}")]
        public Double Amount { get; set; }

        [Required]
        public SaleStatus Status { get; set; }
        public Seller? Seller { get; set; }
        public int SellerId { get; set; }

        public SalesRecord() { }
        public SalesRecord(int id, DateTime date, double amount, SaleStatus saleStatus, Seller seller)
        {
            Id = id;
            Date = date;
            Amount = amount;
            Status = saleStatus;
            Seller = seller;
        }
    }
}
