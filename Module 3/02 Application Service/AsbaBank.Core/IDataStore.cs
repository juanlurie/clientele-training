using System;

namespace AsbaBank.Core
{
    public interface IDataStore : IDisposable
    {
        string DataStoreName { get; }
        bool IsDisposed { get; set; }
    }
}