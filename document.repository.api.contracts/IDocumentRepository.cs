using document.model;
using System.Linq.Expressions;
using System.ServiceModel;

namespace document.repository.api.contracts.grpc
{
    [ServiceContract]
    public interface IDocumentRepository
    {
        [OperationContract]
        ValueTask<Document?> Add(Document document);
        [OperationContract]
        ValueTask<Document?> Read(string id);
        [OperationContract]
        ValueTask<IEnumerable<Document>> Find(Expression<Func<Document, bool>> query);
        [OperationContract]
        ValueTask<Document?> Update(Document document);
        [OperationContract]
        ValueTask<bool> Delete(string id);
    }
}
