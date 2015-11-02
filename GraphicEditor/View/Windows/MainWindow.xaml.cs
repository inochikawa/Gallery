using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Globalization;
using Combogallary.Model.ProxyPattern;
using GraphicEditor.Model;
using GraphicEditor.Model.ShapesModel;
using GraphicEditor.View.Windows;

namespace GraphicEditor
{
    /// <summary>
    /// Interaction logic for MainWindow
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<TabItem> f_tabItems;
        private List<Grid> f_tabGrids;
        private List<DesignerCanvas> f_designerCanvases = new List<DesignerCanvas>();
        private List<PictureProxy> f_pictures;
        private RectangleShape f_rectangleShape;
        private EllipseShape f_ellipseShape;
        private SelectedShape f_selectedShape = SelectedShape.None;

        public MainWindow()
        {
            InitializeComponent();
            f_pictures = new List<PictureProxy>();
            f_tabItems = new List<TabItem>();
            f_tabGrids = new List<Grid>();
            pictureTabView.DataContext = f_tabItems;

            f_rectangleShape = new RectangleShape(100, 50, Brushes.Red);
            f_ellipseShape = new EllipseShape(50, 50, Brushes.Blue);

            addRectToMenu();
            addEllipseToMenu();
        }

        private enum SelectedShape
        {
            /// <summary>
            /// If no select any shape
            /// </summary>
            None,

            /// <summary>
            /// If selected ellipse in toolbox
            /// </summary>
            Elipse,

            /// <summary>
            /// If selected ellipse in toolbox
            /// </summary>
            Rectangle
        }

        private void addRectToMenu()
        {
            Image rectImage = new Image();
            rectImage.BeginInit();
            rectImage.Source = new BitmapImage(
                new Uri(
                    @"pack://application:,,,/View/Resources/Images/rectangle.png",
                    UriKind.RelativeOrAbsolute));
            rectImage.Stretch = Stretch.Uniform;
            rectImage.EndInit();
            MenuItem rectMenuItem = new MenuItem();
            rectMenuItem.Icon = rectImage;
            rectMenuItem.Click += RectMenuItem_Click;
            menuPanel.Items.Add(rectMenuItem);
        }

        private void addEllipseToMenu()
        {
            Image ellipseImage = new Image();
            ellipseImage.BeginInit();
            ellipseImage.Source = new BitmapImage(
                new Uri(
                    @"pack://application:,,,/View/Resources/Images/ellipse.png",
                    UriKind.RelativeOrAbsolute));
            ellipseImage.Stretch = Stretch.Uniform;
            ellipseImage.EndInit();
            MenuItem ellipseMenuItem = new MenuItem();
            ellipseMenuItem.Icon = ellipseImage;
            ellipseMenuItem.Click += ellipseMenuItem_Click;
            menuPanel.Items.Add(ellipseMenuItem);
        }

        private void ellipseMenuItem_Click(object sender, RoutedEventArgs e)
        {
            f_selectedShape = SelectedShape.Elipse;
        }

        private void RectMenuItem_Click(object sender, RoutedEventArgs e)
        {
            f_selectedShape = SelectedShape.Rectangle;
        }

        private void overrideTabContent()
        {
            Grid grid = new Grid();
            DesignerItem designerItem = new DesignerItem();
            Image image = new Image();
            image.Source = f_pictures[f_pictures.Count - 1].Open();
            ScrollViewer scrollViewer = new ScrollViewer();
            scrollViewer.Background = Brushes.Transparent;
            scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            DesignerCanvas designerCanvas = new DesignerCanvas();
            designerCanvas.Background = Brushes.Red;
            designerCanvas.Width = 500;
            designerCanvas.Height = 300;

            DesignerCanvas layerCanvas = new DesignerCanvas();
            layerCanvas.Background = Brushes.Yellow;

            designerItem.Content = image;
            image.Stretch = Stretch.Fill;
            Canvas.SetLeft(designerItem, 100);
            Canvas.SetTop(designerItem, 100);
            designerCanvas.Children.Add(designerItem);

            layerCanvas.Children.Add(designerCanvas);

            scrollViewer.Content = layerCanvas;

            ZoomBox zoomBox = new ZoomBox();
            zoomBox.Width = 180;
            zoomBox.HorizontalAlignment = HorizontalAlignment.Right;
            zoomBox.VerticalAlignment = VerticalAlignment.Top;
            zoomBox.ScrollViewer = scrollViewer;
            zoomBox.Margin = new Thickness(0, 5, 25, 0);

            grid.Children.Add(scrollViewer);
            grid.Children.Add(zoomBox);
            
            f_tabGrids.Add(grid);
        }

