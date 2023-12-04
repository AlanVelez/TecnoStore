using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tecnostore.com.Models
{
    public class ListaFavoritos
    {

        //Tabla Lista de favoritos
        [Key]
        public Guid Id { get; set; }
        public string? IdUsuario { get; set; }
        public Guid IdProducto { get; set; }
        public DateTime FechaAgregado { get; set; }

        //Llaves foraneas
        [ForeignKey(nameof(IdUsuario))]
        public virtual Usuario Usuario { get; set; }

        [ForeignKey(nameof(IdProducto))]
        public virtual Producto Producto { get; set; }
    }
}
