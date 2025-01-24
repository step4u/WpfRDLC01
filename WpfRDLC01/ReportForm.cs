using Microsoft.Reporting.WinForms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Runtime.Remoting.Contexts;
using System.Windows.Controls;
using System.Windows.Forms;
using DataSet = System.Data.DataSet;

namespace WpfRDLC01
{
    public partial class ReportForm : Form
    {
        int pMargin = 10;

        public ReportForm()
        {
            InitializeComponent();

            StartPosition = FormStartPosition.CenterScreen;
            reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);

            PageSettings pageSettings = new PageSettings();
            pageSettings.PaperSize.RawKind = (int)PaperKind.A4;
            pageSettings.Margins = new Margins(pMargin, pMargin, pMargin, pMargin);
            
            reportViewer1.SetPageSettings(pageSettings);
            reportViewer1.ContextMenuChanged += ReportViewer1_ContextMenuChanged;
            reportViewer1.ContextMenuStripChanged += ReportViewer1_ContextMenuStripChanged;
            reportViewer1.ControlAdded += ReportViewer1_ControlAdded;
            reportViewer1.ControlRemoved += ReportViewer1_ControlRemoved;
        }

        private void ReportViewer1_ControlRemoved(object sender, ControlEventArgs e)
        {
            
        }

        private void ReportViewer1_ControlAdded(object sender, ControlEventArgs e)
        {
            
        }

        private void ReportViewer1_ContextMenuStripChanged(object sender, EventArgs e)
        {
            
        }

        private void ReportViewer1_ContextMenuChanged(object sender, EventArgs e)
        {
            
        }

        private void ReportForm_Load(object sender, EventArgs e)
        {
            var jsonStr = File.ReadAllText("data01.json");

            //JArray jsonArray = JArray.Parse(jsonStr);

            // 'content'를 문자열로 변환
            //foreach (var item in jsonArray)
            //{
            //    if (item["content"] != null)
            //    {
            //        item["content"] = item["content"].ToString(Formatting.None); // JSON 배열을 문자열로 변환
            //    }
            //}

            //string modifiedJsonStr = jsonArray.ToString(Formatting.None);
            //DataTable dt = new DataTable();
            //dt.DataSet.Tables.Add(modifiedJsonStr);
            //var dt = JsonConvert.DeserializeObject<DataTable>(modifiedJsonStr);


            // param, table 구분 -> parameters, table 생성

            List<ReportParameter> rptParams = new List<ReportParameter>();
            DataTable dt0 = new DataTable("DT0");
            DataTable dt1 = new DataTable("DT1");
            DataTable dtImage = new DataTable("ImageTable");

            var items = JsonConvert.DeserializeObject<List<DataModel>>(jsonStr);
            foreach (var item in items)
            {
                switch (item.seq)
                {
                    case 3:
                    case 6:
                        {
                            // table

                            string varName = $"subject{item.seq}";
                            rptParams.Add(new ReportParameter(varName, item.subject, true));

                            var content = item.content[0];
                            string contname = $"content{item.seq}";
                            rptParams.Add(new ReportParameter(contname, "", true));
                        }
                        break;
                    case 2:
                        {
                            // image

                            string varName = $"subject{item.seq}";
                            rptParams.Add(new ReportParameter(varName, item.subject, true));

                            var content = item.content[0];
                            var val = content["image"] as JValue;

                            //var p = new Uri(val.ToString());
                            var p = new Uri("D:\\안드로이드_김정수_스크린캡쳐.jpg");
                            string contName = $"content{item.seq}";
                            rptParams.Add(new ReportParameter(contName, p.AbsoluteUri, true));

                            //dtImage.Columns.Add("Name", typeof(string));
                            //dtImage.Columns.Add("ImagePath", typeof(string));
                            //dtImage.Rows.Add("Image", v);
                        }
                        break;
                    case 1:
                    case 4:
                    case 5:
                    case 7:
                        {
                            // text

                            string varName = $"subject{item.seq}";
                            rptParams.Add(new ReportParameter(varName, item.subject, true));

                            var content = item.content[0];
                            var val = content["text"] as JValue;

                            string contName = $"content{item.seq}";
                            rptParams.Add(new ReportParameter(contName, val.ToString(), true));
                        }
                        break;
                    case 0:
                    default:
                        rptParams.Add(new ReportParameter("title", item.subject , true));
                        break;
                }
            }

            //ds.Tables.Add(dt);


            //reportViewer1.LocalReport.ReportPath = "Report1.rdlc";
            reportViewer1.LocalReport.ReportEmbeddedResource = "WpfRDLC01.Report.Report2.rdlc";
            reportViewer1.LocalReport.EnableExternalImages = true;
            //ReportDataSource rds = new ReportDataSource("Image", dtImage);

            //ReportDataSource rds = new ReportDataSource("DataSet1", dtImage);
            //reportViewer1.LocalReport.DataSources.Clear();
            //reportViewer1.LocalReport.DataSources.Add(rds);

            //reportViewer1.LocalReport.DataSources.Clear();
            //reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", "D:\\03.jpg"));

            reportViewer1.LocalReport.SetParameters(rptParams);
            
            reportViewer1.RefreshReport();
        }

        object parseJsonData(DataModel item)
        {
            return new object();
        }
    }
}
