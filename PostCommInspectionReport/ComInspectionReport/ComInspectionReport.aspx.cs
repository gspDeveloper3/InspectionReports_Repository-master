using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mail;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PostCommInspectionReport.ComInspectionReport
{
    public partial class ComInspectionReport : System.Web.UI.Page
    {
        private string con = Properties.Settings.Default.spConnect;
        private string realidentity = Properties.Settings.Default.userConnect;
        string con1 = Properties.Settings.Default.spConnect;
        string con2 = Properties.Settings.Default.spConnect;
        string Rs, Es, Ms, Cs, Fs, Ra, Co, Un, Mi, De, Ti, mainadmin, mainbuild, mainequip, mainperson, employeeid, CreatedBy;
        string savedate = DateTime.Now.ToString("MM-dd-yyyy hh:mm:ss tt");
        string rejectCO, rejectTO, rejectTC;



        protected void Page_Load(object sender, EventArgs e)
        {
            //formsave.Enabled = false;
            upperformdisable();
            lowerformdisable();
            //Button1.Enabled = false;
            if (this.IsPostBack)
            {
                lowerformEnable();
            }
            string user = User.Identity.Name;

            #region Getinfo
            string mainuser = user.Substring(6);
            //string mainuser = "dloyd";
            //user1.Text = "Welcome " + mainuser + "!";
            datevalue.Text = DateTime.Now.ToString("MM-dd-yyyy");
            SqlConnection connection1 = new SqlConnection(realidentity);
            connection1.Open();
            SqlDataReader sqlDataReader1 = new SqlCommand("select parentUnit,City from ViewInspection where sAMAccountName='" + mainuser + "'", connection1).ExecuteReader();
            while (sqlDataReader1.Read())
            {
                string str2 = sqlDataReader1["parentUnit"].ToString();
                trooper.Text = str2;
                citymain.Text = sqlDataReader1["City"].ToString();
                if (str2 == "Troop A")
                    reportid.Text = " TACC-" + DateTime.Now.ToString("MMddyyyy");
                else if (str2 == "Troop B")
                    reportid.Text = " TBCC-" + DateTime.Now.ToString("MMddyyyy");
                else if (str2 == "Troop C")
                    reportid.Text = " TCCC-" + DateTime.Now.ToString("MMddyyyy");
                else if (str2 == "Troop D")
                    reportid.Text = " TDCC-" + DateTime.Now.ToString("MMddyyyy");
                else if (str2 == "Troop E")
                    reportid.Text = " TECC-" + DateTime.Now.ToString("MMddyyyy");
                else if (str2 == "Troop F")
                    reportid.Text = " TFCC-" + DateTime.Now.ToString("MMddyyyy");
                else if (str2 == "Troop G")
                    reportid.Text = " TGCC-" + DateTime.Now.ToString("MMddyyyy");
                else if (str2 == "Troop H")
                    reportid.Text = " THCC-" + DateTime.Now.ToString("MMddyyyy");
                else if (str2 == "Troop I")
                    reportid.Text = " TICC-" + DateTime.Now.ToString("MMddyyyy");
                else if (str2 == "Technology")
                    reportid.Text = " TECH-" + DateTime.Now.ToString("MMddyyyy");
            }
            connection1.Close();
            #endregion
          
            #region identity
            if (!IsPostBack)
            {
                SqlConnection connection2 = new SqlConnection(realidentity);
                connection2.Open();
                SqlDataReader sqlDataReader2 = new SqlCommand("SELECT EmployeeJobCode,EmployeePostionNUmber FROM ViewInspection WHERE sAMAccountName='" + mainuser + "'", connection2).ExecuteReader();
                while (sqlDataReader2.Read())
                {
                    string str2 = sqlDataReader2["EmployeeJobCode"].ToString();
                    string Pstr = sqlDataReader2["EmployeePostionNUmber"].ToString();
                  
                    if (str2 == "GST113") // Chief Operator
                    {
                        #region Chief Operator
                        this.reject.Visible = false;
                        this.approve.Visible = false;
                        this.formupdate.Visible = false;
                        this.troopcom.Visible = false;
                        datevalue.Enabled = true;
                        this.hdrLabel.InnerText = "COMMUNCICATION INSPECTION REPORT--Chief Operator";
                        using (SqlConnection sqlConnection = new SqlConnection(this.realidentity))
                        {
                            using (SqlCommand sqlCommand = new SqlCommand("SELECT EmployeeName,sAMAccountName FROM ViewInspection where EmployeeJobCode='PSM021' AND parentUnit='" + this.trooper.Text + "'"))
                            {
                                sqlCommand.CommandType = CommandType.Text;
                                sqlCommand.Connection = sqlConnection;
                                sqlConnection.Open();
                                this.person2.DataSource = (object)sqlCommand.ExecuteReader();
                                this.person2.DataTextField = "EmployeeName";
                                this.person2.DataValueField = "sAMAccountName";
                                this.person2.DataBind();
                                connection1.Close();
                            }
                        }

                        #region Load table Data
                        if (this.Session["RecordID"] != null)
                        {
                            string str3 = (string)this.Session["RecordID"];
                            this.reportid.Text = str3;
                            SqlConnection connection3 = new SqlConnection(this.con1);
                            connection3.Open();
                            SqlDataReader sqlDataReader3 = new SqlCommand("SELECT * FROM CommInspectionReport WHERE ReportID='" + str3 + "'", connection3).ExecuteReader();
                            while (sqlDataReader3.Read())
                            {
                                this.datevalue.Text = sqlDataReader3["CreatedDate"].ToString().Substring(0, 10);
                                this.totalcalls_cm.Text = sqlDataReader3["TotalCalls_CM"].ToString();
                                this.totalcalls_cm2.Text = sqlDataReader3["TotalCalls_CM2"].ToString();
                                this.totalcalls_lm.Text = sqlDataReader3["TotalCalls_LM"].ToString();
                                this.totalcalls_ytd_cy.Text = sqlDataReader3["TotalCalls_YTD_CY"].ToString();
                                this.totalcalls_smly.Text = sqlDataReader3["TotalCalls_SMLY"].ToString();
                                this.totalcalls_smly2.Text = sqlDataReader3["TotalCalls_SMLY2"].ToString();
                                this.totalcalls_ytd_ly.Text = sqlDataReader3["TotalCalls_YTD_LY"].ToString();
                                this.abandonedveh_cm.Text = sqlDataReader3["AbandonedVeh_CM"].ToString();
                                this.abandonedveh_cm2.Text = sqlDataReader3["AbandonedVeh_CM2"].ToString();
                                this.abandonedveh_lm.Text = sqlDataReader3["AbandonedVeh_LM"].ToString();
                                this.abandonedveh_ytd_cy.Text = sqlDataReader3["AbandonedVeh_YTD_CY"].ToString();
                                this.abandonedveh_smly.Text = sqlDataReader3["AbandonedVeh_SMLY"].ToString();
                                this.abandonedveh_smly2.Text = sqlDataReader3["AbandonedVeh_SMLY2"].ToString();
                                this.abandonedveh_ytd_ly.Text = sqlDataReader3["AbandonedVeh_YTD_LY"].ToString();
                                this.chases_cm.Text = sqlDataReader3["Chases_CM"].ToString();
                                this.chases_cm2.Text = sqlDataReader3["Chases_CM2"].ToString();
                                this.chases_lm.Text = sqlDataReader3["Chases_LM"].ToString();
                                this.chases_ytd_cy.Text = sqlDataReader3["Chases_YTD_CY"].ToString();
                                this.chases_smly.Text = sqlDataReader3["Chases_SMLY"].ToString();
                                this.chases_smly2.Text = sqlDataReader3["Chases_SMLY2"].ToString();
                                this.chases_ytd_ly.Text = sqlDataReader3["Chases_YTD_LY"].ToString();
                                this.controlledburns_cm.Text = sqlDataReader3["ControlledBurns_CM"].ToString();
                                this.controlledburns_cm2.Text = sqlDataReader3["ControlledBurns_CM2"].ToString();
                                this.controlledburns_lm.Text = sqlDataReader3["ControlledBurns_LM"].ToString();
                                this.controlledburns_ytd_cy.Text = sqlDataReader3["ControlledBurns_YTD_CY"].ToString();
                                this.controlledburns_smly.Text = sqlDataReader3["ControlledBurns_SMLY"].ToString();
                                this.controlledburns_smly2.Text = sqlDataReader3["ControlledBurns_SMLY2"].ToString();
                                this.controlledburns_ytd_ly.Text = sqlDataReader3["ControlledBurns_YTD_LY"].ToString();
                                this.criminaldriverhx_cm.Text = sqlDataReader3["CriminalDriverHX_CM"].ToString();
                                this.criminaldriverhx_lm.Text = sqlDataReader3["CriminalDriverHX_LM"].ToString();
                                this.criminaldriverhx_ytd_cy.Text = sqlDataReader3["CriminalDriverHX_YTD_CY"].ToString();
                                this.criminaldriverhx_smly.Text = sqlDataReader3["CriminalDriverHX_SMLY"].ToString();
                                this.criminaldriverhx_smly2.Text = sqlDataReader3["CriminalDriverHX_SMLY2"].ToString();
                                this.criminaldriverhx_ytd_ly.Text = sqlDataReader3["CriminalDriverHX_YTD_LY"].ToString();
                                this.dnrcalls_cm.Text = sqlDataReader3["DNRCalls_CM"].ToString();
                                this.dnrcalls_lm.Text = sqlDataReader3["DNRCalls_LM"].ToString();
                                this.dnrcalls_ytd_cy.Text = sqlDataReader3["DNRCalls_YTD_CY"].ToString();
                                this.dnrcalls_smly.Text = sqlDataReader3["DNRCalls_SMLY"].ToString();
                                this.dnrcalls_ytd_ly.Text = sqlDataReader3["DNRCalls_YTD_LY"].ToString();
                                this.equipissue_cm.Text = sqlDataReader3["EquipIssue_CM"].ToString();
                                this.equipissue_lm.Text = sqlDataReader3["EquipIssue_LM"].ToString();
                                this.equipissue_ytd_cy.Text = sqlDataReader3["EquipIssue_YTD_CY"].ToString();
                                this.equipissue_smly.Text = sqlDataReader3["EquipIssue_SMLY"].ToString();
                                this.equipissue_ytd_ly.Text = sqlDataReader3["EquipIssue_YTD_LY"].ToString();
                                this.flightplans_cm.Text = sqlDataReader3["FlightPlans_CM"].ToString();
                                this.flightplans_lm.Text = sqlDataReader3["FlightPlans_LM"].ToString();
                                this.flightplans_ytd_cy.Text = sqlDataReader3["FlightPlans_YTD_CY"].ToString();
                                this.flightplans_smly.Text = sqlDataReader3["FlightPlans_SMLY"].ToString();
                                this.flightplans_ytd_ly.Text = sqlDataReader3["FlightPlans_YTD_LY"].ToString();
                                this.gcichits_cm.Text = sqlDataReader3["GCICHits_CM"].ToString();
                                this.gcichits_lm.Text = sqlDataReader3["GCICHits_LM"].ToString();
                                this.gcichits_ytd_cy.Text = sqlDataReader3["GCICHits_YTD_CY"].ToString();
                                this.gcichits_smly.Text = sqlDataReader3["GCICHits_SMLY"].ToString();
                                this.gcichits_ytd_ly.Text = sqlDataReader3["GCICHits_YTD_LY"].ToString();
                                this.gcichits_wantedperson.Text = sqlDataReader3["GCICHits_WantedPerson"].ToString();
                                this.motoassist_cm.Text = sqlDataReader3["MototAssist_CM"].ToString();
                                this.motoassist_lm.Text = sqlDataReader3["MototAssist_LM"].ToString();
                                this.motoassist_ytd_cy.Text = sqlDataReader3["MototAssist_YTD_CY"].ToString();
                                this.motoassist_smly.Text = sqlDataReader3["MototAssist_SMLY"].ToString();
                                this.motoassist_ytd_ly.Text = sqlDataReader3["MototAssist_YTD_LY"].ToString();
                                this.motoassist_stolevehicle.Text = sqlDataReader3["MototAssist_StoleVehicle"].ToString();
                                this.openrecord_cm.Text = sqlDataReader3["OpenRecords_CM"].ToString();
                                this.openrecord_lm.Text = sqlDataReader3["OpenRecords_LM"].ToString();
                                this.openrecord_ytd_cy.Text = sqlDataReader3["OpenRecords_YTD_CY"].ToString();
                                this.openrecord_smly.Text = sqlDataReader3["OpenRecords_SMLY"].ToString();
                                this.openrecord_ytd_ly.Text = sqlDataReader3["OpenRecords_YTD_LY"].ToString();
                                this.openrecord_stoleguns.Text = sqlDataReader3["OpenRecords_StoleGuns"].ToString();
                                this.relays_cm.Text = sqlDataReader3["Relays_CM"].ToString();
                                this.relays_lm.Text = sqlDataReader3["Relays_LM"].ToString();
                                this.relays_ytd_cy.Text = sqlDataReader3["Relays_YTD_CY"].ToString();
                                this.relays_smly.Text = sqlDataReader3["Relays_SMLY"].ToString();
                                this.relays_ytd_ly.Text = sqlDataReader3["Relays_YTD_LY"].ToString();
                                this.relays_other.Text = sqlDataReader3["Relays_Other"].ToString();
                                this.roadchecks_cm.Text = sqlDataReader3["RoadChecks_CM"].ToString();
                                this.roadchecks_lm.Text = sqlDataReader3["RoadChecks_LM"].ToString();
                                this.roadchecks_ytd_cy.Text = sqlDataReader3["RoadChecks_YTD_CY"].ToString();
                                this.roadchecks_smly.Text = sqlDataReader3["RoadChecks_SMLY"].ToString();
                                this.roadchecks_ytd_ly.Text = sqlDataReader3["RoadChecks_YTD_LY"].ToString();
                                this.trafficstops_cm.Text = sqlDataReader3["TrafficStops_CM"].ToString();
                                this.trafficstops_lm.Text = sqlDataReader3["TrafficStops_LM"].ToString();
                                this.trafficstops_ytd_cy.Text = sqlDataReader3["TrafficStops_YTD_CY"].ToString();
                                this.trafficstops_smly.Text = sqlDataReader3["TrafficStops_SMLY"].ToString();
                                this.trafficstops_ytd_ly.Text = sqlDataReader3["TrafficStops_YTD_LY"].ToString();
                                this.towedveh_cm.Text = sqlDataReader3["TowedVeh_CM"].ToString();
                                this.towedveh_lm.Text = sqlDataReader3["TowedVeh_LM"].ToString();
                                this.towedveh_ytd_cy.Text = sqlDataReader3["TowedVeh_YTD_CY"].ToString();
                                this.towedveh_smly.Text = sqlDataReader3["TowedVeh_SMLY"].ToString();
                                this.towedveh_ytd_ly.Text = sqlDataReader3["TowedVeh_YTD_LY"].ToString();
                                this.remarks.Text = sqlDataReader3["Remarks"].ToString();
                                this.upperformenabled();
                                this.lowerformEnable();
                            }
                        }
                        #endregion

                        upperformdisable();
                        datevalue.Enabled = true;

                        this.person2.Items.Insert(0, new ListItem("SELECT", "-1"));
                        this.person2.Items.Insert(1, new ListItem("TEAM TRIAL", "pyeboah"));
                        #endregion
                    }
                    else if (str2 == "PSM021") //Troop Officer
                    {
                        #region Troop Officer
                        this.approve.Visible = false;
                        this.save.Visible = false;
                        this.Button1.Visible = false;
                        this.troopcom.Visible = false;
                        this.hdrLabel.InnerText = "COMMUNCICATION INSPECTION REPORT--Troop Officer";
                        this.reportid.Text = Session["RecordID"].ToString();
                        using (SqlConnection sqlConnection = new SqlConnection(this.con))
                        {
                            using (SqlCommand sqlCommand = new SqlCommand("SELECT EmployeeName,sAMAccountName FROM ViewInspection where EmployeeJobCode='PSM022' AND parentUnit='" + this.trooper.Text + "'"))
                            {
                                sqlCommand.CommandType = CommandType.Text;
                                sqlCommand.Connection = sqlConnection;
                                sqlConnection.Open();
                                this.person2.DataSource = (object)sqlCommand.ExecuteReader();
                                this.person2.DataTextField = "EmployeeName";
                                this.person2.DataValueField = "sAMAccountName";
                                this.person2.DataBind();
                                connection1.Close();
                            }
                        }
                        this.person2.Items.Insert(0, new ListItem("SELECT", "-1"));
                        this.person2.Items.Insert(1, new ListItem("TEAM TRIAL", "pyeboah"));

                        #region Checking if Already rated
                        SqlConnection connection33 = new SqlConnection(this.con1);
                        connection33.Open();
                        SqlDataReader sqlDataReader33 = new SqlCommand("SELECT * FROM CommInspectionReport where  ReportID='" + (string)this.Session["RecordID"] + "'", connection33).ExecuteReader();
                        while (sqlDataReader33.Read())
                        {
                            this.pp.Text = sqlDataReader33["PersonnelPresent"].ToString();
                            this.remarks.Text = sqlDataReader33["Remarks"].ToString();
                            //this.person2.SelectedItem.Text = sqlDataReader33["ApprovalOfficer"].ToString();
                            switch (sqlDataReader33["RecordsAndReports"].ToString())
                            {
                                case "Execellent":
                                    this.record.Checked = true;
                                    this.r1.Checked = false;
                                    this.r2.Checked = false;
                                    this.r3.Checked = false;
                                    break;
                                case "Very Satisfactory":
                                    this.record.Checked = false;
                                    this.r1.Checked = true;
                                    this.r2.Checked = false;
                                    this.r3.Checked = false;
                                    break;
                                case "Satisfactory":
                                    this.record.Checked = false;
                                    this.r1.Checked = false;
                                    this.r2.Checked = true;
                                    this.r3.Checked = false;
                                    break;
                                case "UnSatisfactory":
                                    this.record.Checked = false;
                                    this.r1.Checked = false;
                                    this.r2.Checked = false;
                                    this.r3.Checked = true;
                                    break;
                            }
                            switch (sqlDataReader33["EmpPerformanceMgt"].ToString())
                            {
                                case "Execellent":
                                    this.employee.Checked = true;
                                    this.e1.Checked = false;
                                    this.e2.Checked = false;
                                    this.e3.Checked = false;
                                    break;
                                case "Very Satisfactory":
                                    this.employee.Checked = false;
                                    this.e1.Checked = true;
                                    this.e2.Checked = false;
                                    this.e3.Checked = false;
                                    break;
                                case "Satisfactory":
                                    this.employee.Checked = false;
                                    this.e1.Checked = false;
                                    this.e2.Checked = true;
                                    this.e3.Checked = false;
                                    break;
                                case "UnSatisfactory":
                                    this.employee.Checked = false;
                                    this.e1.Checked = false;
                                    this.e2.Checked = false;
                                    this.e3.Checked = true;
                                    break;
                            }
                            switch (sqlDataReader33["SchAndManAllocation"].ToString())
                            {
                                case "Execellent":
                                    this.manpower.Checked = true;
                                    this.m1.Checked = false;
                                    this.m2.Checked = false;
                                    this.m3.Checked = false;
                                    break;
                                case "Very Satisfactory":
                                    this.manpower.Checked = false;
                                    this.m1.Checked = true;
                                    this.m2.Checked = false;
                                    this.m3.Checked = false;
                                    break;
                                case "Satisfactory":
                                    this.manpower.Checked = false;
                                    this.m1.Checked = false;
                                    this.m2.Checked = true;
                                    this.m3.Checked = false;
                                    break;
                                case "UnSatisfactory":
                                    this.manpower.Checked = false;
                                    this.m1.Checked = false;
                                    this.m2.Checked = false;
                                    this.m3.Checked = true;
                                    break;
                            }
                            switch (sqlDataReader33["SchAndManAllocation"].ToString())
                            {
                                case "Execellent":
                                    this.manpower.Checked = true;
                                    this.m1.Checked = false;
                                    this.m2.Checked = false;
                                    this.m3.Checked = false;
                                    break;
                                case "Very Satisfactory":
                                    this.manpower.Checked = false;
                                    this.m1.Checked = true;
                                    this.m2.Checked = false;
                                    this.m3.Checked = false;
                                    break;
                                case "Satisfactory":
                                    this.manpower.Checked = false;
                                    this.m1.Checked = false;
                                    this.m2.Checked = true;
                                    this.m3.Checked = false;
                                    break;
                                case "UnSatisfactory":
                                    this.manpower.Checked = false;
                                    this.m1.Checked = false;
                                    this.m2.Checked = false;
                                    this.m3.Checked = true;
                                    break;
                            }
                            switch (sqlDataReader33["ComCenterCleanAndMain"].ToString())
                            {
                                case "Execellent":
                                    this.com.Checked = true;
                                    this.c1.Checked = false;
                                    this.c2.Checked = false;
                                    this.c3.Checked = false;
                                    break;
                                case "Very Satisfactory":
                                    this.com.Checked = false;
                                    this.c1.Checked = true;
                                    this.c2.Checked = false;
                                    this.c3.Checked = false;
                                    break;
                                case "Satisfactory":
                                    this.com.Checked = false;
                                    this.c1.Checked = false;
                                    this.c2.Checked = true;
                                    this.c3.Checked = false;
                                    break;
                                case "UnSatisfactory":
                                    this.com.Checked = false;
                                    this.c1.Checked = false;
                                    this.c2.Checked = false;
                                    this.c3.Checked = true;
                                    break;
                            }
                            switch (sqlDataReader33["FurnitureAndGrounds"].ToString())
                            {
                                case "Execellent":
                                    this.fur.Checked = true;
                                    this.f1.Checked = false;
                                    this.f2.Checked = false;
                                    this.f3.Checked = false;
                                    break;
                                case "Very Satisfactory":
                                    this.fur.Checked = false;
                                    this.f1.Checked = true;
                                    this.f2.Checked = false;
                                    this.f3.Checked = false;
                                    break;
                                case "Satisfactory":
                                    this.fur.Checked = false;
                                    this.f1.Checked = false;
                                    this.f2.Checked = true;
                                    this.f3.Checked = false;
                                    break;
                                case "UnSatisfactory":
                                    this.fur.Checked = false;
                                    this.f1.Checked = false;
                                    this.f2.Checked = false;
                                    this.f3.Checked = true;
                                    break;
                            }
                            switch (sqlDataReader33["RadioEquipment"].ToString())
                            {
                                case "Execellent":
                                    this.radio.Checked = true;
                                    this.ra1.Checked = false;
                                    this.ra2.Checked = false;
                                    this.ra3.Checked = false;
                                    break;
                                case "Very Satisfactory":
                                    this.radio.Checked = false;
                                    this.ra1.Checked = true;
                                    this.ra2.Checked = false;
                                    this.ra3.Checked = false;
                                    break;
                                case "Satisfactory":
                                    this.radio.Checked = false;
                                    this.ra1.Checked = false;
                                    this.ra2.Checked = true;
                                    this.ra3.Checked = false;
                                    break;
                                case "UnSatisfactory":
                                    this.radio.Checked = false;
                                    this.ra1.Checked = false;
                                    this.ra2.Checked = false;
                                    this.ra3.Checked = true;
                                    break;
                            }
                            switch (sqlDataReader33["ComputerPrinterETC"].ToString())
                            {
                                case "Execellent":
                                    this.computer.Checked = true;
                                    this.co1.Checked = false;
                                    this.co2.Checked = false;
                                    this.co3.Checked = false;
                                    break;
                                case "Very Satisfactory":
                                    this.computer.Checked = false;
                                    this.co1.Checked = true;
                                    this.co2.Checked = false;
                                    this.co3.Checked = false;
                                    break;
                                case "Satisfactory":
                                    this.computer.Checked = false;
                                    this.co1.Checked = false;
                                    this.co2.Checked = true;
                                    this.co3.Checked = false;
                                    break;
                                case "UnSatisfactory":
                                    this.computer.Checked = false;
                                    this.co1.Checked = false;
                                    this.co2.Checked = false;
                                    this.co3.Checked = true;
                                    break;
                            }
                            switch (sqlDataReader33["UniformAndAppearance"].ToString())
                            {
                                case "Execellent":
                                    this.uni.Checked = true;
                                    this.u1.Checked = false;
                                    this.u2.Checked = false;
                                    this.u3.Checked = false;
                                    break;
                                case "Very Satisfactory":
                                    this.uni.Checked = false;
                                    this.u1.Checked = true;
                                    this.u2.Checked = false;
                                    this.u3.Checked = false;
                                    break;
                                case "Satisfactory":
                                    this.uni.Checked = false;
                                    this.u1.Checked = false;
                                    this.u2.Checked = true;
                                    this.u3.Checked = false;
                                    break;
                                case "UnSatisfactory":
                                    this.uni.Checked = false;
                                    this.u1.Checked = false;
                                    this.u2.Checked = false;
                                    this.u3.Checked = true;
                                    break;
                            }
                            switch (sqlDataReader33["MilitaryCourtesy"].ToString())
                            {
                                case "Execellent":
                                    this.mil.Checked = true;
                                    this.mi1.Checked = false;
                                    this.mi2.Checked = false;
                                    this.mi3.Checked = false;
                                    break;
                                case "Very Satisfactory":
                                    this.mil.Checked = false;
                                    this.mi1.Checked = true;
                                    this.mi2.Checked = false;
                                    this.mi3.Checked = false;
                                    break;
                                case "Satisfactory":
                                    this.mil.Checked = false;
                                    this.mi1.Checked = false;
                                    this.mi2.Checked = true;
                                    this.mi3.Checked = false;
                                    break;
                                case "UnSatisfactory":
                                    this.mil.Checked = false;
                                    this.mi1.Checked = false;
                                    this.mi2.Checked = false;
                                    this.mi3.Checked = true;
                                    break;
                            }
                            switch (sqlDataReader33["DemeanorAndMorale"].ToString())
                            {
                                case "Execellent":
                                    this.dem.Checked = true;
                                    this.d1.Checked = false;
                                    this.d2.Checked = false;
                                    this.d3.Checked = false;
                                    break;
                                case "Very Satisfactory":
                                    this.dem.Checked = false;
                                    this.d1.Checked = true;
                                    this.d2.Checked = false;
                                    this.d3.Checked = false;
                                    break;
                                case "Satisfactory":
                                    this.dem.Checked = false;
                                    this.d1.Checked = false;
                                    this.d2.Checked = true;
                                    this.d3.Checked = false;
                                    break;
                                case "UnSatisfactory":
                                    this.dem.Checked = false;
                                    this.d1.Checked = false;
                                    this.d2.Checked = false;
                                    this.d3.Checked = true;
                                    break;
                            }
                            switch (sqlDataReader33["OnTimeAndPreparedForInspection"].ToString())
                            {
                                case "Execellent":
                                    this.time.Checked = true;
                                    this.t1.Checked = false;
                                    this.t2.Checked = false;
                                    this.t3.Checked = false;
                                    continue;
                                case "Very Satisfactory":
                                    this.time.Checked = false;
                                    this.t1.Checked = true;
                                    this.t2.Checked = false;
                                    this.t3.Checked = false;
                                    continue;
                                case "Satisfactory":
                                    this.time.Checked = false;
                                    this.t1.Checked = false;
                                    this.t2.Checked = true;
                                    this.t3.Checked = false;
                                    continue;
                                case "UnSatisfactory":
                                    this.time.Checked = false;
                                    this.t1.Checked = false;
                                    this.t2.Checked = false;
                                    this.t3.Checked = true;
                                    continue;
                                default:
                                    continue;
                            }
                        }
                        connection33.Close();
                        #endregion

                        #region Load table Data
                        if (this.Session["RecordID"] != null)
                        {
                            string str3 = (string)this.Session["RecordID"];
                            this.reportid.Text = str3;
                            SqlConnection connection3 = new SqlConnection(this.con1);
                            connection3.Open();
                            SqlDataReader sqlDataReader3 = new SqlCommand("SELECT * FROM CommInspectionReport WHERE ReportID='" + str3 + "'", connection3).ExecuteReader();
                            while (sqlDataReader3.Read())
                            {
                                this.datevalue.Text = sqlDataReader3["CreatedDate"].ToString().Substring(0, 10);
                                this.totalcalls_cm.Text = sqlDataReader3["TotalCalls_CM"].ToString();
                                this.totalcalls_cm2.Text = sqlDataReader3["TotalCalls_CM2"].ToString();
                                this.totalcalls_lm.Text = sqlDataReader3["TotalCalls_LM"].ToString();
                                this.totalcalls_ytd_cy.Text = sqlDataReader3["TotalCalls_YTD_CY"].ToString();
                                this.totalcalls_smly.Text = sqlDataReader3["TotalCalls_SMLY"].ToString();
                                this.totalcalls_smly2.Text = sqlDataReader3["TotalCalls_SMLY2"].ToString();
                                this.totalcalls_ytd_ly.Text = sqlDataReader3["TotalCalls_YTD_LY"].ToString();
                                this.abandonedveh_cm.Text = sqlDataReader3["AbandonedVeh_CM"].ToString();
                                this.abandonedveh_cm2.Text = sqlDataReader3["AbandonedVeh_CM2"].ToString();
                                this.abandonedveh_lm.Text = sqlDataReader3["AbandonedVeh_LM"].ToString();
                                this.abandonedveh_ytd_cy.Text = sqlDataReader3["AbandonedVeh_YTD_CY"].ToString();
                                this.abandonedveh_smly.Text = sqlDataReader3["AbandonedVeh_SMLY"].ToString();
                                this.abandonedveh_smly2.Text = sqlDataReader3["AbandonedVeh_SMLY2"].ToString();
                                this.abandonedveh_ytd_ly.Text = sqlDataReader3["AbandonedVeh_YTD_LY"].ToString();
                                this.chases_cm.Text = sqlDataReader3["Chases_CM"].ToString();
                                this.chases_cm2.Text = sqlDataReader3["Chases_CM2"].ToString();
                                this.chases_lm.Text = sqlDataReader3["Chases_LM"].ToString();
                                this.chases_ytd_cy.Text = sqlDataReader3["Chases_YTD_CY"].ToString();
                                this.chases_smly.Text = sqlDataReader3["Chases_SMLY"].ToString();
                                this.chases_smly2.Text = sqlDataReader3["Chases_SMLY2"].ToString();
                                this.chases_ytd_ly.Text = sqlDataReader3["Chases_YTD_LY"].ToString();
                                this.controlledburns_cm.Text = sqlDataReader3["ControlledBurns_CM"].ToString();
                                this.controlledburns_cm2.Text = sqlDataReader3["ControlledBurns_CM2"].ToString();
                                this.controlledburns_lm.Text = sqlDataReader3["ControlledBurns_LM"].ToString();
                                this.controlledburns_ytd_cy.Text = sqlDataReader3["ControlledBurns_YTD_CY"].ToString();
                                this.controlledburns_smly.Text = sqlDataReader3["ControlledBurns_SMLY"].ToString();
                                this.controlledburns_smly2.Text = sqlDataReader3["ControlledBurns_SMLY2"].ToString();
                                this.controlledburns_ytd_ly.Text = sqlDataReader3["ControlledBurns_YTD_LY"].ToString();
                                this.criminaldriverhx_cm.Text = sqlDataReader3["CriminalDriverHX_CM"].ToString();
                                this.criminaldriverhx_lm.Text = sqlDataReader3["CriminalDriverHX_LM"].ToString();
                                this.criminaldriverhx_ytd_cy.Text = sqlDataReader3["CriminalDriverHX_YTD_CY"].ToString();
                                this.criminaldriverhx_smly.Text = sqlDataReader3["CriminalDriverHX_SMLY"].ToString();
                                this.criminaldriverhx_smly2.Text = sqlDataReader3["CriminalDriverHX_SMLY2"].ToString();
                                this.criminaldriverhx_ytd_ly.Text = sqlDataReader3["CriminalDriverHX_YTD_LY"].ToString();
                                this.dnrcalls_cm.Text = sqlDataReader3["DNRCalls_CM"].ToString();
                                this.dnrcalls_lm.Text = sqlDataReader3["DNRCalls_LM"].ToString();
                                this.dnrcalls_ytd_cy.Text = sqlDataReader3["DNRCalls_YTD_CY"].ToString();
                                this.dnrcalls_smly.Text = sqlDataReader3["DNRCalls_SMLY"].ToString();
                                this.dnrcalls_ytd_ly.Text = sqlDataReader3["DNRCalls_YTD_LY"].ToString();
                                this.equipissue_cm.Text = sqlDataReader3["EquipIssue_CM"].ToString();
                                this.equipissue_lm.Text = sqlDataReader3["EquipIssue_LM"].ToString();
                                this.equipissue_ytd_cy.Text = sqlDataReader3["EquipIssue_YTD_CY"].ToString();
                                this.equipissue_smly.Text = sqlDataReader3["EquipIssue_SMLY"].ToString();
                                this.equipissue_ytd_ly.Text = sqlDataReader3["EquipIssue_YTD_LY"].ToString();
                                this.flightplans_cm.Text = sqlDataReader3["FlightPlans_CM"].ToString();
                                this.flightplans_lm.Text = sqlDataReader3["FlightPlans_LM"].ToString();
                                this.flightplans_ytd_cy.Text = sqlDataReader3["FlightPlans_YTD_CY"].ToString();
                                this.flightplans_smly.Text = sqlDataReader3["FlightPlans_SMLY"].ToString();
                                this.flightplans_ytd_ly.Text = sqlDataReader3["FlightPlans_YTD_LY"].ToString();
                                this.gcichits_cm.Text = sqlDataReader3["GCICHits_CM"].ToString();
                                this.gcichits_lm.Text = sqlDataReader3["GCICHits_LM"].ToString();
                                this.gcichits_ytd_cy.Text = sqlDataReader3["GCICHits_YTD_CY"].ToString();
                                this.gcichits_smly.Text = sqlDataReader3["GCICHits_SMLY"].ToString();
                                this.gcichits_ytd_ly.Text = sqlDataReader3["GCICHits_YTD_LY"].ToString();
                                this.gcichits_wantedperson.Text = sqlDataReader3["GCICHits_WantedPerson"].ToString();
                                this.motoassist_cm.Text = sqlDataReader3["MototAssist_CM"].ToString();
                                this.motoassist_lm.Text = sqlDataReader3["MototAssist_LM"].ToString();
                                this.motoassist_ytd_cy.Text = sqlDataReader3["MototAssist_YTD_CY"].ToString();
                                this.motoassist_smly.Text = sqlDataReader3["MototAssist_SMLY"].ToString();
                                this.motoassist_ytd_ly.Text = sqlDataReader3["MototAssist_YTD_LY"].ToString();
                                this.motoassist_stolevehicle.Text = sqlDataReader3["MototAssist_StoleVehicle"].ToString();
                                this.openrecord_cm.Text = sqlDataReader3["OpenRecords_CM"].ToString();
                                this.openrecord_lm.Text = sqlDataReader3["OpenRecords_LM"].ToString();
                                this.openrecord_ytd_cy.Text = sqlDataReader3["OpenRecords_YTD_CY"].ToString();
                                this.openrecord_smly.Text = sqlDataReader3["OpenRecords_SMLY"].ToString();
                                this.openrecord_ytd_ly.Text = sqlDataReader3["OpenRecords_YTD_LY"].ToString();
                                this.openrecord_stoleguns.Text = sqlDataReader3["OpenRecords_StoleGuns"].ToString();
                                this.relays_cm.Text = sqlDataReader3["Relays_CM"].ToString();
                                this.relays_lm.Text = sqlDataReader3["Relays_LM"].ToString();
                                this.relays_ytd_cy.Text = sqlDataReader3["Relays_YTD_CY"].ToString();
                                this.relays_smly.Text = sqlDataReader3["Relays_SMLY"].ToString();
                                this.relays_ytd_ly.Text = sqlDataReader3["Relays_YTD_LY"].ToString();
                                this.relays_other.Text = sqlDataReader3["Relays_Other"].ToString();
                                this.roadchecks_cm.Text = sqlDataReader3["RoadChecks_CM"].ToString();
                                this.roadchecks_lm.Text = sqlDataReader3["RoadChecks_LM"].ToString();
                                this.roadchecks_ytd_cy.Text = sqlDataReader3["RoadChecks_YTD_CY"].ToString();
                                this.roadchecks_smly.Text = sqlDataReader3["RoadChecks_SMLY"].ToString();
                                this.roadchecks_ytd_ly.Text = sqlDataReader3["RoadChecks_YTD_LY"].ToString();
                                this.trafficstops_cm.Text = sqlDataReader3["TrafficStops_CM"].ToString();
                                this.trafficstops_lm.Text = sqlDataReader3["TrafficStops_LM"].ToString();
                                this.trafficstops_ytd_cy.Text = sqlDataReader3["TrafficStops_YTD_CY"].ToString();
                                this.trafficstops_smly.Text = sqlDataReader3["TrafficStops_SMLY"].ToString();
                                this.trafficstops_ytd_ly.Text = sqlDataReader3["TrafficStops_YTD_LY"].ToString();
                                this.towedveh_cm.Text = sqlDataReader3["TowedVeh_CM"].ToString();
                                this.towedveh_lm.Text = sqlDataReader3["TowedVeh_LM"].ToString();
                                this.towedveh_ytd_cy.Text = sqlDataReader3["TowedVeh_YTD_CY"].ToString();
                                this.towedveh_smly.Text = sqlDataReader3["TowedVeh_SMLY"].ToString();
                                this.towedveh_ytd_ly.Text = sqlDataReader3["TowedVeh_YTD_LY"].ToString();
                                this.remarks.Text = sqlDataReader3["Remarks"].ToString();
                                this.upperformenabled();
                                this.lowerformEnable();
                            }
                        #endregion
                        }

                        #endregion
                    }
                    else if (str2 == "PSM022") //Troop Commander
                    {
                        #region Troop Commander
                        this.approve.Visible = false;
                        this.save.Visible = false;
                        this.formupdate.Visible = false;
                        this.person2.Enabled = true;
                        this.Button1.Visible = false;
                        this.hdrLabel.InnerText = "COMMUNCICATION INSPECTION REPORT--Troop Commander";
                        this.reportid.Text = Session["RecordID"].ToString();
                        using (SqlConnection sqlConnection = new SqlConnection(this.con))
                        {
                            using (SqlCommand sqlCommand = new SqlCommand("SELECT EmployeeName,sAMAccountName FROM ViewInspection where EmployeePostionNUmber='00105296'"))
                            {
                                sqlCommand.CommandType = CommandType.Text;
                                sqlCommand.Connection = sqlConnection;
                                sqlConnection.Open();
                                this.person2.DataSource = (object)sqlCommand.ExecuteReader();
                                this.person2.DataTextField = "EmployeeName";
                                this.person2.DataValueField = "sAMAccountName";
                                this.person2.DataBind();
                                connection1.Close();
                            }
                        }
                        this.person2.Items.Insert(0, new ListItem("SELECT", "-1"));
                        this.person2.Items.Insert(1, new ListItem("TEAM TRIAL", "pyeboah"));
                        SqlConnection connection3 = new SqlConnection(this.con1);
                        connection3.Open();
                        SqlDataReader sqlDataReader3 = new SqlCommand("SELECT * FROM CommInspectionReport where  ReportID='" + (string)this.Session["RecordID"] + "'", connection3).ExecuteReader();
                        while (sqlDataReader3.Read())
                        {
                            this.pp.Text = sqlDataReader3["PersonnelPresent"].ToString();
                            switch (sqlDataReader3["RecordsAndReports"].ToString())
                            {
                                case "Execellent":
                                    this.record.Checked = true;
                                    this.r1.Checked = false;
                                    this.r2.Checked = false;
                                    this.r3.Checked = false;
                                    break;
                                case "Very Satisfactory":
                                    this.record.Checked = false;
                                    this.r1.Checked = true;
                                    this.r2.Checked = false;
                                    this.r3.Checked = false;
                                    break;
                                case "Satisfactory":
                                    this.record.Checked = false;
                                    this.r1.Checked = false;
                                    this.r2.Checked = true;
                                    this.r3.Checked = false;
                                    break;
                                case "UnSatisfactory":
                                    this.record.Checked = false;
                                    this.r1.Checked = false;
                                    this.r2.Checked = false;
                                    this.r3.Checked = true;
                                    break;
                            }
                            switch (sqlDataReader3["EmpPerformanceMgt"].ToString())
                            {
                                case "Execellent":
                                    this.employee.Checked = true;
                                    this.e1.Checked = false;
                                    this.e2.Checked = false;
                                    this.e3.Checked = false;
                                    break;
                                case "Very Satisfactory":
                                    this.employee.Checked = false;
                                    this.e1.Checked = true;
                                    this.e2.Checked = false;
                                    this.e3.Checked = false;
                                    break;
                                case "Satisfactory":
                                    this.employee.Checked = false;
                                    this.e1.Checked = false;
                                    this.e2.Checked = true;
                                    this.e3.Checked = false;
                                    break;
                                case "UnSatisfactory":
                                    this.employee.Checked = false;
                                    this.e1.Checked = false;
                                    this.e2.Checked = false;
                                    this.e3.Checked = true;
                                    break;
                            }
                            switch (sqlDataReader3["SchAndManAllocation"].ToString())
                            {
                                case "Execellent":
                                    this.manpower.Checked = true;
                                    this.m1.Checked = false;
                                    this.m2.Checked = false;
                                    this.m3.Checked = false;
                                    break;
                                case "Very Satisfactory":
                                    this.manpower.Checked = false;
                                    this.m1.Checked = true;
                                    this.m2.Checked = false;
                                    this.m3.Checked = false;
                                    break;
                                case "Satisfactory":
                                    this.manpower.Checked = false;
                                    this.m1.Checked = false;
                                    this.m2.Checked = true;
                                    this.m3.Checked = false;
                                    break;
                                case "UnSatisfactory":
                                    this.manpower.Checked = false;
                                    this.m1.Checked = false;
                                    this.m2.Checked = false;
                                    this.m3.Checked = true;
                                    break;
                            }
                            switch (sqlDataReader3["SchAndManAllocation"].ToString())
                            {
                                case "Execellent":
                                    this.manpower.Checked = true;
                                    this.m1.Checked = false;
                                    this.m2.Checked = false;
                                    this.m3.Checked = false;
                                    break;
                                case "Very Satisfactory":
                                    this.manpower.Checked = false;
                                    this.m1.Checked = true;
                                    this.m2.Checked = false;
                                    this.m3.Checked = false;
                                    break;
                                case "Satisfactory":
                                    this.manpower.Checked = false;
                                    this.m1.Checked = false;
                                    this.m2.Checked = true;
                                    this.m3.Checked = false;
                                    break;
                                case "UnSatisfactory":
                                    this.manpower.Checked = false;
                                    this.m1.Checked = false;
                                    this.m2.Checked = false;
                                    this.m3.Checked = true;
                                    break;
                            }
                            switch (sqlDataReader3["ComCenterCleanAndMain"].ToString())
                            {
                                case "Execellent":
                                    this.com.Checked = true;
                                    this.c1.Checked = false;
                                    this.c2.Checked = false;
                                    this.c3.Checked = false;
                                    break;
                                case "Very Satisfactory":
                                    this.com.Checked = false;
                                    this.c1.Checked = true;
                                    this.c2.Checked = false;
                                    this.c3.Checked = false;
                                    break;
                                case "Satisfactory":
                                    this.com.Checked = false;
                                    this.c1.Checked = false;
                                    this.c2.Checked = true;
                                    this.c3.Checked = false;
                                    break;
                                case "UnSatisfactory":
                                    this.com.Checked = false;
                                    this.c1.Checked = false;
                                    this.c2.Checked = false;
                                    this.c3.Checked = true;
                                    break;
                            }
                            switch (sqlDataReader3["FurnitureAndGrounds"].ToString())
                            {
                                case "Execellent":
                                    this.fur.Checked = true;
                                    this.f1.Checked = false;
                                    this.f2.Checked = false;
                                    this.f3.Checked = false;
                                    break;
                                case "Very Satisfactory":
                                    this.fur.Checked = false;
                                    this.f1.Checked = true;
                                    this.f2.Checked = false;
                                    this.f3.Checked = false;
                                    break;
                                case "Satisfactory":
                                    this.fur.Checked = false;
                                    this.f1.Checked = false;
                                    this.f2.Checked = true;
                                    this.f3.Checked = false;
                                    break;
                                case "UnSatisfactory":
                                    this.fur.Checked = false;
                                    this.f1.Checked = false;
                                    this.f2.Checked = false;
                                    this.f3.Checked = true;
                                    break;
                            }
                            switch (sqlDataReader3["RadioEquipment"].ToString())
                            {
                                case "Execellent":
                                    this.radio.Checked = true;
                                    this.ra1.Checked = false;
                                    this.ra2.Checked = false;
                                    this.ra3.Checked = false;
                                    break;
                                case "Very Satisfactory":
                                    this.radio.Checked = false;
                                    this.ra1.Checked = true;
                                    this.ra2.Checked = false;
                                    this.ra3.Checked = false;
                                    break;
                                case "Satisfactory":
                                    this.radio.Checked = false;
                                    this.ra1.Checked = false;
                                    this.ra2.Checked = true;
                                    this.ra3.Checked = false;
                                    break;
                                case "UnSatisfactory":
                                    this.radio.Checked = false;
                                    this.ra1.Checked = false;
                                    this.ra2.Checked = false;
                                    this.ra3.Checked = true;
                                    break;
                            }
                            switch (sqlDataReader3["ComputerPrinterETC"].ToString())
                            {
                                case "Execellent":
                                    this.computer.Checked = true;
                                    this.co1.Checked = false;
                                    this.co2.Checked = false;
                                    this.co3.Checked = false;
                                    break;
                                case "Very Satisfactory":
                                    this.computer.Checked = false;
                                    this.co1.Checked = true;
                                    this.co2.Checked = false;
                                    this.co3.Checked = false;
                                    break;
                                case "Satisfactory":
                                    this.computer.Checked = false;
                                    this.co1.Checked = false;
                                    this.co2.Checked = true;
                                    this.co3.Checked = false;
                                    break;
                                case "UnSatisfactory":
                                    this.computer.Checked = false;
                                    this.co1.Checked = false;
                                    this.co2.Checked = false;
                                    this.co3.Checked = true;
                                    break;
                            }
                            switch (sqlDataReader3["UniformAndAppearance"].ToString())
                            {
                                case "Execellent":
                                    this.uni.Checked = true;
                                    this.u1.Checked = false;
                                    this.u2.Checked = false;
                                    this.u3.Checked = false;
                                    break;
                                case "Very Satisfactory":
                                    this.uni.Checked = false;
                                    this.u1.Checked = true;
                                    this.u2.Checked = false;
                                    this.u3.Checked = false;
                                    break;
                                case "Satisfactory":
                                    this.uni.Checked = false;
                                    this.u1.Checked = false;
                                    this.u2.Checked = true;
                                    this.u3.Checked = false;
                                    break;
                                case "UnSatisfactory":
                                    this.uni.Checked = false;
                                    this.u1.Checked = false;
                                    this.u2.Checked = false;
                                    this.u3.Checked = true;
                                    break;
                            }
                            switch (sqlDataReader3["MilitaryCourtesy"].ToString())
                            {
                                case "Execellent":
                                    this.mil.Checked = true;
                                    this.mi1.Checked = false;
                                    this.mi2.Checked = false;
                                    this.mi3.Checked = false;
                                    break;
                                case "Very Satisfactory":
                                    this.mil.Checked = false;
                                    this.mi1.Checked = true;
                                    this.mi2.Checked = false;
                                    this.mi3.Checked = false;
                                    break;
                                case "Satisfactory":
                                    this.mil.Checked = false;
                                    this.mi1.Checked = false;
                                    this.mi2.Checked = true;
                                    this.mi3.Checked = false;
                                    break;
                                case "UnSatisfactory":
                                    this.mil.Checked = false;
                                    this.mi1.Checked = false;
                                    this.mi2.Checked = false;
                                    this.mi3.Checked = true;
                                    break;
                            }
                            switch (sqlDataReader3["DemeanorAndMorale"].ToString())
                            {
                                case "Execellent":
                                    this.dem.Checked = true;
                                    this.d1.Checked = false;
                                    this.d2.Checked = false;
                                    this.d3.Checked = false;
                                    break;
                                case "Very Satisfactory":
                                    this.dem.Checked = false;
                                    this.d1.Checked = true;
                                    this.d2.Checked = false;
                                    this.d3.Checked = false;
                                    break;
                                case "Satisfactory":
                                    this.dem.Checked = false;
                                    this.d1.Checked = false;
                                    this.d2.Checked = true;
                                    this.d3.Checked = false;
                                    break;
                                case "UnSatisfactory":
                                    this.dem.Checked = false;
                                    this.d1.Checked = false;
                                    this.d2.Checked = false;
                                    this.d3.Checked = true;
                                    break;
                            }
                            switch (sqlDataReader3["OnTimeAndPreparedForInspection"].ToString())
                            {
                                case "Execellent":
                                    this.time.Checked = true;
                                    this.t1.Checked = false;
                                    this.t2.Checked = false;
                                    this.t3.Checked = false;
                                    continue;
                                case "Very Satisfactory":
                                    this.time.Checked = false;
                                    this.t1.Checked = true;
                                    this.t2.Checked = false;
                                    this.t3.Checked = false;
                                    continue;
                                case "Satisfactory":
                                    this.time.Checked = false;
                                    this.t1.Checked = false;
                                    this.t2.Checked = true;
                                    this.t3.Checked = false;
                                    continue;
                                case "UnSatisfactory":
                                    this.time.Checked = false;
                                    this.t1.Checked = false;
                                    this.t2.Checked = false;
                                    this.t3.Checked = true;
                                    continue;
                                default:
                                    continue;
                            }
                        }
                        connection3.Close();
                        this.filltable();
                        #endregion
                    }
                    else if (Pstr == "00105296") //Major
                    {
                        #region Major
                        this.formupdate.Visible = false;
                        this.save.Visible = false;
                        this.person2.Visible = false;
                        this.officer.Visible = false;
                        this.Button1.Visible = false;
                        this.troopcom.Visible = false;
                        this.hdrLabel.InnerText = "COMMUNCICATION INSPECTION REPORT--Major";
                        if (this.Session["RecordID"] != null)
                        {
                            this.reportid.Text = Session["RecordID"].ToString();
                            this.lowerformdisable();
                            SqlConnection connection3 = new SqlConnection(this.con1);
                            connection3.Open();
                            SqlDataReader sqlDataReader3 = new SqlCommand("SELECT * FROM CommInspectionReport where  ReportID='" + (string)this.Session["RecordID"] + "'", connection3).ExecuteReader();
                            while (sqlDataReader3.Read())
                            {
                                this.pp.Text = sqlDataReader3["PersonnelPresent"].ToString();
                                switch (sqlDataReader3["RecordsAndReports"].ToString())
                                {
                                    case "Execellent":
                                        this.record.Checked = true;
                                        this.r1.Checked = false;
                                        this.r2.Checked = false;
                                        this.r3.Checked = false;
                                        break;
                                    case "Very Satisfactory":
                                        this.record.Checked = false;
                                        this.r1.Checked = true;
                                        this.r2.Checked = false;
                                        this.r3.Checked = false;
                                        break;
                                    case "Satisfactory":
                                        this.record.Checked = false;
                                        this.r1.Checked = false;
                                        this.r2.Checked = true;
                                        this.r3.Checked = false;
                                        break;
                                    case "UnSatisfactory":
                                        this.record.Checked = false;
                                        this.r1.Checked = false;
                                        this.r2.Checked = false;
                                        this.r3.Checked = true;
                                        break;
                                }
                                switch (sqlDataReader3["EmpPerformanceMgt"].ToString())
                                {
                                    case "Execellent":
                                        this.employee.Checked = true;
                                        this.e1.Checked = false;
                                        this.e2.Checked = false;
                                        this.e3.Checked = false;
                                        break;
                                    case "Very Satisfactory":
                                        this.employee.Checked = false;
                                        this.e1.Checked = true;
                                        this.e2.Checked = false;
                                        this.e3.Checked = false;
                                        break;
                                    case "Satisfactory":
                                        this.employee.Checked = false;
                                        this.e1.Checked = false;
                                        this.e2.Checked = true;
                                        this.e3.Checked = false;
                                        break;
                                    case "UnSatisfactory":
                                        this.employee.Checked = false;
                                        this.e1.Checked = false;
                                        this.e2.Checked = false;
                                        this.e3.Checked = true;
                                        break;
                                }
                                switch (sqlDataReader3["SchAndManAllocation"].ToString())
                                {
                                    case "Execellent":
                                        this.manpower.Checked = true;
                                        this.m1.Checked = false;
                                        this.m2.Checked = false;
                                        this.m3.Checked = false;
                                        break;
                                    case "Very Satisfactory":
                                        this.manpower.Checked = false;
                                        this.m1.Checked = true;
                                        this.m2.Checked = false;
                                        this.m3.Checked = false;
                                        break;
                                    case "Satisfactory":
                                        this.manpower.Checked = false;
                                        this.m1.Checked = false;
                                        this.m2.Checked = true;
                                        this.m3.Checked = false;
                                        break;
                                    case "UnSatisfactory":
                                        this.manpower.Checked = false;
                                        this.m1.Checked = false;
                                        this.m2.Checked = false;
                                        this.m3.Checked = true;
                                        break;
                                }
                                switch (sqlDataReader3["SchAndManAllocation"].ToString())
                                {
                                    case "Execellent":
                                        this.manpower.Checked = true;
                                        this.m1.Checked = false;
                                        this.m2.Checked = false;
                                        this.m3.Checked = false;
                                        break;
                                    case "Very Satisfactory":
                                        this.manpower.Checked = false;
                                        this.m1.Checked = true;
                                        this.m2.Checked = false;
                                        this.m3.Checked = false;
                                        break;
                                    case "Satisfactory":
                                        this.manpower.Checked = false;
                                        this.m1.Checked = false;
                                        this.m2.Checked = true;
                                        this.m3.Checked = false;
                                        break;
                                    case "UnSatisfactory":
                                        this.manpower.Checked = false;
                                        this.m1.Checked = false;
                                        this.m2.Checked = false;
                                        this.m3.Checked = true;
                                        break;
                                }
                                switch (sqlDataReader3["ComCenterCleanAndMain"].ToString())
                                {
                                    case "Execellent":
                                        this.com.Checked = true;
                                        this.c1.Checked = false;
                                        this.c2.Checked = false;
                                        this.c3.Checked = false;
                                        break;
                                    case "Very Satisfactory":
                                        this.com.Checked = false;
                                        this.c1.Checked = true;
                                        this.c2.Checked = false;
                                        this.c3.Checked = false;
                                        break;
                                    case "Satisfactory":
                                        this.com.Checked = false;
                                        this.c1.Checked = false;
                                        this.c2.Checked = true;
                                        this.c3.Checked = false;
                                        break;
                                    case "UnSatisfactory":
                                        this.com.Checked = false;
                                        this.c1.Checked = false;
                                        this.c2.Checked = false;
                                        this.c3.Checked = true;
                                        break;
                                }
                                switch (sqlDataReader3["FurnitureAndGrounds"].ToString())
                                {
                                    case "Execellent":
                                        this.fur.Checked = true;
                                        this.f1.Checked = false;
                                        this.f2.Checked = false;
                                        this.f3.Checked = false;
                                        break;
                                    case "Very Satisfactory":
                                        this.fur.Checked = false;
                                        this.f1.Checked = true;
                                        this.f2.Checked = false;
                                        this.f3.Checked = false;
                                        break;
                                    case "Satisfactory":
                                        this.fur.Checked = false;
                                        this.f1.Checked = false;
                                        this.f2.Checked = true;
                                        this.f3.Checked = false;
                                        break;
                                    case "UnSatisfactory":
                                        this.fur.Checked = false;
                                        this.f1.Checked = false;
                                        this.f2.Checked = false;
                                        this.f3.Checked = true;
                                        break;
                                }
                                switch (sqlDataReader3["RadioEquipment"].ToString())
                                {
                                    case "Execellent":
                                        this.radio.Checked = true;
                                        this.ra1.Checked = false;
                                        this.ra2.Checked = false;
                                        this.ra3.Checked = false;
                                        break;
                                    case "Very Satisfactory":
                                        this.radio.Checked = false;
                                        this.ra1.Checked = true;
                                        this.ra2.Checked = false;
                                        this.ra3.Checked = false;
                                        break;
                                    case "Satisfactory":
                                        this.radio.Checked = false;
                                        this.ra1.Checked = false;
                                        this.ra2.Checked = true;
                                        this.ra3.Checked = false;
                                        break;
                                    case "UnSatisfactory":
                                        this.radio.Checked = false;
                                        this.ra1.Checked = false;
                                        this.ra2.Checked = false;
                                        this.ra3.Checked = true;
                                        break;
                                }
                                switch (sqlDataReader3["ComputerPrinterETC"].ToString())
                                {
                                    case "Execellent":
                                        this.computer.Checked = true;
                                        this.co1.Checked = false;
                                        this.co2.Checked = false;
                                        this.co3.Checked = false;
                                        break;
                                    case "Very Satisfactory":
                                        this.computer.Checked = false;
                                        this.co1.Checked = true;
                                        this.co2.Checked = false;
                                        this.co3.Checked = false;
                                        break;
                                    case "Satisfactory":
                                        this.computer.Checked = false;
                                        this.co1.Checked = false;
                                        this.co2.Checked = true;
                                        this.co3.Checked = false;
                                        break;
                                    case "UnSatisfactory":
                                        this.computer.Checked = false;
                                        this.co1.Checked = false;
                                        this.co2.Checked = false;
                                        this.co3.Checked = true;
                                        break;
                                }
                                switch (sqlDataReader3["UniformAndAppearance"].ToString())
                                {
                                    case "Execellent":
                                        this.uni.Checked = true;
                                        this.u1.Checked = false;
                                        this.u2.Checked = false;
                                        this.u3.Checked = false;
                                        break;
                                    case "Very Satisfactory":
                                        this.uni.Checked = false;
                                        this.u1.Checked = true;
                                        this.u2.Checked = false;
                                        this.u3.Checked = false;
                                        break;
                                    case "Satisfactory":
                                        this.uni.Checked = false;
                                        this.u1.Checked = false;
                                        this.u2.Checked = true;
                                        this.u3.Checked = false;
                                        break;
                                    case "UnSatisfactory":
                                        this.uni.Checked = false;
                                        this.u1.Checked = false;
                                        this.u2.Checked = false;
                                        this.u3.Checked = true;
                                        break;
                                }
                                switch (sqlDataReader3["MilitaryCourtesy"].ToString())
                                {
                                    case "Execellent":
                                        this.mil.Checked = true;
                                        this.mi1.Checked = false;
                                        this.mi2.Checked = false;
                                        this.mi3.Checked = false;
                                        break;
                                    case "Very Satisfactory":
                                        this.mil.Checked = false;
                                        this.mi1.Checked = true;
                                        this.mi2.Checked = false;
                                        this.mi3.Checked = false;
                                        break;
                                    case "Satisfactory":
                                        this.mil.Checked = false;
                                        this.mi1.Checked = false;
                                        this.mi2.Checked = true;
                                        this.mi3.Checked = false;
                                        break;
                                    case "UnSatisfactory":
                                        this.mil.Checked = false;
                                        this.mi1.Checked = false;
                                        this.mi2.Checked = false;
                                        this.mi3.Checked = true;
                                        break;
                                }
                                switch (sqlDataReader3["DemeanorAndMorale"].ToString())
                                {
                                    case "Execellent":
                                        this.dem.Checked = true;
                                        this.d1.Checked = false;
                                        this.d2.Checked = false;
                                        this.d3.Checked = false;
                                        break;
                                    case "Very Satisfactory":
                                        this.dem.Checked = false;
                                        this.d1.Checked = true;
                                        this.d2.Checked = false;
                                        this.d3.Checked = false;
                                        break;
                                    case "Satisfactory":
                                        this.dem.Checked = false;
                                        this.d1.Checked = false;
                                        this.d2.Checked = true;
                                        this.d3.Checked = false;
                                        break;
                                    case "UnSatisfactory":
                                        this.dem.Checked = false;
                                        this.d1.Checked = false;
                                        this.d2.Checked = false;
                                        this.d3.Checked = true;
                                        break;
                                }
                                switch (sqlDataReader3["OnTimeAndPreparedForInspection"].ToString())
                                {
                                    case "Execellent":
                                        this.time.Checked = true;
                                        this.t1.Checked = false;
                                        this.t2.Checked = false;
                                        this.t3.Checked = false;
                                        continue;
                                    case "Very Satisfactory":
                                        this.time.Checked = false;
                                        this.t1.Checked = true;
                                        this.t2.Checked = false;
                                        this.t3.Checked = false;
                                        continue;
                                    case "Satisfactory":
                                        this.time.Checked = false;
                                        this.t1.Checked = false;
                                        this.t2.Checked = true;
                                        this.t3.Checked = false;
                                        continue;
                                    case "UnSatisfactory":
                                        this.time.Checked = false;
                                        this.t1.Checked = false;
                                        this.t2.Checked = false;
                                        this.t3.Checked = true;
                                        continue;
                                    default:
                                        continue;
                                }
                            }
                            connection3.Close();
                            this.filltable();
                        #endregion
                        }
                    }
                    else if (str2 != "GST113" || str2 != "PSM021" || str2 != "PSM022" || str2 != "17817")
                    {
                        Button1.Visible = false;
                        this.reject.Visible = false;
                        this.approve.Visible = false;
                        this.formupdate.Visible = false;
                        this.troopcom.Visible = false;
                        this.save.Visible = false;
                        this.reportid.Text = " NO REPORT GENERATE! INVALID USER";
                        this.reportid.ForeColor = System.Drawing.Color.Red;
                        this.hdrLabel.InnerText = "COMMUNCICATION INSPECTION REPORT--Access Denied";
                        //this.hdrLabel..ForeColor = System.Drawing.Color.Red;
                    }
                }
                connection2.Close();
            }
            #endregion
        }

        private void filltable()
        {
            if (this.Session["RecordID"] == null)
                return;
            string str = (string)this.Session["RecordID"];
            SqlConnection connection = new SqlConnection(this.con1);
            connection.Open();
            SqlDataReader sqlDataReader = new SqlCommand("SELECT * FROM CommInspectionReport WHERE ReportID='" + str + "'", connection).ExecuteReader();
            while (sqlDataReader.Read())
            {
                this.datevalue.Text = sqlDataReader["CreatedDate"].ToString().Substring(0, 10);
                this.totalcalls_cm.Text = sqlDataReader["TotalCalls_CM"].ToString();
                this.totalcalls_cm2.Text = sqlDataReader["TotalCalls_CM2"].ToString();
                this.totalcalls_lm.Text = sqlDataReader["TotalCalls_LM"].ToString();
                this.totalcalls_ytd_cy.Text = sqlDataReader["TotalCalls_YTD_CY"].ToString();
                this.totalcalls_smly.Text = sqlDataReader["TotalCalls_SMLY"].ToString();
                this.totalcalls_smly2.Text = sqlDataReader["TotalCalls_SMLY2"].ToString();
                this.totalcalls_ytd_ly.Text = sqlDataReader["TotalCalls_YTD_LY"].ToString();
                this.abandonedveh_cm.Text = sqlDataReader["AbandonedVeh_CM"].ToString();
                this.abandonedveh_cm2.Text = sqlDataReader["AbandonedVeh_CM2"].ToString();
                this.abandonedveh_lm.Text = sqlDataReader["AbandonedVeh_LM"].ToString();
                this.abandonedveh_ytd_cy.Text = sqlDataReader["AbandonedVeh_YTD_CY"].ToString();
                this.abandonedveh_smly.Text = sqlDataReader["AbandonedVeh_SMLY"].ToString();
                this.abandonedveh_smly2.Text = sqlDataReader["AbandonedVeh_SMLY2"].ToString();
                this.abandonedveh_ytd_ly.Text = sqlDataReader["AbandonedVeh_YTD_LY"].ToString();
                this.chases_cm.Text = sqlDataReader["Chases_CM"].ToString();
                this.chases_cm2.Text = sqlDataReader["Chases_CM2"].ToString();
                this.chases_lm.Text = sqlDataReader["Chases_LM"].ToString();
                this.chases_ytd_cy.Text = sqlDataReader["Chases_YTD_CY"].ToString();
                this.chases_smly.Text = sqlDataReader["Chases_SMLY"].ToString();
                this.chases_smly2.Text = sqlDataReader["Chases_SMLY2"].ToString();
                this.chases_ytd_ly.Text = sqlDataReader["Chases_YTD_LY"].ToString();
                this.controlledburns_cm.Text = sqlDataReader["ControlledBurns_CM"].ToString();
                this.controlledburns_cm2.Text = sqlDataReader["ControlledBurns_CM2"].ToString();
                this.controlledburns_lm.Text = sqlDataReader["ControlledBurns_LM"].ToString();
                this.controlledburns_ytd_cy.Text = sqlDataReader["ControlledBurns_YTD_CY"].ToString();
                this.controlledburns_smly.Text = sqlDataReader["ControlledBurns_SMLY"].ToString();
                this.controlledburns_smly2.Text = sqlDataReader["ControlledBurns_SMLY2"].ToString();
                this.controlledburns_ytd_ly.Text = sqlDataReader["ControlledBurns_YTD_LY"].ToString();
                this.criminaldriverhx_cm.Text = sqlDataReader["CriminalDriverHX_CM"].ToString();
                this.criminaldriverhx_lm.Text = sqlDataReader["CriminalDriverHX_LM"].ToString();
                this.criminaldriverhx_ytd_cy.Text = sqlDataReader["CriminalDriverHX_YTD_CY"].ToString();
                this.criminaldriverhx_smly.Text = sqlDataReader["CriminalDriverHX_SMLY"].ToString();
                this.criminaldriverhx_smly2.Text = sqlDataReader["CriminalDriverHX_SMLY2"].ToString();
                this.criminaldriverhx_ytd_ly.Text = sqlDataReader["CriminalDriverHX_YTD_LY"].ToString();
                this.dnrcalls_cm.Text = sqlDataReader["DNRCalls_CM"].ToString();
                this.dnrcalls_lm.Text = sqlDataReader["DNRCalls_LM"].ToString();
                this.dnrcalls_ytd_cy.Text = sqlDataReader["DNRCalls_YTD_CY"].ToString();
                this.dnrcalls_smly.Text = sqlDataReader["DNRCalls_SMLY"].ToString();
                this.dnrcalls_ytd_ly.Text = sqlDataReader["DNRCalls_YTD_LY"].ToString();
                this.equipissue_cm.Text = sqlDataReader["EquipIssue_CM"].ToString();
                this.equipissue_lm.Text = sqlDataReader["EquipIssue_LM"].ToString();
                this.equipissue_ytd_cy.Text = sqlDataReader["EquipIssue_YTD_CY"].ToString();
                this.equipissue_smly.Text = sqlDataReader["EquipIssue_SMLY"].ToString();
                this.equipissue_ytd_ly.Text = sqlDataReader["EquipIssue_YTD_LY"].ToString();
                this.flightplans_cm.Text = sqlDataReader["FlightPlans_CM"].ToString();
                this.flightplans_lm.Text = sqlDataReader["FlightPlans_LM"].ToString();
                this.flightplans_ytd_cy.Text = sqlDataReader["FlightPlans_YTD_CY"].ToString();
                this.flightplans_smly.Text = sqlDataReader["FlightPlans_SMLY"].ToString();
                this.flightplans_ytd_ly.Text = sqlDataReader["FlightPlans_YTD_LY"].ToString();
                this.gcichits_cm.Text = sqlDataReader["GCICHits_CM"].ToString();
                this.gcichits_lm.Text = sqlDataReader["GCICHits_LM"].ToString();
                this.gcichits_ytd_cy.Text = sqlDataReader["GCICHits_YTD_CY"].ToString();
                this.gcichits_smly.Text = sqlDataReader["GCICHits_SMLY"].ToString();
                this.gcichits_ytd_ly.Text = sqlDataReader["GCICHits_YTD_LY"].ToString();
                this.gcichits_wantedperson.Text = sqlDataReader["GCICHits_WantedPerson"].ToString();
                this.motoassist_cm.Text = sqlDataReader["MototAssist_CM"].ToString();
                this.motoassist_lm.Text = sqlDataReader["MototAssist_LM"].ToString();
                this.motoassist_ytd_cy.Text = sqlDataReader["MototAssist_YTD_CY"].ToString();
                this.motoassist_smly.Text = sqlDataReader["MototAssist_SMLY"].ToString();
                this.motoassist_ytd_ly.Text = sqlDataReader["MototAssist_YTD_LY"].ToString();
                this.motoassist_stolevehicle.Text = sqlDataReader["MototAssist_StoleVehicle"].ToString();
                this.openrecord_cm.Text = sqlDataReader["OpenRecords_CM"].ToString();
                this.openrecord_lm.Text = sqlDataReader["OpenRecords_LM"].ToString();
                this.openrecord_ytd_cy.Text = sqlDataReader["OpenRecords_YTD_CY"].ToString();
                this.openrecord_smly.Text = sqlDataReader["OpenRecords_SMLY"].ToString();
                this.openrecord_ytd_ly.Text = sqlDataReader["OpenRecords_YTD_LY"].ToString();
                this.openrecord_stoleguns.Text = sqlDataReader["OpenRecords_StoleGuns"].ToString();
                this.relays_cm.Text = sqlDataReader["Relays_CM"].ToString();
                this.relays_lm.Text = sqlDataReader["Relays_LM"].ToString();
                this.relays_ytd_cy.Text = sqlDataReader["Relays_YTD_CY"].ToString();
                this.relays_smly.Text = sqlDataReader["Relays_SMLY"].ToString();
                this.relays_ytd_ly.Text = sqlDataReader["Relays_YTD_LY"].ToString();
                this.relays_other.Text = sqlDataReader["Relays_Other"].ToString();
                this.roadchecks_cm.Text = sqlDataReader["RoadChecks_CM"].ToString();
                this.roadchecks_lm.Text = sqlDataReader["RoadChecks_LM"].ToString();
                this.roadchecks_ytd_cy.Text = sqlDataReader["RoadChecks_YTD_CY"].ToString();
                this.roadchecks_smly.Text = sqlDataReader["RoadChecks_SMLY"].ToString();
                this.roadchecks_ytd_ly.Text = sqlDataReader["RoadChecks_YTD_LY"].ToString();
                this.trafficstops_cm.Text = sqlDataReader["TrafficStops_CM"].ToString();
                this.trafficstops_lm.Text = sqlDataReader["TrafficStops_LM"].ToString();
                this.trafficstops_ytd_cy.Text = sqlDataReader["TrafficStops_YTD_CY"].ToString();
                this.trafficstops_smly.Text = sqlDataReader["TrafficStops_SMLY"].ToString();
                this.trafficstops_ytd_ly.Text = sqlDataReader["TrafficStops_YTD_LY"].ToString();
                this.towedveh_cm.Text = sqlDataReader["TowedVeh_CM"].ToString();
                this.towedveh_lm.Text = sqlDataReader["TowedVeh_LM"].ToString();
                this.towedveh_ytd_cy.Text = sqlDataReader["TowedVeh_YTD_CY"].ToString();
                this.towedveh_smly.Text = sqlDataReader["TowedVeh_SMLY"].ToString();
                this.towedveh_ytd_ly.Text = sqlDataReader["TowedVeh_YTD_LY"].ToString();
                this.remarks.Text = sqlDataReader["Remarks"].ToString();
                this.upperformdisable();
                this.lowerformdisable();
                this.person2.Enabled = true;
            }
        }


        protected void formsave_Click(object sender, EventArgs e)
        {
            //if (admin.SelectedValue != "-1")
            //{
            //    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "admin_ch();", true);
            //    adminRequired.Text = "";
            //    SetFocus(t3);
            //}
            //else if (admin.SelectedValue == "-1")
            //{
            //    adminRequired.Text = "*required";
            //    SetFocus(t3);
            //}
            if (!record.Checked && !r1.Checked && !r2.Checked && !r3.Checked)
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "record_ch();", true);
                recordRequired.Text = "*required";
                SetFocus(t3);
            }
            else if (record.Checked || r1.Checked || r2.Checked || r3.Checked)
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "record_ch();", true);
                recordRequired.Text = "";
                SetFocus(t3);
            }
            if (!employee.Checked && !e1.Checked && !e2.Checked && !e3.Checked)
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "employee_ch();", true);
                employeeRequired.Text = "*required";
                SetFocus(t3);

            }
            if (employee.Checked || e1.Checked || e2.Checked || e3.Checked)
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "employee_ch();", true);
                employeeRequired.Text = "";
                SetFocus(t3);

            }
            if (!manpower.Checked && !m1.Checked && !m2.Checked && !m3.Checked)
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "manpower_ch();", true);
                manpowerRequired.Text = "*required";
                SetFocus(t3);

            }
            else if (manpower.Checked || m1.Checked || m2.Checked || m3.Checked)
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "manpower_ch();", true);
                manpowerRequired.Text = "";
                SetFocus(t3);

            }
            //if (build.SelectedValue != "-1")
            //{
            //    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "build_ch();", true);
            //    buildRequired.Text = "";
            //    SetFocus(t3);

            //}
            //else if (build.SelectedValue == "-1")
            //{
            //    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "build_ch();", true);
            //    buildRequired.Text = "*required";
            //    SetFocus(t3);

            //}
            if (!com.Checked && !c1.Checked && !c2.Checked && !c3.Checked)
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "com_ch();", true);
                comRequired.Text = "*required";
                SetFocus(t3);

            }
            else if (com.Checked || c1.Checked || c2.Checked || !c3.Checked)
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "com_ch();", true);
                comRequired.Text = "";
                SetFocus(t3);

            }
            if (!fur.Checked && !f1.Checked && !f2.Checked && !f3.Checked)
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "fur_ch();", true);
                furRequired.Text = "*required";
                SetFocus(t3);

            }
            else if (fur.Checked || f1.Checked || f2.Checked || f3.Checked)
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "fur_ch();", true);
                furRequired.Text = "";
                SetFocus(t3);

            }
            //if (equip.SelectedValue != "-1")
            //{
            //    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "equip_ch();", true);
            //    equipRequired.Text = "";
            //    SetFocus(t3);

            //}
            //else if (equip.SelectedValue == "-1")
            //{
            //    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "equip_ch();", true);
            //    equipRequired.Text = "*required";
            //    SetFocus(t3);

            //}
            if (!radio.Checked && !ra1.Checked && !ra2.Checked && !ra3.Checked)
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "radio_ch();", true);
                radioRquired.Text = "*required";
                SetFocus(t3);

            }
            else if (radio.Checked || ra1.Checked || ra2.Checked || ra3.Checked)
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "radio_ch();", true);
                radioRquired.Text = "";
                SetFocus(t3);

            }
            if (!computer.Checked && !co1.Checked && !co2.Checked && !co3.Checked)
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "computer_ch();", true);
                computerRequired.Text = "*required";
                SetFocus(t3);
            }
            else if (computer.Checked || co1.Checked || co2.Checked || co3.Checked)
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "computer_ch();", true);
                computerRequired.Text = "";
                SetFocus(t3);
            }
            //if (person.SelectedValue != "-1")
            //{
            //    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "person_ch();", true);
            //    personReequired.Text = "";
            //    SetFocus(t3);

            //}
            //else if (person.SelectedValue == "-1")
            //{
            //    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "person_ch();", true);
            //    personReequired.Text = "*required";
            //    SetFocus(t3);

            //}
            if (pp.Text != "")
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "pp_ch();", true);
                ppRequired.Text = "";
                SetFocus(t3);

            }
            else if (pp.Text == "")
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "pp_ch();", true);
                ppRequired.Text = "*required";
                SetFocus(t3);

            }
            if (!uni.Checked && !u1.Checked && !u2.Checked && !u3.Checked)
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "uni_ch();", true);
                uniRequired.Text = "*required";
                SetFocus(t3);

            }
            else if (uni.Checked || u1.Checked || u2.Checked || u3.Checked)
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "uni_ch();", true);
                uniRequired.Text = "";
                SetFocus(t3);

            }
            if (!mil.Checked && !mi1.Checked && !mi2.Checked && !mi3.Checked)
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "mil_ch();", true);
                milRequired.Text = "*required";
                SetFocus(t3);

            }
            else if (mil.Checked || mi1.Checked || !mi2.Checked || !mi3.Checked)
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "mil_ch();", true);
                milRequired.Text = "";
                SetFocus(t3);

            }
            if (!dem.Checked && !d1.Checked && !d2.Checked && !d3.Checked)
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "dem_ch();", true);
                demRequired.Text = "*required";
                SetFocus(t3);

            }
            else if (dem.Checked || d1.Checked || d2.Checked || d3.Checked)
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "dem_ch();", true);
                demRequired.Text = "";
                SetFocus(m3);

            }
            if (!time.Checked && !t1.Checked && !t2.Checked && !t3.Checked)
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "time_ch();", true);
                timeRequired.Text = "*required";
                SetFocus(m3);

            }
            else if (time.Checked || t1.Checked || t2.Checked || t3.Checked)
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "time_ch();", true);
                timeRequired.Text = "";
                //formsave.Enabled = true;
                //SetFocus(formsave);
            }
        }

        private void upperformdisable()
        {
            datevalue.Enabled = false;
           // admin.Enabled = false;
            record.Enabled = false; r1.Enabled = false; r2.Enabled = false; r3.Enabled = false;
            employee.Enabled = false; e1.Enabled = false; e2.Enabled = false; e3.Enabled = false;
            manpower.Enabled = false; m1.Enabled = false; m2.Enabled = false; m3.Enabled = false;
           // build.Enabled = false;
            com.Enabled = false; c1.Enabled = false; c2.Enabled = false; c3.Enabled = false;
            fur.Enabled = false; f1.Enabled = false; f2.Enabled = false; f3.Enabled = false;
            //equip.Enabled = false;
            radio.Enabled = false; ra1.Enabled = false; ra2.Enabled = false; ra3.Enabled = false;
            computer.Enabled = false; co1.Enabled = false; co2.Enabled = false; co3.Enabled = false;
           // person.Enabled = false; pp.Enabled = false;
            uni.Enabled = false; u1.Enabled = false; u2.Enabled = false; u3.Enabled = false;
            mil.Enabled = false; mi1.Enabled = false; mi2.Enabled = false; mi3.Enabled = false;
            dem.Enabled = false; d1.Enabled = false; d2.Enabled = false; d3.Enabled = false;
            time.Enabled = false; t1.Enabled = false; t2.Enabled = false; t3.Enabled = false;
          //  formsave.Enabled = false;
        }

        private void upperformenabled()
        {
            this.datevalue.Enabled = false;
            this.record.Enabled = true;
            this.r1.Enabled = true;
            this.r2.Enabled = true;
            this.r3.Enabled = true;
            this.employee.Enabled = true;
            this.e1.Enabled = true;
            this.e2.Enabled = true;
            this.e3.Enabled = true;
            this.manpower.Enabled = true;
            this.m1.Enabled = true;
            this.m2.Enabled = true;
            this.m3.Enabled = true;
            this.com.Enabled = true;
            this.c1.Enabled = true;
            this.c2.Enabled = true;
            this.c3.Enabled = true;
            this.fur.Enabled = true;
            this.f1.Enabled = true;
            this.f2.Enabled = true;
            this.f3.Enabled = true;
            this.radio.Enabled = true;
            this.ra1.Enabled = true;
            this.ra2.Enabled = true;
            this.ra3.Enabled = true;
            this.computer.Enabled = true;
            this.co1.Enabled = true;
            this.co2.Enabled = true;
            this.co3.Enabled = true;
            this.pp.Enabled = true;
            this.uni.Enabled = true;
            this.u1.Enabled = true;
            this.u2.Enabled = true;
            this.u3.Enabled = true;
            this.mil.Enabled = true;
            this.mi1.Enabled = true;
            this.mi2.Enabled = true;
            this.mi3.Enabled = true;
            this.dem.Enabled = true;
            this.d1.Enabled = true;
            this.d2.Enabled = true;
            this.d3.Enabled = true;
            this.time.Enabled = true;
            this.t1.Enabled = true;
            this.t2.Enabled = true;
            this.t3.Enabled = true;
        }

        private void lowerformdisable()
        {
            totalcalls_cm.Enabled = false; totalcalls_cm2.Enabled = false; totalcalls_lm.Enabled = false; totalcalls_ytd_cy.Enabled = false; totalcalls_smly.Enabled = false; totalcalls_smly2.Enabled = false; totalcalls_ytd_ly.Enabled = false;
            abandonedveh_cm.Enabled = false; abandonedveh_cm2.Enabled = false; abandonedveh_lm.Enabled = false; abandonedveh_ytd_cy.Enabled = false; abandonedveh_smly.Enabled = false; abandonedveh_smly2.Enabled = false; abandonedveh_ytd_ly.Enabled = false;
            chases_cm.Enabled = false; chases_cm2.Enabled = false; chases_lm.Enabled = false; chases_ytd_cy.Enabled = false; chases_smly.Enabled = false; chases_smly2.Enabled = false; chases_ytd_ly.Enabled = false;
            controlledburns_cm.Enabled = false; controlledburns_cm2.Enabled = false; controlledburns_lm.Enabled = false; controlledburns_ytd_cy.Enabled = false; controlledburns_smly.Enabled = false; controlledburns_smly2.Enabled = false; controlledburns_ytd_ly.Enabled = false;
            criminaldriverhx_cm.Enabled = false; criminaldriverhx_lm.Enabled = false; criminaldriverhx_ytd_cy.Enabled = false; criminaldriverhx_smly.Enabled = false; criminaldriverhx_smly2.Enabled = false; criminaldriverhx_ytd_ly.Enabled = false;
            dnrcalls_cm.Enabled = false; dnrcalls_lm.Enabled = false; dnrcalls_ytd_cy.Enabled = false; dnrcalls_smly.Enabled = false; dnrcalls_ytd_ly.Enabled = false;
            equipissue_cm.Enabled = false; equipissue_lm.Enabled = false; equipissue_ytd_cy.Enabled = false; equipissue_smly.Enabled = false; equipissue_ytd_ly.Enabled = false;
            flightplans_cm.Enabled = false; flightplans_lm.Enabled = false; flightplans_ytd_cy.Enabled = false; flightplans_smly.Enabled = false; flightplans_ytd_ly.Enabled = false;
            gcichits_cm.Enabled = false; gcichits_lm.Enabled = false; gcichits_ytd_cy.Enabled = false; gcichits_smly.Enabled = false; gcichits_ytd_ly.Enabled = false; gcichits_wantedperson.Enabled = false;
            motoassist_cm.Enabled = false; motoassist_lm.Enabled = false; motoassist_ytd_cy.Enabled = false; motoassist_smly.Enabled = false; motoassist_ytd_ly.Enabled = false; motoassist_stolevehicle.Enabled = false;
            openrecord_cm.Enabled = false; openrecord_lm.Enabled = false; openrecord_ytd_cy.Enabled = false; openrecord_smly.Enabled = false; openrecord_ytd_ly.Enabled = false; openrecord_stoleguns.Enabled = false;
            relays_cm.Enabled = false; relays_lm.Enabled = false; relays_ytd_cy.Enabled = false; relays_smly.Enabled = false; relays_ytd_ly.Enabled = false; relays_other.Enabled = false;
            roadchecks_cm.Enabled = false; roadchecks_lm.Enabled = false; roadchecks_ytd_cy.Enabled = false; roadchecks_smly.Enabled = false; roadchecks_ytd_ly.Enabled = false;
            trafficstops_cm.Enabled = false; trafficstops_lm.Enabled = false; trafficstops_ytd_cy.Enabled = false; trafficstops_smly.Enabled = false; trafficstops_ytd_ly.Enabled = false;
            towedveh_cm.Enabled = false; towedveh_lm.Enabled = false; towedveh_ytd_cy.Enabled = false; towedveh_smly.Enabled = false; towedveh_ytd_ly.Enabled = false; remarks.Enabled = false; person2.Enabled = false; formupdate.Enabled = false;
        }

        private void lowerformEnable()
        {
            totalcalls_cm.Enabled = true; totalcalls_cm2.Enabled = true; totalcalls_lm.Enabled = true; totalcalls_ytd_cy.Enabled = true; totalcalls_smly.Enabled = true; totalcalls_smly2.Enabled = true; totalcalls_ytd_ly.Enabled = true;
            abandonedveh_cm.Enabled = true; abandonedveh_cm2.Enabled = true; abandonedveh_lm.Enabled = true; abandonedveh_ytd_cy.Enabled = true; abandonedveh_smly.Enabled = true; abandonedveh_smly2.Enabled = true; abandonedveh_ytd_ly.Enabled = true;
            chases_cm.Enabled = true; chases_cm2.Enabled = true; chases_lm.Enabled = true; chases_ytd_cy.Enabled = true; chases_smly.Enabled = true; chases_smly2.Enabled = true; chases_ytd_ly.Enabled = true;
            controlledburns_cm.Enabled = true; controlledburns_cm2.Enabled = true; controlledburns_lm.Enabled = true; controlledburns_ytd_cy.Enabled = true; controlledburns_smly.Enabled = true; controlledburns_smly2.Enabled = true; controlledburns_ytd_ly.Enabled = true;
            criminaldriverhx_cm.Enabled = true; criminaldriverhx_lm.Enabled = true; criminaldriverhx_ytd_cy.Enabled = true; criminaldriverhx_smly.Enabled = true; criminaldriverhx_smly2.Enabled = true; criminaldriverhx_ytd_ly.Enabled = true;
            dnrcalls_cm.Enabled = true; dnrcalls_lm.Enabled = true; dnrcalls_ytd_cy.Enabled = true; dnrcalls_smly.Enabled = true; dnrcalls_ytd_ly.Enabled = true;
            equipissue_cm.Enabled = true; equipissue_lm.Enabled = true; equipissue_ytd_cy.Enabled = true; equipissue_smly.Enabled = true; equipissue_ytd_ly.Enabled = true;
            flightplans_cm.Enabled = true; flightplans_lm.Enabled = true; flightplans_ytd_cy.Enabled = true; flightplans_smly.Enabled = true; flightplans_ytd_ly.Enabled = true;
            gcichits_cm.Enabled = true; gcichits_lm.Enabled = true; gcichits_ytd_cy.Enabled = true; gcichits_smly.Enabled = true; gcichits_ytd_ly.Enabled = true; gcichits_wantedperson.Enabled = true;
            motoassist_cm.Enabled = true; motoassist_lm.Enabled = true; motoassist_ytd_cy.Enabled = true; motoassist_smly.Enabled = true; motoassist_ytd_ly.Enabled = true; motoassist_stolevehicle.Enabled = true;
            openrecord_cm.Enabled = true; openrecord_lm.Enabled = true; openrecord_ytd_cy.Enabled = true; openrecord_smly.Enabled = true; openrecord_ytd_ly.Enabled = true; openrecord_stoleguns.Enabled = true;
            relays_cm.Enabled = true; relays_lm.Enabled = true; relays_ytd_cy.Enabled = true; relays_smly.Enabled = true; relays_ytd_ly.Enabled = true; relays_other.Enabled = true;
            roadchecks_cm.Enabled = true; roadchecks_lm.Enabled = true; roadchecks_ytd_cy.Enabled = true; roadchecks_smly.Enabled = true; roadchecks_ytd_ly.Enabled = true;
            trafficstops_cm.Enabled = true; trafficstops_lm.Enabled = true; trafficstops_ytd_cy.Enabled = true; trafficstops_smly.Enabled = true; trafficstops_ytd_ly.Enabled = true;
            towedveh_cm.Enabled = true; towedveh_lm.Enabled = true; towedveh_ytd_cy.Enabled = true; towedveh_smly.Enabled = true; towedveh_ytd_ly.Enabled = true; remarks.Enabled = true; person2.Enabled = true; formupdate.Enabled = true;
        }

        protected void formupdate_Click(object sender, EventArgs e)
        {
            if (!this.record.Checked && !this.r1.Checked && (!this.r2.Checked && !this.r3.Checked) || !this.employee.Checked && !this.e1.Checked && (!this.e2.Checked && !this.e3.Checked) || (!this.manpower.Checked && !this.m1.Checked && (!this.m2.Checked && !this.m3.Checked) || !this.com.Checked && !this.c1.Checked && (!this.c2.Checked && !this.c3.Checked)) || (!this.fur.Checked && !this.f1.Checked && (!this.f2.Checked && !this.f3.Checked) || !this.radio.Checked && !this.ra1.Checked && (!this.ra2.Checked && !this.ra3.Checked) || (!this.computer.Checked && !this.co1.Checked && (!this.co2.Checked && !this.co3.Checked) || this.pp.Text == "")) || (!this.uni.Checked && !this.u1.Checked && (!this.u2.Checked && !this.u3.Checked) || !this.mil.Checked && !this.mi1.Checked && (!this.mi2.Checked && !this.mi3.Checked) || (!this.dem.Checked && !this.d1.Checked && (!this.d2.Checked && !this.d3.Checked) || !this.time.Checked && !this.t1.Checked && (!this.t2.Checked && !this.t3.Checked) || remarks.Text == "" || person2.SelectedValue == "-1")))
            {
                this.formValidate();
            }
            else
            {
                string user = User.Identity.Name;
                string mainuser = user.Substring(6);
                string text1 = this.datevalue.Text;
                string text2 = this.pp.Text;
                string text3 = this.trooper.Text;
                string text4 = this.post.Text;
                string text5 = this.citymain.Text;
                string editremark = remarks.Text;
                string mainremark = editremark.Replace("'", "''");
                DateTime.Now.ToString("MM");
                DateTime.Now.ToString("yyyy");
                DateTime now = DateTime.Now;
                if (this.record.Checked)
                    this.Rs = "Execellent";
                else if (this.r1.Checked)
                    this.Rs = "Very Satisfactory";
                else if (this.r2.Checked)
                    this.Rs = "Satisfactory";
                else if (this.r3.Checked)
                    this.Rs = "UnSatisfactory";
                if (this.employee.Checked)
                    this.Es = "Execellent";
                else if (this.e1.Checked)
                    this.Es = "Very Satisfactory";
                else if (this.e2.Checked)
                    this.Es = "Satisfactory";
                else if (this.e3.Checked)
                    this.Es = "UnSatisfactory";
                if (this.manpower.Checked)
                    this.Ms = "Execellent";
                else if (this.m1.Checked)
                    this.Ms = "Very Satisfactory";
                else if (this.m2.Checked)
                    this.Ms = "Satisfactory";
                else if (this.m3.Checked)
                    this.Ms = "UnSatisfactory";
                if (this.com.Checked)
                    this.Cs = "Execellent";
                else if (this.c1.Checked)
                    this.Cs = "Very Satisfactory";
                else if (this.c2.Checked)
                    this.Cs = "Satisfactory";
                else if (this.c3.Checked)
                    this.Cs = "UnSatisfactory";
                if (this.fur.Checked)
                    this.Fs = "Execellent";
                else if (this.f1.Checked)
                    this.Fs = "Very Satisfactory";
                else if (this.f2.Checked)
                    this.Fs = "Satisfactory";
                else if (this.f3.Checked)
                    this.Fs = "UnSatisfactory";
                if (this.radio.Checked)
                    this.Ra = "Execellent";
                else if (this.ra1.Checked)
                    this.Ra = "Very Satisfactory";
                else if (this.ra2.Checked)
                    this.Ra = "Satisfactory";
                else if (this.ra3.Checked)
                    this.Ra = "UnSatisfactory";
                if (this.computer.Checked)
                    this.Co = "Execellent";
                else if (this.co1.Checked)
                    this.Co = "Very Satisfactory";
                else if (this.co2.Checked)
                    this.Co = "Satisfactory";
                else if (this.co3.Checked)
                    this.Co = "UnSatisfactory";
                if (this.uni.Checked)
                    this.Un = "Execellent";
                else if (this.u1.Checked)
                    this.Un = "Very Satisfactory";
                else if (this.u2.Checked)
                    this.Un = "Satisfactory";
                else if (this.u3.Checked)
                    this.Un = "UnSatisfactory";
                if (this.mil.Checked)
                    this.Mi = "Execellent";
                else if (this.mi1.Checked)
                    this.Mi = "Very Satisfactory";
                else if (this.mi2.Checked)
                    this.Mi = "Satisfactory";
                else if (this.m3.Checked)
                    this.Mi = "UnSatisfactory";
                if (this.dem.Checked)
                    this.De = "Execellent";
                else if (this.d1.Checked)
                    this.De = "Very Satisfactory";
                else if (this.d2.Checked)
                    this.De = "Satisfactory";
                else if (this.d3.Checked)
                    this.De = "UnSatisfactory";
                if (this.time.Checked)
                    this.Ti = "Execellent";
                else if (this.t1.Checked)
                    this.Ti = "Very Satisfactory";
                else if (this.t2.Checked)
                    this.Ti = "Satisfactory";
                else if (this.t3.Checked)
                    this.Ti = "UnSatisfactory";
                this.User.Identity.Name.Substring(6);
                SqlConnection connection1 = new SqlConnection(this.con);
                SqlCommand sqlCommand = new SqlCommand("select EmployeeID from ViewInspection where sAMAccountName='" + mainuser + "'", connection1);
                connection1.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                    this.employeeid = sqlDataReader["EmployeeID"].ToString();
                connection1.Close();
                SqlConnection connection2 = new SqlConnection(this.con2);
                connection2.Open();
                new SqlCommand("UPDATE CommInspectionReport SET Troop='" + text3 + "', Post='" + text4 + "', City='" + text5 + "',InspectionMonth='" + DateTime.Now.ToString("MM") + "',InspectionYear='" + DateTime.Now.ToString("yyyy") + "',RecordsAndReports='" + this.Rs + "',EmpPerformanceMgt='" + this.Es + "',SchAndManAllocation='" + this.Ms + "',ComCenterCleanAndMain='" + this.Cs + "',FurnitureAndGrounds='" + this.Fs + "',RadioEquipment='" + this.Ra + "',ComputerPrinterETC='" + this.Co + "',PersonnelPresent='" + this.pp.Text + "',UniformAndAppearance='" + this.Un + "',MilitaryCourtesy='" + this.Mi + "',DemeanorAndMorale='" + this.De + "',OnTimeAndPreparedForInspection='" + this.Ti + "',Remarks='" + mainremark + "',ModifiedBy='" + this.employeeid + "',ModifiedDate='" + DateTime.Now.ToString("MM-dd-yyyy h:mm:ss tt") + "',InspectionDate='" + this.datevalue.Text + "', ApprovalStatus='" + "Pending" + "', ReportStage='" + "Awaiting T.C''s Approval" + "' where ReportID = '" + (string)this.Session["RecordID"] + "'", connection2).ExecuteNonQuery();
                connection2.Close();
                this.sendMessage(this.person2.SelectedValue + "@gsp.net");
                this.Response.Redirect("http://10.0.0.109/inspectionreport/FirstApproval.aspx");
            }
        }

        private void sendMessage(string strEmailTo)
        {
            string str = "10.0.0.14";
            string from = "donotreply@gsp.net";
            string messageText = "Inspection Report Test E-Mail\nPlease click the link below:\n\nhttp://10.0.0.109/inspectionreport/FirstApproval.aspx";
            string subject = "Inspection_Report_Approval_Email_Test";
            SmtpMail.SmtpServer = str;
            SmtpMail.Send(from, strEmailTo, subject, messageText);
        }

        protected void remarks_TextChanged(object sender, EventArgs e)
        {
            if (remarks.Text != "")
            {
                remarksRequired.Text = " ";
                lowerformEnable();
                SetFocus(formupdate);
            }
            else
            {
                remarksRequired.Text = "*required";
                lowerformEnable();
                SetFocus(formupdate);
            }
        }

        protected void person2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (person2.SelectedValue != "-1")
            {
                person2Required.Text = " ";
                lowerformEnable();
                SetFocus(formupdate);
            }
            else
            {
                person2Required.Text = "*required";
                lowerformEnable();
                SetFocus(formupdate);
            }
        }

        protected void formsave_Click1(object sender, EventArgs e)
        {
            if (/*this.admin.SelectedValue == "-1"*/ !this.record.Checked && !this.r1.Checked && (!this.r2.Checked && !this.r3.Checked) || (!this.employee.Checked && !this.e1.Checked && (!this.e2.Checked && !this.e3.Checked) || !this.manpower.Checked && !this.m1.Checked && (!this.m2.Checked && !this.m3.Checked)) || (/*this.build.SelectedValue == "-1" ||*/ !this.com.Checked && !this.c1.Checked && (!this.c2.Checked && !this.c3.Checked) || (!this.fur.Checked && !this.f1.Checked && (!this.f2.Checked && !this.f3.Checked) || /*this.equip.SelectedValue == "-1")) ||*/ (!this.radio.Checked && !this.ra1.Checked && (!this.ra2.Checked && !this.ra3.Checked) || !this.computer.Checked && !this.co1.Checked && (!this.co2.Checked && !this.co3.Checked) || (/*this.person.SelectedValue == "-1" ||*/ this.pp.Text == "" || !this.uni.Checked && !this.u1.Checked && (!this.u2.Checked && !this.u3.Checked)) || (!this.mil.Checked && !this.mi1.Checked && (!this.mi2.Checked && !this.mi3.Checked) || !this.dem.Checked && !this.d1.Checked && (!this.d2.Checked && !this.d3.Checked) || !this.time.Checked && !this.t1.Checked && (!this.t2.Checked && !this.t3.Checked))))))
            {
                this.formValidate();
            }
            else
            {
                string text1 = this.datevalue.Text;
                string text2 = this.pp.Text;
                string text3 = this.reportid.Text;
                string text4 = this.trooper.Text;
                string text5 = this.post.Text;
                string text6 = this.citymain.Text;
                string str1 = DateTime.Now.ToString("MM");
                string str2 = DateTime.Now.ToString("yyyy");
                //if (this.admin.SelectedIndex == 1)
                //    this.mainadmin = "Execellent";
                //else if (this.admin.SelectedIndex == 2)
                //    this.mainadmin = "Very Satisfactory";
                //else if (this.admin.SelectedIndex == 3)
                //    this.mainadmin = "Satisfactory";
                //else if (this.admin.SelectedIndex == 4)
                //    this.mainadmin = "UnSatisfactory";
                //if (this.build.SelectedIndex == 1)
                //    this.mainbuild = "Execellent";
                //else if (this.build.SelectedIndex == 2)
                //    this.mainbuild = "Very Satisfactory";
                //else if (this.build.SelectedIndex == 3)
                //    this.mainbuild = "Satisfactory";
                //else if (this.build.SelectedIndex == 4)
                //    this.mainbuild = "UnSatisfactory";
                //if (this.equip.SelectedIndex == 1)
                //    this.mainequip = "Execellent";
                //else if (this.equip.SelectedIndex == 2)
                //    this.mainequip = "Very Satisfactory";
                //else if (this.equip.SelectedIndex == 3)
                //    this.mainequip = "Satisfactory";
                //else if (this.equip.SelectedIndex == 4)
                //    this.mainequip = "UnSatisfactory";
                //if (this.person.SelectedIndex == 1)
                //    this.mainperson = "Execellent";
                //else if (this.person.SelectedIndex == 2)
                //    this.mainperson = "Very Satisfactory";
                //else if (this.person.SelectedIndex == 3)
                //    this.mainperson = "Satisfactory";
                //else if (this.person.SelectedIndex == 4)
                //    this.mainperson = "UnSatisfactory";
                if (this.record.Checked)
                    this.Rs = "Execellent";
                else if (this.r1.Checked)
                    this.Rs = "Very Satisfactory";
                else if (this.r2.Checked)
                    this.Rs = "Satisfactory";
                else if (this.r3.Checked)
                    this.Rs = "UnSatisfactory";
                if (this.employee.Checked)
                    this.Es = "Execellent";
                else if (this.e1.Checked)
                    this.Es = "Very Satisfactory";
                else if (this.e2.Checked)
                    this.Es = "Satisfactory";
                else if (this.e3.Checked)
                    this.Es = "UnSatisfactory";
                if (this.manpower.Checked)
                    this.Ms = "Execellent";
                else if (this.m1.Checked)
                    this.Ms = "Very Satisfactory";
                else if (this.m2.Checked)
                    this.Ms = "Satisfactory";
                else if (this.m3.Checked)
                    this.Ms = "UnSatisfactory";
                if (this.com.Checked)
                    this.Cs = "Execellent";
                else if (this.c1.Checked)
                    this.Cs = "Very Satisfactory";
                else if (this.c2.Checked)
                    this.Cs = "Satisfactory";
                else if (this.c3.Checked)
                    this.Cs = "UnSatisfactory";
                if (this.fur.Checked)
                    this.Fs = "Execellent";
                else if (this.f1.Checked)
                    this.Fs = "Very Satisfactory";
                else if (this.f2.Checked)
                    this.Fs = "Satisfactory";
                else if (this.f3.Checked)
                    this.Fs = "UnSatisfactory";
                if (this.radio.Checked)
                    this.Ra = "Execellent";
                else if (this.ra1.Checked)
                    this.Ra = "Very Satisfactory";
                else if (this.ra2.Checked)
                    this.Ra = "Satisfactory";
                else if (this.ra3.Checked)
                    this.Ra = "UnSatisfactory";
                if (this.computer.Checked)
                    this.Co = "Execellent";
                else if (this.co1.Checked)
                    this.Co = "Very Satisfactory";
                else if (this.co2.Checked)
                    this.Co = "Satisfactory";
                else if (this.co3.Checked)
                    this.Co = "UnSatisfactory";
                if (this.uni.Checked)
                    this.Un = "Execellent";
                else if (this.u1.Checked)
                    this.Un = "Very Satisfactory";
                else if (this.u2.Checked)
                    this.Un = "Satisfactory";
                else if (this.u3.Checked)
                    this.Un = "UnSatisfactory";
                if (this.mil.Checked)
                    this.Mi = "Execellent";
                else if (this.mi1.Checked)
                    this.Mi = "Very Satisfactory";
                else if (this.mi2.Checked)
                    this.Mi = "Satisfactory";
                else if (this.m3.Checked)
                    this.Mi = "UnSatisfactory";
                if (this.dem.Checked)
                    this.De = "Execellent";
                else if (this.d1.Checked)
                    this.De = "Very Satisfactory";
                else if (this.d2.Checked)
                    this.De = "Satisfactory";
                else if (this.d3.Checked)
                    this.De = "UnSatisfactory";
                if (this.time.Checked)
                    this.Ti = "Execellent";
                else if (this.t1.Checked)
                    this.Ti = "Very Satisfactory";
                else if (this.t2.Checked)
                    this.Ti = "Satisfactory";
                else if (this.t3.Checked)
                    this.Ti = "UnSatisfactory";
                string str3 = this.User.Identity.Name.Substring(6);
                SqlConnection connection1 = new SqlConnection(this.con);
                SqlCommand sqlCommand = new SqlCommand("select EmployeeID from ViewInspection where sAMAccountName='" + str3 + "'", connection1);
                connection1.Open();
                SqlDataReader sqlDataReader1 = sqlCommand.ExecuteReader();
                while (sqlDataReader1.Read())
                    this.employeeid = sqlDataReader1["EmployeeID"].ToString();
                connection1.Close();
                SqlConnection connection2 = new SqlConnection(this.con1);
                connection2.Open();
                new SqlCommand("INSERT INTO CommInspectionReport (Troop,Post,City,InspectionDate,InspectionMonth,InspectionYear,AdminAndSupervision,RecordsAndReports,EmpPerformanceMgt,SchAndManAllocation,BuildingAndGrounds,ComCenterCleanAndMain,FurnitureAndGrounds,Equipment,RadioEquipment,ComputerPrinterETC,Personnel,PersonnelPresent,UniformAndAppearance,MilitaryCourtesy,DemeanorAndMorale,OnTimeAndPreparedForInspection,CreatedBy,CreatedDate,ReportID,ApprovalStatus)VALUES ('" + text4 + "','" + text5 + "', '" + text6 + "', '" + text1 + "','" + str1 + "', '" + str2 + "',  '" + this.mainadmin + "', '" + this.Rs + "', '" + this.Es + "','" + this.Ms + "', '" + this.mainbuild + "', '" + this.Cs + "', '" + this.Fs + "', '" + this.mainequip + "', '" + this.Ra + "', '" + this.Co + "', '" + this.mainperson + "', '" + text2 + "', '" + this.Un + "', '" + this.Mi + "', '" + this.De + "', '" + this.Ti + "','" + this.employeeid + "','" + this.savedate + "','" + text3 + "', 'Pending' )", connection2).ExecuteNonQuery();
                connection2.Close();
                this.upperformdisable();
                this.lowerformEnable();
                SqlConnection connection3 = new SqlConnection(this.con1);
                connection3.Open();
                SqlDataReader sqlDataReader2 = new SqlCommand("select * from CommunicationCount", connection3).ExecuteReader();
                while (sqlDataReader2.Read())
                {
                    this.totalcalls_cm.Text = sqlDataReader2["TotalCalls_CM"].ToString();
                    this.totalcalls_lm.Text = sqlDataReader2["TotalCalls_LM"].ToString();
                    this.totalcalls_ytd_cy.Text = sqlDataReader2["TotalCalls_YTD_CY"].ToString();
                    this.totalcalls_smly.Text = sqlDataReader2["TotalCalls_SMLY"].ToString();
                    this.totalcalls_ytd_ly.Text = sqlDataReader2["TotalCalls_YTD_LY"].ToString();
                    this.abandonedveh_cm.Text = sqlDataReader2["AbandonedVeh_CM"].ToString();
                    this.abandonedveh_lm.Text = sqlDataReader2["AbandonedVeh_LM"].ToString();
                    this.abandonedveh_ytd_cy.Text = sqlDataReader2["AbandonedVeh_YTD_CY"].ToString();
                    this.abandonedveh_smly.Text = sqlDataReader2["AbandonedVeh_SMLY"].ToString();
                    this.abandonedveh_ytd_ly.Text = sqlDataReader2["AbandonedVeh_YTD_LY"].ToString();
                    this.chases_cm.Text = sqlDataReader2["Chases_CM"].ToString();
                    this.chases_lm.Text = sqlDataReader2["Chases_LM"].ToString();
                    this.chases_ytd_cy.Text = sqlDataReader2["Chases_YTD_CY"].ToString();
                    this.chases_smly.Text = sqlDataReader2["Chases_SMLY"].ToString();
                    this.chases_ytd_ly.Text = sqlDataReader2["Chases_YTD_LY"].ToString();
                    this.controlledburns_cm.Text = sqlDataReader2["ControlledBurns_CM"].ToString();
                    this.controlledburns_lm.Text = sqlDataReader2["ControlledBurns_LM"].ToString();
                    this.controlledburns_ytd_cy.Text = sqlDataReader2["ControlledBurns_YTD_CY"].ToString();
                    this.controlledburns_smly.Text = sqlDataReader2["ControlledBurns_SMLY"].ToString();
                    this.controlledburns_ytd_ly.Text = sqlDataReader2["ControlledBurns_YTD_LY"].ToString();
                    this.criminaldriverhx_cm.Text = sqlDataReader2["CriminalDriverHX_CM"].ToString();
                    this.criminaldriverhx_lm.Text = sqlDataReader2["CriminalDriverHX_LM"].ToString();
                    this.criminaldriverhx_ytd_cy.Text = sqlDataReader2["CriminalDriverHX_YTD_CY"].ToString();
                    this.criminaldriverhx_smly.Text = sqlDataReader2["CriminalDriverHX_SMLY"].ToString();
                    this.criminaldriverhx_ytd_ly.Text = sqlDataReader2["CriminalDriverHX_YTD_LY"].ToString();
                    this.dnrcalls_cm.Text = sqlDataReader2["DNRCalls_CM"].ToString();
                    this.dnrcalls_lm.Text = sqlDataReader2["DNRCalls_LM"].ToString();
                    this.dnrcalls_ytd_cy.Text = sqlDataReader2["DNRCalls_YTD_CY"].ToString();
                    this.dnrcalls_smly.Text = sqlDataReader2["DNRCalls_SMLY"].ToString();
                    this.dnrcalls_ytd_ly.Text = sqlDataReader2["DNRCalls_YTD_LY"].ToString();
                    this.equipissue_cm.Text = sqlDataReader2["EquipIssue_CM"].ToString();
                    this.equipissue_lm.Text = sqlDataReader2["EquipIssue_LM"].ToString();
                    this.equipissue_ytd_cy.Text = sqlDataReader2["EquipIssue_YTD_CY"].ToString();
                    this.equipissue_smly.Text = sqlDataReader2["EquipIssue_SMLY"].ToString();
                    this.equipissue_ytd_ly.Text = sqlDataReader2["EquipIssue_YTD_LY"].ToString();
                    this.flightplans_cm.Text = sqlDataReader2["FlightPlans_CM"].ToString();
                    this.flightplans_lm.Text = sqlDataReader2["FlightPlans_LM"].ToString();
                    this.flightplans_ytd_cy.Text = sqlDataReader2["FlightPlans_YTD_CY"].ToString();
                    this.flightplans_smly.Text = sqlDataReader2["FlightPlans_SMLY"].ToString();
                    this.flightplans_ytd_ly.Text = sqlDataReader2["FlightPlans_YTD_LY"].ToString();
                    this.gcichits_cm.Text = sqlDataReader2["GCICHits_CM"].ToString();
                    this.gcichits_lm.Text = sqlDataReader2["GCICHits_LM"].ToString();
                    this.gcichits_ytd_cy.Text = sqlDataReader2["GCICHits_YTD_CY"].ToString();
                    this.gcichits_smly.Text = sqlDataReader2["GCICHits_SMLY"].ToString();
                    this.gcichits_ytd_ly.Text = sqlDataReader2["GCICHits_YTD_LY"].ToString();
                    this.motoassist_cm.Text = sqlDataReader2["MototAssist_CM"].ToString();
                    this.motoassist_lm.Text = sqlDataReader2["MototAssist_LM"].ToString();
                    this.motoassist_ytd_cy.Text = sqlDataReader2["MototAssist_YTD_CY"].ToString();
                    this.motoassist_smly.Text = sqlDataReader2["MototAssist_SMLY"].ToString();
                    this.motoassist_ytd_ly.Text = sqlDataReader2["MototAssist_YTD_LY"].ToString();
                    this.openrecord_cm.Text = sqlDataReader2["OpenRecords_CM"].ToString();
                    this.openrecord_lm.Text = sqlDataReader2["OpenRecords_LM"].ToString();
                    this.openrecord_ytd_cy.Text = sqlDataReader2["OpenRecords_YTD_CY"].ToString();
                    this.openrecord_smly.Text = sqlDataReader2["OpenRecords_SMLY"].ToString();
                    this.openrecord_ytd_ly.Text = sqlDataReader2["OpenRecords_YTD_LY"].ToString();
                    this.relays_cm.Text = sqlDataReader2["Relays_CM"].ToString();
                    this.relays_lm.Text = sqlDataReader2["Relays_LM"].ToString();
                    this.relays_ytd_cy.Text = sqlDataReader2["Relays_YTD_CY"].ToString();
                    this.relays_smly.Text = sqlDataReader2["Relays_SMLY"].ToString();
                    this.relays_ytd_ly.Text = sqlDataReader2["Relays_YTD_LY"].ToString();
                    this.roadchecks_cm.Text = sqlDataReader2["RoadChecks_CM"].ToString();
                    this.roadchecks_lm.Text = sqlDataReader2["RoadChecks_LM"].ToString();
                    this.roadchecks_ytd_cy.Text = sqlDataReader2["RoadChecks_YTD_CY"].ToString();
                    this.roadchecks_smly.Text = sqlDataReader2["RoadChecks_SMLY"].ToString();
                    this.roadchecks_ytd_ly.Text = sqlDataReader2["RoadChecks_YTD_LY"].ToString();
                    this.trafficstops_cm.Text = sqlDataReader2["TrafficStops_CM"].ToString();
                    this.trafficstops_lm.Text = sqlDataReader2["TrafficStops_LM"].ToString();
                    this.trafficstops_ytd_cy.Text = sqlDataReader2["TrafficStops_YTD_CY"].ToString();
                    this.trafficstops_smly.Text = sqlDataReader2["TrafficStops_SMLY"].ToString();
                    this.trafficstops_ytd_ly.Text = sqlDataReader2["TrafficStops_YTD_LY"].ToString();
                    this.towedveh_cm.Text = sqlDataReader2["TowedVeh_CM"].ToString();
                    this.towedveh_lm.Text = sqlDataReader2["TowedVeh_LM"].ToString();
                    this.towedveh_ytd_cy.Text = sqlDataReader2["TowedVeh_YTD_CY"].ToString();
                    this.towedveh_smly.Text = sqlDataReader2["TowedVeh_SMLY"].ToString();
                    this.towedveh_ytd_ly.Text = sqlDataReader2["TowedVeh_YTD_LY"].ToString();
                }
                connection3.Close();
                SqlConnection connection4 = new SqlConnection(this.con2);
                connection4.Open();
                SqlDataReader sqlDataReader3 = new SqlCommand("SELECT * FROM CommInspectionPersonnelCount", connection4).ExecuteReader();
                while (sqlDataReader3.Read())
                {
                    this.totalcalls_cm2.Text = sqlDataReader3["Chief_CM"].ToString();
                    this.totalcalls_smly2.Text = sqlDataReader3["Chief_SMLY"].ToString();
                    this.abandonedveh_cm2.Text = sqlDataReader3["Seniors_CM"].ToString();
                    this.abandonedveh_smly2.Text = sqlDataReader3["Seniors_SMLY"].ToString();
                    this.chases_cm2.Text = sqlDataReader3["Dispatcher2_CM"].ToString();
                    this.chases_smly2.Text = sqlDataReader3["Dispatcher2_SMLY"].ToString();
                    this.controlledburns_cm2.Text = sqlDataReader3["Dispathcer1_CM"].ToString();
                    this.controlledburns_smly2.Text = sqlDataReader3["Dispatcher1_SMLY"].ToString();
                    this.criminaldriverhx_smly2.Text = sqlDataReader3["GCICCertification"].ToString();
                    this.gcichits_wantedperson.Text = sqlDataReader3["GCIC_Wanted"].ToString();
                    this.motoassist_stolevehicle.Text = sqlDataReader3["GCI_StolenVeh"].ToString();
                    this.openrecord_stoleguns.Text = sqlDataReader3["GCI_StolenGun"].ToString();
                    this.relays_other.Text = sqlDataReader3["GCIC_Other"].ToString();
                }
                connection4.Close();
                this.SetFocus((Control)this.remarks);
            }
        }

        private void formValidate()
        {
            //if (this.admin.SelectedValue != "-1")
            //    this.adminRequired.Text = "";
            //else if (this.admin.SelectedValue == "-1")
            //{
            //    this.adminRequired.Text = "*Required";
            //    this.m3.Focus();
            //}
            if (!this.record.Checked && !this.r1.Checked && (!this.r2.Checked && !this.r3.Checked))
            {
                this.recordRequired.Text = "*Required";
                this.m3.Focus();
            }
            else if (this.record.Checked || this.r1.Checked || (this.r2.Checked || this.r3.Checked))
                this.recordRequired.Text = "";
            if (!this.employee.Checked && !this.e1.Checked && (!this.e2.Checked && !this.e3.Checked))
            {
                this.employeeRequired.Text = "*Required";
                this.m3.Focus();
            }
            if (this.employee.Checked || this.e1.Checked || (this.e2.Checked || this.e3.Checked))
                this.employeeRequired.Text = "";
            if (!this.manpower.Checked && !this.m1.Checked && (!this.m2.Checked && !this.m3.Checked))
            {
                this.manpowerRequired.Text = "*Required";
                this.m3.Focus();
            }
            else if (this.manpower.Checked || this.m1.Checked || (this.m2.Checked || this.m3.Checked))
                this.manpowerRequired.Text = "";
            //if (this.build.SelectedValue != "-1")
            //    this.buildRequired.Text = "";
            //else if (this.build.SelectedValue == "-1")
            //{
            //    this.buildRequired.Text = "*Required";
            //    this.fur.Focus();
            //}
            if (!this.com.Checked && !this.c1.Checked && (!this.c2.Checked && !this.c3.Checked))
            {
                this.comRequired.Text = "*Required";
                this.fur.Focus();
            }
            else if (this.com.Checked || this.c1.Checked || (this.c2.Checked || !this.c3.Checked))
                this.comRequired.Text = "";
            if (!this.fur.Checked && !this.f1.Checked && (!this.f2.Checked && !this.f3.Checked))
            {
                this.furRequired.Text = "*Required";
               // this.equip.Focus();
            }
            else if (this.fur.Checked || this.f1.Checked || (this.f2.Checked || this.f3.Checked))
                this.furRequired.Text = "";
            //if (this.equip.SelectedValue != "-1")
            //    this.equipRequired.Text = "";
            //else if (this.equip.SelectedValue == "-1")
            //{
            //    this.equipRequired.Text = "*Required";
            //    this.computer.Focus();
            //}
            if (!this.radio.Checked && !this.ra1.Checked && (!this.ra2.Checked && !this.ra3.Checked))
            {
                this.radioRquired.Text = "*Required";
                this.pp.Focus();
            }
            else if (this.radio.Checked || this.ra1.Checked || (this.ra2.Checked || this.ra3.Checked))
                this.radioRquired.Text = "";
            if (!this.computer.Checked && !this.co1.Checked && (!this.co2.Checked && !this.co3.Checked))
            {
                this.computerRequired.Text = "*Required";
                this.pp.Focus();
            }
            else if (this.computer.Checked || this.co1.Checked || (this.co2.Checked || this.co3.Checked))
                this.computerRequired.Text = "";
            //if (this.person.SelectedValue != "-1")
            //    this.personReequired.Text = "";
            //else if (this.person.SelectedValue == "-1")
            //    this.personReequired.Text = "*Required";
            if (this.pp.Text != "")
                this.ppRequired.Text = "";
            else if (this.pp.Text == "")
                this.ppRequired.Text = "*Required";
            if (!this.uni.Checked && !this.u1.Checked && (!this.u2.Checked && !this.u3.Checked))
                this.uniRequired.Text = "*Required";
            else if (this.uni.Checked || this.u1.Checked || (this.u2.Checked || this.u3.Checked))
                this.uniRequired.Text = "";
            if (!this.mil.Checked && !this.mi1.Checked && (!this.mi2.Checked && !this.mi3.Checked))
                this.milRequired.Text = "*Required";
            else if (this.mil.Checked || this.mi1.Checked || (!this.mi2.Checked || !this.mi3.Checked))
                this.milRequired.Text = "";
            if (!this.dem.Checked && !this.d1.Checked && (!this.d2.Checked && !this.d3.Checked))
                this.demRequired.Text = "*Required";
            else if (this.dem.Checked || this.d1.Checked || (this.d2.Checked || this.d3.Checked))
                this.demRequired.Text = "";
            if (!this.time.Checked && !this.t1.Checked && (!this.t2.Checked && !this.t3.Checked))
                this.timeRequired.Text = "*Required";
            else if (this.time.Checked || this.t1.Checked || (this.t2.Checked || this.t3.Checked))
                this.timeRequired.Text = "";
            if (remarks.Text == "")
            {
                remarksRequired.Text = "*Required";
                SetFocus(remarks);
            }
            else
            {
                remarksRequired.Text = "*Required";
            }
            if (person2.SelectedValue != "-1")
            {
                person2Required.Text = "";
            }
            else
            {
                person2Required.Text = "*Required";
            }
            if (reason.Text == "")
            {
                rejectrequired.Visible = true;
            }
            else
            {
                rejectrequired.Visible = false;
            }
            this.remarksRequired.Text = !(this.remarks.Text == "") ? "" : "*Required";
            this.person2Required.Text = !(this.person2.SelectedValue == "-1") ? "" : "*Required";
            this.upperformenabled();
        }

        protected void save_Click(object sender, EventArgs e)
        {
            string text7 = this.totalcalls_cm.Text;
            string text8 = this.totalcalls_lm.Text;
            string text9 = this.totalcalls_ytd_cy.Text;
            string text10 = this.totalcalls_smly.Text;
            string text11 = this.totalcalls_ytd_ly.Text;
            string text12 = this.totalcalls_cm2.Text;
            string text13 = this.totalcalls_smly2.Text;
            string text14 = this.abandonedveh_cm.Text;
            string text15 = this.abandonedveh_lm.Text;
            string text16 = this.abandonedveh_ytd_cy.Text;
            string text17 = this.abandonedveh_smly.Text;
            string text18 = this.abandonedveh_ytd_ly.Text;
            string text19 = this.abandonedveh_cm2.Text;
            string text20 = this.abandonedveh_smly2.Text;
            string text21 = this.chases_cm.Text;
            string text22 = this.chases_lm.Text;
            string text23 = this.chases_ytd_cy.Text;
            string text24 = this.chases_smly.Text;
            string text25 = this.chases_ytd_ly.Text;
            string text26 = this.chases_cm2.Text;
            string text27 = this.chases_smly2.Text;
            string text28 = this.controlledburns_cm.Text;
            string text29 = this.controlledburns_lm.Text;
            string text30 = this.controlledburns_ytd_cy.Text;
            string text31 = this.controlledburns_smly.Text;
            string text32 = this.controlledburns_ytd_ly.Text;
            string text33 = this.controlledburns_cm2.Text;
            string text34 = this.controlledburns_smly2.Text;
            string text35 = this.criminaldriverhx_cm.Text;
            string text36 = this.criminaldriverhx_lm.Text;
            string text37 = this.criminaldriverhx_ytd_cy.Text;
            string text38 = this.criminaldriverhx_smly.Text;
            string text39 = this.criminaldriverhx_ytd_ly.Text;
            string text40 = this.criminaldriverhx_smly2.Text;
            string text41 = this.dnrcalls_cm.Text;
            string text42 = this.dnrcalls_lm.Text;
            string text43 = this.dnrcalls_ytd_cy.Text;
            string text44 = this.dnrcalls_smly.Text;
            string text45 = this.dnrcalls_ytd_ly.Text;
            string text46 = this.equipissue_cm.Text;
            string text47 = this.equipissue_lm.Text;
            string text48 = this.equipissue_ytd_cy.Text;
            string text49 = this.equipissue_smly.Text;
            string text50 = this.equipissue_ytd_ly.Text;
            string text51 = this.flightplans_cm.Text;
            string text52 = this.flightplans_lm.Text;
            string text53 = this.flightplans_ytd_cy.Text;
            string text54 = this.flightplans_smly.Text;
            string text55 = this.flightplans_ytd_ly.Text;
            string text56 = this.gcichits_cm.Text;
            string text57 = this.gcichits_lm.Text;
            string text58 = this.gcichits_ytd_cy.Text;
            string text59 = this.gcichits_smly.Text;
            string text60 = this.gcichits_ytd_ly.Text;
            string text61 = this.gcichits_wantedperson.Text;
            string text62 = this.motoassist_cm.Text;
            string text63 = this.motoassist_lm.Text;
            string text64 = this.motoassist_ytd_cy.Text;
            string text65 = this.motoassist_smly.Text;
            string text66 = this.motoassist_ytd_ly.Text;
            string text67 = this.motoassist_stolevehicle.Text;
            string text68 = this.openrecord_cm.Text;
            string text69 = this.openrecord_lm.Text;
            string text70 = this.openrecord_ytd_cy.Text;
            string text71 = this.openrecord_smly.Text;
            string text72 = this.openrecord_ytd_ly.Text;
            string text73 = this.openrecord_stoleguns.Text;
            string text74 = this.relays_cm.Text;
            string text75 = this.relays_lm.Text;
            string text76 = this.relays_ytd_cy.Text;
            string text77 = this.relays_smly.Text;
            string text78 = this.relays_ytd_ly.Text;
            string text79 = this.relays_other.Text;
            string text80 = this.roadchecks_cm.Text;
            string text81 = this.roadchecks_lm.Text;
            string text82 = this.roadchecks_ytd_cy.Text;
            string text83 = this.roadchecks_smly.Text;
            string text84 = this.roadchecks_ytd_ly.Text;
            string text85 = this.trafficstops_cm.Text;
            string text86 = this.trafficstops_lm.Text;
            string text87 = this.trafficstops_ytd_cy.Text;
            string text88 = this.trafficstops_smly.Text;
            string text89 = this.trafficstops_ytd_ly.Text;
            string text90 = this.towedveh_cm.Text;
            string text91 = this.towedveh_lm.Text;
            string text92 = this.towedveh_ytd_cy.Text;
            string text93 = this.towedveh_smly.Text;
            string text94 = this.towedveh_ytd_ly.Text;
            string text95 = this.reportid.Text;
            string text96 = this.remarks.Text;
            String MainRemark = text96.Replace("'", "''");
            this.person2.SelectedItem.ToString();

            SqlConnection checkcon = new SqlConnection(con1);
            checkcon.Open();
            SqlCommand checkcmd = new SqlCommand("SELECT COUNT(ReportID) FROM CommInspectionReport WHERE ReportID='" + Session["RecordID"] + "'", checkcon);
            int RecordExist = (int)checkcmd.ExecuteScalar();
            if (RecordExist == 1)
            {
                //Record Exist
                SqlConnection updatecon = new SqlConnection(con2);
                updatecon.Open();
                SqlCommand updatecmd = new SqlCommand("UPDATE CommInspectionReport SET TotalCalls_CM = '" + text7 + "', TotalCalls_LM = '" + text8 + "', TotalCalls_YTD_CY = '" + text9 + "', TotalCalls_SMLY = '" + text10 + "',TotalCalls_YTD_LY = '" + text11 + "', TotalCalls_CM2 = '" + text12 + "', TotalCalls_SMLY2 = '" + text13 + "',  AbandonedVeh_CM = '" + text14 + "', AbandonedVeh_CM2 = '" + text19 + "',AbandonedVeh_LM = '" + text15 + "', AbandonedVeh_YTD_CY = '" + text16 + "', AbandonedVeh_SMLY = '" + text17 + "', AbandonedVeh_SMLY2 = '" + text20 + "', AbandonedVeh_YTD_LY = '" + text18 + "',Chases_CM = '" + text21 + "', Chases_CM2 = '" + text26 + "', Chases_LM = '" + text22 + "', Chases_YTD_CY = '" + text23 + "', Chases_SMLY = '" + text24 + "', Chases_SMLY2 = '" + text27 + "', Chases_YTD_LY = '" + text25 + "',ControlledBurns_CM = '" + text28 + "', ControlledBurns_CM2 = '" + text33 + "', ControlledBurns_LM = '" + text29 + "', ControlledBurns_YTD_CY = '" + text30 + "', ControlledBurns_SMLY = '" + text31 + "',ControlledBurns_SMLY2 = '" + text34 + "', ControlledBurns_YTD_LY = '" + text32 + "', CriminalDriverHX_CM = '" + text35 + "', CriminalDriverHX_LM = '" + text36 + "', CriminalDriverHX_YTD_CY = '" + text37 + "',CriminalDriverHX_SMLY = '" + text38 + "', CriminalDriverHX_SMLY2 = '" + text40 + "', CriminalDriverHX_YTD_LY = '" + text39 + "', DNRCalls_CM = '" + text41 + "', DNRCalls_LM = '" + text42 + "',DNRCalls_YTD_CY = '" + text43 + "', DNRCalls_SMLY = '" + text44 + "', DNRCalls_YTD_LY = '" + text45 + "', EquipIssue_CM = '" + text46 + "', EquipIssue_LM = '" + text47 + "', EquipIssue_YTD_CY = '" + text48 + "',EquipIssue_SMLY = '" + text49 + "', EquipIssue_YTD_LY = '" + text50 + "', FlightPlans_CM = '" + text51 + "', FlightPlans_LM = '" + text52 + "', FlightPlans_YTD_CY = '" + text53 + "', FlightPlans_SMLY = '" + text54 + "',FlightPlans_YTD_LY = '" + text55 + "', GCICHits_CM = '" + text56 + "', GCICHits_LM = '" + text57 + "', GCICHits_YTD_CY = '" + text58 + "', GCICHits_SMLY = '" + text59 + "', GCICHits_YTD_LY = '" + text60 + "',GCICHits_WantedPerson = '" + text61 + "', MototAssist_CM = '" + text62 + "', MototAssist_LM = '" + text63 + "', MototAssist_YTD_CY = '" + text64 + "', MototAssist_SMLY = '" + text65 + "', MototAssist_YTD_LY = '" + text66 + "',MototAssist_StoleVehicle = '" + text67 + "', OpenRecords_CM = '" + text68 + "', OpenRecords_LM = '" + text69 + "', OpenRecords_YTD_CY = '" + text70 + "', OpenRecords_SMLY = '" + text71 + "', OpenRecords_YTD_LY = '" + text72 + "',OpenRecords_StoleGuns = '" + text73 + "', Relays_CM = '" + text74 + "', Relays_LM = '" + text75 + "', Relays_YTD_CY = '" + text76 + "', Relays_SMLY = '" + text77 + "', Relays_YTD_LY = '" + text78 + "', Relays_Other = '" + text79 + "',RoadChecks_CM = '" + text80 + "', RoadChecks_LM = '" + text81 + "', RoadChecks_YTD_CY = '" + text82 + "', RoadChecks_SMLY = '" + text83 + "', RoadChecks_YTD_LY = '" + text84 + "',TrafficStops_CM = '" + text85 + "', TrafficStops_LM = '" + text86 + "', TrafficStops_YTD_CY = '" + text87 + "', TrafficStops_SMLY = '" + text88 + "', TrafficStops_YTD_LY = '" + text89 + "',TowedVeh_CM = '" + text90 + "', TowedVeh_LM = '" + text91 + "', TowedVeh_YTD_CY = '" + text92 + "', TowedVeh_SMLY = '" + text93 + "', TowedVeh_YTD_LY = '" + text94 + "', ApprovalStatus='" + "Pending" + "', Remarks = '" + MainRemark + "' WHERE ReportID = '" + text95 + "' ", updatecon);
                updatecmd.ExecuteNonQuery();
                updatecon.Close();
                this.sendMessage(this.person2.SelectedValue + "@gsp.net");
                this.Response.Redirect(this.Request.Url.AbsoluteUri);
            }
            else
            {
                #region mainInsertCode
                if (this.person2.SelectedValue != "-1")
                {
                    string text1 = this.datevalue.Text;
                    string text2 = this.trooper.Text;
                    string text3 = this.post.Text;
                    string text4 = this.citymain.Text;
                    DateTime.Now.ToString("MM");
                    DateTime.Now.ToString("yyyy");
                    this.person2Required.Text = "";
                    string str = this.User.Identity.Name.Substring(6);
                    string text5 = this.reportid.Text;
                    this.troopcom.Visible = false;
                    string text6 = this.person2.SelectedItem.Text;
                    SqlConnection connection1 = new SqlConnection(this.realidentity);
                    SqlCommand sqlCommand = new SqlCommand("select EmployeeID,EmployeeName from ViewInspection where sAMAccountName='" + str + "'", connection1);
                    connection1.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        this.employeeid = sqlDataReader["EmployeeID"].ToString();
                        this.CreatedBy = sqlDataReader["EmployeeName"].ToString();
                    }
                    connection1.Close();
                    SqlConnection connection2 = new SqlConnection(this.con1);
                    connection2.Open();
                    new SqlCommand("INSERT INTO CommInspectionReport (Post,ApprovalOfficer,CreatedBy,CreatedDate,ReportID,ApprovalStatus,ReportStage)VALUES ('COMMUNICATIONS','" + text6 + "','" + this.employeeid + "','" + this.savedate + "','" + text5 + "', 'Pending', 'Awaiting T.O''s Approval' )", connection2).ExecuteNonQuery();
                    connection2.Close();

                    SqlConnection connection3 = new SqlConnection(this.con1);
                    connection3.Open();
                    new SqlCommand("UPDATE CommInspectionReport SET TotalCalls_CM = '" + text7 + "', TotalCalls_LM = '" + text8 + "', TotalCalls_YTD_CY = '" + text9 + "', TotalCalls_SMLY = '" + text10 + "',TotalCalls_YTD_LY = '" + text11 + "', TotalCalls_CM2 = '" + text12 + "', TotalCalls_SMLY2 = '" + text13 + "',  AbandonedVeh_CM = '" + text14 + "', AbandonedVeh_CM2 = '" + text19 + "',AbandonedVeh_LM = '" + text15 + "', AbandonedVeh_YTD_CY = '" + text16 + "', AbandonedVeh_SMLY = '" + text17 + "', AbandonedVeh_SMLY2 = '" + text20 + "', AbandonedVeh_YTD_LY = '" + text18 + "',Chases_CM = '" + text21 + "', Chases_CM2 = '" + text26 + "', Chases_LM = '" + text22 + "', Chases_YTD_CY = '" + text23 + "', Chases_SMLY = '" + text24 + "', Chases_SMLY2 = '" + text27 + "', Chases_YTD_LY = '" + text25 + "',ControlledBurns_CM = '" + text28 + "', ControlledBurns_CM2 = '" + text33 + "', ControlledBurns_LM = '" + text29 + "', ControlledBurns_YTD_CY = '" + text30 + "', ControlledBurns_SMLY = '" + text31 + "',ControlledBurns_SMLY2 = '" + text34 + "', ControlledBurns_YTD_LY = '" + text32 + "', CriminalDriverHX_CM = '" + text35 + "', CriminalDriverHX_LM = '" + text36 + "', CriminalDriverHX_YTD_CY = '" + text37 + "',CriminalDriverHX_SMLY = '" + text38 + "', CriminalDriverHX_SMLY2 = '" + text40 + "', CriminalDriverHX_YTD_LY = '" + text39 + "', DNRCalls_CM = '" + text41 + "', DNRCalls_LM = '" + text42 + "',DNRCalls_YTD_CY = '" + text43 + "', DNRCalls_SMLY = '" + text44 + "', DNRCalls_YTD_LY = '" + text45 + "', EquipIssue_CM = '" + text46 + "', EquipIssue_LM = '" + text47 + "', EquipIssue_YTD_CY = '" + text48 + "',EquipIssue_SMLY = '" + text49 + "', EquipIssue_YTD_LY = '" + text50 + "', FlightPlans_CM = '" + text51 + "', FlightPlans_LM = '" + text52 + "', FlightPlans_YTD_CY = '" + text53 + "', FlightPlans_SMLY = '" + text54 + "',FlightPlans_YTD_LY = '" + text55 + "', GCICHits_CM = '" + text56 + "', GCICHits_LM = '" + text57 + "', GCICHits_YTD_CY = '" + text58 + "', GCICHits_SMLY = '" + text59 + "', GCICHits_YTD_LY = '" + text60 + "',GCICHits_WantedPerson = '" + text61 + "', MototAssist_CM = '" + text62 + "', MototAssist_LM = '" + text63 + "', MototAssist_YTD_CY = '" + text64 + "', MototAssist_SMLY = '" + text65 + "', MototAssist_YTD_LY = '" + text66 + "',MototAssist_StoleVehicle = '" + text67 + "', OpenRecords_CM = '" + text68 + "', OpenRecords_LM = '" + text69 + "', OpenRecords_YTD_CY = '" + text70 + "', OpenRecords_SMLY = '" + text71 + "', OpenRecords_YTD_LY = '" + text72 + "',OpenRecords_StoleGuns = '" + text73 + "', Relays_CM = '" + text74 + "', Relays_LM = '" + text75 + "', Relays_YTD_CY = '" + text76 + "', Relays_SMLY = '" + text77 + "', Relays_YTD_LY = '" + text78 + "', Relays_Other = '" + text79 + "',RoadChecks_CM = '" + text80 + "', RoadChecks_LM = '" + text81 + "', RoadChecks_YTD_CY = '" + text82 + "', RoadChecks_SMLY = '" + text83 + "', RoadChecks_YTD_LY = '" + text84 + "',TrafficStops_CM = '" + text85 + "', TrafficStops_LM = '" + text86 + "', TrafficStops_YTD_CY = '" + text87 + "', TrafficStops_SMLY = '" + text88 + "', TrafficStops_YTD_LY = '" + text89 + "',TowedVeh_CM = '" + text90 + "', TowedVeh_LM = '" + text91 + "', TowedVeh_YTD_CY = '" + text92 + "', TowedVeh_SMLY = '" + text93 + "', TowedVeh_YTD_LY = '" + text94 + "', Remarks = '" + text96 + "' WHERE ReportID = '" + text95 + "' ", connection3).ExecuteNonQuery();
                    connection3.Close();
                    this.sendMessage(this.person2.SelectedValue + "@gsp.net");
                    this.Response.Redirect(this.Request.Url.AbsoluteUri);
                }
                else
                {
                    this.person2Required.Text = "*Required";
                    this.SetFocus((Control)this.save);
                }
                #endregion
            }
            checkcon.Close();


        }

        protected void troopcom_Click(object sender, EventArgs e)
        {
            if (this.person2.SelectedValue == "-1")
            {
                this.person2Required.Text = "*Required";
                this.lowerformdisable();
                this.person2.Enabled = true;
                this.SetFocus((Control)this.person2);
            }
            else
            {
                this.person2Required.Text = "";
                SqlConnection connection = new SqlConnection(this.con1);
                connection.Open();
                new SqlCommand("UPDATE CommInspectionReport SET ReportStage='Awaiting Major''s Approval', ApprovalStatus='" + "Pending" + "' where ReportID = '" + (string)this.Session["RecordID"] + "'", connection).ExecuteNonQuery();
                connection.Close();
                this.sendMessage(this.person2.SelectedValue + "@gsp.net");
                this.Response.Redirect("http://10.0.0.109/inspectionreport/FirstApproval.aspx");
            }
        }

        protected void approve_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(this.con1);
            connection.Open();
            new SqlCommand("UPDATE CommInspectionReport SET ApprovalStatus = 'APPROVED', ReportStage='COMPLETED' where ReportID = '" + (string)this.Session["RecordID"] + "'", connection).ExecuteNonQuery();
            connection.Close();
            this.Response.Redirect("http://10.0.0.109/inspectionreport/FirstApproval.aspx");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            SqlConnection checkcon = new SqlConnection(con1);
            checkcon.Open();
            SqlCommand checkcmd = new SqlCommand("SELECT COUNT(ReportID) FROM CommInspectionReport WHERE ReportID='" + reportid.Text + "'", checkcon);
            int RecordExist = (int)checkcmd.ExecuteScalar();
            if (RecordExist == 1)
            {
                //Record Exist
                GenError.Visible = true;
                upperformdisable();
                lowerformdisable();

            }
            else
            {
                #region GenCode
                datevalue.Enabled = true;
                this.lowerformEnable();
                SqlConnection connection1 = new SqlConnection(this.con1);
                connection1.Open();
                SqlDataReader sqlDataReader1 = new SqlCommand("select * from CommunicationCount", connection1).ExecuteReader();
                while (sqlDataReader1.Read())
                {
                    this.totalcalls_cm.Text = sqlDataReader1["TotalCalls_CM"].ToString();
                    this.totalcalls_lm.Text = sqlDataReader1["TotalCalls_LM"].ToString();
                    this.totalcalls_ytd_cy.Text = sqlDataReader1["TotalCalls_YTD_CY"].ToString();
                    this.totalcalls_smly.Text = sqlDataReader1["TotalCalls_SMLY"].ToString();
                    this.totalcalls_ytd_ly.Text = sqlDataReader1["TotalCalls_YTD_LY"].ToString();
                    this.abandonedveh_cm.Text = sqlDataReader1["AbandonedVeh_CM"].ToString();
                    this.abandonedveh_lm.Text = sqlDataReader1["AbandonedVeh_LM"].ToString();
                    this.abandonedveh_ytd_cy.Text = sqlDataReader1["AbandonedVeh_YTD_CY"].ToString();
                    this.abandonedveh_smly.Text = sqlDataReader1["AbandonedVeh_SMLY"].ToString();
                    this.abandonedveh_ytd_ly.Text = sqlDataReader1["AbandonedVeh_YTD_LY"].ToString();
                    this.chases_cm.Text = sqlDataReader1["Chases_CM"].ToString();
                    this.chases_lm.Text = sqlDataReader1["Chases_LM"].ToString();
                    this.chases_ytd_cy.Text = sqlDataReader1["Chases_YTD_CY"].ToString();
                    this.chases_smly.Text = sqlDataReader1["Chases_SMLY"].ToString();
                    this.chases_ytd_ly.Text = sqlDataReader1["Chases_YTD_LY"].ToString();
                    this.controlledburns_cm.Text = sqlDataReader1["ControlledBurns_CM"].ToString();
                    this.controlledburns_lm.Text = sqlDataReader1["ControlledBurns_LM"].ToString();
                    this.controlledburns_ytd_cy.Text = sqlDataReader1["ControlledBurns_YTD_CY"].ToString();
                    this.controlledburns_smly.Text = sqlDataReader1["ControlledBurns_SMLY"].ToString();
                    this.controlledburns_ytd_ly.Text = sqlDataReader1["ControlledBurns_YTD_LY"].ToString();
                    this.criminaldriverhx_cm.Text = sqlDataReader1["CriminalDriverHX_CM"].ToString();
                    this.criminaldriverhx_lm.Text = sqlDataReader1["CriminalDriverHX_LM"].ToString();
                    this.criminaldriverhx_ytd_cy.Text = sqlDataReader1["CriminalDriverHX_YTD_CY"].ToString();
                    this.criminaldriverhx_smly.Text = sqlDataReader1["CriminalDriverHX_SMLY"].ToString();
                    this.criminaldriverhx_ytd_ly.Text = sqlDataReader1["CriminalDriverHX_YTD_LY"].ToString();
                    this.dnrcalls_cm.Text = sqlDataReader1["DNRCalls_CM"].ToString();
                    this.dnrcalls_lm.Text = sqlDataReader1["DNRCalls_LM"].ToString();
                    this.dnrcalls_ytd_cy.Text = sqlDataReader1["DNRCalls_YTD_CY"].ToString();
                    this.dnrcalls_smly.Text = sqlDataReader1["DNRCalls_SMLY"].ToString();
                    this.dnrcalls_ytd_ly.Text = sqlDataReader1["DNRCalls_YTD_LY"].ToString();
                    this.equipissue_cm.Text = sqlDataReader1["EquipIssue_CM"].ToString();
                    this.equipissue_lm.Text = sqlDataReader1["EquipIssue_LM"].ToString();
                    this.equipissue_ytd_cy.Text = sqlDataReader1["EquipIssue_YTD_CY"].ToString();
                    this.equipissue_smly.Text = sqlDataReader1["EquipIssue_SMLY"].ToString();
                    this.equipissue_ytd_ly.Text = sqlDataReader1["EquipIssue_YTD_LY"].ToString();
                    this.flightplans_cm.Text = sqlDataReader1["FlightPlans_CM"].ToString();
                    this.flightplans_lm.Text = sqlDataReader1["FlightPlans_LM"].ToString();
                    this.flightplans_ytd_cy.Text = sqlDataReader1["FlightPlans_YTD_CY"].ToString();
                    this.flightplans_smly.Text = sqlDataReader1["FlightPlans_SMLY"].ToString();
                    this.flightplans_ytd_ly.Text = sqlDataReader1["FlightPlans_YTD_LY"].ToString();
                    this.gcichits_cm.Text = sqlDataReader1["GCICHits_CM"].ToString();
                    this.gcichits_lm.Text = sqlDataReader1["GCICHits_LM"].ToString();
                    this.gcichits_ytd_cy.Text = sqlDataReader1["GCICHits_YTD_CY"].ToString();
                    this.gcichits_smly.Text = sqlDataReader1["GCICHits_SMLY"].ToString();
                    this.gcichits_ytd_ly.Text = sqlDataReader1["GCICHits_YTD_LY"].ToString();
                    this.motoassist_cm.Text = sqlDataReader1["MototAssist_CM"].ToString();
                    this.motoassist_lm.Text = sqlDataReader1["MototAssist_LM"].ToString();
                    this.motoassist_ytd_cy.Text = sqlDataReader1["MototAssist_YTD_CY"].ToString();
                    this.motoassist_smly.Text = sqlDataReader1["MototAssist_SMLY"].ToString();
                    this.motoassist_ytd_ly.Text = sqlDataReader1["MototAssist_YTD_LY"].ToString();
                    this.openrecord_cm.Text = sqlDataReader1["OpenRecords_CM"].ToString();
                    this.openrecord_lm.Text = sqlDataReader1["OpenRecords_LM"].ToString();
                    this.openrecord_ytd_cy.Text = sqlDataReader1["OpenRecords_YTD_CY"].ToString();
                    this.openrecord_smly.Text = sqlDataReader1["OpenRecords_SMLY"].ToString();
                    this.openrecord_ytd_ly.Text = sqlDataReader1["OpenRecords_YTD_LY"].ToString();
                    this.relays_cm.Text = sqlDataReader1["Relays_CM"].ToString();
                    this.relays_lm.Text = sqlDataReader1["Relays_LM"].ToString();
                    this.relays_ytd_cy.Text = sqlDataReader1["Relays_YTD_CY"].ToString();
                    this.relays_smly.Text = sqlDataReader1["Relays_SMLY"].ToString();
                    this.relays_ytd_ly.Text = sqlDataReader1["Relays_YTD_LY"].ToString();
                    this.roadchecks_cm.Text = sqlDataReader1["RoadChecks_CM"].ToString();
                    this.roadchecks_lm.Text = sqlDataReader1["RoadChecks_LM"].ToString();
                    this.roadchecks_ytd_cy.Text = sqlDataReader1["RoadChecks_YTD_CY"].ToString();
                    this.roadchecks_smly.Text = sqlDataReader1["RoadChecks_SMLY"].ToString();
                    this.roadchecks_ytd_ly.Text = sqlDataReader1["RoadChecks_YTD_LY"].ToString();
                    this.trafficstops_cm.Text = sqlDataReader1["TrafficStops_CM"].ToString();
                    this.trafficstops_lm.Text = sqlDataReader1["TrafficStops_LM"].ToString();
                    this.trafficstops_ytd_cy.Text = sqlDataReader1["TrafficStops_YTD_CY"].ToString();
                    this.trafficstops_smly.Text = sqlDataReader1["TrafficStops_SMLY"].ToString();
                    this.trafficstops_ytd_ly.Text = sqlDataReader1["TrafficStops_YTD_LY"].ToString();
                    this.towedveh_cm.Text = sqlDataReader1["TowedVeh_CM"].ToString();
                    this.towedveh_lm.Text = sqlDataReader1["TowedVeh_LM"].ToString();
                    this.towedveh_ytd_cy.Text = sqlDataReader1["TowedVeh_YTD_CY"].ToString();
                    this.towedveh_smly.Text = sqlDataReader1["TowedVeh_SMLY"].ToString();
                    this.towedveh_ytd_ly.Text = sqlDataReader1["TowedVeh_YTD_LY"].ToString();
                }
                connection1.Close();
                SqlConnection connection2 = new SqlConnection(this.con2);
                connection2.Open();
                SqlDataReader sqlDataReader2 = new SqlCommand("SELECT * FROM CommInspectionPersonnelCount", connection2).ExecuteReader();
                while (sqlDataReader2.Read())
                {
                    this.totalcalls_cm2.Text = sqlDataReader2["Chief_CM"].ToString();
                    this.totalcalls_smly2.Text = sqlDataReader2["Chief_SMLY"].ToString();
                    this.abandonedveh_cm2.Text = sqlDataReader2["Seniors_CM"].ToString();
                    this.abandonedveh_smly2.Text = sqlDataReader2["Seniors_SMLY"].ToString();
                    this.chases_cm2.Text = sqlDataReader2["Dispatcher2_CM"].ToString();
                    this.chases_smly2.Text = sqlDataReader2["Dispatcher2_SMLY"].ToString();
                    this.controlledburns_cm2.Text = sqlDataReader2["Dispathcer1_CM"].ToString();
                    this.controlledburns_smly2.Text = sqlDataReader2["Dispatcher1_SMLY"].ToString();
                    this.criminaldriverhx_smly2.Text = sqlDataReader2["GCICCertification"].ToString();
                    this.gcichits_wantedperson.Text = sqlDataReader2["GCIC_Wanted"].ToString();
                    this.motoassist_stolevehicle.Text = sqlDataReader2["GCI_StolenVeh"].ToString();
                    this.openrecord_stoleguns.Text = sqlDataReader2["GCI_StolenGun"].ToString();
                    this.relays_other.Text = sqlDataReader2["GCIC_Other"].ToString();
                }
                connection2.Close();
                this.SetFocus((Control)this.save);
                #endregion
            }
            checkcon.Close();
        }

        protected void reject_Click(object sender, EventArgs e)
        {
            string user = User.Identity.Name;
            string mainuser = user.Substring(6);
            this.officer.Visible = false;
            this.person2.Visible = false;
            this.person2Required.Visible = false;
            this.reason.Visible = true;
            this.rejectrequired.Visible = true;
            this.Label4.Visible = true;
            this.troopcom.Enabled = false;
            this.approve.Enabled = false;
            lowerformdisable();
            SetFocus(reason);

            if (reason.Text == "")
            {
                formValidate();
                upperformdisable();
                SetFocus(reason);
            }
            else
            {
                #region FetchInfo
                SqlConnection rejectcon = new SqlConnection(con1);
                rejectcon.Open();
                SqlCommand rejectcmd = new SqlCommand("SELECT * FROM CommInspectionReport WHERE ReportID='" + Session["RecordID"] + "'", rejectcon);
                SqlDataReader rejectreader = rejectcmd.ExecuteReader();
                while (rejectreader.Read())
                {
                    rejectCO = rejectreader["CreatedBy"].ToString(); // This is use to get the Chief Operation and Send Email ---CreatedBy
                    rejectTO = rejectreader["ModifiedBy"].ToString(); // This is use to get the Troop Officer and Send Email   ---ModifiedBy
                    rejectTC = rejectreader["ApprovalOfficer"].ToString(); // This is use to get tehe Troop Commander and send Email ---ApprovalOfficer
                }
                rejectcon.Close();
                #endregion

                SqlConnection connection2 = new SqlConnection(realidentity);
                connection2.Open();
                SqlDataReader sqlDataReader2 = new SqlCommand("SELECT EmployeeJobCode,EmployeePostionNUmber FROM ViewInspection WHERE sAMAccountName='" + mainuser + "'", connection2).ExecuteReader();
                while (sqlDataReader2.Read())
                {
                    string str2 = sqlDataReader2["EmployeeJobCode"].ToString();
                    string Pstr = sqlDataReader2["EmployeePostionNUmber"].ToString();

                    if (str2 == "PSM021") //Troop Officer to Chief Operator
                    {
                        #region MessageTo_C.O
                        SqlConnection emailCOcon = new SqlConnection(con);
                        emailCOcon.Open();
                        SqlCommand emailCOcmd = new SqlCommand("SELECT * FROM ViewInspection WHERE EmployeeID = '" + rejectCO + "'", emailCOcon);
                        SqlDataReader emailCOredaer = emailCOcmd.ExecuteReader();
                        while (emailCOredaer.Read())
                        {
                            string UserCO = emailCOredaer["sAMAccountName"].ToString() + "@gsp.net";
                            rejectMessage(UserCO);
                            lowerformdisable();
                            RejectedUpdate();
                            this.Response.Redirect("http://10.0.0.109/inspectionreport/FirstApproval.aspx");
                        }
                        emailCOcon.Close();
                        #endregion
                    }
                    else if (str2 == "PSM022") //Troop Commander to Chief Operator and Troop Officer
                    {
                        #region MessageTo_C.O
                        SqlConnection emailCOcon = new SqlConnection(con);
                        emailCOcon.Open();
                        SqlCommand emailCOcmd = new SqlCommand("SELECT * FROM ViewInspection WHERE EmployeeID = '" + rejectCO + "'", emailCOcon);
                        SqlDataReader emailCOredaer = emailCOcmd.ExecuteReader();
                        while (emailCOredaer.Read())
                        {
                            string UserCO = emailCOredaer["sAMAccountName"].ToString() + "@gsp.net";
                            rejectMessage(UserCO);
                        }
                        emailCOcon.Close();
                        #endregion

                        #region MessageTo_T.O
                        SqlConnection emailTOcon = new SqlConnection(con);
                        emailTOcon.Open();
                        SqlCommand emailTOcmd = new SqlCommand("SELECT * FROM ViewInspection WHERE EmployeeID = '" + rejectTO + "'", emailTOcon);
                        SqlDataReader emailTOreader = emailTOcmd.ExecuteReader();
                        while (emailTOreader.Read())
                        {
                            string UserTO = emailTOreader["sAMAccountName"].ToString() + "@gsp.net";
                            rejectMessage(UserTO);
                            lowerformdisable();
                            RejectedUpdate();
                            this.Response.Redirect("http://10.0.0.109/inspectionreport/FirstApproval.aspx");
                        }
                        emailTOcon.Close();
                        #endregion
                    }
                    else if (Pstr == "00105296") //Major to Chief Operator, Troop Officer and Troop Commander
                    {
                        //Send message to the Chief Operator- Identified by CreatedBy--Checked Under the EmployeeID Column -- DB=ViewInspection
                        #region MessageTo_C.O
                        SqlConnection emailCOcon = new SqlConnection(con);
                        emailCOcon.Open();
                        SqlCommand emailCOcmd = new SqlCommand("SELECT * FROM ViewInspection WHERE EmployeeID = '" + rejectCO + "'", emailCOcon);
                        SqlDataReader emailCOredaer = emailCOcmd.ExecuteReader();
                        while (emailCOredaer.Read())
                        {
                            string UserCO = emailCOredaer["sAMAccountName"].ToString() + "@gsp.net";
                            rejectMessage(UserCO);
                        }
                        emailCOcon.Close();
                        #endregion

                        //Send message to the Chief Operator- Identified by ModifiedBy--Checked Under the EmployeeID Column -- DB=ViewInspection
                        #region MessageTo_T.O
                        SqlConnection emailTOcon = new SqlConnection(con);
                        emailTOcon.Open();
                        SqlCommand emailTOcmd = new SqlCommand("SELECT * FROM ViewInspection WHERE EmployeeID = '" + rejectTO + "'", emailTOcon);
                        SqlDataReader emailTOreader = emailTOcmd.ExecuteReader();
                        while (emailTOreader.Read())
                        {
                            string UserTO = emailTOreader["sAMAccountName"].ToString() + "@gsp.net";
                            rejectMessage(UserTO);
                        }
                        emailTOcon.Close();
                        #endregion

                        //Send message to the Chief Operator- Identified by ApprovalOfficer--Checked Under the EmployeeName Column -- DB=ViewInspection
                        #region MessageTo_T.C
                        SqlConnection emailTCcon = new SqlConnection(con);
                        emailTCcon.Open();
                        SqlCommand emailTCcmd = new SqlCommand("SELECT * FROM ViewInspection WHERE EmployeeName = '" + rejectTC + "'", emailTCcon);
                        SqlDataReader emailTCreader = emailTCcmd.ExecuteReader();
                        while (emailTCreader.Read())
                        {
                            string UserTC = emailTCreader["sAMAccountName"].ToString() + "@gsp.net";
                            rejectMessage(UserTC);
                            lowerformdisable();
                            RejectedUpdate();
                            this.Response.Redirect("http://10.0.0.109/inspectionreport/FirstApproval.aspx");
                        }
                        emailTCcon.Close();
                        #endregion
                    }
                }
                connection2.Close();
            }
        }

        private void RejectedUpdate()
        {
            string user = User.Identity.Name;
            string mainuser = user.Substring(6);
            String reasonremark = reason.Text;
            String mainreson = reasonremark.Replace("'", "''");

            SqlConnection connection2 = new SqlConnection(realidentity);
            connection2.Open();
            SqlDataReader sqlDataReader2 = new SqlCommand("SELECT EmployeeJobCode,EmployeePostionNUmber FROM ViewInspection WHERE sAMAccountName='" + mainuser + "'", connection2).ExecuteReader();
            while (sqlDataReader2.Read())
            {
                string str2 = sqlDataReader2["EmployeeJobCode"].ToString();
                string Pstr = sqlDataReader2["EmployeePostionNUmber"].ToString();

                if (str2 == "PSM021") //Troop Officer
                {
                    SqlConnection rejectcon = new SqlConnection(con1);
                    rejectcon.Open();
                    SqlCommand rejectcmd = new SqlCommand("UPDATE CommInspectionReport SET Remarks='" + mainreson + "', ApprovalStatus='" + "Rejected - By T.O" + "',ReportStage='" + "Awaiting Correction" + "' WHERE ReportID = '" + Session["RecordID"] + "'", rejectcon);
                    rejectcmd.ExecuteNonQuery();
                    rejectcon.Close();
                }
                else if (str2 == "PSM022")
                {
                    SqlConnection rejectcon = new SqlConnection(con1);
                    rejectcon.Open();
                    SqlCommand rejectcmd = new SqlCommand("UPDATE CommInspectionReport SET Remarks='" + mainreson + "', ApprovalStatus='" + "Rejected - ByT.C" + "',ReportStage='" + "Awaiting Correction" + "' WHERE ReportID = '" + Session["RecordID"] + "'", rejectcon);
                    rejectcmd.ExecuteNonQuery();
                    rejectcon.Close();
                }
                else if (Pstr == "00105296")
                {
                    SqlConnection rejectcon = new SqlConnection(con1);
                    rejectcon.Open();
                    SqlCommand rejectcmd = new SqlCommand("UPDATE CommInspectionReport SET Remarks='" + mainreson + "', ApprovalStatus='" + "Rejected - By MAJOR" + "',ReportStage='" + "Awaiting Correction" + "' WHERE ReportID = '" + Session["RecordID"] + "'", rejectcon);
                    rejectcmd.ExecuteNonQuery();
                    rejectcon.Close();
                }
            }
            connection2.Close();


        }

        private void rejectMessage(string strEmailTo)
        {
            string str = "10.0.0.14";
            string from = "donotreply@gsp.net";
            string messageText = "Inspection Report Rejection Test E-Mail\nPlease click the link below:\n\nhttp://10.0.0.109/inspectionreport/FirstApproval.aspx";
            string subject = "Inspection_Report_Reject_Email_Test";
            SmtpMail.SmtpServer = str;
            SmtpMail.Send(from, strEmailTo, subject, messageText);
        }

    }
}