using Learning_Razor.Models;

namespace Learning_Razor.Services
{
    public class DepartmentService
    {
        private readonly Learning_RazorContext _context;

        public DepartmentService(Learning_RazorContext context)
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
