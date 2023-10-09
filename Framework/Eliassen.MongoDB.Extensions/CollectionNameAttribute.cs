using System;

namespace Eliassen.MongoDB.Extensions
{
    /// <summary>
    /// declarative attribute for labeling properties as MongoDB Collections
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class, AllowMultiple = false)]
    public class CollectionNameAttribute : Attribute
    {
        /// <summary>
        /// Name to use for MongoDB collection
        /// </summary>
        public string CollectionName { get; }

        /// <inheritdoc/>
        public CollectionNameAttribute (
            string collectionName)
        {
            CollectionName = collectionName;
        }

        /// <inheritdoc/>
        public override object TypeId => this;
    }
}