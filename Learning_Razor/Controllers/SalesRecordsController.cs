using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Learning_Razor.Models;
using Learning_Razor.Services;
using Learning_Razor.Models.ViewModels;
using Learning_Razor.Models.Enums;
using Microsoft.Build.Framework;
using Learning_Razor.Facade;

namespace Learning_Razor.Controllers
{
    public class SalesRecordsController : Controller
    {
        private readonly SalesRecordService _salesRecordService;
        private readonly SellerService _sellerService;
        private readonly Learning_RazorContext _context;

        public SalesRecordsController(Learning_RazorContext context,
            SalesRecordService salesRecordService,
            SellerService sellerService)
        {
            _context = context;
            _salesRecordService = salesRecordService;
            _sellerService = sellerService;
        }

        // GET: SalesRecords
        public IActionResult Index()
        {
            DateTime minDate = new DateTime(DateTime.Now.Year, 1, 1);
            DateTime maxDate = DateTime.Now;
            ViewData["minDate"] = minDate.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = maxDate.ToString("yyyy-MM-dd");
            return View();
        }

        // GET: SalesRecords
        public async Task<IActionResult> Initial()
        {
            var list = await _salesRecordService.FindAllAsync();
            return View(list);
        }

        // GET: SalesRecords
        public async Task<IActionResult> SimpleSearch(DateTime? minDate, DateTime? maxDate)
        {
            if (!minDate.HasValue)
            {
                minDate = new DateTime(DateTime.Now.Year, 1, 1);
            }
            if (!maxDate.HasValue)
            {
                maxDate = DateTime.Now;
            }
            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");
            var obj = await _salesRecordService.FindByDateAsync(minDate, maxDate);
            return View(obj);
        }

        // GET: SalesRecords
        public async Task<IActionResult> GroupingSearch(DateTime? minDate, DateTime? maxDate)
        {
            if (!minDate.HasValue)
            {
                minDate = new DateTime(DateTime.Now.Year, 1, 1);
            }
            if (!maxDate.HasValue)
            {
                maxDate = DateTime.Now;
            }
            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");
            var obj = await _salesRecordService.FindByDateGroupingAsync(minDate, maxDate);
            return View(obj);
        }

        // GET: SalesRecords/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesRecord = await _salesRecordService.FindByIdAsync(id.Value);
            if (salesRecord == null)
            {
                return NotFound();
            }

            return View(salesRecord);
        }

        // GET: SalesRecords/Create
        public IActionResult Create(Fachada fachada)
        {
            List<Seller> sellers = _sellerService.FindAll();
            var status = fachada.GetEnumList<SaleStatus>();
            SalesRecordViewModel viewModel= new SalesRecordViewModel { 
                Sellers = sellers,
                SaleStatus = status
            };
            return View(viewModel);
        }

        // POST: SalesRecords/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SalesRecord salesRecord, Fachada fachada)
        {
            if (!ModelState.IsValid)
            {
                var status = fachada.GetEnumList<SaleStatus>();
                var sellers = _sellerService.FindAll();
                var viewModel = new SalesRecordViewModel { 
                    SalesRecord = salesRecord, 
                    Sellers = sellers,
                    SaleStatus = status
                };
                return View(viewModel);
            }
            await _salesRecordService.Insert(salesRecord);
            return RedirectToAction(nameof(Initial));
        }

        // GET: SalesRecords/Edit/5
        public async Task<IActionResult> Edit(int? id, Fachada fachada)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesRecord = await _salesRecordService.FindByIdAsync(id.Value);
            if (salesRecord == null)
            {
                return NotFound();
            }
            var status = fachada.GetEnumList<SaleStatus>();
            var sellers = _sellerService.FindAll();
            var viewModel = new SalesRecordViewModel { 
                SalesRecord = salesRecord, 
                Sellers = sellers,
                SaleStatus = status
            };
            return View(viewModel);
        }

        // POST: SalesRecords/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SalesRecord salesRecord, Fachada fachada)
        {
            if (!ModelState.IsValid)
            {
                var status = fachada.GetEnumList<SaleStatus>();
                var sellers = _sellerService.FindAll();
                var viewModel = new SalesRecordViewModel { 
                    SalesRecord = salesRecord, 
                    Sellers = sellers,
                    SaleStatus = status
                };
                return View(viewModel);
                
            }
            if (id != salesRecord.Id)
            {
                return NotFound();
            }
            try
            {
                await _salesRecordService.Update(salesRecord);
                return RedirectToAction(nameof(Initial));
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

        }

        // GET: SalesRecords/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesRecord = await _salesRecordService.FindByIdAsync(id.Value);
            if (salesRecord == null)
            {
                return NotFound();
            }
            return View(salesRecord);
        }

        // POST: SalesRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _salesRecordService.Remove(id);
                return RedirectToAction(nameof(Initial));
            }
            catch(ApplicationException e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
