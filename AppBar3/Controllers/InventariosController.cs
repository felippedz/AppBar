using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppBar3.Models;

namespace AppBar3.Controllers
{
    public class InventariosController : Controller
    {
        private readonly AppBarContext _context;

        public InventariosController(AppBarContext context)
        {
            _context = context;
        }

        // GET: Inventarios
        public async Task<IActionResult> Index()
        {
            var appBarContext = _context.Inventarios.Include(i => i.IdProductoNavigation).Include(i => i.Usuario);
            return View(await appBarContext.ToListAsync());
        }

        // GET: Inventarios/Details/5
        public async Task<IActionResult> Details(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventario = await _context.Inventarios
                .Include(i => i.IdProductoNavigation)
                .Include(i => i.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (inventario == null)
            {
                return NotFound();
            }

            return View(inventario);
        }

        // GET: Inventarios/Create
        public IActionResult Create()
        {
            ViewBag.TipoMovimiento = new SelectList(new List<string> { "Ingreso", "Salida" });
            ViewData["IdProducto"] = new SelectList(_context.Productos, "Id", "Nombre");
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Correo");
            return View();
        }

        // POST: Inventarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdProducto,TipoMovimiento,Cantidad,FechaMovimiento,UsuarioId")] Inventario inventario)
        {
            inventario.TipoMovimiento = "Ingreso";
            // Obtener el usuario de la base de datos con el UsuarioId seleccionado en el formulario
            var usuario = await _context.Usuarios.FindAsync(inventario.UsuarioId);

            // Verificar si el usuario existe y si tiene el rol de 'Mesero'
            if (usuario != null && usuario.IdRol == 3)
            {
                ModelState.AddModelError("", "Solo Administracion puede agregar inventarios.");
            }
            if (inventario.Cantidad < 0)
            {
                ModelState.AddModelError("Cantidad", "La cantidad no puede ser negativa.");
            }
            if (ModelState.IsValid)
            {
                _context.Add(inventario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdProducto"] = new SelectList(_context.Productos, "Id", "Nombre", inventario.IdProducto);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Correo", inventario.UsuarioId);
            return View(inventario);
        }

        // GET: Inventarios/Edit/5
        public async Task<IActionResult> Edit(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventario = await _context.Inventarios.FindAsync(id);
            if (inventario == null)
            {
                return NotFound();
            }
            ViewData["IdProducto"] = new SelectList(_context.Productos, "Id", "Nombre", inventario.IdProducto);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Correo", inventario.UsuarioId);
            return View(inventario);
        }

        // POST: Inventarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(byte id, [Bind("Id,IdProducto,TipoMovimiento,Cantidad,FechaMovimiento,UsuarioId")] Inventario inventario)
        {
            if (id != inventario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inventario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InventarioExists(inventario.Id))
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
            ViewData["IdProducto"] = new SelectList(_context.Productos, "Id", "Id", inventario.IdProducto);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", inventario.UsuarioId);
            return View(inventario);
        }

        // GET: Inventarios/Delete/5
        public async Task<IActionResult> Delete(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventario = await _context.Inventarios
                .Include(i => i.IdProductoNavigation)
                .Include(i => i.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (inventario == null)
            {
                return NotFound();
            }

            return View(inventario);
        }

        // POST: Inventarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(byte id)
        {
            var inventario = await _context.Inventarios.FindAsync(id);
            if (inventario != null)
            {
                _context.Inventarios.Remove(inventario);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InventarioExists(byte id)
        {
            return _context.Inventarios.Any(e => e.Id == id);
        }
    }
}
