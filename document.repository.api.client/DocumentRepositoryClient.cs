using document.model;
using document.repository.api.contracts;
using Grpc.Net.Client;
using ProtoBuf.Grpc.Client;
using System.Linq.Expressions;

namespace document.repository.api.client
{
    public class DocumentRepositoryClient : IDocumentRepository, IDisposable
    {
        private readonly GrpcChannel channel;
        private readonly contracts.grpc.IDocumentRepository client;

        public DocumentRepositoryClient(string grpcAddress)
        {
            if(string.IsNullOrWhiteSpace(grpcAddress) || !grpcAddress.StartsWith("http"))
            {
                throw new ArgumentException("invalid url.");
            }

            channel = GrpcChannel.ForAddress(grpcAddress);
            client = channel.CreateGrpcService<contracts.grpc.IDocumentRepository>();
        }

        ~DocumentRepositoryClient()
        {
            channel.Dispose();
        }

        public async Task<Document?> Add(Document document)
        {
            return await client.Add(document);
        }

        public async Task<bool> Delete(Guid id)
        {
            return await client.Delete(id.ToString());
        }

        public void Dispose()
        {
            channel.Dispose();
        }

        public async Task<IEnumerable<Document>> Find(Expression<Func<Document, bool>> query)
        {
            return await client.Find(query);
        }

        public async Task<Document?> Read(Guid id)
        {
            return await client.Read(id.ToString());
        }

        public async Task<Document?> Update(Document document)
        {
            return await client.Update(document);
        }
    }
}
