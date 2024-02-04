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
            txt_Instrucciones.Text = "Instrucciones: \n\n" +
                    "Para AÑADIR/MODIFICAR un resultado electoral, por favor siga los siguientes pasos:\n" +
                    "\t1. Introduzca el nombre del partido y sus escaños correspondientes.\n" +
                    "\t2. Pulse el botón 'Añadir' para sobreescribir el resultado (si no existía el partido, se añade).\n" +
                    "\t3. Si necesita modificar o añadir más partidos, repita el proceso\n" +
                    "\t4. Para registrar los cambios realizados, pulse el botón guardar.\n" +
                    "En todo momento, puede modificar o eliminar un resultado. Para eliminarlo, introduzca el nombre del partido" +
                    "y ponga el número de escaños a 0.\n" +
                    "Las fechas han de estar especificadas en el formato DD/MM/AAAA.\n" +
                    "El tipo de elección debe ser especificado como 'G' (Generales) o 'A' (Autonómicas)" +
                    "\n\n---------------------------------------------\n\n";
            if (eleccion != null)
            {
                modo = 1;
                eleccionSeleccionada = eleccion;
                resultados = eleccionSeleccionada.Results;
                txtBox_Parlamento.Text = eleccionSeleccionada.Parlamento;
                if (eleccionSeleccionada.Tipo.Equals("Generales")) txtBox_Tipo.Text = "G"; else txtBox_Tipo.Text = "A";
                txtBox_Fecha.Text = eleccionSeleccionada.Fecha;
                txt_Instrucciones.Text =
                    "\n-----------MODIFICIAR UNA ELECCIÓN-----------\n\n" + txt_Instrucciones.Text;
            }
            else
            {
                txt_Instrucciones.Text = "\n-------------CREAR UNA ELECCIÓN-------------\n\n" + txt_Instrucciones.Text;
            }
        }
        private void btn_AñadePartido_Click(object sender, RoutedEventArgs e)
        {
            int escaños;
            string partido = "";

            if (txtBox_Escaños.Text == "" && txtBox_Partido.Text == "")
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
            int index = 0;
            
            foreach(Resultado res in resultados)
            {
                if (res.Partido.Equals(partido))
                {
                    if(escaños == 0)
                    {
                        eleccionSeleccionada.Results.RemoveAt(index);
                        MessageBox.Show("RESULTADO ELIMINADO:\nPartido: " + res.partido + "\nEscaños anteriores: " + res.escaños);
                        return;
                    }
                    MessageBox.Show("RESULTADO MODIFICADO:\nPartido: " + res.partido + "\nEscaños anteriores: " + res.escaños
                        + "\nNuevos escaños: " + escaños);
                    res.escaños = escaños;
                    txtBox_Escaños.Text = "";
                    txtBox_Partido.Text = "";
                    return;
                }
                index++;
            }
            
            if(escaños == 0)
            {
                MessageBox.Show("ERROR. Se ha intentado eliminar un resultado de un partido " +
                    "que no constaba previamente en las elecciones.");
            }
            

            MessageBox.Show("RESULTADO AÑADIDO:\nPartido: " + r.partido + "\nEscaños: " + r.escaños);
            txtBox_Escaños.Text = "";
            txtBox_Partido.Text = "";
            resultados.Add(r);

        }

        private void btn_Guardar_Click(object sender, RoutedEventArgs e)
        {
            string fecha;
            if (resultados.Count <= 1 && modo == 0)
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

            switch (modo)
            {
                case 0:
                    elecciones.Add(eleccion);
                    MessageBox.Show("Elección añadida con éxito.");
                    break;
                case 1:
                    eleccion.Results = eleccionSeleccionada.Results;
                    eleccionSeleccionada = eleccion;
                    MessageBox.Show("Elección modificada con éxito.");
                    this.Close();
                    break;
            }

            txtBox_Parlamento.Text = "";
            txtBox_Tipo.Text = "";
            txtBox_Fecha.Text = "";

            resultados = new List<Resultado>();
        }
    }
}
