using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using GraphicEditor.Model;
using GraphicEditor.Model.ToolBehavior.ToolProperties;
using GraphicEditor.View.UserControls.CSharpFiles;

namespace GraphicEditor.ViewModel
{
    public class GraphicToolPropertiesViewModel : INotifyPropertyChanged, IViewModel
    {
        private ObservableCollection<BrushPropertyItem> f_templateBrushProperties;
        private readonly List<GraphicToolProperties> f_subscribes;
        private double f_thicknessValue;
        private double f_softnessValue;

        public GraphicToolPropertiesViewModel()
        {
            TemplateBrushProperties = new ObservableCollection<BrushPropertyItem>();
            f_subscribes = new List<GraphicToolProperties>();

            for (int i = 2; i < 74; i++)
            {
                if (i % 2 == 0)
                    TemplateBrushProperties.Add(new BrushPropertyItem()
                    {
                        ThicknessValue = i,
                        SoftnessValue = .5
                    });
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<BrushPropertyItem> TemplateBrushProperties
        {
            get { return f_templateBrushProperties; }
            set
            {
                f_templateBrushProperties = value;
            }
        }

        public double ThicknessValue
        {
            get { return f_thicknessValue; }
            set
            {
                f_thicknessValue = value;
                NotifyPropertyChanged("ThicknessValue");
            }
        }

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

        public void ThicknessSliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ThicknessValue = e.NewValue;
            Notify();
        }

        public void SoftnessSliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SoftnessValue = e.NewValue / 100.0;
            Notify();
        }

        public void Subscribe(GraphicToolProperties observer)
        {
            f_subscribes.Add(observer);
        }

        public void Unsubscribe(GraphicToolProperties observer)
        {
            if (f_subscribes.Contains(observer))
                f_subscribes.Remove(observer);
        }

        public void Notify()
        {
            IToolProperties properties = new GraphicToolProperties() { Color = null, Softness = SoftnessValue, Thickness = ThicknessValue };

            foreach (GraphicToolProperties graphicToolProperty in f_subscribes)
                graphicToolProperty.UpdateProperties(properties);
        }

        public void TemplateSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ThicknessValue = ((BrushPropertyItem)((ListBox)sender).SelectedItem).ThicknessValue;
            // SoftnessValue = ((BrushPropertyItem)((ListBox)sender).SelectedItem).SoftnessValue;
            Notify();
        }
    }
}