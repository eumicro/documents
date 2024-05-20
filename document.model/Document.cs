using GeoJSON.Net;
using System.Runtime.Serialization;

namespace document.model
{
    [DataContract]
    public class Document : IEquatable<Document?>
    {
        public Document()
        {
            ID = Guid.NewGuid();
        }

        [DataMember(Order = 0)]
        public Guid ID { get; set; }

        [DataMember(Order = 1)]
        public IDictionary<string, object>? Properties { get; set; }

        [DataMember(Order = 2)]
        public IGeoJSONObject? GeoJson { get; set; }

        [DataMember(Order = 3)]
        public ICollection<DocumentFile>? Files { get; set; }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Document);
        }

        public bool Equals(Document? other)
        {
            return other is not null &&
                   ID.Equals(other.ID);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ID);
        }

        public static bool operator ==(Document? left, Document? right)
        {
            return EqualityComparer<Document>.Default.Equals(left, right);
        }

        public static bool operator !=(Document? left, Document? right)
        {
            return !(left == right);
        }
    }
}
