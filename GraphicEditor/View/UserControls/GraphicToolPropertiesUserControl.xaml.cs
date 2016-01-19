using System.Windows;
using System.Windows.Controls;

namespace GraphicEditor.View.UserControls
{
    /// <summary>
    /// Interaction logic for GraphicToolParameters.xaml
    /// </summary>
    public partial class GraphicToolPropertiesUserControl : UserControl
    {
        public GraphicToolPropertiesUserControl()
        {
            InitializeComponent();
            thicknessSlider.ValueChanged += viewModel.ThicknessSliderValueChanged;
            softnessSlider.ValueChanged += viewModel.SoftnessSliderValueChanged;
            TemplatesListBox.SelectionChanged += viewModel.TemplateSelectionChanged;
        }
    }
}
