using System;
using System.Collections.Generic;

namespace AppBar3.Models;

public partial class VistaProductosVendido
{
    public byte IdProducto { get; set; }

    public string? NombreProducto { get; set; }

    public int? CantidadVendida { get; set; }

    public decimal? CostoVenta { get; set; }

    public decimal? PrecioVenta { get; set; }

    public decimal? Ganancia { get; set; }

    public string? Sede { get; set; }
}
