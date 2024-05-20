using document.model;
using document.services.CQRS.Commands;
using document.services.CQRS.Queries;
using MediatR;

namespace document.services.CQRS.Handlers
{
    internal class SimpleDocumentServiceHandler :
        IRequestHandler<CreateNewDocumentCommand, Document>,
        IRequestHandler<CreateDocumentCommand, Document>,
        IRequestHandler<UpdateDocumentCommand, Document>,
        IRequestHandler<DeleteDocumentCommand>,
        IRequestHandler<ReadDocumentQuery, Document>,
        IRequestHandler<FindDocumentsQuery, IEnumerable<Document>>,
        IRequestHandler<AddDocumentMetadataCommand>,
        IRequestHandler<UpdateDocumentMetadataCommand>,
        IRequestHandler<DeleteDocumentMetadataCommand>,
        IRequestHandler<AddDocumentFileCommand>,
        IRequestHandler<UpdateDocumentFileCommand>,
        IRequestHandler<GetDocumentFileDataQuery, byte[]>,
        IRequestHandler<DeleteDocumentFileCommand>
    {
        private readonly IDocumentService documentService;

        public SimpleDocumentServiceHandler(IDocumentService documentService)
        {
            this.documentService = documentService ?? throw new ArgumentNullException(nameof(documentService));
        }

        public async Task<Document> Handle(CreateDocumentCommand request, CancellationToken cancellationToken)
        {
            return await documentService.Create(request.Document);
        }

        public async Task<Document> Handle(UpdateDocumentCommand request, CancellationToken cancellationToken)
        {
            return await documentService.Update(request.Document);
        }

        public async Task Handle(DeleteDocumentCommand request, CancellationToken cancellationToken)
        {
            await documentService.Delete(request.DocumentID);
        }

        public async Task<Document> Handle(CreateNewDocumentCommand request, CancellationToken cancellationToken)
        {
            return await documentService.Create();
        }

        public async Task<Document> Handle(ReadDocumentQuery request, CancellationToken cancellationToken)
        {
            return await documentService.Read(request.ID);
        }

        public async Task<IEnumerable<Document>> Handle(FindDocumentsQuery request, CancellationToken cancellationToken)
        {
            return await documentService.Find(request.Query);
        }

        public async Task Handle(AddDocumentMetadataCommand request, CancellationToken cancellationToken)
        {
            await documentService.AddMetadata(request.DocumentID, request.MetadataKey, request.MetadataValue);
        }

        public async Task Handle(DeleteDocumentMetadataCommand request, CancellationToken cancellationToken)
        {
            await documentService.DeleteMetadata(request.DocumentID, request.MetadataKey);
        }

        public async Task Handle(UpdateDocumentMetadataCommand request, CancellationToken cancellationToken)
        {
            await documentService.UpdateMetadata(request.DocumentID, request.MetadataKey, request.MetadataValue);
        }

        public async Task Handle(AddDocumentFileCommand request, CancellationToken cancellationToken)
        {
            await documentService.AddDocumentFile(request.DocumentID, request.DocumentFile, request.Data);
        }

        public async Task<byte[]> Handle(GetDocumentFileDataQuery request, CancellationToken cancellationToken)
        {
            return await documentService.GetDocumentFileData(request.FileID);
        }

        public async Task Handle(DeleteDocumentFileCommand request, CancellationToken cancellationToken)
        {
            await documentService.RemoveDocumentFile(request.DocumentID, request.FileID);
        }

        public async Task Handle(UpdateDocumentFileCommand request, CancellationToken cancellationToken)
        {
            await documentService.UpdateDocumentFile(request.DocumentID, request.DocumentFile, request.Data);
        }
    }
}
