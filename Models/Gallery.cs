using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; // Agrega esta directiva
using Microsoft.AspNetCore.Http;

namespace Tecnostore.com.Models
{
    public class Gallery
    {
        //Tabla Galeria
        [Key]
        public Guid Id { get; set; }
        public Guid IdProducto { get; set; }
        public string? Imagen { get; set; }
        [NotMapped] // No se mapea a la base de datos
        public IFormFile ImagenFile { get; set; }
        public DateTime FechaDeSubida { get; set; }
        

        //Llaves foraneas
        [ForeignKey(nameof(IdProducto))]
        public virtual Producto Producto { get; set; }


    }
}
