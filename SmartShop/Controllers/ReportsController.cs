using SmartShop.Models;
using SmartShop.PublicClasses;
using iTextSharp.text.pdf;
using Microsoft.Ajax.Utilities;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Web.Configuration;
using System.Threading.Tasks;
namespace SmartShop.Controllers
{
    public class ReportsController : BaseController
    {
        SmartShopEntities db = new SmartShopEntities();

        // GET: Reports
        public ActionResult BalanceReport()
        {
            ViewBag.Stores = db.Stores.ToList();

            return View();
        }
        public ActionResult DailyReport()
        {

            return View();
        }
        public ActionResult MonthReport()
        {

            return View();
        }
        public ActionResult YearReport()
        {

            return View();
        }
        public JsonResult GetDailyReport(DateTime DateF)
        {
            db.Configuration.ProxyCreationEnabled = false;

            if (DateF !=null)
            {
                var BankStatment = db.DailyReport( DateF).ToList();



                return Json(BankStatment, JsonRequestBehavior.AllowGet);

            }



            return Json(new SelectList(""), JsonRequestBehavior.AllowGet);

        }


        public JsonResult GetMonthReport(DateTime DateF)
        {
            db.Configuration.ProxyCreationEnabled = false;

            if (DateF != null)
            {
                var BankStatment = db.MonthReport(DateF).ToList();



                return Json(BankStatment, JsonRequestBehavior.AllowGet);

            }



            return Json(new SelectList(""), JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetYearReport(float DateF = 0)
        {
            db.Configuration.ProxyCreationEnabled = false;

            if (DateF != 0)
            {
                var BankStatment = db.YearReport(DateF).ToList();



                return Json(BankStatment, JsonRequestBehavior.AllowGet);

            }



            return Json(new SelectList(""), JsonRequestBehavior.AllowGet);

        }


        public JsonResult GetItemCredit( int StkID = 0)
        {
            db.Configuration.ProxyCreationEnabled = false;
            
                    var rest = db.GetItemsCredit_proc(StkID).ToList();
                    return Json(rest, JsonRequestBehavior.AllowGet);

        }

        public ActionResult ExportBalanceReport( int StkID = 0)
        {

            var rest = db.GetItemsCredit_proc(StkID).ToList();

           
          

            Microsoft.Reporting.WebForms.LocalReport rpt = new Microsoft.Reporting.WebForms.LocalReport();

            /* Bind Here Report Data Set */
            string filename = DateTime.Now.ToString() + ".pdf";
            byte[] streamBytes = null;
            string mimeType = "";
            string encoding = "";
            string filenameExtension = "";
            string[] streamids = null;
            Warning[] warnings = null;


            string RptPath = Server.MapPath(@"~\Reports\ItemsBalance\BalanceReport.rdlc");

            ReportViewer rv = new ReportViewer();

            ReportDataSource rds = new ReportDataSource();
            rds.Name = "DataSet1";
            rds.Value = rest;

           

            //ReportViewer rv = new Microsoft.Reporting.WebForms.ReportViewer();
            rv.ProcessingMode = ProcessingMode.Local;
            rv.LocalReport.ReportPath = RptPath;

            // Add the new report datasource to the report.
            rv.LocalReport.DataSources.Add(rds);

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            rv.LocalReport.EnableHyperlinks = true;


            rv.LocalReport.Refresh();

            streamBytes = rv.LocalReport.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
            // This will download the pdf file
            //return File(streamBytes, mimeType, filename);

            //This will open directly the pdf file
            string Agent = HttpContext.Request.Headers["User-Agent"].ToString();

            //create and set PdfStamper  

            if (Agent.Contains("Android"))
                return File(streamBytes, "application/pdf", filename);
            else
                return File(streamBytes, "application/pdf");


        }

        public ActionResult ExportDailyReport(DateTime DateF)
        {

            var rest = db.DailyReport(DateF).ToList();




            Microsoft.Reporting.WebForms.LocalReport rpt = new Microsoft.Reporting.WebForms.LocalReport();

            /* Bind Here Report Data Set */
            string filename = DateTime.Now.ToString() + ".pdf";
            byte[] streamBytes = null;
            string mimeType = "";
            string encoding = "";
            string filenameExtension = "";
            string[] streamids = null;
            Warning[] warnings = null;


            string RptPath = Server.MapPath(@"~\Reports\DayReport\DayReport.rdlc");

            ReportViewer rv = new ReportViewer();

            ReportDataSource rds = new ReportDataSource();
            rds.Name = "DataSet1";
            rds.Value = rest;



            //ReportViewer rv = new Microsoft.Reporting.WebForms.ReportViewer();
            rv.ProcessingMode = ProcessingMode.Local;
            rv.LocalReport.ReportPath = RptPath;

            // Add the new report datasource to the report.
            rv.LocalReport.DataSources.Add(rds);

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            rv.LocalReport.EnableHyperlinks = true;


            rv.LocalReport.Refresh();

            streamBytes = rv.LocalReport.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
            // This will download the pdf file
            //return File(streamBytes, mimeType, filename);

            //This will open directly the pdf file
            string Agent = HttpContext.Request.Headers["User-Agent"].ToString();

            //create and set PdfStamper  

            if (Agent.Contains("Android"))
                return File(streamBytes, "application/pdf", filename);
            else
                return File(streamBytes, "application/pdf");


        }

        public ActionResult ExportMonthReport(DateTime DateF)
        {

            var rest = db.MonthReport(DateF).ToList();




            Microsoft.Reporting.WebForms.LocalReport rpt = new Microsoft.Reporting.WebForms.LocalReport();

            /* Bind Here Report Data Set */
            string filename = DateTime.Now.ToString() + ".pdf";
            byte[] streamBytes = null;
            string mimeType = "";
            string encoding = "";
            string filenameExtension = "";
            string[] streamids = null;
            Warning[] warnings = null;


            string RptPath = Server.MapPath(@"~\Reports\MonthlyReport\MonthReport.rdlc");

            ReportViewer rv = new ReportViewer();

            ReportDataSource rds = new ReportDataSource();
            rds.Name = "DataSet1";
            rds.Value = rest;



            //ReportViewer rv = new Microsoft.Reporting.WebForms.ReportViewer();
            rv.ProcessingMode = ProcessingMode.Local;
            rv.LocalReport.ReportPath = RptPath;

            // Add the new report datasource to the report.
            rv.LocalReport.DataSources.Add(rds);

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            rv.LocalReport.EnableHyperlinks = true;


            rv.LocalReport.Refresh();

            streamBytes = rv.LocalReport.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
            // This will download the pdf file
            //return File(streamBytes, mimeType, filename);

            //This will open directly the pdf file
            string Agent = HttpContext.Request.Headers["User-Agent"].ToString();

            //create and set PdfStamper  

            if (Agent.Contains("Android"))
                return File(streamBytes, "application/pdf", filename);
            else
                return File(streamBytes, "application/pdf");


        }
        public ActionResult ExportYearReport(float DateF)
        {

            var rest = db.YearReport(DateF).ToList();




            Microsoft.Reporting.WebForms.LocalReport rpt = new Microsoft.Reporting.WebForms.LocalReport();

            /* Bind Here Report Data Set */
            string filename = DateTime.Now.ToString() + ".pdf";
            byte[] streamBytes = null;
            string mimeType = "";
            string encoding = "";
            string filenameExtension = "";
            string[] streamids = null;
            Warning[] warnings = null;


            string RptPath = Server.MapPath(@"~\Reports\YearlyReport\YearReport.rdlc");

            ReportViewer rv = new ReportViewer();

            ReportDataSource rds = new ReportDataSource();
            rds.Name = "DataSet1";
            rds.Value = rest;



            //ReportViewer rv = new Microsoft.Reporting.WebForms.ReportViewer();
            rv.ProcessingMode = ProcessingMode.Local;
            rv.LocalReport.ReportPath = RptPath;

            // Add the new report datasource to the report.
            rv.LocalReport.DataSources.Add(rds);

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            rv.LocalReport.EnableHyperlinks = true;


            rv.LocalReport.Refresh();

            streamBytes = rv.LocalReport.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
            // This will download the pdf file
            //return File(streamBytes, mimeType, filename);

            //This will open directly the pdf file
            string Agent = HttpContext.Request.Headers["User-Agent"].ToString();

            //create and set PdfStamper  

            if (Agent.Contains("Android"))
                return File(streamBytes, "application/pdf", filename);
            else
                return File(streamBytes, "application/pdf");


        }

    }

}