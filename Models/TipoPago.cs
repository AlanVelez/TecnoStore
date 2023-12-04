using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tecnostore.com.Models
{
    public class TipoPago
    {
        [Key]
        public Guid Id { get; set; }
        public string? TipoTarjeta { get; set; }
        public int NumeroTarjeta { get; set; }
        public string? NombreAsociado { get; set; }
        public string? Banco { get; set; }
        public string? Pais { get; set; }
        public string? Multinacional { get; set; }
        public DateTime FechaTarjeta { get; set; }
        public string? CuentaAsociada { get; set; }

        //Llaves foraneas
        [ForeignKey(nameof(CuentaAsociada))]
        public virtual Usuario Usuario { get; set; }

        //Relaciones foraneas
        public virtual ICollection<Pago> Pagos { get; set; }
    }
}
