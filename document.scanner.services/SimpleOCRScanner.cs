using document.scanner.model;
using document.scanner.model.Scan.Result;
using document.scanner.services.CQRS.Scanner.Commands;
using MediatR;
using System.Reflection;
using Tesseract;
namespace document.scanner.services
{
    public class SimpleOCRScanner : IDocumentScanner<SimpleTextResult>, IDocumentScanner<SimpleTextArrayResult>, IRequestHandler<ScanCommand<SimpleTextResult>, SimpleTextResult>, IRequestHandler<ScanCommand<SimpleTextArrayResult>, SimpleTextArrayResult>
    {
        private TesseractEngine engine;

        public SimpleOCRScanner()
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "tessdata");
            bool exists = Path.Exists(path);
            engine = new TesseractEngine(path, "deu");

        }

        public async Task<SimpleTextResult> Handle(ScanCommand<SimpleTextResult> request, CancellationToken cancellationToken)
        {
            return await Scan(request.Document);
        }

        public async Task<SimpleTextArrayResult> Handle(ScanCommand<SimpleTextArrayResult> request, CancellationToken cancellationToken)
        {
            return await ScanToArray(request.Document);
        }

        public async Task<SimpleTextResult> Scan(Document document)
        {
            return await ScanToText(document);
        }

        async Task<SimpleTextArrayResult> IDocumentScanner<SimpleTextArrayResult>.Scan(Document document)
        {
            return await ScanToArray(document);
        }

        private async Task<SimpleTextArrayResult> ScanToArray(Document document)
        {
            SimpleTextResult result = await ScanToText(document);

            return new SimpleTextArrayResult
            {
                Text = result?.Text?.Split("\n")?.Where(t => !string.IsNullOrWhiteSpace(t)).ToArray()
            };
        }

        private async Task<SimpleTextResult> ScanToText(Document document)
        {
            try
            {
                Pix data = Pix.LoadFromMemory(document.Data);
                Page page = engine.Process(data);

                string text = page.GetText();

                return new SimpleTextResult
                {
                    Text = text
                };
            }
            catch(Exception ex)
            {
                throw new ScanFailedException($"Scan failed due to internal Error: '{ex.Message}'");
            }
        }
    }
}
