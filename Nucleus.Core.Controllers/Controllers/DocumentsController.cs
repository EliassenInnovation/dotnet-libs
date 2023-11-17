using Eliassen.AspNetCore.Mvc.Filters;
using Eliassen.Azure.StorageAccount;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nucleus.Core.Contracts;
using Nucleus.Core.Contracts.Managers;
using Nucleus.Core.Contracts.Models;
using System.Threading.Tasks;

namespace Nucleus.Core.Controllers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly IDocumentManager _documentManager;

        public DocumentsController(IDocumentManager documentManager)
        {
            _documentManager = documentManager;
        }

        [HttpGet("{**id}")]
        [ResponseCache(VaryByHeader = "User-Agent", Duration = 604800)]
        public async Task<IActionResult> GetDocument(string id)
        {
            BlobDto? blob = await _documentManager.DownloadDocument(id);
            if (blob == null || blob.Content == null)
                return BadRequest();
            return File(blob.Content, blob.ContentType ?? "application/octet-stream", blob.Name);
        }

        [Authorize]
        [HttpPost]
        [ApplicationRight(Rights.Lesson.Manager, Rights.Blog.Manager, Rights.Project.Manager)]
        [ProducesResponseType(200, Type = typeof(ResponseModel<DocumentModel>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> SaveDocument([FromForm] DocumentModel document)
        {
            IFormFile? file = null;
            if (this.Request.HasFormContentType && this.Request.Form.Files.Count > 0)
                file = this.Request.Form.Files[0];

            if (file == null)
                return BadRequest();

            document.DocumentName = file.FileName;
            document.DocumentType = file.ContentType;
            document.DocumentSize = file.Length;

            return Ok(await _documentManager.SaveDocument(document, file.OpenReadStream()));
        }

        [Authorize]
        [HttpDelete("{**id}")]
        [ApplicationRight(Rights.Lesson.Manager, Rights.Blog.Manager, Rights.Project.Manager)]
        public async Task<ResponseModel<bool>> DeleteDocument(string id) =>
            await _documentManager.RemoveDocument(id);

    }
}
