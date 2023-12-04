using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tecnostore.com.Models
{
    public class DireccionCliente
    {
        [Key]
        public Guid Id { get; set; }
        public string? IdUsuario { get; set; }
        public string? Pais { get; set; }
        public string? Ciudad { get; set; }
        public string? Estado { get; set; }
        public string? Calle { get; set; }
        public int PostalCode { get; set; }
        public string? NumeroCasa { get; set; }
        public string? Colonia { get; set; }
        public string? CalleIzq { get; set; }
        public string? CalleDer { get; set; }

        //Llaves foraneas

        [ForeignKey(nameof(IdUsuario))]
        public virtual Usuario Usuario { get; set; }

        //Relaciones foraneas
        public virtual ICollection<Envio> Envios { get; set; }
        public virtual ICollection<Pago> Pagos { get; set; }

    }
}
