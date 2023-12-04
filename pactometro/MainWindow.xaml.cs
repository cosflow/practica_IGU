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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace pactometro
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DatosElecciones datos; 
        public MainWindow()
        {
            InitializeComponent();
            datos = new DatosElecciones();
        }
        private void mostrarTodo_Click(object sender, RoutedEventArgs e)
        {
            List<Eleccion> elecciones = datos.ObtenerElecciones();
        }
    }
}