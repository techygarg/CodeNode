using System.Data;
using CodeNode.Datalayer.Reader;
using CodeNode.Datalayer.Request;

namespace CodeNode.Datalayer.Provider
{
    public interface IDataProvider
    {
        INullSafeDataReader ExecuteDataReader(DataRequest request);
        INullSafeDataReader ExecuteDataReader(DataRequest request, CommandBehavior behavior);
        object ExecuteScalar(DataRequest request);
        int ExecuteNonQuery(DataRequest request);
        T ExecuteNonQueryForOutParameter<T>(DataRequest request, string parametName);
        DataSet ExecuteDataSet(DataRequest request);
        DataTable ExecuteDataTable(DataRequest request);
        void ExecuteBulkCopy(DataBulkRequest request);
    }
}