using System;
using System.Collections.Generic;

namespace EntityLayer.Models;

public partial class TbCategoria
{
    public int Idcategoria { get; set; }

    public string NombreCategoria { get; set; } = null!;

    public string? Descripcion { get; set; }

    public bool? Estado { get; set; }

    public virtual ICollection<TbProducto> TbProductos { get; set; } = new List<TbProducto>();
}
