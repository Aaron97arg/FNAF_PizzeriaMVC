using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LandingPizzeria.Core.Entidades
{
    public class DetallePedido
    {
        public int IdPedido { get; set; }
        public int IdPizza { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
    }
}