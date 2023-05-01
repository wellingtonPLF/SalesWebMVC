using SalesWebSeller.Models;
using SalesWebSeller.Services.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace SalesWebSeller.Services
{
    public class SalesRecordService
    {
        private readonly SalesWebContext _context;

        public SalesRecordService(SalesWebContext context)
        {
            _context = context;
        }
        
        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = from obj in _context.SalesRecord select obj;
            if (minDate.HasValue)
            {
                result = result.Where(x => x.Date >= minDate.Value);
            }
            if (maxDate.HasValue)
            {
                result = result.Where(x => x.Date <= maxDate.Value);
            }
            return await result
                .Include(x => x.Seller)
                .Include(x => x.Seller!.Department)
                .OrderByDescending(x => x.Date)
                .ToListAsync();
        }

        public async Task<List<IGrouping<Department, SalesRecord>>> 
            FindByDateGroupingAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = from obj in _context.SalesRecord select obj;
            if (minDate.HasValue)
            {
                result = result.Where(x => x.Date >= minDate.Value);
            }
            if (maxDate.HasValue)
            {
                result = result.Where(x => x.Date <= maxDate.Value);
            }
            return await result.Include(x => x.Seller!)
                .Include(x => x.Seller!.Department!)
                .OrderByDescending(x => x.Date!)
                .GroupBy(x => x.Seller!.Department!)
                .ToListAsync();
        }

        public async Task<List<SalesRecord>> FindAllAsync()
        {
            return await _context.SalesRecord.ToListAsync();
        }
        public async Task Insert(SalesRecord obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<SalesRecord?> FindByIdAsync(int id)
        {
            return await _context.SalesRecord.Include(obj => obj.Seller).FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task Remove(int id)
        {
            var obj = await _context.SalesRecord.FindAsync(id);
            _context.SalesRecord.Remove(obj!);
            await _context.SaveChangesAsync();
        }

        public async Task Update(SalesRecord salesRecord)
        {
            
            bool hasAny = await _context.SalesRecord.AnyAsync(x => x.Id == salesRecord.Id);
            if (!hasAny)
            {
                throw new Exception();
            }
            try
            {
                _context.Update(salesRecord);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception();
            }
        }
    }
}
