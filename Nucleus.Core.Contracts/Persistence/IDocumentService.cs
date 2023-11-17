using Eliassen.Azure.StorageAccount;
using Nucleus.Core.Contracts.Models.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nucleus.Core.Contracts.Persistence
{
    public interface IDocumentService
    {
#warning retire this
        Task<DocumentModel?> GetDocumentAsync(DocumentsFilter filter);
#warning retire this
        Task<List<DocumentModel>?> GetDocumentsAsync(DocumentsFilter filter);
        Task CreateAsync(DocumentModel document);
        Task UpdateAsync(DocumentModel document);
        Task RemoveAsync(string id);
    }
}
