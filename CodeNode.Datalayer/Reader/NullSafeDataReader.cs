using System;
using System.Data;

namespace CodeNode.Datalayer.Reader
{
    public class NullSafeDataReader : INullSafeDataReader
    {
        private readonly IDataReader _mDataReader;

        #region "Constructor"

        /// <summary>
        ///     Initializes the SafeDataReader object to use data from
        ///     the provided DataReader object.
        /// </summary>
        /// <param name="dataReader">The source DataReader object containing the data.</param>
        public NullSafeDataReader(IDataReader dataReader)
        {
            _mDataReader = dataReader;
        }

        #endregion

        /// <summary>
        ///     Get a reference to the underlying data reader
        ///     object that actually contains the data from
        ///     the data source.
        /// </summary>
        public IDataReader DataReader
        {
            get { return _mDataReader; }
        }

        /// <summary>
        ///     Gets a string value from the datareader.
        /// </summary>
        /// <remarks>
        ///     Returns empty string for null.
        /// </remarks>
        /// <param name="i">Ordinal column position of the value.</param>
        public virtual string GetString(int i)
        {
            return _mDataReader.IsDBNull(i) ? "" : _mDataReader.GetString(i);
        }

        /// <summary>
        ///     Gets a string value from the data reader.
        /// </summary>
        /// <remarks>
        ///     Returns empty string for null.
        /// </remarks>
        /// <param name="name">Name of the column containing the value.</param>
        public virtual string GetString(string name)
        {
            var index = GetOrdinal(name);
            return GetString(index);
        }

        /// <summary>
        ///     Gets a value of type <see cref="System.Object" /> from the datareader.
        /// </summary>
        /// <param name="i">Ordinal column position of the value.</param>
        public virtual object GetValue(int i)
        {
            if (_mDataReader.IsDBNull(i))
            {
                return null;
            }
            return _mDataReader.GetValue(i);
        }

        /// <summary>
        ///     Gets a value of type <see cref="System.Object" /> from the datareader.
        /// </summary>
        /// <param name="name">Name of the column containing the value.</param>
        public virtual object GetValue(string name)
        {
            var index = GetOrdinal(name);
            return GetValue(index);
        }

        /// <summary>
        ///     Gets an integer from the datareader.
        /// </summary>
        /// <remarks>
        ///     Returns 0 for null.
        /// </remarks>
        /// <param name="i">Ordinal column position of the value.</param>
        public virtual int GetInt32(int i)
        {
            if (_mDataReader.IsDBNull(i))
            {
                return 0;
            }
            return _mDataReader.GetInt32(i);
        }

        /// <summary>
        ///     Gets an integer from the datareader.
        /// </summary>
        /// <remarks>
        ///     Returns 0 for null.
        /// </remarks>
        /// <param name="name">Name of the column containing the value.</param>
        public virtual int GetInt32(string name)
        {
            var index = GetOrdinal(name);
            return GetInt32(index);
        }

        /// <summary>
        ///     Gets a double from the datareader.
        /// </summary>
        /// <remarks>
        ///     Returns 0 for null.
        /// </remarks>
        /// <param name="i">Ordinal column position of the value.</param>
        public virtual double GetDouble(int i)
        {
            if (_mDataReader.IsDBNull(i))
            {
                return 0;
            }
            return _mDataReader.GetDouble(i);
        }

        /// <summary>
        ///     Gets a double from the datareader.
        /// </summary>
        /// <remarks>
        ///     Returns 0 for null.
        /// </remarks>
        /// <param name="name">Name of the column containing the value.</param>
        public virtual double GetDouble(string name)
        {
            var index = GetOrdinal(name);
            return GetDouble(index);
        }

        /// <summary>
        ///     Gets a Guid value from the datareader.
        /// </summary>
        /// <remarks>
        ///     Returns Guid.Empty for null.
        /// </remarks>
        /// <param name="i">Ordinal column position of the value.</param>
        public virtual Guid GetGuid(int i)
        {
            if (_mDataReader.IsDBNull(i))
            {
                return Guid.Empty;
            }
            return _mDataReader.GetGuid(i);
        }

