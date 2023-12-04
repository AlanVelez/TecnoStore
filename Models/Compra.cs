using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tecnostore.com.Models
{
    public class Compra
    {
        [Key]
        public Guid Id { get; set; }
        public Guid IdCart { get; set; }
        public Guid IdEnvio { get; set; }
        public DateTime FechaCompra { get; set; }
        public decimal Subtotal { get; set; }

        //Llaves foraneas
        [ForeignKey(nameof(IdCart))]
        public virtual Cart Cart { get; set; }
        [ForeignKey(nameof(IdEnvio))]
        public virtual Envio Envio { get; set; }

        //Relaciones foraneas
        public virtual ICollection<HistorialCompra> HistorialCompra { get; set; }
        public virtual ICollection<Pago> Pagos { get; set; }

    }
}
