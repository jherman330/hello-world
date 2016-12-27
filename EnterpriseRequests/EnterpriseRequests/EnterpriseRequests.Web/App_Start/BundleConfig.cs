using System.Diagnostics.CodeAnalysis;
using System.Web.Optimization;

namespace EnterpriseRequests.Web.App_Start
{
    [ExcludeFromCodeCoverage]
    public static class BundleConfig
    {
        #region Public Methods and Operators

        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Scripts/jquery-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include("~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/appScripts").IncludeDirectory("~/Scripts/App", "*.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrapScripts")
                .Include("~/Scripts/bootstrap.js")
                .Include("~/Scripts/bootstrap-switch.js"));

            bundles.Add(new ScriptBundle("~/bundles/knockoutScripts")
                .Include("~/Scripts/knockout-{version}.js")
                .Include("~/Scripts/knockout-kendo.js"));

            bundles.Add(new ScriptBundle("~/bundles/miscScripts")
                .Include("~/Scripts/toastr.js")
                .Include("~/Scripts/spin.js")
                .Include("~/Scripts/jszip.min.js")
                .Include("~/Scripts/Views/Shared/Layout.js"));

            bundles.Add(
                new StyleBundle("~/Content/styles/css").Include(
                    "~/Content/site.css",
                    "~/Content/toastr.css"));

            bundles.Add(new StyleBundle("~/Content/styles/bootstrap")
                .Include("~/Content/bootstrap.min.css")
                .Include("~/Content/bootstrap-theme.min.css")
                .Include("~/Content/bootstrap-switch.css"));

            // The Kendo JavaScript bundle
            bundles.Add(new ScriptBundle("~/bundles/kendo")
                .Include("~/Scripts/kendo/kendo.web.min.js")
                .Include("~/Scripts/kendo/kendo.aspnetmvc.min.js"));

            // The Kendo CSS bundle
            bundles.Add(new StyleBundle("~/Content/kendo/bundle")
                .Include("~/Content/kendo/kendo.common.min.css")
                .Include("~/Content/kendo/kendo.common-material.min.css")
                .Include("~/Content/kendo/kendo.material.min.css"));

            // Clear all items from the ignore list to allow minified CSS and JavaScript files in debug mode
            bundles.IgnoreList.Clear();

            // Add back the default ignore list rules sans the ones which affect minified files and debug mode
            bundles.IgnoreList.Ignore("*.intellisense.js");
            bundles.IgnoreList.Ignore("*-vsdoc.js");
            bundles.IgnoreList.Ignore("*.debug.js", OptimizationMode.WhenEnabled);
        }

        #endregion

        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
    }
}
