using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tecnostore.com.Models
{
    public class Envio
    {
        [Key]
        public Guid Id { get; set; }
        public Guid IdDireccion { get; set; }
        public Guid IdStatus { get; set; }

        //Llaves foraneas
        [ForeignKey(nameof(IdDireccion))]
        public virtual DireccionCliente DireccionCliente { get; set; }

        [ForeignKey(nameof(IdStatus))]
        public virtual StatusEntrega StatusEntrega { get; set; }

        //Relacion con la tabla Compra
        public virtual ICollection<Compra> Compras { get; set; }
    }
}
