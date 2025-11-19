using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication10_Nov19.Data;
using WebApplication10_Nov19.Models;

namespace WebApplication10_Nov19.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class PurchaseController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<IdentityUser> _userManager;

        public PurchaseController(ApplicationDbContext context,
            UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Purchase
        public async Task<IActionResult> Index()
        {


            return View(await _context.Purchases.ToListAsync());
        }

        // GET: Purchase/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseModel = await _context.Purchases
                .FirstOrDefaultAsync(m => m.PurchaseId == id);
            if (purchaseModel == null)
            {
                return NotFound();
            }

            return View(purchaseModel);
        }

        [Authorize(Roles = "Admin")]
        // GET: Purchase/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Purchase/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PurchaseId,Name,Price")] PurchaseModel purchaseModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(purchaseModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(purchaseModel);
        }

        // GET: Purchase/Edit/5
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Edit(int? id)
        {

            var user = await _userManager.GetUserAsync(User);

            if (await _userManager.IsInRoleAsync(user, "Admin"))
            {

                TempData["Message"] = "Admin is logged in";
            }
            else {
                TempData["Message"] = "Manager alert!";

            }

            if (id == null)
            {
                return NotFound();
            }

            var purchaseModel = await _context.Purchases.FindAsync(id);
            if (purchaseModel == null)
            {
                return NotFound();
            }
            return View(purchaseModel);
        }

        // POST: Purchase/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Edit(int id, [Bind("PurchaseId,Name,Price")] PurchaseModel purchaseModel)
        {
            if (id != purchaseModel.PurchaseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(purchaseModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PurchaseModelExists(purchaseModel.PurchaseId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(purchaseModel);
        }

        // GET: Purchase/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseModel = await _context.Purchases
                .FirstOrDefaultAsync(m => m.PurchaseId == id);
            if (purchaseModel == null)
            {
                return NotFound();
            }

            return View(purchaseModel);
        }

        // POST: Purchase/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var purchaseModel = await _context.Purchases.FindAsync(id);
            if (purchaseModel != null)
            {
                _context.Purchases.Remove(purchaseModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PurchaseModelExists(int id)
        {
            return _context.Purchases.Any(e => e.PurchaseId == id);
        }
    }
}
