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
    /// <summary>
    /// Lógica de interacción para Window1.xaml
    /// </summary>
    public partial class CDPactómetro : Window
    {

        Eleccion eleccionSeleccionada;
        List<Resultado> resultadosAñadidos;
        public CDPactómetro(Eleccion e)
        {
            InitializeComponent();
            eleccionSeleccionada = e;
            tablaResultados.ItemsSource = eleccionSeleccionada.Results;
            tablaResultadosAñadidos.ItemsSource = resultadosAñadidos;
            nombreElección.Text = e.Título;
        } 
    }
}
