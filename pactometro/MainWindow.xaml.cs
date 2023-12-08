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
using System.Windows.Threading;

namespace pactometro
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CDTablas cdTablas = null;
        Eleccion eleccionSeleccionada = null;
        List<Eleccion> elecciones;

        public MainWindow()
        {
            InitializeComponent();
            elecciones = new List<Eleccion>();
            DatosElecciones datos = new DatosElecciones(elecciones);
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
                switch (e.Tipo)
                {
                    case "Generales":
                        tituloEleccion.Text = e.Tipo + " " + e.Fecha;
                        break;
                    default:
                        tituloEleccion.Text = e.Tipo+" " +e.Parlamento + " " + e.Fecha;
                        break;
                }
                tituloEleccion.FontSize = 20;
                List<Resultado> resultados = e.Results;
                double[] alturas = obtenerAlturasPorcentuales(resultados);

                int tam = resultados.Count;
                double ancho = lienzo.ActualWidth /(tam* 3);
                double inicio = lienzo.ActualWidth/tam;

                Point[] puntos = new Point[tam];

                double mayor = 0;
                int indice = 0;
                for (int j = 0; j < tam; j++)
                {
                    if (alturas[j] > mayor)
                    {
                        mayor = alturas[j];
                        indice = j;
                    }
                }

                for (int i = 0; i < tam; i++)
                {


                    if (i == 0)
                    {
                        puntos[i].X = inicio;
                    }
                    puntos[i].X = (lienzo.ActualWidth * (i) /(tam+2)) + puntos[0].X;
                    puntos[i].Y = lienzo.ActualHeight-lienzo.Margin.Top;

                    TextBlock part = new TextBlock();
                    part.Text = resultados[i].Partido;
                    
                        
                    Canvas.SetLeft(part, puntos[i].X);
                    Canvas.SetTop(part, puntos[i].Y);
                        
                    Rectangle rect = new Rectangle();

                    rect.Width = ancho;
                    rect.Height = alturas[i];
                    SolidColorBrush pincel = new SolidColorBrush(getColor(resultados[i].Partido));
                    rect.Fill = pincel;

                    ScaleTransform scaleTransform = new ScaleTransform(1, -1);
                    rect.RenderTransform = scaleTransform;

                    Canvas.SetLeft(part, puntos[i].X);
                    Canvas.SetBottom(part, lienzo.ActualHeight);

                    Canvas.SetLeft(rect, puntos[i].X);
                    Canvas.SetTop(rect, puntos[i].Y);

                    lienzo.Children.Add(rect);
                    lienzo.Children.Add(part);

                    if (i == indice)
                    {
                        generarEjes(resultados[i].Escaños,rect);
                    }
                }
                
            }
        }

        private void generarEjes(int maxResult,Rectangle rect)
        {
            int division;
            if (maxResult > 100)
            {
                while (maxResult % 20 != 0)
                {
                    maxResult++;
                }
                division = 20;
            }
            else if (maxResult > 50)
            {
                while (maxResult % 10 != 0)
                {
                    maxResult++;
                }
                division = 10;
            }
            else
            {
                while (maxResult % 5 != 0)
                {
                    maxResult++;
                }
                division = 5;
            }
            double conversion = rect.Height/ (double) maxResult;

            int alturaMax = 1;

            while(maxResult >= 0)
            {
                alturaMax =(int) (maxResult * conversion);
                Rectangle marca = new Rectangle();
                marca.Height = 1;
                marca.Width = 10;
                marca.StrokeThickness = 2;
                marca.Fill = Brushes.Red;

                Canvas.SetTop(marca, lienzo.ActualHeight-alturaMax-lienzo.Margin.Top);
                Canvas.SetLeft(marca, 0);

                lienzo.Children.Add(marca);

                Label textAltura = new Label();

                textAltura.Content = ""+maxResult;
                textAltura.FontSize = 12;
                textAltura.Foreground = Brushes.Red;
                Canvas.SetLeft(textAltura, marca.Width);
                Canvas.SetTop(textAltura, Canvas.GetTop(marca)-15);

                lienzo.Children.Add(textAltura);

                maxResult -= division;
            }
        }

        private double[] obtenerAlturasPorcentuales(List<Resultado> resultados)
        {
            int tam = resultados.Count;
            double[] alturas = new double[tam];

            double alturaMax = (lienzo.ActualHeight - lienzo.Margin.Top)*.9;

            if (resultados != null)
            {
                double mayor = 0;
                for (int j = 0; j < tam; j++ )
                {
                    if (resultados[j].Escaños > mayor)
                    {
                        mayor = resultados[j].Escaños;
                    }
                }

                for (int i = 0; i < tam; i++)
                {
                    alturas[i] = resultados[i].Escaños * alturaMax / mayor;
                }
            }
            return alturas;
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
                case "UPN": return Colors.LightGray;
                case "BNG": return Colors.Magenta;
                case "CCA": return Colors.Gold;
                case "OTROS": return Colors.Black;
                default:
                    byte[] rgb = new byte[3];
                    Random random = new Random();
                    random.NextBytes(rgb);

                    return Color.FromRgb(rgb[0], rgb[1], rgb[2]);
            }
            
        }
        private void Canvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Canvas canvas = (Canvas)sender;
            SizeChangedEventArgs canvas_Changed_Args = e;
            if (canvas == null || canvas.ActualHeight == 0 || canvas.ActualWidth ==0)
            {
                foreach(UIElement element in canvas.Children)
                {
                    canvas.Children.Remove(element);
                }
                return;
            }
            if (canvas.Children != null) 
            {
                foreach (UIElement el in canvas.Children)
                {
                    double proporcionX = Canvas.GetLeft(el) / e.PreviousSize.Width;
                    double proporcionY = Canvas.GetTop(el) / e.PreviousSize.Height;

                    Canvas.SetLeft(el, proporcionX * canvas.ActualWidth);
                    Canvas.SetTop(el, proporcionY * canvas.ActualHeight);

                    //if (el is FrameworkElement frEl)
                    //{
                    //    frEl.Width = proporcionX * frEl.ActualWidth;
                    //    frEl.Height = proporcionY * frEl.ActualHeight;
                    //}
                }
            }
        }
        private void Conf_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (cdTablas == null)
            {
                cdTablas = new CDTablas();
                cdTablas.Closed += cdsec_Closed;
                cdTablas.CambioSeleccion += Cdsec_CambioSeleccion;
            }
            cdTablas.Show();
        }
        void cdsec_Closed(object sender, EventArgs e)
        {
            cdTablas = null; 
        }

        private void Cdsec_CambioSeleccion(object sender, CambioSeleccionEventArgs e)
        {
            lienzo.Children.Clear();
            eleccionSeleccionada = e.eleccionSeleccionada;
            visualizarResultados(eleccionSeleccionada);

        }

        private void Historico_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (eleccionSeleccionada != null)
            {
                MessageBox.Show("Cargados los datos históricos sobre las elecciones " 
                    + eleccionSeleccionada.Tipo + " del día " + eleccionSeleccionada.Fecha + " en " + eleccionSeleccionada.Parlamento);
                string[] parts = new string[eleccionSeleccionada.Results.Count];
                for (int i = 0; i<eleccionSeleccionada.Results.Count; i++)
                {
                    parts[i] = eleccionSeleccionada.Results[i].Partido;
                }
                List<Resultado> resultados = new List<Resultado>();
                string parlamento = eleccionSeleccionada.Parlamento;
                for (int i = 0;i<parts.Length;i++)
                {
                    foreach(Eleccion el in elecciones)
                    {
                        if(parlamento.Equals(el.Parlamento))
                        {
                            foreach(Resultado resultado in el.Results)
                            {
                                if(resultado.Partido == parts[i] )
                                {
                                    resultados.Add(resultado);
                                }
                            }
                        }
                    }
                }
                Eleccion nuevaEleccion = new Eleccion(resultados, eleccionSeleccionada.Parlamento, eleccionSeleccionada.Tipo, eleccionSeleccionada.Fecha);
                lienzo.Children.Clear();
                visualizarResultados(nuevaEleccion);
                mostrarLeyenda();
            }
            else
            {
                MessageBox.Show("ERROR\nSeleccione una elección en la ventana de Configuración, por favor");
            }
        }

        private void mostrarLeyenda()
        {
            Rectangle fondo = new Rectangle();
            fondo.Height = (lienzo.ActualHeight - lienzo.Margin.Top)/4;
            fondo.Width = (lienzo.ActualWidth - lienzo.Margin.Right) / 4;
            fondo.Fill = Brushes.LightGray;

            Canvas.SetTop(fondo, 0);
            Canvas.SetRight(fondo, 0);

            lienzo.Children.Add(fondo);
        }

        private void Normal_MenuItem_Click(object sender, EventArgs e)
        {
            if (eleccionSeleccionada != null)
            {
                lienzo.Children.Clear();
                visualizarResultados(eleccionSeleccionada);
            }
            else MessageBox.Show("ERROR\nSeleccione una elección en la ventana de Configuración, por favor");
        }
    }
}