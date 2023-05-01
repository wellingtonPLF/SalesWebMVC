using SalesWebSeller.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using SalesWebSeller.Services.Exceptions;

namespace SalesWebSeller.Services
{
    public class SellerService
    {
        private readonly SalesWebContext _context;

        public SellerService(SalesWebContext context)
        {
            _context = context;
        }

        public List<Seller> FindAll()
        {
            return _context.Seller.ToList();
        }

        public void Insert(Seller obj)
        {
            _context.Add(obj);
            _context.SaveChanges();
        }

        /*
        public async Task Insert(Seller obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }
        */
        public Seller? FindById(int id)
        {
            //Include equivale ao Join
            return _context.Seller.Include( obj => obj.Department).FirstOrDefault( obj => obj.Id == id);
            // return await _context.Seller.Include(obj => obj.Department).FirstOrDefaultAsync(obj => obj.Id == id);
        }
        /*
        public async Task<Seller?> FindByIdAsync(int id)
        {
            return await _context.Seller.Include(obj => obj.Department).FirstOrDefaultAsync(obj => obj.Id == id);
        }
        */

        public void Remove(int id)
        {
            try
            {
                var obj = _context.Seller.Find(id);
                _context.Seller.Remove(obj!);
                _context.SaveChanges();
            }
            catch (ApplicationException)
            {
                throw new IntegrityException("Can't delete seller because he/she has sales.");
            }
        }

        public void Update(Seller seller)
        {
            bool hasAny = !_context.Seller.Any(x => x.Id == seller.Id);
            // bool hasAny = await _context.Seller.AnyAsync(x => x.Id == seller.Id);
            if (hasAny)
            {
                throw new Exception();
            }
            try
            {
                _context.Update(seller);
                _context.SaveChanges();
            }
            catch
            {
                throw new Exception();
            }
        }
    }
}
