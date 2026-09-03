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

            abiertos.Add(inicial);

            do
            {

            } while (!fallo || !exito);
        }


    }
}
