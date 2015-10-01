using System;
using System.Windows.Media.Imaging;

namespace Combogallary.Model.Interfaces
{
    public interface IPicture
    {
        Guid Id { get; set; }
        string Name { get; set; }
        string Location { get; set; }
        string Dimension { get; set; }
        long Size { get; set; }
        DateTime DateAdded { get; set; }
        BitmapImage Open();
    }
}