        /// <summary>
        ///     Gets a Guid value from the datareader.
        /// </summary>
        /// <remarks>
        ///     Returns Guid.Empty for null.
        /// </remarks>
        /// <param name="name">Name of the column containing the value.</param>
        public virtual Guid GetGuid(string name)
        {
            var index = GetOrdinal(name);
            return GetGuid(index);
        }

        public bool HasColumn(string name)
        {
            for (var x = 0; x <= _mDataReader.FieldCount - 1; x++)
            {
                if (_mDataReader.GetName(x).Equals(name, StringComparison.InvariantCultureIgnoreCase))
                    return true;
            }
            return false;
        }

        /// <summary>
        ///     Reads the next row of data from the datareader.
        /// </summary>
        public virtual bool Read()
        {
            return _mDataReader.Read();
        }

        /// <summary>
        ///     Moves to the next result set in the datareader.
        /// </summary>
        public virtual bool NextResult()
        {
            return _mDataReader.NextResult();
        }

        /// <summary>
        ///     Closes the datareader.
        /// </summary>
        public virtual void Close()
        {
            _mDataReader.Close();
        }

        /// <summary>
        ///     Returns the depth property value from the datareader.
        /// </summary>
        public virtual int Depth
        {
            get { return _mDataReader.Depth; }
        }

        /// <summary>
        ///     Returns the FieldCount property from the datareader.
        /// </summary>
        public virtual int FieldCount
        {
            get { return _mDataReader.FieldCount; }
        }

        /// <summary>
        ///     Gets a boolean value from the datareader.
        /// </summary>
        /// <remarks>
        ///     Returns <see langword="false" /> for null.
        /// </remarks>
        /// <param name="i">Ordinal column position of the value.</param>
        public virtual bool GetBoolean(int i)
        {
            if (_mDataReader.IsDBNull(i))
            {
                return false;
            }
            return _mDataReader.GetBoolean(i);
        }

        /// <summary>
        ///     Gets a boolean value from the datareader.
        /// </summary>
        /// <remarks>
        ///     Returns <see langword="false" /> for null.
        /// </remarks>
        /// <param name="name">Name of the column containing the value.</param>
        public virtual bool GetBoolean(string name)
        {
            var index = GetOrdinal(name);
            return GetBoolean(index);
        }

        /// <summary>
        ///     Gets a byte value from the datareader.
        /// </summary>
        /// <remarks>
        ///     Returns 0 for null.
        /// </remarks>
        /// <param name="i">Ordinal column position of the value.</param>
        public virtual byte GetByte(int i)
        {
            if (_mDataReader.IsDBNull(i))
            {
                return 0;
            }
            return _mDataReader.GetByte(i);
        }

        /// <summary>
        ///     Gets a byte value from the datareader.
        /// </summary>
        /// <remarks>
        ///     Returns 0 for null.
        /// </remarks>
        /// <param name="name">Name of the column containing the value.</param>
        public virtual byte GetByte(string name)
        {
            var index = GetOrdinal(name);
            return GetByte(index);
        }

        /// <summary>
        ///     Invokes the GetBytes method of the underlying datareader.
        /// </summary>
        /// <remarks>
        ///     Returns 0 for null.
        /// </remarks>
        /// <param name="i">Ordinal column position of the value.</param>
        /// <param name="buffer">Array containing the data.</param>
        /// <param name="bufferOffset">Offset position within the buffer.</param>
        /// <param name="fieldOffset">Offset position within the field.</param>
        /// <param name="length">Length of data to read.</param>
        public virtual long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferOffset, int length)
        {
            if (_mDataReader.IsDBNull(i))
            {
                return 0;
            }
            return _mDataReader.GetBytes(i, fieldOffset, buffer, bufferOffset, length);
        }

