using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tecnostore.com.Helpers
{
    public class PagedList<T>
    {
        public List<T> Items { get; }
        public int TotalItems { get; }
        public int NumeroPagina { get; }
        public int TamanoPagina { get; }
        public int TotalPaginas => (int)Math.Ceiling((double)TotalItems / TamanoPagina);
        public int PrimerElementoEnPagina => (NumeroPagina - 1) * TamanoPagina + 1;
        public int UltimoElementoEnPagina => Math.Min(NumeroPagina * TamanoPagina, TotalItems);
        public bool HasAnterior => NumeroPagina > 1;
        public bool HasSiguiente => NumeroPagina < TotalPaginas;
    
        public PagedList(List<T> items, int totalItems, int numeroPagina, int tamanoPagina)
        {
            Items = items;
            TotalItems = totalItems;
            NumeroPagina = numeroPagina;
            TamanoPagina = tamanoPagina;
        }
    }

}