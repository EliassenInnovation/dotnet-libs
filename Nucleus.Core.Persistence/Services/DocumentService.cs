using MongoDB.Driver;
using Nucleus.Core.Contracts.Models.Filters;
using Nucleus.Core.Contracts.Persistence;
using Nucleus.Core.Persistence.Collections;
using Nucleus.Core.Shared.Persistence.Services.ServiceHelpers;
using Nucleus.External.Azure.StorageAccount;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nucleus.Core.Persistence.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly ICoreMongoDatabase _db;

        private readonly ProjectionDefinition<DocumentCollection, DocumentModel>? _documentProjection;
        private readonly BsonCollectionBuilder<DocumentModel, DocumentCollection> _documentCollectionBuilder;

        public DocumentService(
            ICoreMongoDatabase db
            )
        {
            _db = db;

            _documentCollectionBuilder = new BsonCollectionBuilder<DocumentModel, DocumentCollection>();
            _documentProjection = Builders<DocumentCollection>.Projection.Expression(item => new DocumentModel()
            {
                DocumentId = item.DocumentId,
                DocumentName = item.DocumentName,
                DocumentType = item.DocumentType,
                DocumentKey = item.DocumentKey,
                DocumentRepository = item.DocumentRepository,
                DocumentSize = item.documentSize,
                DocumentCategory = item.DocumentCategory,
                CreatedOn = item.CreatedOn
            });
        }

#warning Retire this
        private FilterDefinition<DocumentCollection> GetDocumentsPredicateBuilder(DocumentFilterItem? filterItems)
        {
            // Keeping this business logic in the access layer.  Cannot move it to the business layer yet
            // until I can create an extension that can translate for multiple database.  Moving this to db
            // layer forces you to include mongo drivers which will no longer make this a good solution to be
            // a hybrid database solution just by changing interfaces in the IOC
            var builder = Builders<DocumentCollection>.Filter;
            var filter = builder.Empty;

            // Document Id
            if (!string.IsNullOrWhiteSpace(filterItems?.DocumentId))
                filter &= builder.Where(r => r.DocumentId == filterItems.DocumentId);

            // Document Name
            if (!string.IsNullOrEmpty(filterItems?.DocumentName))
                filter &= builder.Where(r => r.DocumentName == filterItems.DocumentName);

            // Document Key
            if (!string.IsNullOrEmpty(filterItems?.DocumentKey))
                filter &= builder.Where(r => r.DocumentKey == filterItems.DocumentKey);

            // Created on or after
            if (filterItems?.CreatedOnAfter != null)
                filter &= builder.Where(r => r.CreatedOn >= filterItems.CreatedOnAfter);

            // Created on or before
            if (filterItems?.CreatedOnBefore != null)
                filter &= builder.Where(r => r.CreatedOn <= filterItems.CreatedOnBefore);

            return filter;
        }

#warning Retire this
        public async Task<DocumentModel?> GetDocumentAsync(DocumentsFilter filter) =>
            await _db.Documents.Find(GetDocumentsPredicateBuilder(filter.DocumentsFilters)).Project(_documentProjection).FirstOrDefaultAsync();

#warning Retire this
        public async Task<List<DocumentModel>?> GetDocumentsAsync(DocumentsFilter filter) =>
            await _db.Documents.Find(GetDocumentsPredicateBuilder(filter.DocumentsFilters)).Project(_documentProjection).ToListAsync();

        public async Task CreateAsync(DocumentModel document) =>
          await _db.Documents.InsertOneAsync(_documentCollectionBuilder.BuildCollection(document));

        public async Task UpdateAsync(DocumentModel document) =>
            await _db.Documents.ReplaceOneAsync(x => x.DocumentId == document.DocumentId, _documentCollectionBuilder.BuildCollection(document));

        public async Task RemoveAsync(string id) =>
            await _db.Documents.DeleteOneAsync(x => x.DocumentId == id);


    }
}
