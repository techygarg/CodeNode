using System;
using System.Data;

namespace CodeNode.Datalayer.Request
{
    [Serializable]
    public class AdhocParameter
    {
        #region "Private Variables"

        private int _size = -1;
        private SqlDbType _dataType = SqlDbType.Variant;
        private string _paramName;
        private object _paramValue;
        private ParameterDirection _paramDirection = ParameterDirection.Input;

        #endregion

        #region "Public Properties"

        public int Size
        {
            get { return _size; }
            set { _size = value; }
        }

        public SqlDbType DataType
        {
            get { return _dataType; }
            set { _dataType = value; }
        }

        public string ParamName
        {
            get { return _paramName; }
            set { _paramName = value; }
        }

        public object ParamValue
        {
            get { return _paramValue; }
            set { _paramValue = value; }
        }

        public ParameterDirection ParamDirection
        {
            get { return _paramDirection; }
            set { _paramDirection = value; }
        }

        #endregion

        #region "Constructors"

        /// <summary>
        ///     Initializes a new instance of the <see cref="AdhocParameter" /> class.
        /// </summary>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="paramValue">The parameter value.</param>
        public AdhocParameter(string paramName, object paramValue)
        {
            _paramName = paramName;
            _paramValue = paramValue;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="AdhocParameter" /> class.
        /// </summary>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="paramValue">The parameter value.</param>
        /// <param name="paramDirection">The parameter direction.</param>
        public AdhocParameter(string paramName, object paramValue, ParameterDirection paramDirection)
            : this(paramName, paramValue)
        {
            _paramDirection = paramDirection;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="AdhocParameter" /> class.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <param name="dataType">Type of the data.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="paramValue">The parameter value.</param>
        public AdhocParameter(int size, SqlDbType dataType, string paramName, object paramValue)
            : this(paramName, paramValue)
        {
            _size = size;
            _dataType = dataType;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="AdhocParameter" /> class.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <param name="dataType">Type of the data.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="paramValue">The parameter value.</param>
        /// <param name="paramDirection">The parameter direction.</param>
        public AdhocParameter(int size, SqlDbType dataType, string paramName, object paramValue,
            ParameterDirection paramDirection)
            : this(paramName, paramValue, paramDirection)
        {
            _size = size;
            _dataType = dataType;
        }

        #endregion
    }
}