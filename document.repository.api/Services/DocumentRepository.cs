using document.model;
using document.repository.api.contracts;
using System.Linq.Expressions;

namespace document.repository.api.Services
{
    public class DocumentRepositoryService : contracts.grpc.IDocumentRepository
    {
        private readonly ILogger<DocumentRepositoryService> _logger;
        private readonly IDocumentRepository repository;

        public DocumentRepositoryService(ILogger<DocumentRepositoryService> logger, IDocumentRepository repository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async ValueTask<Document?> Add(Document document)
        {
            return await repository.Add(document);
        }

        public async ValueTask<bool> Delete(string id)
        {
            return await repository.Delete(Guid.Parse(id));
        }

        public async ValueTask<IEnumerable<Document>> Find(Expression<Func<Document, bool>> query)
        {
            return await repository.Find(query);
        }

        public async ValueTask<Document?> Read(string id)
        {
            return await repository.Read(Guid.Parse(id));
        }

        public async ValueTask<Document?> Update(Document document)
        {
            return await repository.Update(document);
        }
    }
}
