using Microsoft.Reporting.WinForms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WpfRDLC01
{
    public partial class ReportForm : Form
    {
        public ReportForm()
        {
            InitializeComponent();

            StartPosition = FormStartPosition.CenterScreen;
            reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.Normal);

            PageSettings pageSettings = new PageSettings();
            pageSettings.PaperSize.RawKind = (int)PaperKind.A4;
            pageSettings.Margins = new Margins(0, 0, 0, 0);
            
            reportViewer1.SetPageSettings(pageSettings);
        }



        private void ReportForm_Load(object sender, EventArgs e)
        {
            var jsonStr = File.ReadAllText("data01.json");

            JArray jsonArray = JArray.Parse(jsonStr);

            // 'content'를 문자열로 변환
            foreach (var item in jsonArray)
            {
                if (item["content"] != null)
                {
                    item["content"] = item["content"].ToString(Formatting.None); // JSON 배열을 문자열로 변환
                }
            }

            // 변환된 JSON을 다시 문자열로 직렬화
            string modifiedJsonStr = jsonArray.ToString(Formatting.None);

            //var dt = JsonSerializer.Deserialize<DataTable>(jsonStr);
            var dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(modifiedJsonStr);


            //reportViewer1.LocalReport.ReportPath = "Report1.rdlc";
            reportViewer1.LocalReport.ReportEmbeddedResource = "WpfRDLC01.Report.Report2.rdlc";

            ReportDataSource rds = new ReportDataSource("DataSet1", dt);
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rds);

            List<ReportParameter> reportParams = new List<ReportParameter>();
            reportParams.Add(new ReportParameter("paramTitle", "영상 위변조 분석 결과서", true));
            reportViewer1.LocalReport.SetParameters(reportParams);

            reportViewer1.RefreshReport();
        }
    }
}
