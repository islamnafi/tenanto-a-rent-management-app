using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tenanto.Data;
using tenanto.Models;
using System.Diagnostics;
using tenant.Models;

namespace tenanto.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var tenants = _context.Tenants.ToList();
            return View(tenants);
        }

        public IActionResult TenantDetails(int id)
        {
            var tenant = _context.Tenants
                .Include(t => t.RentRecords
                                .OrderByDescending(r => r.Year)
                                .ThenByDescending(r => r.Month))
                .FirstOrDefault(t => t.TenantId == id);

            if (tenant == null)
            {
                return NotFound();
            }

            return View(tenant);
        }

        public IActionResult AddRent(int tenantId)
        {
            var newRecord = new RentRecord
            {
                TenantId = tenantId,
                Year = DateTime.Now.Year, 
                Month = DateTime.Now.Month 
            };

            return View(newRecord);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddRent(RentRecord rentRecord)
        {
            ModelState.Remove(nameof(rentRecord.Tenant));

            rentRecord.TotalRent = rentRecord.BaseRent +
                                   (rentRecord.ElectricityBill ?? 0) +
                                   (rentRecord.WaterBill ?? 0) +
                                   (rentRecord.GasBill ?? 0) +
                                   (rentRecord.ServiceCharge ?? 0);

            if (ModelState.IsValid)
            {
                _context.RentRecords.Add(rentRecord);
                _context.SaveChanges();
                return RedirectToAction("TenantDetails", new { id = rentRecord.TenantId });
            }

            return View(rentRecord);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}