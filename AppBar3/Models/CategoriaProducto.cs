using System;
using System.Collections.Generic;

namespace AppBar3.Models;

public partial class CategoriaProducto
{
    public byte Id { get; set; }

    public string? NombreCategoria { get; set; }

    public bool? Estado { get; set; }

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
