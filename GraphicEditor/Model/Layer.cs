using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Xml;

namespace GraphicEditor.Model
{
    public class Layer : Canvas
    {
        public Layer()
        {
            IsActive = true;
            IsSelected = true;
            Background = Brushes.Transparent;
            VerticalAlignment = VerticalAlignment.Stretch;
            HorizontalAlignment = HorizontalAlignment.Stretch;
        }
        
        public Layer(string name)
            : this()
        {
            string[] n = name.Split(' ');
            Name = n[1] + n[n.Length - 1];
            LayerName = name;
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool IsActive { get; set; }

        public bool IsSelected { get; set; }

        public string LayerName { get; set; }
        
        public Layer Clone()
        {
            var xaml = XamlWriter.Save(this);
            var xamlString = new StringReader(xaml);
            var xmlTextReader = new XmlTextReader(xamlString);
            var deepCopyObject = XamlReader.Load(xmlTextReader) as Layer;
            if (deepCopyObject != null)
            {
                deepCopyObject.LayerName += " clone";
                IsSelected = false;
                return deepCopyObject;
            }
            return null;
        }
        
        public void Unactivate()
        {
            IsActive = false;
            Opacity = 0;
        }

        public void Activate()
        {
            IsActive = true;
            Opacity = 1;
        }

        public void Select(object sender, RoutedEventArgs e)
        {
            IsSelected = true;
            IsHitTestVisible = true;
        }

        public void Unselect(object sender, RoutedEventArgs e)
        {
            IsSelected = false;
            IsHitTestVisible = false;
        }
    }
}
