using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using GraphicEditor.ViewModel;
using WinInterop = System.Windows.Interop;

namespace GraphicEditor
{
    /// <summary>
    /// Interaction logic for MainWindow
    /// </summary>
    public partial class MainWindow
    {
        private MainWindowViewModel f_mainWindowViewModel;

        public MainWindow()
        {
            InitializeComponent();
            f_mainWindowViewModel = new MainWindowViewModel();
            DataContext = f_mainWindowViewModel;
            f_mainWindowViewModel.ShowChildWindows(this);
            f_mainWindowViewModel.SubscribeMenuItemsToChildWindows(new List<MenuItem>()
            {
                layersMenuItem,
                colorPickerMenuItem,
                overviewMenuItem
            });
            statusBar.viewModel.UpdateSize((int)f_mainWindowViewModel.GraphicContent.WorkSpace.Width, (int)f_mainWindowViewModel.GraphicContent.WorkSpace.Height);
            f_mainWindowViewModel.StatusBar = statusBar.viewModel;

            // Does not match the MVVM pattern
            GraphicToolPropertiesUserControl.viewModel.Subscribe(f_mainWindowViewModel.GraphicContent.GraphicToolProperties);

            Loaded += MainContainer_Loaded;
            Closing += MainContainer_Unloaded;
        }

        private void MainContainer_Unloaded(object sender, CancelEventArgs e)
        {
            f_mainWindowViewModel.SaveChildWindowsStates();
        }


        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.Z)
                f_mainWindowViewModel.UndoExecute();

