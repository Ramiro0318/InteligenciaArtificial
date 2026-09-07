using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Text;

namespace LaberintoC_
{
    public class Algoritmo
    {
        public List<Nodo> abiertos;
        public List<Nodo> cerrados;


        //public string tablero;
        public Nodo final;
        public Nodo inicial;

        public Algoritmo(Nodo final, Nodo inicial)
        {
            this.final = final;
            this.inicial = inicial;

        }


        public void Buscar()
        {
            abiertos = new List<Nodo>();
            cerrados = new List<Nodo>();

            bool exito = false;
            bool fallo = false;

            inicial.CalcularH(final);
            inicial.G = 0;
            Nodo mejorNodo = null;
            abiertos.Add(inicial);

            do
            {
                mejorNodo = abiertos.OrderBy(x => x.G).First();
                abiertos.Remove(mejorNodo);
                cerrados.Add(mejorNodo);

                if (mejorNodo.X == final.X && mejorNodo.Y == final.Y)
                {
                    exito = true;
                }
                else
                {
                    var sucesores = mejorNodo.GenerarSucesores();
                    Nodo viejo = null;
                    foreach (var nodo in sucesores)
                    {
                        viejo = sucesores.Any<Nodo>(x => nodo.X == x.X && nodo.Y == x.Y);
                    }
                }
            } while (!fallo || !exito);
        }


    }
}
