using document.model;
using document.repository.api.contracts;
using System.Linq.Expressions;

namespace document.repository.mongodb
{
    public class DocumentRepositoryMongoDB : IDocumentRepository
    {
        private readonly ISet<Document> _documents;

        public DocumentRepositoryMongoDB()
        {
            _documents = new HashSet<Document>();
        }

        public async Task<Document?> Add(Document document)
        {
            if(_documents.Add(document))
            {
                return document;
            }

            return null;
        }

        public async Task<bool> Delete(Guid id)
        {
            try
            {

                Document doc = _documents.Single(d => d.ID == id);
                return _documents.Remove(doc);
            }
            catch(Exception)
            {

                return false;
            }
        }

        public async Task<IEnumerable<Document>> Find(Expression<Func<Document, bool>> query)
        {
            return _documents.Where(query.Compile()).ToList();
        }

        public async Task<Document?> Read(Guid id)
        {
            return _documents.SingleOrDefault(d => d.ID == id);
        }

        public async Task<Document?> Update(Document document)
        {
            try
            {
                Document? doc = await Read(document.ID);
                _documents.Remove(doc);
                _documents.Add(document);
                return document;
            }
            catch(Exception)
            {
                return null;
            }
        }
    }
}
