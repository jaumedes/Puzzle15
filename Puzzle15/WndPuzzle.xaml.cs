using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;
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
        Graella grGraella;

        //CRONÒMETRE
        DispatcherTimer tmrCronometre;
        TimeSpan tempsTrasncorregut = TimeSpan.Zero;
        DateTime tempsInicial = DateTime.Now;
        bool enMarxa = false;

        public WndPuzzle(int nFiles, int nColumnes)
        {
            InitializeComponent();
            Graella graella = new Graella(nFiles, nColumnes);
            dpPrincipal.Children.Add(graella);
            DockPanel.SetDock(graella, Dock.Bottom);
            this.grGraella = graella;
            
            //Cronòmetre
            tmrCronometre = new DispatcherTimer();
            //cada 100 milisegons iniciarà un esdeveniment
            tmrCronometre.Interval = TimeSpan.FromMilliseconds(100);
            //+= iniciem un esdeveniment, tabulador+enter
            tmrCronometre.Tick += TmrCronometre_Tick; ;
            tmrCronometre.Start();
            enMarxa = true;

            if (grGraella.Completat == true)
            {
                Completat();
            }
        }

        private void TmrCronometre_Tick(object? sender, EventArgs e)
        {
            if (enMarxa)
            {
                tempsTrasncorregut = DateTime.Now - tempsInicial;
            }
            tbMoviments.Text = $"{grGraella.Moviments} Moviments";
            tbCompletat.Text = $"{grGraella.PercentCompletat}% Completat";
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
                tbJocPausa.Text = $"PAUSA\n\nMoviments: {grGraella.Moviments}\nCompletat: {grGraella.PercentCompletat}%\n{tbTemps.Text}";
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

        private void Completat()
        {
            tmrCronometre.Stop();
            grGraella.Visibility = Visibility.Hidden;
            tbFinalitzat.Text = $"JOC COMPLETAT!\n\nMoviments: {grGraella.Moviments}\nCompletat: {grGraella.PercentCompletat}%\n{tbTemps.Text} ";
            tbJocPausa.Visibility = Visibility.Visible;            
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.P)
            {
                Pausa();
            }
        }     
    }
}