        /// <summary>
        ///     Invokes the GetBytes method of the underlying datareader.
        /// </summary>
        /// <remarks>
        ///     Returns 0 for null.
        /// </remarks>
        /// <param name="name">Name of the column containing the value.</param>
        /// <param name="buffer">Array containing the data.</param>
        /// <param name="bufferOffset">Offset position within the buffer.</param>
        /// <param name="fieldOffset">Offset position within the field.</param>
        /// <param name="length">Length of data to read.</param>
        public virtual long GetBytes(string name, long fieldOffset, byte[] buffer, int bufferOffset, int length)
        {
            var index = GetOrdinal(name);
            return GetBytes(index, fieldOffset, buffer, bufferOffset, length);
        }

        /// <summary>
        ///     Gets a char value from the datareader.
        /// </summary>
        /// <remarks>
        ///     Returns Char.MinValue for null.
        /// </remarks>
        /// <param name="i">Ordinal column position of the value.</param>
        public virtual char GetChar(int i)
        {
            if (_mDataReader.IsDBNull(i))
            {
                return char.MinValue;
            }
            var myChar = new char[1];
            _mDataReader.GetChars(i, 0, myChar, 0, 1);
            return myChar[0];
        }

        /// <summary>
        ///     Gets a char value from the datareader.
        /// </summary>
        /// <remarks>
        ///     Returns Char.MinValue for null.
        /// </remarks>
        /// <param name="name">Name of the column containing the value.</param>
        public virtual char GetChar(string name)
        {
            var index = GetOrdinal(name);
            return GetChar(index);
        }

        /// <summary>
        ///     Invokes the GetChars method of the underlying datareader.
        /// </summary>
        /// <remarks>
        ///     Returns 0 for null.
        /// </remarks>
        /// <param name="i">Ordinal column position of the value.</param>
        /// <param name="buffer">Array containing the data.</param>
        /// <param name="bufferOffset">Offset position within the buffer.</param>
        /// <param name="fieldOffset">Offset position within the field.</param>
        /// <param name="length">Length of data to read.</param>
        public virtual long GetChars(int i, long fieldOffset, char[] buffer, int bufferOffset, int length)
        {
            if (_mDataReader.IsDBNull(i))
            {
                return 0;
            }
            return _mDataReader.GetChars(i, fieldOffset, buffer, bufferOffset, length);
        }

        /// <summary>
        ///     Invokes the GetChars method of the underlying datareader.
        /// </summary>
        /// <remarks>
        ///     Returns 0 for null.
        /// </remarks>
        /// <param name="name">Name of the column containing the value.</param>
        /// <param name="buffer">Array containing the data.</param>
        /// <param name="bufferOffset">Offset position within the buffer.</param>
        /// <param name="fieldOffset">Offset position within the field.</param>
        /// <param name="length">Length of data to read.</param>
        public virtual long GetChars(string name, long fieldOffset, char[] buffer, int bufferOffset, int length)
        {
            var index = GetOrdinal(name);
            return GetChars(index, fieldOffset, buffer, bufferOffset, length);
        }

        /// <summary>
        ///     Invokes the GetData method of the underlying datareader.
        /// </summary>
        /// <param name="i">Ordinal column position of the value.</param>
        public virtual IDataReader GetData(int i)
        {
            return _mDataReader.GetData(i);
        }

        /// <summary>
        ///     Invokes the GetData method of the underlying datareader.
        /// </summary>
        /// <param name="name">Name of the column containing the value.</param>
        public virtual IDataReader GetData(string name)
        {
            var index = GetOrdinal(name);
            return GetData(index);
        }

        /// <summary>
        ///     Invokes the GetDataTypeName method of the underlying datareader.
        /// </summary>
        /// <param name="i">Ordinal column position of the value.</param>
        public virtual string GetDataTypeName(int i)
        {
            return _mDataReader.GetDataTypeName(i);
        }

