using System.Windows.Controls;

namespace GraphicEditor.View.UserControls
{
    /// <summary>
    /// Interaction logic for LayersView.xaml
    /// </summary>
    public partial class LayersView : UserControl
    {
        public LayersView()
        {
            InitializeComponent();
            ViewModel.ListBox = LayersList;
        }
    }
}
