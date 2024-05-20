
using System.Runtime.Serialization;

namespace document.model
{

    [DataContract]
    public class DocumentFile : IEquatable<DocumentFile?>
    {
        public DocumentFile()
        {
            ID = Guid.NewGuid();
        }

        [DataMember(Order = 0)]
        public Guid ID { get; set; }

        [DataMember(Order = 1)]
        public string DisplayName { get; set; }

        [DataMember(Order = 2)]
        public string Extension { get; set; }

        public override bool Equals(object? obj)
        {
            return Equals(obj as DocumentFile);
        }

        public bool Equals(DocumentFile? other)
        {
            return other is not null &&
                   ID.Equals(other.ID);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ID);
        }

        public static bool operator ==(DocumentFile? left, DocumentFile? right)
        {
            return EqualityComparer<DocumentFile>.Default.Equals(left, right);
        }

        public static bool operator !=(DocumentFile? left, DocumentFile? right)
        {
            return !(left == right);
        }
    }
}
