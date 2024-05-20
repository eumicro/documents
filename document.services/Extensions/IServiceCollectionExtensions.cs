using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace document.services.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddDocumentService(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IDocumentService, DocumentService>();
            serviceCollection.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });
        }
    }
}
