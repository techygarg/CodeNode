using System;
using System.Data;

namespace CodeNode.Datalayer.Reader
{
    public interface INullSafeDataReader : IDataReader
    {
        IDataReader DataReader { get; }
        string GetString(string name);
        object GetValue(string name);
        int GetInt32(string name);
        double GetDouble(string name);
        Guid GetGuid(string name);
        bool HasColumn(string name);
        bool GetBoolean(string name);
        byte GetByte(string name);
        long GetBytes(string name, long fieldOffset, byte[] buffer, int bufferOffset, int length);
        char GetChar(string name);
        long GetChars(string name, long fieldOffset, char[] buffer, int bufferOffset, int length);
        IDataReader GetData(string name);
        string GetDataTypeName(string name);
        DateTime GetDateTime(string name);
        decimal GetDecimal(string name);
        Type GetFieldType(string name);
        float GetFloat(string name);
        short GetInt16(string name);
        long GetInt64(string name);
        bool IsDBNull(string name);
    }
}