using NumerosDeslizantes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace NumerosDeslizantes.Service
{
    public class PartidaService
    {
        Tablero tablero = new Tablero();
        Random r = new Random();


        public Tablero GenerarTablero()
        {
            tablero.Matriz = new int[4, 4];
            tablero.Estado = estado.Inicial;
            tablero.Movimientos = 0;

            // Crear tablero resuelto
            int numero = 1;

            for (int fila = 0; fila < 4; fila++)
            {
                for (int columna = 0; columna < 4; columna++)
                {
                    tablero.Matriz[fila, columna] = numero;
                    numero++;
                }
            }

            tablero.Matriz[3, 3] = 0;

            // Cantidad aleatoria de movimientos para desordenar
            int movimientos = r.Next(20, 31);

            for (int i = 0; i < movimientos; i++)
            {
                MoverAleatoriamente();
            }

            return tablero;
        }



        public void MoverAleatoriamente()
        {
            int filaVacia = 0;
            int columnaVacia = 0;

            // Buscar el 0
            for (int fila = 0; fila < 4; fila++)
            {
                for (int columna = 0; columna < 4; columna++)
                {
                    if (tablero.Matriz[fila, columna] == 0)
                    {
                        filaVacia = fila;
                        columnaVacia = columna;
                    }
                }
            }

            // Lista de movimientos válidos
            List<(int fila, int columna)> movimientosValidos = new List<(int, int)>();

            // Arriba
            if (filaVacia > 0) movimientosValidos.Add((filaVacia - 1, columnaVacia));

            // Abajo
            if (filaVacia < 3) movimientosValidos.Add((filaVacia + 1, columnaVacia));

            // Izquierda
            if (columnaVacia > 0) movimientosValidos.Add((filaVacia, columnaVacia - 1));

            // Derecha
            if (columnaVacia < 3) movimientosValidos.Add((filaVacia, columnaVacia + 1));

            // Elegir una posición válida al azar
            int indice = r.Next(movimientosValidos.Count);

            int nuevaFila = movimientosValidos[indice].fila;
            int nuevaColumna = movimientosValidos[indice].columna;

            // Intercambiar el 0 con el número elegido
            tablero.Matriz[filaVacia, columnaVacia] = tablero.Matriz[nuevaFila, nuevaColumna];
            tablero.Matriz[nuevaFila, nuevaColumna] = 0;
        }








        //Calculo de distancia Manhattan

        public int CalcularH(Tablero tablero)
        {
            int h = 0;

            for (int fila = 0; fila < 4; fila++)
            {
                for (int columna = 0; columna < 4; columna++)
                {
                    int valor = tablero.Matriz[fila, columna];

                    // El espacio vacío no se considera
                    if (valor == 0)
                        continue;

                    int filaObjetivo = (valor - 1) / 4;
                    int columnaObjetivo = (valor - 1) % 4;

                    h += Math.Abs(fila - filaObjetivo) + Math.Abs(columna - columnaObjetivo);
                }
            }

            return h;
        }





        //Esto busca y regresa todos los movimientos validos que tine el 0 en el estado actual del tablero

        private List<(int fila, int columna)> ObtenerMovimientosValidos(Tablero tablero)
        {
            List<(int fila, int columna)> movimientosValidos = new List<(int fila, int columna)>();

            int filaVacia = 0;
            int columnaVacia = 0;

            // Buscar el 0
            for (int fila = 0; fila < 4; fila++)
            {
                for (int columna = 0; columna < 4; columna++)
                {
                    if (tablero.Matriz[fila, columna] == 0)
                    {
                        filaVacia = fila;
                        columnaVacia = columna;
                    }
                }
            }

            // Comprobar movimientos posibles

            // Arriba
            if (filaVacia > 0)
                movimientosValidos.Add((filaVacia - 1, columnaVacia));

            // Abajo
            if (filaVacia < 3)
                movimientosValidos.Add((filaVacia + 1, columnaVacia));

            // Izquierda
            if (columnaVacia > 0)
                movimientosValidos.Add((filaVacia, columnaVacia - 1));

            // Derecha
            if (columnaVacia < 3)
                movimientosValidos.Add((filaVacia, columnaVacia + 1));

            return movimientosValidos;
        }




        // Recibe un tablero y una posición válida a la que puede moverse el 0;
        // Crea una copia del tablero y realiza ese movimiento en la copia.
        private Tablero CrearMovimiento(Tablero tablero, int nuevaFila, int nuevaColumna)
        {
            Tablero nuevoTablero = new Tablero();

            nuevoTablero.Matriz = new int[4, 4];

            // Copiar la matriz
            for (int fila = 0; fila < 4; fila++)
            {
                for (int columna = 0; columna < 4; columna++)
                {
                    nuevoTablero.Matriz[fila, columna] = tablero.Matriz[fila, columna];
                }
            }

            // Buscar el 0
            int filaVacia = 0;
            int columnaVacia = 0;

            for (int fila = 0; fila < 4; fila++)
            {
                for (int columna = 0; columna < 4; columna++)
                {
                    if (nuevoTablero.Matriz[fila, columna] == 0)
                    {
                        filaVacia = fila;
                        columnaVacia = columna;
                    }
                }
            }

            // Mover el 0
            nuevoTablero.Matriz[filaVacia, columnaVacia] =
                nuevoTablero.Matriz[nuevaFila, nuevaColumna];

            nuevoTablero.Matriz[nuevaFila, nuevaColumna] = 0;

            return nuevoTablero;
        }










        private List<Nodo> GenerarPosibilidades(
    Nodo nodoActual,
    List<Nodo> abiertos,
    List<Nodo> cerrados)
        {
            List<Nodo> posibilidades = new List<Nodo>();

            List<(int fila, int columna)> movimientos =
                ObtenerMovimientosValidos(nodoActual.tablero);

            foreach (var movimiento in movimientos)
            {
                Tablero nuevoTablero = CrearMovimiento(
                    nodoActual.tablero,
                    movimiento.fila,
                    movimiento.columna
                );

                // Si ya analizamos este tablero, lo ignoramos
                if (ContieneTablero(cerrados, nuevoTablero))
                {
                    continue;
                }

                // Si ya está pendiente de analizar, lo ignoramos
                if (ContieneTablero(abiertos, nuevoTablero))
                {
                    continue;
                }

                Nodo nuevoNodo = new Nodo();

                nuevoNodo.tablero = nuevoTablero;
                nuevoNodo.Padre = nodoActual;

                nuevoNodo.G = nodoActual.G + 1;

                nuevoNodo.H = CalcularH(nuevoTablero);

                nuevoNodo.F = nuevoNodo.G + nuevoNodo.H;

                posibilidades.Add(nuevoNodo);
            }

            return posibilidades;
        }






        private Nodo ObtenerMejorNodo(List<Nodo> abiertos)
        {
            Nodo mejor = abiertos[0];

            foreach (Nodo nodo in abiertos)
            {
                if (nodo.F < mejor.F)
                {
                    mejor = nodo;
                }
            }

            return mejor;
        }




        public List<Nodo> Resolver(Tablero tablero)
        {
            Nodo inicial = new Nodo();

            inicial.tablero = tablero;
            inicial.Padre = null;

            inicial.G = 0;
            inicial.H = CalcularH(tablero);
            inicial.F = inicial.G + inicial.H;

            List<Nodo> abiertos = new List<Nodo>();
            List<Nodo> cerrados = new List<Nodo>();

            abiertos.Add(inicial);

            while (abiertos.Count > 0)
            {
                Nodo actual = ObtenerMejorNodo(abiertos);

                abiertos.Remove(actual);

                if (EstaResuelto(actual.tablero))
                {
                    return ObtenerSolucion(actual);
                }

                cerrados.Add(actual);

                List<Nodo> posibilidades = GenerarPosibilidades(
                    actual,
                    abiertos,
                    cerrados
                );

                foreach (Nodo nodo in posibilidades)
                {
                    abiertos.Add(nodo);
                }
            }

            return new List<Nodo>();
        }




        private bool EsMismoTablero(Tablero tablero1, Tablero tablero2)
        {
            for (int fila = 0; fila < 4; fila++)
            {
                for (int columna = 0; columna < 4; columna++)
                {
                    if (tablero1.Matriz[fila, columna] != tablero2.Matriz[fila, columna])
                    {
                        return false;
                    }
                }
            }

            return true;
        }



        private bool EstaResuelto(Tablero tablero)
        {
            int numeroEsperado = 1;

            for (int fila = 0; fila < 4; fila++)
            {
                for (int columna = 0; columna < 4; columna++)
                {
                    // La última posición debe contener el 0
                    if (fila == 3 && columna == 3)
                    {
                        return tablero.Matriz[fila, columna] == 0;
                    }

                    if (tablero.Matriz[fila, columna] != numeroEsperado)
                    {
                        return false;
                    }

                    numeroEsperado++;
                }
            }

            return true;
        }



        private bool ContieneTablero(List<Nodo> nodos, Tablero tablero)
        {
            foreach (Nodo nodo in nodos)
            {
                if (EsMismoTablero(nodo.tablero, tablero))
                {
                    return true;
                }
            }

            return false;
        }




        private List<Nodo> ObtenerSolucion(Nodo nodoFinal)
        {
            List<Nodo> solucion = new List<Nodo>();

            Nodo actual = nodoFinal;

            while (actual != null)
            {
                solucion.Add(actual);
                actual = actual.Padre;
            }

            solucion.Reverse();

            return solucion;
        }

















    }
}
