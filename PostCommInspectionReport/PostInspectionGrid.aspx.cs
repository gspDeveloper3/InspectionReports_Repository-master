using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using PostCommInspectionReport.Properties;
using PostCommInspectionReport.Utilis;


namespace PostCommInspectionReport
{
    public partial class PostInspectionGrid : Page
    {
        readonly string _dbConn = Settings.Default.userConnect;
        readonly string _spConnect = Settings.Default.spConnect;
        private string employeeJobCode = string.Empty;
        
        protected void Page_Load(object sender, EventArgs e)
        {

            //get data and populate table
            var currentUser = User.Identity.Name.Substring(6);
           
            var employeePostionNUmber = string.Empty;
            var troop = string.Empty;
            var mycon = new SqlConnection(_dbConn);
            mycon.Open();
            var cmd = new SqlCommand(
                string.Format(@"select * from ViewInspection where sAMAccountName='{0}'", currentUser),
                mycon);
            var read = cmd.ExecuteReader();
            while (read.Read())
            {
                troop = read["parentUnit"].ToString(); // Troop = parentUnit in the database    
                employeeJobCode = read["EmployeeJobCode"].ToString();
                employeePostionNUmber = read["EmployeePostionNUmber"].ToString();
            }
            mycon.Close();
            //var util = new Utils();
            lblTroop.Text = troop;
            switch (employeeJobCode.Trim())
            {
                case "GST051":
                    GetData(lblTroop.Text);
                    break;
                case "PSM020":
                    GetData(lblTroop.Text);
                    break;
                case "PSM021":
                    GetData(lblTroop.Text);
                    break;
                case "PSM022":
                    GetData(lblTroop.Text);
                    break;
                default:
                    if (employeePostionNUmber == "00105296" )
                    {
                        var mycon2 = new SqlConnection(_spConnect);
                        DataSet dataSet = new DataSet();
                        SqlCommand cmd2 =
                            new SqlCommand("SELECT * FROM PostInspectionReport",
                                mycon2) { CommandType = CommandType.Text };
                        SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd2);
                        dataAdapter.Fill(dataSet);
                        gridPostInspection.DataSource = dataSet.Tables[0];
                        gridPostInspection.DataBind();
                        mycon2.Close();
                    }
                    else
                    {
                        Response.Redirect("UnauthorizedUser.aspx");
                    }
                    break;
            }

            #region OldCode
            //       var matchJobCode =
            //util.AuthorizedEmployeeJobCodeList().FirstOrDefault(x => x.Contains(employeePostionNUmber));
            //       if (matchJobCode != null && (!matchJobCode.Equals(employeeJobCode)) || (!employeePostionNUmber.Equals("00105296")))
            //       {
            //           Response.Redirect("UnauthorizedUser.aspx");
            //       }
            //       else
            //       {
            //           //If major---get * reports
            //           if (employeePostionNUmber == "00105296")
            //           {
            //               var mycon2 = new SqlConnection(_spConnect);
            //               DataSet dataSet = new DataSet();
            //               SqlCommand cmd2 =
            //                   new SqlCommand("SELECT * FROM PostInspectionReport",
            //                       mycon2) { CommandType = CommandType.Text };
            //               SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd2);
            //               dataAdapter.Fill(dataSet);
            //               gridPostInspection.DataSource = dataSet.Tables[0];
            //               gridPostInspection.DataBind();
            //               mycon2.Close();
            //           }
            //           else
            //           {
            //               //Get All reports for Troop
            //               try
            //               {
            //                   var mycon2 = new SqlConnection(_spConnect);
            //                   DataSet dataSet = new DataSet();
            //                   SqlCommand cmd2 =
            //                       new SqlCommand("SELECT * FROM PostInspectionReport WHERE Troop='" + troop + "'",
            //                           mycon2) { CommandType = CommandType.Text };
            //                   SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd2);
            //                   dataAdapter.Fill(dataSet);
            //                   gridPostInspection.DataSource = dataSet.Tables[0];
            //                   gridPostInspection.DataBind();
            //                   mycon2.Close();
            //               }
            //               catch (Exception ex)
            //               {
            //                   //TODO ADD ERROR HANDLING
            //                   throw ex;
            //               }
            //           }
            //       } 
            #endregion

        }

        protected void lblReportIDLink_OnClick(object sender, EventArgs e)
        {
            LinkButton lb = (LinkButton)sender;
            Session["ReportId"] = lb.CommandArgument;
            var reportId = lb.CommandArgument;
            //gridPostInspection.SelectedRow.DataItem;
            var reportStage = string.Empty;
            //get report Stage
            var mycon = new SqlConnection(_spConnect);
            mycon.Open();
            var cmd = new SqlCommand(
                @"select ReportStage from [PostInspectionReport] where ReportId= '" + reportId + "'",
                mycon);
            /*("SELECT PostId, Post FROM [dps_common].[dbo].[PostTroop] WHERE TROOP = '" + troop + "'", dbConnect);*/
            var read = cmd.ExecuteReader();
            while (read.Read())
            {
                reportStage = read["ReportStage"].ToString(); // Troop = parentUnit in the database    
                //post = read["parentUnit"].ToString();
                //city = read["parentUnit"].ToString();

            }
            mycon.Close();

            Response.Redirect("~/PostInspectionReport.aspx?reportID=" + Request.QueryString["ReportId"]);
        }

