using document.file.repository.api.contracts;
using document.model;

namespace document.file.repository.minio
{
    public class DocumentFileRepository : IFileRepository
    {
        private readonly ISet<FileData> _files;

        public DocumentFileRepository()
        {
            _files = new HashSet<FileData>();
        }

        public async Task<DocumentFile> Add(DocumentFile file, byte[] data)
        {
            _files.Add(new FileData(file, data));
            return file;
        }

        public async Task Delete(Guid id)
        {
            FileData d = _files.FirstOrDefault(f => f.FileInfo.ID == id);
            if(d != null)
            {
                _files.Remove(d);
            }
        }

        public async Task<bool> Exists(Guid id)
        {
            return _files.Any(f => f.FileInfo.ID == id);
        }

        public async Task<byte[]?> Get(Guid id)
        {
            return _files.FirstOrDefault(f => f.FileInfo.ID == id)?.Data;
        }
    }

    internal class FileData : IEquatable<FileData?>
    {

        public DocumentFile FileInfo;
        public byte[] Data;

        public FileData(DocumentFile fileInfo, byte[] data)
        {
            FileInfo = fileInfo;
            Data = data;
        }
        public override bool Equals(object? obj)
        {
            return Equals(obj as FileData);
        }

        public bool Equals(FileData? other)
        {
            return other is not null &&
                   EqualityComparer<DocumentFile>.Default.Equals(FileInfo, other.FileInfo);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(FileInfo);
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
