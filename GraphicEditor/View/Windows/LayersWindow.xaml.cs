using GraphicEditor.Model;
using System;
using System.Collections.Generic;
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

namespace GraphicEditor.View.Windows
{
    /// <summary>
    /// Interaction logic for LayersWindow.xaml
    /// </summary>
    public partial class LayersWindow : Window
    {
        public LayersWindow()
        {
            InitializeComponent();
            LayersList.SelectionChanged += ViewModel.LayersSelectionChanged;
        }

        public ListBox Layers
        {
            get { return LayersList; }
            set { LayersList = value; }
        }
    }
}
