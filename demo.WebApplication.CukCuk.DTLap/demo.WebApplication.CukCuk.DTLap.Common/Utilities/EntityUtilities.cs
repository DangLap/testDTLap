using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Demo.WebApplication.Common.Utilities
{
    public static class EntityUtilities
    {
        ///<sumary>
        ///Lấy tên bảng của entity
        /// </sumary>
        /// <typeparam name="T">Kiểu dữ liệu của Entity</typeparam>
        /// <returns> Tên bảng</returns>
        /// Created by: DTLap (26/8)
        public static string GetTableName<T>()
        {
            string tableName = typeof(T).Name;
            var tableAttributes = typeof(T).GetTypeInfo().GetCustomAttributes<TableAttribute>();
            if (tableAttributes.Count() > 0)
            {
                tableName = tableAttributes.First().Name;
            }
            return tableName;
        }

        public static IEnumerable<dynamic> GetColumnAttributeProperties<T>()
        {
            var properties = typeof(T).GetProperties();
            return properties;
        }
    }
}
