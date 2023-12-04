using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tecnostore.com.Models
{
    public class Producto
    {
        //Tabla Producto
        [Key]
        public Guid Id { get; set; }
        public Guid IdCategoria { get; set; }
        [Required(ErrorMessage = "El titulo es requerido")]
        [StringLength(200, ErrorMessage = "El titulo debe tener entre 3 y 50 caracteres", MinimumLength = 3)]
        public string? Titulo { get; set; }
        public string? DescBreve { get; set; }
        [Required(ErrorMessage = "La descripcion es requerida")]
        [StringLength(10000, ErrorMessage = "La descripcion debe tener entre 3 y 10,000 caracteres", MinimumLength = 3)]
        public string? Descripcion { get; set; }
        [Required(ErrorMessage = "El precio es requerido")]
        [Range(0, 1000000, ErrorMessage = "El precio debe ser mayor a 0")]        
        public decimal? Precio { get; set; }
        public string? Etiqueta { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime LastUpdate { get; set; }
        [Required(ErrorMessage = "La marca es requerida")]
        [StringLength(50, ErrorMessage = "La marca debe tener entre 3 y 50 caracteres", MinimumLength = 3)]
        public string? Marca { get; set; }
        [Required(ErrorMessage = "El Color es requerido")]
        public string? Color { get; set; }
        [Required(ErrorMessage = "Las caracteristicas son requeridas")]
        [StringLength(10000, ErrorMessage = "Las caracteristicas deben tener entre 3 y 10,000 caracteres", MinimumLength = 3)]
        public string? Caracteristicas { get; set; }
        [Required(ErrorMessage = "El pais de origen es requerido")]
        public string? PaisOrigen { get; set;}
        public decimal? OpinionMedia { get; set; }

        //Llaves foraneas
        [ForeignKey(nameof(IdCategoria))]
        public virtual Categoria Categoria { get; set; }

        //Relaciones foraneas
        public virtual ICollection<ProductoView> ProductoViews { get; set; }
        public virtual ICollection<Review> Review { get; set; }
        public virtual ICollection<Articulo> Articulo { get; set; }
        public virtual ICollection<ListaFavoritos> ListaFavoritos { get; set; }
        public virtual ICollection<ProductoEliminado> ProdcutoEliminado { get; set; }
        public virtual ICollection<Gallery> Galerias { get; set; }


    }
}
