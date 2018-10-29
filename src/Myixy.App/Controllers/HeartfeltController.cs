using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Myixy.App.Data;
using Myixy.App.Models;
using MyixyUtilities = Myixy.App.Utilities;

namespace Myixy.App.Controllers
{
    [Authorize]
    public class HeartfeltController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger<HeartfeltController> _logger;

        public HeartfeltController(AppDbContext context, ILogger<HeartfeltController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [AllowAnonymous]
        public IActionResult Home()
        {
            return View();
        }
        
        // GET: Heartfelts
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("Index page");
            return View(await _context.Heartfelts.OrderByDescending(t => t.CreatedDatetime).ToListAsync());
        }

        // GET: Heartfelts/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var heartfelts = await _context.Heartfelts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (heartfelts == null)
            {
                return NotFound();
            }

            return View(heartfelts);
        }

        // GET: Heartfelts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Heartfelts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CreatedDatetime,UserId,Content")] Heartfelt heartfelt)
        {
            if (ModelState.IsValid)
            {
                heartfelt.CreatedDatetime = MyixyUtilities.Common.GetChinaStandardTimeNow();
                _context.Add(heartfelt);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(heartfelt);
        }

        // GET: Heartfelts/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var heartfelt = await _context.Heartfelts.FindAsync(id);
            if (heartfelt == null)
            {
                return NotFound();
            }
            return View(heartfelt);
        }

        // POST: Heartfelts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,CreatedDatetime,UserId,Content")] Heartfelt heartfelt)
        {
            if (id != heartfelt.Id)
            {
                _logger.LogError($"{id} doesn't exist");
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(heartfelt);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    _logger.LogCritical("DbUpdateConcurrencyException in Edit submitting");
                    if (!HeartfeltExists(heartfelt.Id))
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
            return View(heartfelt);
        }

        // GET: Heartfelts/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var heartfelt = await _context.Heartfelts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (heartfelt == null)
            {
                return NotFound();
            }

            return View(heartfelt);
        }

        // POST: Heartfelts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var heartfelt = await _context.Heartfelts.FindAsync(id);
            _context.Heartfelts.Remove(heartfelt);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HeartfeltExists(string id)
        {
            return _context.Heartfelts.Any(e => e.Id == id);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
