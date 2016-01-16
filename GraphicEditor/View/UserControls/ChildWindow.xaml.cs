using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Shapes;
using GraphicEditor.Model;

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
        private double f_windowWidth;
        private TranslateTransform f_translateTransform;
        private List<MenuItem> f_menuItems;

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
            f_translateTransform = new TranslateTransform();
            RenderTransform = f_translateTransform;
            f_menuItems = new List<MenuItem>();
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

        public UIElement Child
        {
            get { return Children[0]; }
            set
            {
                if (!Children.Contains(value) && Children.Count < 1)
                    Children.Add(value);
            }
        }

        public void Move(int x, int? y)
        {
            f_translateTransform.X = x;
            if (y != null)
                f_translateTransform.Y = (int)y;
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
                    if (senderRect.Name.ToLower().Contains("right")) //right
                    {
                        width += 5;
                        if (width > 0)
                        {
                            control.Width = width;
                            Move((int)(e.GetPosition(this.Parent as UIElement).X - ((Grid)Parent).ActualWidth + 5), null);
                        }
                    }
                    if (senderRect.Name.ToLower().Contains("bottom"))
                    {
                        height += 5;
                        if (height > 0)
                            control.Height = height;
                    }
                }
            }
        }

        private void TopPanelMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                // If not clicked on border
                if (e.GetPosition(this).Y > TopPanel.Height || e.GetPosition(this).Y < 1)
                    return;

                Point currentPosition = e.GetPosition(this.Parent as UIElement);

                f_windowWidth = ((Grid)Parent).ActualWidth;
                Move((int)(e.GetPosition(this).X - (f_windowWidth - currentPosition.X) - (e.GetPosition(this).X - ActualWidth / 2)),
                    (int)(currentPosition.Y - 10));
            }
        }

        private void TopPanelMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            f_clickPosition = e.GetPosition(this);
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            Notify();
        }

        public void Subscribe(MenuItem observer)
        {
            f_menuItems.Add(observer);
        }

        public void Unsubscribe(MenuItem observer)
        {
            f_menuItems.Remove(observer);
        }

        public void Notify()
        {
            foreach (MenuItem menuItem in f_menuItems) 
                menuItem.IsChecked = IsVisible; 
        }
    }
}
