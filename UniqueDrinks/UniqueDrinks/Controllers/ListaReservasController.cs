using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<ApplicationUser> _userManager;

        public ListaReservasController(UniqueDb context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: ListaReservas
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var uniqueDb = _context.ListaReservas.Include(l => l.Cliente).Where(c => c.Cliente.Username == user.Id);
            return View(await uniqueDb.ToListAsync());
        }

        public IActionResult Checkout()
        {
            return View();
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

        [Authorize]
        public async Task<IActionResult> CheckoutCart()
        {
            var user = await _userManager.GetUserAsync(User);
            var cliente = _context.Clientes.FirstOrDefault(c => c.Username == user.Id);
            var cart = _context.ListaReservas.FirstOrDefault(c => c.ClienteFK == cliente.Id);
            // TODO: count cart items, must be > 0

            cart.CheckOut = true;
            ListaReservas newCart = new ListaReservas
            {
                Reservado = DateTime.Now,
                CheckOut = false,
                ClienteFK = cliente.Id
            };
            await _context.AddAsync(newCart);
            await _context.SaveChangesAsync();
            return View(nameof(Checkout));
        }
    }
}
