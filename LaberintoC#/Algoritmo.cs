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
                        viejo = abiertos.FirstOrDefault<Nodo>(x => nodo.X == x.X && nodo.Y == x.Y);
                        if (viejo != null)
                        {
                            if (viejo.G > nodo.G)
                            {
                                nodo.CalcularH(final);
                                abiertos.Remove(viejo);
                                abiertos.Add(nodo);
                            }
                        }
                        viejo = abiertos.FirstOrDefault<Nodo>(x => nodo.X == x.X && nodo.Y == x.Y);
                        if (viejo != null)
                        {
                            cerrados.Remove(viejo);
                            cerrados.Add(nodo);
                            nodo.CalcularH(final);
                            nodo.Padre = viejo.Padre;
                            propagarG(nodo);
                        }

                        if (abiertos.Count == 0)
                        {
                            fallo = true;
                        }
                    }

                }
            } while (!fallo || !exito);
            if (exito)
            {
                Nodo temp;
                List<Nodo> solucion = new();
                temp = mejorNodo;
                while (temp.Padre != null)
                {
                    solucion.Add(temp);
                    temp = temp.Padre;
                }
                solucion.Add(temp);

            }
        }

        /// <summary>
        /// Mi primera documentacion
        /// </summary>
        /// <param name="n"></param>

        public void propagarG(Nodo n)
        {
            var hijos = abiertos.Where(x => x.Padre == n);

            foreach (Nodo hijo in hijos)
            {
                hijo.G = n.G +1;
            }
            
            hijos = cerrados.Where(x => x.Padre == n);
            foreach (Nodo hijo in hijos)
            {
                hijo.G = n.G + 1;
                propagarG(hijo);
            }


        }

    }
}
