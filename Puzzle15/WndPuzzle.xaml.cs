using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Puzzle15
{
    /// <summary>
    /// Lógica de interacción para WndPuzzle.xaml
    /// </summary>
    public partial class WndPuzzle : Window
    {
        //JOC
        int nFiles;
        int nColumnes;
        Fitxa[,] fitxes;
        int moviments = 0;
        bool completat = false;
        double percentCompletat = 0;

        //CRONÒMETRE
        DispatcherTimer tmrCronometre;
        TimeSpan tempsTrasncorregut = TimeSpan.Zero;
        DateTime tempsInicial = DateTime.Now;
        bool enMarxa = false;

        public WndPuzzle(int nFiles, int nColumnes)
        {
            InitializeComponent();
            this.nFiles = nFiles;
            this.nColumnes = nColumnes;
            fitxes = new Fitxa[nFiles, nColumnes];
            InicialitzaGraella(nFiles, nColumnes);

            //Cronòmetre
            tmrCronometre = new DispatcherTimer();
            //cada 100 milisegons iniciarà un esdeveniment
            tmrCronometre.Interval = TimeSpan.FromMilliseconds(100);
            //+= iniciem un esdeveniment, tabulador+enter
            tmrCronometre.Tick += TmrCronometre_Tick; ;
            tmrCronometre.Start();
            enMarxa = true;
        }

        private void TmrCronometre_Tick(object? sender, EventArgs e)
        {
            if (enMarxa)
            {
                tempsTrasncorregut = DateTime.Now - tempsInicial;
            }

            MostraTemps(tbTemps, tempsTrasncorregut);
        }

        private void MostraTemps(TextBlock textBlock, TimeSpan tempsTrasncorregut)
        {
            textBlock.Text = $"{tempsTrasncorregut.Hours:00}:" +
                $"{tempsTrasncorregut.Minutes:00}:" +
                $"{tempsTrasncorregut.Seconds:00}." +
                $"{tempsTrasncorregut.Milliseconds / 100:0}";
        }

        private void Pausa()
        {
            if (enMarxa)
            {
                tmrCronometre.Stop();
                grGraella.Visibility = Visibility.Hidden;
                tbJocPausa.Text = $"PAUSA\n\nMoviments: {moviments}\nCompletat: {percentCompletat}%\n{tbTemps.Text}";
                tbJocPausa.Visibility = Visibility.Visible;
                enMarxa = false;
            }
            else
            {
                tmrCronometre.Start();
                grGraella.Visibility = Visibility.Visible;
                tbJocPausa.Visibility = Visibility.Collapsed;
                enMarxa = true;
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.P)
            {
                Pausa();
            }
        }

        public void InicialitzaGraella(int nFiles, int nColumnes)
        {
            grGraella.ShowGridLines = true;
            grGraella.CreaFiles(nFiles);
            grGraella.CreaColumnes(nColumnes);

            PosarBotons(grGraella);
        }

        private void PosarBotons(Grid graella)
        {
            int[] numeros = DesordenarNumeros();
            int count = 0;
            if (!EsSoluble(numeros))
            {
                FerSoluble(numeros);
            }

            for (int i = 0; i < nFiles; i++)
            {
                for (int y = 0; y < nColumnes; y++)
                {
                    Fitxa fitxa = new Fitxa();
                    Grid.SetRow(fitxa, i);
                    Grid.SetColumn(fitxa, y);
                    fitxa.Fila = i;
                    fitxa.Columna = y;
                    fitxa.FontSize = 40;
                    fitxa.Click += Boto_Click;
                    fitxes[i, y] = fitxa;
                    graella.Children.Add(fitxa);

                    if (i == fitxes.GetLength(0) - 1 && y == fitxes.GetLength(1) - 1)
                    {
                        fitxa.Numero = 0;
                        fitxa.Content = fitxa.Numero;
                        fitxa.Visibility = Visibility.Hidden;
                        fitxes[i, y] = fitxa;
                    }
                    else
                    {
                        fitxa.Numero = numeros[count];
                        fitxa.Content = fitxa.Numero;
                        count++;
                    }
                }
            }
            Pintar();
        }

        private int[] DesordenarNumeros()
        {
            Random r = new Random();
            int num;
            int[] numeros = new int[nFiles * nColumnes - 1];
            int i = 0;

            while (i < numeros.Length)
            {
                num = r.Next(1, numeros.Length + 1);
                if (!numeros.Contains(num))
                {
                    numeros[i] = num;
                    i++;
                }
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

            if (fitxa.Columna + 1 < fitxes.GetLength(1) && fitxes[fitxa.Fila, fitxa.Columna + 1].Numero == 0)
            {
                fitxa.Numero = fitxes[fitxa.Fila, fitxa.Columna + 1].Numero;
                fitxes[fitxa.Fila, fitxa.Columna + 1].Numero = tmp;
                fitxa.Content = fitxa.Numero;
                fitxa.Visibility = Visibility.Hidden;
                fitxes[fitxa.Fila, fitxa.Columna + 1].Content = fitxes[fitxa.Fila, fitxa.Columna + 1].Numero;
                fitxes[fitxa.Fila, fitxa.Columna + 1].Visibility = Visibility.Visible;
            }

            else if (fitxa.Columna - 1 >= 0 && fitxes[fitxa.Fila, fitxa.Columna - 1].Numero == 0)
            {
                fitxa.Numero = fitxes[fitxa.Fila, fitxa.Columna - 1].Numero;
                fitxes[fitxa.Fila, fitxa.Columna - 1].Numero = tmp;
                fitxa.Content = fitxa.Numero;
                fitxa.Visibility = Visibility.Hidden;
                fitxes[fitxa.Fila, fitxa.Columna - 1].Content = fitxes[fitxa.Fila, fitxa.Columna - 1].Numero;
                fitxes[fitxa.Fila, fitxa.Columna - 1].Visibility = Visibility.Visible;
            }

            else if (fitxa.Fila + 1 < fitxes.GetLength(0) && fitxes[fitxa.Fila + 1, fitxa.Columna].Numero == 0)
            {
                fitxa.Numero = fitxes[fitxa.Fila + 1, fitxa.Columna].Numero;
                fitxes[fitxa.Fila + 1, fitxa.Columna].Numero = tmp;
                fitxa.Content = fitxa.Numero;
                fitxa.Visibility = Visibility.Hidden;
                fitxes[fitxa.Fila + 1, fitxa.Columna].Content = fitxes[fitxa.Fila + 1, fitxa.Columna].Numero;
                fitxes[fitxa.Fila + 1, fitxa.Columna].Visibility = Visibility.Visible;
            }

            else if (fitxa.Fila - 1 >= 0 && fitxes[fitxa.Fila - 1, fitxa.Columna].Numero == 0)
            {
                fitxa.Numero = fitxes[fitxa.Fila - 1, fitxa.Columna].Numero;
                fitxes[fitxa.Fila - 1, fitxa.Columna].Numero = tmp;
                fitxa.Content = fitxa.Numero;
                fitxa.Visibility = Visibility.Hidden;
                fitxes[fitxa.Fila - 1, fitxa.Columna].Content = fitxes[fitxa.Fila - 1, fitxa.Columna].Numero;
                fitxes[fitxa.Fila - 1, fitxa.Columna].Visibility = Visibility.Visible;
            }

            moviments++;
            tbMoviments.Text = "Moviments: " + moviments.ToString();
            Pintar();
        }
        public void Pintar()
        {
            int numCorrectes = 0;
            int j = 1;
            for (int i = 0; i < fitxes.GetLength(0); i++)
            {
                for (int y = 0; y < fitxes.GetLength(1); y++)
                {
                    if (fitxes[i, y].Numero == j)
                    {
                        fitxes[i, y].Background = Brushes.Green;
                        numCorrectes++;
                    }
                    else
                    {
                        fitxes[i, y].Background = Brushes.Red;
                    }
                    j++;
                }
            }
            percentCompletat = ((double)numCorrectes / ((nFiles * nColumnes) - 1)) * 100.0;
            tbCompletat.Text = $"{percentCompletat}% Completat";
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
