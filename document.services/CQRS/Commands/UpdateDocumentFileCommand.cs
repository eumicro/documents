using document.model;
using MediatR;

namespace document.services.CQRS.Commands
{
    public record UpdateDocumentFileCommand(Guid DocumentID, DocumentFile DocumentFile, byte[] Data) : IRequest;
}
