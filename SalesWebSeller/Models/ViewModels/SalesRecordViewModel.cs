using SalesWebSeller.Models.Enums;

namespace SalesWebSeller.Models.ViewModels
{
    public class SalesRecordViewModel
    {
        public SalesRecord? SalesRecord { get; set; }
        public List<SaleStatus>? SaleStatus { get; set; }
        public ICollection<Seller>? Sellers { get; set; }
    }
}
