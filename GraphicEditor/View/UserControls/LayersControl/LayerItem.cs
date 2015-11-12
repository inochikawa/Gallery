using GraphicEditor.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace GraphicEditor.View.UserControls.LayersControl
{
    public class LayerItem : Control, INotifyPropertyChanged
    {
        private CheckBox f_checkBox;
        private bool f_isChecked;
        private BitmapImage f_preview;
        private string f_name;

        public LayerItem() { }

        public string LayerName
        {
            get
            {
                return f_name;
            }

            set
            {
                f_name = value;
            }
        }

        public BitmapImage Preview
        {
            get
            {
                return f_preview;
            }

            set
            {
                f_preview = value;
                NotifyPropertyChanged("Preview");
            }
        }

        public bool IsChecked
        {
            get
            {
                return f_isChecked;
            }

            set
            {
                f_isChecked = value;
            }
        }

        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        public delegate void LayerUpdateDelegate();

        public event LayerUpdateDelegate OnCheckBoxChecked;

        public event LayerUpdateDelegate OnCheckBoxUnchecked;

        public static readonly DependencyProperty IsSelectedProperty =
          DependencyProperty.Register("IsSelected", typeof(bool),
                                      typeof(LayerItem),
                                      new FrameworkPropertyMetadata(false));

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            f_checkBox = Template.FindName("PART_CheckBox", this) as CheckBox;
            if (f_checkBox == null)
                throw new NullReferenceException("CheckBox is missing!");

            f_checkBox.Unchecked += checkBox_Unchecked;
            f_checkBox.Checked += checkBox_Checked;
        }

        public void LayerMouseLeftButtonUp(Layer layer)
        {
            if(IsChecked)
                Preview = layer.Preview();
        }

        private void checkBox_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            f_isChecked = true;
            OnCheckBoxChecked();
        }

        private void checkBox_Unchecked(object sender, System.Windows.RoutedEventArgs e)
        {
            f_isChecked = false;
            OnCheckBoxUnchecked();
        }
    }
}
