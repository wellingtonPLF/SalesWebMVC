using Learning_Razor.Models.Enums;

namespace Learning_Razor.Models.ViewModels
{
    public class SalesRecordViewModel
    {
        public SalesRecord? SalesRecord { get; set; }
        public List<SaleStatus>? SaleStatus { get; set; }
        public ICollection<Seller>? Sellers { get; set; }
    }
}
