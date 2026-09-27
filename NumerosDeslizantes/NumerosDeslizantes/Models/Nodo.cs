using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumerosDeslizantes.Models
{
    public class Nodo
    {
        public Tablero tablero { get;set;}

        public Nodo Padre { get;set;}
        public int G {  get;set;}
        public int H {  get;set;}
        public int F {  get;set;}

    }
}
