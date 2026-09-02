using System;
using System.Collections.Generic;
using System.Text;

namespace LaberintoC_
{
    public class Nodo
    {
        public int X { get; set; }
        public int Y { get; set; }
        public static int X0;
        public static int Y0;

        public static string Tablero { get; set; } = "            ** *                              *           *           * *  *           * *          * *      * * *      * *    *     * *      * *                 *     *          *           *          *     *               *               * **   *        ";

        public List<Nodo> GenerarSucesores()
        {
            if (Y != 0)
            {
                if (Tablero[ (Y-1) * 16 * X] != 'X')
                {
                    yield return new Nodo { X = this.X, Y = this.Y - 1 };
                }
            }

        }

    }
}
