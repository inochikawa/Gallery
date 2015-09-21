using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using ActiveRecordPattern.Attributes;
using System.Text;

namespace ActiveRecordPattern
{
    public abstract class ActiveRecordBase
    {
        protected static string _connString = "Data Source=(LocalDB)\\v11.0;AttachDbFilename=\"D:\\Combogallary\\ActiveRecordPattern\\LocalDB.mdf\";Integrated Security=SSPI";

        public static bool SetConnection
        {
            get
            {
                SqlConnection sqlConnection = new SqlConnection(_connString);

                try
                {
                    sqlConnection.Open();
                    sqlConnection.Close();
                    return true;
                }
                catch(SqlException e)
                {
                    MessageBox.Show(e.ToString());
                    return false;
                }
            }
        }


        /// <summary>
        /// Устанавливает соединение с базой данных и в случае успеха, если данной таблици не существует,
        /// создает новую таблицу
        /// </summary>
        public void Initialize()
        {
            if (!SetConnection) return;
            if (existTable) return;

            Type classType = GetType();

            StringBuilder query = new StringBuilder("CREATE TABLE [dbo].[" + tableName(classType) + "] ([" + propertyKeyName(classType) + "] " + ConvertType.FromCLR(propertyKeyType(classType)) + " PRIMARY KEY, ");


            int colomnCount = propertyNames(classType).Count();
            for (int i = 0; i < colomnCount; i++)
            {
                query.Append("[" + propertyNames(classType)[i] + "] " + ConvertType.FromCLR(propertyTypes(classType)[i]));
                if (i == colomnCount - 1)
                    query.Append(")");
                else
                    query.Append(", ");
            }

            SqlConnection sqlConnection = new SqlConnection(_connString);

            SqlCommand sqlCom = new SqlCommand(query.ToString(), sqlConnection);

            sqlConnection.Open();

            try
            {
                sqlCom.ExecuteNonQuery();
                
            }
            catch (SqlException e)
            { MessageBox.Show(e.ToString()); }

            sqlConnection.Close();
        }

        private bool existTable
        {
            get
            {
                SqlConnection sqlConnection = new SqlConnection(_connString);

                sqlConnection.Open();

                Type classType = GetType();
                
                if (Attribute.IsDefined(classType, typeof(ActiveRecord)))
                {
                    string query = "SELECT COUNT(*) FROM " + tableName(classType) + ";";
                    SqlCommand sqlCom = new SqlCommand(query, sqlConnection);

                    try
                    {
                        sqlCom.ExecuteNonQuery();
                    }
                    catch (SqlException e )
                    {
                        sqlConnection.Close();
                        return false;
                    }
                }
                sqlConnection.Close();
                return true;
            }
        }

        /// <summary>
        /// Определяет существует ли данный объект в таблице
        /// </summary>
        /// <param name="Object"></param>
        /// <returns></returns>
        protected bool isDefineObject
        {
            get
            {
                Type classType = GetType();
                PropertyInfo propertyInfo = classType.GetProperties()[0];
                string query = "SELECT COUNT(*) FROM " + tableName(classType) + " WHERE Id LIKE '" + propertyInfo.GetValue(this) + "'";

                SqlConnection sqlConnection = new SqlConnection(_connString);

                SqlCommand sqlCom = new SqlCommand(query, sqlConnection);
                sqlConnection.Open();

                try
                {
                    SqlDataReader sqlReader = sqlCom.ExecuteReader();
                    sqlReader.Read();
                    var countObject = sqlReader[0];
                    sqlReader.Close();

                    if ((int)countObject == 0)
                    {
                        sqlConnection.Close();
                        return false;
                    }
                    else
                    {
                        sqlConnection.Close();
                        return true;
                    }
                }
                catch (SqlException e)
                {
                    MessageBox.Show(e.ToString());
                    sqlConnection.Close();
                    return false;
                }
            }
        }


