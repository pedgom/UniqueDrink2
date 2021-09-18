using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UniqueDrinks.Data;
using UniqueDrinks.Models;

namespace UniqueDrinks.Controllers
{
    public class ListaReservasController : Controller
    {
        private readonly UniqueDb _context;

        public ListaReservasController(UniqueDb context)
        {
            _context = context;
        }

        // GET: ListaReservas
        public async Task<IActionResult> Index()
        {
            var uniqueDb = _context.ListaReservas.Include(l => l.Cliente);
            return View(await uniqueDb.ToListAsync());
        }

        // GET: ListaReservas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listaReservas = await _context.ListaReservas
                .Include(l => l.Cliente)
                .FirstOrDefaultAsync(m => m.LRId == id);
            if (listaReservas == null)
            {
                return NotFound();
            }

            return View(listaReservas);
        }

        // GET: ListaReservas/Create
        public IActionResult Create()
        {
            ViewData["ClienteFK"] = new SelectList(_context.Clientes, "Id", "Nome");
            return View();
        }

        // POST: ListaReservas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LRId,Reservado,CheckOut,ClienteFK")] ListaReservas listaReservas)
        {
            if (ModelState.IsValid)
            {
                _context.Add(listaReservas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteFK"] = new SelectList(_context.Clientes, "Id", "Nome", listaReservas.ClienteFK);
            return View(listaReservas);
        }

        // GET: ListaReservas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listaReservas = await _context.ListaReservas.FindAsync(id);
            if (listaReservas == null)
            {
                return NotFound();
            }
            ViewData["ClienteFK"] = new SelectList(_context.Clientes, "Id", "Nome", listaReservas.ClienteFK);
            return View(listaReservas);
        }

        // POST: ListaReservas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LRId,Reservado,CheckOut,ClienteFK")] ListaReservas listaReservas)
        {
            if (id != listaReservas.LRId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(listaReservas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ListaReservasExists(listaReservas.LRId))
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
            ViewData["ClienteFK"] = new SelectList(_context.Clientes, "Id", "Nome", listaReservas.ClienteFK);
            return View(listaReservas);
        }

        // GET: ListaReservas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listaReservas = await _context.ListaReservas
                .Include(l => l.Cliente)
                .FirstOrDefaultAsync(m => m.LRId == id);
            if (listaReservas == null)
            {
                return NotFound();
            }

            return View(listaReservas);
        }

        // POST: ListaReservas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var listaReservas = await _context.ListaReservas.FindAsync(id);
            _context.ListaReservas.Remove(listaReservas);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ListaReservasExists(int id)
        {
            return _context.ListaReservas.Any(e => e.LRId == id);
        }
    }
}
