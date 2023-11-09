using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;

namespace Puzzle15
{
    public class Graella: Grid
    {
        Fitxa[,] fitxes;
        int nColumnes;
        int nFiles;
        int moviments = 0;
        double percentCompletat = 0;
        bool completat = false;

        public Fitxa[,] Fitxes { get => fitxes; set => fitxes = value; }
        public int NColumnes { get => nColumnes; set => nColumnes = value; }
        public int NFiles { get => nFiles; set => nFiles = value; }
        public int Moviments { get => moviments; set => moviments = value; }
        public bool Completat { get => completat; set => completat = value; }
        public double PercentCompletat { get => percentCompletat; set => percentCompletat = value; }

        public Graella (int nFiles, int nColumnes)
        {
            this.NFiles = nFiles;
            this.NColumnes = nColumnes;
            this.Fitxes = new Fitxa[nFiles, nColumnes];

            this.ShowGridLines = true;
            this.CreaFiles(nFiles);
            this.CreaColumnes(nColumnes);

            PosarBotons();
        }

        private void PosarBotons()
        {
            int[] numeros = DesordenarNumeros();
            int count = 0;

            if (!EsSoluble(numeros))
            {
                FerSoluble(numeros);
            }

            for (int i = 0; i < NFiles; i++)
            {
                for (int y = 0; y < NColumnes; y++)
                {
                    if (i == Fitxes.GetLength(0) - 1 && y == Fitxes.GetLength(1) - 1)
                    {
                        Fitxa fitxa = new Fitxa(0, i, y, true);

                        Grid.SetRow(fitxa, i);
                        Grid.SetColumn(fitxa, y);
                        this.Children.Add(fitxa);
                        fitxa.Click += Boto_Click;
                        fitxa.Visibility = Visibility.Hidden;
                        Fitxes[i, y] = fitxa;
                    }
                    else
                    {
                        Fitxa fitxa = new Fitxa(numeros[count], i, y, false);

                        Grid.SetRow(fitxa, i);
                        Grid.SetColumn(fitxa, y);
                        this.Children.Add(fitxa);
                        fitxa.Click += Boto_Click;
                        Fitxes[i, y] = fitxa;
                        count++;
                    }
                }
            }
            Pintar();
        }

        public void Pintar()
        {
            int numCorrectes = 0;
            int j = 1;
            for (int i = 0; i < Fitxes.GetLength(0); i++)
            {
                for (int y = 0; y < Fitxes.GetLength(1); y++)
                {
                    if (Fitxes[i, y].Numero == j)
                    {
                        Fitxes[i, y].Background = Brushes.Green;
                        numCorrectes++;
                    }
                    else
                    {
                        Fitxes[i, y].Background = Brushes.Red;
                    }
                    j++;
                }
            }
            PercentCompletat = ((double)numCorrectes / ((NFiles * NColumnes) - 1)) * 100.0;
            if (PercentCompletat == 100.0) { completat = true; }
        }

        private void FerSoluble(int[] numerosDesordenats)
        {
            int tmp;
            int ultimNum = numerosDesordenats.Length - 1;
            int penultimNum = numerosDesordenats.Length - 2;

            tmp = ultimNum;
            ultimNum = penultimNum;
            penultimNum = tmp;
        }

