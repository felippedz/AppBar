using System;
using System.Collections.Generic;

namespace AppBar3.Models;

public partial class Inventario
{
    public byte Id { get; set; }

    public byte? IdProducto { get; set; }

    public string? TipoMovimiento { get; set; }

    public double? Cantidad { get; set; }

    public DateTime? FechaMovimiento { get; set; }

    public byte? UsuarioId { get; set; }

    public virtual Producto? IdProductoNavigation { get; set; }

    public virtual Usuario? Usuario { get; set; }
}
