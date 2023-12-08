﻿using System;
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
    public class CambioSeleccionEventArgs : EventArgs
    {
        public Eleccion eleccionSeleccionada { get; set; }

        public CambioSeleccionEventArgs(Eleccion e)
        {
            eleccionSeleccionada = e;
        }
    }
    public partial class Secundaria : Window
    {
        DatosElecciones datos;
        public event EventHandler<CambioSeleccionEventArgs> CambioSeleccion;
        List<Eleccion> listaElecciones;
        List<Resultado> listaResultados;
        public Secundaria()
        {
            InitializeComponent();
            listaElecciones = new List<Eleccion>();
            listaResultados = new List<Resultado>();
            listaElecciones.Clear();
            tablaElecciones.Items.Clear();
            tablaElecciones.ItemsSource = listaElecciones;
            listaResultados.Clear();
            tablaResultados.Items.Clear();
            tablaResultados.ItemsSource = listaResultados;
            datos = new DatosElecciones(listaElecciones);
        }

        void OnCambioSeleccion(CambioSeleccionEventArgs e)
        {
            if (CambioSeleccion != null) CambioSeleccion(this, e);
        }

        private void tablaElecciones_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tablaElecciones.SelectedItem != null)
            {
                Eleccion eleccionSelect = (Eleccion)tablaElecciones.SelectedItem;
                CambioSeleccionEventArgs misargs = new CambioSeleccionEventArgs(eleccionSelect);
                OnCambioSeleccion(misargs);
                tablaResultados.ItemsSource = null;
                listaResultados.Clear();
                tablaResultados.ItemsSource = listaResultados;
                listaResultados.AddRange(eleccionSelect.Results);
            }
        }
    }
}