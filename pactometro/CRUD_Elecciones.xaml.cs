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
    /// Lógica de interacción para CRUD_Elecciones.xaml
    /// </summary>
    public partial class CRUD_Elecciones : Window
    {
        public CRUD_Elecciones()
        {
            ObservableCollection<Eleccion> elecciones = new ObservableCollection<Eleccion>();
            InitializeComponent();
            DatosElecciones datos = new DatosElecciones(elecciones);
        }
        private void Create_Eleccion(object sender, EventArgs e)
        {
            
        }

        private void ScrollViewer_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {

        }
    }
}
