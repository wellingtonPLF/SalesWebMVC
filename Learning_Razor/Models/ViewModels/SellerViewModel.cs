using Learning_Razor.Models.Enums;

namespace Learning_Razor.Models.ViewModels
{
    public class SellerViewModel
    {
        public Seller? Seller { get; set; }
        public ICollection<Department>? Departments { get; set; }
    }
}
