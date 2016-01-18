using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace GraphicEditor.ViewModel
{
    public class StatusBarViewModel : INotifyPropertyChanged
    {
        private string f_size;
        private string f_pos;
        private string f_color;
        private string f_tool;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Size
        {
            get { return f_size; }
            set
            {
                f_size = value;
                NotifyPropertyChanged("Size");
            }
        }

        public string Pos
        {
            get { return f_pos; }
            set
            {
                f_pos = value;
                NotifyPropertyChanged("Pos");
            }
        }
        
        public string Tool
        {
            get { return f_tool; }
            set
            {
                f_tool = value;
                NotifyPropertyChanged("Tool");
            }
        }

        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        
        public void UpdateTool(string name)
        {
            Tool = name + "\t";
        }

        public void UpdatePosition(Point position)
        {
            Pos = position.X + "x" + position.Y + "\t";
        }

        public void UpdateSize(int width, int height)
        {
            Size = width + "x" + height + "\t";
        }
    }
}
