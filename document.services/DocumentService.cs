using document.file.repository.api.contracts;
using document.model;
using document.repository.api.contracts;
using document.services.DataContract;
using document.services.Exceptions;
using GeoJSON.Net;

namespace document.services
{
    public class DocumentService : IDocumentService
    {
        private readonly IFileRepository fileRepository;
        private readonly IDocumentRepository documentRepository;
        public DocumentService(IFileRepository fileRepository, IDocumentRepository documentRepository)
        {
            this.fileRepository = fileRepository ?? throw new ArgumentNullException(nameof(fileRepository));
            this.documentRepository = documentRepository ?? throw new ArgumentNullException(nameof(documentRepository));
        }

        public async Task AddDocumentFile(Guid documentId, DocumentFile file, byte[] data)
        {
            Document doc = await Read(documentId);
            await fileRepository.Add(file, data);
            doc.Files.Add(file);
            await Update(doc);
        }

        public async Task AddMetadata(Guid id, string key, object value)
        {
            Document doc = await Read(id);
            if(doc.Properties.ContainsKey(key))
            {
                throw new MetadataAlreadyExistsException();
            }
            else
            {
                doc.Properties.Add(key, value);
            }
            await Update(doc);
        }

        public async Task<Document> Create(Document document)
        {
            if((document == await documentRepository.Add(document)) == null)
            {
                throw new DocumentAlreadyExistsException();
            }
            return document;
        }

        public Task<Document> Create()
        {
            return documentRepository.Add(new Document());
        }

        public async Task Delete(Guid id)
        {
            if(!await documentRepository.Delete(id))
            {
                throw new DocumentNotFoundException();
            }
        }

        public async Task DeleteMetadata(Guid id, string key)
        {
            Document doc = await Read(id);
            if(doc.Properties.ContainsKey(key))
            {
                doc.Properties.Remove(key);
                await Update(doc);
            }
            else
            {
                throw new MetadataNotFoundException();
            };
        }

        public async Task<IEnumerable<Document>> Find(DocumentQueryObject query)
        {
            return (await documentRepository.Find(query.Query)).ToArray();
        }

        public async Task<byte[]> GetDocumentFileData(Guid fileId)
        {
            return await fileRepository.Get(fileId) ?? throw new DocumentFileDataNotFoundException();
        }

        public async Task<Document> Read(Guid id)
        {
            return await documentRepository.Read(id) ?? throw new DocumentNotFoundException();

        }

        public async Task RemoveDocumentFile(Guid documentId, Guid fileId)
        {
            Document doc = await Read(documentId);

            DocumentFile? file = doc.Files.FirstOrDefault(f => f.ID == fileId);

            if(file != null)
            {
                doc.Files.Remove(file);
            }

            await Update(doc);
        }

        public async Task<Document> Update(Document document)
        {
            return await documentRepository.Update(document) ?? throw new DocumentNotFoundException();
        }

        public async Task UpdateDocumentFile(Guid documentId, DocumentFile file, byte[] data)
        {
            Document doc = await Read(documentId);
            if(doc.Files.Remove(file))
            {
                doc.Files.Add(file);
            }
            await fileRepository.Delete(file.ID);
            await fileRepository.Add(file, data);
            await Update(doc);
        }

        public async Task UpdateGeoJson(Guid id, IGeoJSONObject geoJSONObject)
        {
            Document doc = await Read(id);
            doc.GeoJson = geoJSONObject;
            await Update(doc);
        }

        public async Task UpdateMetadata(Guid id, string key, object value)
        {
            Document doc = await Read(id);
            if(doc.Properties.ContainsKey(key))
            {
                doc.Properties[key] = value;
                await Update(doc);
            }
            else
            {
                throw new MetadataNotFoundException();
            }
        }
    }
}
