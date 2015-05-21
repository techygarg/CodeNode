using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using CodeNode.Core.Utils;

namespace CodeNode.Extension
{
    public static class DataTableExtensions
    {
        /// <summary>
        ///     Gets the data in column.
        /// </summary>
        /// <param name="column">The column.</param>
        /// <returns></returns>
        public static IEnumerable<string> GetColumnData(this DataColumn column)
        {
            return column.Table.Rows.Cast<DataRow>().Select(row => row[column.ColumnName].ToString());
        }

        /// <summary>
        ///     To the list of T.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt">The data table.</param>
        /// <returns></returns>
        public static List<T> ToListof<T>(this DataTable dt)
        {
            Ensure.Argument.NotNull(dt, "dataTable");

            const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;
            var columnNames = dt.Columns.Cast<DataColumn>()
                .Select(c => c.ColumnName)
                .ToList();

            var objectProperties = typeof(T).GetProperties(flags);
            var targetList = dt.AsEnumerable().Select(dataRow =>
            {
                var instanceOfT = Activator.CreateInstance<T>();

                foreach (
                    var properties in
                        objectProperties.Where(
                            properties =>
                                columnNames.Contains(properties.Name) && dataRow[properties.Name] != DBNull.Value))
                {
                    properties.SetValue(instanceOfT, dataRow[properties.Name], null);
                }
                return instanceOfT;
            }).ToList();

            return targetList;
        }

        /// <summary>
        ///     To the XML.
        /// </summary>
        /// <param name="dataTable">The dataTable.</param>
        /// <param name="rootName">Name of the root.</param>
        /// <returns></returns>
        public static XDocument ToXml(this DataTable dataTable, string rootName)
        {
            Ensure.Argument.NotNull(dataTable, "dataTable");

            var xdoc = new XDocument
            {
                Declaration = new XDeclaration("1.0", "utf-8", "")
            };
            xdoc.Add(new XElement(rootName));
            foreach (DataRow row in dataTable.Rows)
            {
                var element = new XElement(dataTable.TableName);
                foreach (DataColumn col in dataTable.Columns)
                {
                    element.Add(new XElement(col.ColumnName, row[col].ToString().Trim(' ')));
                }
                if (xdoc.Root != null) xdoc.Root.Add(element);
            }

            return xdoc;
        }
    }
}