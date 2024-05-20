using document.model;
using MediatR;

namespace document.services.CQRS.Commands
{
    public record AddDocumentFileCommand(Guid DocumentID, DocumentFile DocumentFile, byte[] Data) : IRequest;
}
