using System;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using Application = System.Windows.Application;
using MessageBox = System.Windows.Forms.MessageBox;

namespace GraphicEditor
{
    /// <summary>
    /// Interaction logic for App
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            if (e.Args != null && e.Args.Count() > 0)
            {
                try
                {
                    var fname = e.Args[0];

                    // It comes in as a URI; this helps to convert it to a path.
                    Uri uri = new Uri(fname);
                    fname = uri.LocalPath;

                    this.Properties["ArbitraryArgName"] = fname;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Graphic editor open error", MessageBoxButtons.OK);
                }
            }
            base.OnStartup(e);
        }

    }
}
