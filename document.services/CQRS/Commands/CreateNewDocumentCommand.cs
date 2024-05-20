using document.model;
using MediatR;

namespace document.services.CQRS.Commands
{
    public record CreateNewDocumentCommand : IRequest<Document>;
}
