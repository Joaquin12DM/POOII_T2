using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DTO
{
    public class ProductoCategoriaDTO
    {
        public int Idproducto { get; set; }
        public string Nombre { get; set; }
        public string? Descripcion { get; set; }
        public decimal PrecioUnitario { get; set; }
        public int Stock { get; set; }
        public int? Idcategoria { get; set; }
        public string NombreCategoria { get; set; } = null!;
        public DateTime? FechaRegistro { get; set; }
        public bool? Estado { get; set; }
    }
}
