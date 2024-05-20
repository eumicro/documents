using document.model;
using MediatR;

namespace document.services.CQRS.Commands
{
    public record CreateDocumentCommand(Document Document) : IRequest<Document>;
}
