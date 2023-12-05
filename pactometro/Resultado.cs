using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace pactometro
{
    internal class Resultado
    {

        public event PropertyChangedEventHandler PropertyChanged;

        string partido;
        int escaños;
        

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

        public Resultado(string p, int e)
        {
            Partido = p;
            Escaños = e;
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
