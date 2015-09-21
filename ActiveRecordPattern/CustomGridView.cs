using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ActiveRecordPattern
{
    public static class CustomGridView
    {
        public static DataGridView Table(Type Type)
        {
            DataGridView table = new DataGridView();

            string[] columnNames = ActiveRecordBase.ColomnNames(Type);
            foreach (string columnName in columnNames) 
                table.Columns.Add("cln" + columnName, columnName);

            return table;
        }
    }
}
