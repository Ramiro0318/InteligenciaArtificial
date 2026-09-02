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
                if (Tablero[X * 16 * Y] != 'X')
                {

                }
            }
        }

    }
}
