using document.model;
using System.ServiceModel;

namespace document.file.repository.api.contracts.grpc
{

    [ServiceContract]
    public interface IFileRepository
    {
        [OperationContract]
        ValueTask<DocumentFile> Add(DocumentFile file, byte[] data);
        [OperationContract]
        ValueTask<byte[]?> Get(string id);
        [OperationContract]
        ValueTask<bool> Exists(string id);
        [OperationContract]
        ValueTask Delete(string id);
    }
}
