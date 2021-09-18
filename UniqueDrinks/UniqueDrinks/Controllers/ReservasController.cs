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
    public class ReservasController : Controller
    {
        private readonly UniqueDb _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ReservasController(UniqueDb context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Reservas
        public async Task<IActionResult> Index()
        {
            float total = 0;
            var user = await _userManager.GetUserAsync(User);
            var cliente = _context.Clientes.FirstOrDefault(c => c.Username == user.Id);
            var uniqueDb = _context.Reservas.Include(r => r.Bebida).Include(r => r.ListaReserva).Where(r => r.ListaReserva.ClienteFK == cliente.Id && !r.ListaReserva.CheckOut); ;
            List<Reservas> reserva = await uniqueDb.ToListAsync();
            foreach (var reservas in reserva)
            {
                float preco = reservas.Bebida.Preco;
                int quantidad = reservas.Quantidade;
                total += preco * quantidad;
            }
            ViewBag.total = total;
            return View(reserva);
        }

        // GET: Reservas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservas = await _context.Reservas
                .Include(r => r.Bebida)
                .Include(r => r.ListaReserva)
                .FirstOrDefaultAsync(m => m.ReservaID == id);
            if (reservas == null)
            {
                return NotFound();
            }

            return View(reservas);
        }

        

        // POST: Reservas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            return RedirectToAction("Index");
        }


        [Authorize]
        public async Task<ActionResult> AddToShoppingCart(int? bebidaId)
        {
            if (bebidaId == null)
            {
                return RedirectToAction("Index", "Bebidas"); ;
            }
            var selectB = _context.Bebidas.FirstOrDefault(s => s.Id == bebidaId);

            if (selectB == null)
            {
                return RedirectToAction("Index", "Bebidas"); ;
            }
            var user = await _userManager.GetUserAsync(User);
            var cliente = _context.Clientes.FirstOrDefault(c => c.Username == user.Id);
            var lista = _context.ListaReservas.FirstOrDefault(l => l.ClienteFK == cliente.Id && !l.CheckOut);
            var reserva = new Reservas
            {
                BebidaFK = (int)bebidaId,
                LRIdFK = lista.LRId,
                Quantidade = 1
            };
            _context.Add(reserva);
            _context.SaveChanges();
            return RedirectToAction("Index", "Bebidas");
        }

        // GET: Reservas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservas = await _context.Reservas.FindAsync(id);
            if (reservas == null)
            {
                return NotFound();
            }
            ViewData["BebidaFK"] = new SelectList(_context.Bebidas, "Id", "Categoria", reservas.BebidaFK);
            ViewData["LRIdFK"] = new SelectList(_context.ListaReservas, "LRId", "LRId", reservas.LRIdFK);
            return View(reservas);
        }

        // POST: Reservas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReservaID,Quantidade,LRIdFK,BebidaFK")] Reservas reservas)
        {
            if (id != reservas.ReservaID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservasExists(reservas.ReservaID))
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
            ViewData["BebidaFK"] = new SelectList(_context.Bebidas, "Id", "Categoria", reservas.BebidaFK);
            ViewData["LRIdFK"] = new SelectList(_context.ListaReservas, "LRId", "LRId", reservas.LRIdFK);
            return View(reservas);
        }

        // GET: Reservas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservas = await _context.Reservas
                .Include(r => r.Bebida)
                .Include(r => r.ListaReserva)
                .FirstOrDefaultAsync(m => m.ReservaID == id);
            if (reservas == null)
            {
                return NotFound();
            }

            return View(reservas);
        }

        // POST: Reservas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservas = await _context.Reservas.FindAsync(id);
            _context.Reservas.Remove(reservas);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservasExists(int id)
        {
            return _context.Reservas.Any(e => e.ReservaID == id);
        }
    }
}
