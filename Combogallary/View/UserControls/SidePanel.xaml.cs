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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Combogallary.Themes
{
    /// <summary>
    /// Interaction logic for SidePanel.xaml
    /// </summary>
    public partial class SidePanel : UserControl
    {
        public SidePanel()
        {
            InitializeComponent();
        }

        public System.Windows.Thickness TitleMargin
        {
            get { return borderTitle.Margin; }
            set { borderTitle.Margin = value; }
        }

        public string Title
        {
            get { return txtTitle.Text; }
            set { txtTitle.Text = value; }
        }

        public double TitleTranslateTransformY
        {
            get { return borderTitleTranslateTransform.Y; }
            set { borderTitleTranslateTransform.Y = value; }
        }

        public double TitleRectHeight
        {
            get { return borderTitle.Width; }
            set { borderTitle.Width = value; }
        }
        public double TitleRectWidth
        {
            get { return borderTitle.Height; }
            set { borderTitle.Height = value; }
        }

        public double MainPanelWidth
        {
            get { return borderMainPanel.Width; }
            set { borderMainPanel.Width = value; }
        }

        public double SideEffectWigth()
        {
            return borderMainPanel.Width;
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

       public static readonly DependencyProperty AdditionalContentProperty = DependencyProperty.Register("AdditionalContent", typeof(object), typeof(SidePanel), new PropertyMetadata(null));
    }
}
