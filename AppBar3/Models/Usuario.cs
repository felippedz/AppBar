using System;
using System.Collections.Generic;

namespace AppBar3.Models;

public partial class Usuario
{
    public byte Id { get; set; }

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public string? Documento { get; set; }

    public string? Correo { get; set; }

    public string? Password { get; set; }

    public bool? Estado { get; set; }

    public byte? IdDocumento { get; set; }

    public byte? IdSede { get; set; }

    public byte? IdRol { get; set; }

    public virtual Documento? IdDocumentoNavigation { get; set; }

    public virtual Role? IdRolNavigation { get; set; }

    public virtual Sede? IdSedeNavigation { get; set; }

    public virtual ICollection<Inventario> Inventarios { get; set; } = new List<Inventario>();
}
