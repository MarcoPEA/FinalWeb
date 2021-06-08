using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProWeb.Data;
using ProWeb.Models;
using Microsoft.AspNetCore.Authorization;

namespace ProWeb.Controllers
{
    public class DetalleController : Controller
    {
        private readonly SpContext _context;

        public DetalleController(SpContext context)
        {
            _context = context;
        }

        // GET: Detalle
        public async Task<IActionResult> Index()
        {
            var spContext = _context.Detalle.Include(d => d.Pedido).Include(d => d.Producto);
            return View(await spContext.ToListAsync());
        }

        // GET: Detalle/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalle = await _context.Detalle
                .Include(d => d.Pedido)
                .Include(d => d.Producto)
                .FirstOrDefaultAsync(m => m.ProductoId == id);
            if (detalle == null)
            {
                return NotFound();
            }

            return View(detalle);
        }

        // GET: Detalle/Create
        public IActionResult Create()
        {
            ViewData["PedidoId"] = new SelectList(_context.Pedidos, "Id", "Id");
            ViewData["ProductoId"] = new SelectList(_context.Productos, "Id", "Id");
            return View();
        }

        // POST: Detalle/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Cantidad,ProductoId,PedidoId")] Detalle detalle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(detalle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PedidoId"] = new SelectList(_context.Pedidos, "Id", "Id", detalle.PedidoId);
            ViewData["ProductoId"] = new SelectList(_context.Productos, "Id", "Id", detalle.ProductoId);
            return View(detalle);
        }

        // GET: Detalle/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalle = await _context.Detalle.FindAsync(id);
            if (detalle == null)
            {
                return NotFound();
            }
            ViewData["PedidoId"] = new SelectList(_context.Pedidos, "Id", "Id", detalle.PedidoId);
            ViewData["ProductoId"] = new SelectList(_context.Productos, "Id", "Id", detalle.ProductoId);
            return View(detalle);
        }

        // POST: Detalle/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Cantidad,ProductoId,PedidoId")] Detalle detalle)
        {
            if (id != detalle.ProductoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(detalle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetalleExists(detalle.ProductoId))
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
            ViewData["PedidoId"] = new SelectList(_context.Pedidos, "Id", "Id", detalle.PedidoId);
            ViewData["ProductoId"] = new SelectList(_context.Productos, "Id", "Id", detalle.ProductoId);
            return View(detalle);
        }

        // GET: Detalle/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalle = await _context.Detalle
                .Include(d => d.Pedido)
                .Include(d => d.Producto)
                .FirstOrDefaultAsync(m => m.ProductoId == id);
            if (detalle == null)
            {
                return NotFound();
            }

            return View(detalle);
        }

        // POST: Detalle/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var detalle = await _context.Detalle.FindAsync(id);
            _context.Detalle.Remove(detalle);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DetalleExists(int id)
        {
            return _context.Detalle.Any(e => e.ProductoId == id);
        }
    }
}
