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
        int columna;
        int fila;
        public Fitxa()
        {
            //Viewbox viewbox = new Viewbox();
            //Content = viewbox;
        }

        public int Numero { get { return numero; } set { numero = value; } }
        public int Columna { get { return columna; } set { columna = value; } }
        public int Fila { get { return fila; } set { fila = value; } }

        //public string Text
        //{
        //    get { return ((Viewbox)Content).Child.ToString(); }
        //    set { }
        //}
        public override string ToString()
        {
            return Numero.ToString();
        }

    }
}
