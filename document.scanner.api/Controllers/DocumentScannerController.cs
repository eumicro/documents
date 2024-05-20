using document.scanner.model;
using document.scanner.model.Scan.Result;
using document.scanner.services.CQRS.Scanner.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DocumentScannerController : ControllerBase
    {

        private readonly ILogger<DocumentScannerController> _logger;
        private readonly IMediator mediator;

        public DocumentScannerController(ILogger<DocumentScannerController> logger, IMediator mediator)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost("scan2text")]
        public async Task<SimpleTextResult> SimpleScan([FromForm] FileUploadRequest model)
        {

            try
            {
                Document doc = new Document
                {
                    Data = await model.File?.GetBytes()
                };

                SimpleTextResult res = await mediator.Send(new ScanCommand<SimpleTextResult>(doc));

                return res;
            }
            catch(Exception ex)
            {
                _logger.LogWarning($"Scan failed: '{ex.Message}'");
                throw;
            }
        }

        [HttpPost("scan2textarray")]
        public async Task<SimpleTextArrayResult> SimpleScan2Array([FromForm] FileUploadRequest model)
        {

            try
            {
                Document doc = new Document
                {
                    Data = await model.File?.GetBytes()
                };

                SimpleTextArrayResult res = await mediator.Send(new ScanCommand<SimpleTextArrayResult>(doc));

                return res;
            }
            catch(Exception ex)
            {
                _logger.LogWarning($"Scan failed: '{ex.Message}'");
                throw;
            }
        }
    }

    public class FileUploadRequest
    {
        public IEnumerable<string>? Filter { get; set; }
        public IFormFile? File { get; set; }
    }

    public static class FormFileExtensions
    {
        public static async Task<byte[]> GetBytes(this IFormFile formFile)
        {
            await using MemoryStream memoryStream = new MemoryStream();
            await formFile.CopyToAsync(memoryStream);
            return memoryStream.ToArray();
        }
    }
}
