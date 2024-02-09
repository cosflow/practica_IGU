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
    public class Eleccion : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        ObservableCollection<Resultado> results;
        string parlamento;
        string tipo;
        string fecha;
        int totalEscaños;
        int mayoría;
        string titulo;
        string partidos;
        string escaños;

        public ObservableCollection<Resultado> Results
        {
            get { return results; }
            set { results = value; OnPropertyChanged("Results"); }
        }
        public string Parlamento
        {
            get { return parlamento; }
            set { parlamento = value; OnPropertyChanged("Parlamento"); }
        }
        public string Fecha
        {
            get { return fecha; }
            set { fecha = value; OnPropertyChanged("Fecha"); }
        }

        public string Tipo
        {
            get { return tipo; }
            set { tipo = value; OnPropertyChanged("Tipo"); }
        }
        public int TotalEscaños
        {
            get { return totalEscaños; }
            set { totalEscaños = value; OnPropertyChanged("TotalEscaños"); }
        }

        public int Mayoría
        {
            get { return mayoría; }
            set { mayoría = value; OnPropertyChanged("Mayoría"); }
        }

        public string Título
        {
            get { return titulo; }
            set { titulo = value; OnPropertyChanged("Título"); }
        }

        public string Partidos
        {
            get { return partidos; }
            set { partidos = value; OnPropertyChanged("Partidos"); }
        }

        public string Escaños
        {
            get { return escaños; }
            set { escaños = value; OnPropertyChanged("Escaños"); }
        }

        public Eleccion(ObservableCollection<Resultado> results, string parlamento, string tipo, string fecha)
        {
            Results = results;
            Parlamento = parlamento;
            Tipo = tipo;
            Fecha = fecha;
            foreach(Resultado resultado in results)
            {
                totalEscaños += resultado.Escaños;
                partidos += resultado.Partido + '\n';
                escaños += resultado.Escaños + '\n';
            }
            
            double aprox;
            if(totalEscaños%2 == 0)
            {
                mayoría = (totalEscaños / 2)+1;
            }
            else
            {
                aprox = (double)(totalEscaños / 2.0);
                mayoría =(int)(aprox+0.5);
            }
            if (tipo == "Generales")  titulo = "Elecciones " + tipo + " " + fecha + " "; 
            else titulo = "Elecciones "+ tipo + " " +parlamento + " " + fecha;
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
