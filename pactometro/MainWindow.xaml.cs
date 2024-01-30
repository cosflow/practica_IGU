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
    public partial class MainWindow : Window
    {
        CDTablas cdTablas = null;
        Eleccion eleccionSeleccionada = null;
        ObservableCollection<Eleccion> elecciones;
        int modo;
        public MainWindow()
        {
            InitializeComponent();
            elecciones = new ObservableCollection<Eleccion>();
            DatosElecciones datos = new DatosElecciones(elecciones);
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
                double inicio = lienzo.ActualWidth/10;
                double ancho = (lienzo.ActualWidth -inicio)/(tam* 3);

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

                List<String> repetidos = new List<String>();
                List<String> fechasRep = new List<String>();
                for (int i = 0; i < tam; i++)
                {
                    if (i == 0)
                    {
                        puntos[i].X = inicio;
                    }

                    puntos[i].X = ((lienzo.ActualWidth-inicio) * (i) /(tam+2)) + puntos[0].X;
                    puntos[i].Y = lienzo.ActualHeight-lienzo.Margin.Top;

                    TextBlock part = new TextBlock();
                    Rectangle rect = new Rectangle();
                    rect.Width = ancho;
                    rect.Height = alturas[i];
                    ScaleTransform scaleTransform = new ScaleTransform(1, -1);
                    int numReps = 0;

                    rect.RenderTransform = scaleTransform;
                    if (repetidos.Count == 0)
                    {
                        part.Text = resultados[i].Partido;
                        Canvas.SetLeft(part, puntos[i].X);
                        Canvas.SetTop(part, puntos[i].Y);
                        lienzo.Children.Add(part);
                        repetidos.Add(resultados[i].Partido);
                    }
                    else
                    {
                        if (!repetidos.Contains(resultados[i].partido))
                        {
                            numReps = 0;
                            part.Text = resultados[i].Partido;
                            Canvas.SetLeft(part, puntos[i].X);
                            Canvas.SetTop(part, puntos[i].Y);
                            lienzo.Children.Add(part);
                            repetidos.Add(resultados[i].Partido);
                        }
                        else
                        {
                            numReps++;
                            puntos[i].X = puntos[i-1].X +3*rect.Width/2;
                        }
                    }

                    Canvas.SetLeft(rect, puntos[i].X);
                    Canvas.SetTop(rect, puntos[i].Y);

                    if (numReps == 0)
                    {

                        SolidColorBrush pincel = new SolidColorBrush(getColor(resultados[i].Partido));
                        rect.Fill = pincel;
                    }
                    else {

                        Color c = getColor(resultados[i].Partido);
                        c.A = (byte)(255 - 2*255/elecciones.Count*numReps);
                        SolidColorBrush pincelArgb = new SolidColorBrush(c);
                        rect.Fill = pincelArgb;
                    }
                    
                    lienzo.Children.Add(rect);
                    
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
                case "UPL": return Colors.Cornsilk;
                case "XAV": return Colors.Crimson;
                case "SY": return Colors.Azure;
                default:
                    byte[] rgb = new byte[3];
                    Random random = new Random();
                    random.NextBytes(rgb);

                    return Color.FromRgb(rgb[0], rgb[1], rgb[2]);
            }
            
        }

        private void lienzo_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (eleccionSeleccionada != null)
            {
                lienzo.Children.Clear();
                if(modo == 0)
                {
                    visualizarResultados(eleccionSeleccionada);
                }
                if(modo == 1)
                {
                    obtenerHistorico(eleccionSeleccionada);
                }
            }
        }
        private void Conf_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (cdTablas == null)
            {
                cdTablas = new CDTablas(elecciones);
                cdTablas.Closed += cdTablas_Closed;
                cdTablas.CambioSeleccion += Cdsec_CambioSeleccion;
            }
            cdTablas.Show();
        }
        void cdTablas_Closed(object sender, EventArgs e)
        {
            cdTablas = null; 
        }

        private void Cdsec_CambioSeleccion(object sender, CambioSeleccionEventArgs e)
        {
            lienzo.Children.Clear();
            eleccionSeleccionada = e.eleccionSeleccionada;
            if(modo == 0) visualizarResultados(eleccionSeleccionada);
            if (modo == 1) obtenerHistorico(eleccionSeleccionada);
        }

        private void Historico_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (eleccionSeleccionada != null)
            {
                obtenerHistorico(eleccionSeleccionada);
            }
            else
            {
                MessageBox.Show("ERROR\nSeleccione una elección en la ventana de Opciones->Registro, por favor");
            }
        }

        private void obtenerHistorico(Eleccion eleccion)
        {
            if(eleccion == null)
            {
                MessageBox.Show("ERROR\nSeleccione una elección en la ventana de  Opciones->Registro, por favor");
                return;
            }

            modo = 1;
            lienzo.Children.Clear();

            string[] parts = new string[eleccion.Results.Count];
            for (int i = 0; i < eleccion.Results.Count; i++)
            {
                parts[i] = eleccion.Results[i].Partido;
            }

            List<Resultado> resultados = new List<Resultado>();
            List<String> fechasRep = new List<string>();
            string[] partes = eleccion.Fecha.Split('/');

            foreach (Eleccion e in elecciones)
            {
                if (e.Parlamento.Equals(eleccion.Parlamento))
                {
                    string[] partes2 = e.Fecha.Split('/');
                    if (int.Parse(partes2[2]) < int.Parse(partes[2]))
                    {
                        fechasRep.Add(e.Fecha);
                    }
                    if (int.Parse(partes2[2]) == int.Parse(partes[2]))
                    {
                        if (int.Parse(partes2[1]) < int.Parse(partes[1]))
                        {
                            fechasRep.Add(e.Fecha);
                        }
                        if (int.Parse(partes2[1]) == int.Parse(partes[1]))
                            {
                            if (int.Parse(partes2[0]) <= int.Parse(partes[0]))
                            {
                                fechasRep.Add(e.Fecha);
                            }
                        }
                    }
                }
            }

            for (int i = 0; i < parts.Length; i++)
            {
                foreach (Eleccion el in elecciones)
                {
                    if (fechasRep.Contains(el.Fecha))
                    {
                        foreach (Resultado resultado in el.Results)
                        {
                            if (resultado.Partido == parts[i]) resultados.Add(resultado);
                        }
                    }
                }
            }

            Eleccion nuevaEleccion = new Eleccion(resultados, eleccion.Parlamento, eleccion.Tipo, eleccion.Fecha);
            visualizarResultados(nuevaEleccion);
            mostrarLeyenda(fechasRep);
        }
        private void mostrarLeyenda(List<String> fechas)
        {
            Rectangle fondo = new Rectangle();
            fondo.Height = 50;
            fondo.Width = 100;
            fondo.Fill = Brushes.Ivory;

            Canvas.SetTop(fondo, 0);
            Canvas.SetRight(fondo, 0);

            lienzo.Children.Add(fondo);

            for(int i = 0; i < fechas.Count; i++)
            {
                Color c = new Color();
                c = Color.FromArgb((byte)(255 / (i+1)), 128, 128, 128);

                Rectangle r = new Rectangle();
                r.Height = fondo.Height / 7;
                r.Width = fondo.Width *2/ 6;
                SolidColorBrush pincel = new SolidColorBrush(c);
                r.Fill = pincel;

                Label fecha = new Label();
                fecha.Content = fechas[i];
                fecha.FontSize = 8;

                Canvas.SetRight(r,7*fondo.Width/12);
                Canvas.SetTop(r, 50/(2-i)-r.Height-5);

                Canvas.SetRight(fecha,fondo.Width / 12);
                Canvas.SetTop(fecha, Canvas.GetTop(r)-5);

                lienzo.Children.Add(r);
                lienzo.Children.Add(fecha);
            }
        }

        private void Normal_MenuItem_Click(object sender, EventArgs e)
        {
            if (eleccionSeleccionada != null)
            {
                lienzo.Children.Clear();
                visualizarResultados(eleccionSeleccionada);
                modo = 0;
            }
            else MessageBox.Show("ERROR\nSeleccione una elección en la ventana de  Opciones->Registro, por favor");
        }

        private void Pactómetro_MenuItem_Click(object sender, EventArgs e) 
        {
            if(eleccionSeleccionada == null)
            {
                return;
            }
            List<Resultado> resultados = new List<Resultado>();
            double[] alturas = obtenerAlturasPorcentuales(resultados);
            foreach(Resultado resultado in resultados)
            {
                if (resultado.Escaños >= eleccionSeleccionada.Mayoría)
                {

                }
            }
        }

        private void Eleccion_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Eleccion eleccion = (Eleccion)sender;
            switch (e.PropertyName)
            {
                case "Results":
                    break;
                case "Tipo":
                    break;
                case "Fecha":
                    break;
                case "Parlamento":
                    break;
            }

        }
    }
}