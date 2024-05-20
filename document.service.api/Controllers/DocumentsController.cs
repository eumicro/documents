using document.model;
using document.services.CQRS.Commands;
using document.services.CQRS.Queries;
using document.services.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace document.service.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DocumentsController : ControllerBase
    {
        private readonly IMediator mediator;

        public DocumentsController(IMediator mediator)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("/document/{documentID}")]
        public async Task<ActionResult<Document>> Get(Guid documentID)
        {
            try
            {
                return await mediator.Send(new ReadDocumentQuery(documentID));
            }
            catch(DocumentNotFoundException)
            {
                return NotFound($"Document not found.");
            }
            catch(Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, $"Server response: '{ex.Message}'");
            }
        }

        [HttpPost("/document/createnew")]
        public async Task<ActionResult<Document>> Create()
        {
            try
            {
                return await mediator.Send(new CreateNewDocumentCommand());
            }
            catch(Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, $"Server response: '{ex.Message}'");
            }
        }

        [HttpPost("/document/create")]
        public async Task<ActionResult<Document>> Create(Document document)
        {
            try
            {
                if(document == null)
                {
                    return UnprocessableEntity("Document must not be empty.");
                }

                return await mediator.Send(new CreateDocumentCommand(document));
            }
            catch(DocumentAlreadyExistsException)
            {
                return Conflict($"Document with id: '{document.ID}' aleready exists.");
            }
            catch(Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, $"Server response: '{ex.Message}'");
            }
        }

        [HttpPut("/document/{documentID}")]
        public async Task<ActionResult<Document>> Update(Guid documentID, Document document)
        {
            try
            {
                if(document == null)
                {
                    return UnprocessableEntity("Document must not be empty.");
                }

                if(document.ID != documentID)
                {
                    return UnprocessableEntity($"Document ID '{document.ID}' != '{documentID}'.");
                }

                return await mediator.Send(new UpdateDocumentCommand(document));
            }
            catch(DocumentNotFoundException)
            {
                return NotFound($"Document not found.");
            }
            catch(Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, $"Server response: '{ex.Message}'");
            }
        }

        [HttpDelete("/document/{documentID}")]
        public async Task<ActionResult> Delete(Guid documentID)
        {
            try
            {
                await mediator.Send(new DeleteDocumentCommand(documentID));
                return Ok();
            }
            catch(DocumentNotFoundException)
            {
                return NotFound($"Document not found.");
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Server response: '{ex.Message}'");
            }
        }

        [HttpPost("/document/{documentID}/metadata/{metadataKey}")]
        public async Task<ActionResult> AddMetadata(Guid documentID, string metadataKey, object metadataValue)
        {
            try
            {
                await mediator.Send(new AddDocumentMetadataCommand(documentID, metadataKey, metadataValue));
                return Ok();
            }
            catch(MetadataAlreadyExistsException)
            {
                return BadRequest($"Metadata with key '{metadataKey}' already exists.");
            }
            catch(DocumentNotFoundException)
            {
                return NotFound($"Document not found.");
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Server response: '{ex.Message}'");
            }
        }

        [HttpPut("/document/{documentID}/metadata/{metadataKey}")]
        public async Task<ActionResult> UpdateMetadata(Guid documentID, string metadataKey, object metadataValue)
        {
            try
            {
                await mediator.Send(new UpdateDocumentMetadataCommand(documentID, metadataKey, metadataValue));
                return Ok();
            }
            catch(MetadataNotFoundException)
            {
                return BadRequest($"Metadata with key '{metadataKey}' not found.");
            }
            catch(DocumentNotFoundException)
            {
                return NotFound($"Document not found.");
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Server response: '{ex.Message}'");
            }
        }

        [HttpDelete("/document/{documentID}/metadata/{metadataKey}")]
        public async Task<ActionResult> DeleteMetadata(Guid documentID, string metadataKey)
        {
            try
            {
                await mediator.Send(new DeleteDocumentMetadataCommand(documentID, metadataKey));
                return Ok();
            }
            catch(MetadataNotFoundException)
            {
                return BadRequest($"Metadata with key '{metadataKey}' not found.");
            }
            catch(DocumentNotFoundException)
            {
                return NotFound($"Document not found.");
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Server response: '{ex.Message}'");
            }
        }

        [HttpPost("/document/{documentID}/file")]
        public async Task<ActionResult> AddDocumentFile(Guid documentID, string fileName, IFormFile fileData)
        {
            try
            {
                byte[] data = await fileData.GetBytes();
                string displayName = fileName.Split(".")[0];
                string extension = fileName.Split(".")[1];
                await mediator.Send(new AddDocumentFileCommand(documentID, new DocumentFile
                {
                    DisplayName = displayName,
                    Extension = extension,
                }, data));
                return Ok();
            }
            catch(DocumentNotFoundException)
            {
                return NotFound($"Document not found.");
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Server response: '{ex.Message}'");
            }
        }

        [HttpPut("/document/{documentID}/file/{fileID}")]
        public async Task<ActionResult> UpdateDocumentFile(Guid documentID, Guid fileID, string fileName, IFormFile fileData)
        {
            try
            {
                byte[] data = await fileData.GetBytes();
                string displayName = fileName.Split(".")[0];
                string extension = fileName.Split(".")[1];
                await mediator.Send(new UpdateDocumentFileCommand(documentID, new DocumentFile { ID = fileID, DisplayName = displayName, Extension = extension }, data));
                return Ok();
            }
            catch(DocumentFileDataNotFoundException)
            {
                return NotFound($"File with ID '{fileID}' not found.");
            }
            catch(DocumentNotFoundException)
            {
                return NotFound($"Document not found.");
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Server response: '{ex.Message}'");
            }
        }

        [HttpDelete("/document/{documentID}/file/{fileID}")]
        public async Task<ActionResult> DeleteDocumentFileData(Guid documentID, Guid fileID)
        {
            try
            {
                await mediator.Send(new DeleteDocumentFileCommand(documentID, fileID));
                return Ok();
            }
            catch(MetadataNotFoundException)
            {
                return BadRequest($"File with ID '{fileID}' not found.");
            }
            catch(DocumentNotFoundException)
            {
                return NotFound($"Document not found.");
            }
            catch(DocumentFileDataNotFoundException)
            {
                return NotFound($"File not found.");
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Server response: '{ex.Message}'");
            }
        }

        [HttpGet("/document/{documentID}/file/{fileID}")]
        public async Task<ActionResult<byte[]>> GetDocumentFileData(Guid documentID, Guid fileID)
        {
            try
            {
                Document doc = await mediator.Send(new ReadDocumentQuery(documentID));
                DocumentFile? docFile = doc.Files.FirstOrDefault(f => f.ID == fileID);
                byte[] data = await mediator.Send(new GetDocumentFileDataQuery(fileID));
                Response.Headers.Add("content-disposition", $"attachment; filename={docFile.DisplayName}.{docFile.Extension}");
                return File(data, "application/octet-stream");
            }
            catch(DocumentNotFoundException)
            {
                return NotFound($"Document not found.");
            }
            catch(DocumentFileDataNotFoundException)
            {
                return NotFound($"File not found.");
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Server response: '{ex.Message}'");
            }
        }
    }
    public record AddOrUpdateDocumentFileRequestModel(DocumentFile DocumentFile, IFormFile FileData);
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
