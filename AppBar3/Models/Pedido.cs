using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppBar3.Models;

public partial class Pedido
{
    public byte Id { get; set; }

    public DateOnly? FechaPedido { get; set; }

    public string? EstadoPedido { get; set; }

    [DisplayFormat(DataFormatString = "{0:F0}", ApplyFormatInEditMode = true)]
    public decimal? Total { get; set; }

    public string? MetodoPago { get; set; }

    public byte? IdMesa { get; set; }

    public virtual ICollection<DetallePedido> DetallePedidos { get; set; } = new List<DetallePedido>();

    public virtual Mesa? IdMesaNavigation { get; set; }
}
