using MediatR;

namespace document.services.CQRS.Commands
{
    public record DeleteDocumentFileCommand(Guid DocumentID, Guid FileID) : IRequest;
}
