using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace pactometro
{
    public class Resultado
    {

        public event PropertyChangedEventHandler PropertyChanged;

        public string partido;
        public int escaños;
        public double altura;

        public string Partido
        {
            get { return partido; }
            set { partido = value; OnPropertyChanged("Partido"); }
        }

        public int Escaños
        {
            get { return escaños; }
            set { escaños = value; OnPropertyChanged("Escaños"); }
        }

        public double Altura
        {
            get { return altura; }
            set { altura = value; OnPropertyChanged("Altura"); }
        }

        public Resultado(string p, int e)
        {
            Partido = p;
            Escaños = e;
            Altura = 0;
        }

        void OnPropertyChanged(string propertyName)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
