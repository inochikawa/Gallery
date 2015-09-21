using Combogallary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Combogallary.ViewModel
{
    public class MainWindowViewModel
    {
        public int AlbumCount = 0;
        public MainWindowViewModel()
        {
            AlbumCount = Albums.Count;
        }
        public List<Album> Albums
        {
            get
            {
                return new List<Album>(ActiveRecordPattern.ActiveRecordBaseGeneric<Album>.LoadAll);
            }
        }
    }
}
