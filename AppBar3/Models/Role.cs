using System;
using System.Collections.Generic;

namespace AppBar3.Models;

public partial class Role
{
    public byte Id { get; set; }

    public string? NombreRol { get; set; }

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
