using DomainLayer;
using EntityLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace POOII_T2_DELGADO_JOAQUIN.Controllers
{
    public class InventarioController : Controller
    {
        private readonly InventarioService _inventarioService;

        public InventarioController(InventarioService inventarioService)
        {
            _inventarioService = inventarioService;
        }
        public void DropDownList()
        {
            var categorias = _inventarioService.CategoriaDropDownList();
            ViewBag.Categorias = new SelectList(categorias, "Idcategoria", "NombreCategoria");
        }

        public IActionResult IndexProducto()
        {
            var list = _inventarioService.GetProducts();
            return View(list);
        }

        public IActionResult NuevoProducto()
        {
            DropDownList();
            return View();
        }

        [HttpPost]
        public IActionResult NuevoProducto(TbProducto producto)
        {
            if(producto == null)
            {
                return View(producto);
            }

            try
            {
                _inventarioService.Create(producto);
                ViewBag.mensaje1 = "Producto Registrado con éxito.";
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.mensaje2 = "Error al registrar el producto";
                return View(producto);
            }

        }

        public IActionResult Edit(int Id)
        {
            TbProducto producto = new TbProducto();
            producto = _inventarioService.GetPorId(Id);
            DropDownList();
            return View(producto);

        }

        [HttpPost]
        public IActionResult Edit(TbProducto producto)
        {
            _inventarioService.Edit(producto);
            ViewBag.mensaje = "Producto Actualizado correctamente";
            DropDownList();
            return View(producto);
        }

        public IActionResult Eliminar(int Id)
        {
            TbProducto producto = new TbProducto();
            producto = _inventarioService.GetPorId(Id);
            DropDownList();
            return View(producto);

        }

        [HttpPost]
        public IActionResult DeletePro(TbProducto producto)
        {
            _inventarioService.delete(producto.Idproducto);
            ViewBag.mensaje = "Producto Eliminado correctamente";
            return View("Eliminar");
           
        }


        
        
    }
}