        protected void rbGridSortList_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if ((lblTroop.Text == "MCCD") || (lblTroop.Text == "Comptroller Ofc") || (lblTroop.Text == "State Patrol") || (lblTroop.Text == "HQ"))
                GetData();
            GetData(lblTroop.Text);
        }

        private DataSet GetData()
        {
            var status = rbGridSortList.SelectedItem.Text ?? "Pending";

            // var  = rbGridSortList.SelectedItem.Text;
            //if (rbGridSortList.SelectedItem.Text == "")
            //{
            //    status = "Pending";
            //}
            // var troop = lblTroop.Text;
            var mycon = new SqlConnection(_spConnect);

            DataSet dataSet = new DataSet();
            using (new SqlConnection(_spConnect))
            {
                try
                {
                    if (status != string.Empty)
                    {

                        SqlCommand cmd =
                            new SqlCommand("SELECT * FROM PostInspectionReport WHERE ReportStatus='" + status + "'",
                                mycon) { CommandType = CommandType.Text };
                        SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                        dataAdapter.Fill(dataSet);
                        gridPostInspection.DataSource = dataSet.Tables[0];
                        gridPostInspection.DataBind();
                    }
                }
                catch (Exception exception)
                {
                    //TODO ADD ERROR HANDLING
                    throw exception;
                }
            }
            return dataSet;
        }


        private DataSet GetData(string troop)
        {
            var status = rbGridSortList.SelectedItem.Text.Trim();
       
            // var troop = lblTroop.Text;
            var mycon = new SqlConnection(_spConnect);

            DataSet dataSet = new DataSet();
            using (new SqlConnection(_spConnect))
            {
                try
                {
                    if (status != string.Empty)
                    {

                        SqlCommand cmd =
                            new SqlCommand("SELECT * FROM PostInspectionReport WHERE ReportStatus='" + status + "'" + "AND troop ='" + troop + "'",
                                mycon) { CommandType = CommandType.Text };
                        SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                        dataAdapter.Fill(dataSet);
                        gridPostInspection.DataSource = dataSet.Tables[0];
                        gridPostInspection.DataBind();
                    }
                }
                catch (Exception exception)
                {
                    //TODO ADD ERROR HANDLING
                    throw exception;
                }
            }
            return dataSet;
        }



        protected void gridPostInspection_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if ((lblTroop.Text == "MCCD") || (lblTroop.Text == "Comptroller Ofc") || (lblTroop.Text == "State Patrol") || (lblTroop.Text == "HQ"))
            {
                gridPostInspection.PageIndex = e.NewPageIndex;
                DataSet gvInspectionDataSet1 = GetData();
                gridPostInspection.DataSource = gvInspectionDataSet1;
                gridPostInspection.DataBind();
            }
            else
            {
                gridPostInspection.PageIndex = e.NewPageIndex;
                DataSet gvInspectionDataSet = GetData(lblTroop.Text);
                gridPostInspection.DataSource = gvInspectionDataSet;
                gridPostInspection.DataBind();
            }
        }

        protected void ddlSearch_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (!ddlSearch.SelectedValue.Equals("-1")) 
            {
                tbxSearch.Enabled = true;
            }
            else
            {
                tbxSearch.Enabled = false;
            }

            gridPostInspection.DataSource = null;
            gridPostInspection.EmptyDataText = string.Empty;
            gridPostInspection.DataBind();

            tbxSearch.Text = string.Empty;
        }

        protected void btnSearch_OnClick(object sender, EventArgs e)
        {
            lblSearchErrorMst.Text = string.Empty;

            if (ddlSearch.SelectedItem.Text == "Report Id" && string.IsNullOrWhiteSpace(tbxSearch.Text))
            {
                lblSearchErrorMst.Visible = true;
                lblSearchErrorMst.Text = "Please enter a number for Report ID.";
            }
            else if (ddlSearch.SelectedValue == "-1")
            {
                lblSearchErrorMst.Visible = true;
                lblSearchErrorMst.Text = "Please select a search type.";
            }
            else if (tbxSearch.Text == string.Empty)
            {
                lblSearchErrorMst.Visible = true;
                lblSearchErrorMst.Text = "Please enter a value.";
            }
            else
            {
                lblSearchErrorMst.Visible = false;
               var util = new Utils();
                

                string reportID = string.Empty;
                string post = string.Empty;
                string troop = string.Empty;

                switch (ddlSearch.SelectedValue)
                {
                    case "reportID":
                        reportID = tbxSearch.Text;
                        break;
                    case "post":
                        post = tbxSearch.Text;
                        break;
                    case "troop":
                        troop = tbxSearch.Text;
                        break;
                }

              Session["Data"] = util.GetReortSearchData(troop,reportID,post);
                gridPostInspection.DataSource = Session["Data"];
                gridPostInspection.DataBind();
         
            }
        }

        protected void btnClear_OnClick(object sender, EventArgs e)
        {
            gridPostInspection.DataSource = null;
            gridPostInspection.EmptyDataText = string.Empty;
            gridPostInspection.DataBind();

            tbxSearch.Text = string.Empty;
        }
    }
}