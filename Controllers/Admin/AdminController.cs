using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tecnostore.com.Data;
using Tecnostore.com.Models;
using PagedList;
using Tecnostore.com.Helpers;

namespace Tecnostore.com.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        // public async Task<IActionResult> Categorias()
        // {
        //    return View(await _context.Categorias.ToListAsync());
        // }
        public async Task<IActionResult> Categorias(int? pagina, int tamanoPagina = 3)
        {
            int numeroPagina = pagina ?? 1;

            var categoriasPaginadas = await _context.Categorias
                .OrderBy(c => c.Id)
                .Skip((numeroPagina - 1) * tamanoPagina)
                .Take(tamanoPagina)
                .ToListAsync();

            int totalCategorias = await _context.Categorias.CountAsync();

            var model = new Tecnostore.com.Helpers.PagedList<Categoria>(categoriasPaginadas, totalCategorias, numeroPagina, tamanoPagina);


            return View(model);
        }
        public IActionResult Ordenes()
        {
            return View();
        }
        // public async Task<IActionResult> Productos(int? pagina, int tamanoPagina = 3)
        // {
        //     int numeroPagina = pagina ?? 1;
        //     // Realiza una operación de unión entre Productos y Categorias para obtener el nombre de la categoría
        //     // También incluye la primera imagen asociada a cada producto
        //     var productosConCategoriasEImagen = await _context.Productos
        //         .Include(p => p.Categoria)  // Incluye la relación de navegación a la tabla de Categorias
        //         .Include(p => p.Galerias)   // Incluye la relación de navegación a la tabla de Galerias
        //         .Select(p => new
        //         {
        //             Producto = p,
        //             PrimeraImagen = p.Galerias
        //                 .OrderBy(g => g.FechaDeSubida)  // Ordena por fecha de subida para obtener la primera imagen
        //                 .FirstOrDefault()
        //         })
        //         .ToListAsync();

        //     // Proyecta los resultados en una lista de Productos con la primera imagen asociada
        //     var productosConCategoriasEPrimeraImagen = productosConCategoriasEImagen.Select(p => new Producto
        //     {
        //         Id = p.Producto.Id,
        //         Titulo = p.Producto.Titulo,
        //         Descripcion = p.Producto.Descripcion,
        //         Categoria = p.Producto.Categoria,
        //         // Verifica si hay una primera imagen y, si es así, la agrega a la lista de imágenes
        //         Galerias = p.PrimeraImagen != null ? new List<Gallery> { p.PrimeraImagen } : new List<Gallery>(),
        //         Precio = p.Producto.Precio,  // Asegúrate de incluir la propiedad Precio
        //         FechaCreacion = p.Producto.FechaCreacion

        //     }).ToList();

        //     return View(productosConCategoriasEPrimeraImagen);
        // }
        public async Task<IActionResult> Productos(int? pagina, int tamanoPagina = 3)
        {
            int numeroPagina = pagina ?? 1;

            var productosConCategoriasEImagen = await _context.Productos
                .Include(p => p.Categoria)
                .Include(p => p.Galerias)
                .Select(p => new
                {
                    Producto = p,
                    PrimeraImagen = p.Galerias
                        .OrderBy(g => g.FechaDeSubida)
                        .FirstOrDefault()
                })
                .ToListAsync();

            var productosConCategoriasEPrimeraImagen = productosConCategoriasEImagen.Select(p => new Producto
            {
                Id = p.Producto.Id,
                Titulo = p.Producto.Titulo,
                Descripcion = p.Producto.Descripcion,
                Categoria = p.Producto.Categoria,
                Galerias = p.PrimeraImagen != null ? new List<Gallery> { p.PrimeraImagen } : new List<Gallery>(),
                Precio = p.Producto.Precio,
                FechaCreacion = p.Producto.FechaCreacion
            }).ToList();

            // Paginación
            var productosPaginados = productosConCategoriasEPrimeraImagen
                .Skip((numeroPagina - 1) * tamanoPagina)
                .Take(tamanoPagina)
                .ToList();

            var totalProductos = productosConCategoriasEPrimeraImagen.Count;

            var model = new Tecnostore.com.Helpers.PagedList<Producto>(productosPaginados, totalProductos, numeroPagina, tamanoPagina);

            return View(model);
        }

        public IActionResult Reviews()
        {
            return View();
        }
        public IActionResult Usuarios()
        {
            return View();
        }
    }
}