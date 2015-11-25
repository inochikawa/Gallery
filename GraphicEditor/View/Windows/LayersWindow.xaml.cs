using System.Windows;
using System.Windows.Controls;

namespace GraphicEditor.View.Windows
{
    /// <summary>
    /// Interaction logic for LayersWindow.
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
