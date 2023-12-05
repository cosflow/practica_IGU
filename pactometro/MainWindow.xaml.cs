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
            int ancho = 10;
            List<Eleccion> elecciones = datos.getElecciones();
            List<Resultado> resultados = elecciones[0].Results;

            int tam = resultados.Count;
            
            Point[] puntosX1 = new Point[tam];
            Point[] puntosY1 = new Point[tam];

            Point[] puntosX2 = new Point[tam];
            Point[] puntosY2 = new Point[tam];


            for (int i = 0; i < tam; i++)
            {
                puntosX1[i].X = lienzo.ActualWidth * (i + 1) / tam;
                puntosX1[i].Y = lienzo.ActualHeight;
                puntosY1[i].X = puntosX1[i].X;
                puntosY1[i].Y = (puntosX1[i].Y - resultados[i].Escaños);

                puntosX2[i].X = puntosX1[i].X + ancho;
                puntosX2[i].Y = puntosX1[i].Y;
                puntosY2[i].X = puntosY1[i].X + ancho;
                puntosY2[i].Y = puntosY1[i].Y;



                Polyline p = new Polyline();
                p.Points.Add(puntosX1[i]); p.Points.Add(puntosX2[i]);p.Points.Add(puntosY2[i]);p.Points.Add(puntosY1[i]);
                p.Fill = Brushes.Red;
                lienzo.Children.Add(p);
            }
        }
    }
}