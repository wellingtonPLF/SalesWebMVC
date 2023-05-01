﻿using SalesWebSeller.Models.Enums;

namespace SalesWebSeller.Models.ViewModels
{
    public class SellerViewModel
    {
        public Seller? Seller { get; set; }
        public ICollection<Department>? Departments { get; set; }
    }
}
