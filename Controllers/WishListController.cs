using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tecnostore.com.Data;
using Tecnostore.com.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;


namespace Tecnostore.com.Controllers
{
    public class WishListController:Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<WishListController> _logger;
        private readonly UserManager<Usuario> _userManager;


        public WishListController(ApplicationDbContext context, ILogger<WishListController> logger, UserManager<Usuario> userManager)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
        }

        public async Task<IActionResult> WishList(){

            return View(await _context.ListaFavoritos.ToListAsync());
        }

        public async Task<IActionResult> AddWishList(Guid id){
            var username = User.Identity.Name;
            var ListaFavoritos = _context.ListaFavoritos
            .Where(p => p.IdProducto == id)
            .FirstOrDefault();

            var user = await _userManager.FindByNameAsync(username);
            var userId = user.Id;
            if(ListaFavoritos == null)
            {
                var favorito = new ListaFavoritos
                {
                    IdUsuario = userId,
                    IdProducto = id,
                    FechaAgregado = DateTime.Now
                };
                _context.ListaFavoritos.Add(favorito);
                await _context.SaveChangesAsync();
            }
            else
            {
                _context.ListaFavoritos.Remove(ListaFavoritos);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("WishList", "WishList");
        }
    }
}