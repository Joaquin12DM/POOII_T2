using DataLayer.Contexto;
using EntityLayer.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public class InventarioRepository
    {

        private readonly Inventario2025Context _context;

        public InventarioRepository(Inventario2025Context context)
        {
            _context = context;
        }

        public List<TbProducto> GetProducts() 
        {
            return _context.TbProductos
            .FromSqlRaw("EXEC usp_ListarProductos")
            .ToList();
        }

        public List<TbCategoria> GetCategorias()
        {
            return _context.TbCategorias
            .FromSqlRaw("EXEC usp_ListarCategorias")
            .ToList();
        }

        public void Create(TbProducto producto)
        {
            _context.Database.ExecuteSqlInterpolated($@"
                EXEC usp_AgregarProducto 
                @nombre = {producto.Nombre}, 
                @descripcion = {producto.Descripcion}, 
                @precio_unitario = {producto.PrecioUnitario}, 
                @stock = {producto.Stock}, 
                @idcategoria = {producto.Idcategoria }");
        }

        public void Edit(TbProducto producto)
        {
            _context.Database.ExecuteSqlInterpolated($@"
                EXEC usp_ActualizarProducto 
                @idproducto = {producto.Idproducto}, 
                @nombre = {producto.Nombre}, 
                @descripcion = {producto.Descripcion}, 
                @precio_unitario = {producto.PrecioUnitario}, 
                @stock = {producto.Stock}, 
                @idcategoria = {producto.Idcategoria}");
        }

        public TbProducto GetProductById(int Id)
        {
            var IdP = new SqlParameter("@idproducto", Id);
            return _context.TbProductos
                .FromSqlInterpolated($@"
            EXEC usp_ObtenerProductoPorID @idproducto = {IdP}")
                .AsEnumerable()
                .FirstOrDefault();

        }

        public void delete(int id)
        {
            var producto = GetProductById(id);

            if (producto != null)
            {
                producto.Estado = false; 
                _context.TbProductos.Update(producto);
                _context.SaveChanges();
            }
        }





    }
}