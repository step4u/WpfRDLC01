using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WpfRDLC01
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        ReportForm rpt = new ReportForm();

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            rpt.Height = 1000;
            rpt.Show();
            rpt.FormClosed += Rpt_FormClosed;
        }

        private void Rpt_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
