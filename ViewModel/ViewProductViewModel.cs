using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Tecnostore.com.Models;

namespace Tecnostore.com.ViewModel
{
    public class ViewProductViewModel{

        public IEnumerable<Categoria> Categorias { get; set; }
        public Producto Producto { get; set; }
        public Articulo Articulo { get; set; }

    }
}