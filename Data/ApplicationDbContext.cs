using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Tecnostore.com.Models;

namespace Tecnostore.com.Data
{
    public class ApplicationDbContext : IdentityDbContext<Usuario>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Articulo> Articulos { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Compra> Compras { get; set; }
        public DbSet<DireccionCliente> DireccionClientes { get; set; }
        public DbSet<Envio> Envios { get; set; }
        public DbSet<HistorialCompra> HistorialCompras { get; set; }
        public DbSet<ListaFavoritos> ListaFavoritos { get; set; }
        public DbSet<Pago> Pagos { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<ProductoEliminado> ProductoEliminados { get; set; }
        public DbSet<ProductoView> ProductoViews { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<StatusEntrega> StatusEntregas { get; set; }
        public DbSet<TipoPago> TipoPagos { get; set; }
        public DbSet<Gallery> Galerias { get; set; }

    }
}