﻿using System;
using System.Collections.Generic;

namespace AppBar3.Models;

public partial class Mesa
{
    public byte Id { get; set; }

    public string? NombreMesa { get; set; }

    public byte? IdSede { get; set; }

    public bool? Estado { get; set; }

    public virtual Sede? IdSedeNavigation { get; set; }

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
}
