using MediatR;

namespace document.services.CQRS.Commands
{
    public record AddDocumentMetadataCommand(Guid DocumentID, string MetadataKey, object MetadataValue) : IRequest;
}
