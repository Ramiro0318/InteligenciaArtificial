using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LaberintoCSharp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Nodo inicial = new Nodo() { X=1, Y=1 };
            Nodo final = new Nodo() { X=14,Y=15 };
            Algoritmo algoritmo = new Algoritmo(final, inicial);
            var ruta = algoritmo.Buscar();
        }
    }
}