﻿using System.Web.Optimization;

namespace MyBlog
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js", "~/Scripts/script.js"));
            bundles.Add(new ScriptBundle("~/bundles/Admin").Include(
                        "~/Scripts/jquery-{version}.js", "~/Content/admin/js/jquery.metisMenu.js", "~/Content/admin/js/jquery.slimscroll.min.js",
                        "~/Content/admin/js/jquery.nicescroll.js", "~/Content/admin/js/scripts.js",
                        "~/Content/admin/js/custom.js", "~/Content/admin/js/screenfull.js",
                        "~/Scripts/jquery.min.js", "~/Scripts/bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
        }
    }
}