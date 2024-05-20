using MediatR;

namespace document.services.CQRS.Commands
{
    public record UpdateDocumentMetadataCommand(Guid DocumentID, string MetadataKey, object MetadataValue) : IRequest;
}
