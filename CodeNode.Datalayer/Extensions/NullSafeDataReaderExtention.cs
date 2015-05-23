using System;
using System.Collections.Generic;
using System.Linq;
using CodeNode.Datalayer.Attributes;
using CodeNode.Datalayer.Reader;

namespace CodeNode.Datalayer.Extensions
{


    /// <summary>
    /// 
    /// </summary>
    public static class NullSafeDataReaderExtention
    {


        /// <summary>
        ///     Get Object of type T form data reader
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataReader">The data reader.</param>
        /// <param name="useMapColumnAttribute"></param>
        /// <returns></returns>
        public static T MapToObject<T>(this INullSafeDataReader dataReader, bool useMapColumnAttribute = false)
        {
            if (dataReader.Read())
                return dataReader.GetObject<T>(useMapColumnAttribute);
            else
                return default(T);
        }
       
        /// <summary>
        ///     Get list of object of type T form data reader
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataReader">The data reader.</param>
        /// <param name="useMapColumnAttribute"></param>
        /// <returns></returns>
        public static List<T> MapToList<T>(this INullSafeDataReader dataReader, bool useMapColumnAttribute = false)
        {
            var resultList = new List<T>();
            if (dataReader != null)
            {
                while (dataReader.Read())
                {
                    resultList.Add(dataReader.GetObject<T>(useMapColumnAttribute));
                }
            }
            return resultList;
        }

        /// <summary>
        /// Gets the object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataReader">The data reader.</param>
        /// <param name="useMapColumnAttribute">if set to <c>true</c> [use map column attribute].</param>
        /// <returns></returns>
        private static T GetObject<T>(this INullSafeDataReader dataReader, bool useMapColumnAttribute = false)
        {
            var instance = Activator.CreateInstance<T>();
            foreach (var prop in instance.GetType().GetProperties())
            {
                var memberName = string.Empty;
                if (useMapColumnAttribute)
                {
                    // if attribute of type MapColumnAttribute found use it, else use property name itself
                    var attribute =
                        prop.GetCustomAttributes(false).FirstOrDefault(x => x.GetType() == typeof(MapColumnAttribute));
                    if (attribute != null)
                    {
                        var mapTo = attribute as MapColumnAttribute;
                        if (mapTo != null) memberName = mapTo.Name;
                    }
                    else
                    {
                        memberName = prop.Name;
                    }
                }
                else
                {
                    memberName = prop.Name;
                }

                var typeCode = Type.GetTypeCode(prop.PropertyType);
                if (dataReader.HasColumn(memberName) && !Equals(dataReader[memberName], DBNull.Value))
                {
                    switch (typeCode)
                    {
                        case TypeCode.String:
                            prop.SetValue(instance, dataReader.GetString(memberName), null);
                            break;
                        case TypeCode.Int16:
                            prop.SetValue(instance, dataReader.GetInt16(memberName), null);
                            break;
                        case TypeCode.Int32:
                            prop.SetValue(instance, dataReader.GetInt32(memberName), null);
                            break;
                        case TypeCode.Int64:
                            prop.SetValue(instance, dataReader.GetInt64(memberName), null);
                            break;
                        case TypeCode.Decimal:
                            prop.SetValue(instance, dataReader.GetDecimal(memberName), null);
                            break;
                        case TypeCode.DateTime:
                            prop.SetValue(instance, dataReader.GetDateTime(memberName), null);
                            break;
                        case TypeCode.Double:
                            prop.SetValue(instance, dataReader.GetDouble(memberName), null);
                            break;
                        case TypeCode.Boolean:
                            prop.SetValue(instance, dataReader.GetBoolean(memberName), null);
                            break;
                        case TypeCode.Char:
                            prop.SetValue(instance, dataReader.GetChar(memberName), null);
                            break;
                        case TypeCode.Byte:
                            prop.SetValue(instance, dataReader.GetChar(memberName), null);
                            break;
                        case TypeCode.Object:
                            if (prop.PropertyType == typeof(Guid))
                                prop.SetValue(instance, dataReader.GetGuid(memberName), null);
                            break;
                    }
                }
                else
                {
                    switch (typeCode)
                    {
                        case TypeCode.String:
                            prop.SetValue(instance, string.Empty, null);
                            break;
                    }
                }
            }
            return instance;
        }

    }
}