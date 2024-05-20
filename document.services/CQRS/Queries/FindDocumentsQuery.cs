using document.model;
using document.services.DataContract;
using MediatR;

namespace document.services.CQRS.Queries
{
    public record FindDocumentsQuery(DocumentQueryObject Query) : IRequest<IEnumerable<Document>>;
}
