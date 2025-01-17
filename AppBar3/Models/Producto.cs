﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace AppBar3.Models;

public partial class Producto
{
    public byte Id { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }
    [DisplayFormat(DataFormatString = "{0:F0}", ApplyFormatInEditMode = true)]
    public decimal? Precio { get; set; }
    [DisplayFormat(DataFormatString = "{0:F0}", ApplyFormatInEditMode = true)]
    public decimal? PrecioVenta { get; set; }

    public bool? Estado { get; set; }

    public byte? IdCategoria { get; set; }

    public byte? IdEnvase { get; set; }

    public byte? IdMarca { get; set; }

    public virtual ICollection<DetallePedido> DetallePedidos { get; set; } = new List<DetallePedido>();

    public virtual CategoriaProducto? IdCategoriaNavigation { get; set; }

    public virtual EnvaseProducto? IdEnvaseNavigation { get; set; }

    public virtual MarcaProducto? IdMarcaNavigation { get; set; }

    public virtual ICollection<Inventario> Inventarios { get; set; } = new List<Inventario>();

}
