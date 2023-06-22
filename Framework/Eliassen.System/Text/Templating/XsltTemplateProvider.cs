﻿using Eliassen.System.Reflection;
using System;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace Eliassen.System.Text.Templating
{
    /// <inheritdoc />
    public class XsltTemplateProvider : ITemplateProvider
    {
        /// <summary>
        /// HTTP Content Type Supported for this Provider
        /// </summary>
        public const string ContentType = "application/xslt+xml";

        /// <inheritdoc />
        public async Task<bool> ApplyAsync(ITemplateContext context, object data, Stream target)
        {
            using var readStream = context.OpenTemplate(context);

            var xslt = new XslCompiledTransform(false);
            using var styleSheet = XmlReader.Create(readStream, new XmlReaderSettings
            {
                DtdProcessing = DtdProcessing.Parse,
                ConformanceLevel = ConformanceLevel.Document,
                NameTable = new NameTable(),
            });
            var xsltSettings = new XsltSettings(false, false);
            xslt.Load(styleSheet);

            var navigator = await GetNavigatorAsync(data);

            using var writer = XmlWriter.Create(target, new XmlWriterSettings
            {
                CloseOutput = false,
            });
            xslt.Transform(navigator, writer);
            return true;
        }

        private async Task<IXPathNavigable> GetNavigatorAsync(object data)
        {
            if (data is IXPathNavigable navigable)
            {
                return navigable;
            }
            else if (data is JsonDocument jsonDocument)
            {
                data = jsonDocument.RootElement;
            }

            //if (data is JsonElement jsonElement)
            //{
            //    var built = jsonElement.ToXmlDocument();
            //    var builtNavigator = built?.CreateNavigator();
            //    if (builtNavigator != null)
            //        return builtNavigator;
            //}

            var serializer = new global::System.Xml.Serialization.XmlSerializer(data.GetType());
            using var xml = new MemoryStream();
            serializer.Serialize(xml, data);
            xml.Position = 0;
            var document = await XDocument.LoadAsync(xml, LoadOptions.None, CancellationToken.None);
            var navigator = document.CreateNavigator();
            return navigator;
        }

        /// <inheritdoc />
        public bool CanApply(ITemplateContext context) =>
            string.Equals(context.TemplateContentType, ContentType, StringComparison.InvariantCultureIgnoreCase);
    }
}
