using System;
using System.IO;
using System.Reflection;

namespace WebApplication1.Api
{
    public class SwaggerConfig
    {
        internal static string GetControllersXmlCommentsPath()
        {
            var applicationPath = AppDomain.CurrentDomain.BaseDirectory;

            var name = Assembly.GetExecutingAssembly().GetName().Name;
            var commentsFileName = name + ".xml";
            var commentsFile = Path.Combine(applicationPath, commentsFileName);
            return commentsFile;
        }
    }
}
