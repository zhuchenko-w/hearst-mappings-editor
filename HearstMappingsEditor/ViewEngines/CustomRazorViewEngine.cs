using System.Web.Mvc;

namespace HearstMappingsEditor
{
    public class CustomRazorViewEngine : RazorViewEngine
    {
        public CustomRazorViewEngine()
        {
            var viewLocations = new[] {
                "~/Views/Mappings/{1}/{0}.cshtml",
                "~/Views/References/Hearst/{1}/{0}.cshtml",
                "~/Views/References/Source/{1}/{0}.cshtml",
                "~/Views/References/Source/OrgStructure/{1}/{0}.cshtml"
            };

            PartialViewLocationFormats = viewLocations;
            ViewLocationFormats = viewLocations;
        }
    }
}