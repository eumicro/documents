using MediatR;

namespace document.services.CQRS.Commands
{
    public record DeleteDocumentMetadataCommand(Guid DocumentID, string MetadataKey) : IRequest;
}
