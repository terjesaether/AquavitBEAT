using System.Web;
using System.Web.Optimization;

namespace AquavitBEAT
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-1.12.1.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/readmorejs").Include(
                      "~/Scripts/Readmore-js/readmore.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/soundmanager2js").Include(
                      "~/Scripts/SoundManager2/demo/360-player/script/berniecode-animator.js",
                      "~/Scripts/SoundManager2/script/soundmanager2-nodebug-jsmin.js",
                      "~/Scripts/SoundManager2/demo/360-player/script/360player.js"
                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      //"~/Content/bootstrap.css",
                      "~/Content/site.min.css"));

            bundles.Add(new StyleBundle("~/Content/jqueryuicss").Include(
                      "~/Content/themes/base/jquery-ui.css",
                      "~/Content/themes/base/datepicker.css"));

            bundles.Add(new StyleBundle("~/Content/miniaudioplayercss").Include(
                      "~/Scripts/miniaudioplayer/css/jQuery.mb.miniAudioPlayer.min.css"));


        }
    }
}
