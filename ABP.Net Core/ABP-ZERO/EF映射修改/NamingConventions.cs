using System;
using System.Collections.Generic;
using System.Text;

namespace Research.PropertyInfoes
{
    public static class NamingConventions
    {
        public static bool IsPropertyName(string name)
        {
            string[] parts = name.Split(" ");
            if (string.IsNullOrWhiteSpace(name) || parts.Length < 3)
            {
                return false;
            }
            return true;
        }
        public static bool IsMigratorCodeLine(string name)
        {
            string[] parts = name.Split(" ");
            if (string.IsNullOrWhiteSpace(name) ||
               parts.Length < 3 || !parts[1].Equals("="))
            {
                return false;
            }
            return true;
        }

        #region Migrations

        public static string GetColumnFirstName(string propertyFullName)
        {
            if (propertyFullName.Contains("class"))
            {
                return propertyFullName;
            }
            String[] parts = propertyFullName.TrimStart().Split(" ");
            if ((string.IsNullOrWhiteSpace(propertyFullName) ||
                parts.Length < 3))
            {
                return propertyFullName;
            }
            else if (parts[1].Equals("="))
            {

                return GetSqlColumnRename(parts[0]);
            }
            else
            {
                return propertyFullName;
            }
        }
<<<<<<< HEAD

        public static string ReplaceMigrator2(string m_migratorName, string m_columnName="")
        {
            if (m_migratorName.Contains("class"))
            {
                return m_migratorName;
            }
            String[] parts = m_migratorName.TrimStart().Split(" ");
            if ((string.IsNullOrWhiteSpace(m_migratorName) ||
                parts.Length < 3))
            {
                return m_migratorName;
            }
            else if (parts[1].Equals("="))
            {
                var result = GetSqlColumnRename(parts[0]);
                for (int i = 1; i < parts.Length; i++)
                {
                    result += " " + parts[i];
                }
                return result;
            }
            else
            {
                return m_migratorName;
            }
        }
        public static string ReplaceMigrator(string m_migratorName, string m_columnName)
=======
        public static string ReplaceMigrator(string migratorName, string  columnName)
>>>>>>> 48297e6d6665bb68e0ff67dd1dc96755e06bfd14
        {
            if (migratorName.Contains("class"))
            {
                return migratorName;
            }
            String[] parts = migratorName.TrimStart().Split(" ");
            if ((string.IsNullOrWhiteSpace(migratorName) ||
                parts.Length < 3))
            {
                return migratorName;
            }
            else if (parts[1].Equals("="))
            {
                var newMigratorRename = GetSqlColumnRename(parts[0]);

                for (int i = 1; i < parts.Length; i++)
                {
                    newMigratorRename += " " + parts[i];
                }
                return newMigratorRename;

            }
            else
            {
                return migratorName;
            }
        }

        public static string ReplaceMigrator2(string migratorName, string  columnName)
        {
            if (migratorName.Contains("class"))
            {
                return migratorName;
            }
            String[] parts = migratorName.TrimStart().Split(" ");
            if (IsMigratorCodeLine(migratorName))
            {
                return migratorName;
            }
<<<<<<< HEAD
            var m_newMigratorName = GetColumnFirstName(m_migratorName);
=======
            var  newMigratorName =  columnName;
>>>>>>> 48297e6d6665bb68e0ff67dd1dc96755e06bfd14
            for (int i = 1; i < parts.Length; i++)
            {
                 newMigratorName += " " + parts[i];
            }
            return  newMigratorName;
        }

        #endregion


        #region Property
         
        public static string GetPropertyName(string propertyFullName)
        {
            if (propertyFullName.Contains("class"))
            {
                return propertyFullName;
            }
            String[] parts = propertyFullName.Split(" ");
            if (!IsPropertyName(propertyFullName))
            {
                return propertyFullName;
            }
            return parts[2];
        }

        /// <summary>
        /// override or new 之前的虚属性
        /// </summary>
        /// <param name="propertyFullName"></param>
        /// <returns></returns>
        public static string ReNewPropertyName(string propertyFullName)
        {
            if (propertyFullName.Contains("class"))
            {
                return propertyFullName;
            }
            String[] parts = propertyFullName.Split(" ");
            if (!IsPropertyName(propertyFullName))
            {
                return propertyFullName;
            }
            var  newPropertyName = parts[0] + " new ";
            for (int i = 1; i < parts.Length; i++)
            {
                 newPropertyName += " " + parts[i];
            }
            return  newPropertyName;
        } 

        /// <summary>
        /// 属性名上注解  添加sql的Entity关系映射
        /// </summary>
        /// <param name="propertyFullName"></param>
        /// <param name=" column_name"></param>
        /// <param name="isZeroModule"></param>
        /// <returns></returns>
        public static string GetPropertyNameWithColumn(string propertyFullName, string  column_name, byte isZeroModule)
        {
            if (propertyFullName.Contains("class"))
            {
                return propertyFullName;
            }
            String[] parts = propertyFullName.Split(" ");
            if (!IsPropertyName(propertyFullName))
            {
                return propertyFullName;
            }
            if (isZeroModule == 1)
            {
                return "[Column(\"" +  column_name + "\")]" + "\n" + ReNewPropertyName(propertyFullName);
            }
            return "[Column(\"" +  column_name + "\")]" + "\n" + propertyFullName;
        }

