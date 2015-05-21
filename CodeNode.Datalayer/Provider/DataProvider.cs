using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using CodeNode.Core.Utils;
using CodeNode.Datalayer.Reader;
using CodeNode.Datalayer.Request;

namespace CodeNode.Datalayer.Provider
{
    /// <summary>
    /// </summary>
    public class DataProvider : BaseDataProvider, IDataProvider
    {
        #region Private Methods

        /// <summary>
        ///     Creates the command.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="sqlCon">The SQL con.</param>
        /// <returns></returns>
        private static SqlCommand CreateSqlCommand(AdhocRequest request, SqlConnection sqlCon)
        {
            var command = new SqlCommand(request.Command, sqlCon) {CommandType = request.CommandType};

            foreach (var item in request.Params)
            {
                var param = new SqlParameter("@" + item.ParamName, item.ParamValue);

                if (item.DataType != SqlDbType.Variant)
                    param.SqlDbType = item.DataType;

                if (item.Size != -1)
                    param.Size = item.Size;

                param.Direction = item.ParamDirection;
                command.Parameters.Add(param);
            }

            if (request.Prepare)
                command.Prepare();

            return command;
        }

        #endregion

        #region Constructor

        /// <summary>
        ///     Initializes a new instance of the <see cref="DataProvider" /> class.
        /// </summary>
        public DataProvider()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="DataProvider" /> class.
        /// </summary>
        /// <param name="connectionStringName">Name of the connection string.</param>
        public DataProvider(string connectionStringName)
            : base(connectionStringName)
        {
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Executes the data reader.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public virtual INullSafeDataReader ExecuteDataReader(AdhocRequest request)
        {
            return ExecuteDataReader(request, CommandBehavior.CloseConnection);
        }

        /// <summary>
        ///     Executes the data reader.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="behavior">The behavior.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">request</exception>
        public virtual INullSafeDataReader ExecuteDataReader(AdhocRequest request, CommandBehavior behavior)
        {
            Ensure.Argument.NotNull(request, "AdhocRequest");

            NullSafeDataReader datareader = null;
            var sqlCon = CreateConnection();

            if ((sqlCon != null))
            {
                var cm = CreateSqlCommand(request, sqlCon);
                datareader = new NullSafeDataReader(cm.ExecuteReader(behavior));
            }

            return datareader;
        }

        /// <summary>
        ///     Executes the scalar.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">request</exception>
        public virtual object ExecuteScalar(AdhocRequest request)
        {
            Ensure.Argument.NotNull(request, "AdhocRequest");
            object result;
            using (var sqlCon = CreateConnection())
            {
                Ensure.Argument.NotNull(sqlCon, "SQL Connection");
                using (var cm = CreateSqlCommand(request, sqlCon))
                {
                    result = cm.ExecuteScalar();
                }
            }

            return result;
        }

        /// <summary>
        ///     Executes the non query.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">request</exception>
        public virtual int ExecuteNonQuery(AdhocRequest request)
        {
            Ensure.Argument.NotNull(request, "AdhocRequest");
            int result;
            using (var sqlCon = CreateConnection())
            {
                Ensure.Argument.NotNull(sqlCon, "SQL Connection");
                using (var cm = CreateSqlCommand(request, sqlCon))
                {
                    result = cm.ExecuteNonQuery();
                }
            }

            return result;
        }

        /// <summary>
        ///     Executes the non query for out parameter.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request">The request.</param>
        /// <param name="parameterName">Name of the  output parameterName.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">request</exception>
        public virtual T ExecuteNonQueryForOutParameter<T>(AdhocRequest request, string parameterName)
        {
            Ensure.Argument.NotNull(request, "AdhocRequest");
            var result = default(T);

            using (var sqlCon = CreateConnection())
            {
                Ensure.Argument.NotNull(sqlCon, "SQL Connection");
                using (var cm = CreateSqlCommand(request, sqlCon))
                {
                    cm.ExecuteNonQuery();
                    if (!string.IsNullOrWhiteSpace(parameterName))
                        result =
                            (T)
                                Convert.ChangeType(cm.Parameters["@" + parameterName].Value, typeof (T),
                                    CultureInfo.InvariantCulture);
                }
            }
            return result;
        }

        /// <summary>
        ///     Executes the data set.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">request</exception>
        public virtual DataSet ExecuteDataSet(AdhocRequest request)
        {
            Ensure.Argument.NotNull(request, "AdhocRequest");
            var dataset = new DataSet();

            using (var sqlCon = CreateConnection())
            {
                Ensure.Argument.NotNull(sqlCon, "SQL Connection");
                using (var cm = CreateSqlCommand(request, sqlCon))
                {
                    var ds = new SqlDataAdapter(cm);
                    ds.Fill(dataset);
                }
            }
            return dataset;
        }

        /// <summary>
        ///     Executes the data table.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">request</exception>
        public virtual DataTable ExecuteDataTable(AdhocRequest request)
        {
            Ensure.Argument.NotNull(request, "AdhocRequest");
            var dataTable = new DataTable();

            using (var sqlCon = CreateConnection())
            {
                Ensure.Argument.NotNull(sqlCon, "SQL Connection");
                using (var cm = CreateSqlCommand(request, sqlCon))
                {
                    var ds = new SqlDataAdapter(cm);
                    ds.Fill(dataTable);
                }
            }
            return dataTable;
        }

        /// <summary>
        ///     Bulks the copy.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <exception cref="ArgumentNullException">request</exception>
        public virtual void BulkCopy(AdhocBulkRequest request)
        {
            Ensure.Argument.NotNull(request, "AdhocBulkRequest");

            using (var bulkCopy = new SqlBulkCopy(CreateConnection()))
            {
                bulkCopy.DestinationTableName = request.DestinaitionTable;
                bulkCopy.BulkCopyTimeout = request.TimeOut;
                bulkCopy.BatchSize = request.BatchSize;
                bulkCopy.WriteToServer(request.SourceTable);
            }
        }

        #endregion
    }
}