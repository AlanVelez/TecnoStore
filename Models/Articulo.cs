using System.ComponentModel.DataAnnotations.Schema;

namespace Tecnostore.com.Models
{
    public class Articulo
    {
        public Guid Id { get; set; }
        public Guid IdProducto { get; set; }
        public string Nombre { get; set; }
        public int Cantidad { get; set; }
        public decimal? Precio { get; set; }
        public string? Imagen { get; set; }

        //Llaves foraneas
        [ForeignKey(nameof(IdProducto))]
        public virtual Producto Producto { get; set; }
    }
}
