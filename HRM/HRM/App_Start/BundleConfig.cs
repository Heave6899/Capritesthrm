using System.Web;
using System.Web.Optimization;

namespace HRM
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery/jquery.min.js",
                        "~/Scripts/jquery/jquery.blockUI.js",
                        "~/Scripts/jquery/JQueryExtension.js",
                        "~/Scripts/plugins/jquery-jvectormap.js",
                        "~/Scripts/plugins/jquery.bootstrap-wizard.js",
                        "~/Scripts/plugins/jquery.dataTables.min.js",
                        "~/Scripts/plugins/jquery.tagsinput.js",
                        "~/Scripts/plugins/perfect-scrollbar.jquery.min.js",
                        "~/Scripts/plugins/sweetalert2.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/plugins/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap/bootstrap-material-design.min.js",
                      "~/Scripts/plugins/bootstrap-datetimepicker.min.js",
                      "~/Scripts/plugins/bootstrap-notify.js",
                      "~/Scripts/plugins/bootstrap-selectpicker.js",
                      "~/Scripts/plugins/bootstrap-notify.js",
                      "~/Scripts/plugins/bootstrap-tagsinput.js",
                      "~/Scripts/plugins/jasny-bootstrap.min.js",
                      "~/Scripts/plugins/moment.min.js",
                      "~/Scripts/plugins/nouislider.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/themeJs").Include(
                     "~/Scripts/plugins/material-dashboard.js",
                     "~/Scripts/plugins/material-dashboard.min.js",
                     "~/Scripts/plugins/material-dashboard.js.map"));


            bundles.Add(new StyleBundle("~/Content/bootstrap").Include(
                      "~/Content/material-dashboard.min.css"));

        }
    }
}