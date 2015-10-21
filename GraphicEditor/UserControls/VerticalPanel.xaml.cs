using System.Windows;
using System.Windows.Controls;

namespace GraphicEditor.UserControls
{
    /// <summary>
    /// Interaction logic for VerticalPanel
    /// </summary>
    public partial class VerticalPanel : UserControl
    {
        public static readonly DependencyProperty AdditionalContentProperty =
            DependencyProperty.Register("AdditionalContent", typeof(object), typeof(VerticalPanel), new PropertyMetadata(null));

        public VerticalPanel()
        {
            InitializeComponent();
        }

        public object AdditionalContent
        {
            get
            {
                return (object)GetValue(AdditionalContentProperty);
            }

            set
            {
                SetValue(AdditionalContentProperty, value);
            }
        }
    }
}
