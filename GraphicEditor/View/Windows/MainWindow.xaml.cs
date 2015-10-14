using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Combogallary.Model.ProxyPattern;
using GraphicEditor.UserControls.Model;
using GraphicEditor.UserControls.Model.Shapes;
using GraphicEditor.View.Windows;

namespace GraphicEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<TabItem> tabItems;
        private List<Grid> tabGrids;
        private List<DesignerCanvas> designerCanvases = new List<DesignerCanvas>();
        private List<PictureProxy> pictures;
        private RectangleShape rectangleShape;
        private EllipseShape ellipseShape;
        private SelectedShape selectedShape = SelectedShape.None;

        public MainWindow()
        {
            InitializeComponent();
            pictures = new List<PictureProxy>();
            tabItems = new List<TabItem>();
            tabGrids = new List<Grid>();
            pictureTabView.DataContext = tabItems;

            rectangleShape = new RectangleShape(100, 50, Brushes.Red);
            ellipseShape = new EllipseShape(50, 50, Brushes.Blue);

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
            selectedShape = SelectedShape.Elipse;
        }

        private void RectMenuItem_Click(object sender, RoutedEventArgs e)
        {
            selectedShape = SelectedShape.Rectangle;
        }

        private void overrideTabContent()
        {
            Grid grid = new Grid();
            DesignerItem designerItem = new DesignerItem();
            Image image = new Image();
            image.Source = pictures[pictures.Count - 1].Open();
            ScrollViewer scrollViewer = new ScrollViewer();
            scrollViewer.Background = Brushes.Transparent;
            scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            DesignerCanvas designerCanvas = new DesignerCanvas();
            designerCanvas.Background = Brushes.White;
            designerCanvas.AllowDrop = true;

            designerItem.Content = image;
            designerCanvas.Children.Add(designerItem);
            scrollViewer.Content = designerCanvas;

            ZoomBox zoomBox = new ZoomBox();
            zoomBox.Width = 180;
            zoomBox.HorizontalAlignment = HorizontalAlignment.Right;
            zoomBox.VerticalAlignment = VerticalAlignment.Top;
            zoomBox.ScrollViewer = scrollViewer;
            zoomBox.Margin = new Thickness(0, 5, 25, 0);

            grid.Children.Add(scrollViewer);
            grid.Children.Add(zoomBox);

            designerCanvases.Add(designerCanvas);
            tabGrids.Add(grid);
        }

        private TabItem AddTabItem()
        {
            int count = tabItems.Count;
            TabItem tab = new TabItem();
            tab.Header = string.Format("Tab {0}", count);
            tab.Name = string.Format("tab{0}", count);
            if (pictures.Count != 0)
            {
                overrideTabContent();
                tab.Content = tabGrids.Last();
            }
            
            tabItems.Insert(count, tab);
            pictureTabView.DataContext = null;
            pictureTabView.DataContext = tabItems;
            pictureTabView.SelectedItem = tab;

            return tab;
        }

        private void openFileMenuItem_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".png";
            dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
            bool? result = dlg.ShowDialog();
            if (result == true)
            {
                BitmapImage image = new BitmapImage(new Uri(dlg.FileName));
                PictureProxy picture = new PictureProxy(dlg.SafeFileName, dlg.FileName);
                picture.Width = image.Width;
                picture.Height = image.Height;
                pictures.Add(picture);
                AddTabItem();
                return;
            }
        }

        private void imageProperties_Click(object sender, RoutedEventArgs e)
        {
            if (pictures.Count != 0)
                new ImagePropertiesWindow(pictures[pictureTabView.SelectedIndex]).ShowDialog();
        }
        
        private void pictureTabView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TabItem tab = pictureTabView.SelectedItem as TabItem;
            if (tab == null) return;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            string tabName = (sender as Button).CommandParameter.ToString();

            var item = pictureTabView.Items.Cast<TabItem>().Where(i => i.Name.Equals(tabName)).SingleOrDefault();

            TabItem tab = item as TabItem;

            if (tab != null)
            {
                if (tabItems.Count < 2)
                {
                    MessageBox.Show("Cannot remove last tab.");
                }
                else
                {
                    // get selected tab
                    TabItem selectedTab = pictureTabView.SelectedItem as TabItem;

                    // clear tab control binding
                    pictureTabView.DataContext = null;

                    tabItems.Remove(tab);

                    // bind tab control
                    pictureTabView.DataContext = tabItems;

                    // select previously selected tab. if that is removed then select first tab
                    if (selectedTab == null || selectedTab.Equals(tab))
                    {
                        selectedTab = tabItems[0];
                    }

                    pictureTabView.SelectedItem = selectedTab;
                }
            }
        }

        private void pictureTabView_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            TabItem currentTabItem = tabItems[pictureTabView.SelectedIndex];

            Shape renderShape = null;
            switch (selectedShape)
            {
                case SelectedShape.Elipse:
                    EllipseShape newEllipse = ellipseShape.Clone() as EllipseShape;
                    renderShape = newEllipse.Ellipse;
                    break;
                case SelectedShape.Rectangle:
                    RectangleShape newRect = rectangleShape.Clone() as RectangleShape;
                    renderShape = newRect.Rectangle;
                    break;
                default:
                    return; 
            }

            Canvas.SetLeft(renderShape, e.GetPosition(designerCanvases[pictureTabView.SelectedIndex]).X);
            Canvas.SetTop(renderShape, e.GetPosition(designerCanvases[pictureTabView.SelectedIndex]).Y);
            designerCanvases[pictureTabView.SelectedIndex].Children.Add(renderShape);            
        }
    }
}
