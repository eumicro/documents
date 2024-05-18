using document.scanner.model;
using document.scanner.model.Scan.Result;
using MediatR;

namespace document.scanner.services.CQRS.Scanner.Commands
{
    public record ScanCommand<TResut>(Document Document) : IRequest<TResut> where TResut : ScanResultBase;
}
