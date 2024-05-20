using document.model;
using MediatR;

namespace document.services.CQRS.Commands
{
    public record UpdateDocumentCommand(Document Document) : IRequest<Document>;
}
