using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SalesWebSeller.Models;
using SalesWebSeller.Services;
using SalesWebSeller.Models.ViewModels;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using SalesWebSeller.Services.Exceptions;
using System.Diagnostics;

namespace SalesWebSeller.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;

        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }

        // GET: Sellers //ByMe
        public IActionResult Index()
        {
            var list = _sellerService.FindAll();
            return View(list);
        }

        /*
        // GET: Sellers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Seller == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Not Found" });
            }

            var seller = await _context.Seller
                .FirstOrDefaultAsync(m => m.Id == id);
            if (seller == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Not Found" });
            }

            return View(seller);
        }
        */
        // GET: Sellers/Details //ByMe
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Not Found" });
            }

            var obj = _sellerService.FindById(id.Value);

            return View(obj);
        }


        // GET: Sellers/Create //ByMe
        public IActionResult Create()
        {
            var departments = _departmentService.FindAll();
            var viewModel = new SellerViewModel { Departments= departments };
            return View(viewModel);
        }

        // POST: Sellers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Email,BirthDate,BaseSalary")] Seller seller)
        {
            if (ModelState.IsValid)
            {
                _context.Add(seller);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(seller);
        }*/

        //POST: Sellers/Create //ByMe
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller)
        {
            if (!ModelState.IsValid)
            {
                List<Department> departments = _departmentService.FindAll();
                SellerViewModel viewModel = new SellerViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }
            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index));
        }
        /*
        // GET: Sellers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Seller == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Not Found" });
            }

            var seller = await _context.Seller.FindAsync(id);
            if (seller == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Not Found" });
            }
            return View(seller);
        }*/

        // GET: Sellers/Edit //ByMe
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Not Found" });
            }

            var obj = _sellerService.FindById(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Not Found" });
            }

            List<Department> departments = _departmentService.FindAll();
            SellerViewModel viewModel = new SellerViewModel { Seller= obj, Departments = departments };

            return View(viewModel);
        }

        // POST: Sellers/Edit //ByMe
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Seller seller)
        {
            if (!ModelState.IsValid)
            {
                List<Department> departments = _departmentService.FindAll();
                SellerViewModel viewModel = new SellerViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }
            if (id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            }
            try {
                _sellerService.Update(seller);
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }
        /*
        // POST: Sellers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,BirthDate,BaseSalary")] Seller seller)
        {
            if (id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Not Found" });
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(seller);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SellerExists(seller.Id))
                    {
                        return RedirectToAction(nameof(Error), new { message = "Not Found" });
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(seller);
        }
        */
        /*
        // GET: Sellers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Seller == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Not Found" });
            }

            var seller = await _context.Seller
                .FirstOrDefaultAsync(m => m.Id == id);
            if (seller == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Not Found" });
            }

            return View(seller);
        }
        */

        // GET: Sellers/Delete //ByMe
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Not Found" });
            }
            var obj = _sellerService.FindById(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Not Found" });
            }
            return View(obj);
        }

        // POST: Sellers/Delete //ByMe
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            try
            {
                _sellerService.Remove(id);
                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
            
        }

        /*
        // POST: Sellers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Seller == null)
            {
                return Problem("Entity set 'Learning_RazorContext.Seller'  is null.");
            }
            var seller = await _context.Seller.FindAsync(id);
            if (seller != null)
            {
                _context.Seller.Remove(seller);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        */
        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModel);
        }
    }
}
