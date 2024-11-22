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
    public class DetallePedidoesController : Controller
    {
        private readonly AppBarContext _context;

        public DetallePedidoesController(AppBarContext context)
        {
            _context = context;
        }

        // GET: DetallePedidoes
        public async Task<IActionResult> Index()
        {
            var appBarContext = _context.DetallePedidos.Include(d => d.IdPedidoNavigation).Include(d => d.IdProductoNavigation).Include(d => d.IdSedeNavigation);
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


        [HttpGet]
        public JsonResult ObtenerPreciosProductos()
        {
            var productos = _context.Productos
                                    .Select(p => new
                                    {
                                        p.Id,
                                        p.Nombre,
                                        p.PrecioVenta // Asegúrate de que 'PrecioVenta' esté en tu modelo
                                    })
                                    .ToList();

            return Json(productos);
        }
        // GET: DetallePedidoes/Details/5
        public async Task<IActionResult> Details(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detallePedido = await _context.DetallePedidos
                .Include(d => d.IdPedidoNavigation)
                .Include(d => d.IdProductoNavigation)
                .Include(d => d.IdSedeNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (detallePedido == null)
            {
                return NotFound();
            }

            return View(detallePedido);
        }

        // GET: DetallePedidoes/Create
        //public IActionResult Create()
        //{
        //    ViewData["IdPedido"] = new SelectList(_context.Pedidos, "Id", "Id");
        //    ViewData["IdProducto"] = new SelectList(_context.Productos, "Id", "Id");
        //    ViewData["IdSede"] = new SelectList(_context.Sedes, "Id", "Id");
        //    return View();
        //}

        // POST: DetallePedidoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,Estado,Cantidad,PrecioUnitario,Subtotal,Total,IdPedido,IdProducto,IdSede")] DetallePedido detallePedido)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(detallePedido);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["IdPedido"] = new SelectList(_context.Pedidos, "Id", "Id", detallePedido.IdPedido);
        //    ViewData["IdProducto"] = new SelectList(_context.Productos, "Id", "Id", detallePedido.IdProducto);
        //    ViewData["IdSede"] = new SelectList(_context.Sedes, "Id", "Id", detallePedido.IdSede);
        //    return View(detallePedido);
        //}

        public IActionResult Create(byte? id)
        {
            // Verificamos si el ID del pedido es válido
            if (id == null)
            {
                return NotFound();
            }

            // Obtener el pedido específico, incluyendo la mesa (sede)
            var pedido = _context.Pedidos
                                 .Include(p => p.IdMesaNavigation) // Incluir la mesa (sede)
                                 .FirstOrDefault(p => p.Id == id);

            if (pedido == null)
            {
                return NotFound();
            }

            // Obtener los productos disponibles para el DropDownList
            var productos = _context.Productos
                                    .Select(p => new { p.Id, p.Nombre })
                                    .ToList();

            // Crear un SelectList para los productos
            ViewData["Productos"] = new SelectList(productos, "Id", "Nombre");

            // Crear un SelectList para los pedidos (si es necesario mostrar los otros pedidos)
            var pedidos = _context.Pedidos
                                  .Select(p => new { p.Id, DisplayName = $"{p.Id} - Mesa {p.IdMesaNavigation.NombreMesa}" })
                                  .ToList();

            ViewData["Pedidos"] = new SelectList(pedidos, "Id", "DisplayName");

            // Asegurarnos de pasar el IdSede del Pedido actual al nuevo DetallePedido
            var detallePedido = new DetallePedido
            {
                IdPedido = id,
                IdSede = pedido.IdMesaNavigation.Id // Asignar la mesa del pedido al detalle
            };

            // Pasamos el nombre de la mesa como una propiedad adicional que se mostrará solo de lectura
            ViewData["Mesa"] = pedido.IdMesaNavigation.NombreMesa;

            return View(detallePedido);
        }



        // POST: DetallePedidoes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Estado,Cantidad,PrecioUnitario,Subtotal,Total,IdPedido,IdProducto,IdSede")] DetallePedido detallePedido)
        {
            if (ModelState.IsValid)
            {
                // Verificar que IdProducto e IdPedido no sean null
                if (detallePedido.IdProducto == null || detallePedido.IdPedido == null)
                {
                    ModelState.AddModelError("", "Producto o Pedido no seleccionados.");
                    return View(detallePedido); // Si hay algún valor faltante, volvemos a mostrar el formulario
                }

                // Verificar que el producto existe en la base de datos
                var producto = await _context.Productos.FindAsync(detallePedido.IdProducto);
                if (producto == null)
                {
                    ModelState.AddModelError("IdProducto", "Producto no encontrado.");
                    return View(detallePedido); // Si no se encuentra el producto, volvemos a mostrar el formulario
                }

                // Verificar que el pedido existe en la base de datos
                var pedido = await _context.Pedidos.FindAsync(detallePedido.IdPedido);
                if (pedido == null)
                {
                    ModelState.AddModelError("IdPedido", "Pedido no encontrado.");
                    return View(detallePedido); // Si no se encuentra el pedido, volvemos a mostrar el formulario
                }

                // Realizar los cálculos del precio unitario, subtotal y total
                detallePedido.PrecioUnitario = producto.PrecioVenta; // Suponiendo que tienes PrecioVenta en Producto
                detallePedido.Calcular(); // Calcula el subtotal y el total (incluyendo IVA)

                // Añadir el detalle del pedido sin modificar inventario
                _context.Add(detallePedido);
                await _context.SaveChangesAsync();

                // Redirigimos a la vista de detalles del pedido o cualquier otra vista que desees
                return RedirectToAction("Details", "Pedidoes", new { id = detallePedido.IdPedido });
            }

            // Si ocurre un error, volvemos a cargar los productos
            ViewData["Productos"] = new SelectList(_context.Productos, "Id", "Nombre", detallePedido.IdProducto);
            return View(detallePedido);
        }

        // GET: DetallePedidoes/Edit/5
        public async Task<IActionResult> Edit(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detallePedido = await _context.DetallePedidos.FindAsync(id);
            if (detallePedido == null)
            {
                return NotFound();
            }
            ViewData["IdPedido"] = new SelectList(_context.Pedidos, "Id", "Id", detallePedido.IdPedido);
            ViewData["IdProducto"] = new SelectList(_context.Productos, "Id", "Id", detallePedido.IdProducto);
            ViewData["IdSede"] = new SelectList(_context.Sedes, "Id", "Id", detallePedido.IdSede);
            return View(detallePedido);
        }

        // POST: DetallePedidoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(byte id, [Bind("Id,Estado,Cantidad,PrecioUnitario,Subtotal,Total,IdPedido,IdProducto,IdSede")] DetallePedido detallePedido)
        {
            if (id != detallePedido.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(detallePedido);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetallePedidoExists(detallePedido.Id))
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
            ViewData["IdPedido"] = new SelectList(_context.Pedidos, "Id", "Id", detallePedido.IdPedido);
            ViewData["IdProducto"] = new SelectList(_context.Productos, "Id", "Id", detallePedido.IdProducto);
            ViewData["IdSede"] = new SelectList(_context.Sedes, "Id", "Id", detallePedido.IdSede);
            return View(detallePedido);
        }

        // GET: DetallePedidoes/Delete/5
        public async Task<IActionResult> Delete(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detallePedido = await _context.DetallePedidos
                .Include(d => d.IdPedidoNavigation)
                .Include(d => d.IdProductoNavigation)
                .Include(d => d.IdSedeNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (detallePedido == null)
            {
                return NotFound();
            }

            return View(detallePedido);
        }

        // POST: DetallePedidoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(byte id)
        {
            var detallePedido = await _context.DetallePedidos.FindAsync(id);
            if (detallePedido != null)
            {
                _context.DetallePedidos.Remove(detallePedido);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DetallePedidoExists(byte id)
        {
            return _context.DetallePedidos.Any(e => e.Id == id);
        }
    }
}
