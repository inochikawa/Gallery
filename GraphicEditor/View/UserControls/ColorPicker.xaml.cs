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
using GraphicEditor.ViewModel;

namespace GraphicEditor.View.UserControls
{
    /// <summary>
    /// Interaction logic for ColorPicker.xaml
    /// </summary>
    public partial class ColorPicker : UserControl
    {
        public ColorPicker()
        {
            InitializeComponent();
            ColorPickerViewModel = new ColorPickerViewModel(f_colorPaletteImage, PickerEllipse);
            DataContext = ColorPickerViewModel;
            f_colorPaletteImage.MouseLeftButtonDown += ColorPickerViewModel.ColorPaletteMouseLeftButtonDown;
            f_colorPaletteImage.MouseMove += ColorPickerViewModel.ColorPaletteMouseMove;
            f_slider.ValueChanged += ColorPickerViewModel.AlphaSliderValueChanged;
        }

        public ColorPickerViewModel ColorPickerViewModel
        {
            get;
            set;
        }
    }
}
