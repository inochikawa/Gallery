using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Combogallary
{
    internal static class LocalExtensions
    {
        public static void ForWindowFromTemplate(this object templateFrameworkElement, Action<Window> action)
        {
            Window window = ((FrameworkElement)templateFrameworkElement).TemplatedParent as Window;
            if (window != null) action(window); 
        }

        public static void ForTextBoxFromTemplate(this object templateFrameworkElement, Action<TextBox> action)
        {
            TextBox textBox = ((FrameworkElement)templateFrameworkElement).TemplatedParent as TextBox;
            action(textBox);
        }
    }

    public partial class FlatWindow
    {
        void TopBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            sender.ForWindowFromTemplate(w => w.DragMove());
        }

        void btnClose_Click(object sender, RoutedEventArgs e)
        {
            sender.ForWindowFromTemplate(w => w.Close());
        }

        void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            sender.ForWindowFromTemplate(w => w.WindowState = System.Windows.WindowState.Minimized);
        }
        
        private void ComboGalleryWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //sender.ForWindowFromTemplate(w => w.Closing -= ComboGalleryWindow_Closing);
            //sender.ForWindowFromTemplate(w => w.OnClosing());
            //e.Cancel = true;
            //var anim = new DoubleAnimation(0, (Duration)TimeSpan.FromMilliseconds(250));
            //anim.Completed += (s, _) => this.Close();
            //this.BeginAnimation(UIElement.OpacityProperty, anim);
        }
    }
}
