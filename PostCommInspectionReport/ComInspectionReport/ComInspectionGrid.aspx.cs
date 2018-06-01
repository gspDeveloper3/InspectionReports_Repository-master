using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PostCommInspectionReport.ComInspectionReport
{
    public partial class ComInspectionGrid : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ReportID = GridView1.EmptyDataText;
        }

        protected void MainLink_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            Session["RecordID"] = btn.Text;
            Response.Redirect("ComInspectionReport.aspx");
        }

        protected void rbGridSortList_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            //TODO filter grid with correct info
            //throw new NotImplementedException();
        }
    }
}