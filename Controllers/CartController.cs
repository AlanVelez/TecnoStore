using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tecnostore.com.Data;
using Tecnostore.com.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using Tecnostore.com.Extensions;
using Microsoft.Extensions.Logging;


namespace Tecnostore.com.Controllers
{
    public class CartController:Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CartController> _logger;

        public CartController(ApplicationDbContext context, ILogger<CartController> logger)
        {
            _context = context;
            _logger = logger;
        }
        public IActionResult Cart(){
            var productosEnCarrito = HttpContext.Session.Get<List<Articulo>>("cart") ?? new List<Articulo>();
            return View(productosEnCarrito);
        }
        public IActionResult AddToCart(Guid id)
        {
            //Verifica si existe el producto
            // var producto = _context.Productos.Include(p => ).Find(id);
            var producto = _context.Productos
            .Where(p => p.Id == id)
            .Include(p => p.Categoria)
            .Include(p => p.Galerias.Take(1))
            .FirstOrDefault();

            //Si no, manda un error NotFound
            if(producto == null)
            {
                return NotFound();
            }
            
            var articulo = new Articulo
            {
                IdProducto = producto.Id,
                Nombre = producto.Titulo,
                Precio = producto.Precio,
                Imagen = producto.Galerias.First().Imagen,
                Cantidad = 1
            };
            _logger.LogInformation("Articulo: ----------------------");
            _logger.LogInformation(articulo.ToString());

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
            //Agrega el producto a la lista
            //Si el producto ya existe en la lista, aumenta la cantidad
            if(productos.Any(p => p.IdProducto == id))
            {
                var articuloExistente = productos.FirstOrDefault(p => p.IdProducto == id);
                articuloExistente.Cantidad += 1;
            }
            else{
                productos.Add(articulo);
            }
            //Guarda la lista en la sesion
            HttpContext.Session.Set("cart", productos);
            //Redirecciona a la vista Cart
            return RedirectToAction("Cart");
        }    
        public IActionResult DelToCart(Guid id)
        {
            //Verifica si existe el producto
            var producto = _context.Productos.Find(id);
            //Si no, manda un error NotFound
            if(producto == null)
            {
                return NotFound();
            }
            //Si existe el producto, obtiene la lista de productos
            var productos = HttpContext.Session.Get<List<Articulo>>("cart");
            //Si el producto existe en la lista, lo elimina
            if(productos.Any(p => p.IdProducto == id))
            {
                if(productos.Any(p => p.Cantidad == 1))
                {
                    var articuloExistente = productos.FirstOrDefault(p => p.IdProducto == id);
                    productos.Remove(articuloExistente);
                }
                else
                {
                    var articuloExistente = productos.FirstOrDefault(p => p.IdProducto == id);
                    articuloExistente.Cantidad -= 1;
                }
            }
            //Guarda la lista en la sesion
            HttpContext.Session.Set("cart", productos);
            //Redirecciona a la vista Cart
            return RedirectToAction("Cart");
        }
        public IActionResult RemoveFromCart(Guid id)
        {
            //Verifica si existe el producto
            var producto = _context.Productos.Find(id);
            //Si no, manda un error NotFound
            if(producto == null)
            {
                return NotFound();
            }
            //Si existe el producto, obtiene la lista de productos
            var productos = HttpContext.Session.Get<List<Articulo>>("cart");
            //Si el producto existe en la lista, lo elimina
            if(productos.Any(p => p.IdProducto == id))
            {
                var articuloExistente = productos.FirstOrDefault(p => p.IdProducto == id);
                productos.Remove(articuloExistente);
            }
            //Guarda la lista en la sesion
            HttpContext.Session.Set("cart", productos);
            //Redirecciona a la vista Cart
            return RedirectToAction("Cart");
        }
    }
}