using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace pactometro
{
    public partial class CRUD_Elecciones : Window
    {

        ObservableCollection<Eleccion> elecciones;
        List<Resultado> resultados;
        Eleccion eleccionSeleccionada = null;
        int modo = 0;

        public CRUD_Elecciones(ObservableCollection<Eleccion> e, Eleccion eleccion)
        {
            elecciones = e;
            InitializeComponent();
            resultados = new List<Resultado>();
            if (eleccionSeleccionada != null)
            {
                modo = 1;
                eleccionSeleccionada = eleccion;
            }
        }
        
        private void btn_AñadePartido_Click(object sender, RoutedEventArgs e)
        {
            int escaños = 0;
            string partido = "";

            if(txtBox_Escaños.Text == "" && txtBox_Partido.Text == "")
            {

                MessageBox.Show("ERROR. Introduzca el partido y sus escaños");
                return;
                
            }

            if (txtBox_Escaños.Text == "")
            {
                MessageBox.Show("ERROR. Introduzca el número de escaños");
                return;
            }
            escaños = int.Parse(txtBox_Escaños.Text);

            if (txtBox_Partido.Text == "")
            {
                MessageBox.Show("ERROR. Introduzca el nombre del partido");
                return;
            }
            partido = txtBox_Partido.Text;

            Resultado r = new Resultado(partido, escaños);
            MessageBox.Show("RESULTADO AÑADIDO:\nPartido: " + r.partido + "\nEscaños: " + r.escaños);
            txtBox_Escaños.Text = "";
            txtBox_Partido.Text = "";
            resultados.Add(r);
        }

        private void btn_Guardar_Click(object sender, RoutedEventArgs e)
        {
            string fecha;
            if (resultados.Count <= 1)
            {
                MessageBox.Show("ERROR. Introduzca como mínimo 2 resultados, vivimos en una democracia (supuestamente).");
                return;
            }

            if (txtBox_Parlamento.Text == "")
            {
                MessageBox.Show("ERROR. Introduzca el nombre del parlamento");
                return;
            }

            if(txtBox_Fecha.Text == "")
            {
                MessageBox.Show("ERROR. Introduzca la fecha de las elecciones");
                return;
            }

            if(txtBox_Tipo.Text == "")
            {
                MessageBox.Show("ERROR. Introduzca el tipo de elecciones --> GENERALES (G) ó AUTONÓMICAS (A)");
                return;
            }

            if(txtBox_Tipo.Text != "G" && txtBox_Tipo.Text != "A")
            {
                MessageBox.Show("ERROR. Introduzca el tipo de elecciones correctamente --> GENERALES (G) ó AUTONÓMICAS (A)");
                return;
            }

            fecha = txtBox_Fecha.Text;
            if (fecha.Contains('/'))
            {
                string[] fechas = fecha.Split('/');
                if(fechas.Length == 3 && int.Parse(fechas[0]) > 0 && int.Parse(fechas[0]) <= 31 
                    && int.Parse(fechas[1]) > 0  && int.Parse(fechas[1]) <= 12
                    && int.Parse(fechas[2]) > 0)
                {
                    fecha = fechas[0] + '/' + fechas[1] + '/' + fechas[2];
                }
                else
                {
                    MessageBox.Show("ERROR. Formato fecha obligatorio --> DD/MM/AAAA)");
                    return;
                }
            }
            else
            {
                MessageBox.Show("ERROR. Formato fecha obligatorio --> DD/MM/AAAA)");
                return;
            }

            string tipo;

            if (txtBox_Tipo.Text == "A") tipo = "Autonómicas";
            else tipo = "Generales";

            Eleccion eleccion = new Eleccion(resultados, txtBox_Parlamento.Text, tipo, fecha);
            elecciones.Add(eleccion);

            MessageBox.Show("Elección añadida con éxito.");

            txtBox_Parlamento.Text = "";
            txtBox_Tipo.Text = "";
            txtBox_Fecha.Text = "";

            resultados = new List<Resultado>();
        }
    }
}
