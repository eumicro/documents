using MediatR;

namespace document.services.CQRS.Queries
{
    public record GetDocumentFileDataQuery(Guid FileID) : IRequest<byte[]>;
}