        /// <summary>
        ///     Invokes the GetDataTypeName method of the underlying datareader.
        /// </summary>
        /// <param name="name">Name of the column containing the value.</param>
        public virtual string GetDataTypeName(string name)
        {
            var index = GetOrdinal(name);
            return GetDataTypeName(index);
        }

        /// <summary>
        ///     Gets a date value from the datareader.
        /// </summary>
        /// <remarks>
        ///     Returns DateTime.MinValue for null.
        /// </remarks>
        /// <param name="i">Ordinal column position of the value.</param>
        public virtual DateTime GetDateTime(int i)
        {
            if (_mDataReader.IsDBNull(i))
            {
                return DateTime.MinValue;
            }
            return _mDataReader.GetDateTime(i);
        }

        /// <summary>
        ///     Gets a date value from the datareader.
        /// </summary>
        /// <remarks>
        ///     Returns DateTime.MinValue for null.
        /// </remarks>
        /// <param name="name">Name of the column containing the value.</param>
        public virtual DateTime GetDateTime(string name)
        {
            var index = GetOrdinal(name);
            return GetDateTime(index);
        }

        /// <summary>
        ///     Gets a decimal value from the datareader.
        /// </summary>
        /// <remarks>
        ///     Returns 0 for null.
        /// </remarks>
        /// <param name="i">Ordinal column position of the value.</param>
        public virtual decimal GetDecimal(int i)
        {
            if (_mDataReader.IsDBNull(i))
            {
                return 0;
            }
            return _mDataReader.GetDecimal(i);
        }

        /// <summary>
        ///     Gets a decimal value from the datareader.
        /// </summary>
        /// <remarks>
        ///     Returns 0 for null.
        /// </remarks>
        /// <param name="name">Name of the column containing the value.</param>
        public virtual decimal GetDecimal(string name)
        {
            var index = GetOrdinal(name);
            return GetDecimal(index);
        }

        /// <summary>
        ///     Invokes the GetFieldType method of the underlying datareader.
        /// </summary>
        /// <param name="i">Ordinal column position of the value.</param>
        public virtual Type GetFieldType(int i)
        {
            return _mDataReader.GetFieldType(i);
        }

        /// <summary>
        ///     Invokes the GetFieldType method of the underlying datareader.
        /// </summary>
        /// <param name="name">Name of the column containing the value.</param>
        public virtual Type GetFieldType(string name)
        {
            var index = GetOrdinal(name);
            return GetFieldType(index);
        }

        /// <summary>
        ///     Gets a Single value from the datareader.
        /// </summary>
        /// <remarks>
        ///     Returns 0 for null.
        /// </remarks>
        /// <param name="i">Ordinal column position of the value.</param>
        public virtual float GetFloat(int i)
        {
            if (_mDataReader.IsDBNull(i))
            {
                return 0;
            }
            return _mDataReader.GetFloat(i);
        }

        /// <summary>
        ///     Gets a Single value from the datareader.
        /// </summary>
        /// <remarks>
        ///     Returns 0 for null.
        /// </remarks>
        /// <param name="name">Name of the column containing the value.</param>
        public virtual float GetFloat(string name)
        {
            var index = GetOrdinal(name);
            return GetFloat(index);
        }

        /// <summary>
        ///     Gets a Short value from the datareader.
        /// </summary>
        /// <remarks>
        ///     Returns 0 for null.
        /// </remarks>
        /// <param name="i">Ordinal column position of the value.</param>
        public virtual short GetInt16(int i)
        {
            if (_mDataReader.IsDBNull(i))
            {
                return 0;
            }
            return _mDataReader.GetInt16(i);
        }

        /// <summary>
        ///     Gets a Short value from the datareader.
        /// </summary>
        /// <remarks>
        ///     Returns 0 for null.
        /// </remarks>
        /// <param name="name">Name of the column containing the value.</param>
        public virtual short GetInt16(string name)
        {
            var index = GetOrdinal(name);
            return GetInt16(index);
        }

