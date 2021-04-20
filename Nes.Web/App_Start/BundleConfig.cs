using System.Web;
using System.Web.Optimization;

namespace Nes.Web
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/adminjs").Include(
                "~/Scripts/admin/jquery-1.10.2.js",
                "~/Scripts/admin/bootstrap.min.js",
                "~/Scripts/admin/plugins/metisMenu/jquery.metisMenu.js",
                "~/Scripts/admin/sb-admin.js",
                 "~/Scripts/dialogmessage.js"
                ));
             bundles.Add(new ScriptBundle("~/bundles/dashboardjs").Include(
                "~/Scripts/admin/plugins/morris/raphael-2.1.0.min.js",
                "~/Scripts/adminplugins/morris/morris.js",
                "~/Scripts/admin/demo/dashboard-demo.js"));
         
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));

            bundles.Add(new StyleBundle("~/Content/themes/admin/common").Include(
                       "~/Content/themes/admin/css/bootstrap.min.css",
                       "~/Content/themes/admin/font-awesome/css/font-awesome.css",
                       "~/Content/themes/admin/css/sb-admin.css"));

            bundles.Add(new StyleBundle("~/Content/themes/admin/dashboard").Include(
                       "~/Content/themes/admin/css/plugins/morris/morris-0.4.3.min.css",
                       "~/Content/themes/admin/css/plugins/timeline/timeline.css"));
            BundleTable.EnableOptimizations = true;
        }
    }
}