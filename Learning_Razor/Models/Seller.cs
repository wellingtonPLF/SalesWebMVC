using System.ComponentModel.DataAnnotations;

namespace Learning_Razor.Models
{
    public class Seller
    {
        public int Id { get; set; }

        [Required] //Falar que é obrigatorio
        [StringLength(45, MinimumLength = 3, ErrorMessage = "{0} size should be bettween {2} and {1}.")]
        public string? Name { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Enter a valid email")]
        public string? Email { get; set; }

        [Display(Name= "Birth Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Base Salary")]
        [Range(100.0, 5000.0, ErrorMessage = "{0} must be from {1} to {2}")]
        [DisplayFormat(DataFormatString = "${0:F2}")]
        public Double BaseSalary { get; set; }
        public Department? Department { get; set; }
        public int DepartmentId { get; set; }
        public ICollection<SalesRecord> SalesRecord { get; set; } = new List<SalesRecord>();

        public Seller() { }

        public Seller(int id, string? name, string? email, DateTime birthDate, double baseSalary, Department department)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            BaseSalary = baseSalary;
            Department = department;
        }
        public void AddSales(SalesRecord sr)
        {
            SalesRecord.Add(sr);
        }
        public void RemoveSales(SalesRecord sr)
        {
            SalesRecord.Remove(sr);
        }
        public double TotalSales(DateTime initial, DateTime final)
        {
            return SalesRecord.Where(sr => sr.Date >= initial && sr.Date <= final).Sum(sr => sr.Amount);
        }
    }
}
