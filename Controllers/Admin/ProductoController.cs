using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tecnostore.com.Data;
using Tecnostore.com.ViewModel;
using Tecnostore.com.Models;
using Microsoft.AspNetCore.Http;

namespace Tecnostore.com.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    public class ProductoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductoController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> AddProducto()
        {
            var viewModel = new AddProductViewModel
            {
                Categorias = await _context.Categorias.ToListAsync()
            };
            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> AddProducto(AddProductViewModel viewModel)
        {
            try
            {
                var existingImagesCount = _context.Galerias.Count(g => g.IdProducto == viewModel.Producto.Id);
                if (existingImagesCount + viewModel.ImagenFiles.Count() > 3)
                {
                    ModelState.AddModelError(string.Empty, "No se pueden agregar más de 3 imágenes para un producto.");
                    viewModel.Categorias = await _context.Categorias.ToListAsync();
                    return View(viewModel);
                }
                viewModel.Producto.Id = Guid.NewGuid();
                
                // Crea una carpeta con el ID del producto dentro de la carpeta "productos"
                var productFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "productos", viewModel.Producto.Id.ToString());
                if (!Directory.Exists(productFolder))
                {
                    Directory.CreateDirectory(productFolder);
                }

                // Guarda las imágenes en la carpeta del producto
                foreach (var imagenFile in viewModel.ImagenFiles)
                {
                    if (imagenFile.Length > 0)
                    {
                        // Genera un nombre único para la imagen
                        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imagenFile.FileName);

                        // Ruta completa del archivo en la carpeta del producto
                        var filePath = Path.Combine(productFolder, fileName);

                        // Guarda el archivo en la ruta especificada
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await imagenFile.CopyToAsync(fileStream);
                        }

                        // Guarda el nombre de la imagen en la tabla Galeria
                        _context.Galerias.Add(new Gallery
                        {
                            IdProducto = viewModel.Producto.Id,
                            Imagen = fileName
                        });
                    }
                }

                // Resto del código...
                viewModel.Producto.FechaCreacion = DateTime.Now;
                viewModel.Producto.LastUpdate = DateTime.Now;
                _context.Productos.Add(viewModel.Producto);
                await _context.SaveChangesAsync();

                return RedirectToAction("Productos", "Admin");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Ocurrió un error al agregar el producto.");
                viewModel.Categorias = await _context.Categorias.ToListAsync();
                return View(viewModel);
            }
        }
        [HttpPost]
        public async Task<IActionResult> RemoveProducto(Guid id)
        {
            try
            {
                var producto = await _context.Productos.FindAsync(id);
        
                if (producto == null)
                {
                    return NotFound();
                }
        
                // Elimina todas las imágenes del sistema de archivos
                var productFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "productos", producto.Id.ToString());
                if (Directory.Exists(productFolder))
                {
                    var imageFiles = Directory.GetFiles(productFolder);
                    foreach (var filePath in imageFiles)
                    {
                        System.IO.File.Delete(filePath);
                    }
        
                    // Elimina la carpeta del producto
                    Directory.Delete(productFolder);
                }
        
                // Elimina el producto de la base de datos
                _context.Productos.Remove(producto);
                await _context.SaveChangesAsync();
        
                return RedirectToAction("Productos", "Admin");
            }
            catch (Exception ex)
            {
                // Maneja la excepción
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return RedirectToAction("Productos", "Admin");
            }
        }
        
    }
}