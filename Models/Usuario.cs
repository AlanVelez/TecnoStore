using Microsoft.AspNetCore.Identity;

namespace Tecnostore.com.Models
{
    public class Usuario : IdentityUser
    {
        //Especifica que son datos personales y por lo tanto descargables
        [PersonalData]
        public string? FirstName { get; set; }
        [PersonalData]
        public string? LastName { get; set; }

        //Relaciones foraneas
        public virtual ICollection<DireccionCliente>? DireccionClientes { get; set; }
        public virtual ICollection<ProductoView>? ProductoViews { get; set; }
        public virtual ICollection<TipoPago>? TipoPagos { get; set; }
        public virtual ICollection<HistorialCompra>? HistorialCompras { get; set; }
        public virtual ICollection<Review>? Reviews { get; set; }
        public virtual ICollection<Cart>? Carts { get; set; }
        public virtual ICollection<ListaFavoritos>? ListaFavoritos { get; set; }
        public virtual ICollection<ProductoEliminado>? ProdcutosEliminados { get; set; }

    }
}
