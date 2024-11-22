using System;
using System.Collections.Generic;

namespace AppBar3.Models;

public partial class Documento
{
    public byte Id { get; set; }

    public string? TipoDocumento { get; set; }

    public bool? Estado { get; set; }

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
