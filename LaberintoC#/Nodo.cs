using System;
using System.Collections.Generic;
using System.Text;

namespace LaberintoC_
{
    public class Nodo
    {
        public int H { get; set; }
        public int G { get; set; }
        public int F
        {
            get { return H + g; }
        }

        public int X { get; set; }
        public int Y { get; set; }
        public static int X0;
        public static int Y0;

        public static string Tablero { get; set; } = "            ** *                              *           *           * *  *           * *          * *      * * *      * *    *     * *      * *                 *     *          *           *          *     *               *               * **   *        ";

        public IEnumerable<Nodo> GenerarSucesores()
        {
            if (Y != 0)
            {
                if (Tablero[(Y - 1) * 16 * X] != 'X')
                {
                    yield return new Nodo { X = this.X, Y = this.Y - 1 };
                }
            }
            if (Y < 15)
            {
                if (Tablero[(Y + 1) * 16 * X] != 'X')
                {
                    yield return new Nodo { X = this.X, Y = this.Y + 1 };

                }
            }

            if (X != 0)
            {
                if (Tablero[Y * 16 + (X - 1)] != 'X')
                {
                    yield return new Nodo { X = this.X - 1, Y = this.Y + 1 };
                }
            }
            if (X < 15)
            {
                if (Tablero[Y * 16 + (X + 1)] != 'X')
                {
                    yield return new Nodo { X = this.X - 1, Y = this.Y + 1 };
                }
            }

        }

    }
}
