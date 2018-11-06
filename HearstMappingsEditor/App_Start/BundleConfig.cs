using System.Web.Optimization;

namespace HearstMappingsEditor
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/respond.js",
                      "~/Scripts/umd/popper.js",
                      "~/Scripts/umd/popper-utils.js",
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/common").Include(
                      "~/Scripts/select2.js",
                      "~/Scripts/bootstrap-datepicker/bootstrap-datepicker.js",
                      "~/Scripts/floatthead/jquery.floatThead.js",
                      "~/Scripts/app/tables.js",
                      "~/Scripts/app/common.js"));

            bundles.Add(new ScriptBundle("~/bundles/mapping").Include(
                      "~/Scripts/app/mappings/mappings.js"));

            bundles.Add(new ScriptBundle("~/bundles/mapping/accounts").Include(
                      "~/Scripts/app/mappings/accountMapping.js"));

            bundles.Add(new ScriptBundle("~/bundles/mapping/brands").Include(
                      "~/Scripts/app/mappings/brandMapping.js"));

            bundles.Add(new ScriptBundle("~/bundles/mapping/cost-centers").Include(
                      "~/Scripts/app/mappings/costCenterMapping.js"));

            bundles.Add(new ScriptBundle("~/bundles/mapping/entities").Include(
                      "~/Scripts/app/mappings/entityMapping.js"));

            bundles.Add(new ScriptBundle("~/bundles/mapping/item-pl-kinds").Include(
                      "~/Scripts/app/mappings/itemPLKinds.js"));

            bundles.Add(new ScriptBundle("~/bundles/refs").Include(
                      "~/Scripts/app/references/references.js"));

            bundles.Add(new ScriptBundle("~/bundles/refs/source/dimItem").Include(
                      "~/Scripts/app/references/source/dimItem.js"));
            bundles.Add(new ScriptBundle("~/bundles/refs/source/dimCompany").Include(
                      "~/Scripts/app/references/source/orgStructure/dimCompany.js"));
            bundles.Add(new ScriptBundle("~/bundles/refs/source/dimPerimeter").Include(
                      "~/Scripts/app/references/source/orgStructure/dimPerimeter.js"));
            bundles.Add(new ScriptBundle("~/bundles/refs/source/dimPerimeterLaw").Include(
                      "~/Scripts/app/references/source/orgStructure/dimPerimeterLaw.js"));
            bundles.Add(new ScriptBundle("~/bundles/refs/source/dimAllOrgStructure").Include(
                      "~/Scripts/app/references/source/orgStructure/dimAllOrgStructure.js"));
            bundles.Add(new ScriptBundle("~/bundles/refs/source/orgSubordinationByDate").Include(
                      "~/Scripts/app/references/source/orgStructure/orgSubordinationByDate.js"));
            bundles.Add(new ScriptBundle("~/bundles/refs/source/dimProject").Include(
                      "~/Scripts/app/references/source/dimProject.js"));
            bundles.Add(new ScriptBundle("~/bundles/refs/source/dimDept").Include(
                      "~/Scripts/app/references/source/dimDept.js"));
            bundles.Add(new ScriptBundle("~/bundles/refs/source/dimPLKind").Include(
                      "~/Scripts/app/references/source/dimPLKind.js"));
            bundles.Add(new ScriptBundle("~/bundles/refs/source/dimPLGroup").Include(
                      "~/Scripts/app/references/source/dimPLGroup.js"));

            bundles.Add(new ScriptBundle("~/bundles/refs/hearst/dimAccount").Include(
                      "~/Scripts/app/references/hearst/dimAccount.js"));
            bundles.Add(new ScriptBundle("~/bundles/refs/hearst/dimAccountGroup").Include(
                      "~/Scripts/app/references/hearst/dimAccountGroup.js"));
            bundles.Add(new ScriptBundle("~/bundles/refs/hearst/dimEntity").Include(
                      "~/Scripts/app/references/hearst/dimEntity.js"));
            bundles.Add(new ScriptBundle("~/bundles/refs/hearst/dimScenario").Include(
                      "~/Scripts/app/references/hearst/dimScenario.js"));
            bundles.Add(new ScriptBundle("~/bundles/refs/hearst/dimProduct").Include(
                      "~/Scripts/app/references/hearst/dimProduct.js"));
            bundles.Add(new ScriptBundle("~/bundles/refs/hearst/dimConsoSection").Include(
                      "~/Scripts/app/references/hearst/dimConsoSection.js"));
            bundles.Add(new ScriptBundle("~/bundles/refs/hearst/dimYear").Include(
                      "~/Scripts/app/references/hearst/dimYear.js"));
            bundles.Add(new ScriptBundle("~/bundles/refs/hearst/dimChannel").Include(
                      "~/Scripts/app/references/hearst/dimChannel.js"));

            bundles.Add(new StyleBundle("~/common/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/bootstrap-grid.css",
                      "~/Content/bootstrap-reboot.css",
                      "~/Content/css/select2.css",
                      "~/Content/css/bootstrap-datepicker/bootstrap-datepicker*",
                      "~/Content/app/common.css"));

            bundles.Add(new StyleBundle("~/error/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/bootstrap-grid.css",
                      "~/Content/bootstrap-reboot.css",
                      "~/Content/app/error.css"));
        }
    }
}
