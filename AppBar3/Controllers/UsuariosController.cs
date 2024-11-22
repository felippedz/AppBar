using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppBar3.Models;
using System.Text;
using System.Security.Cryptography;
using System.Data;
using ClosedXML.Excel;

namespace AppBar3.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly AppBarContext _context;

        public UsuariosController(AppBarContext context)
        {
            _context = context;
        }

        public static string ConvertToSHA256(string input)
        {
            // Crear una instancia de SHA256
            using (SHA256 sha256 = SHA256.Create())
            {
                // Convertir la contraseña en un arreglo de bytes y luego obtener el hash
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));

                // Convertir los bytes del hash a una cadena hexadecimal
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("x2")); // Formato hexadecimal
                }

                // Devuelve la cadena hexadecimal del hash
                return sb.ToString();
            }
        }


        [HttpGet]
        public async Task<FileResult> ExportarUsuario()
        {
            var usuarios = await _context.Usuarios.ToListAsync();
            var NombreArchivo = $"Usuarios.xlsx";
            return Exportar(NombreArchivo, usuarios);
        }

        private FileResult Exportar(string NombreArchivo, IEnumerable<Usuario> usuarios)
        {
            DataTable dataTable = new DataTable("Usuario");
            dataTable.Columns.AddRange(new DataColumn[]
            {

                new DataColumn("Correo"),
                new DataColumn("Nombre"),
                new DataColumn("Apellido"),
                new DataColumn("Documento"),
                new DataColumn("Rol"),
                new DataColumn("Sede")
            });
            foreach (var usuario in usuarios)
            {
                dataTable.Rows.Add(
                    usuario.Correo,
                    usuario.Nombre,
                    usuario.Apellido,
                    usuario.Documento,
                    usuario.IdRol,
                    usuario.IdSede);
            }
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dataTable);

                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", NombreArchivo);
                }
            }
        }

        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            var appBarContext = _context.Usuarios.Include(u => u.IdDocumentoNavigation).Include(u => u.IdRolNavigation).Include(u => u.IdSedeNavigation).OrderBy(u => u.IdRolNavigation.NombreRol);
            return View(await appBarContext.ToListAsync());
        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .Include(u => u.IdDocumentoNavigation)
                .Include(u => u.IdRolNavigation)
                .Include(u => u.IdSedeNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Usuarios/Create
        public IActionResult Create()
        {
            ViewData["IdDocumento"] = new SelectList(_context.Documentos, "Id", "TipoDocumento");
            ViewData["IdRol"] = new SelectList(_context.Roles, "Id", "NombreRol");
            ViewData["IdSede"] = new SelectList(_context.Sedes, "Id", "NombreSede");
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Apellido,Documento,Correo,Password,Estado,IdDocumento,IdSede,IdRol")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                usuario.Estado = true;

                // Generar el correo del usuario
                usuario.Correo = (usuario.Nombre ?? "") + (usuario.Apellido ?? "") +
                                 (usuario.IdSedeNavigation?.NombreSede ?? "") + "@bar.com";

                // Si la contraseña no está vacía, convertirla a SHA-256
                if (!string.IsNullOrEmpty(usuario.Password))
                {
                    // Convertir la contraseña a SHA-256 y guardarla como un arreglo de bytes
                    usuario.Password = ConvertToSHA256(usuario.Password);
                }
                else
                {
                    // Si la contraseña está vacía, podrías lanzar una excepción o devolver un error
                    ModelState.AddModelError("Password", "La contraseña no puede estar vacía.");
                    return View(usuario);
                }

                // Agregar el usuario a la base de datos
                _context.Add(usuario);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            // Si hay errores, enviar los datos a la vista
            ViewData["IdDocumento"] = new SelectList(_context.Documentos, "Id", "TipoDocumento", usuario.IdDocumento);
            ViewData["IdRol"] = new SelectList(_context.Roles, "Id", "NombreRol", usuario.IdRol);
            ViewData["IdSede"] = new SelectList(_context.Sedes, "Id", "NombreSede", usuario.IdSede);

            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            ViewData["IdDocumento"] = new SelectList(_context.Documentos, "Id", "TipoDocumento", usuario.IdDocumento);
            ViewData["IdRol"] = new SelectList(_context.Roles, "Id", "NombreRol", usuario.IdRol);
            ViewData["IdSede"] = new SelectList(_context.Sedes, "Id", "NombreSede", usuario.IdSede);
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(byte id, [Bind("Id,Nombre,Apellido,Documento,Correo,Password,Estado,IdDocumento,IdSede,IdRol")] Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.Id))
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
            ViewData["IdDocumento"] = new SelectList(_context.Documentos, "Id", "Id", usuario.IdDocumento);
            ViewData["IdRol"] = new SelectList(_context.Roles, "Id", "Id", usuario.IdRol);
            ViewData["IdSede"] = new SelectList(_context.Sedes, "Id", "Id", usuario.IdSede);
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .Include(u => u.IdDocumentoNavigation)
                .Include(u => u.IdRolNavigation)
                .Include(u => u.IdSedeNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(byte id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(byte id)
        {
            return _context.Usuarios.Any(e => e.Id == id);
        }
    }
}
