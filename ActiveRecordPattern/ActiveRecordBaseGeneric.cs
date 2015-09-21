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
    public abstract class ActiveRecordBaseGeneric<T> : ActiveRecordBase 
        where T : ActiveRecordBaseGeneric<T>
    {

        #region Save, Update, Delete, Load

        public virtual void Save()
        {
            SqlConnection sqlConnection = new SqlConnection(_connString);
            SqlCommand sqlCom = sqlConnection.CreateCommand();

            Type classType = typeof(T);
            PropertyInfo[] props = properties(classType);
            string[] propNames = propertyNames(classType);
            Type[] propTypes = propertyTypes(classType);

            sqlCom.Parameters.Add("@" + propertyKeyName(classType), ConvertType.FromCLR(propertyKeyType(classType)));
            sqlCom.Parameters["@" + propertyKeyName(classType)].Value = propertyKey(classType).GetValue(this);

            StringBuilder query = new StringBuilder("INSERT INTO " + tableName(classType) + " VALUES(@" + propertyKeyName(classType) + ", ");

            for (int i = 0; i < propTypes.Count(); i++)
            {
                sqlCom.Parameters.Add("@" + propNames[i], ConvertType.FromCLR(propTypes[i]));
                sqlCom.Parameters["@" + propNames[i]].Value = props[i].GetValue(this);
                query.Append("@" + propNames[i]);
                if (i == propTypes.Count() - 1)
                    query.Append(");");
                else
                    query.Append(", ");
            }

            sqlCom.CommandText = query.ToString();

            sqlConnection.Open();

            try
            {
                sqlCom.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.ToString());
            }

            sqlConnection.Close();
        }
        /// <summary>
        /// Запись элемента в таблицу; если сущестует - перезаписать
        /// </summary>
        /// <param name="Object"></param>
        public virtual void Update()
        {
            if(!isDefineObject)
                Save();
            else
            {
                SqlConnection sqlConnection = new SqlConnection(_connString);
                SqlCommand sqlCom = sqlConnection.CreateCommand();
                Type classType = typeof(T);

                PropertyInfo[] props = properties(classType);
                string[] propNames = propertyNames(classType);
                Type[] propTypes = propertyTypes(classType);

                StringBuilder query = new StringBuilder("UPDATE " + tableName(classType) + " SET " + propertyKeyName(classType) 
                    + "=@" + propertyKeyName(classType) + ", ");
                
                sqlCom.Parameters.Add("@" + propertyKeyName(classType), ConvertType.FromCLR(propertyKeyType(classType)));
                sqlCom.Parameters["@" + propertyKeyName(classType)].Value = propertyKey(classType).GetValue(this);
                
                for (int i = 0; i < propTypes.Count(); i++)
                {
                    sqlCom.Parameters.Add("@" + propNames[i], propTypes[i]);
                    sqlCom.Parameters["@" + propNames[i]].Value = props[i].GetValue(this);
                    query.Append(propNames[i] + "=@" + propNames[i]);
                    if (i != propTypes.Count() - 1)
                        query.Append(", ");
                }

                query.Append(" WHERE " + propertyKeyName(classType) + "=@" + propertyKeyName(classType) + ";");
                sqlCom.CommandText = query.ToString();
                try
                {
                    sqlConnection.Open();
                    sqlCom.ExecuteNonQuery();

                }
                catch (SqlException e)
                { MessageBox.Show(e.ToString());  }
                finally { sqlConnection.Close(); }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Object"></param>
        public virtual void Delete()
        {
            if(!isDefineObject) 
                return;
            else
            {
                SqlConnection sqlConnection = new SqlConnection(_connString);
                SqlCommand sqlCom = sqlConnection.CreateCommand();
                Type classType = typeof(T);
                
                string query = "DELETE FROM " + tableName(classType) + " WHERE " +
                propertyKeyName(classType) + "=@" + propertyKeyName(classType) + ";";

                sqlCom.Parameters.Add("@" + propertyKeyName(classType), ConvertType.FromCLR(propertyKeyType(classType)));
                sqlCom.Parameters["@" + propertyKeyName(classType)].Value = propertyKey(classType).GetValue(this);
                
                sqlCom.CommandText = query.ToString();
                try
                {
                    sqlConnection.Open();
                    sqlCom.ExecuteNonQuery();
                }
                catch (SqlException e)
                { MessageBox.Show(e.ToString()); }
                finally { sqlConnection.Close(); }
            }
        }

        public virtual void Load()
        {
            SqlConnection sqlConnection = new SqlConnection(_connString);
            SqlCommand sqlCom = sqlConnection.CreateCommand();
            Type classType = typeof(T);

            string query = "SELECT * FROM " + tableName(classType) + " WHERE " + propertyKeyName(classType) + "=@" + propertyKeyName(classType) + ";";

            sqlCom.Parameters.Add("@" + propertyKeyName(classType), ConvertType.FromCLR(propertyKeyType(classType)));
            sqlCom.Parameters["@" + propertyKeyName(classType)].Value = propertyKey(classType).GetValue(this);

            sqlConnection.Open();
            sqlCom.CommandText = query.ToString();

            try
            {
                SqlDataReader sqlReader = sqlCom.ExecuteReader();

                int i = 1;
                while (sqlReader.Read())
                {
                    PropertyInfo propKey = propertyKey(classType);
                    propKey.SetValue(this, sqlReader[0]);
                    foreach (PropertyInfo propertyInfo in properties(classType))
                    {
                        propertyInfo.SetValue(this, sqlReader[i]);
                        i++;
                    }
                    i = 1;
                }

                sqlReader.Close();
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.ToString());
                sqlConnection.Close();
            }

            sqlConnection.Close();
        }

        #endregion

        public static List<T> LoadAll
        {
            get
            {
                Type type = typeof(T);
                List<T> items = new List<T>();
                SqlConnection sqlConnection = new SqlConnection(_connString);

                sqlConnection.Open();

                string query = "SELECT * FROM " + tableName(type) + ";";
                SqlCommand sqlCom = new SqlCommand(query, sqlConnection);

                try
                {
                    SqlDataReader sqlReader = sqlCom.ExecuteReader();

                    int i = 0;
                    while (sqlReader.Read())
                    {
                        dynamic obj = Activator.CreateInstance(type);
                        propertyKey(type).SetValue(obj, sqlReader[i]);
                        i++;
                        foreach (PropertyInfo propertyInfo in properties(type))
                        {
                            propertyInfo.SetValue(obj, sqlReader[i]);
                            i++;
                        }
                        i = 0;
                        items.Add(obj);
                    }

                    sqlReader.Close();

                }
                catch (SqlException e)
                {
                    sqlConnection.Close();
                }

                sqlConnection.Close();
                sqlConnection.Dispose();
                return items;
            }
        }

        //public static List<T> LoadSpecificAlbums(string login) 
        //{
        //    Type type = typeof(T);
        //    List<T> items = new List<T>();
        //    SqlConnection sqlConnection = new SqlConnection(_connString);

        //    sqlConnection.Open();

        //    string query = "SELECT * FROM " + tableName(type) + " WHERE " + propertyNames(type)[0] + "=@" + propertyNames(type)[0] + ";";
        //    SqlCommand sqlCom = new SqlCommand(query, sqlConnection);

        //    sqlCom.Parameters.Add("@" + propertyNames(type)[0], ConvertType.FromCLR(propertyTypes(type)[0]));
        //    sqlCom.Parameters["@" + propertyNames(type)[0]].Value = login;

        //    sqlConnection.Open();
        //    sqlCom.CommandText = query.ToString();

        //    try
        //    {
        //        SqlDataReader sqlReader = sqlCom.ExecuteReader();

        //        int i = 0;
        //        while (sqlReader.Read())
        //        {
        //            dynamic obj = Activator.CreateInstance(type);
        //            foreach (PropertyInfo propertyInfo in properties(type))
        //            {
        //                propertyInfo.SetValue(obj, sqlReader[i]);
        //                i++;
        //            }
        //            i = 0;
        //            items.Add(obj);
        //        }

        //        sqlReader.Close();

        //    }
        //    catch (SqlException e)
        //    {
        //        MessageBox.Show(e.ToString());
        //        sqlConnection.Close();
        //    }

        //    sqlConnection.Close();
        //    return items;
        //}

        //public static T LoadSpecificUser(string login)
        //{
        //    Type type = typeof(T);
        //    SqlConnection sqlConnection = new SqlConnection(_connString);

        //    sqlConnection.Open();

        //    string query = "SELECT * FROM " + tableName(type) + " WHERE " + propertyKeyName(type) + "=@" + propertyKeyName(type) + ";";
        //    SqlCommand sqlCom = new SqlCommand(query, sqlConnection);

        //    sqlCom.Parameters.Add("@" + propertyKeyName(type), ConvertType.FromCLR(propertyKeyType(type)));
        //    sqlCom.Parameters["@" + propertyKeyName(type)].Value = login;

        //    sqlConnection.Open();
        //    sqlCom.CommandText = query.ToString();

        //    dynamic obj = Activator.CreateInstance(type);
        //    try
        //    {
        //        SqlDataReader sqlReader = sqlCom.ExecuteReader();

        //        int i = 0;
        //        while (sqlReader.Read())
        //        {
        //            foreach (PropertyInfo propertyInfo in properties(type))
        //            {
        //                propertyInfo.SetValue(obj, sqlReader[i]);
        //                i++;
        //            }
        //            i = 0;
        //        }

        //        sqlReader.Close();

        //    }
        //    catch (SqlException e)
        //    {
        //        MessageBox.Show(e.ToString());
        //        sqlConnection.Close();
        //    }

        //    sqlConnection.Close();
        //    return obj;
        //}

        public static List<T> LoadSpecificPictures(Guid albumId)
        {
            Type type = typeof(T);
            List<T> items = new List<T>();
            SqlConnection sqlConnection = new SqlConnection(_connString);

            sqlConnection.Open();

            string query = "SELECT * FROM " + tableName(type) + " WHERE " + propertyNames(type)[1] + "=@" + propertyNames(type)[1] + ";";
            SqlCommand sqlCom = new SqlCommand(query, sqlConnection);

            sqlCom.Parameters.Add("@" + propertyNames(type)[1], ConvertType.FromCLR(propertyTypes(type)[0]));
            sqlCom.Parameters["@" + propertyNames(type)[1]].Value = albumId;

            try
            {                
                sqlConnection.Open();
            }
            catch (Exception)
            {
                sqlConnection.Close();
                sqlConnection.Open();
            }
            sqlCom.CommandText = query.ToString();

            try
            {
                SqlDataReader sqlReader = sqlCom.ExecuteReader();

                int i = 0;
                while (sqlReader.Read())
                {
                    dynamic obj = Activator.CreateInstance(type);
                    propertyKey(type).SetValue(obj, sqlReader[i]);
                    i++;
                    foreach (PropertyInfo propertyInfo in properties(type))
                    {
                        propertyInfo.SetValue(obj, sqlReader[i]);
                        i++;
                    }
                    i = 0;
                    items.Add(obj);
                }

                sqlReader.Close();

            }
            catch (SqlException e)
            {
                MessageBox.Show(e.ToString());
                sqlConnection.Close();
            }

            sqlConnection.Close();
            return items;
        }
    }
}
