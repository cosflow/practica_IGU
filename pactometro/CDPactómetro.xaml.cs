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
    
    public class CambioSeleccionResultadoEventArgs : EventArgs
    {
        public Resultado resultadoSeleccionado { get; set; }

        public CambioSeleccionResultadoEventArgs(Resultado r)
        {
            resultadoSeleccionado = r;
        }
    }
    public partial class CDPactómetro : Window
    {
        Resultado rSeleccionado = null;
        int mayoría;
        ObservableCollection<Resultado> resultados;
        ObservableCollection<Resultado> resultadosAñadidos;
        public event EventHandler<CambioSeleccionResultadoEventArgs> CambioSeleccion;
        public CDPactómetro(ObservableCollection<Resultado> resultadosEleccion, ObservableCollection<Resultado> resultadosFormados, int m)
        {
            InitializeComponent();
            resultados = resultadosEleccion;
            mayoría = m;
            tablaResultados.ItemsSource = resultados;
            resultadosAñadidos = resultadosFormados;
            tablaResultadosAñadidos.ItemsSource = resultadosAñadidos;
            txt_Mayoría.Text += mayoría;
            txt_ResultadosRestantes.Text += mayoría;
        }
        void OnCambioSeleccion(CambioSeleccionResultadoEventArgs r)
        {
            if (CambioSeleccion != null) CambioSeleccion(this, r);
        }
        private void tablaResultados_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Resultado resultadoSelect = (Resultado)tablaResultados.SelectedItem;
            if(resultadoSelect != null)
            {
                CambioSeleccionResultadoEventArgs misargs = new CambioSeleccionResultadoEventArgs(resultadoSelect);
                OnCambioSeleccion(misargs);
                rSeleccionado = resultadoSelect;
                btn_AñadirResultado.IsEnabled = true;
            }
            else
            {
                btn_AñadirResultado.IsEnabled = false;
            }
        }
        private void tablaResultadosAñadidos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Resultado resultadoSelect = (Resultado)tablaResultadosAñadidos.SelectedItem;
            if (resultadoSelect != null)
            {
                CambioSeleccionResultadoEventArgs misargs = new CambioSeleccionResultadoEventArgs(resultadoSelect);
                OnCambioSeleccion(misargs);
                rSeleccionado = resultadoSelect;
                btn_EliminarResultado.IsEnabled = true;
            }
            else
            {
                btn_EliminarResultado.IsEnabled = false;
            }
        }
        private void btn_AñadirResultado_Click(object sender, RoutedEventArgs e)
        {
            if(rSeleccionado != null)
            {
                if (!resultadosAñadidos.Contains(rSeleccionado))
                {
                    resultados.Remove(rSeleccionado);
                    resultadosAñadidos.Add(rSeleccionado);
                    int restantes = mayoría;
                    foreach (Resultado r in resultadosAñadidos)
                    {
                        restantes -= r.escaños;
                        if(restantes <= 0)
                        {
                            restantes = 0;
                            MessageBox.Show("MAYORÍA ALCANZADA!");
                        }
                    }
                    txt_ResultadosRestantes.Text = "Escaños restantes para alcanzar la mayoría:\n\t"+restantes;
                    tablaResultados.ItemsSource = resultados;
                    tablaResultadosAñadidos.ItemsSource = resultadosAñadidos;
                }
                else MessageBox.Show("ERROR. Resultado ya añadido!");
            }
        }
        private void btn_EliminarResultado_Click(object sender, RoutedEventArgs e)
        {
            if(rSeleccionado != null)
            {
                resultadosAñadidos.Remove(rSeleccionado);
                resultados.Add(rSeleccionado);
                int restantes = mayoría;
                foreach (Resultado r in resultadosAñadidos)
                {
                    restantes -= r.escaños;
                    if (restantes <= 0)
                    {
                        restantes = 0;
                        MessageBox.Show("MAYORÍA ALCANZADA!");
                    }
                }
                txt_ResultadosRestantes.Text = "Escaños restantes para alcanzar la mayoría:\n\t" + restantes;
                tablaResultados.ItemsSource = resultados;
                tablaResultadosAñadidos.ItemsSource = resultadosAñadidos;
            }
        }
    }
}
