using System.ComponentModel.DataAnnotations;

namespace Tecnostore.com.Models
{
    public class StatusEntrega
    {
        [Key]
        public Guid Id { get; set; }

        public string? status { get; set; }

        //Llaves foraneas
        public virtual ICollection<Envio> Envios { get; set; }
    }
}
