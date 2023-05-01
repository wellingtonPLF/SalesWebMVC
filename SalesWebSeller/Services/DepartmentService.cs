using SalesWebSeller.Models;

namespace SalesWebSeller.Services
{
    public class DepartmentService
    {
        private readonly SalesWebContext _context;

        public DepartmentService(SalesWebContext context)
        {
            _context = context;
        }

        public List<Department> FindAll()
        {
            return _context.Department.OrderBy(x => x.Name).ToList();
        }

        /*
        public async Task<List<Department>> FindAllAsync(){
            return await _context.Department.OrderBy(x => x.Name).ToListAsync();
        }
        */
    }
}
