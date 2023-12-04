using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Tecnostore.com.Models;
using Microsoft.EntityFrameworkCore;
using Tecnostore.com.Data;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using Tecnostore.com.ViewModel;
using Microsoft.Extensions.Logging;
using Tecnostore.com.Extensions;
using PagedList;
using Tecnostore.com.Helpers;


namespace Tecnostore.com.Controllers
{
    //[Authorize(Roles = "Admin")]

    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ApplicationDbContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var viewModel = new ViewIndexViewModel
            {
                Categorias = await _context.Categorias.ToListAsync(),
                Productos = await _context.Productos
                .Include(p => p.Categoria)
                .Include(p => p.Galerias.Take(1))
                .OrderByDescending(p => p.OpinionMedia) // Asumiendo que tienes una propiedad OpinionMedia en tu modelo Producto
                .Take(8)
                .ToListAsync()
            };
            return View(viewModel);
        }
        // public IActionResult ViewCategoria(Guid? id, int? pagina, int tamanoPagina = 8, string precioMinFiltro = null, string precioMaxFiltro = null, string marcaFiltro = null, string searchFiltro = null)
        // {
        //     var productosQuery = _context.Productos
        //     .Include(p => p.Categoria)
        //     .Include(p => p.Galerias.Take(1)) as IQueryable<Tecnostore.com.Models.Producto>;
            
        //     List<string> marcas = _context.Productos
        //         .Select(p => p.Marca)
        //         .Distinct()
        //         .ToList();

        //     if(id != null)
        //     {
        //         productosQuery = productosQuery.Where(p => p.IdCategoria == id);

        //     }
        //     else if(!string.IsNullOrEmpty(searchFiltro)){
        //            productosQuery = productosQuery.Where(p => p.Titulo.Contains(searchFiltro) || p.Descripcion.Contains(searchFiltro));
        //     }
        //     // Aplicar filtros si se proporcionan
        //     if (!string.IsNullOrEmpty(precioMinFiltro) && decimal.TryParse(precioMinFiltro, out decimal precioMin))
        //     {
        //         productosQuery = productosQuery.Where(p => p.Precio >= precioMin);
        //     }

        //     if (!string.IsNullOrEmpty(precioMaxFiltro) && decimal.TryParse(precioMaxFiltro, out decimal precioMax))
        //     {
        //         productosQuery = productosQuery.Where(p => p.Precio <= precioMax);
        //     }
        //     if (!string.IsNullOrEmpty(marcaFiltro))
        //     {
        //         productosQuery = productosQuery.Where(p => p.Marca.Contains(marcaFiltro));
        //     }
        //     // Resto de tu lógica para paginación y creación del modelo PagedList
        //     ViewBag.Marcas = marcas;
        //     int numeroPagina = pagina ?? 1;
            
        //     var productosPaginados = productosQuery
        //         .OrderBy(p => p.Id) // Ajusta según tus necesidades
        //         .Skip((numeroPagina - 1) * tamanoPagina)
        //         .Take(tamanoPagina)
        //         .ToList();

        //     var totalProductos = productosQuery.Count();

        //     var model = new Tecnostore.com.Helpers.PagedList<Producto>(productosPaginados, totalProductos, numeroPagina, tamanoPagina);

        //     return View(model);
        // }
        public IActionResult ViewCategoria(Guid? id, int? pagina, int tamanoPagina = 8, string precioMinFiltro = null, string precioMaxFiltro = null, string marcaFiltro = null, string searchFiltro = null)
        {
            var productosQuery = _context.Productos
                .Include(p => p.Categoria)
                .Include(p => p.Galerias.Take(1)) as IQueryable<Tecnostore.com.Models.Producto>;
        
            if (id != null)
            {
                productosQuery = productosQuery.Where(p => p.IdCategoria == id);
            }
        
            // Filtro de búsqueda
            if (!string.IsNullOrEmpty(searchFiltro))
            {
                productosQuery = productosQuery.Where(p => p.Titulo.Contains(searchFiltro) || p.Descripcion.Contains(searchFiltro));
            }
        
            // Filtros adicionales
            if (!string.IsNullOrEmpty(precioMinFiltro) && decimal.TryParse(precioMinFiltro, out decimal precioMin))
            {
                productosQuery = productosQuery.Where(p => p.Precio >= precioMin);
            }
        
            if (!string.IsNullOrEmpty(precioMaxFiltro) && decimal.TryParse(precioMaxFiltro, out decimal precioMax))
            {
                productosQuery = productosQuery.Where(p => p.Precio <= precioMax);
            }
        
            if (!string.IsNullOrEmpty(marcaFiltro))
            {
                productosQuery = productosQuery.Where(p => p.Marca.Contains(marcaFiltro));
            }
        
            // Resto de tu lógica para paginación y creación del modelo PagedList
            int numeroPagina = pagina ?? 1;
        
            var productosPaginados = productosQuery
                .OrderBy(p => p.Id) // Ajusta según tus necesidades
                .Skip((numeroPagina - 1) * tamanoPagina)
                .Take(tamanoPagina)
                .ToList();
        
            var totalProductos = productosQuery.Count();
        
            var model = new Tecnostore.com.Helpers.PagedList<Producto>(productosPaginados, totalProductos, numeroPagina, tamanoPagina);
        
            return View(model);
        }

        public IActionResult ViewProducto(Guid id)
        {
            var producto = _context.Productos
            .Where(p => p.Id == id)
            .Include(p => p.Categoria)
            .Include(p => p.Galerias)
            .FirstOrDefault();
            
            if(producto == null)
            {
                return NotFound();
            }
            var viewModel = new ViewProductViewModel
            {
                Producto = producto,
            };
            return View(viewModel);
        }
        [HttpPost]
        public IActionResult ViewProducto(ViewProductViewModel viewModel){
            //Crear un objeto articulo
            var articulo = new Articulo
            {
                IdProducto = viewModel.Producto.Id,
                Nombre = viewModel.Producto.Titulo,
                Precio = viewModel.Producto.Precio,
                Imagen = viewModel.Articulo.Imagen,
                Cantidad = viewModel.Articulo.Cantidad
            };
            //Inicializa la lista de productos
            List<Articulo> productos;
            //Si no existe la sesion cart, crea una lista vacia
            if(HttpContext.Session.GetString("cart") == null)
            {
                productos = new List<Articulo>();
            }
            //Si existe la sesion cart, obtiene la lista de productos
            else{
                productos = HttpContext.Session.Get<List<Articulo>>("cart");
            }

            if(productos.Any(p => p.IdProducto == articulo.IdProducto))
            {
                var articuloExistente = productos.FirstOrDefault(p => p.IdProducto == articulo.IdProducto);
                articuloExistente.Cantidad += articulo.Cantidad;
            }
            else{
                productos.Add(articulo);
            }
            //Guarda la lista en la sesion
            HttpContext.Session.Set("cart", productos);
            return RedirectToAction("Cart", "Cart");
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Terminos()
        {
            return View();
        }
        public IActionResult WelcomePartial()
        {
            return PartialView("_WelcomePartial");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}