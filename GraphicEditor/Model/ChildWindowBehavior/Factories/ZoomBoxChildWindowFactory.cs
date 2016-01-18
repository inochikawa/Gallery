using System;
using System.Windows;
using System.Windows.Media;
using GraphicEditor.Model.ChildWindowBehavior.ChildWondows;
using GraphicEditor.Model.ChildWindowBehavior.Interfaces;
using GraphicEditor.Model.ToolBehavior;

namespace GraphicEditor.Model.ChildWindowBehavior.Factories
{
    class ZoomBoxChildWindowFactory : IChildWindowFactory
    {
        private  string Path = AppDomain.CurrentDomain.BaseDirectory + @"\Parameters\ZoomBoxChildWindow.cwd";

        public ZoomBoxChildWindowFactory()
        {
            ChildWindow = new ZoomBoxChildWindow();
            ChildWindow.Move(-20, 430);
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

            if(windowParameters == null) return;

            ChildWindow.ChildWindow.Width = windowParameters.Width;
            ChildWindow.ChildWindow.Height = windowParameters.Height;
            ((TranslateTransform)ChildWindow.ChildWindow.RenderTransform).Y = windowParameters.Y;
            ((TranslateTransform)ChildWindow.ChildWindow.RenderTransform).X = windowParameters.X;
            
            ChildWindow.ChildWindow.Visibility = windowParameters.IsVisible ? Visibility.Visible : Visibility.Hidden;
        }
    }
}
