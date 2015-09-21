using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Data;

namespace Combogallary
{
    public static class CustomCursor
    {
        public static Cursor NormalCursor()
        {
            return new Cursor(new FileStream(@"Source\Cursores\2\Normal Select v2.1.ani", FileMode.Open));
        }

        public static Cursor TextSelect()
        {
            return new Cursor(new FileStream(@"Source\Cursores\2\Text Select v2.1.ani", FileMode.Open));
        }

        public static Cursor LinkSelect()
        {
            return new Cursor(new FileStream(@"Source\Cursores\2\Link Select v2.1.ani", FileMode.Open));
        }

        public static Cursor HandWriting()
        {
            return new Cursor(new FileStream(@"Source\Cursores\2\Handwriting v2.1.ani", FileMode.Open));
        }
    }
}
