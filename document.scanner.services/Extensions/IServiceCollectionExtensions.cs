using document.scanner.model.Scan.Result;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace document.scanner.services.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddDocumentScannerServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IDocumentScanner<SimpleTextResult>, SimpleOCRScanner>();
            serviceCollection.AddSingleton<IDocumentScanner<SimpleTextArrayResult>, SimpleOCRScanner>();
            serviceCollection.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });
        }
    }
}
