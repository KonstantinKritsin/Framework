using System;
using System.IO;
using System.Text;

namespace Project.Common.ExternalContracts.Zip
{
    public interface ICompressor : IDisposable
    {
        int Count { get; }
        long ContentLength { get; }
        void Add(string name, byte[] content);
        void Add(string name, string content, Encoding encoding);
        void Add(string name, Stream content);
        void SaveResult(Stream outputStream);
    }
}