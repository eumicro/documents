using document.file.repository.api.contracts;
using document.model;
using Grpc.Net.Client;
using ProtoBuf.Grpc.Client;

namespace document.repository.api.client
{
    public class FileRepositoryClient : IFileRepository, IDisposable
    {
        private readonly GrpcChannel channel;
        private readonly document.file.repository.api.contracts.grpc.IFileRepository client;

        public FileRepositoryClient(string grpcAddress)
        {
            if(string.IsNullOrWhiteSpace(grpcAddress) || !grpcAddress.StartsWith("http"))
            {
                throw new ArgumentException("invalid url.");
            }

            channel = GrpcChannel.ForAddress(grpcAddress);
            client = channel.CreateGrpcService<document.file.repository.api.contracts.grpc.IFileRepository>();
        }

        ~FileRepositoryClient()
        {
            channel.Dispose();
        }

        public async Task<DocumentFile> Add(DocumentFile file, byte[] data)
        {
            return await client.Add(file, data);
        }

        public async Task Delete(Guid id)
        {
            await client.Delete(id.ToString());
        }

        public void Dispose()
        {
            channel.Dispose();
        }

        public async Task<bool> Exists(Guid id)
        {
            return await client.Exists(id.ToString());
        }

        public async Task<byte[]?> Get(Guid id)
        {
            return await client.Get(id.ToString());
        }
    }
}
