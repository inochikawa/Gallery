using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using GraphicEditor.Model;

namespace GraphicEditor.View.UserControls.LayersControl
{
    public class LayerItem : ListBoxItem, INotifyPropertyChanged
    {
        private CheckBox f_checkBox;
        private bool f_isChecked;
        private BitmapImage f_preview;
        private string f_name;

        public event LayerUpdateDelegate OnCheckBoxChecked;

        public event LayerUpdateDelegate OnCheckBoxUnchecked;
        
        public event PropertyChangedEventHandler PropertyChanged;

        public delegate void LayerUpdateDelegate();

        public Canvas BindedCanvas
        {
            get { return (Canvas)GetValue(CanvasProperty); }
            set
            {
                SetValue(CanvasProperty, value);
                NotifyPropertyChanged("BindedCanvas");
            }
        }

        public static readonly DependencyProperty CanvasProperty =
            DependencyProperty.Register("Canvas", typeof(Canvas), typeof(LayerItem));

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
        
        private void checkBox_Checked(object sender, RoutedEventArgs e)
        {
            f_isChecked = true;
            OnCheckBoxChecked?.Invoke();
        }

        private void checkBox_Unchecked(object sender, RoutedEventArgs e)
        {
            f_isChecked = false;
            OnCheckBoxUnchecked?.Invoke();
        }
    }
}
