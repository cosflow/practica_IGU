// Updated by XamlIntelliSenseFileGenerator 06/02/2024 0:28:41
#pragma checksum "..\..\CDPactómetro.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "62DFDA795D4D0161E1078987A289530D21A8B5522954A743E882B29AE3EFC929"
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using pactometro;


namespace pactometro
{


    /// <summary>
    /// CDPactómetro
    /// </summary>
    public partial class CDPactómetro : System.Windows.Window, System.Windows.Markup.IComponentConnector
    {

#line default
#line hidden


#line 23 "..\..\CDPactómetro.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_AñadirResultado;

#line default
#line hidden


#line 24 "..\..\CDPactómetro.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock resultadosAñadidos_Title;

#line default
#line hidden


#line 26 "..\..\CDPactómetro.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_EliminarResultado;

#line default
#line hidden


#line 27 "..\..\CDPactómetro.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView tablaResultados;

#line default
#line hidden


#line 35 "..\..\CDPactómetro.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView tablaResultadosAñadidos;

#line default
#line hidden


#line 43 "..\..\CDPactómetro.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txt_Mayoría;

#line default
#line hidden


#line 44 "..\..\CDPactómetro.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txt_ResultadosRestantes;

#line default
#line hidden

        private bool _contentLoaded;

        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (_contentLoaded)
            {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/pactometro;component/cdpact%c3%b3metro.xaml", System.UriKind.Relative);

#line 1 "..\..\CDPactómetro.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);

#line default
#line hidden
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.nombreElección = ((System.Windows.Controls.TextBlock)(target));
                    return;
                case 2:
                    this.btn_AñadirResultado = ((System.Windows.Controls.Button)(target));

#line 23 "..\..\CDPactómetro.xaml"
                    this.btn_AñadirResultado.Click += new System.Windows.RoutedEventHandler(this.btn_AñadirResultado_Click);

#line default
#line hidden
                    return;
                case 3:
                    this.resultadosAñadidos_Title = ((System.Windows.Controls.TextBlock)(target));
                    return;
                case 4:
                    this.btn_EliminarResultado = ((System.Windows.Controls.Button)(target));

#line 26 "..\..\CDPactómetro.xaml"
                    this.btn_EliminarResultado.Click += new System.Windows.RoutedEventHandler(this.btn_EliminarResultado_Click);

#line default
#line hidden
                    return;
                case 5:
                    this.tablaResultados = ((System.Windows.Controls.ListView)(target));

#line 27 "..\..\CDPactómetro.xaml"
                    this.tablaResultados.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.tablaResultados_SelectionChanged);

#line default
#line hidden
                    return;
                case 6:
                    this.tablaResultadosAñadidos = ((System.Windows.Controls.ListView)(target));

#line 35 "..\..\CDPactómetro.xaml"
                    this.tablaResultadosAñadidos.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.tablaResultadosAñadidos_SelectionChanged);

#line default
#line hidden
                    return;
                case 7:
                    this.txt_Mayoría = ((System.Windows.Controls.TextBlock)(target));
                    return;
                case 8:
                    this.txt_ResultadosRestantes = ((System.Windows.Controls.TextBlock)(target));
                    return;
            }
            this._contentLoaded = true;
        }
    }
}

