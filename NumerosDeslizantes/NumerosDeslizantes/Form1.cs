using NumerosDeslizantes.Models;
using NumerosDeslizantes.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NumerosDeslizantes
{
    public partial class Form1 : Form
    {

        PartidaService partidaService = new PartidaService();
        Button[,] botones = new Button[4, 4];

        Tablero tableroActual;
        List<Nodo> solucion;
        int pasoActual = 0;

        public Form1()
        {
            InitializeComponent();

            tableroActual = new Tablero();

            tableroActual.Matriz = new int[4, 4];

            int numero = 1;


            for (int fila = 0; fila < 4; fila++)
            {
                for (int columna = 0; columna < 4; columna++)
                {
                    Button boton = new Button();

                    boton.Width = 70;
                    boton.Height = 70;

                    boton.Left = columna * 70;
                    boton.Top = fila * 70;

                    boton.Tag = (fila, columna);
                    boton.Click += BotonTablero_Click;

                    botones[fila, columna] = boton;

                    tableroActual.Matriz[fila, columna] = numero;
                    numero++;

                    Controls.Add(boton);
                }
            }

            tableroActual.Matriz[3, 3] = 0;
            MostrarTablero(tableroActual);
        }



        private void BotonTablero_Click(object sender, EventArgs e)
        {
            Button boton = (Button)sender;

            var posicion = ((int fila, int columna))boton.Tag;

            int fila = posicion.fila;
            int columna = posicion.columna;

            if (!MovimientoValido(fila, columna))
            {
                return;
            }

            int filaVacia = 0;
            int columnaVacia = 0;

            for (int f = 0; f < 4; f++)
            {
                for (int c = 0; c < 4; c++)
                {
                    if (tableroActual.Matriz[f, c] == 0)
                    {
                        filaVacia = f;
                        columnaVacia = c;
                    }
                }
            }

            int temporal = tableroActual.Matriz[fila, columna];

            tableroActual.Matriz[fila, columna] = 0;
            tableroActual.Matriz[filaVacia, columnaVacia] = temporal;

            MostrarTablero(tableroActual);

            solucion = null;
            pasoActual = 0;

            btnSiguiente.Enabled = false;
        }


        private bool MovimientoValido(int fila, int columna)
        {
            int filaVacia = 0;
            int columnaVacia = 0;

            for (int f = 0; f < 4; f++)
            {
                for (int c = 0; c < 4; c++)
                {
                    if (tableroActual.Matriz[f, c] == 0)
                    {
                        filaVacia = f;
                        columnaVacia = c;
                    }
                }
            }

            int diferenciaFila = Math.Abs(fila - filaVacia);
            int diferenciaColumna = Math.Abs(columna - columnaVacia);

            return diferenciaFila + diferenciaColumna == 1;
        }



        private void btnGenerar_Click(object sender, EventArgs e)
        {
            tableroActual = partidaService.GenerarTablero();

            solucion = null;
            pasoActual = 0;

            btnSiguiente.Enabled = false;

            listBox1.Items.Clear();

            MostrarTablero(tableroActual);
        }

        private void btnResolver_Click(object sender, EventArgs e)
        {
            solucion = partidaService.Resolver(tableroActual);
            pasoActual = 0;
            listBox1.Items.Clear();

            if (solucion.Count == 0)
            {
                MessageBox.Show("No se encontró una solución.");
                return;
            }

            btnSiguiente.Enabled = true;
            tableroActual = solucion[pasoActual].tablero;
            MostrarTablero(solucion[pasoActual].tablero);

            listBox1.Items.Add("Paso: " + pasoActual);
            listBox1.Items.Add("G(n) = " + solucion[pasoActual].G);
            listBox1.Items.Add("H(n) = " + solucion[pasoActual].H);
            listBox1.Items.Add("F(n) = " + solucion[pasoActual].F);

            MessageBox.Show("Movimientos encontrados: " + (solucion.Count - 1));
        }







        private void MostrarTablero(Tablero tablero)
        {
            for (int fila = 0; fila < 4; fila++)
            {
                for (int columna = 0; columna < 4; columna++)
                {
                    int valor = tablero.Matriz[fila, columna];

                    if (valor == 0)
                    {
                        botones[fila, columna].Text = "";
                    }
                    else
                    {
                        botones[fila, columna].Text = valor.ToString();
                    }
                }
            }
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {

            if (pasoActual >= solucion.Count - 1)
            {
                MessageBox.Show("El tablero ya está resuelto.");
                return;
            }

            pasoActual++;

            Nodo nodoActual = solucion[pasoActual];
            tableroActual = nodoActual.tablero;

            MostrarTablero(nodoActual.tablero);

            listBox1.Items.Clear();

            listBox1.Items.Add("Paso: " + pasoActual);
            listBox1.Items.Add("G(n) = " + nodoActual.G);
            listBox1.Items.Add("H(n) = " + nodoActual.H);
            listBox1.Items.Add("F(n) = " + nodoActual.F);
        }
    }
}
