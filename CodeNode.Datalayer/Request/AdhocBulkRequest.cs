using System.Data;

namespace CodeNode.Datalayer.Request
{
    public class AdhocBulkRequest
    {
        private readonly string _destinaitonTable;
        private readonly DataTable _sourceTable;
        private readonly int _timeOut = 500;

        public AdhocBulkRequest(DataTable source, string destinationTable)
        {
            _sourceTable = source;
            _destinaitonTable = destinationTable;
        }

        public AdhocBulkRequest(DataTable source, string destinationTable, int timeOut) : this(source, destinationTable)
        {
            _timeOut = timeOut;
        }

        public AdhocBulkRequest(DataTable source, string destinationTable, int timeOut, int batchSize)
            : this(source, destinationTable, timeOut)
        {
            BatchSize = batchSize;
        }

        public DataTable SourceTable
        {
            get { return _sourceTable; }
        }

        public string DestinaitionTable
        {
            get { return _destinaitonTable; }
        }

        public int TimeOut
        {
            get { return _timeOut; }
            //set { _timeOut = value; }
        }

        public int BatchSize { get; private set;
            //set { _batchSize = value; }
        }
    }
}