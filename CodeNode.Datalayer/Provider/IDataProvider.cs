using System.Data;
using CodeNode.Datalayer.Reader;
using CodeNode.Datalayer.Request;

namespace CodeNode.Datalayer.Provider
{
    public interface IDataProvider
    {
        INullSafeDataReader ExecuteDataReader(AdhocRequest request);
        INullSafeDataReader ExecuteDataReader(AdhocRequest request, CommandBehavior behavior);
        object ExecuteScalar(AdhocRequest request);
        int ExecuteNonQuery(AdhocRequest request);
        T ExecuteNonQueryForOutParameter<T>(AdhocRequest request, string parametName);
        DataSet ExecuteDataSet(AdhocRequest request);
        DataTable ExecuteDataTable(AdhocRequest request);
        void BulkCopy(AdhocBulkRequest request);
    }
}