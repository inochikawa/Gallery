using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Combogallary
{
    /// <summary>
    /// Interaction logic for LogInWindow.xaml
    /// </summary>
    public partial class LogInView : Window
    {
        List<ImageBrush> _images = new List<ImageBrush>();
        DispatcherTimer _timer = new DispatcherTimer();
        int _counter = 1;

        public LogInView()
        {
            InitializeComponent();
            //_images = ImageProcessing.AddImagesToList(@"Source\BackgroundImages");
            if (_images.Count != 0)
            {
                //borderMain.Background = _images[0];
                _timer.Interval = new TimeSpan(0, 0, 0, 5, 0);
                _timer.Tick += _timer_Tick;
                _timer.Start();
            }
        }

        void _timer_Tick(object sender, EventArgs e)
        {
            //ImageProcessing.SetBorderBackgroundSlider(_images[_counter], borderMain);
            _counter++;
            if (_counter > _images.Count-1) _counter = 0; 
        }
        
        private void TopBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
        }

        private void btnSingIn_Click(object sender, RoutedEventArgs e)
        {
            SingUpView singUpWin = new SingUpView();
            singUpWin.Show();
            this.Close();
        }

        private void LogInWin_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Closing -= LogInWin_Closing;
            e.Cancel = true;
            var anim = new DoubleAnimation(0, (Duration)TimeSpan.FromMilliseconds(250));
            anim.Completed += (s, _) => this.Close();
            this.BeginAnimation(UIElement.OpacityProperty, anim);
        }

        private void btnSingUp_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }
    }
}
