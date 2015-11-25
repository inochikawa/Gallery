using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;

namespace GraphicEditor.View.Styles
{
    internal static class LocalExtensions
    {
        public static void ForWindowFromTemplate(this object templateFrameworkElement, Action<Window> action)
        {
            var window = ((FrameworkElement)templateFrameworkElement).TemplatedParent as Window;
            if (window != null) action(window);
        }

        public static void ForTextBoxFromTemplate(this object templateFrameworkElement, Action<TextBox> action)
        {
            var textBox = ((FrameworkElement)templateFrameworkElement).TemplatedParent as TextBox;
            action(textBox);
        }
    }

    public partial class FlatInnerWindow
    {
        bool f_resizeInProcess;

        private void TopBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            sender.ForWindowFromTemplate(w => w.DragMove());
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            sender.ForWindowFromTemplate(w => w.Close());
        }

        #region WinResizeble

        private void Resize_Init(object sender, MouseButtonEventArgs e)
        {
            var senderRect = sender as Rectangle;
            if (senderRect != null)
            {
                f_resizeInProcess = true;
                senderRect.CaptureMouse();
            }
        }

        private void Resize_End(object sender, MouseButtonEventArgs e)
        {
            var senderRect = sender as Rectangle;
            if (senderRect != null)
            {
                f_resizeInProcess = false;
                senderRect.ReleaseMouseCapture();
            }
        }

        private void Resizeing_Form(object sender, MouseEventArgs e)
        {
            if (f_resizeInProcess)
            {
                var senderRect = sender as Rectangle;
                if (senderRect != null)
                {
                    var mainWindow = senderRect.Tag as Window;
                    {
                        var width = e.GetPosition(mainWindow).X;
                        var height = e.GetPosition(mainWindow).Y;
                        senderRect.CaptureMouse();
                        if (senderRect.Name.ToLower().Contains("right"))
                        {
                            width += 5;
                            if (width > 0)
                                if (mainWindow != null) mainWindow.Width = width;
                        }
                        if (senderRect.Name.ToLower().Contains("left"))
                        {
                            width -= 5;
                            if (mainWindow != null)
                            {
                                mainWindow.Left += width;
                                width = mainWindow.Width - width;
                                if (width > 0)
                                {
                                    mainWindow.Width = width;
                                }
                            }
                        }
                        if (senderRect.Name.ToLower().Contains("bottom"))
                        {
                            height += 5;
                            if (height > 0)
                                if (mainWindow != null) mainWindow.Height = height;
                        }
                        if (senderRect.Name.ToLower().Contains("top"))
                        {
                            height -= 5;
                            if (mainWindow == null) return;
                            mainWindow.Top += height;
                            height = mainWindow.Height - height;
                            if (height > 0)
                            {
                                mainWindow.Height = height;
                            }
                        }
                    }
                }
            }
        }

        #endregion
    }
}
