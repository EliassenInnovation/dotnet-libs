using Eliassen.Azure.StorageAccount;
using Nucleus.Core.Contracts.Models;
using Nucleus.Core.Contracts.Models.Filters;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Nucleus.Core.Contracts.Managers
{
    public interface IDocumentManager
    {
#warning retire this
        Task<DocumentModel?> GetDocument(DocumentsFilter filter);
#warning retire this
        Task<List<DocumentModel>?> GetDocuments(DocumentsFilter filter);

        Task<BlobDto?> DownloadDocument(string DocumentName);

        Task<ResponseModel<DocumentModel?>> SaveDocument(DocumentModel document, Stream content);

        Task<ResponseModel<bool>> RemoveDocument(string id);
    }
}
