using System;
using System.Collections.Generic;

namespace AppBar3.Models;

public partial class DetallePedido
{
    public byte Id { get; set; }

    public bool? Estado { get; set; }

    public byte? Cantidad { get; set; }

    public decimal? PrecioUnitario { get; set; }

    public decimal? Subtotal { get; set; }

    public decimal? Total { get; set; }

    public byte? IdPedido { get; set; }

    public byte? IdProducto { get; set; }

    public byte? IdSede { get; set; }

    public virtual Pedido? IdPedidoNavigation { get; set; }

    public virtual Producto? IdProductoNavigation { get; set; }

    public virtual Sede? IdSedeNavigation { get; set; }

    // Lógica adicional para cálculo del subtotal y total
    public void Calcular()
    {
        Subtotal = Cantidad * PrecioUnitario;
        Total = Subtotal * 1.21m; // Aplicar IVA del 21%
    }
}