            if (Keyboard.Modifiers == (ModifierKeys.Control | ModifierKeys.Shift) && e.Key == Key.Z)
                f_mainWindowViewModel.RedoExecute();
        }

        void MainContainer_Loaded(object sender, RoutedEventArgs e)
        {
            if (Application.Current.Properties["ArbitraryArgName"] != null)
            {
                string fname = Application.Current.Properties["ArbitraryArgName"].ToString();
                f_mainWindowViewModel.OpenGeFileOnStartup(fname);
            }
            f_mainWindowViewModel.LoadChildWindowsStates();
        }

        private void MainWindow_OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Visual visual = e.OriginalSource as Visual;

            if (visual != null && !visual.IsDescendantOf(GraphicToolPropertiesUserControl.Expander))
                GraphicToolPropertiesUserControl.Expander.IsExpanded = false;
        }

        // *********************************************************************
        // **********Fixing taksbar cover in Maximized state********************
        // *********************************************************************

        public override void OnApplyTemplate()
        {
            IntPtr handle = (new WinInterop.WindowInteropHelper(this)).Handle;
            WinInterop.HwndSource hwndSource = WinInterop.HwndSource.FromHwnd(handle);
            hwndSource?.AddHook(WindowProc);
        }

        private static IntPtr WindowProc(
              IntPtr hwnd,
              int msg,
              IntPtr wParam,
              IntPtr lParam,
              ref bool handled)
        {
            switch (msg)
            {
                case 0x0024:
                    WmGetMinMaxInfo(hwnd, lParam);
                    handled = true;
                    break;
            }

            return (IntPtr)0;
        }

        private static void WmGetMinMaxInfo(IntPtr hwnd, IntPtr lParam)
        {

            Minmaxinfo mmi = (Minmaxinfo)Marshal.PtrToStructure(lParam, typeof(Minmaxinfo));

            // Adjust the maximized size and position to fit the work area of the correct monitor
            int MONITOR_DEFAULTTONEAREST = 0x00000002;
            IntPtr monitor = MonitorFromWindow(hwnd, MONITOR_DEFAULTTONEAREST);

            if (monitor != IntPtr.Zero)
            {

                Monitorinfo monitorInfo = new Monitorinfo();
                GetMonitorInfo(monitor, monitorInfo);
                Rect rcWorkArea = monitorInfo.rcWork;
                Rect rcMonitorArea = monitorInfo.rcMonitor;
                mmi.ptMaxPosition.x = Math.Abs(rcWorkArea.left - rcMonitorArea.left);
                mmi.ptMaxPosition.y = Math.Abs(rcWorkArea.top - rcMonitorArea.top);
                mmi.ptMaxSize.x = Math.Abs(rcWorkArea.right - rcWorkArea.left);
                mmi.ptMaxSize.y = Math.Abs(rcWorkArea.bottom - rcWorkArea.top);
            }

            Marshal.StructureToPtr(mmi, lParam, true);
        }

        /// <summary>
        /// Point aka POINTAPI
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct Point
        {
            /// <summary>
            /// x coordinate of point.
            /// </summary>
            public int x;
            /// <summary>
            /// y coordinate of point.
            /// </summary>
            public int y;

            /// <summary>
            /// Construct a point of coordinates (x,y).
            /// </summary>
            public Point(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Minmaxinfo
        {
            public Point ptReserved;
            public Point ptMaxSize;
            public Point ptMaxPosition;
            public Point ptMinTrackSize;
            public Point ptMaxTrackSize;
        };
        
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class Monitorinfo
        {           
            public int cbSize = Marshal.SizeOf(typeof(Monitorinfo));
          
            public Rect rcMonitor = new Rect();
          
            public Rect rcWork = new Rect();
           
            public int dwFlags = 0;
        }
        
        /// <summary> Win32 </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 0)]
        public struct Rect
        {
            /// <summary> Win32 </summary>
            public readonly int left;
            /// <summary> Win32 </summary>
            public readonly int top;
            /// <summary> Win32 </summary>
            public readonly int right;
            /// <summary> Win32 </summary>
            public readonly int bottom;
            /// <summary> Win32 </summary>
            public static readonly Rect Empty;

            /// <summary> Win32 </summary>
            public int Width
            {
                get { return Math.Abs(right - left); }  // Abs needed for BIDI OS
            }

            /// <summary> Win32 </summary>
            public int Height
            {
                get { return bottom - top; }
            }

            /// <summary> Win32 </summary>
            public Rect(int left, int top, int right, int bottom)
            {
                this.left = left;
                this.top = top;
                this.right = right;
                this.bottom = bottom;
            }


            /// <summary> Win32 </summary>
            public Rect(Rect rcSrc)
            {
                left = rcSrc.left;
                top = rcSrc.top;
                right = rcSrc.right;
                bottom = rcSrc.bottom;
            }

            /// <summary> Win32 </summary>
            public bool IsEmpty
            {
                get
                {
                    // BUGBUG : On Bidi OS (hebrew arabic) left > right
                    return left >= right || top >= bottom;
                }
            }

            /// <summary> Return a user friendly representation of this struct </summary>
            public override string ToString()
            {
                if (this == Empty) { return "Rect {Empty}"; }
                return "Rect { left : " + left + " / top : " + top + " / right : " + right + " / bottom : " + bottom + " }";
            }

            /// <summary> Determine if 2 Rect are equal (deep compare) </summary>
            public override bool Equals(object obj)
            {
                if (!(obj is System.Windows.Rect)) { return false; }
                return (this == (Rect)obj);
            }

            /// <summary>Return the HashCode for this struct (not garanteed to be unique)</summary>
            public override int GetHashCode()
            {
                return left.GetHashCode() + top.GetHashCode() + right.GetHashCode() + bottom.GetHashCode();
            }
            
            /// <summary> Determine if 2 Rect are equal (deep compare)</summary>
            public static bool operator ==(Rect rect1, Rect rect2)
            {
                return (rect1.left == rect2.left && rect1.top == rect2.top && rect1.right == rect2.right && rect1.bottom == rect2.bottom);
            }

            /// <summary> Determine if 2 Rect are different(deep compare)</summary>
            public static bool operator !=(Rect rect1, Rect rect2)
            {
                return !(rect1 == rect2);
            }
        }

        [DllImport("User32")]
        internal static extern bool GetMonitorInfo(IntPtr hMonitor, Monitorinfo lpmi);
        
        [DllImport("User32")]
        internal static extern IntPtr MonitorFromWindow(IntPtr handle, int flags);
    }
}
