using System.Windows.Input;
using GraphicEditor.Model;

namespace GraphicEditor.ViewModel
{
    public class MainWindowViewModel
    {
        private ICommand f_showMessageCommand;

        public MainWindowViewModel()
        {
            f_showMessageCommand = new RelayCommand(ShowMessage);
        }

        public ICommand ShowMessageCommand
        {
            get
            {
                return f_showMessageCommand;
            }
            set
            {
                f_showMessageCommand = value;
            }
        }

        private void ShowMessage(object obj)
        {
            System.Windows.Forms.MessageBox.Show("Test");
        }

        private void pictureTabView_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
        }
    }
}
