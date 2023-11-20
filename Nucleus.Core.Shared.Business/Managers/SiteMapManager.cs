using Nucleus.Core.Shared.Contracts.Managers;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Nucleus.Core.Shared.Business.Managers
{
    public class SiteMapManager : ISiteMapManager
    {
        //TODO: this this a better way.

        public SiteMapManager(
            ) 
        { 
        }

        public async Task<string> RenderSiteMap()
        {
            // Not many good ways to do this because it is reflection of the UI Routing files

           string host = "https://nucleusaccnetapi.azurewebsites.net";

           var output = new StringBuilder();

            using (var xml = XmlWriter.Create(output, new XmlWriterSettings { Indent = true }))
            {
                xml.WriteStartDocument();
                xml.WriteStartElement("urlset", "http://www.sitemaps.org/schemas/sitemap/0.9");

                // Home Page
                xml.WriteStartElement("url");
                xml.WriteElementString("loc", host);
                xml.WriteEndElement();

                xml.WriteEndElement();
            }

            return output.ToString();
        }
    }
}
