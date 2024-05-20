using document.model;
using System.ServiceModel;

namespace document.file.repository.api.contracts
{

    [ServiceContract]
    public interface IFileRepository
    {
        [OperationContract]
        Task<DocumentFile> Add(DocumentFile file, byte[] data);
        [OperationContract]
        Task<byte[]?> Get(Guid id);
        [OperationContract]
        Task<bool> Exists(Guid id);
        [OperationContract]
        Task Delete(Guid id);
    }
}
