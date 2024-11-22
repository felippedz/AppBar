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
    public class PedidoesController : Controller
    {
        private readonly AppBarContext _context;

        public PedidoesController(AppBarContext context)
        {
            _context = context;
        }

        private List<string> GetMetodoPago()
        {
            return new List<string> { "Tarjeta Credito", "Tarjeta Debito" };
        }
        // GET: Pedidoes
        public async Task<IActionResult> Index()
        {
          
            var appBarContext = _context.Pedidos.Include(p => p.IdMesaNavigation);
            var pedidos = await appBarContext.ToListAsync();

            // Crear lista de mesas con DisplayName
            var mesas = _context.Mesas.Include(m => m.IdSedeNavigation)
                .Select(m => new
                {
                    m.Id, // ID de la mesa
                    DisplayName = $"{m.NombreMesa} - {m.IdSedeNavigation.NombreSede}" // Combinación de nombre de mesa y sede
                }).ToList();

            // Pasar la lista de mesas a la vista
            ViewData["Mesas"] = mesas;
            return View(pedidos);
            //return View(await appBarContext.ToListAsync());
        }

        // GET: Pedidoes/Details/5
        public async Task<IActionResult> Details(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedidos
                .Include(p => p.IdMesaNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        // GET: Pedidoes/Create
        //public IActionResult Create()
        //{
        //    var mesas = _context.Mesas.Include(m => m.IdSedeNavigation)
        //                     .Select(m => new
        //                     {
        //                         m.Id, // ID de la mesa
        //                         DisplayName = $"{m.NombreMesa} - {m.IdSedeNavigation.NombreSede}" // Combinación de nombre de mesa y sede
        //                     }).ToList();

        //    ViewData["IdMesa"] = new SelectList(_context.Mesas.Where(m => m.Estado == true), "Id", "DisplayName");
        //    ViewData["MetodosPago"] = new SelectList(new[]
        //    {
        //    new { ID = 1, Nombre = "Tarjeta Credito" },
        //    new { ID = 2, Nombre = "Tarjeta Debito" }
        //     } , "ID", "Nombre");

        //    return View();
        //}

        // GET: Pedidoes/Create
        public IActionResult Create()
        {
            // Aquí cargamos las mesas y los métodos de pago
            var mesas = _context.Mesas.Include(m => m.IdSedeNavigation)
                .Select(m => new
                {
                    m.Id, // ID de la mesa
                    DisplayName = $"{m.NombreMesa} - {m.IdSedeNavigation.NombreSede}" // Combinación de nombre de mesa y sede
                }).ToList();

            ViewData["IdMesa"] = new SelectList(mesas, "Id", "DisplayName");
            ViewData["MetodosPago"] = new SelectList(new List<string> { "Tarjeta Credito", "Tarjeta Debito" });

            return View();
        }

        // POST: Pedidoes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FechaPedido,EstadoPedido,Total,MetodoPago,IdMesa")] Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                pedido.FechaPedido = DateOnly.FromDateTime(DateTime.Now);
                pedido.EstadoPedido = "EN PROCESO";
                pedido.Total = 0; // El total será calculado en los detalles

                _context.Add(pedido);
                await _context.SaveChangesAsync();

                // Después de crear el pedido, redirigimos al usuario a la página para agregar los detalles del pedido.
                return RedirectToAction("Create", "DetallePedidoes", new { id = pedido.Id });
            }

            // Si hay un error, vuelve a mostrar los datos de la vista Create
            var mesas = _context.Mesas.Include(m => m.IdSedeNavigation)
                .Select(m => new
                {
                    m.Id,
                    DisplayName = $"{m.NombreMesa} - {m.IdSedeNavigation.NombreSede}"
                }).ToList();

            ViewData["IdMesa"] = new SelectList(mesas, "Id", "DisplayName", pedido.IdMesa);
            ViewData["MetodosPago"] = new SelectList(new List<string> { "Tarjeta Credito", "Tarjeta Debito" }, pedido.MetodoPago);
            return View(pedido);
        }
        // GET: Pedidoes/Details/5
        public async Task<IActionResult> Details(byte id)
        {
            var pedido = await _context.Pedidos
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pedido == null)
            {
                return NotFound();
            }

            // Obtener los detalles del pedido usando el IdPedido
            var detallesPedido = await _context.DetallePedidos
                .Where(d => d.IdPedido == id)
                .ToListAsync();

            // Pasar los detalles a la vista
            ViewData["DetallesPedido"] = detallesPedido;

            return View(pedido);
        }


        // POST: Pedidoes/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,FechaPedido,EstadoPedido,Total,MetodoPago,IdMesa")] Pedido pedido)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        pedido.FechaPedido = DateOnly.FromDateTime(DateTime.Now);
        //        pedido.EstadoPedido = "EN PROCESO";
        //        pedido.Total = 0;
        //        _context.Add(pedido);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["IdMesa"] = new SelectList(_context.Mesas, "Id", "NombreMesa", pedido.IdMesa);
        //    return View(pedido);
        //}

        // GET: Pedidoes/Edit/5
        public async Task<IActionResult> Edit(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido == null)
            {
                return NotFound();
            }
            ViewData["IdMesa"] = new SelectList(_context.Mesas, "Id", "Id", pedido.IdMesa);
            return View(pedido);
        }

        // POST: Pedidoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(byte id, [Bind("Id,FechaPedido,EstadoPedido,Total,MetodoPago,IdMesa")] Pedido pedido)
        {
            if (id != pedido.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pedido);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PedidoExists(pedido.Id))
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
            ViewData["IdMesa"] = new SelectList(_context.Mesas, "Id", "Id", pedido.IdMesa);
            return View(pedido);
        }

        // GET: Pedidoes/Delete/5
        public async Task<IActionResult> Delete(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedidos
                .Include(p => p.IdMesaNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        // POST: Pedidoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(byte id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido != null)
            {
                _context.Pedidos.Remove(pedido);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PedidoExists(byte id)
        {
            return _context.Pedidos.Any(e => e.Id == id);
        }
    }
}
