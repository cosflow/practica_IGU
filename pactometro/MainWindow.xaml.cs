using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
        CDPactómetro cdPactometro = null;

        Eleccion eleccionSeleccionada = null;
        ObservableCollection<Eleccion> elecciones;

        ObservableCollection<Resultado> copiaResultados = null;
        ObservableCollection<Resultado> resultadosAñadidos = null;

        int modo = 0;
        public MainWindow()
        {
            InitializeComponent();
            elecciones = new ObservableCollection<Eleccion>();

            DatosElecciones datos = new DatosElecciones(elecciones);
            elecciones.CollectionChanged += CollectionChangedHandler;
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
                switch (modo)
                {
                    case 0:
                        tituloEleccion.Text += "\nVista: Normal";
                        break;
                    case 1:
                        tituloEleccion.Text += "\nVista: Histórico";
                        break;
                    case 2:
                        tituloEleccion.Text += "\nVista: Pactómetro";
                        break;
                }
                tituloEleccion.FontSize = 20;
                if (setAlturasPorcentuales(e) == -1 ) return;


                int tam = e.Results.Count;
                double inicio = lienzo.ActualWidth/10;
                double ancho = (lienzo.ActualWidth -inicio)/(tam* 3);

                Point[] puntos = new Point[tam];

                double mayor = 0;
                int indice = 0;
                int j = 0;
                foreach (Resultado r in e.Results)
                {
                    if (r.Altura > mayor)
                    {
                        mayor = r.Altura;
                        indice = j;
                    }
                    j++;
                }

                List<String> repetidos = new List<String>();
                List<String> fechasRep = new List<String>();
                Resultado[] rs = e.Results.ToArray();
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
                    
                    rect.Height = rs[i].Altura;
                    ScaleTransform rotar180 = new ScaleTransform(1, -1);
                    int numReps = 0;

                    rect.RenderTransform = rotar180;
                    if (repetidos.Count == 0)
                    {
                        part.Text = e.Results[i].Partido;
                        Canvas.SetLeft(part, puntos[i].X);
                        Canvas.SetTop(part, puntos[i].Y);
                        lienzo.Children.Add(part);
                        repetidos.Add(e.Results[i].Partido);
                    }
                    else
                    {
                        if (!repetidos.Contains(e.Results[i].partido))
                        {
                            numReps = 0;
                            part.Text = e.Results[i].Partido;
                            Canvas.SetLeft(part, puntos[i].X);
                            Canvas.SetTop(part, puntos[i].Y);
                            lienzo.Children.Add(part);
                            repetidos.Add(e.Results[i].Partido);
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

                        SolidColorBrush pincel = new SolidColorBrush(getColor(e.Results[i].Partido));
                        rect.Fill = pincel;
                    }
                    else {

                        Color c = getColor(e.Results[i].Partido);
                        c.A = (byte)(255 - 2*255/elecciones.Count*numReps);
                        SolidColorBrush pincelArgb = new SolidColorBrush(c);
                        rect.Fill = pincelArgb;
                    }
                    
                    lienzo.Children.Add(rect);
                    
                    if (i == indice)
                    {
                        generarEjes(e.Results[i].Escaños,rect.Height);
                    }
                }
            }
        }
        private void generarEjes(int maxResult,double altRect)
        {
            int division = maxResult/6;
            double conversion = altRect/ (double) maxResult;

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
        private int setAlturasPorcentuales(Eleccion e)
        {
            if (e == null) return -1;
            ObservableCollection<Resultado> resultados = e.Results;
            double alturaMax = (lienzo.ActualHeight - lienzo.Margin.Top)*.9;
            double mayor = 0;

            if (modo == 2)
            {
                int totalC = 0, totalA = 0, total;

                if(copiaResultados != null && copiaResultados.Count > 0)
                {
                    foreach(Resultado r in copiaResultados)
                    {
                        totalC += r.Escaños;
                    }

                }
                if (resultadosAñadidos != null && resultadosAñadidos.Count > 0)
                {
                    foreach (Resultado r in resultadosAñadidos)
                    {
                        totalA += r.Escaños;
                    }

                }
                if (totalA > totalC) total = totalA; else total = totalC;

                if (resultadosAñadidos.Count > 0 && resultadosAñadidos!=null )
                {
                    foreach(Resultado r in resultadosAñadidos)
                    {
                        r.Altura = alturaMax * r.Escaños / total;
                        if(r.Altura < 0)
                        {
                            return -1;
                        }
                    }

                }
                if(copiaResultados.Count > 0 && copiaResultados != null)
                {
                    foreach(Resultado r in copiaResultados )
                    {
                        r.Altura = alturaMax * r.Escaños / total;
                        if(r.Altura < 0)
                        {
                            return -1;
                        }
                    }
                }

                return total;
            }

            foreach (Resultado r in resultados)
            {
                if (r.Escaños > mayor)
                {
                    mayor = r.Escaños;
                }
            }

            foreach (Resultado r in resultados)
            {
                r.Altura = r.Escaños * alturaMax / mayor;
                if (r.Altura <= 0)
                {
                    return -1;
                }
            }
            
            return 0;
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
                    byte r,g,b;
                    Random random = new Random();
                    r = (byte)random.Next(256);
                    g = (byte)random.Next(256);
                    b = (byte)random.Next(256);

                    return Color.FromRgb(r,g,b);
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

            ObservableCollection<Resultado> resultados = new ObservableCollection<Resultado>();
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
                Canvas.SetTop(r, 50/((2-i)+1)-r.Height-5);

                Canvas.SetRight(fecha,fondo.Width / 12);
                Canvas.SetTop(fecha, Canvas.GetTop(r)-5);

                lienzo.Children.Add(r);
                lienzo.Children.Add(fecha);
            }
        }
        private void visualizarResultadosPactómetro(Eleccion e)
        {
            lienzo.Children.Clear();
            if (eleccionSeleccionada == null)
            {
                MessageBox.Show("ERROR\nSeleccione una elección en la ventana de  Opciones->Registro, por favor");
                return;
            }
            if (e == null) return;

            switch (e.Tipo)
            {
                case "Generales":
                    tituloEleccion.Text = e.Tipo + " " + e.Fecha;
                    break;
                default:
                    tituloEleccion.Text = e.Tipo + " " + e.Parlamento + " " + e.Fecha;
                    break;
            }
            tituloEleccion.FontSize = 20;

            bool hayMayoría = false;
            int mayor = setAlturasPorcentuales(e);
            if (mayor < 0) return;
            if(mayor > 0) generarLíneaMayoría(mayor, e.Mayoría);
            ScaleTransform rotar180 = new ScaleTransform(1, -1);

            if (copiaResultados == null) return;

            Resultado aux = null;

            foreach (Resultado r in copiaResultados)
            {
                if (r.Escaños >= e.Mayoría)
                {
                    aux = r;
                    hayMayoría = true;
                    break;
                }
            }

            if (hayMayoría)
            {
                Rectangle rect = new Rectangle();

                SolidColorBrush brocha = new SolidColorBrush(getColor(aux.Partido));
                rect.RenderTransform = rotar180;
                rect.Fill = brocha;
                rect.Width = lienzo.ActualWidth / 3;
                rect.Height = aux.Altura;

                Point p = new Point();
                p.X = lienzo.ActualWidth / 2 - rect.Width / 2;
                p.Y = (lienzo.ActualHeight - lienzo.Margin.Top)*.9;
                Canvas.SetLeft(rect, p.X);

                Canvas.SetTop(rect, p.Y);
                lienzo.Children.Add(rect);

                Label etiqueta = new Label();
                etiqueta.Content = aux.Partido + " - " + aux.Escaños;
                etiqueta.FontSize = 10;
                etiqueta.Foreground = Brushes.Black;
                Canvas.SetLeft(etiqueta, p.X + rect.Width / 2);
                Canvas.SetTop(etiqueta, p.Y - rect.Height);
                lienzo.Children.Add(etiqueta);

                btn_GestionarPactos.IsEnabled = false;
                return;
            }

            if (copiaResultados == null && resultadosAñadidos == null) return;


            Point[] puntosMont = new Point[copiaResultados.Count];
            Point[] puntosMay = new Point[resultadosAñadidos.Count];

            Point inicioMayoría = new Point();
            Point inicioMont = new Point();

            inicioMayoría.X = lienzo.ActualWidth / 3;
            inicioMayoría.Y = (lienzo.ActualHeight - lienzo.Margin.Top);

            inicioMont.X = lienzo.ActualWidth * 2 / 3;
            inicioMont.Y = inicioMayoría.Y;


            if (copiaResultados.Count > 0) 
            {
                Resultado[] arrayR = copiaResultados.ToArray();
                double altura_anterior = 0;
                for (int i = 0; i < copiaResultados.Count; i++)
                {
                    if (i == 0)
                    {
                        puntosMont[i] = inicioMont;
                    }
                    else
                    {
                        puntosMont[i].X = puntosMont[i - 1].X;
                        puntosMont[i].Y = puntosMont[i - 1].Y - altura_anterior;
                    }

                    Rectangle rect = new Rectangle();
                    rect.Width = lienzo.ActualWidth / 5;
                    rect.Height = arrayR[i].Altura;
                    rect.RenderTransform = rotar180;
                    SolidColorBrush brocha = new SolidColorBrush(getColor(arrayR[i].Partido));
                    rect.Fill = brocha;

                    Canvas.SetLeft(rect, puntosMont[i].X - rect.Width / 2);
                    Canvas.SetTop(rect, puntosMont[i].Y);

                    lienzo.Children.Add(rect);
                    altura_anterior = rect.Height;

                    if (arrayR[i].Escaños > 3)
                    {
                        Label etiqueta = new Label();
                        etiqueta.Content = arrayR[i].Partido + " - " + arrayR[i].Escaños;
                        etiqueta.FontSize = 10;
                        etiqueta.Foreground = Brushes.Black;
                        Canvas.SetLeft(etiqueta, puntosMont[i].X + rect.Width / 2);
                        Canvas.SetTop(etiqueta, puntosMont[i].Y - rect.Height);
                        lienzo.Children.Add(etiqueta);
                    }
                }
                int total = 0;
                foreach(Resultado r in copiaResultados)
                {
                    total += r.Escaños;
                }

                Label etiquetaTotalMont = new Label();
                etiquetaTotalMont.Content = total;
                etiquetaTotalMont.FontSize = 10;
                etiquetaTotalMont.Foreground = Brushes.Black;

                Canvas.SetLeft(etiquetaTotalMont, inicioMont.X);
                Canvas.SetTop(etiquetaTotalMont, inicioMont.Y);

                lienzo.Children.Add(etiquetaTotalMont);
            }
            if (resultadosAñadidos.Count > 0)
            {
                Resultado[] arrayR = resultadosAñadidos.ToArray();
                double altura_anterior = 0;
                for (int i = 0; i < resultadosAñadidos.Count; i++)
                {
                    if (i == 0)
                    {
                        puntosMay[i] = inicioMayoría;
                    }
                    else
                    {
                        puntosMay[i].X = puntosMay[i - 1].X;
                        puntosMay[i].Y = puntosMay[i - 1].Y - altura_anterior;
                    }

                    Rectangle rect = new Rectangle();
                    rect.Width = lienzo.ActualWidth / 5;
                    rect.Height = arrayR[i].Altura;
                    rect.RenderTransform = rotar180;
                    SolidColorBrush brocha = new SolidColorBrush(getColor(arrayR[i].Partido));
                    rect.Fill = brocha;

                    Canvas.SetLeft(rect, puntosMay[i].X - rect.Width / 2);
                    Canvas.SetTop(rect, puntosMay[i].Y);

                    lienzo.Children.Add(rect);
                    altura_anterior = rect.Height;

                    if (arrayR[i].Escaños > 3)
                    {
                        Label etiqueta = new Label();
                        etiqueta.Content = arrayR[i].Partido + " - " + arrayR[i].Escaños;
                        etiqueta.FontSize = 10;
                        etiqueta.Foreground = Brushes.Black;
                        Canvas.SetLeft(etiqueta, puntosMay[i].X + rect.Width / 2);
                        Canvas.SetTop(etiqueta, puntosMay[i].Y - rect.Height);
                        lienzo.Children.Add(etiqueta);
                    }
                }
                int total = 0;
                foreach (Resultado r in resultadosAñadidos)
                {
                    total += r.Escaños;
                }

                Label etiquetaTotalMay = new Label();
                etiquetaTotalMay.Content = total;
                etiquetaTotalMay.FontSize = 10;
                etiquetaTotalMay.Foreground = Brushes.Black;

                Canvas.SetLeft(etiquetaTotalMay, inicioMayoría.X);
                Canvas.SetTop(etiquetaTotalMay, inicioMayoría.Y);

                lienzo.Children.Add(etiquetaTotalMay);
            }
        }
        private void generarLíneaMayoría(int t, int m)
        {

            double alturaMax = (lienzo.ActualHeight - lienzo.Margin.Top);

            int total = t;

            ScaleTransform rotar180 = new ScaleTransform(1, -1);
            double alturaMayoría = m*alturaMax/total;

            Rectangle linea_Mayor = new Rectangle();
            SolidColorBrush pincel = new SolidColorBrush();
            pincel.Color = Colors.Black;
            linea_Mayor.Fill = pincel;
            linea_Mayor.RenderTransform = rotar180;

            linea_Mayor.Width = lienzo.ActualWidth;
            linea_Mayor.Height = 2;

            Canvas.SetLeft(linea_Mayor, 0);
            Canvas.SetBottom(linea_Mayor, alturaMayoría);

            lienzo.Children.Add(linea_Mayor);

            Label etiquetaMay = new Label();
            etiquetaMay.Content = "Mayoría = "+m;
            etiquetaMay.FontSize = 10;
            etiquetaMay.Foreground = Brushes.Red;

            Canvas.SetRight(etiquetaMay, 0);
            Canvas.SetBottom(etiquetaMay, alturaMayoría-10-linea_Mayor.Height);

            lienzo.Children.Add(etiquetaMay);
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
                if(modo == 2)
                {
                    visualizarResultadosPactómetro(eleccionSeleccionada);
                }
            }
        }
        private void Normal_MenuItem_Click(object sender, EventArgs e)
        {
            if (eleccionSeleccionada != null)
            {
                lienzo.Children.Clear();
                modo = 0;
                visualizarResultados(eleccionSeleccionada);
                btn_GestionarPactos.IsEnabled = false;
            }
            else MessageBox.Show("ERROR\nSeleccione una elección en la ventana de  Opciones->Registro, por favor");
        }
        private void Historico_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            
            if (eleccionSeleccionada != null)
            {
                btn_GestionarPactos.IsEnabled = false;
                modo = 1;
                obtenerHistorico(eleccionSeleccionada);
            }
            else
            {
                MessageBox.Show("ERROR\nSeleccione una elección en la ventana de Opciones->Registro, por favor");
            }
        }
        private void Pactómetro_MenuItem_Click(object sender, EventArgs e) 
        {
            if(eleccionSeleccionada == null)
            {
                MessageBox.Show("ERROR\nSeleccione una elección en la ventana de  Opciones->Registro, por favor");
                return;
            }
            modo = 2;
            btn_GestionarPactos.IsEnabled = true;
            iniciarPactómetro();
            visualizarResultadosPactómetro(eleccionSeleccionada);
        }
        private void iniciarPactómetro()
        {
            if (eleccionSeleccionada == null)
            {
                MessageBox.Show("ERROR\nSeleccione una elección en la ventana de  Opciones->Registro, por favor");
                return;
            }
            lienzo.Children.Clear();
            copiaResultados = new ObservableCollection<Resultado>(eleccionSeleccionada.Results);
            resultadosAñadidos = new ObservableCollection<Resultado>();
            copiaResultados.CollectionChanged += CollectionChangedHandler;
            resultadosAñadidos.CollectionChanged += CollectionChangedHandler;
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
        private void btn_GestionarPactos_Click(object sender, RoutedEventArgs e)
        {
            if (cdPactometro == null)
            {
                cdPactometro = new CDPactómetro(copiaResultados, resultadosAñadidos, eleccionSeleccionada.Mayoría);
                cdPactometro.Closed += cdPactómetro_Closed;
            }
            cdPactometro.Show();
        }
        private void CollectionChangedHandler(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (eleccionSeleccionada == null) return;
            lienzo.Children.Clear();
            switch (modo)
            {
                case 0:
                    visualizarResultados(eleccionSeleccionada); 
                    break;
                case 1:
                    obtenerHistorico(eleccionSeleccionada); 
                    break;
                case 2:
                    visualizarResultadosPactómetro(eleccionSeleccionada);
                    break;
                default:
                    break;
            }
        }
        void cdTablas_Closed(object sender, EventArgs e)
        {
            cdTablas = null; 
        }
        private void Cdsec_CambioSeleccion(object sender, CambioSeleccionEleccionEventArgs e)
        {
            lienzo.Children.Clear();
            eleccionSeleccionada = e.eleccionSeleccionada;
            if(modo == 0) visualizarResultados(eleccionSeleccionada);
            if (modo == 1) obtenerHistorico(eleccionSeleccionada);
            if (modo == 2)
            {
                iniciarPactómetro();
                visualizarResultadosPactómetro(eleccionSeleccionada);
                if(cdPactometro != null)
                {
                    cdPactometro.Close();
                    cdPactometro = new CDPactómetro(copiaResultados, resultadosAñadidos, eleccionSeleccionada.Mayoría);
                    cdPactometro.Closed += cdPactómetro_Closed;
                    cdPactometro.Show();
                }
            }
        }
        void cdPactómetro_Closed(object sender, EventArgs e)
        {
            cdPactometro = null;
        }
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            if(cdPactometro != null) { cdPactometro.Close();  cdPactometro = null; }
            if (cdTablas != null) { cdTablas.Close(); cdTablas = null; }
        }
    }
}