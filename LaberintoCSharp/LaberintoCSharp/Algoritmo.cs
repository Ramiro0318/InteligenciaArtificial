using System;
using System.Collections.Generic;
using System.Linq;

namespace LaberintoCSharp
{
    public class Algoritmo
    {
        public List<Nodo> abiertos;
        public List<Nodo> cerrados;
        public Nodo inicial;
        public Nodo final;

        public Algoritmo(Nodo inicial, Nodo final)
        {
            this.inicial = inicial;
            this.final = final;
        }

        /// <summary>
        /// Ejecuta A* y devuelve el camino desde el inicio hasta la meta,
        /// o null si no hay solución.
        /// </summary>
        public List<Nodo> Buscar()
        {
            abiertos = new List<Nodo>();
            cerrados = new List<Nodo>();

            inicial.g = 0;
            inicial.CalcularH(final);
            abiertos.Add(inicial);

            while (abiertos.Count > 0)
            {
                // Seleccionar el nodo con menor f
                Nodo actual = abiertos.OrderBy(x => x.f).First();

                // ¿Es la meta?
                if (actual.MismaPosicion(final))
                {
                    return ReconstruirCamino(actual);
                }

                // Mover de abiertos a cerrados
                abiertos.Remove(actual);
                cerrados.Add(actual);

                // Expandir sucesores
                foreach (var sucesor in actual.GenerarSucesores())
                {
                    sucesor.CalcularH(final);

                    // ¿Ya está en abiertos?
                    Nodo enAbiertos = abiertos.FirstOrDefault(x => x.MismaPosicion(sucesor));
                    if (enAbiertos != null)
                    {
                        // Si encontramos un camino mejor, actualizamos
                        if (sucesor.g < enAbiertos.g)
                        {
                            enAbiertos.g = sucesor.g;
                            enAbiertos.Padre = actual;
                        }
                        continue;
                    }

                    // ¿Ya está en cerrados?
                    Nodo enCerrados = cerrados.FirstOrDefault(x => x.MismaPosicion(sucesor));
                    if (enCerrados != null)
                    {
                        // Si encontramos un camino mejor, lo reabrimos
                        if (sucesor.g < enCerrados.g)
                        {
                            cerrados.Remove(enCerrados);
                            abiertos.Add(sucesor);
                        }
                        continue;
                    }

                    // Nodo nuevo: añadir a abiertos
                    abiertos.Add(sucesor);
                }
            }

            // No se encontró solución
            return null;
        }

        /// <summary>
        /// Reconstruye el camino desde la meta hasta el inicio y lo invierte.
        /// </summary>
        private List<Nodo> ReconstruirCamino(Nodo meta)
        {
            var camino = new List<Nodo>();
            Nodo temp = meta;
            while (temp != null)
            {
                camino.Add(temp);
                temp = temp.Padre;
            }
            camino.Reverse();
            return camino;
        }
    }
}