using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GraphicEditor.View.UserControls
{
    /// <summary>
    /// Interaction logic for ChildWindow.
    /// </summary>
    [ContentProperty("Children")]
    public partial class ChildWindow : UserControl
    {
        private bool f_resizeInProcess;
        private Point f_clickPosition;

        public static readonly DependencyPropertyKey ChildrenProperty = DependencyProperty.RegisterReadOnly(
             "Children",
             typeof(UIElementCollection),
             typeof(ChildWindow),
             new PropertyMetadata());

        public ChildWindow()
        {
            InitializeComponent();
            Children = PART_Host.Children;

            MouseLeftButtonDown += TopPanelMouseLeftButtonDown;
            MouseMove += TopPanelMouseMove;
        }
        
        private void TopPanelMouseMove(object sender, MouseEventArgs e)
        {
            var draggableControl = sender as UserControl;

            if (e.LeftButton == MouseButtonState.Pressed && draggableControl != null)
            {
                Point currentPosition = e.GetPosition(this.Parent as UIElement);

                // If not clicked on border
                if(f_clickPosition.Y > TopPanel.Height)
                    return;

                var transform = draggableControl.RenderTransform as TranslateTransform;
                if (transform == null)
                {
                    transform = new TranslateTransform();
                    draggableControl.RenderTransform = transform;
                }

                transform.X = currentPosition.X - f_clickPosition.X;
                transform.Y = currentPosition.Y - f_clickPosition.Y;
            }
        }

        private void TopPanelMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            f_clickPosition = e.GetPosition(this);
        }

        public UIElementCollection Children
        {
            get { return (UIElementCollection)GetValue(ChildrenProperty.DependencyProperty); }
            set { SetValue(ChildrenProperty, value); }
        }

        public string Header
        {
            get { return HeaderTxt.Text; }
            set { HeaderTxt.Text = value; }
        }

        private void Resize_Init(object sender, MouseButtonEventArgs e)
        {
            Rectangle senderRect = sender as Rectangle;
            if (senderRect != null)
            {
                f_resizeInProcess = true;
                senderRect.CaptureMouse();
            }
        }

        private void Resize_End(object sender, MouseButtonEventArgs e)
        {
            Rectangle senderRect = sender as Rectangle;
            if (senderRect != null)
            {
                f_resizeInProcess = false; ;
                senderRect.ReleaseMouseCapture();
            }
        }

        private void Resizeing_Form(object sender, MouseEventArgs e)
        {
            if (f_resizeInProcess)
            {
                Rectangle senderRect = sender as Rectangle;
                if (senderRect != null)
                {
                    UserControl control = senderRect.Tag as UserControl;
                    double width = e.GetPosition(control).X;
                    double height = e.GetPosition(control).Y;
                    senderRect.CaptureMouse();
                    if (senderRect.Name.ToLower().Contains("right"))
                    {
                        width += 5;
                        if (width > 0)
                            control.Width = width;
                    }
                    if (senderRect.Name.ToLower().Contains("left"))
                    {
                        width -= 5;
                        //mainWindow.Width += width;
                        width = control.Width - width;
                        if (width > 0)
                        {
                            control.Width = width;
                        }
                    }
                    if (senderRect.Name.ToLower().Contains("bottom"))
                    {
                        height += 5;
                        if (height > 0)
                            control.Height = height;
                    }
                    if (senderRect.Name.ToLower().Contains("top"))
                    {
                        height -= 5;
                        //mainWindow.Height += height;
                        height = control.Height - height;
                        if (height > 0)
                        {
                            control.Height = height;
                        }
                    }
                }
            }
        }
    }
}
