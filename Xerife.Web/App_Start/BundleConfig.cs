using System.Web;
using System.Web.Optimization;

namespace Xerife.Web
{
    /// <summary>
    /// Configura pacotes de arquivos
    /// </summary>
    public class BundleConfig
    {
        /// <summary>
        /// Registra os pacotes
        /// </summary>
        /// <param name="bundles"></param>
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = false;

            RegisterLayout(bundles);

            RegisterShared(bundles);

            RegisterElements(bundles);

            RegisterProjeto(bundles);

            RegisterConsultas(bundles);

            RegisterVpn(bundles);

            RegisterBaseline(bundles);

            RegisterParametroSistema(bundles);

            RegisterUsuario(bundles);

        }

        /// <summary>
        /// Registra o script de projeto
        /// </summary>
        /// <param name="bundles"></param>
        private static void RegisterProjeto(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Scripts/Projeto/all.js").Include(
               "~/Scripts/View/Projeto.js"));
            bundles.Add(new ScriptBundle("~/Scripts/LogIntegracao/all.js").Include(
               "~/Scripts/View/LogIntegracao.js"));
        }

        /// <summary>
        /// Registra o script de vpn
        /// </summary>
        /// <param name="bundles"></param>
        private static void RegisterVpn(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Scripts/Vpn/all.js").Include(
               "~/Scripts/View/UsuarioVpn.js"));
            bundles.Add(new ScriptBundle("~/Scripts/HistoricoVpn/all.js").Include(
               "~/Scripts/View/HistoricoVpn.js"));
        }

        /// <summary>
        /// Registra o script de baseline
        /// </summary>
        /// <param name="bundles"></param>
        private static void RegisterBaseline(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Scripts/Baseline/all.js").Include(
               "~/Scripts/View/Baseline.js"));
        }

        /// <summary>
        /// Registra o script de baseline
        /// </summary>
        /// <param name="bundles"></param>
        private static void RegisterParametroSistema(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Scripts/ParametroSistema/all.js").Include(
               "~/Scripts/View/ParametroSistema.js"));
        }
        /// <summary>
        /// Registra o script de baseline
        /// </summary>
        /// <param name="bundles"></param>
        private static void RegisterUsuario(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Scripts/Usuario/all.js").Include(
               "~/Scripts/View/Usuario.js"));
        }

        /// <summary>
        /// Registra o script de consultas
        /// </summary>
        /// <param name="bundles"></param>
        private static void RegisterConsultas(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Scripts/Consultas/all.js").Include(
               "~/Scripts/View/Consultas.js"));
            bundles.Add(new ScriptBundle("~/Scripts/ProximasEntregas/all.js").Include(
               "~/Scripts/View/ProximasEntregas.js"));
        }
        
        /// <summary>
        /// Registra o script de layout de menu
        /// </summary>
        /// <param name="bundles"></param>
        private static void RegisterElements(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Scripts/Elements/Buttons/menu/all.js").Include(
                "~/Scripts/Elements/Buttons-menu.js"));

            bundles.Add(new ScriptBundle("~/Scripts/Elements/General/menu/all.js").Include(
                "~/Scripts/Elements/General-menu.js"));

            bundles.Add(new StyleBundle("~/Styles/Elements/General/all.css").Include(
                "~/Styles/Elements/General.css"));

            bundles.Add(new ScriptBundle("~/Scripts/Elements/Icons/menu/all.js").Include(
                "~/Scripts/Elements/Icons-menu.js"));

            bundles.Add(new StyleBundle("~/Styles/Elements/Icons/all.css").Include(
                "~/Styles/Elements/Icons.css"));

            bundles.Add(new ScriptBundle("~/Scripts/Elements/Modals/menu/all.js").Include(
                "~/Scripts/Elements/Modals-menu.js"));

            bundles.Add(new StyleBundle("~/Styles/Elements/Modals").Include(
                "~/Styles/Elements/Modals.css"));

            bundles.Add(new ScriptBundle("~/Scripts/Elements/Sliders/all.js").Include(
                "~/Scripts/Elements/Sliders.js"));

            bundles.Add(new ScriptBundle("~/Scripts/Elements/Sliders/menu/all.js").Include(
                "~/Scripts/Elements/Sliders-menu.js"));

            bundles.Add(new ScriptBundle("~/Scripts/Elements/Timeline/menu/all.js").Include(
                "~/Scripts/Elements/Timeline-menu.js"));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bundles"></param>
        private static void RegisterShared(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Scripts/Shared/_Layout/all.js").Include(
                "~/Scripts/Shared/_Layout.js"));
            bundles.Add(new ScriptBundle("~/Scripts/Tfs/all.js").Include(
                "~/Scripts/View/Tfs.js"));
            bundles.Add(new ScriptBundle("~/Scripts/Channel/all.js").Include(
                "~/Scripts/View/Channel.js"));
            bundles.Add(new ScriptBundle("~/Scripts/Shared/all.js").Include(
                "~/Scripts/View/Shared.js"));
        }

        /// <summary>
        /// Bootstrap de layout
        /// </summary>
        /// <param name="bundles"></param>
        private static void RegisterLayout(BundleCollection bundles)
        {
            // bootstrap
            bundles.Add(new ScriptBundle("~/AdminLTE/bootstrap/js/all.js").Include(
                "~/AdminLTE/bootstrap/js/bootstrap.min.js"));

            bundles.Add(new StyleBundle("~/AdminLTE/bootstrap/css/all.css").Include(
                "~/AdminLTE/bootstrap/css/bootstrap.min.css"));

            // dist
            bundles.Add(new ScriptBundle("~/AdminLTE/dist/js/all.js").Include(
                "~/AdminLTE/dist/js/app.js"));

            bundles.Add(new StyleBundle("~/AdminLTE/dist/css/all.css").Include(
                "~/AdminLTE/dist/css/admin-lte.min.css"));

            bundles.Add(new StyleBundle("~/AdminLTE/dist/css/skins/all.css").Include(
                "~/AdminLTE/dist/css/skins/_all-skins.min.css"));

            // documentation
            bundles.Add(new ScriptBundle("~/AdminLTE/documentation/js/all.js").Include(
                "~/AdminLTE/documentation/js/docs.js"));

            bundles.Add(new StyleBundle("~/AdminLTE/documentation/css/all.css").Include(
                "~/AdminLTE/documentation/css/style.css"));

            // plugins | bootstrap-slider
            bundles.Add(new ScriptBundle("~/AdminLTE/plugins/bootstrap-slider/js/all.js").Include(
                                        "~/AdminLTE/plugins/bootstrap-slider/js/bootstrap-slider.js"));

            bundles.Add(new StyleBundle("~/AdminLTE/plugins/bootstrap-slider/css/all.css").Include(
                                        "~/AdminLTE/plugins/bootstrap-slider/css/slider.css"));

            // plugins | bootstrap-wysihtml5
            bundles.Add(new ScriptBundle("~/AdminLTE/plugins/bootstrap-wysihtml5/js/all.js").Include(
                                         "~/AdminLTE/plugins/bootstrap-wysihtml5/js/bootstrap3-wysihtml5.all.min.js"));

            bundles.Add(new StyleBundle("~/AdminLTE/plugins/bootstrap-wysihtml5/css/all.css").Include(
                                        "~/AdminLTE/plugins/bootstrap-wysihtml5/css/bootstrap3-wysihtml5.min.css"));

            // plugins | chartjs
            bundles.Add(new ScriptBundle("~/AdminLTE/plugins/chartjs/js/all.js").Include(
                                         "~/AdminLTE/plugins/chartjs/js/chart.min.js"));

            // plugins | ckeditor
            bundles.Add(new ScriptBundle("~/AdminLTE/plugins/ckeditor/js/all.js").Include(
                                         "~/AdminLTE/plugins/ckeditor/js/ckeditor.js"));

            // plugins | colorpicker
            bundles.Add(new ScriptBundle("~/AdminLTE/plugins/colorpicker/js/all.js").Include(
                                         "~/AdminLTE/plugins/colorpicker/js/bootstrap-colorpicker.min.js"));

            bundles.Add(new StyleBundle("~/AdminLTE/plugins/colorpicker/css/all.css").Include(
                                        "~/AdminLTE/plugins/colorpicker/css/bootstrap-colorpicker.css"));

            // plugins | datatables
            bundles.Add(new ScriptBundle("~/AdminLTE/plugins/datatables/js/all.js").Include(
                                         "~/AdminLTE/plugins/datatables/js/jquery.dataTables.min.js",
                                         "~/AdminLTE/plugins/datatables/js/dataTables.bootstrap.min.js"));

            bundles.Add(new StyleBundle("~/AdminLTE/plugins/datatables/css/all.css").Include(
                                        "~/AdminLTE/plugins/datatables/css/dataTables.bootstrap.css"));

            // plugins | datepicker
            bundles.Add(new ScriptBundle("~/AdminLTE/plugins/datepicker/js/all.js").Include(
                                         "~/AdminLTE/plugins/datepicker/js/bootstrap-datepicker.js",
                                         "~/AdminLTE/plugins/datepicker/js/locales/bootstrap-datepicker*"));

            bundles.Add(new StyleBundle("~/AdminLTE/plugins/datepicker/css/all.css").Include(
                                        "~/AdminLTE/plugins/datepicker/css/datepicker3.css"));

            // plugins | daterangepicker
            bundles.Add(new ScriptBundle("~/AdminLTE/plugins/daterangepicker/js/all.js").Include(
                                         "~/AdminLTE/plugins/daterangepicker/js/moment.min.js",
                                         "~/AdminLTE/plugins/daterangepicker/js/daterangepicker.js"));

            bundles.Add(new StyleBundle("~/AdminLTE/plugins/daterangepicker/css/all.css").Include(
                                        "~/AdminLTE/plugins/daterangepicker/css/daterangepicker-bs3.css"));

            // plugins | fastclick
            bundles.Add(new ScriptBundle("~/AdminLTE/plugins/fastclick/js/all.js").Include(
                                         "~/AdminLTE/plugins/fastclick/js/fastclick.min.js"));

            // plugins | flot
            bundles.Add(new ScriptBundle("~/AdminLTE/plugins/flot/js/all.js").Include(
                                         "~/AdminLTE/plugins/flot/js/jquery.flot.min.js",
                                         "~/AdminLTE/plugins/flot/js/jquery.flot.resize.min.js",
                                         "~/AdminLTE/plugins/flot/js/jquery.flot.pie.min.js",
                                         "~/AdminLTE/plugins/flot/js/jquery.flot.categories.min.js"));

            // plugins | font-awesome
            bundles.Add(new StyleBundle("~/AdminLTE/plugins/font-awesome/css/all.css").Include(
                                        "~/AdminLTE/plugins/font-awesome/css/font-awesome.min.css"));

            // plugins | fullcalendar
            bundles.Add(new ScriptBundle("~/AdminLTE/plugins/fullcalendar/js/all.js").Include(
                                         "~/AdminLTE/plugins/fullcalendar/js/fullcalendar.min.js"));

            bundles.Add(new StyleBundle("~/AdminLTE/plugins/fullcalendar/css/all.css").Include(
                                        "~/AdminLTE/plugins/fullcalendar/css/fullcalendar.min.css"));

            bundles.Add(new StyleBundle("~/AdminLTE/plugins/fullcalendar/css/print/all.css").Include(
                                        "~/AdminLTE/plugins/fullcalendar/css/print/fullcalendar.print.css"));

            // plugins | icheck
            bundles.Add(new ScriptBundle("~/AdminLTE/plugins/icheck/js/all.js").Include(
                                         "~/AdminLTE/plugins/icheck/js/icheck.min.js"));

            bundles.Add(new StyleBundle("~/AdminLTE/plugins/icheck/css/all.css").Include(
                                        "~/AdminLTE/plugins/icheck/css/all.css"));

            bundles.Add(new StyleBundle("~/AdminLTE/plugins/icheck/css/flat/all.css").Include(
                                        "~/AdminLTE/plugins/icheck/css/flat/flat.css"));

            bundles.Add(new StyleBundle("~/AdminLTE/plugins/icheck/css/sqare/blue/all.css").Include(
                                        "~/AdminLTE/plugins/icheck/css/sqare/blue.css"));

            // plugins | input-mask
            bundles.Add(new ScriptBundle("~/AdminLTE/plugins/input-mask/js/all.js").Include(
                                         "~/AdminLTE/plugins/input-mask/js/jquery.inputmask.js",
                                         "~/AdminLTE/plugins/input-mask/js/jquery.inputmask.date.extensions.js",
                                         "~/AdminLTE/plugins/input-mask/js/jquery.inputmask.extensions.js"));

            // plugins | ionicons
            bundles.Add(new StyleBundle("~/AdminLTE/plugins/ionicons/css/all.css").Include(
                                        "~/AdminLTE/plugins/ionicons/css/ionicons.min.css"));

            // plugins | ionslider
            bundles.Add(new ScriptBundle("~/AdminLTE/plugins/ionslider/js/all.js").Include(
                                         "~/AdminLTE/plugins/ionslider/js/ion.rangeSlider.min.js"));

            bundles.Add(new StyleBundle("~/AdminLTE/plugins/ionslider/css/all.css").Include(
                                        "~/AdminLTE/plugins/ionslider/css/ion.rangeSlider.css",
                                        "~/AdminLTE/plugins/ionslider/css/ion.rangeSlider.skinNice.css"));

            // plugins | jquery
            bundles.Add(new ScriptBundle("~/AdminLTE/plugins/jquery/js/all.js").Include(
                                         "~/AdminLTE/plugins/jquery/js/jQuery-2.1.4.min.js"));

            // plugins | jquery-validate
            bundles.Add(new ScriptBundle("~/AdminLTE/plugins/jquery-validate/js/all.js").Include(
                                         "~/AdminLTE/plugins/jquery-validate/js/jquery.validate*"));

            // plugins | jquery-ui
            bundles.Add(new ScriptBundle("~/AdminLTE/plugins/jquery-ui/js/all.js").Include(
                                         "~/AdminLTE/plugins/jquery-ui/js/jquery-ui.min.js"));

            // plugins | jvectormap
            bundles.Add(new ScriptBundle("~/AdminLTE/plugins/jvectormap/js/all.js").Include(
                                         "~/AdminLTE/plugins/jvectormap/js/jquery-jvectormap-1.2.2.min.js",
                                         "~/AdminLTE/plugins/jvectormap/js/jquery-jvectormap-world-mill-en.js"));

            bundles.Add(new StyleBundle("~/AdminLTE/plugins/jvectormap/css/all.css").Include(
                                        "~/AdminLTE/plugins/jvectormap/css/jquery-jvectormap-1.2.2.css"));

            // plugins | knob
            bundles.Add(new ScriptBundle("~/AdminLTE/plugins/knob/js/all.js").Include(
                                         "~/AdminLTE/plugins/knob/js/jquery.knob.js"));

            // plugins | morris
            bundles.Add(new StyleBundle("~/AdminLTE/plugins/morris/css/all.css").Include(
                                        "~/AdminLTE/plugins/morris/css/morris.css"));

            // plugins | momentjs
            bundles.Add(new ScriptBundle("~/AdminLTE/plugins/momentjs/js/all.js").Include(
                                         "~/AdminLTE/plugins/momentjs/js/moment.min.js"));

            // plugins | pace
            bundles.Add(new ScriptBundle("~/AdminLTE/plugins/pace/js/all.js").Include(
                                         "~/AdminLTE/plugins/pace/js/pace.js"));

            bundles.Add(new StyleBundle("~/AdminLTE/plugins/pace/css/all.css").Include(
                                        "~/AdminLTE/plugins/pace/css/pace.min.css"));

            // plugins | slimscroll
            bundles.Add(new ScriptBundle("~/AdminLTE/plugins/slimscroll/js/all.js").Include(
                                         "~/AdminLTE/plugins/slimscroll/js/jquery.slimscroll.min.js"));

            // plugins | sparkline
            bundles.Add(new ScriptBundle("~/AdminLTE/plugins/sparkline/js/all.js").Include(
                                         "~/AdminLTE/plugins/sparkline/js/jquery.sparkline.min.js"));

            // plugins | timepicker
            bundles.Add(new ScriptBundle("~/AdminLTE/plugins/timepicker/js/all.js").Include(
                                         "~/AdminLTE/plugins/timepicker/js/bootstrap-timepicker.min.js"));

            bundles.Add(new StyleBundle("~/AdminLTE/plugins/timepicker/css/all.css").Include(
                                        "~/AdminLTE/plugins/timepicker/css/bootstrap-timepicker.min.css"));

            // plugins | raphael
            bundles.Add(new ScriptBundle("~/AdminLTE/plugins/raphael/js/all.js").Include(
                                         "~/AdminLTE/plugins/raphael/js/raphael-min.js"));

            // plugins | select2
            bundles.Add(new ScriptBundle("~/AdminLTE/plugins/select2/js/all.js").Include(
                                         "~/AdminLTE/plugins/select2/js/select2.full.min.js"));

            bundles.Add(new StyleBundle("~/AdminLTE/plugins/select2/css/all.css").Include(
                                        "~/AdminLTE/plugins/select2/css/select2.min.css"));

            // plugins | morris
            bundles.Add(new ScriptBundle("~/AdminLTE/plugins/morris/js/all.js").Include(
                                         "~/AdminLTE/plugins/morris/js/morris.min.js"));

            // plugins | chosen
            bundles.Add(new ScriptBundle("~/AdminLTE/plugins/chosen/js/all.js").Include(
                                         "~/AdminLTE/plugins/chosen/js/chosen.jquery.min.js"));

            //bundles.Add(new StyleBundle("~/AdminLTE/plugins/chosen/css").Include(
            //"~/AdminLTE/plugins/chosen/css/chosen.min.css"));
            bundles.Add(new StyleBundle("~/AdminLTE/plugins/chosen/css/all.css").Include(
            "~/AdminLTE/plugins/chosen/css/chosen-bootstrap.css"));
        }
    }
}