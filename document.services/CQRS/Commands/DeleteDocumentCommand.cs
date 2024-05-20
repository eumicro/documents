using MediatR;

namespace document.services.CQRS.Commands
{
    public record DeleteDocumentCommand(Guid DocumentID) : IRequest;
}
