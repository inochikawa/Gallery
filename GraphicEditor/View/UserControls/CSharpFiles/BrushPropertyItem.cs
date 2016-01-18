using System.ComponentModel;
using System.Windows.Controls;

namespace GraphicEditor.View.UserControls.CSharpFiles
{
    public class BrushPropertyItem : ListBoxItem, INotifyPropertyChanged
    {
        private double f_thicknessValue;
        private double f_softnessValue;

        public event PropertyChangedEventHandler PropertyChanged;
        
        public double ThicknessValue
        {
            get { return f_thicknessValue; }
            set
            {
                f_thicknessValue = value;
                NotifyPropertyChanged("ThicknessValue");
            }
        }
        
        /// <summary>
        /// Softness of brush in percentage
        /// </summary>
        public double SoftnessValue
        {
            get { return f_softnessValue; }
            set
            {
                f_softnessValue = value;
                NotifyPropertyChanged("SoftnessValue");
            }
        }

        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
