namespace document.scanner.model.Scan
{

    public class ScanJob
    {
        public ScanJob()
        {
            Status = ScanJobStatus.Active;
        }
        public ScanJobStatus Status { get; set; }
    }
}
