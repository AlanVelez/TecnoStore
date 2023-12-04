using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tecnostore.com.Models
{
    public class Pago
    {
        [Key]
        public Guid Id { get; set; }
        public decimal Total { get; set; }
        public DateTime FechaPago { get; set; }
        public Guid IdTipoPago { get; set; }
        public Guid IdCompra { get; set; }
        public string? EstadoPago { get; set; }
        public string? Direccion { get; set; }
        public string? Ciudad { get; set; }
        public int CodigoPostal { get; set; }
        public string? Pais { get; set; }
        public string? Detalles { get; set; }

        //Llaves Foraneas
        [ForeignKey(nameof(IdTipoPago))]
        public virtual TipoPago TipoPago { get; set; }

        [ForeignKey(nameof(IdCompra))]
        public virtual Compra Compra { get; set; }



    }
}
