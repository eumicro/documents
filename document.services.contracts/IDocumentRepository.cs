using document.model;
using System.Linq.Expressions;
using System.ServiceModel;

namespace document.repository.api.contracts
{
    [ServiceContract]
    public interface IDocumentRepository
    {
        [OperationContract]
        Task<Document?> Add(Document document);
        [OperationContract]
        Task<Document?> Read(Guid id);
        [OperationContract]
        Task<IEnumerable<Document>> Find(Expression<Func<Document, bool>> query);
        [OperationContract]
        Task<Document?> Update(Document document);
        [OperationContract]
        Task<bool> Delete(Guid id);
    }
}
