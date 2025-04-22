using System;
using System.Collections.Generic;

namespace EntityLayer.Models;

public partial class TbProducto
{
    public int Idproducto { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public decimal PrecioUnitario { get; set; }

    public int Stock { get; set; }

    public int? Idcategoria { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public bool? Estado { get; set; }

    public virtual TbCategoria? IdcategoriaNavigation { get; set; }
}
