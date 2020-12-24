using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
namespace HastaneOtomasyonu.App_Start
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
            //            "~/Scripts/jquery-ui-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.validate*"));

            //// Use the development version of Modernizr to develop with and learn from. Then, when you're
            //// ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/Js").Include(
                      "~/Content/bower_components/jquery/dist/jquery.min.js",
                      "~/Content/bower_components/bootstrap/dist/js/bootstrap.min.js",
                       "~/Content/bower_components/jquery-slimscroll/jquery.slimscroll.min.js",
                        "~/Content/bower_components/fastclick/lib/fastclick.js",
                        "~/Content/dist/js/adminlte.min.js",
                        "~/Content/dist/js/demo.js"
                      )); ;
            bundles.Add(new StyleBundle("~/Css").Include(

                  "~/Content/bootstrap/dist/css/bootstrap.min.css",
                  "~/Content/dist/css/AdminLTE.min.css",
                   "~/Content/dist/css/skins/_all-skins.min.css"

                  ));

            BundleTable.EnableOptimizations = true;
        }
    }
}