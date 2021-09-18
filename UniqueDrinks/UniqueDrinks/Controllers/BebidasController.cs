using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UniqueDrinks.Data;
using UniqueDrinks.Models;

namespace UniqueDrinks.Controllers
{
    public class BebidasController : Controller
    {
        private readonly UniqueDb _context;
        private readonly IWebHostEnvironment _caminho;

        public BebidasController(UniqueDb context, IWebHostEnvironment caminho)
        {
            _context = context;
            _caminho = caminho;

        }

        // GET: Bebidas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Bebidas.ToListAsync());
        }

        // GET: Bebidas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bebidas = await _context.Bebidas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bebidas == null)
            {
                return NotFound();
            }

            return View(bebidas);
        }

        // GET: Bebidas/Create
        [Authorize(Roles = "Gestor")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Bebidas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> Create([Bind("Id,Nome,Descricao,Preco,Imagem,Stock,Categoria")] Bebidas bebida, IFormFile fotoBebida)
        {
            string caminhoCompleto = "";
           
            bool hImagem = false;

           
                
                if (fotoBebida.ContentType == "image/jpeg" || fotoBebida.ContentType == "image/jpg" || fotoBebida.ContentType == "image/png")
                {
                   
                    Guid g;
                    g = Guid.NewGuid();
                    string extensao = Path.GetExtension(fotoBebida.FileName).ToLower();
                    string nomeB = g.ToString() + extensao;

                    
                    caminhoCompleto = Path.Combine(_caminho.WebRootPath, "Imagens\\Bebidas\\", nomeB);

                   
                    bebida.Imagem = nomeB;

                   
                    hImagem = true;
                }
                else
                {
                   
                    bebida.Imagem = "noDrink.png";
                }

            

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(bebida);
                    await _context.SaveChangesAsync();

                    // se há imagem, vou guardá-la no disco rígido
                    if (hImagem)
                    {
                        using var streamA = new FileStream(caminhoCompleto, FileMode.Create);
                        await fotoBebida.CopyToAsync(streamA);
                    }  
                    return RedirectToAction(nameof(Index));
                } 

                // caso este catch seja executado, houve algo que correu mal no processo
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);

                }

                return RedirectToAction(nameof(Index));
            }
        
            return View(bebida);
        }

        // GET: Bebidas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bebidas = await _context.Bebidas.FindAsync(id);
            if (bebidas == null)
            {
                return NotFound();
            }
            return View(bebidas);
        }

        // POST: Bebidas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Descricao,Preco,Imagem,Stock,Categoria")] Bebidas bebidas)
        {
            if (id != bebidas.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bebidas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BebidasExists(bebidas.Id))
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
            return View(bebidas);
        }

        // GET: Bebidas/Delete/5
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bebidas = await _context.Bebidas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bebidas == null)
            {
                return NotFound();
            }

            return View(bebidas);
        }

        // POST: Bebidas/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Gestor")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bebidas = await _context.Bebidas.FindAsync(id);
            _context.Bebidas.Remove(bebidas);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BebidasExists(int id)
        {
            return _context.Bebidas.Any(e => e.Id == id);
        }
    }
}
