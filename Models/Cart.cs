using System.ComponentModel.DataAnnotations.Schema;

namespace Tecnostore.com.Models
{
    public class Cart
    {
        public Guid Id { get; set; }
        public string? IdUsuario { get; set; }

        //Llaves foraneas
        [ForeignKey(nameof(IdUsuario))]
        public virtual Usuario Usuario { get; set; }

        //Relaciones foraneas
        public virtual ICollection<Compra> Compras { get; set; }
    }
}
