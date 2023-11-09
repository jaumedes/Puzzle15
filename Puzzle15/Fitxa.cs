using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Puzzle15
{
    public class Fitxa: Button
    {
        int numero;
        int fila;
        int columna;
        bool esCorrecte;
        int numDesitjat;
        bool esBuida;

        public Fitxa(int numero, int fila, int columna, bool esBuida)
        {
            Viewbox viewbox = new Viewbox();
            Content = viewbox;
            TextBlock textBlock = new TextBlock();
            textBlock.Text = numero.ToString();
            viewbox.Child = textBlock;
            this.FontSize = 40;
            this.numero = numero;
            this.Content = numero;
            this.fila = fila;
            this.columna = columna;
            this.esBuida = esBuida;
            this.esCorrecte = numDesitjat == numero ? true : false;
        }

        public int Numero { get { return numero; } set { numero = value; } }

        public int Columna { get { return columna; } set { columna = value; } }
        
        public int Fila { get { return fila; } set { fila = value; } }

        public bool EsCorrecte {  get { return esCorrecte; } set { esCorrecte = value; } }

        public bool EsBuida { get { return esBuida; } set { esBuida = value; } }

        public int NumDesitjat { get { return  numDesitjat; } set { numDesitjat = value; } }

        public override string ToString()
        {
            return Numero.ToString();
        }

    }
}
