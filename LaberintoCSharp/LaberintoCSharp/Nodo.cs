using System;
using System.Collections.Generic;

namespace LaberintoCSharp
{
    public class Nodo
    {
        // Coste acumulado desde el inicio
        public int g { get; set; }

        // Heurística (estimación hasta la meta)
        public int h { get; set; }

        // Coste total f = g + h
        public int f
        {
            get { return g + h; }
        }

        public int X { get; set; }
        public int Y { get; set; }

        // Laberinto 16x16 representado como cadena.
        // 'X' = pared, ' ' = camino libre.
        public static string Tablero { get; set; } =
            "     XX           X                  X                      XXXX XX X                                     X X               X X           X             X             X                      XXX                                      X XXX                     ";

        public const int ANCHO = 16;
        public const int ALTO = 16;

        public Nodo Padre { get; set; }

        /// <summary>
        /// Genera los nodos vecinos alcanzables (arriba, abajo, izquierda, derecha).
        /// Asigna Padre y g a cada sucesor.
        /// </summary>
        public IEnumerable<Nodo> GenerarSucesores()
        {
            var sucesores = new List<Nodo>();

            int[] dx = { 0, 0, -1, 1 };
            int[] dy = { -1, 1, 0, 0 };

            for (int i = 0; i < 4; i++)
            {
                int nx = X + dx[i];
                int ny = Y + dy[i];

                // Comprobar límites
                if (nx < 0 || nx >= ANCHO || ny < 0 || ny >= ALTO)
                    continue;

                // Comprobar que no sea pared
                if (Tablero[ny * ANCHO + nx] == 'X')
                    continue;

                sucesores.Add(new Nodo
                {
                    X = nx,
                    Y = ny,
                    Padre = this,
                    g = this.g + 1
                });
            }

            return sucesores;
        }

        /// <summary>
        /// Calcula la función heurística (distancia Manhattan) y la guarda en h.
        /// </summary>
        public void CalcularH(Nodo final)
        {
            h = Math.Abs(this.X - final.X) + Math.Abs(this.Y - final.Y);
        }

        /// <summary>
        /// Devuelve true si este nodo está en la misma posición que otro.
        /// </summary>
        public bool MismaPosicion(Nodo otro)
        {
            return otro != null && this.X == otro.X && this.Y == otro.Y;
        }
    }
}