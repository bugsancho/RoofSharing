using System.Web;
using System.Web.Optimization;

namespace RoofSharing.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/signalR").Include(
                "~/Scripts/jquery.signalR-2.2.0.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryajax").Include(
                "~/Scripts/jquery.unobtrusive-ajax.js"));
            
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/kendo-scripts").Include(
                "~/Scripts/kendo/2014.2.1008/kendo.all.min.js",
                "~/Scripts/kendo/2014.2.1008/kendo.aspnetmvc.min.js",
                "~/Scripts/kendo.modernizr.custom.js"));
            
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap-theme").Include(
                "~/Scripts/bootstrap.js",
                "~/Scripts/script.js",
                "~/Scripts/jquery.appear.js",
                "~/Scripts/jquery.nicescroll.min.js",
                "~/Scripts/toastr.min.js"));
            
            bundles.Add(new StyleBundle("~/Content/css-theme").Include(
                "~/Content/bootstrap.css",
                "~/Content/bootstrap-social.css",
                "~/Content/font-awesome.css",
                "~/Content/animate.min.css",
                "~/Content/responsive.css",
                "~/Content/colors/red.css",
                "~/Content/style.css",
                "~/Content/site.css",
                "~/Content/toastr.min.css"));

            //bundles.Add(new StyleBundle("~/Content/Kendo").Include(
            //    "~/Content/kendo/2014.2.1008/kendo.common.min.css",
            //    "~/Content/kendo/2014.2.1008/kendo.bootstrap.min.css"));

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = false;
        }
    }
}