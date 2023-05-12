using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SecretsKeeper.Data;
using SecretsKeeper.Models;

namespace SecretsKeeper.Controllers
{
    public class SecretsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SecretsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Secrets
        [Authorize]
        public async Task<IActionResult> Index()
        {
              return _context.Secret != null ? 
                          View(await _context.Secret.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Secret'  is null.");
        }

        // GET: Secrets/ShowSearchForm
        [Authorize]
        public async Task<IActionResult> ShowNewSearchForm()
        {
            return View();
        }

        // POST: Secrets/ShowSearchResults
        [Authorize]
        public async Task<IActionResult> ShowSearchResults(String SearchPhrase)
        {
            return _context.Secret != null ?
                        View("Index", await _context.Secret.Where(j => j.Name.Contains(SearchPhrase)).ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Secret'  is null.");
            //return View("Index", await _context.Secret.Where(j => j.Name.Contains(SearchPhrase)).ToListAsync());
        }

        // GET: Secrets/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Secret == null)
            {
                return NotFound();
            }

            var secret = await _context.Secret
                .FirstOrDefaultAsync(m => m.Id == id);
            if (secret == null)
            {
                return NotFound();
            }

            return View(secret);
        }

        // GET: Secrets/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Secrets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,eMail,UserId,Password,Question1,Answer1,Question2,Answer2,Question3,Answer3,PIN,Note,CreationDate")] Secret secret)
        {
            if (ModelState.IsValid)
            {
                _context.Add(secret);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(secret);
        }

        // GET: Secrets/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Secret == null)
            {
                return NotFound();
            }

            var secret = await _context.Secret.FindAsync(id);
            if (secret == null)
            {
                return NotFound();
            }
            return View(secret);
        }

        // POST: Secrets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,eMail,UserId,Password,Question1,Answer1,Question2,Answer2,Question3,Answer3,PIN,Note,CreationDate")] Secret secret)
        {
            if (id != secret.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(secret);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SecretExists(secret.Id))
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
            return View(secret);
        }

        // GET: Secrets/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Secret == null)
            {
                return NotFound();
            }

            var secret = await _context.Secret
                .FirstOrDefaultAsync(m => m.Id == id);
            if (secret == null)
            {
                return NotFound();
            }

            return View(secret);
        }

        // POST: Secrets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Secret == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Secret'  is null.");
            }
            var secret = await _context.Secret.FindAsync(id);
            if (secret != null)
            {
                _context.Secret.Remove(secret);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SecretExists(int id)
        {
          return (_context.Secret?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
