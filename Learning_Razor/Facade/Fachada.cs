using Learning_Razor.Models.Enums;

namespace Learning_Razor.Facade
{
    public class Fachada
    {
        public Fachada() { }

        //list enum
        public List<SaleStatus> GetEnumList<SaleStatus>() where SaleStatus : Enum
        => ((SaleStatus[])Enum.GetValues(typeof(SaleStatus))).ToList();
    }
}
