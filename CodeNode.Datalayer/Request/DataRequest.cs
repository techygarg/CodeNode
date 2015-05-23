using System.Collections.Generic;
using System.Data;

namespace CodeNode.Datalayer.Request
{
    /// <summary>
    /// </summary>
    public class DataRequest
    {
        #region "Private Variables"

        private int _timeout = -1;
        private List<DataParameter> _params = new List<DataParameter>();
        private CommandType _commandType = CommandType.Text;

        #endregion

        #region "Constructor"

        /// <summary>
        ///     Initializes a new instance of the <see cref="DataRequest" /> class.
        /// </summary>
        /// <param name="command">The command.</param>
        public DataRequest(string command)
        {
            Command = command;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="DataRequest" /> class.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="commandType">Type of the command.</param>
        public DataRequest(string command, CommandType commandType)
            : this(command)
        {
            _commandType = commandType;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="DataRequest" /> class.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <param name="prepare">if set to <c>true</c> [prepare].</param>
        public DataRequest(string command, CommandType commandType, bool prepare)
            : this(command, commandType)
        {
            Prepare = prepare;
        }

        #endregion

        #region "Public Properties"

        /// <summary>
        ///     The AHDOC SQL statement to run against the data store.
        /// </summary>
        /// <remarks></remarks>
        public string Command { get; set; }

        /// <summary>
        ///     The timeout of the ADHOC request.
        /// </summary>
        /// <remarks></remarks>
        public int Timeout
        {
            get { return _timeout; }
            set { _timeout = value; }
        }

        /// <summary>
        ///     The optional parameters for the ADHOC request.
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public List<DataParameter> Params
        {
            get { return _params; }
            set { _params = value; }
        }

        /// <summary>
        ///     Gets or sets the type of the command.
        /// </summary>
        /// <value>
        ///     The type of the command.
        /// </value>
        public CommandType CommandType
        {
            get { return _commandType; }
            set { _commandType = value; }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether this <see cref="DataRequest" /> is prepare.
        /// </summary>
        /// <value>
        ///     <c>true</c> if prepare; otherwise, <c>false</c>.
        /// </value>
        public bool Prepare { get; set; }

        #endregion
    }
}