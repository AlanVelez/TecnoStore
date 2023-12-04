using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tecnostore.com.Models
{
    public class Review
    {
        [Key]
        public Guid Id { get; set; }
        public string? IdUsuario { get; set; }
        public Guid IdProducto { get; set; }
        public string? Opinion { get; set; }
        public decimal Calificacion { get; set; }

        //Llaves foraneas
        [ForeignKey(nameof(IdUsuario))]
        public virtual Usuario Usuario { get; set; }
        [ForeignKey(nameof(IdProducto))]
        public virtual Producto Producto { get; set; }

    }
}
