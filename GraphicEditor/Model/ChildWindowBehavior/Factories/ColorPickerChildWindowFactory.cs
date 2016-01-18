using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using GraphicEditor.Model.ChildWindowBehavior.ChildWondows;
using GraphicEditor.Model.ChildWindowBehavior.Interfaces;
using GraphicEditor.Model.ToolBehavior;
using GraphicEditor.View.UserControls;

namespace GraphicEditor.Model.ChildWindowBehavior.Factories
{
    public class ColorPickerChildWindowFactory : IChildWindowFactory
    {
        private string Path = AppDomain.CurrentDomain.BaseDirectory + @"\Parameters\ColorPickerChildWindow.cwd";

        public ColorPickerChildWindowFactory()
        {
            ChildWindow = new ColorPickerChildWindow() { Header = "Color picker" };
            ChildWindow.Move(-20, 220);
        }

        public IChildWindow ChildWindow { get; set; }

        public GraphicContent GraphicContent { get; set; }

        public void SaveState()
        {
            WindowParameters windowParameters = new WindowParameters()
            {
                Height = (int)ChildWindow.ChildWindow.Height,
                Width = (int)ChildWindow.ChildWindow.Width,
                X = (int)((TranslateTransform)ChildWindow.ChildWindow.RenderTransform).X,
                Y = (int)((TranslateTransform)ChildWindow.ChildWindow.RenderTransform).Y,
                IsVisible = ChildWindow.ChildWindow.IsVisible
            };

            windowParameters.Save(Path);
        }

        public void LoadState()
        {
            WindowParameters windowParameters = WindowParameters.Load(Path);

            if (windowParameters == null) return;

            ChildWindow.ChildWindow.Width = windowParameters.Width;
            ChildWindow.ChildWindow.Height = windowParameters.Height;
            ((TranslateTransform)ChildWindow.ChildWindow.RenderTransform).Y = windowParameters.Y;
            ((TranslateTransform)ChildWindow.ChildWindow.RenderTransform).X = windowParameters.X;

            ChildWindow.ChildWindow.Visibility = windowParameters.IsVisible ? Visibility.Visible : Visibility.Hidden;
        }
    }
}