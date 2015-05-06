using System;
using System.Configuration;
using System.Data.SqlClient;

namespace CodeNode.Datalayer.Provider
{
    public abstract class BaseDataProvider : IDisposable
    {
        #region Private Variables

        private SqlConnection _connection;
        private string _connectionString;

        #endregion

        #region Constructor

        /// <summary>
        ///     Initializes a new instance of the <see cref="BaseDataProvider" /> class.
        /// </summary>
        protected BaseDataProvider()
        {
            SetConnectionString();
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="BaseDataProvider" /> class.
        /// </summary>
        /// <param name="connectionStringName">Name of the connection string.</param>
        protected BaseDataProvider(string connectionStringName)
        {
            SetConnectionString(connectionStringName);
        }

        #endregion

        #region Private & Protected Methods

        /// <summary>
        ///     Sets the connection string.
        /// </summary>
        /// <param name="connectionStringName">Name of the connection string.</param>
        private void SetConnectionString(string connectionStringName = "DefaultConnection")
        {
            _connectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
        }


        /// <summary>
        ///     Gets the connection.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NullReferenceException">Connection string not set.</exception>
        protected virtual SqlConnection CreateConnection()
        {
            if (string.IsNullOrWhiteSpace(_connectionString))
                throw new NullReferenceException("Connection string not set.");

            var connection = new SqlConnection(_connectionString);
            connection.Open();
            return connection;
        }

        #endregion

        #region "IDisposable Support"

        // To detect redundant calls
        private bool _disposedValue;

        // IDisposable
        /// <summary>
        ///     Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing">
        ///     <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only
        ///     unmanaged resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    if ((_connection != null))
                    {
                        _connection.Close();
                        _connection.Dispose();
                        _connection = null;
                    }
                }
            }
            _disposedValue = true;
        }


        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}