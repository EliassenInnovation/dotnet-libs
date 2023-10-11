using Nucleus.Blog.Contracts.Managers;
using Nucleus.Blog.Contracts.Models;
using Nucleus.Core.Shared.Contracts.Managers;
using Nucleus.Lesson.Contracts.Managers;
using Nucleus.Lesson.Contracts.Models;
using Nucleus.Project.Contracts.Managers;
using Nucleus.Project.Contracts.Models;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Nucleus.Core.Shared.Business.Managers
{
    public class SiteMapManager : ISiteMapManager
    {
        //TODO: this this a better way.

        private readonly IPublicBlogManager _publicBlogManager;
        private readonly IPublicLessonScheduleManager _publicLessonScheduleManager;
        private readonly IPublicLessonManager _publicLessonManager;
        private readonly IPublicProjectManager _publicProjectManager;

        public SiteMapManager(
                  IPublicBlogManager publicBlogManager,
                  IPublicLessonScheduleManager publicLessonScheduleManager, 
                  IPublicLessonManager publicLessonManager,
                  IPublicProjectManager publicProjectManager
            ) 
        { 
            _publicBlogManager = publicBlogManager;
            _publicLessonScheduleManager = publicLessonScheduleManager;
            _publicLessonManager = publicLessonManager;
            _publicProjectManager = publicProjectManager;
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

                // Blogs
                xml.WriteStartElement("url");
                xml.WriteElementString("loc", host + "/blogs");
                xml.WriteEndElement();

                foreach (BlogModel blog in await _publicBlogManager.GetBlogs())
                {
                    xml.WriteStartElement("url");
                    xml.WriteElementString("loc", host + "/blog/" + blog.Slug);
                    xml.WriteEndElement();
                }

                // LessonsAdmin
                xml.WriteStartElement("url");
                xml.WriteElementString("loc", host + "/lessonSchedule");
                xml.WriteEndElement();

                foreach (LessonScheduleModel lesson in await _publicLessonScheduleManager.GetLessons())
                {
                    xml.WriteStartElement("url");
                    xml.WriteElementString("loc", host + "/lessonSchedule/");
                    xml.WriteEndElement();
                }

                // Lessons
                xml.WriteStartElement("url");
                xml.WriteElementString("loc", host + "/lessons");
                xml.WriteEndElement();

                foreach (LessonModel lesson in await _publicLessonManager.GetLessons())
                {
                    xml.WriteStartElement("url");
                    xml.WriteElementString("loc", host + "/singlelesson/" + lesson.Student);
                    xml.WriteEndElement();
                }

                // Projects
                xml.WriteStartElement("url");
                xml.WriteElementString("loc", host + "/projects");
                xml.WriteEndElement();

                foreach (ProjectModel project in await _publicProjectManager.GetProjects())
                {
                    xml.WriteStartElement("url");
                    xml.WriteElementString("loc", host + "/project/" + project.Slug);
                    xml.WriteEndElement();
                }

                xml.WriteEndElement();
            }

            return output.ToString();
        }
    }
}
