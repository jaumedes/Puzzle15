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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Puzzle15
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnNFilesColumnes_Click(object sender, RoutedEventArgs e)
        {
            int nFiles, nColumnes;

            if (int.TryParse(txtNFiles.Text, out nFiles) && int.TryParse(txtNColumnes.Text, out nColumnes))
            {
                if (nFiles >= 3 && nFiles <= 8 && nColumnes >= 3 && nColumnes <= 8)
                {
                    WndPuzzle wndPuzzle1 = new WndPuzzle(nFiles, nColumnes);
                    wndPuzzle1.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Introdueix entre 3 i 8 files + entre 3 i 8 columnes.");
                }
            }
        }
    }
}
