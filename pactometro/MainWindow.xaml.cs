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
        List<Eleccion> elecciones = new List<Eleccion>();
        public MainWindow()
        {
            InitializeComponent();
            datos = new DatosElecciones();
            elecciones = datos.getElecciones();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
        }

        private void visualizarResultados(Eleccion e)
        {

            if (e != null)
            {
                int ancho = 10;
                List<Resultado> resultados = e.Results;

                int tam = resultados.Count;

                Point[] puntosX1 = new Point[tam];
                Point[] puntosY1 = new Point[tam];

                Point[] puntosX2 = new Point[tam];
                Point[] puntosY2 = new Point[tam];


                for (int i = 0; i < tam; i++)
                {
                    puntosX1[i].X = lienzo.ActualWidth * (i) / tam;
                    puntosX1[i].Y = lienzo.ActualHeight;
                    puntosY1[i].X = puntosX1[i].X;
                    puntosY1[i].Y = (puntosX1[i].Y - resultados[i].Escaños);



                    puntosX2[i].X = puntosX1[i].X + ancho;
                    puntosX2[i].Y = puntosX1[i].Y;
                    puntosY2[i].X = puntosY1[i].X + ancho;
                    puntosY2[i].Y = puntosY1[i].Y;

                    TextBlock part = new TextBlock();
                    part.Text = resultados[i].Partido;

                    Canvas.SetLeft(part, puntosX1[i].X);
                    Canvas.SetTop(part, puntosX1[i].Y);

                    Polyline p = new Polyline();
                    p.Points.Add(puntosX1[i]); p.Points.Add(puntosX2[i]); p.Points.Add(puntosY2[i]); p.Points.Add(puntosY1[i]);
                    SolidColorBrush pincel = new SolidColorBrush(getColor(resultados[i].Partido));
                    p.Fill = pincel;
                    
                    lienzo.Children.Add(p);
                    lienzo.Children.Add(part);
                }
            }
        }

        private Color getColor(String partido)
        {
            switch (partido)
            {
                case "PP": return Colors.Blue;
                case "PSOE": return Colors.Red;
                case "PODEMOS": return Colors.Purple;
                case "VOX": return Colors.LightGreen;
                case "SUMAR": return Colors.Pink;
                case "ERC": return Colors.Yellow;
                case "JUNTS": return Colors.SpringGreen;
                case "EH_BILDU": return Colors.Cyan;
                case "EAJ_PNV": return Colors.DarkGreen;
                case "CS": return Colors.Orange;
                case "CCA": return Colors.Gold;
                case "OTROS": return Colors.Black;
                default:
                    byte[] rgb = new byte[3];
                    Random random = new Random();
                    random.NextBytes(rgb);

                    return Color.FromRgb(rgb[0], rgb[1], rgb[2]);
            }
            
        }

        private void MenuItem_Opciones_Click(object sender, RoutedEventArgs e)
        {

            visualizarResultados(elecciones[0]);
        }

        //private void Elemento_MouseEnter(object sender, MouseEventArgs e)
        //{
        //    // Se ejecuta cuando el ratón entra en el elemento
        //    Point p = e.GetPosition(lienzo);
        //    TextBox infoPart = new TextBox();
        //    infoPart.Text = "";
        //}

        //private void Canvas_SizeChanged(object sender, SizeChangedEventArgs e)
        //{
        //    Canvas canvas = (Canvas)sender;
        //    SizeChangedEventArgs canvas_Changed_Args = e;

        //    if (canvas_Changed_Args.PreviousSize.Width == 0) return;

        //    double pre_altura = canvas_Changed_Args.PreviousSize.Height;
        //    double pre_ancho = canvas_Changed_Args.PreviousSize.Width;

        //    double post_altura = canvas_Changed_Args.NewSize.Height;
        //    double post_ancho = canvas_Changed_Args.NewSize.Width;

        //    double escalaAlt = post_altura / pre_altura;
        //    double escalaAnch = post_ancho / pre_ancho;

        //    foreach (FrameworkElement elemento in canvas.Children)
        //    {
        //        double preX = Canvas.GetLeft(elemento);
        //        double preY = Canvas.GetLeft(elemento);

        //        Canvas.SetLeft(elemento, preX * escalaAnch);
        //        Canvas.SetTop(elemento, preY * escalaAlt);

        //        elemento.Width = elemento.Width* escalaAnch;
        //        elemento.Height = elemento.Height* escalaAlt;
        //    }
        //}
    }
}