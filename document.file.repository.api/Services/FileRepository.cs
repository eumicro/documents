using document.model;

namespace document.file.repository.api.Services
{
    public class FileRepository : contracts.grpc.IFileRepository
    {
        private readonly ILogger<FileRepository> _logger;

        private readonly ISet<FileData> fileData;
        public FileRepository(ILogger<FileRepository> logger)
        {
            _logger = logger;
            fileData = new HashSet<FileData>();
        }

        public async ValueTask<DocumentFile> Add(DocumentFile file, byte[] data)
        {
            if(fileData.Add(new FileData { DocumentFile = file, Data = data }))
            {
                return file;
            }
            else
            {
                return null;
            }
        }

        public async ValueTask Delete(string id)
        {
            FileData? d = fileData.SingleOrDefault(fd => fd.DocumentFile.ID.ToString().Equals(id));
            if(d != null)
            {
                fileData.Remove(d);
            }
        }

        public async ValueTask<bool> Exists(string id)
        {
            return fileData.Any(fd => fd.DocumentFile.ID.ToString() == id);
        }

        public async ValueTask<byte[]?> Get(string id)
        {
            return fileData.FirstOrDefault(fd => fd.DocumentFile.ID.ToString().Equals(id))?.Data;
        }
    }

    internal class FileData : IEquatable<FileData?>
    {
        public DocumentFile DocumentFile { get; set; }
        public byte[] Data { get; set; }

        public override bool Equals(object? obj)
        {
            return Equals(obj as FileData);
        }

        public bool Equals(FileData? other)
        {
            return other is not null &&
                   EqualityComparer<DocumentFile>.Default.Equals(DocumentFile, other.DocumentFile);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(DocumentFile);
        }

        public static bool operator ==(FileData? left, FileData? right)
        {
            return EqualityComparer<FileData>.Default.Equals(left, right);
        }

        public static bool operator !=(FileData? left, FileData? right)
        {
            return !(left == right);
        }
    }
}
