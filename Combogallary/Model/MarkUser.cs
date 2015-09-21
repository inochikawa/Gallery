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
    class MarkUser : MetaData<MarkUser>
    {
        public static List<MarkUser> Items = new List<MarkUser>();

        public MarkUser(Guid idUser, Guid idPicture)
        {
            this.IdUser = idUser;
            this.IdPicture = idPicture;
            this.Id = Guid.NewGuid();
            Items.Add(this);
        }
    }
}
