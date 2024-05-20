using document.model;
using MediatR;

namespace document.services.CQRS.Queries
{
    public record ReadDocumentQuery(Guid ID) : IRequest<Document>;
}
