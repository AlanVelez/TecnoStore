using System.ComponentModel.DataAnnotations.Schema;

namespace Tecnostore.com.Models
{
    public class ProductoEliminado
    {
        public Guid Id { get; set; }
        public Guid IdProducto { get; set; }
        public string? EliminadoPor { get; set; }
        public DateTime FechaEliminacion { get; set; }


        //Llaves foraneas
        [ForeignKey(nameof(IdProducto))]
        public virtual Producto Producto { get; set; }

        [ForeignKey(nameof(EliminadoPor))]
        public virtual Usuario Usuario { get; set; }
    }
}
