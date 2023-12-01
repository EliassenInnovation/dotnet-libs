using Eliassen.Azure.StorageAccount;
using Microsoft.Extensions.Configuration;
using Nucleus.Core.Contracts.Managers;
using Nucleus.Core.Contracts.Models;
using Nucleus.Core.Contracts.Models.Filters;
using Nucleus.Core.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nucleus.Core.Business.Managers
{
    public class DocumentManager : IDocumentManager
    {
        private readonly IConfiguration _config;
        private readonly IDocumentProvider _documentProvider;
        private readonly IDocumentService _documentService;
        public DocumentManager(
            IConfiguration config,
            IDocumentService documentService,
            IDocumentProvider documentProvider
            )
        {
            _config = config;
            _documentService = documentService;
            _documentProvider = documentProvider;
        }
#warning retire this
        public async Task<DocumentModel?> GetDocument(DocumentsFilter filter) =>
            await _documentService.GetDocumentAsync(filter);
#warning retire this
        public async Task<List<DocumentModel>?> GetDocuments(DocumentsFilter filter) =>
            await _documentService.GetDocumentsAsync(filter);

        public async Task<BlobDto?> DownloadDocument(string DocumentName) =>
            await _documentProvider.DownloadAsync(DocumentName);

        public async Task<ResponseModel<DocumentModel?>> SaveDocument(DocumentModel document, Stream content)
        {
            if (document == null
                || content == null
                || string.IsNullOrEmpty(document.DocumentName)
                )
                return new ResponseModel<DocumentModel?>()
                {
                    IsSuccess = false,
                    Message = "Missing Required Fields"
                };

            if (!string.IsNullOrEmpty(document.DocumentId))
                await _documentProvider.DeleteAsync(document.DocumentId);
            else
                document.DocumentKey = GetDocumentDirectory(document.DocumentCategory) + GenerateDocumentKey(document.DocumentName, 10);

            // Will make this more robust later through config, but for not we are capturing who owns this document 
            document.DocumentRepository = _config[ConfigKeys.Container.DefaultProvider];

            var response = await _documentProvider.UploadAsync(document, content);

            var result = new ResponseModel<DocumentModel?>();
            if (response.Error == false)
            {

                if (string.IsNullOrEmpty(document.DocumentId))
                {
                    document.CreatedOn = DateTimeOffset.Now;
                    await _documentService.CreateAsync(document);
                    result.Response = document;
                }
                else
                {
                    await _documentService.UpdateAsync(document);
                    result.Response = document;
                }
            }
            else
            {
                result.Message = response.Status;
                result.IsSuccess = false;
            }
            return result;
        }

        public async Task<ResponseModel<Boolean>> RemoveDocument(string id)
        {
            if (id == null)
                return new ResponseModel<Boolean>()
                {
                    Response = false,
                    IsSuccess = false,
                    Message = "Missing Required Fields"
                };
            var filter = new DocumentsFilter();
            var filterItem = new DocumentFilterItem
            {
                DocumentId = id
            };

            filter.DocumentsFilters = filterItem;


            var document = await _documentService.GetDocumentAsync(filter);
            if (document == null)
            {
                return new ResponseModel<Boolean>()
                {
                    Response = false,
                    IsSuccess = true
                };
            }
            if (document.DocumentKey != null)
                await _documentProvider.DeleteAsync(document.DocumentKey);
            await _documentService.RemoveAsync(id);
            return new ResponseModel<bool>()
            {
                Response = true,
                IsSuccess = true
            };
        }

        private string? GetDocumentDirectory(string? documentType)
        {
            if (documentType == null || _config.GetSection(ConfigKeys.Container.Directories._Base + documentType).Exists() == false)
                return _config[ConfigKeys.Container.Directories.Misc];

            return _config[ConfigKeys.Container.Directories._Base + documentType];
        }

        private static string GenerateDocumentKey(string documentName, int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string key = new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            string[] documentparts = documentName.Split('.');
            if (documentparts.Length > 0)
            {
                StringBuilder str = new StringBuilder();
                for (int i = 0; i < documentparts.Length; i++)
                {
                    if (i == documentparts.Length - 1)
                        str.Append("." + key);

                    if (i == 0)
                        str.Append(documentparts[i]);
                    else
                        str.Append("." + documentparts[i]);
                }
                return str.ToString();
            }
            else
                return documentName + "." + key;
        }
    }
}