        #endregion


        #region Column

        public static string GetSqlColumnRename (string name)
        {

            if (name.Contains("CreationTime"))
            {
                return "create_time";
            }
            if (name.Contains("LastModificationTime"))
            {
                return "modified_time";
            }
            if (name.Contains("DeletionTime"))
            {
                return "deleted_time";
            }
            return UnderscoreName(name);
        }  
        public static string UnderscoreName(string name)
        {
             
            StringBuilder result = new StringBuilder();
            char[]  list = name.ToCharArray();
            if (name != null &&  list.Length > 0)
            {  // 将第一个字符处理成小写
                result.Append(name.Substring(0, 1).ToLowerInvariant());
                for (int i = 1; i < name.Length; i++)
                {
                    if ( list[i].ToString().Equals( list[i].ToString().ToUpperInvariant()))
                    { // 在大写字母前添加下划线
                        result.Append("_");
                    } // 其他字符直接转成小写
                    result.Append( list[i].ToString().ToLowerInvariant());
                }
            }
            return result.ToString();
        }
         
        #endregion


        #region Generator-forsaken


        //private static string GetColumnFirstName(string propertyFullName)
        //{
        //    if (propertyFullName.Contains("class"))
        //    {
        //        return propertyFullName;
        //    }
        //    String[] parts = propertyFullName.TrimStart().Split(" ");
        //    if ((string.IsNullOrWhiteSpace(propertyFullName) ||
        //        parts.Length < 3) )
        //    {
        //        return propertyFullName;
        //    }
        //    else if (parts[1].Equals("="))
        //    {

        //        return UnderscoreName(parts[0]);
        //    }
        //    else
        //    { 
        //        return propertyFullName;
        //    }
        //}
        //private static string ReplaceMigrator(string migratorName, string  columnName)
        //{
        //    if (migratorName.Contains("class"))
        //    {
        //        return migratorName;
        //    }
        //    String[] parts = migratorName.TrimStart().Split(" ");
        //    if (string.IsNullOrWhiteSpace(migratorName) ||
        //        parts.Length < 3 || !parts[1].Equals("="))
        //    {
        //        return migratorName;
        //    }
        //    var  newMigratorName =  columnName;
        //    for (int i = 1; i < parts.Length; i++)
        //    {
        //         newMigratorName += " " + parts[i];
        //    }
        //    return  newMigratorName;
        //}

        //private static string GetPropertyName(string propertyFullName)
        //{
        //    if (propertyFullName.Contains("class"))
        //    {
        //        return propertyFullName;
        //    }
        //    String[] parts = propertyFullName.Split(" ");
        //    if (string.IsNullOrWhiteSpace(propertyFullName)||parts.Length<3)
        //    {
        //        return propertyFullName;
        //    }
        //    return parts[2];
        //}
        //private static string ReNewPropertyName(string propertyFullName)
        //{
        //    if (propertyFullName.Contains("class"))
        //    {
        //        return propertyFullName;
        //    }
        //    String[] parts = propertyFullName.Split(" ");
        //    if (string.IsNullOrWhiteSpace(propertyFullName)||parts.Length < 3)
        //    {
        //        return propertyFullName;
        //    }
        //    var  newPropertyName = parts[0]+" new ";
        //    for (int i = 1; i < parts.Length; i++)
        //    {
        //         newPropertyName += " "+parts[i];
        //    }
        //    return  newPropertyName;
        //}
        //public static string GetPropertyNameWithColumn(string propertyFullName, string  column_name,byte isZeroModule)
        //{ 
        //    if (propertyFullName.Contains("class"))
        //    {
        //        return propertyFullName;
        //    }
        //    String[] parts = propertyFullName.Split(" ");
        //    if (string.IsNullOrWhiteSpace(propertyFullName)||parts.Length < 3)
        //    {
        //        return propertyFullName;
        //    }
        //    if (isZeroModule==1)
        //    { 
        //        return "[Column(\"" +  column_name + "\")]" + "\n" + ReNewPropertyName(propertyFullName);
        //    }
        //    return "[Column(\"" +  column_name + "\")]" + "\n" + propertyFullName; 
        //}

        //public static string UnderscoreName(string name)
        //{
        //    if (name.Contains("CreationTime"))
        //    {
        //        return "create_time";
        //    }
        //    if (name.Contains("LastModificationTime"))
        //    {
        //        return "modified_time";
        //    }
        //    if (name.Contains("DeletionTime"))
        //    {
        //        return "deleted_time";
        //    }
        //    StringBuilder result = new StringBuilder();
        //    char[]  list = name.ToCharArray();
        //    if (name != null &&  list.Length > 0)
        //    {  // 将第一个字符处理成小写
        //        result.Append(name.Substring(0, 1).ToLowerInvariant()); 
        //        for (int i = 1; i < name.Length; i++)
        //        {
        //            if ( list[i].ToString().Equals( list[i].ToString().ToUpperInvariant())  )
        //            { // 在大写字母前添加下划线
        //                result.Append("_");
        //            } // 其他字符直接转成小写
        //            result.Append( list[i].ToString().ToLowerInvariant());
        //        }
        //    }
        //    return result.ToString(); 
        //}

        #endregion

    }
}
