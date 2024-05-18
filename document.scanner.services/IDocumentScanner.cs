using document.scanner.model;
using document.scanner.model.Scan.Result;

namespace document.scanner.services
{
    public interface IDocumentScanner<TResult> where TResult : ScanResultBase
    {
        Task<TResult> Scan(Document document);
    }

    public class ScanFailedException : Exception
    {
        public ScanFailedException(string message = "Scan failed.") : base(message) { }
    }
}