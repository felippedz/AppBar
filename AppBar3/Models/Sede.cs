using System;
using System.Collections.Generic;

namespace AppBar3.Models;

public partial class Sede
{
    public byte Id { get; set; }

    public string? NombreSede { get; set; }

    public string? Direccion { get; set; }

    public string? Correo { get; set; }

    public bool? Estado { get; set; }

    public virtual ICollection<DetallePedido> DetallePedidos { get; set; } = new List<DetallePedido>();

    public virtual ICollection<Mesa> Mesas { get; set; } = new List<Mesa>();

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
