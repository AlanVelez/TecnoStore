using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tecnostore.com.Models
{
    public class ProductoView
    {
        [Key]
        public Guid Id { get; set; }
        public string? IdUsuario { get; set; }
        public Guid IdProducto { get; set; }
        public DateTime FechaVisto { get; set; }

        //Llaves foraneas
        [ForeignKey(nameof(IdProducto))]
        public virtual Producto Producto { get; set; }

        [ForeignKey(nameof(IdUsuario))]
        public virtual string Usuario { get; set; }
    }
}
