using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; // Agrega esta directiva
using Microsoft.AspNetCore.Http;


namespace Tecnostore.com.Models
{
    public class Categoria
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        [StringLength(50, ErrorMessage = "El nombre debe tener entre 3 y 50 caracteres", MinimumLength = 3)]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "El nombre solo puede contener letras")]
        public string? Nombre { get; set; }

        [Required(ErrorMessage = "La descripcion es requerida")]
        [StringLength(120, ErrorMessage = "La descripcion debe tener entre 3 y 120 caracteres", MinimumLength = 3)]
        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "La etiqueta es requerida")]
        [StringLength(50, ErrorMessage = "La etiqueta debe tener entre 3 y 50 caracteres", MinimumLength = 3)]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "La etiqueta solo puede contener letras")]
        public string Etiqueta { get; set; }
        public string? Imagen { get; set; }
        [NotMapped] // No se mapea a la base de datos
        public IFormFile ImagenFile { get; set; }
        public DateTime fechaCreacion { get; set; }
        public DateTime LastUpdate { get; set; }

        //Define la relacion foranea.
        public virtual ICollection<Producto> Productos { get; set; }
    }
}
