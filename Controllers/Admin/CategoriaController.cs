using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tecnostore.com.Data;
using Tecnostore.com.Models;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace Tecnostore.com.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    public class CategoriaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoriaController(ApplicationDbContext context)
        {
            _context = context;
            
        }

        [HttpGet]
        public IActionResult AddCategoria()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddCategoria(Categoria categoria)
        {
            try
            {
                if (categoria.ImagenFile != null && categoria.ImagenFile.Length > 0)
                {
                    // Genera un nombre único para la imagen
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(categoria.ImagenFile.FileName);

                    // Ruta completa del archivo en wwwroot/img
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "categorias", fileName);

                    // Guarda el archivo en la ruta especificada
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await categoria.ImagenFile.CopyToAsync(fileStream);
                    }

                    // Guarda el nombre de la imagen en la propiedad Imagen
                    categoria.Imagen = fileName;
                }

                categoria.fechaCreacion = DateTime.Now;
                categoria.LastUpdate = DateTime.Now;
                _context.Categorias.Add(categoria);
                await _context.SaveChangesAsync();

                return RedirectToAction("Categorias", "Admin");
            }
            catch (Exception ex)
            {
                // Maneja la excepción
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return View(categoria);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Borrar(Guid id)
        {
            try
            {
                var categoria = await _context.Categorias.FindAsync(id);

                if (categoria == null)
                {
                    return NotFound();
                }

                // Elimina la imagen del sistema de archivos
                if (!string.IsNullOrEmpty(categoria.Imagen))
                {
                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "categorias", categoria.Imagen);
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                }

                // Elimina la entrada de la base de datos
                _context.Categorias.Remove(categoria);
                await _context.SaveChangesAsync();

                return RedirectToAction("Categorias", "Admin");
            }
            catch (Exception ex)
            {
                // Maneja la excepción
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return RedirectToAction("Categorias", "Admin");
            }
        }
    }
}