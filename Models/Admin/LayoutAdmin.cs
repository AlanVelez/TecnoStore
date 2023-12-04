#nullable disable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Tecnostore.com.Models.Admin
{
    public class LayoutAdmin
    {
        public static string Index => "Index";
        public static string Producto => "Productos";
        public static string Categoria => "Categorias";
        public static string Orden => "Ordenes";
        public static string Usuario => "Usuarios";
        public static string Review => "Reviews";

        public static string IndexClass(ViewContext viewContext) => PageClass(viewContext, Index);
        public static string ProductoClass(ViewContext viewContext) => PageClass(viewContext, Producto);
        public static string CategoriaClass(ViewContext viewContext) => PageClass(viewContext, Categoria);
        public static string OrdenClass(ViewContext viewContext) => PageClass(viewContext, Orden);
        public static string UsuarioClass(ViewContext viewContext) => PageClass(viewContext, Usuario);
        public static string ReviewClass(ViewContext viewContext) => PageClass(viewContext, Review);

        public static string PageClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePage"] as string
                ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active-dark" : null;
        }
    }
}