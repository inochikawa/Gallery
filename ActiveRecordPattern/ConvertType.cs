using System;
using System.Collections.Generic;
using System.Linq;

namespace ActiveRecordPattern
{
    public static class ConvertType
    {
        private static Dictionary<string, string> dictionaryType = new Dictionary<string, string>();
        private static bool _createdDic = false;

        private static void setTypes()
        {
            if (!_createdDic)
            {//
                dictionaryType.Add("System.Int32", "INT");
                dictionaryType.Add("System.Single", "FLOAT"); 
                dictionaryType.Add("System.String", "TEXT");
                dictionaryType.Add("System.Float", "FLOAT(20)");
                dictionaryType.Add("System.Double", "FLOAT(20)");
                dictionaryType.Add("System.Boolean", "BIT");
                dictionaryType.Add("System.DateTime", "DATETIME"); 
                dictionaryType.Add("System.Guid", "UNIQUEIDENTIFIER NOT NULL DEFAULT newid()");
                dictionaryType.Add("System.Windows.Media.Imaging.BitmapImage", "IMAGE");
                dictionaryType.Add("System.Byte[]", "VARBINARY(8000)");
                dictionaryType.Add("System.Uri", "TEXT");
            }
            _createdDic = true;
        }

        public static string FromCLR(Type Type)
        {
            setTypes();
            try
            {
                return dictionaryType[Type.FullName];
            }
            catch
            {
                return "NVARCHAR(50)";
            }
            
        }

        public static string FromSQL(Type Type)
        {
            setTypes();
            try
            {
                return dictionaryType.FirstOrDefault(x => x.Value == Type.ToString()).Key;
            }
            catch
            {
                return "System.String";
            }

        }
    }
}
