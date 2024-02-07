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
    public class CambioSeleccionEleccionEventArgs : EventArgs
    {
        public Eleccion eleccionSeleccionada { get; set; }

        public CambioSeleccionEleccionEventArgs(Eleccion e)
        {
            eleccionSeleccionada = e;
        }
    }
    public partial class CDTablas : Window
    {
        CRUD_Elecciones CRUD = null;
        public event EventHandler<CambioSeleccionEleccionEventArgs> CambioSeleccion;
        ObservableCollection<Eleccion> listaElecciones;
        ObservableCollection<Resultado> listaResultados = new ObservableCollection<Resultado>();
        public CDTablas(ObservableCollection<Eleccion> l)
        {
            InitializeComponent();
            listaElecciones = l;
            listaElecciones.CollectionChanged += ListaElecciones_CollectionChanged;
            listaResultados.Clear();
            tablaElecciones.Items.Clear();
            tablaResultados.Items.Clear();
            tablaElecciones.ItemsSource = listaElecciones;
            tablaResultados.ItemsSource = listaResultados;
        }

        private void ListaElecciones_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            tablaElecciones.ItemsSource = null;
            tablaResultados.ItemsSource = null;
            tablaElecciones.Items.Clear();
            tablaResultados.Items.Clear();
            tablaElecciones.ItemsSource = listaElecciones;
            tablaResultados.ItemsSource = listaResultados;
        }

        void OnCambioSeleccion(CambioSeleccionEleccionEventArgs e)
        {
            if (CambioSeleccion != null) CambioSeleccion(this, e);
        }

        private void tablaElecciones_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tablaElecciones.SelectedItem != null)
            {
                Eleccion eleccionSelect = (Eleccion)tablaElecciones.SelectedItem;
                CambioSeleccionEleccionEventArgs misargs = new CambioSeleccionEleccionEventArgs(eleccionSelect);
                OnCambioSeleccion(misargs);
                listaResultados = eleccionSelect.Results;
                tablaResultados.ItemsSource = listaResultados;
                btn_D_eleccion.IsEnabled = true;
                btn_U_eleccion.IsEnabled = true;
            }

            else
            {
                btn_U_eleccion.IsEnabled = false;
                btn_D_eleccion.IsEnabled = false;
            }
        }
        private void CRUD_Closed(object sender, EventArgs e)
        {
            CRUD = null;
        }

        private void C_eleccion_Click(object sender, RoutedEventArgs e)
        {
            if (CRUD == null)
            {
                CRUD = new CRUD_Elecciones(listaElecciones, null);
                CRUD.Closed += CRUD_Closed;
            }
            CRUD.Show();
        }

        private void U_eleccion_Click(object sender, RoutedEventArgs e)
        {
            if (CRUD == null)
            {
                Eleccion eleccionSelect = (Eleccion)tablaElecciones.SelectedItem;
                CRUD = new CRUD_Elecciones(listaElecciones, eleccionSelect);
                CRUD.Closed += CRUD_Closed;
            }
            CRUD.Show();
        }

        private void D_eleccion_Click(object sender, RoutedEventArgs e)
        {
            Eleccion eleccionSelect = (Eleccion)tablaElecciones.SelectedItem;
            if (eleccionSelect == null)
            {
                return;
            }
            int i = 0;
            foreach(Eleccion el in listaElecciones)
            {
                if (el.Equals(eleccionSelect))
                {
                    break;
                }
                else i++;
            }
            listaElecciones.RemoveAt(i);
        }
    }
}