        private void Boto_Click(object sender, RoutedEventArgs e)
        {
            Fitxa fitxa = (Fitxa)sender;
            int tmp = fitxa.Numero;

            if (fitxa.Columna + 1 < Fitxes.GetLength(1) && Fitxes[fitxa.Fila, fitxa.Columna + 1].EsBuida == true)
            {
                Fitxa fitBuida = Fitxes[fitxa.Fila, fitxa.Columna + 1];

                fitxa.Numero = fitBuida.Numero;
                fitBuida.Numero = tmp;
                fitxa.Content = fitxa.Numero;
                fitxa.EsBuida = true;
                fitxa.Visibility = Visibility.Hidden;

                fitBuida.Content = fitBuida.Numero;
                fitBuida.Visibility = Visibility.Visible;
                fitBuida.EsBuida = false;
            }

            else if (fitxa.Columna - 1 >= 0 && Fitxes[fitxa.Fila, fitxa.Columna - 1].EsBuida == true)
            {
                Fitxa fitBuida = Fitxes[fitxa.Fila, fitxa.Columna - 1];

                fitxa.Numero = fitBuida.Numero;
                fitBuida.Numero = tmp;
                fitxa.Content = fitxa.Numero;
                fitxa.EsBuida = true;
                fitxa.Visibility = Visibility.Hidden;

                fitBuida.Content = fitBuida.Numero;
                fitBuida.Visibility = Visibility.Visible;
                fitBuida.EsBuida = false;
            }

            else if (fitxa.Fila + 1 < Fitxes.GetLength(0) && Fitxes[fitxa.Fila + 1, fitxa.Columna].EsBuida == true)
            {
                Fitxa fitBuida = Fitxes[fitxa.Fila + 1, fitxa.Columna];

                fitxa.Numero = fitBuida.Numero;
                fitBuida.Numero = tmp;
                fitxa.Content = fitxa.Numero;
                fitxa.EsBuida = true;
                fitxa.Visibility = Visibility.Hidden;

                fitBuida.Content = fitBuida.Numero;
                fitBuida.Visibility = Visibility.Visible;
                fitBuida.EsBuida = false;
            }

            else if (fitxa.Fila - 1 >= 0 && Fitxes[fitxa.Fila - 1, fitxa.Columna].EsBuida == true)
            {
                Fitxa fitBuida = Fitxes[fitxa.Fila - 1, fitxa.Columna];

                fitxa.Numero = fitBuida.Numero;
                fitBuida.Numero = tmp;
                fitxa.Content = fitxa.Numero;
                fitxa.EsBuida = true;
                fitxa.Visibility = Visibility.Hidden;

                fitBuida.Content = fitBuida.Numero;
                fitBuida.Visibility = Visibility.Visible;
                fitBuida.EsBuida = false;
            }

            Moviments++;
            Pintar();
        }

        private int[] DesordenarNumeros()
        {
            Random r = new Random();
            int[] numeros = new int[this.NFiles * this.NColumnes - 1];

            for (int i = 0; i < numeros.Length; i++)
            {
                numeros[i] = i + 1;
            }

            for (int i = numeros.Length - 1; i > 0; i--)
            {
                int j = r.Next(0, i + 1);
                int temp = numeros[i];
                numeros[i] = numeros[j];
                numeros[j] = temp;
            }

            return numeros;
        }

        private bool EsSoluble(int[] numerosDesordenats)
        {
            bool soluble = false;
            int count = 0;

            for (int i = 0; i < numerosDesordenats.Length - 1; i++)
            {
                for (int j = i + 1; j < numerosDesordenats.Length; j++)
                {
                    if (numerosDesordenats[i] > numerosDesordenats[j])
                    {
                        count++;
                    }
                }
            }

            if (count % 2 == 0) { soluble = true; }

            return soluble;
        }
    }

    public static class MetodesExtensio
    {
        public static void CreaColumnes(this Grid graella, int numColumnes)
        {
            for (int fila = 0; fila < numColumnes; fila++)
            {
                ColumnDefinition cd = new ColumnDefinition();
                cd.Width = new GridLength(1, GridUnitType.Star);
                graella.ColumnDefinitions.Add(cd);
            }
        }

        public static void CreaFiles(this Grid graella, int numFiles)
        {
            for (int fila = 0; fila < numFiles; fila++)
            {
                RowDefinition rd = new RowDefinition();
                rd.Height = new GridLength(1F, GridUnitType.Star);
                graella.RowDefinitions.Add(rd);
            }
        }
    }
}
