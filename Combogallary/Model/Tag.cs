using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ActiveRecordPattern;
using ActiveRecordPattern.Attributes;

namespace Combogallary.Model
{
    [ActiveRecord]
    public class Tag
    {
        private string _text;

        [PropertyRecord]
        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                //OnPropertyChanged("Text");
            }
        }

        public Tag(Guid idPicture, string textTag)
        {
            this.Text = textTag;
            //this.IdPicture = idPicture;
            //this.Id = Guid.NewGuid();
        }
    }
}
