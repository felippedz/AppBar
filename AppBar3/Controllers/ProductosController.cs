using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppBar3.Models;
using ClosedXML.Excel;

namespace AppBar3.Controllers
{
    public class ProductosController : Controller
    {
        private readonly AppBarContext _context;

        public ProductosController(AppBarContext context)
        {
            _context = context;
        }


        // GET: Productos
        public async Task<IActionResult> Index()
        {
            var appBarContext = _context.Productos
                .Include(p => p.IdCategoriaNavigation)
                .Include(p => p.IdEnvaseNavigation)
                .Include(p => p.IdMarcaNavigation);
            return View(await appBarContext.ToListAsync());
        }

        [HttpGet]
        public JsonResult ObtenerSubtotalYTotal(byte productoId, int cantidad)
        {
            var producto = _context.Productos
                                   .Where(p => p.Id == productoId)
                                   .FirstOrDefault();

            if (producto == null)
            {
                return Json(new { error = "Producto no encontrado" });
            }

            var precioUnitario = producto.PrecioVenta;
            var subtotal = cantidad * precioUnitario;
            var total = subtotal * 1.21m; // Aplica el IVA

            return Json(new { subtotal, total });
        }

        // GET: Productos/Details/5
        public async Task<IActionResult> Details(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .Include(p => p.IdCategoriaNavigation)
                .Include(p => p.IdEnvaseNavigation)
                .Include(p => p.IdMarcaNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // Método para obtener productos filtrados por categoría
        [HttpGet]
        public async Task<IActionResult> GetProductsByCategory(byte categoryId)
        {
            var productos = await _context.Productos
                .Where(p => p.IdCategoria == categoryId)
                .Select(p => new
                {
                    Id = p.Id,
                    Nombre = p.Nombre
                })
                .ToListAsync();

            return Json(productos);
        }

        // GET: Productos/Create
        public IActionResult Create()
        {
            ViewData["IdCategoria"] = new SelectList(_context.CategoriaProductos, "Id", "NombreCategoria");
            ViewData["IdEnvase"] = new SelectList(_context.EnvaseProductos, "Id", "NombreEnvase");
            ViewData["IdMarca"] = new SelectList(_context.MarcaProductos, "Id", "NombreMarca");
            return View();
        }

        // POST: Productos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Descripcion,Precio,PrecioVenta,Estado,IdCategoria,IdEnvase,IdMarca")] Producto producto)
        {

            if (producto.Precio < 0)
            {
                ModelState.AddModelError("Precio", "La cantidad no puede ser negativa.");
            }
            if (ModelState.IsValid)
            {
                producto.Estado = true;
                producto.Precio = decimal.Round((decimal)producto.Precio, 2, MidpointRounding.AwayFromZero);
                _context.Add(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCategoria"] = new SelectList(_context.CategoriaProductos, "Id", "NombreCategoria", producto.IdCategoria);
            ViewData["IdEnvase"] = new SelectList(_context.EnvaseProductos, "Id", "NombreEnvase", producto.IdEnvase);
            ViewData["IdMarca"] = new SelectList(_context.MarcaProductos, "Id", "NombreMarca", producto.IdMarca);
            return View(producto);
        }



        // GET: Productos/Edit/5
        public async Task<IActionResult> Edit(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            ViewData["IdCategoria"] = new SelectList(_context.CategoriaProductos, "Id", "NombreCategoria", producto.IdCategoria);
            ViewData["IdEnvase"] = new SelectList(_context.EnvaseProductos, "Id", "NombreEnvase", producto.IdEnvase);
            ViewData["IdMarca"] = new SelectList(_context.MarcaProductos, "Id", "NombreMarca", producto.IdMarca);
            return View(producto);
        }

        // POST: Productos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(byte id, [Bind("Id,Nombre,Descripcion,Precio,PrecioVenta,Estado,IdCategoria,IdEnvase,IdMarca")] Producto producto)
        {
            if (id != producto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    producto.Precio = decimal.Round((decimal)producto.Precio, 2, MidpointRounding.AwayFromZero);
                    _context.Update(producto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(producto.Id))
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
            ViewData["IdCategoria"] = new SelectList(_context.CategoriaProductos, "Id", "Id", producto.IdCategoria);
            ViewData["IdEnvase"] = new SelectList(_context.EnvaseProductos, "Id", "Id", producto.IdEnvase);
            ViewData["IdMarca"] = new SelectList(_context.MarcaProductos, "Id", "Id", producto.IdMarca);
            return View(producto);
        }

        // GET: Productos/Delete/5
        public async Task<IActionResult> Delete(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .Include(p => p.IdCategoriaNavigation)
                .Include(p => p.IdEnvaseNavigation)
                .Include(p => p.IdMarcaNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(byte id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto != null)
            {
                _context.Productos.Remove(producto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult GenerarReporte()
        {
            var reportData = _context.DetallePedidos
                .Include(dp => dp.IdProducto)
                .Include(dp => dp.IdPedido)
                .ToList();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Reporte de Ventas");
                worksheet.Cell(1, 1).Value = "Producto";
                worksheet.Cell(1, 2).Value = "Cantidad";
                worksheet.Cell(1, 3).Value = "Precio";
                worksheet.Cell(1, 4).Value = "Total";

                for (int i = 0; i < reportData.Count; i++)
                {
                    worksheet.Cell(i + 2, 1).Value = reportData[i].IdProductoNavigation.Nombre;
                    worksheet.Cell(i + 2, 2).Value = reportData[i].Cantidad;
                    worksheet.Cell(i + 2, 3).Value = reportData[i].IdProductoNavigation.PrecioVenta;
                    worksheet.Cell(i + 2, 4).Value = reportData[i].Cantidad * reportData[i].IdProductoNavigation.PrecioVenta;
                }

                var stream = new MemoryStream();
                workbook.SaveAs(stream);
                stream.Position = 0;
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ReporteVentas.xlsx");
            }
        }
        private bool ProductoExists(byte id)
        {
            return _context.Productos.Any(e => e.Id == id);
        }
    }
}
