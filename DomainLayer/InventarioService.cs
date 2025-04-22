using DataLayer;
using DomainLayer.DTO;
using EntityLayer.Models;

namespace DomainLayer
{
    public class InventarioService
    {
        private readonly InventarioRepository inventarioRepository;

        public InventarioService(InventarioRepository inventarioRepository)
        {
            this.inventarioRepository = inventarioRepository;
        }

        public List<ProductoCategoriaDTO> GetProducts() 
        {
            var listPro = inventarioRepository.GetProducts();    
            var listCat = inventarioRepository.GetCategorias();   

            var list = listPro.Select(p => new ProductoCategoriaDTO
            {
                Idproducto = p.Idproducto,
                Nombre = p.Nombre,
                Descripcion = p.Descripcion,
                PrecioUnitario = p.PrecioUnitario,
                Stock = p.Stock,
                Idcategoria = p.Idcategoria,
                NombreCategoria = listCat.FirstOrDefault(c => c.Idcategoria == p.Idcategoria)?.NombreCategoria!,
                FechaRegistro = p.FechaRegistro,
                Estado = p.Estado
            }).ToList();

            return list;
        }

        public List<TbCategoria> CategoriaDropDownList()
        {
            return inventarioRepository.GetCategorias();
        }

        public void Create(TbProducto producto)
        {
            inventarioRepository.Create(producto);
        }

        public void Edit(TbProducto producto)
        {
            inventarioRepository.Edit(producto);
        }


        public TbProducto GetPorId(int Id)
        {
            return inventarioRepository.GetProductById(Id);
        }
        public void delete(int Id)
        {
            inventarioRepository.delete(Id);
        }






    }
}