using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumerosDeslizantes.Models
{
    public enum estado {Inicial, EnProceso, Resuelto}
    public class Tablero
    {
        public estado Estado { get; set; }

        public int[,] Matriz { get; set; }

        public int Movimientos { get; set; }
    }
}
