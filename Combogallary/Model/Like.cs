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
    class Like: MetaData<Like>
    {
        public Like(Guid idUser, Guid idPicture)
        {
            this.IdUser = idUser;
            this.IdPicture = idPicture;
            this.Id = Guid.NewGuid();
        }
    }
}
