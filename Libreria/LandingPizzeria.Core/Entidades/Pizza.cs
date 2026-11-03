using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LandingPizzeria.Core.Entidades
{
    public class Pizza
    {
        public int IdPizza { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public decimal Precio { get; set; }
        public bool Disponible { get; set; }
    }
}