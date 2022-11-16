
<%@ Page Language="C#"  Inherits="System.Web.Mvc.ViewPage" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {//CustName
        if (!Page.IsPostBack)
        {

            System.Data.DataTable T = new System.Data.DataTable() ;
            T = ViewBag.AccountData;

            String ReportName = "PersonCreditStatment.rdlc";
            RV.LocalReport.ReportPath = Server.MapPath($"~/Reports/{ReportName}");
            RV.LocalReport.DataSources.Clear();
            RV.LocalReport.DataSources.Add(new ReportDataSource("DataSet",T));
            ReportParameter[] P = new ReportParameter[4];
            P[0] = new ReportParameter("Image", ViewBag.Logo);
            P[1] = new ReportParameter("ShopName", ViewBag.Name);
            P[2] = new ReportParameter("Name", ViewBag.AccountName);


            RV.LocalReport.SetParameters(P);
            RV.LocalReport.EnableExternalImages = true;
            RV.LocalReport.Refresh();
            RV.DataBind();


        }
    }
</script>
<body>
    <form id="form1" runat="server">
        <div class="center-block" >
			<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
           <asp:TextBox runat="server" ID="yu" Width="211px"></asp:TextBox>
           
			<rsweb:ReportViewer runat="server" ID="RV" AsyncRendering="false" SizeToReportContent="true" >
			</rsweb:ReportViewer>
        </div>
    
 
    </form>
</body>

