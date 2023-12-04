using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tecnostore.com.Models
{
    public class HistorialCompra
    {
        [Key]
        public Guid Id { get; set; }
        public string? IdUsuario { get; set; }
        public Guid IdCompra { get; set; }

        //Llaves foraneas
        [ForeignKey(nameof(IdUsuario))]
        public virtual Usuario Usuario { get; set; }

        [ForeignKey(nameof(IdCompra))]
        public virtual Compra Compra { get; set; }


    }
}