        #region For work with table components --- Colomns name, type, table name...
        protected static PropertyInfo[] properties(Type Type)
        {
            List<PropertyInfo> propertyInfos = new List<PropertyInfo>();
            PropertyInfo[] properies = Type.GetProperties();
            foreach (PropertyInfo property in properies)
                if (Attribute.IsDefined(property, typeof(PropertyRecord)))
                    propertyInfos.Add(property);

            return propertyInfos.ToArray();
        }

        protected static PropertyInfo propertyKey(Type Type)
        {
            PropertyInfo[] properies = Type.GetProperties();
            foreach (PropertyInfo property in properies)
                if (Attribute.IsDefined(property, typeof(PropertyKeyRecord)))
                    return property;
            return null;
        }

        /// <summary>
        /// возращает строку, состоящую из именований столбцов
        /// </summary>
        /// <param name="Type"></param>
        /// <returns></returns>
        protected static string colomnString(Type Type)
        {
            StringBuilder colomns = new StringBuilder("(");

            foreach (string colomnName in propertyNames(Type))
            {
                colomns.Append(colomnName);

                if (colomnName == propertyNames(Type)[propertyNames(Type).Count() - 1])
                    colomns.Append(")");
                else
                    colomns.Append(", ");
            }

            return colomns.ToString();
        }

        /// <summary>
        /// Возращает название таблици
        /// </summary>
        /// <param name="Type"></param>
        /// <returns></returns>
        protected static string tableName(Type Type)
        {
            
            StringBuilder tableName = new StringBuilder();

            var attributeValue = Attribute.GetCustomAttribute(Type, typeof(ActiveRecord)) as ActiveRecord;
            if (attributeValue.Name == null)
                tableName.Append(Type.Name);
            else
                tableName.Append(attributeValue.Name);

            return tableName.ToString();
        }

        /// <summary>
        /// Возращает имена колонок для даного типа класса.
        /// </summary>
        /// <param name="Type"></param>
        /// <returns></returns>
        protected static string[] propertyNames(Type Type)
        {
            PropertyInfo[] properies = Type.GetProperties();
            List<string> colomnNames = new List<string>();
            foreach (PropertyInfo property in properies)
            {
                if (Attribute.IsDefined(property, typeof(PropertyRecord)))
                {
                    var attributeValue =
                        Attribute.GetCustomAttribute(property, typeof(PropertyRecord)) as PropertyRecord;

                    if (attributeValue.Name != null) colomnNames.Add(attributeValue.Name);
                    else colomnNames.Add(property.Name);
                }
            }

            return colomnNames.ToArray();
        }

        protected static string propertyKeyName(Type Type)
        {
            PropertyInfo[] properies = Type.GetProperties();
            foreach (PropertyInfo property in properies)
            {
                if (Attribute.IsDefined(property, typeof(PropertyKeyRecord)))
                {
                    var attributeValue =
                        Attribute.GetCustomAttribute(property, typeof(PropertyKeyRecord)) as PropertyKeyRecord;

                    if (attributeValue.Name != null) return attributeValue.Name;
                    else return property.Name;
                }
            }

            return null;
        }

        /// <summary>
        /// Возращает типи колонок для даного типа класса.
        /// </summary>
        /// <param name="Type"></param>
        /// <returns></returns>
        protected static Type[] propertyTypes(Type Type)
        {
            PropertyInfo[] properies = Type.GetProperties();
            List<Type> types = new List<Type>();

            foreach (PropertyInfo property in properies)
                if (Attribute.IsDefined(property, typeof(PropertyRecord)))
                    types.Add(property.PropertyType);

            return types.ToArray();
        }

        protected static Type propertyKeyType(Type Type)
        {
            PropertyInfo[] properies = Type.GetProperties();

            foreach (PropertyInfo property in properies)
                if (Attribute.IsDefined(property, typeof(PropertyKeyRecord)))
                    return property.PropertyType;

            return null;
        }

        #endregion
    }
}
