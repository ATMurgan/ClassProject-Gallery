using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClassProject_Gallery.Data;
using ClassProject_Gallery.Models;

namespace ClassProject_Gallery.Controllers
{
    public class ArtworksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ArtworksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Artworks
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Artwork.Include(a => a.Artist).Include(a => a.Category);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Artworks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artwork = await _context.Artwork
                .Include(a => a.Artist)
                .Include(a => a.Category)
                .FirstOrDefaultAsync(m => m.ArtworkId == id);
            if (artwork == null)
            {
                return NotFound();
            }

            return View(artwork);
        }

        // GET: Artworks/Create
        public IActionResult Create()
        {
            ViewData["ArtistId"] = new SelectList(_context.Set<Artist>(), "ArtistId", "Autobiography");
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "CategoryId", "Description");
            return View();
        }

        // POST: Artworks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ArtworkId,Title,Description,Price,ImageUrl,ArtistId,CategoryId")] Artwork artwork)
        {
            if (ModelState.IsValid)
            {
                _context.Add(artwork);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArtistId"] = new SelectList(_context.Set<Artist>(), "ArtistId", "Autobiography", artwork.ArtistId);
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "CategoryId", "Description", artwork.CategoryId);
            return View(artwork);
        }

        // GET: Artworks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artwork = await _context.Artwork.FindAsync(id);
            if (artwork == null)
            {
                return NotFound();
            }
            ViewData["ArtistId"] = new SelectList(_context.Set<Artist>(), "ArtistId", "Autobiography", artwork.ArtistId);
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "CategoryId", "Description", artwork.CategoryId);
            return View(artwork);
        }

        // POST: Artworks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ArtworkId,Title,Description,Price,ImageUrl,ArtistId,CategoryId")] Artwork artwork)
        {
            if (id != artwork.ArtworkId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(artwork);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArtworkExists(artwork.ArtworkId))
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
            ViewData["ArtistId"] = new SelectList(_context.Set<Artist>(), "ArtistId", "Autobiography", artwork.ArtistId);
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "CategoryId", "Description", artwork.CategoryId);
            return View(artwork);
        }

        // GET: Artworks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artwork = await _context.Artwork
                .Include(a => a.Artist)
                .Include(a => a.Category)
                .FirstOrDefaultAsync(m => m.ArtworkId == id);
            if (artwork == null)
            {
                return NotFound();
            }

            return View(artwork);
        }

        // POST: Artworks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var artwork = await _context.Artwork.FindAsync(id);
            if (artwork != null)
            {
                _context.Artwork.Remove(artwork);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArtworkExists(int id)
        {
            return _context.Artwork.Any(e => e.ArtworkId == id);
        }
    }
}
