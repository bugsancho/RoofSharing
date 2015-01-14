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

            bundles.Add(new ScriptBundle("~/bundles/jqueryajax").Include(
                "~/Scripts/jquery.unobtrusive-ajax.js"));
            
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js",
                "~/Scripts/respond.js"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap-theme").Include(
            //    "~/Scripts/wow.min.js",
            //    "~/Scripts/jquery.prettyPhoto.js",
            //    "~/Scripts/jquery.isotope.min.js",
            //    "~/Scripts/main.js"));

              bundles.Add(new ScriptBundle("~/bundles/bootstrap-theme").Include(
                "~/Scripts/jquery.prettyPhoto.js",
                "~/Scripts/jquery.isotope.min.js",
                "~/Scripts/jquery.migrate.js",
                //"~/Scripts/jquery.easypiechart.min.js",
                //"~/Scripts/mediaelement-and-player.js",
                "~/Scripts/script.js",
                "~/Scripts/jquery.parallax.js",
                "~/Scripts/jquery.lettering.js",
                "~/Scripts/jquery.textillate.js",
                "~/Scripts/count-to.js",
                "~/Scripts/jquery.appear.js",
                //"~/Scripts/jquery.fitvids.js",
                "~/Scripts/owl.carousel.min.js",
                "~/Scripts/nivo-lightbox.min.js",
                "~/Scripts/jquery.nicescroll.min.js"));



            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/bootstrap-social.css",
                "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/css-theme").Include(
              //  "~/Content/main.css",
                "~/Content/font-awesome.css",
                "~/Content/animate.min.css",
                "~/Content/prettyPhoto.css",
                "~/Content/responsive.css"));

            bundles.Add(new StyleBundle("~/Content/margo-theme").Include(
                "~/Content/style.css"));

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = false;
        }
    }
}