        /// <summary>
        ///     Gets a Long value from the datareader.
        /// </summary>
        /// <remarks>
        ///     Returns 0 for null.
        /// </remarks>
        /// <param name="i">Ordinal column position of the value.</param>
        public virtual long GetInt64(int i)
        {
            if (_mDataReader.IsDBNull(i))
            {
                return 0;
            }
            return _mDataReader.GetInt64(i);
        }

        /// <summary>
        ///     Gets a Long value from the datareader.
        /// </summary>
        /// <remarks>
        ///     Returns 0 for null.
        /// </remarks>
        /// <param name="name">Name of the column containing the value.</param>
        public virtual long GetInt64(string name)
        {
            var index = GetOrdinal(name);
            return GetInt64(index);
        }

        /// <summary>
        ///     Invokes the GetName method of the underlying datareader.
        /// </summary>
        /// <param name="i">Ordinal column position of the value.</param>
        public virtual string GetName(int i)
        {
            return _mDataReader.GetName(i);
        }

        /// <summary>
        ///     Gets an ordinal value from the datareader.
        /// </summary>
        /// <param name="name">Name of the column containing the value.</param>
        public virtual int GetOrdinal(string name)
        {
            return _mDataReader.GetOrdinal(name);
        }

        /// <summary>
        ///     Invokes the GetSchemaTable method of the underlying datareader.
        /// </summary>
        public virtual DataTable GetSchemaTable()
        {
            return _mDataReader.GetSchemaTable();
        }

        /// <summary>
        ///     Invokes the GetValues method of the underlying datareader.
        /// </summary>
        /// <param name="values">
        ///     An array of System.Object to
        ///     copy the values into.
        /// </param>
        public virtual int GetValues(object[] values)
        {
            return _mDataReader.GetValues(values);
        }

        /// <summary>
        ///     Returns the IsClosed property value from the datareader.
        /// </summary>
        public bool IsClosed
        {
            get { return _mDataReader.IsClosed; }
        }

        /// <summary>
        ///     Invokes the IsDBNull method of the underlying datareader.
        /// </summary>
        /// <param name="i">Ordinal column position of the value.</param>
        public virtual bool IsDBNull(int i)
        {
            return _mDataReader.IsDBNull(i);
        }

        /// <summary>
        ///     Invokes the IsDBNull method of the underlying datareader.
        /// </summary>
        /// <param name="name">Name of the column containing the value.</param>
        public virtual bool IsDBNull(string name)
        {
            var index = GetOrdinal(name);
            return IsDBNull(index);
        }

        /// <summary>
        ///     Returns a value from the datareader.
        /// </summary>
        /// <param name="name">Name of the column containing the value.</param>
        public virtual object this[string name]
        {
            get
            {
                var value = _mDataReader[name];
                if (DBNull.Value.Equals(value))
                {
                    return null;
                }
                return value;
            }
        }

        /// <summary>
        ///     Returns a value from the data reader.
        /// </summary>
        /// <param name="i">Ordinal column position of the value.</param>
        public virtual object this[int i]
        {
            get
            {
                if (_mDataReader.IsDBNull(i))
                {
                    return null;
                }
                return _mDataReader[i];
            }
        }

        /// <summary>
        ///     Returns the RecordsAffected property value from the underlying datareader.
        /// </summary>
        public int RecordsAffected
        {
            get { return _mDataReader.RecordsAffected; }
        }

        #region " IDisposable Support "

        // To detect redundant calls
        private bool _disposedValue;

        /// <summary>
        ///     Disposes the object.
        /// </summary>
        /// <param name="disposing">
        ///     True if called by
        ///     the public Dispose method.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    // free unmanaged resources when explicitly called
                    _mDataReader.Dispose();
                }

                // free shared unmanaged resources
            }
            _disposedValue = true;
        }

        /// <summary>
        ///     Disposes the object.
        /// </summary>
        public void Dispose()
        {
            // Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}