using document.model;
using document.services.DataContract;
using document.services.Exceptions;
using GeoJSON.Net;

namespace document.services
{
    public interface IDocumentService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="DocumentAlreadyExistsException">If a Document with the same ID already exists</exception>
        /// <param name="document"></param>
        /// <returns><see cref="Document"/></returns>
        Task<Document> Create(Document document);
        Task<Document> Create();
        Task<Document> Update(Document document);

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="DocumentNotFoundException"></exception>
        /// <param name="id"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        Task AddMetadata(Guid id, string key, object value);

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="MetadataNotFoundException"></exception>
        /// <exception cref="DocumentNotFoundException"></exception>
        /// <param name="id"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        Task UpdateMetadata(Guid id, string key, object value);
        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="MetadataNotFoundException"></exception>
        /// <exception cref="DocumentNotFoundException"></exception>
        /// <param name="id"></param>
        /// <param name="key"></param>
        Task DeleteMetadata(Guid id, string key);
        Task UpdateGeoJson(Guid id, IGeoJSONObject geoJSONObject);
        Task<Document> Read(Guid id);
        Task<IEnumerable<Document>> Find(DocumentQueryObject query);

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="DocumentNotFoundException">If given id is unknown.</exception>
        /// <param name="id"></param>
        Task Delete(Guid id);

        Task AddDocumentFile(Guid documentId, DocumentFile file, byte[] data);
        Task UpdateDocumentFile(Guid documentId, DocumentFile file, byte[] data);
        Task RemoveDocumentFile(Guid documentId, Guid fileId);
        Task<byte[]> GetDocumentFileData(Guid fileId);
    }
}
