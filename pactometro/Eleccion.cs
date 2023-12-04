using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pactometro
{
    internal class Eleccion
    {

        public PropertyChangedEventHandler PropertyChanged;

        List<Resultado> results;
        string parlamento;
        string tipo;
        string fecha;

        public List<Resultado> Results
        {
            get { return results; }
            set { results = value; }
        }
        public string Parlamento
        {
            get { return parlamento; }
            set { parlamento = value; }
        }
        public string Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }

        public string Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }
        public Eleccion(List<Resultado> results, string parlamento, string tipo, string fecha)
        {
            Results = results;
            Parlamento = parlamento;
            Tipo = tipo;
            Fecha = fecha;
        }

        void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