        private TabItem AddTabItem()
        {
            int count = f_tabItems.Count;
            TabItem tab = new TabItem();
            tab.Header = string.Format(CultureInfo.CurrentCulture, "Tab {0}", count);
            tab.Name = string.Format(CultureInfo.CurrentCulture, "tab{0}", count);
            if (f_pictures.Count != 0)
            {
                overrideTabContent();
                tab.Content = f_tabGrids.Last();
            }

            f_tabItems.Insert(count, tab);
            pictureTabView.DataContext = null;
            pictureTabView.DataContext = f_tabItems;
            pictureTabView.SelectedItem = tab;

            return tab;
        }

        private void openFileMenuItem_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".png";
            dlg.Filter =
                "JPG Files (*.jpg)|*.jpg|JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|GIF Files (*.gif)|*.gif|All files (*.*)|*.*";
            bool? result = dlg.ShowDialog();
            if (result == true)
            {
                BitmapImage image = new BitmapImage(new Uri(dlg.FileName));
                PictureProxy picture = new PictureProxy(dlg.SafeFileName, dlg.FileName);
                picture.Width = image.Width;
                picture.Height = image.Height;
                f_pictures.Add(picture);
                AddTabItem();
                return;
            }
        }

        private void imageProperties_Click(object sender, RoutedEventArgs e)
        {
            if (f_pictures.Count != 0)
                new ImagePropertiesWindow(f_pictures[pictureTabView.SelectedIndex]).ShowDialog();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            string tabName = (sender as Button).CommandParameter.ToString();

            var item = pictureTabView.Items.Cast<TabItem>().Where(i => i.Name.Equals(tabName)).SingleOrDefault();

            TabItem tab = item as TabItem;

            if (tab != null)
            {
                if (f_tabItems.Count < 2)
                {
                    MessageBox.Show("Cannot remove last tab.");
                }
                else
                {
                    // get selected tab
                    TabItem selectedTab = pictureTabView.SelectedItem as TabItem;

                    // clear tab control binding
                    pictureTabView.DataContext = null;

                    f_tabItems.Remove(tab);

                    // bind tab control
                    pictureTabView.DataContext = f_tabItems;

                    // select previously selected tab. if that is removed then select first tab
                    if (selectedTab == null || selectedTab.Equals(tab))
                    {
                        selectedTab = f_tabItems[0];
                    }

                    pictureTabView.SelectedItem = selectedTab;
                }
            }
        }

        private void pictureTabView_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Shape renderShape = null;
            switch (f_selectedShape)
            {
                case SelectedShape.Elipse:
                    EllipseShape newEllipse = f_ellipseShape.Clone() as EllipseShape;
                    renderShape = newEllipse.Ellipse;
                    break;
                case SelectedShape.Rectangle:
                    RectangleShape newRect = f_rectangleShape.Clone() as RectangleShape;
                    renderShape = newRect.Rectangle;
                    break;
                default:
                    return;
            }

            Canvas.SetLeft(renderShape, e.GetPosition(f_designerCanvases[pictureTabView.SelectedIndex]).X);
            Canvas.SetTop(renderShape, e.GetPosition(f_designerCanvases[pictureTabView.SelectedIndex]).Y);
            f_designerCanvases[pictureTabView.SelectedIndex].Children.Add(renderShape);
        }
    }
}
