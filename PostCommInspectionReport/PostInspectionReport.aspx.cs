using System;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PostCommInspectionReport.Properties;
using PostCommInspectionReport.Utilis;
using System.Web.Mail;
using System.Windows.Forms;

namespace PostCommInspectionReport
{
    public partial class PostInspectionReport : Page
    {
        #region Private Properties

        readonly string _dbConn = Settings.Default.userConnect;
        readonly string _spConnect = Settings.Default.spConnect;
        private string employeeCode = string.Empty;

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var employeePostionNUmber = string.Empty;
                string currentUser = User.Identity.Name.Substring(6);
                var mycon = new SqlConnection(_dbConn);
                var cmd = new SqlCommand(
                    //@"select * from ViewInspection where sAMAccountName='jstultz'",
                      @"select * from ViewInspection where sAMAccountName='" + currentUser + "'",
                    mycon);
                mycon.Open();
                var read = cmd.ExecuteReader();
                while (read.Read())
                {
                    txtTroop.Text =
                        read["parentUnit"].ToString(); // Troop = parentUnit in the database                    
                    employeeCode = read["EmployeeJobCode"].ToString();
                    employeePostionNUmber = read["EmployeePostionNUmber"].ToString();
                }
                mycon.Close();
                //Check if report is complete, if true--disable entire report

                switch (employeeCode.Trim())
                {
                    case "GST051":
                        PostReport_PostSec();
                        break;
                    case "PSM020":
                        PostReport_PostCMDR();
                        break;
                    case "PSM021":
                        PostReport_TroopOfficer();
                        break;
                    case "PSM022":
                        PostReport_TroopCMDR();
                        break;
                    default:
                        if (employeePostionNUmber.Trim() == "00105296")
                        {
                            PostReport_Major();
                        }
                        else
                        {
                            Response.Redirect("UnauthorizedUser.aspx");
                        }
                        break;
                }
            }
        }

        protected void btnTableGenereate_Click(object sender, EventArgs e)
        {

            if (txtDate.Text != "")
            {
                PopulateTextBoxes();
                DisableTextTable(true);
                var inpsectionDay = Convert.ToDateTime(txtDate.Text).Day;
                var inspectionMonth = Convert.ToDateTime(txtDate.Text).Month;
                var inspectionYear = Convert.ToDateTime(txtDate.Text).Year;
                var postNumber = new string(ddlPost.SelectedItem.Text.Where(char.IsNumber).ToArray());
                if (lblReportId.Text == string.Empty)
                {
                    lblReportId.Text = "P" + "-" + postNumber + inpsectionDay + inspectionMonth +
                                       inspectionYear;
                }
            }
        }

        private void PopulateTextBoxes()
        {
            SqlConnection mycon = new SqlConnection(_spConnect);
            mycon.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM PostInspectionCount", mycon);
            SqlDataReader dataReader = command.ExecuteReader();

            #region DataRead

            while (dataReader.Read())
            {
                //Crashes
                string crashCm = dataReader["Crash_CM"].ToString();
                txtbxCrashes_CM.Text = crashCm;
                string crashAl = dataReader["Crash_AL"].ToString();
                txtbxCrashes_AL.Text = crashAl;
                string crashSmly = dataReader["Crash_SMLY"].ToString();
                txtbxCrashes_SMLY.Text = crashSmly;
                string crashYtd = dataReader["Crashes_YTD"].ToString();
                txtbxCrashes_YTD.Text = crashYtd;
                string crashYtdLy = dataReader["Crashes_YTD_LY"].ToString();
                txtbxCrashes_YTD_LY.Text = crashYtdLy;
                //PercentageChange
                txtCrashesChange.Text = CalculateChange(Convert.ToInt64(crashYtdLy), Convert.ToInt64(crashYtd))
                    .ToString("F");

                //Fatalities
                string fatalitiesCm = dataReader["Fatalities_CM"].ToString();
                txtbxFatalities_CM.Text = fatalitiesCm;
                string fatalitiesAl = dataReader["Fatalities_AL"].ToString();
                txtbxFatalities_AL.Text = fatalitiesAl;
                string fatalitiesSmly = dataReader["Fatalities_SMLY"].ToString();
                txtbxFatalities_SMLY.Text = fatalitiesSmly;
                string fatalitiesYtd = dataReader["fatalities_YTD"].ToString();
                txtbxFatalities_YTD.Text = fatalitiesYtd;
                string fatalitiesYtdLy = dataReader["fatalities_YTD_LY"].ToString();
                txtbxFatalities_YTD_LY.Text = fatalitiesYtdLy;
                txtbxFatalitiesChange.Text =
                    CalculateChange(Convert.ToInt64(fatalitiesYtdLy), Convert.ToInt64(fatalitiesYtd))
                        .ToString("F");

                //Arrest
                string arrestCm = dataReader["Arrest_CM"].ToString();
                txtbxArrests_CM.Text = arrestCm;
                string arrestAl = dataReader["Arrest_AL"].ToString();
                txtbxArrests_AL.Text = arrestAl;
                string arrestSmly = dataReader["Arrest_SMLY"].ToString();
                txtbxArrests_SMLY.Text = arrestSmly;
                string arrestYtd = dataReader["Arrest_YTD"].ToString();
                txtbxArrests_YTD.Text = arrestYtd;
                string arrestYtdLy = dataReader["Arrest_YTD_LY"].ToString();
                txtbxArrests_YTD_LY.Text = arrestYtdLy;
                //PercentageChange
                txtArrestChange.Text = CalculateChange(Convert.ToInt64(arrestYtd), Convert.ToInt64(arrestYtdLy))
                    .ToString("F");

                //Warnings
                string warningsCm = dataReader["Warnings_CM"].ToString();
                txtbxWarnings_CM.Text = warningsCm;
                string warningsAl = dataReader["Warnings_AL"].ToString();
                txtbxWarnings_AL.Text = warningsAl;
                string warningsSmly = dataReader["Warnings_SMLY"].ToString();
                txtbxWarnings_SMLY.Text = warningsSmly;
                string warningsYtd = dataReader["Warnings_YTD"].ToString();
                txtbxWarnings_YTD.Text = warningsYtd;
                string warningsYtdLy = dataReader["Warnings_YTD_LY"].ToString();
                txtbxWarnings_YTD_LY.Text = warningsYtdLy;
                //PercentageChange
                txtWarningsChange.Text = CalculateChange(Convert.ToInt64(warningsYtdLy), Convert.ToInt64(warningsYtd))
                    .ToString("F");

                //DuiArrest
                string duiArrestCm = dataReader["DUIArrest_CM"].ToString();
                txtbxDUIArrest_CM.Text = duiArrestCm;
                string duiArrestAl = dataReader["DUIArrest_AL"].ToString();
                txtbxDUIArrest_AL.Text = duiArrestAl;
                string duiArrestSmly = dataReader["DUIArrest_SMLY"].ToString();
                txtbxDUIArrest_SMLY.Text = duiArrestSmly;
                string duiArrestYtd = dataReader["DUIArrest_YTD"].ToString();
                txtbxDUIArrest_YTD.Text = duiArrestYtd;
                string duiArrestYtdLy = dataReader["DUIArrest_YTD_LY"].ToString();
                txtbxDUIArrest_YTD_LY.Text = duiArrestYtdLy;
                //PercentageChange
                txtDUIArrestChange.Text =
                    CalculateChange(Convert.ToInt64(duiArrestYtdLy), Convert.ToInt64(duiArrestYtd)).ToString("F");

                //VehicleStops
                string vehicleCm = dataReader["VehicleStops_CM"].ToString();
                txtbxVechileStops_CM.Text = vehicleCm;
                string vehicleAl = dataReader["VehicleStops_AL"].ToString();
                txtbxVechileStops_AL.Text = vehicleAl;
                string vehicleSmly = dataReader["VehicleStops_SMLY"].ToString();
                txtbxVechileStops_SMLY.Text = vehicleSmly;
                string vehicleYtd = dataReader["VehicleStops_YTD"].ToString();
                txtbxVechileStops_YTD.Text = vehicleYtd;
                string vehicleYtdLy = dataReader["VehicleStops_YTD_LY"].ToString();
                txtbxVechileStops_YTD_LY.Text = vehicleYtdLy;
                //PercentageChange
                txtVechileChange.Text = CalculateChange(Convert.ToInt64(vehicleYtdLy), Convert.ToInt64(vehicleYtd))
                    .ToString("F");

                //Sergeants Assigned
                string sergeantsAssigned_CM = dataReader["SergeantsAssigned_CM"].ToString();
                txtbxSergeantsAssigned_CM.Text = sergeantsAssigned_CM;
                string sergeantsAssignedSmly = dataReader["SergeantsAssigned_SMLY"].ToString();
                txtbxSergeantsAssigned_SMLY.Text = sergeantsAssignedSmly;
                //Corporals Assigned
                string corporalsAssigned_CM = dataReader["CorporalsAssigned_CM"].ToString();
                txtbxCorporalsAssigned_CM.Text = corporalsAssigned_CM;
                string corporalsAssignedSmly = dataReader["CorporalsAssigned_SMLY"].ToString();
                txtbxCorporalsAssigned_SMLY.Text = corporalsAssignedSmly;
                //Troopers Assigned
                string troppersAssigned_CM = dataReader["TroopersAssigned_CM"].ToString();
                txtbxTroopersAssigned_CM.Text = troppersAssigned_CM;
                string troopersAssignedSmly = dataReader["TroopersAssigned_SMLY"].ToString();
                txtbxTroopersAssigned_SMLY.Text = troopersAssignedSmly;
                //[Secretary Assigned
                string secretaryAssigned_CM = dataReader["SecretaryAssigned_CM"].ToString();
                txtbxSecretaryAssigned_CM.Text = secretaryAssigned_CM;
                string secretaryAssignedSmly = dataReader["SecretaryAssigned_SMLY"].ToString();
                txtbxSecretaryAssigned_SMLY.Text = secretaryAssignedSmly;
                //Leave
                string leave_CM = dataReader["Leave_CM"].ToString();
                txtbxLeave_CM.Text = leave_CM;
                string leaveSmly = dataReader["Leave_SMLY"].ToString();
                txtbxLeave_SMLY.Text = leaveSmly;
                //Detached
                string detached_CM = dataReader["Detached_CM"].ToString();
                txtbxDetached_CM.Text = detached_CM;
                string detachedSmly = dataReader["Detached_SMLY"].ToString();
                txtbxDetached_SMLY.Text = detachedSmly;
            }
            mycon.Close();

            #endregion
        }

        private void PopulateTextBoxesWithReportDataPostSec()
        {
            SqlConnection mycon = new SqlConnection(_spConnect);
            mycon.Open();
            SqlCommand command =
                new SqlCommand(
                    "SELECT * FROM PostInspectionReport Where ReportId ='" + (string)Session["ReportId"] +
                    "'", mycon);
            SqlDataReader dataReader = command.ExecuteReader();

            #region DataRead

            while (dataReader.Read())
            {
                txtPost.Text = dataReader["Post"].ToString();
                txtCity.Text = dataReader["City"].ToString();
                txtDate.Text = dataReader["Created"].ToString();
                //Crashes
                string crashCm = dataReader["Crash_CM"].ToString();
                txtbxCrashes_CM.Text = crashCm;
                string crashAl = dataReader["Crash_AL"].ToString();
                txtbxCrashes_AL.Text = crashAl;
                string crashSmly = dataReader["Crash_SMLY"].ToString();
                txtbxCrashes_SMLY.Text = crashSmly;
                string crashYtd = dataReader["Crashes_YTD"].ToString();
                txtbxCrashes_YTD.Text = crashYtd;
                string crashYtdLy = dataReader["Crashes_YTD_LY"].ToString();
                txtbxCrashes_YTD_LY.Text = crashYtdLy;
                //PercentageChange
                txtCrashesChange.Text = CalculateChange(Convert.ToInt64(crashYtdLy), Convert.ToInt64(crashYtd))
                    .ToString("F");

                //Fatalities
                string fatalitiesCm = dataReader["Fatalities_CM"].ToString();
                txtbxFatalities_CM.Text = fatalitiesCm;
                string fatalitiesAl = dataReader["Fatalities_AL"].ToString();
                txtbxFatalities_AL.Text = fatalitiesAl;
                string fatalitiesSmly = dataReader["Fatalities_SMLY"].ToString();
                txtbxFatalities_SMLY.Text = fatalitiesSmly;
                string fatalitiesYtd = dataReader["fatalities_YTD"].ToString();
                txtbxFatalities_YTD.Text = fatalitiesYtd;
                string fatalitiesYtdLy = dataReader["fatalities_YTD_LY"].ToString();
                txtbxFatalities_YTD_LY.Text = fatalitiesYtdLy;

                //Arrest
                string arrestCm = dataReader["Arrest_CM"].ToString();
                txtbxArrests_CM.Text = arrestCm;
                string arrestAl = dataReader["Arrest_AL"].ToString();
                txtbxArrests_AL.Text = arrestAl;
                string arrestSmly = dataReader["Arrest_SMLY"].ToString();
                txtbxArrests_SMLY.Text = arrestSmly;
                string arrestYtd = dataReader["Arrest_YTD"].ToString();
                txtbxArrests_YTD.Text = arrestYtd;
                string arrestYtdLy = dataReader["Arrest_YTD_LY"].ToString();
                txtbxArrests_YTD_LY.Text = arrestYtdLy;
                //PercentageChange
                txtArrestChange.Text = CalculateChange(Convert.ToInt64(arrestYtd), Convert.ToInt64(arrestYtdLy))
                    .ToString("F");

                //Warnings
                string warningsCm = dataReader["Warnings_CM"].ToString();
                txtbxWarnings_CM.Text = warningsCm;
                string warningsAl = dataReader["Warnings_AL"].ToString();
                txtbxWarnings_AL.Text = warningsAl;
                string warningsSmly = dataReader["Warnings_SMLY"].ToString();
                txtbxWarnings_SMLY.Text = warningsSmly;
                string warningsYtd = dataReader["Warnings_YTD"].ToString();
                txtbxWarnings_YTD.Text = warningsYtd;
                string warningsYtdLy = dataReader["Warnings_YTD_LY"].ToString();
                txtbxWarnings_YTD_LY.Text = warningsYtdLy;
                //PercentageChange
                txtWarningsChange.Text = CalculateChange(Convert.ToInt64(warningsYtdLy), Convert.ToInt64(warningsYtd))
                    .ToString("F");

                //DuiArrest
                string duiArrestCm = dataReader["DUIArrest_CM"].ToString();
                txtbxDUIArrest_CM.Text = duiArrestCm;
                string duiArrestAl = dataReader["DUIArrest_AL"].ToString();
                txtbxDUIArrest_AL.Text = duiArrestAl;
                string duiArrestSmly = dataReader["DUIArrest_SMLY"].ToString();
                txtbxDUIArrest_SMLY.Text = duiArrestSmly;
                string duiArrestYtd = dataReader["DUIArrest_YTD"].ToString();
                txtbxDUIArrest_YTD.Text = duiArrestYtd;
                string duiArrestYtdLy = dataReader["DUIArrest_YTD_LY"].ToString();
                txtbxDUIArrest_YTD_LY.Text = duiArrestYtdLy;
                //PercentageChange
                txtDUIArrestChange.Text =
                    CalculateChange(Convert.ToInt64(duiArrestYtdLy), Convert.ToInt64(duiArrestYtd)).ToString("F");

                //VehicleStops
                string vehicleCm = dataReader["VehicleStops_CM"].ToString();
                txtbxVechileStops_CM.Text = vehicleCm;
                string vehicleAl = dataReader["VehicleStops_AL"].ToString();
                txtbxVechileStops_AL.Text = vehicleAl;
                string vehicleSmly = dataReader["VehicleStops_SMLY"].ToString();
                txtbxVechileStops_SMLY.Text = vehicleSmly;
                string vehicleYtd = dataReader["VehicleStops_YTD"].ToString();
                txtbxVechileStops_YTD.Text = vehicleYtd;
                string vehicleYtdLy = dataReader["VehicleStops_YTD_LY"].ToString();
                txtbxVechileStops_YTD_LY.Text = vehicleYtdLy;
                //PercentageChange
                txtVechileChange.Text = CalculateChange(Convert.ToInt64(vehicleYtdLy), Convert.ToInt64(vehicleYtd))
                    .ToString("F");

                //Sergeants Assigned
                string sergeantsAssigned_CM = dataReader["SergeantsAssigned_CM"].ToString();
                txtbxSergeantsAssigned_CM.Text = sergeantsAssigned_CM;
                string sergeantsAssignedSmly = dataReader["SergeantsAssigned_SMLY"].ToString();
                txtbxSergeantsAssigned_SMLY.Text = sergeantsAssignedSmly;
                //Corporals Assigned
                string corporalsAssigned_CM = dataReader["CorporalsAssigned_CM"].ToString();
                txtbxCorporalsAssigned_CM.Text = corporalsAssigned_CM;
                string corporalsAssignedSmly = dataReader["CorporalsAssigned_SMLY"].ToString();
                txtbxCorporalsAssigned_SMLY.Text = corporalsAssignedSmly;
                //Troopers Assigned
                string troppersAssigned_CM = dataReader["TroopersAssigned_CM"].ToString();
                txtbxTroopersAssigned_CM.Text = troppersAssigned_CM;
                string troopersAssignedSmly = dataReader["TroopersAssigned_SMLY"].ToString();
                txtbxTroopersAssigned_SMLY.Text = troopersAssignedSmly;
                //[Secretary Assigned
                string secretaryAssigned_CM = dataReader["SecretaryAssigned_CM"].ToString();
                txtbxSecretaryAssigned_CM.Text = secretaryAssigned_CM;
                string secretaryAssignedSmly = dataReader["SecretaryAssigned_SMLY"].ToString();
                txtbxSecretaryAssigned_SMLY.Text = secretaryAssignedSmly;
                //Leave
                string leave_CM = dataReader["Leave_CM"].ToString();
                txtbxLeave_CM.Text = leave_CM;
                string leaveSmly = dataReader["Leave_SMLY"].ToString();
                txtbxLeave_SMLY.Text = leaveSmly;
                //Detached
                string detached_CM = dataReader["Detached_CM"].ToString();
                txtbxDetached_CM.Text = detached_CM;
                string detachedSmly = dataReader["Detached_SMLY"].ToString();
                txtbxDetached_SMLY.Text = detachedSmly;

                string remarks = dataReader["GeneralRemarks"].ToString();
                txtbxGeneralRemarks.Text = remarks;

                string personnelPresent = dataReader["PersonnelPresent"].ToString();
                tbxPersonnelPresent.Text = personnelPresent;

                string vehiclePresent = dataReader["VechPresent"].ToString();
                txtbxVechilesInspected.Text = vehiclePresent;
            }
            mycon.Close();

            #endregion

        }

        private void PopulateTextBoxesWithReportData()
        {
            SqlConnection mycon = new SqlConnection(_spConnect);
            mycon.Open();
            SqlCommand command =
                new SqlCommand("SELECT * FROM PostInspectionReport Where ReportId ='" + lblReportId.Text + "'", mycon);
            SqlDataReader dataReader = command.ExecuteReader();

            #region DataRead

            while (dataReader.Read())
            {
                txtPost.Text = dataReader["Post"].ToString();
                txtCity.Text = dataReader["City"].ToString();
                //Crashes
                string crashCm = dataReader["Crash_CM"].ToString();
                txtbxCrashes_CM.Text = crashCm;
                string crashAl = dataReader["Crash_AL"].ToString();
                txtbxCrashes_AL.Text = crashAl;
                string crashSmly = dataReader["Crash_SMLY"].ToString();
                txtbxCrashes_SMLY.Text = crashSmly;
                string crashYtd = dataReader["Crashes_YTD"].ToString();
                txtbxCrashes_YTD.Text = crashYtd;
                string crashYtdLy = dataReader["Crashes_YTD_LY"].ToString();
                txtbxCrashes_YTD_LY.Text = crashYtdLy;
                //PercentageChange
                txtCrashesChange.Text = CalculateChange(Convert.ToInt64(crashYtdLy), Convert.ToInt64(crashYtd))
                    .ToString("F");

                //Fatalities
                string fatalitiesCm = dataReader["Fatalities_CM"].ToString();
                txtbxFatalities_CM.Text = fatalitiesCm;
                string fatalitiesAl = dataReader["Fatalities_AL"].ToString();
                txtbxFatalities_AL.Text = fatalitiesAl;
                string fatalitiesSmly = dataReader["Fatalities_SMLY"].ToString();
                txtbxFatalities_SMLY.Text = fatalitiesSmly;
                string fatalitiesYtd = dataReader["fatalities_YTD"].ToString();
                txtbxFatalities_YTD.Text = fatalitiesYtd;
                string fatalitiesYtdLy = dataReader["fatalities_YTD_LY"].ToString();
                txtbxFatalities_YTD_LY.Text = fatalitiesYtdLy;

                //Arrest
                string arrestCm = dataReader["Arrest_CM"].ToString();
                txtbxArrests_CM.Text = arrestCm;
                string arrestAl = dataReader["Arrest_AL"].ToString();
                txtbxArrests_AL.Text = arrestAl;
                string arrestSmly = dataReader["Arrest_SMLY"].ToString();
                txtbxArrests_SMLY.Text = arrestSmly;
                string arrestYtd = dataReader["Arrest_YTD"].ToString();
                txtbxArrests_YTD.Text = arrestYtd;
                string arrestYtdLy = dataReader["Arrest_YTD_LY"].ToString();
                txtbxArrests_YTD_LY.Text = arrestYtdLy;
                //PercentageChange
                txtArrestChange.Text = CalculateChange(Convert.ToInt64(arrestYtd), Convert.ToInt64(arrestYtdLy))
                    .ToString("F");

                //Warnings
                string warningsCm = dataReader["Warnings_CM"].ToString();
                txtbxWarnings_CM.Text = warningsCm;
                string warningsAl = dataReader["Warnings_AL"].ToString();
                txtbxWarnings_AL.Text = warningsAl;
                string warningsSmly = dataReader["Warnings_SMLY"].ToString();
                txtbxWarnings_SMLY.Text = warningsSmly;
                string warningsYtd = dataReader["Warnings_YTD"].ToString();
                txtbxWarnings_YTD.Text = warningsYtd;
                string warningsYtdLy = dataReader["Warnings_YTD_LY"].ToString();
                txtbxWarnings_YTD_LY.Text = warningsYtdLy;
                //PercentageChange
                txtWarningsChange.Text = CalculateChange(Convert.ToInt64(warningsYtdLy), Convert.ToInt64(warningsYtd))
                    .ToString("F");

                //DuiArrest
                string duiArrestCm = dataReader["DUIArrest_CM"].ToString();
                txtbxDUIArrest_CM.Text = duiArrestCm;
                string duiArrestAl = dataReader["DUIArrest_AL"].ToString();
                txtbxDUIArrest_AL.Text = duiArrestAl;
                string duiArrestSmly = dataReader["DUIArrest_SMLY"].ToString();
                txtbxDUIArrest_SMLY.Text = duiArrestSmly;
                string duiArrestYtd = dataReader["DUIArrest_YTD"].ToString();
                txtbxDUIArrest_YTD.Text = duiArrestYtd;
                string duiArrestYtdLy = dataReader["DUIArrest_YTD_LY"].ToString();
                txtbxDUIArrest_YTD_LY.Text = duiArrestYtdLy;
                //PercentageChange
                txtDUIArrestChange.Text =
                    CalculateChange(Convert.ToInt64(duiArrestYtdLy), Convert.ToInt64(duiArrestYtd)).ToString("F");

                //VehicleStops
                string vehicleCm = dataReader["VehicleStops_CM"].ToString();
                txtbxVechileStops_CM.Text = vehicleCm;
                string vehicleAl = dataReader["VehicleStops_AL"].ToString();
                txtbxVechileStops_AL.Text = vehicleAl;
                string vehicleSmly = dataReader["VehicleStops_SMLY"].ToString();
                txtbxVechileStops_SMLY.Text = vehicleSmly;
                string vehicleYtd = dataReader["VehicleStops_YTD"].ToString();
                txtbxVechileStops_YTD.Text = vehicleYtd;
                string vehicleYtdLy = dataReader["VehicleStops_YTD_LY"].ToString();
                txtbxVechileStops_YTD_LY.Text = vehicleYtdLy;
                //PercentageChange
                txtVechileChange.Text = CalculateChange(Convert.ToInt64(vehicleYtdLy), Convert.ToInt64(vehicleYtd))
                    .ToString("F");

                //Sergeants Assigned
                string sergeantsAssigned_CM = dataReader["SergeantsAssigned_CM"].ToString();
                txtbxSergeantsAssigned_CM.Text = sergeantsAssigned_CM;
                string sergeantsAssignedSmly = dataReader["SergeantsAssigned_SMLY"].ToString();
                txtbxSergeantsAssigned_SMLY.Text = sergeantsAssignedSmly;
                //Corporals Assigned
                string corporalsAssigned_CM = dataReader["CorporalsAssigned_CM"].ToString();
                txtbxCorporalsAssigned_CM.Text = corporalsAssigned_CM;
                string corporalsAssignedSmly = dataReader["CorporalsAssigned_SMLY"].ToString();
                txtbxCorporalsAssigned_SMLY.Text = corporalsAssignedSmly;
                //Troopers Assigned
                string troppersAssigned_CM = dataReader["TroopersAssigned_CM"].ToString();
                txtbxTroopersAssigned_CM.Text = troppersAssigned_CM;
                string troopersAssignedSmly = dataReader["TroopersAssigned_SMLY"].ToString();
                txtbxTroopersAssigned_SMLY.Text = troopersAssignedSmly;
                //[Secretary Assigned
                string secretaryAssigned_CM = dataReader["SecretaryAssigned_CM"].ToString();
                txtbxSecretaryAssigned_CM.Text = secretaryAssigned_CM;
                string secretaryAssignedSmly = dataReader["SecretaryAssigned_SMLY"].ToString();
                txtbxSecretaryAssigned_SMLY.Text = secretaryAssignedSmly;
                //Leave
                string leave_CM = dataReader["Leave_CM"].ToString();
                txtbxLeave_CM.Text = leave_CM;
                string leaveSmly = dataReader["Leave_SMLY"].ToString();
                txtbxLeave_SMLY.Text = leaveSmly;
                //Detached
                string detached_CM = dataReader["Detached_CM"].ToString();
                txtbxDetached_CM.Text = detached_CM;
                string detachedSmly = dataReader["Detached_SMLY"].ToString();
                txtbxDetached_SMLY.Text = detachedSmly;

                string personnelPresent = dataReader["PersonnelPresent"].ToString();
                tbxPersonnelPresent.Text = personnelPresent;

                string vehiclePresent = dataReader["VechPresent"].ToString();
                txtbxVechilesInspected.Text = vehiclePresent;
            }
            mycon.Close();

            #endregion

        }

        private void DisableRadioButtonGroup()
        {

            rdGroupRecordsReports.Enabled = false;
            rdGroupDemeanor.Enabled = false;
            rdGroupEmployeePerformMGT.Enabled = false;
            rdGroupEvidencePropMgt.Enabled = false;
            rdGroupFurniture.Enabled = false;
            rdGroupLawnCondition.Enabled = false;
            rdGroupMilitaryCourtesy.Enabled = false;
            rdGroupMotorVehicles.Enabled = false;
            rdGroupOnTimePrepared.Enabled = false;
            rdGroupPostGenerator.Enabled = false;
            rdGroupDemeanor.Enabled = false;
            rdGroupPostStructural.Enabled = false;
            rdGroupScheduleManPower.Enabled = false;
            rdGroupFurniture.Enabled = false;
            rdGroupUniforms.Enabled = false;
            rdGroupWeapons.Enabled = false;
            rdGrouptPostCleanliness.Enabled = false;


        }

        private void DisableTextBoxes()
        {
            txtTroop.Enabled = false;
            ddlPost.Enabled = false;
            txtPost.Enabled = false;
            txtDate.Enabled = false;
            DisableTextTable(false);
            txtbxVechilesInspected.Enabled = false;
            tbxPersonnelPresent.Enabled = false;

        }

        private void DisableTextTable(bool trueFalse)
        {
            switch (trueFalse)
            {
                case true:
                    //Crashes
                    txtbxCrashes_AL.Enabled = true;
                    txtbxCrashes_CM.Enabled = true;
                    txtbxCrashes_YTD.Enabled = true;
                    txtbxCrashes_SMLY.Enabled = true;
                    txtbxCrashes_YTD_LY.Enabled = true;
                    //fatalities
                    txtbxFatalities_AL.Enabled = true;
                    txtbxFatalities_CM.Enabled = true;
                    txtbxFatalities_YTD.Enabled = true;
                    txtbxFatalities_SMLY.Enabled = true;
                    txtbxFatalities_YTD_LY.Enabled = true;
                    //Arrests
                    txtbxArrests_AL.Enabled = true;
                    txtbxArrests_CM.Enabled = true;
                    txtbxArrests_YTD.Enabled = true;
                    txtbxArrests_SMLY.Enabled = true;
                    txtbxArrests_YTD_LY.Enabled = true;
                    //Warnings
                    txtbxWarnings_AL.Enabled = true;
                    txtbxWarnings_CM.Enabled = true;
                    txtbxWarnings_YTD.Enabled = true;
                    txtbxWarnings_SMLY.Enabled = true;
                    txtbxWarnings_YTD_LY.Enabled = true;
                    //DUI Arrest
                    txtbxDUIArrest_AL.Enabled = true;
                    txtbxDUIArrest_CM.Enabled = true;
                    txtbxDUIArrest_YTD.Enabled = true;
                    txtbxDUIArrest_SMLY.Enabled = true;
                    txtbxDUIArrest_YTD_LY.Enabled = true;
                    //Vechile stops
                    txtbxVechileStops_AL.Enabled = true;
                    txtbxVechileStops_CM.Enabled = true;
                    txtbxVechileStops_YTD.Enabled = true;
                    txtbxVechileStops_SMLY.Enabled = true;
                    txtbxVechileStops_YTD_LY.Enabled = true;
                    //Sergeants
                    txtbxSergeantsAssigned_CM.Enabled = true;
                    txtbxSergeantsAssigned_SMLY.Enabled = true;
                    //Corporals
                    txtbxCorporalsAssigned_CM.Enabled = true;
                    txtbxCorporalsAssigned_SMLY.Enabled = true;
                    //troopers
                    txtbxTroopersAssigned_CM.Enabled = true;
                    txtbxTroopersAssigned_SMLY.Enabled = true;
                    //Secretary
                    txtbxSecretaryAssigned_CM.Enabled = true;
                    txtbxSecretaryAssigned_SMLY.Enabled = true;
                    //Leave
                    txtbxLeave_CM.Enabled = true;
                    txtbxLeave_SMLY.Enabled = true;
                    //Detached
                    txtbxDetached_CM.Enabled = true;
                    txtbxDetached_SMLY.Enabled = true;
                    // txtbxGeneralRemarks.Enabled = true;
                    break;

                case false:
                    //Crashes
                    txtbxCrashes_AL.Enabled = false;
                    txtbxCrashes_CM.Enabled = false;
                    txtbxCrashes_YTD.Enabled = false;
                    txtbxCrashes_SMLY.Enabled = false;
                    txtbxCrashes_YTD_LY.Enabled = false;
                    //fatalities
                    txtbxFatalities_AL.Enabled = false;
                    txtbxFatalities_CM.Enabled = false;
                    txtbxFatalities_YTD.Enabled = false;
                    txtbxFatalities_SMLY.Enabled = false;
                    txtbxFatalities_YTD_LY.Enabled = false;
                    //Arrests
                    txtbxArrests_AL.Enabled = false;
                    txtbxArrests_CM.Enabled = false;
                    txtbxArrests_YTD.Enabled = false;
                    txtbxArrests_SMLY.Enabled = false;
                    txtbxArrests_YTD_LY.Enabled = false;
                    //Warnings
                    txtbxWarnings_AL.Enabled = false;
                    txtbxWarnings_CM.Enabled = false;
                    txtbxWarnings_YTD.Enabled = false;
                    txtbxWarnings_SMLY.Enabled = false;
                    txtbxWarnings_YTD_LY.Enabled = false;
                    //DUI Arrest
                    txtbxDUIArrest_AL.Enabled = false;
                    txtbxDUIArrest_CM.Enabled = false;
                    txtbxDUIArrest_YTD.Enabled = false;
                    txtbxDUIArrest_SMLY.Enabled = false;
                    txtbxDUIArrest_YTD_LY.Enabled = false;
                    //Vechile stops
                    txtbxVechileStops_AL.Enabled = false;
                    txtbxVechileStops_CM.Enabled = false;
                    txtbxVechileStops_YTD.Enabled = false;
                    txtbxVechileStops_SMLY.Enabled = false;
                    txtbxVechileStops_YTD_LY.Enabled = false;

                    //Sergeants
                    txtbxSergeantsAssigned_CM.Enabled = false;
                    txtbxSergeantsAssigned_SMLY.Enabled = false;
                    //Corporals
                    txtbxCorporalsAssigned_CM.Enabled = false;
                    txtbxCorporalsAssigned_SMLY.Enabled = false;
                    //troopers
                    txtbxTroopersAssigned_CM.Enabled = false;
                    txtbxTroopersAssigned_SMLY.Enabled = false;
                    //Secretary
                    txtbxSecretaryAssigned_CM.Enabled = false;
                    txtbxSecretaryAssigned_SMLY.Enabled = false;
                    //Leave
                    txtbxLeave_CM.Enabled = false;
                    txtbxLeave_SMLY.Enabled = false;
                    //Detached
                    txtbxDetached_CM.Enabled = false;
                    txtbxDetached_SMLY.Enabled = false;
                    //txtbxGeneralRemarks.Enabled = false;
                    break;
            }
        }

        protected void btnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            employeeCode = GetEmployeeJobCode();
            // var employeePostionNUmber = GetEmployeePositionNum();
            if ((employeeCode == "PSM021") || (employeeCode == "PSM022"))
            {
                if (!IsRadioButtonValid())
                {
                    Validate("RadioButtonValidations");
                    //  throw new Exception("Please check form for completeness");
                }
                else
                {
                    Upddate();

                    var strEmailTo = ddlEmail.SelectedValue;
                    if (strEmailTo != string.Empty)
                    {
                        var util = new Utils();
                        var formattedEmailAddress = strEmailTo + "@gsp.net";
                        strEmailTo = "ssweatman@gsp.net";
                        util.SendEmail(formattedEmailAddress, lblReportId.Text);
                        Response.Redirect("~/PostInspectionGrid.aspx");
                    }
                    else if (strEmailTo == string.Empty)
                    {
                        rfvEmail.Visible = true;
                    }

                    //Response.Redirect("~/PostInspectionReport.aspx");
                    Response.Redirect("~/PostInspectionGrid.aspx");
                }

            }
            else
            {
                Upddate();

                var strEmailTo = ddlEmail.SelectedValue;
                if (strEmailTo != string.Empty)
                {
                    var util = new Utils();
                    var formattedEmailAddress = strEmailTo + "@gsp.net";
                    formattedEmailAddress = "ssweatman@gsp.net";
                    util.SendEmail(formattedEmailAddress, lblReportId.Text);
                    Response.Redirect("~/PostInspectionGrid.aspx");
                }
                else if (strEmailTo == string.Empty)
                {
                    rfvEmail.Visible = true;
                }

                //Response.Redirect("~/PostInspectionReport.aspx");
                Response.Redirect("~/PostInspectionGrid.aspx");
            }
        }

        protected void btnPrint_OnClick(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", "attachment");
            //Response.AddHeader("Content-Length", pdf.Length.ToString());
            //Response.BinaryWrite(pdf);
            Response.End();
        }

        protected void ddlPost_SelectedIndexChanged(object sender, EventArgs e)
        {
            var post = ddlPost.SelectedItem.Text;
            var mycon = new SqlConnection(_dbConn);
            mycon.Open();
            var cmd = new SqlCommand("select City from ViewInspection where unit='" + post + "'", mycon);
            var read = cmd.ExecuteReader();
            while (read.Read())
                txtCity.Text = read["City"].ToString();
            mycon.Close();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            InsertReport();
            btnSubmit.Visible = true;
            btnSubmit.Text = "Submit";
            btnSubmit.Focus();
            btnSave.Visible = false;
            btnExit.Text = "Exit without Submitting";

        }

        private void InsertReport()
        {

            //check to see if reportID is already in database
            var helperClass = new Utils();
            bool reportExists = helperClass.IsReportInDataBase(lblReportId.Text, 1);

            if (reportExists)
            {
                Upddate();
            }
            else
            {
                string recordsAndReports = rdGroupRecordsReports.SelectedValue;
                string empPerformanceMgt = rdGroupEmployeePerformMGT.SelectedValue;
                string schAndManAllocation = rdGroupScheduleManPower.SelectedValue;
                string evidenceProp = rdGroupEvidencePropMgt.SelectedValue;
                string postStructCondition = rdGroupPostStructural.SelectedValue;
                string postCleanliness = rdGrouptPostCleanliness.SelectedValue;
                string lawnCondition = rdGroupLawnCondition.SelectedValue;
                string furnitureAndApplicances = rdGroupFurniture.SelectedValue;
                string motorVechicles = rdGroupMotorVehicles.SelectedValue;
                string postGenerator = rdGroupPostGenerator.SelectedValue;
                string weapons = rdGroupWeapons.SelectedValue;
                string uniformAndAppearance = rdGroupUniforms.SelectedValue;
                string militaryCourtesy = rdGroupMilitaryCourtesy.SelectedValue;
                string demeanorAndMorale = rdGroupDemeanor.SelectedValue;
                string onTimeAndPreparedForInspection = rdGroupOnTimePrepared.SelectedValue;

                var inpsectionDay = Convert.ToDateTime(txtDate.Text).Day.ToString();
                var inspectionMonth = Convert.ToDateTime(txtDate.Text).Month.ToString();
                var inspectionYear = Convert.ToDateTime(txtDate.Text).Year.ToString();
                var postNumber = new string(ddlPost.SelectedItem.Text.Where(char.IsNumber).ToArray());
                if (lblReportId.Text == string.Empty)
                {
                    lblReportId.Text = "P" + "--" + postNumber + inpsectionDay + inspectionMonth +
                                       inspectionYear;
                }
                var createdBy = User.Identity.Name.Substring(6);
                var created = Convert.ToDateTime(txtDate.Text);
                var personnelPresent = tbxPersonnelPresent.Text;
                //   DateTime modifiedDate =DateTime.Now;
                var modifiedBy = User.Identity.Name.Substring(6);
                var vehiclesPresent = txtbxVechilesInspected.Text;
                var generalRemarks = txtbxGeneralRemarks.Text;
                var reportId = lblReportId.Text;

                try
                {

                    var reportStatus = "Incomplete";
                    var reportStage = "Saved, Not Submitted";

                    #region InsertReport

                    helperClass.InsertPostInspectionReport(txtTroop.Text, ddlPost.SelectedItem.Text, txtCity.Text,
                        inpsectionDay, inspectionMonth,
                        inspectionYear, recordsAndReports,
                        empPerformanceMgt, schAndManAllocation,
                        evidenceProp, postStructCondition, postCleanliness,
                        lawnCondition, furnitureAndApplicances, motorVechicles, vehiclesPresent, postGenerator, weapons,
                        personnelPresent, uniformAndAppearance, militaryCourtesy, demeanorAndMorale,
                        onTimeAndPreparedForInspection,
                        Convert.ToInt32(txtbxCrashes_CM.Text),
                        Convert.ToInt32(txtbxCrashes_AL.Text),
                        Convert.ToInt32(txtbxCrashes_SMLY.Text),
                        Convert.ToInt32(txtbxCrashes_YTD.Text),
                        Convert.ToInt32(txtbxCrashes_YTD_LY.Text),
                        Convert.ToInt32(txtbxFatalities_CM.Text),
                        Convert.ToInt32(txtbxFatalities_AL.Text),
                        Convert.ToInt32(txtbxFatalities_SMLY.Text),
                        Convert.ToInt32(txtbxFatalities_YTD.Text),
                        Convert.ToInt32(txtbxFatalities_YTD_LY.Text),
                        Convert.ToInt32(txtbxArrests_CM.Text),
                        Convert.ToInt32(txtbxArrests_AL.Text),
                        Convert.ToInt32(txtbxArrests_SMLY.Text),
                        Convert.ToInt32(txtbxArrests_YTD.Text),
                        Convert.ToInt32(txtbxArrests_YTD_LY.Text),
                        Convert.ToInt32(txtbxWarnings_CM.Text),
                        Convert.ToInt32(txtbxWarnings_AL.Text),
                        Convert.ToInt32(txtbxWarnings_SMLY.Text),
                        Convert.ToInt32(txtbxWarnings_YTD.Text),
                        Convert.ToInt32(txtbxWarnings_YTD_LY.Text),
                        Convert.ToInt32(txtbxDUIArrest_CM.Text),
                        Convert.ToInt32(txtbxDUIArrest_AL.Text),
                        Convert.ToInt32(txtbxDUIArrest_SMLY.Text),
                        Convert.ToInt32(txtbxDUIArrest_YTD.Text),
                        Convert.ToInt32(txtbxDUIArrest_YTD_LY.Text),
                        Convert.ToInt32(txtbxVechileStops_CM.Text),
                        Convert.ToInt32(txtbxVechileStops_AL.Text),
                        Convert.ToInt32(txtbxVechileStops_SMLY.Text),
                        Convert.ToInt32(txtbxVechileStops_YTD.Text),
                        Convert.ToInt32(txtbxVechileStops_YTD_LY.Text),
                        Convert.ToInt32(txtbxSergeantsAssigned_CM.Text),
                        Convert.ToInt32(txtbxSergeantsAssigned_SMLY.Text),
                        Convert.ToInt32(txtbxCorporalsAssigned_CM.Text),
                        Convert.ToInt32(txtbxCorporalsAssigned_SMLY.Text),
                        Convert.ToInt32(txtbxTroopersAssigned_CM.Text),
                        Convert.ToInt32(txtbxTroopersAssigned_SMLY.Text),
                        Convert.ToInt32(txtbxSecretaryAssigned_CM.Text),
                        Convert.ToInt32(txtbxSecretaryAssigned_SMLY.Text),
                        Convert.ToInt32(txtbxLeave_CM.Text),
                        Convert.ToInt32(txtbxLeave_SMLY.Text),
                        Convert.ToInt32(txtbxDetached_CM.Text),
                        Convert.ToInt32(txtbxDetached_SMLY.Text), generalRemarks, createdBy, created, modifiedBy,
                        DateTime.Now,
                        reportId, reportStatus, reportStage);
                }
                catch (Exception ex)
                {
                    //TODO ADD ERROR HANDLING
                    lblConfirm.Text = "There was a problem saving report";
                }

                    #endregion

                lblSavedSuccess.Visible = true;
                btnSave.Visible = true;
                btnSubmit.Visible = true;
                // ddlEmail.Enabled = true;
                btnTableGenerate.Visible = false;
                // PopulateTextBoxes();
                ddlEmail.Focus();
                //  Response.Redirect("PostInspectionGrid.aspx", true);
            }
        }

        private void Upddate()
        {
            employeeCode = GetEmployeeJobCode();
            var employeePostionNUmber = GetEmployeePositionNum();

            var vehiclesPresent = txtbxVechilesInspected.Text;
            var personnelPresent = tbxPersonnelPresent.Text;
            // ValidateRadioButtonControl();
            #region RadioButton value setting
            //Get radio button values
            string recordsAndReports = rdGroupRecordsReports.SelectedValue;
            string empPerformanceMgt = rdGroupEmployeePerformMGT.SelectedValue;
            string schAndManAllocation = rdGroupScheduleManPower.SelectedValue;
            string evidenceProp = rdGroupEvidencePropMgt.SelectedValue;
            string postStructCondition = rdGroupPostStructural.SelectedValue;
            string postCleanliness = rdGrouptPostCleanliness.SelectedValue;
            string lawnCondition = rdGroupLawnCondition.SelectedValue;
            string furnitureAndApplicances = rdGroupFurniture.SelectedValue;
            string motorVechicles = rdGroupMotorVehicles.SelectedValue;
            string postGenerator = rdGroupPostGenerator.SelectedValue;
            string weapons = rdGroupWeapons.SelectedValue;
            string uniformAndAppearance = rdGroupUniforms.SelectedValue;
            string militaryCourtesy = rdGroupMilitaryCourtesy.SelectedValue;
            string demeanorAndMorale = rdGroupDemeanor.SelectedValue;
            string onTimeAndPreparedForInspection = rdGroupOnTimePrepared.SelectedValue;
            #endregion

            #region UpdatePostCommReport

            var reportId = lblReportId.Text;
            var modifiedBy = User.Identity.Name.Substring(6);
            var modified = DateTime.Now;
            var reportStage = "";
            var reportStatus = "Pending";

            switch (employeeCode.Trim())
            {
                case "GST051":
                    reportStage = "Awaiting Troop Officer Approval";
                    break;
                case "PSM020":
                    reportStage = "Awaiting Troop Officer Approval";
                    break;
                case "PSM021":
                    //  validate();
                    reportStage = "Awaiting Troop Commander Approval";
                    break;
                case "PSM022":
                    // validate();
                    reportStage = "Awaiting Major Approval";
                    break;
                default:
                    switch (employeePostionNUmber)
                    {
                        case "00105296":
                            reportStatus = "APPROVED";
                            reportStage = "COMPLETED";
                            break;
                    }
                    break;
            }

            var hlpClass = new Utils();
            hlpClass.UpatePostComInspectionReport(reportId, txtTroop.Text, txtPost.Text, txtCity.Text, recordsAndReports,
                empPerformanceMgt, schAndManAllocation,
                evidenceProp, postStructCondition, postCleanliness,
                lawnCondition, furnitureAndApplicances, motorVechicles, vehiclesPresent, postGenerator, weapons,
                personnelPresent, uniformAndAppearance, militaryCourtesy, demeanorAndMorale,
                onTimeAndPreparedForInspection,
                Convert.ToInt32(txtbxCrashes_CM.Text),
                Convert.ToInt32(txtbxCrashes_AL.Text),
                Convert.ToInt32(txtbxCrashes_SMLY.Text),
                Convert.ToInt32(txtbxCrashes_YTD.Text),
                Convert.ToInt32(txtbxCrashes_YTD_LY.Text),
                Convert.ToInt32(txtbxFatalities_CM.Text),
                Convert.ToInt32(txtbxFatalities_AL.Text),
                Convert.ToInt32(txtbxFatalities_SMLY.Text),
                Convert.ToInt32(txtbxFatalities_YTD.Text),
                Convert.ToInt32(txtbxFatalities_YTD_LY.Text),
                Convert.ToInt32(txtbxArrests_CM.Text),
                Convert.ToInt32(txtbxArrests_AL.Text),
                Convert.ToInt32(txtbxArrests_SMLY.Text),
                Convert.ToInt32(txtbxArrests_YTD.Text),
                Convert.ToInt32(txtbxArrests_YTD_LY.Text),
                Convert.ToInt32(txtbxWarnings_CM.Text),
                Convert.ToInt32(txtbxWarnings_AL.Text),
                Convert.ToInt32(txtbxWarnings_SMLY.Text),
                Convert.ToInt32(txtbxWarnings_YTD.Text),
                Convert.ToInt32(txtbxWarnings_YTD_LY.Text),
                Convert.ToInt32(txtbxDUIArrest_CM.Text),
                Convert.ToInt32(txtbxDUIArrest_AL.Text),
                Convert.ToInt32(txtbxDUIArrest_SMLY.Text),
                Convert.ToInt32(txtbxDUIArrest_YTD.Text),
                Convert.ToInt32(txtbxDUIArrest_YTD_LY.Text),
                Convert.ToInt32(txtbxVechileStops_CM.Text),
                Convert.ToInt32(txtbxVechileStops_AL.Text),
                Convert.ToInt32(txtbxVechileStops_SMLY.Text),
                Convert.ToInt32(txtbxVechileStops_YTD.Text),
                Convert.ToInt32(txtbxVechileStops_YTD_LY.Text),
                Convert.ToInt32(txtbxSergeantsAssigned_CM.Text),
                Convert.ToInt32(txtbxSergeantsAssigned_SMLY.Text),
                Convert.ToInt32(txtbxCorporalsAssigned_CM.Text),
                Convert.ToInt32(txtbxCorporalsAssigned_SMLY.Text),
                Convert.ToInt32(txtbxTroopersAssigned_CM.Text),
                Convert.ToInt32(txtbxTroopersAssigned_SMLY.Text),
                Convert.ToInt32(txtbxSecretaryAssigned_CM.Text),
                Convert.ToInt32(txtbxSecretaryAssigned_SMLY.Text),
                Convert.ToInt32(txtbxLeave_CM.Text),
                Convert.ToInt32(txtbxLeave_SMLY.Text),
                Convert.ToInt32(txtbxDetached_CM.Text),
                Convert.ToInt32(txtbxDetached_SMLY.Text),
                txtbxGeneralRemarks.Text,
                modifiedBy,
                modified,
                reportStatus,
                reportStage);

            #endregion

        }

        private void PopulateReport()
        {

            lblReportId.Text = (string)Session["ReportId"];
            divConfirm.Visible = false;
            lblConfirm.Visible = false;
            //txtDate.Text = DateTime.Now.ToShortDateString();
            var mycon = new SqlConnection(_spConnect);
            mycon.Open();
            var cmd = new SqlCommand(
                @"select * from [PostInspectionReport] WHERE ReportId = '" + lblReportId.Text + "'",
                mycon);
            var read = cmd.ExecuteReader();
            #region DataRead
            while (read.Read())
            {
                txtTroop.Text = read["Troop"].ToString(); // Troop = parentUnit in the database      
                txtPost.Text = read["Post"].ToString();
                txtCity.Text = read["City"].ToString();
                txtDate.Text = read["Created"].ToString();

                ///////////AdminRadioButtons
                rdGroupRecordsReports.SelectedValue = read["RecordsandReports"].ToString();
                rdGroupEmployeePerformMGT.SelectedValue = read["EmpPerformanceMgt"].ToString();
                rdGroupScheduleManPower.SelectedValue = read["SchAndManAllocation"].ToString();
                rdGroupEvidencePropMgt.SelectedValue = read["EvidenceAndPropertyMgt"].ToString();
                rdGroupPostStructural.SelectedValue = read["PostStructral"].ToString();
                rdGrouptPostCleanliness.SelectedValue = read["PostClean"].ToString();
                rdGroupLawnCondition.SelectedValue = read["LawnCondition"].ToString();
                rdGroupFurniture.SelectedValue = read["Furniture"].ToString();
                rdGroupMotorVehicles.SelectedValue = read["MotorVehPatrol"].ToString();
                rdGroupPostGenerator.SelectedValue = read["PostGenerator"].ToString();
                rdGroupWeapons.SelectedValue = read["Weapons"].ToString();
                rdGroupUniforms.SelectedValue = read["Uniforms"].ToString();
                rdGroupMilitaryCourtesy.SelectedValue = read["Military"].ToString();
                rdGroupDemeanor.SelectedValue = read["Demeanor"].ToString();
                rdGroupOnTimePrepared.SelectedValue = read["OntimePrepared"].ToString();
                // PopulateTextBoxesWithReportData();
                txtbxGeneralRemarks.Text = read["GeneralRemarks"].ToString();
                txtbxVechilesInspected.Text = read["VechPresent"].ToString();/*Query coming*/
                tbxPersonnelPresent.Text = read["PersonnelPresent"].ToString();/*Query coming*/

                #region TextFields

                ///*Crashes*/
                //txtbxCrashes_CM.Text = hlpClass.GetValue<int>(read, "Crash_CM").ToString();
                ////txtbxCrashes_CM.Text = hlpClass.GetValue<int?>(read,"Crash_CM").ToString();
                //txtbxCrashes_AL.Text = hlpClass.GetValue<int>(read, "Crash_AL").ToString();
                //txtbxCrashes_SMLY.Text = hlpClass.GetValue<int>(read, "Crash_SMLY").ToString();
                //txtbxCrashes_YTD.Text = hlpClass.GetValue<int>(read, "Crashes_YTD").ToString();
                //txtbxCrashes_YTD_LY.Text = hlpClass.GetValue<int>(read, "Crashes_YTD_LY").ToString();
                ///*Fatalities*/
                //txtbxFatalities_CM.Text = hlpClass.GetValue<int>(read, "Fatalities_CM").ToString();
                //txtbxFatalities_AL.Text = hlpClass.GetValue<int>(read, "Fatalities_AL").ToString();
                //txtbxFatalities_SMLY.Text = hlpClass.GetValue<int>(read, "Fatalities_SMLY").ToString();
                //txtbxFatalities_YTD.Text = hlpClass.GetValue<int>(read, "Fatalities_YTD").ToString();
                //txtbxFatalities_YTD_LY.Text = hlpClass.GetValue<int>(read, "Fatalities_YTD_LY").ToString();
                ///*Arrest*/
                //txtbxArrests_AL.Text = hlpClass.GetValue<int>(read, "Arrest_AL").ToString();
                //txtbxArrests_CM.Text = hlpClass.GetValue<int>(read, "Arrest_CM").ToString();
                //txtbxArrests_SMLY.Text = hlpClass.GetValue<int>(read, "Arrest_SMLY").ToString();
                //txtbxArrests_YTD.Text = hlpClass.GetValue<int>(read, "Arrest_YTD").ToString();
                //txtbxArrests_YTD_LY.Text = hlpClass.GetValue<int>(read, "Arrest_YTD_LY").ToString();
                ///*Warnings*/
                //txtbxWarnings_CM.Text = hlpClass.GetValue<int>(read, "Warnings_CM").ToString();
                //txtbxWarnings_AL.Text = hlpClass.GetValue<int>(read, "Warnings_AL").ToString();
                //txtbxWarnings_SMLY.Text = hlpClass.GetValue<int>(read, "Warnings_SMLY").ToString();
                //txtbxWarnings_YTD.Text = hlpClass.GetValue<int>(read, "Warnings_YTD").ToString();
                //txtbxWarnings_YTD_LY.Text = hlpClass.GetValue<int>(read, "Warnings_YTD_LY").ToString();
                ///*DUI Arrest*/
                //txtbxDUIArrest_CM.Text = hlpClass.GetValue<int>(read, "DUIArrest_CM").ToString();
                //txtbxDUIArrest_AL.Text = hlpClass.GetValue<int>(read, "DUIArrest_AL").ToString();
                //txtbxDUIArrest_SMLY.Text = hlpClass.GetValue<int>(read, "DUIArrest_SMLY").ToString();
                //txtbxDUIArrest_YTD.Text = hlpClass.GetValue<int>(read, "DUIArrest_YTD").ToString();
                //txtbxDUIArrest_YTD_LY.Text = hlpClass.GetValue<int>(read, "DUIArrest_YTD_LY").ToString();
                ///*Vehicle Stops*/
                //txtbxVechileStops_CM.Text = hlpClass.GetValue<int>(read, "VehicleStops_CM").ToString();
                //txtbxVechileStops_AL.Text = hlpClass.GetValue<int>(read, "VehicleStops_AL").ToString();
                //txtbxVechileStops_SMLY.Text = hlpClass.GetValue<int>(read, "VehicleStops_SMLY").ToString();
                //txtbxVechileStops_YTD.Text = hlpClass.GetValue<int>(read, "VehicleStops_ytd").ToString();
                //txtbxVechileStops_YTD_LY.Text = hlpClass.GetValue<int>(read, "VehicleStops_YTD_LY").ToString();
                ///*[SergeantsAssigned*/
                //txtbxSergeantsAssigned_CM.Text = hlpClass.GetValue<int>(read, "SergeantsAssigned_CM").ToString();
                //txtbxSergeantsAssigned_SMLY.Text = hlpClass.GetValue<int>(read, "SergeantsAssigned_SMLY").ToString();
                ///*[CorporalsAssigned*/
                //txtbxCorporalsAssigned_CM.Text = hlpClass.GetValue<int>(read, "CorporalsAssigned_CM").ToString();
                //txtbxCorporalsAssigned_SMLY.Text = hlpClass.GetValue<int>(read, "CorporalsAssigned_SMLY").ToString();
                ///*[TroopersAssigned*/
                //txtbxTroopersAssigned_CM.Text = hlpClass.GetValue<int>(read, "TroopersAssigned_CM").ToString();
                //txtbxTroopersAssigned_SMLY.Text = hlpClass.GetValue<int>(read, "TroopersAssigned_SMLY").ToString();
                ///*[SecretaryAssigned*/
                //txtbxSecretaryAssigned_CM.Text = hlpClass.GetValue<int>(read, "SecretaryAssigned_CM").ToString();
                //txtbxSecretaryAssigned_SMLY.Text = hlpClass.GetValue<int>(read, "SecretaryAssigned_SMLY").ToString();
                ///*Leave*/
                //txtbxLeave_CM.Text = hlpClass.GetValue<int>(read, "Leave_CM").ToString();
                //txtbxLeave_SMLY.Text = hlpClass.GetValue<int>(read, "Leave_SMLY").ToString();
                ///*Detached*/
                //txtbxDetached_CM.Text = hlpClass.GetValue<int>(read, "Detached_CM").ToString();
                //txtbxDetached_SMLY.Text = hlpClass.GetValue<int>(read, "Detached_SMLY").ToString();

                #endregion

            }
            mycon.Close();
            #endregion

            txtDate.Text = Convert.ToDateTime(txtDate.Text).ToShortDateString();
        }

        private void PostReport_PostSec()
        {
            var helpUtils = new Utils();
            var path = HttpContext.Current.Request.UrlReferrer.AbsolutePath;

            //HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath("~/PostInspectionGrid.aspx");
            //
            if (path != "/PostInspectionGrid.aspx")//TODO Verify all senarios
            {
                /*New Report*/
                btnSave.Visible = true;
                ddlEmail.Enabled = true;
                btnRejct.Visible = false;
                btnSubmit.Visible = true;
                btnSubmit.Text = "Submit";
                // DisableTopForm();
                divConfirm.Visible = false;
                lblSavedSuccess.Visible = false;
                DisableTextTable(true);
                // DisableTopForm();
                txtbxVechilesInspected.Enabled = true;
                tbxPersonnelPresent.Enabled = true;
                btnTableGenerate.Visible = true;
                tbxPersonnelPresent.Enabled = true;
                txtbxVechilesInspected.Enabled = true;
                hdrLabel.InnerText = "POST INSPECTION REPORT-- Post Secretary";
                //email list to the approval employee
                ddlEmail.DataSource = helpUtils.GetTroopOfficerEmailList(txtTroop.Text);
                ddlEmail.DataTextField = "EmployeeName";
                ddlEmail.DataValueField = "sAMAccountName";
                ddlEmail.DataBind();
                ddlEmail.Items.Insert(0, new ListItem("-- Select One --", "-1"));
                ddlEmail.Enabled = true;

                //  txtDate.Text = Convert.ToDateTime(txtDate.Text).ToString("MM/dd/yyyy");
                // var listPost = new Utils();
                //populate Post

                ddlPost.DataSource = helpUtils.ListPostByTroop(txtTroop.Text);
                ddlPost.DataTextField = "Post";
                ddlPost.DataValueField = "PostId";
                ddlPost.DataBind();
                ddlPost.Items.Insert(0, new ListItem("-- Select One --", "-1"));
            }
            else
            {
                /*Report Already Created*/
                var reportSession = (string)Session["ReportId"] ?? lblReportId.Text;
                var status = helpUtils.CheckReportStatus(reportSession);
                if (status == "APPROVED")
                {
                    reportComplete();
                }
                else if (string.IsNullOrWhiteSpace(status))
                {
                    Response.Redirect("~/PostInspectionGrid.aspx");//TODO Change route
                }
                else
                {
                    // PopulateReport();
                    PopulateTextBoxesWithReportDataPostSec();
                    txtDate.Text = Convert.ToDateTime(txtDate.Text).ToString("MM/dd/yyyy");
                    lblReportId.Text = reportSession;
                    //Session["ReportId"].ToString();
                    btnSubmit.Visible = true;
                    btnSubmit.Text = "Submit";
                    btnSave.Visible = true;
                    ddlEmail.Enabled = true;
                    divConfirm.Visible = false;
                    lblSavedSuccess.Visible = false;
                    txtbxVechilesInspected.Enabled = true;
                    tbxPersonnelPresent.Enabled = true;
                    // DisableTopForm();
                    btnExit.Visible = false;
                    ddlPost.Visible = false;
                    ddlPost.Enabled = false;
                    btnRejct.Visible = false;
                    btnExit.Visible = true;

                    txtPost.Enabled = false;
                    txtPost.Visible = true;
                    DisableTextTable(true);
                    txtDate.Enabled = false;
                    btnTableGenerate.Visible = false;
                    hdrLabel.InnerText = "POST INSPECTION REPORT-- Post Secretary";
                    //email list to the approval employee
                    // var emailList = new Utils();
                    ddlEmail.DataSource = helpUtils.GetTroopOfficerEmailList(txtTroop.Text);
                    ddlEmail.DataTextField = "EmployeeName";
                    ddlEmail.DataValueField = "sAMAccountName";
                    ddlEmail.DataBind();
                    ddlEmail.Items.Insert(0, new ListItem("-- Select One --", "-1"));
                    ddlEmail.Enabled = true;
                }
            }
        }

        private void PostReport_PostCMDR()
        {
            var helpUtils = new Utils();
            var path = HttpContext.Current.Request.UrlReferrer.AbsolutePath;
            if (path != "/PostInspectionGrid.aspx")
            {
                /*New Report*/
                btnSave.Visible = true;
                ddlEmail.Enabled = true;
                btnRejct.Visible = false;
                btnSubmit.Visible = true;
                btnSubmit.Text = "Submit";
                // DisableTopForm();
                divConfirm.Visible = false;
                lblSavedSuccess.Visible = false;
                DisableTextTable(true);
                //DisableTopForm();
                txtbxVechilesInspected.Enabled = true;
                tbxPersonnelPresent.Enabled = true;
                btnTableGenerate.Visible = true;
                tbxPersonnelPresent.Enabled = true;
                hdrLabel.InnerText = "POST INSPECTION REPORT-- Post Commander";
                //email list to the approval employee
                ddlEmail.DataSource = helpUtils.GetTroopOfficerEmailList(txtTroop.Text);
                ddlEmail.DataTextField = "EmployeeName";
                ddlEmail.DataValueField = "sAMAccountName";
                ddlEmail.DataBind();
                ddlEmail.Items.Insert(0, new ListItem("-- Select One --", "-1"));
                ddlEmail.Enabled = true;

                ddlPost.DataSource = helpUtils.ListPostByTroop(txtTroop.Text);
                ddlPost.DataTextField = "Post";
                ddlPost.DataValueField = "PostId";
                ddlPost.DataBind();
                ddlPost.Items.Insert(0, new ListItem("-- Select One --", "-1"));
            }
            else
            {
                /*Report Already Created*/
                var status = helpUtils.CheckReportStatus((string)Session["ReportId"]);
                if (status == "APPROVED")
                {
                    reportComplete();
                }
                else if (string.IsNullOrWhiteSpace(status))
                {
                    Response.Redirect("~/PostInspectionGrid.aspx");
                }
                else
                {
                    PopulateTextBoxesWithReportDataPostSec();
                    txtDate.Text = Convert.ToDateTime(txtDate.Text).ToString("MM/dd/yyyy");
                    lblReportId.Text = (string)Session["ReportId"];
                    btnSubmit.Visible = true;
                    btnSubmit.Text = "Submit";
                    btnSave.Visible = true;
                    ddlEmail.Enabled = true;
                    divConfirm.Visible = false;
                    lblSavedSuccess.Visible = false;
                    txtbxVechilesInspected.Enabled = false;
                    tbxPersonnelPresent.Enabled = false;
                    // DisableTopForm();
                    btnExit.Visible = false;
                    ddlPost.Visible = false;
                    ddlPost.Enabled = false;
                    btnRejct.Visible = false;
                    btnExit.Visible = true;

                    txtPost.Enabled = false;
                    txtPost.Visible = true;
                    DisableTextTable(true);
                    txtDate.Enabled = false;
                    btnTableGenerate.Visible = false;
                    hdrLabel.InnerText = "POST INSPECTION REPORT-- Post Secretary";
                    //email list to the approval employee
                    // var emailList = new Utils();
                    ddlEmail.DataSource = helpUtils.GetTroopOfficerEmailList(txtTroop.Text);
                    ddlEmail.DataTextField = "EmployeeName";
                    ddlEmail.DataValueField = "sAMAccountName";
                    ddlEmail.DataBind();
                    ddlEmail.Items.Insert(0, new ListItem("-- Select One --", "-1"));
                    ddlEmail.Enabled = true;
                }
            }
        }

        private void PostReport_TroopOfficer()
        {
            var helpUtils = new Utils();

            var path = HttpContext.Current.Request.UrlReferrer.AbsolutePath;
            if (path != "/PostInspectionGrid.aspx")
            {
                /*New Report*/
                btnSave.Visible = true;
                ddlEmail.Enabled = true;
                btnRejct.Visible = false;
                btnSubmit.Visible = true;
                btnSubmit.Text = "Submit";
                // DisableTopForm();
                divConfirm.Visible = false;
                lblSavedSuccess.Visible = false;
                DisableTextTable(true);
                //DisableTopForm();
                txtbxVechilesInspected.Enabled = true;
                tbxPersonnelPresent.Enabled = true;
                btnTableGenerate.Visible = true;
                tbxPersonnelPresent.Enabled = true;
                hdrLabel.InnerText = "POST INSPECTION REPORT-- Post Commander";
                //email list to the approval employee
                ddlEmail.DataSource = helpUtils.GetTroopOfficerEmailList(txtTroop.Text);
                ddlEmail.DataTextField = "EmployeeName";
                ddlEmail.DataValueField = "sAMAccountName";
                ddlEmail.DataBind();
                ddlEmail.Items.Insert(0, new ListItem("-- Select One --", "-1"));
                ddlEmail.Enabled = true;

                ddlPost.DataSource = helpUtils.ListPostByTroop(txtTroop.Text);
                ddlPost.DataTextField = "Post";
                ddlPost.DataValueField = "PostId";
                ddlPost.DataBind();
                ddlPost.Items.Insert(0, new ListItem("-- Select One --", "-1"));
            }
            else
            {
                var status = helpUtils.CheckReportStatus((string)Session["ReportId"]);
                if (status == "APPROVED")
                {
                    reportComplete();
                }
                else if (string.IsNullOrWhiteSpace(status))//no Report redirect to grid
                {
                    Response.Redirect("~/PostInspectionGrid.aspx");
                }
                else
                {

                    PopulateReport();
                    PopulateTextBoxesWithReportData();
                    btnSubmit.Visible = true;
                    btnSave.Visible = false;
                    ddlEmail.Enabled = true;
                    divConfirm.Visible = false;
                    lblSavedSuccess.Visible = false;
                    txtbxVechilesInspected.Enabled = true;
                    tbxPersonnelPresent.Enabled = true;
                    // DisableTopForm();
                    btnExit.Visible = false;
                    ddlPost.Visible = false;
                    ddlPost.Enabled = false;
                    txtPost.Enabled = false;
                    txtPost.Visible = true;
                    DisableTextTable(true);
                    txtDate.Enabled = false;
                    btnTableGenerate.Visible = false;
                    hdrLabel.InnerText = "POST INSPECTION REPORT-- Troop Officer";
                    //email list to the approval employee

                    ddlEmail.DataSource = helpUtils.GetTroopCMDEmailList(txtTroop.Text);
                    ddlEmail.DataTextField = "EmployeeName";
                    ddlEmail.DataValueField = "sAMAccountName";
                    ddlEmail.DataBind();
                    ddlEmail.Items.Insert(0, new ListItem("-- Select One --", "-1"));
                    ddlEmail.Enabled = true;

                }
            }
        }
        private void PostReport_TroopCMDR()
        {
            var helpUtils = new Utils();
            var status = helpUtils.CheckReportStatus((string)Session["ReportId"]);
            if (status == "APPROVED")
            {
                reportComplete();
            }
            else if (string.IsNullOrWhiteSpace(status))
            {
                Response.Redirect("UnauthorizedUser.aspx");
            }
            else
            {
                PopulateReport();
                PopulateTextBoxesWithReportData();
                btnSubmit.Visible = true;
                btnSave.Visible = false;
                ddlEmail.Enabled = true;
                ddlPost.Enabled = false;
                ddlPost.Visible = false;
                txtPost.Enabled = false;
                txtPost.Visible = true;
                txtDate.Enabled = false;
                txtDate.Text = Convert.ToDateTime(txtDate.Text).ToShortDateString();
                //DisableTopForm();
                divConfirm.Visible = false;
                lblSavedSuccess.Visible = false;
                DisableTextTable(false);
                // DisableRadioButtonGroup();
                DisableTextBoxes();
                PopulateTextBoxes();
                btnTableGenerate.Visible = false;
                btnExit.Visible = false;
                hdrLabel.InnerText = "POST INSPECTION REPORT-- Troop Commander";
                //email list to the approval employee
                ddlEmail.DataSource = helpUtils.GetMajorEmailList();
                ddlEmail.DataTextField = "EmployeeName";
                ddlEmail.DataValueField = "sAMAccountName";
                ddlEmail.DataBind();
                ddlEmail.Items.Insert(0, new ListItem("-- Select One --", "-1"));
                ddlEmail.Enabled = true;
            }
        }

        private void PostReport_Major()
        {
            var helpUtils = new Utils();
            var status = helpUtils.CheckReportStatus((string)Session["ReportId"]);
            if (status == "APPROVED")
            {
                reportComplete();
            }
            else if (string.IsNullOrWhiteSpace(status))
            {
                Response.Redirect("UnauthorizedUser.aspx");
            }
            else
            {
                PopulateReport();
                PopulateTextBoxesWithReportData();
                btnSubmit.Visible = true;
                btnSave.Visible = false;
                lblEmail.Visible = false;
                ddlEmail.Visible = false;
                ddlEmail.Enabled = false;
                ddlPost.Visible = false;
                ddlPost.Enabled = false;
                txtPost.Enabled = false;
                txtPost.Visible = true;
                txtDate.Enabled = false;
                DisableRadioButtonGroup();
                DisableTextBoxes();
                PopulateTextBoxesWithReportData();
                divConfirm.Visible = false;
                lblSavedSuccess.Visible = false;
                DisableTextTable(false);
                btnTableGenerate.Visible = false;
                btnExit.Visible = false;
                hdrLabel.InnerText = "POST INSPECTION REPORT-- Major";
            }
        }

        private string GetEmployeeJobCode()
        {
            string currentUser = User.Identity.Name.Substring(6);
            // currentUser = "bboulware";
            //var employeePostionNUmber = string.Empty;
            var mycon = new SqlConnection(_dbConn);
            mycon.Open();
            var cmd = new SqlCommand(
                @"select * from ViewInspection where sAMAccountName='" + currentUser + "'",
                mycon);
            var read = cmd.ExecuteReader();
            while (read.Read())
            {
                employeeCode = read["EmployeeJobCode"].ToString();
                //employeePostionNUmber = read["EmployeePostionNUmber"].ToString();
            }
            mycon.Close();
            return employeeCode;
        }

        private string GetEmployeePositionNum()
        {
            string currentUser = User.Identity.Name.Substring(6);
            // currentUser = "bboulware";
            var employeePostionNUmber = string.Empty;
            var mycon = new SqlConnection(_dbConn);
            mycon.Open();
            var cmd = new SqlCommand(
                @"select * from ViewInspection where sAMAccountName='" + currentUser + "'",
                mycon);
            var read = cmd.ExecuteReader();
            while (read.Read())
            {
                //employeeCode = read["EmployeeJobCode"].ToString();
                employeePostionNUmber = read["EmployeePostionNUmber"].ToString();
            }
            mycon.Close();
            return employeePostionNUmber;
        }



        #region Private Methods
        private static double CalculateChange(long lastYear, long currentYear)
        {

            var percent = (currentYear - lastYear) * 100;
            return (double)percent / lastYear;
            //var percentageOfChange = Math.Round(percent);
            //  return  "-" + percent;
        }

        protected void txtbxCrashes_YTD_OnTextChanged(object sender, EventArgs e)
        {
            var crashesYTD = int.Parse(txtbxCrashes_YTD.Text);
            var crashesYtdLY = int.Parse(txtbxCrashes_YTD_LY.Text);
            var resultChange = CalculateChange(crashesYtdLY, crashesYTD);
            txtCrashesChange.Text = resultChange.ToString("F");
            txtbxGeneralRemarks.Focus();
        }

        protected void txtbxFatalities_YTD_LY_OnTextChanged(object sender, EventArgs e)
        {
            var fatalitiesYTD = int.Parse(txtbxFatalities_YTD.Text);
            var fatalitiesYtdLY = int.Parse(txtbxFatalities_YTD_LY.Text);
            var resultChange = CalculateChange(fatalitiesYtdLY, fatalitiesYTD);
            txtbxFatalitiesChange.Text = resultChange.ToString("F");
            txtbxGeneralRemarks.Focus();
        }

        protected void txtbxArrests_YTD_OnTextChanged(object sender, EventArgs e)
        {
            var arrestYTD = int.Parse(txtbxArrests_YTD.Text);
            var arrestYtdLY = int.Parse(txtbxArrests_YTD_LY.Text);
            var resultChange = CalculateChange(arrestYtdLY, arrestYTD);
            txtArrestChange.Text = resultChange.ToString("F");
            txtbxGeneralRemarks.Focus();
        }

        protected void txtbxWarnings_YTD_OnTextChanged(object sender, EventArgs e)
        {
            var warningsYTD = int.Parse(txtbxWarnings_YTD.Text);
            var warningYtdLY = int.Parse(txtbxWarnings_YTD_LY.Text);
            var resultChange = CalculateChange(warningYtdLY, warningsYTD);
            txtWarningsChange.Text = resultChange.ToString("F");
            txtbxGeneralRemarks.Focus();
        }

        protected void txtbxDUIArrest_YTD_OnTextChanged(object sender, EventArgs e)
        {
            var duiYTD = int.Parse(txtbxDUIArrest_YTD.Text);
            var duiYtdLY = int.Parse(txtbxDUIArrest_YTD_LY.Text);
            var resultChange = CalculateChange(duiYtdLY, duiYTD);
            txtDUIArrestChange.Text = resultChange.ToString("F");
            txtbxGeneralRemarks.Focus();

        }

        protected void txtbxVechileStops_YTD_OnTextChanged(object sender, EventArgs e)
        {
            var stopsYTD = int.Parse(txtbxVechileStops_YTD.Text);
            var stopsYtdLy = int.Parse(txtbxVechileStops_YTD_LY.Text);
            var resultChange = CalculateChange(stopsYtdLy, stopsYTD);
            txtVechileChange.Text = resultChange.ToString("F");
            txtbxGeneralRemarks.Focus();
        }

        private bool IsRadioButtonValid()
        {

            if (!string.IsNullOrWhiteSpace(rdGroupRecordsReports.SelectedValue) ||
                (!string.IsNullOrWhiteSpace(rdGroupEmployeePerformMGT.SelectedValue)) ||
                 (!string.IsNullOrWhiteSpace(rdGroupScheduleManPower.SelectedValue)) ||
                (!string.IsNullOrWhiteSpace(rdGroupEvidencePropMgt.SelectedValue)) ||
                (!string.IsNullOrWhiteSpace(rdGroupPostStructural.SelectedValue)) ||
               (!string.IsNullOrWhiteSpace(rdGrouptPostCleanliness.SelectedValue)) ||
                (!string.IsNullOrWhiteSpace(rdGroupLawnCondition.SelectedValue)) ||
                (!string.IsNullOrWhiteSpace(rdGroupFurniture.SelectedValue)) ||
                (!string.IsNullOrWhiteSpace(rdGroupMotorVehicles.SelectedValue)) ||
                (!string.IsNullOrWhiteSpace(rdGroupPostGenerator.SelectedValue)) ||
                (!string.IsNullOrWhiteSpace(rdGroupWeapons.SelectedValue)) ||
                (!string.IsNullOrWhiteSpace(rdGroupUniforms.SelectedValue)) ||
                (!string.IsNullOrWhiteSpace(rdGroupMilitaryCourtesy.SelectedValue)) ||
                (!string.IsNullOrWhiteSpace(rdGroupDemeanor.SelectedValue)) ||
                (!string.IsNullOrWhiteSpace(rdGroupOnTimePrepared.SelectedValue)))
            {
                return true;
            }
            return false;
        }

        private void reportComplete()
        {
            PopulateReport();
            PopulateTextBoxesWithReportData();
            PopulateTextBoxesWithReportDataPostSec();
            DisableRadioButtonGroup();
            DisableTextTable(false);
            txtbxGeneralRemarks.Enabled = false;
            ddlEmail.Visible = false;
            divConfirm.Visible = false;
            divSaved.Visible = false;
            ddlPost.Visible = false;
            txtPost.Visible = true;
            lblEmail.Visible = false;
            txtPost.Enabled = false;
            txtDate.Enabled = false;
            txtDate.Text = Convert.ToDateTime(txtDate.Text).ToShortDateString();
            btnSave.Visible = false;
            btnExit.Visible = true;
            btnRejct.Visible = false;
            btnTableGenerate.Visible = false;
            btnPrint.Visible = true;
            txtbxVechilesInspected.Enabled = false;
            tbxPersonnelPresent.Enabled = false;


        }

        #endregion

        #region Reject Functions

        private void insertRejectReason()
        {
            SqlConnection mycon = new SqlConnection(_spConnect);
            mycon.Open();
            SqlCommand cmd = new SqlCommand("Update PostInspectionReport set GeneralRemarks = '" + txtbxreject.Text + "', ReportStatus='" + "Rejected" + "', ReportStage='" + "Awaiting Correction" + "' where ReportId= '" + Session["ReportId"] + "'", mycon);
            cmd.ExecuteNonQuery();
            mycon.Close();
        }

        private void formvalidation()
        {
            if (string.IsNullOrWhiteSpace(txtbxreject.Text))
            {
                lblGeneralRemarkRequired.Visible = true;
            }
            else
            {
                lblGeneralRemarkRequired.Visible = false;
            }
        }

        private void emptyremark()
        {
            txtbxGeneralRemarks.Text = " ";
            ddlEmail.Visible = false;
            lblEmail.Visible = false;
        }


        string rejectPS, rejectPC, rejectTC, rejectTO;
        protected void btnRejct_OnClick(object sender, EventArgs e)
        {
            //emptyremark();

            txtbxGeneralRemarks.Visible = false;
            lblGeneralRemarks.Text = "REJECT REASON";
            txtbxreject.Visible = true;
            ddlEmail.Visible = false;
            lblEmail.Visible = false;
            btnSubmit.Enabled = false;
            txtbxreject.Focus();

            var util = new Utils();
            rejectPC = util.GetTroopOfficerEmailList(txtTroop.Text).ToString();

            if (!string.IsNullOrWhiteSpace(txtbxreject.Text))
            {
                //emptyremark();
                //lblGeneralRemarkRequired.Style.Add("display", "block");

                //lblGeneralRemarkRequired.Visible = false;
                lblGeneralRemarkRequired.Style.Add("display", "none");

                //txtbxGeneralRemarks.Text = string.Empty;
                string maintroop = txtTroop.Text;
                string mainuser = User.Identity.Name.Substring(6);
                //ddlEmail.Visible = false;
                //lblEmail.Visible = false;

                //MessageBox.Show("Not Implemented", "NOT IMPLEMENTED", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                //Response.Redirect("PostInspectionReport.aspx", false);
                #region Getinfo
                SqlConnection rejectcon1 = new SqlConnection(_spConnect);
                rejectcon1.Open();
                SqlCommand rejectcmd = new SqlCommand("SELECT * FROM PostInspectionReport WHERE ReportId='" + Session["ReportId"] + "'", rejectcon1);
                SqlDataReader rejectreader = rejectcmd.ExecuteReader();
                while (rejectreader.Read())
                {
                    rejectPS = rejectreader["CreatedBy"].ToString(); // This is use to get the Post Secretary and Send Email ---CreatedBy
                    rejectPC = rejectreader["ModifiedBy"].ToString(); // This is use to get the Troop Officer and Send Email   ---ModifiedBy
                }
                rejectcon1.Close();

                SqlConnection rejectcon2 = new SqlConnection(_dbConn);
                rejectcon2.Open();
                SqlCommand rejectcmd2 = new SqlCommand("SELECT * FROM ViewInspection WHERE parentUnit='" + maintroop + "' AND EmployeeJobCode='" + "PSM022" + "'", rejectcon2);
                try
                {
                    //if (rejectcon2.State == System.Data.ConnectionState.Closed)
                    //{
                    //    rejectcon2.Open();
                    //}
                    SqlDataReader rejectreader2 = rejectcmd2.ExecuteReader();
                    while (rejectreader2.Read())
                    {
                        rejectTC = rejectreader2["sAMAccountName"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                rejectcon2.Close();


                SqlConnection rejectcon3 = new SqlConnection(_dbConn);
                rejectcon3.Open();
                SqlCommand rejectcmd3 = new SqlCommand("SELECT * FROM ViewInspection WHERE parentUnit='" + maintroop + "' AND EmployeeJobCode='" + "PSM021" + "'", rejectcon3);
                try
                {
                    SqlDataReader rejectreader3 = rejectcmd3.ExecuteReader();
                    while (rejectreader3.Read())
                    {
                        rejectTO = rejectreader3["sAMAccountName"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                rejectcon3.Close();

                #endregion

                SqlConnection connection2 = new SqlConnection(_dbConn);
                connection2.Open();
                SqlDataReader sqlDataReader2 = new SqlCommand("SELECT EmployeeJobCode,EmployeePostionNUmber FROM ViewInspection WHERE sAMAccountName='" + mainuser + "'", connection2).ExecuteReader();
                while (sqlDataReader2.Read())
                {
                    string str2 = sqlDataReader2["EmployeeJobCode"].ToString();
                    string Pstr = sqlDataReader2["EmployeePostionNUmber"].ToString();

                    if (str2 == "PSM021") //Troop Officer to  Post Secretary or Post Commander
                    {
                        #region MesaageTo_PS_&_PC

                        //if (hdrLabel.InnerText == "Post Secretary")
                        //{
                        //    string PS_email = rejectPS + "@gsp.net";
                        //    sendMessage(PS_email, lblReportId.Text);
                        //    insertRejectReason();
                        //    this.Response.Redirect("http://10.0.0.109/PostInspectionReport/PostInspectionGrid.aspx");
                        //}
                        //else if(hdrLabel.InnerText == "Post Commander")
                        //{
                        //    string PC_email = rejectPC + "@gsp.net";
                        //    sendMessage(PC_email, lblReportId.Text);
                        //    insertRejectReason();
                        //    this.Response.Redirect("http://10.0.0.109/PostInspectionReport/PostInspectionGrid.aspx");
                        //}

                        string PS_email = rejectPS + "@gsp.net";
                        sendMessage(PS_email, lblReportId.Text);
                        insertRejectReason();
                        this.Response.Redirect("http://10.0.0.109/PostInspectionReport/PostInspectionGrid.aspx");

                        #endregion
                    }
                    else if (str2 == "PSM022") //Troop Commander to Troop Officer to Post Commander or Post Secretary
                    {
                        #region MesaageTo_PS_&_PC_&_TO

                        string PS_email = rejectPS + "@gsp.net";
                        sendMessage(PS_email, lblReportId.Text);

                        string PC_email = rejectPC + "@gsp.net";
                        sendMessage(PC_email, lblReportId.Text);

                        string TO_email = rejectTO + "@gsp.net";
                        sendMessageWithoutLink(TO_email);
                        insertRejectReason();
                        this.Response.Redirect("http://10.0.0.109/PostInspectionReport/PostInspectionGrid.aspx");

                        #endregion
                    }
                    else if (Pstr == "00105296") //Major to Troop Commander to Troop Officer to Post Commander or Post Secretary
                    {
                        #region MesaageTo_PS_&_PC_&_TO_&_TC

                        string PS_email = rejectPS + "@gsp.net";
                        sendMessage(PS_email, lblReportId.Text);

                        string PC_email = rejectPC + "@gsp.net";
                        sendMessage(PC_email, lblReportId.Text);

                        string TO_email = rejectTO + "@gsp.net";
                        sendMessageWithoutLink(TO_email);

                        string TC_email = rejectTC + "@gsp.net";
                        sendMessageWithoutLink(TC_email);
                        insertRejectReason();
                        this.Response.Redirect("http://10.0.0.109/PostInspectionReport/PostInspectionGrid.aspx");

                        #endregion
                    }
                }
                connection2.Close();
            }
            else
            {
                lblGeneralRemarkRequired.Style.Add("display", "block");
                //lblGeneralRemarkRequired.Visible = true;
                //SetFocus(txtbxreject);
                txtbxreject.Focus();
            }

            #region OldCode

            //if (string.IsNullOrWhiteSpace(txtbxGeneralRemarks.Text))
            //{
            //    lblGeneralRemarkRequired.Style.Add("display", "block");
            //    //lblGeneralRemarkRequired.Visible = true;
            //    SetFocus(txtbxGeneralRemarks);
            //}
            //else
            //{
            //    emptyremark();
            //    lblGeneralRemarkRequired.Style.Add("display", "block");

            //    //lblGeneralRemarkRequired.Visible = false;
            //    lblGeneralRemarkRequired.Style.Add("display", "none");

            //    //txtbxGeneralRemarks.Text = string.Empty;
            //    string maintroop = txtTroop.Text;
            //    string mainuser = User.Identity.Name.Substring(6);
            //    ddlEmail.Visible = false;
            //    lblEmail.Visible = false;

            //    //MessageBox.Show("Not Implemented", "NOT IMPLEMENTED", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    //Response.Redirect("PostInspectionReport.aspx", false);
            //    #region Getinfo
            //    SqlConnection rejectcon1 = new SqlConnection(_spConnect);
            //    rejectcon1.Open();
            //    SqlCommand rejectcmd = new SqlCommand("SELECT * FROM PostInspectionReport WHERE ReportId='" + Session["ReportId"] + "'", rejectcon1);
            //    SqlDataReader rejectreader = rejectcmd.ExecuteReader();
            //    while (rejectreader.Read())
            //    {
            //        rejectPS = rejectreader["CreatedBy"].ToString(); // This is use to get the Post Secretary and Send Email ---CreatedBy
            //        rejectPC = rejectreader["ModifiedBy"].ToString(); // This is use to get the Troop Officer and Send Email   ---ModifiedBy
            //    }
            //    rejectcon1.Close();

            //    SqlConnection rejectcon2 = new SqlConnection(_dbConn);
            //    rejectcon2.Open();
            //    SqlCommand rejectcmd2 = new SqlCommand("SELECT * FROM ViewInspection WHERE parentUnit='" + maintroop + "' AND EmployeeJobCode='" + "PSM022" + "'", rejectcon2);
            //    try
            //    {
            //        //if (rejectcon2.State == System.Data.ConnectionState.Closed)
            //        //{
            //        //    rejectcon2.Open();
            //        //}
            //        SqlDataReader rejectreader2 = rejectcmd2.ExecuteReader();
            //        while (rejectreader2.Read())
            //        {
            //            rejectTC = rejectreader2["sAMAccountName"].ToString();
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        throw ex;
            //    }

            //    rejectcon2.Close();


            //    SqlConnection rejectcon3 = new SqlConnection(_dbConn);
            //    rejectcon3.Open();
            //    SqlCommand rejectcmd3 = new SqlCommand("SELECT * FROM ViewInspection WHERE parentUnit='" + maintroop + "' AND EmployeeJobCode='" + "PSM021" + "'", rejectcon3);
            //    try
            //    {
            //        SqlDataReader rejectreader3 = rejectcmd3.ExecuteReader();
            //        while (rejectreader3.Read())
            //        {
            //            rejectTO = rejectreader3["sAMAccountName"].ToString();
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        throw ex;
            //    }

            //    rejectcon3.Close();

            //    #endregion

            //    SqlConnection connection2 = new SqlConnection(_dbConn);
            //    connection2.Open();
            //    SqlDataReader sqlDataReader2 = new SqlCommand("SELECT EmployeeJobCode,EmployeePostionNUmber FROM ViewInspection WHERE sAMAccountName='" + mainuser + "'", connection2).ExecuteReader();
            //    while (sqlDataReader2.Read())
            //    {
            //        string str2 = sqlDataReader2["EmployeeJobCode"].ToString();
            //        string Pstr = sqlDataReader2["EmployeePostionNUmber"].ToString();

            //        if (str2 == "PSM021") //Troop Officer to  Post Secretary or Post Commander
            //        {
            //            #region MesaageTo_PS_&_PC

            //            string PS_email = rejectPS + "@gsp.net";
            //            sendMessage(PS_email);                    

            //            string PC_email = rejectPC + "@gsp.net";
            //            sendMessage(PC_email);
            //            insertRejectReason();
            //            this.Response.Redirect("http://10.0.0.109/PostInspectionReport/PostInspectionGrid.aspx");

            //            #endregion
            //        }
            //        else if (str2 == "PSM022") //Troop Commander to Troop Officer to Post Commander or Post Secretary
            //        {
            //            #region MesaageTo_PS_&_PC_&_TO

            //            string PS_email = rejectPS + "@gsp.net";
            //            sendMessage(PS_email);                    

            //            string PC_email = rejectPC + "@gsp.net";
            //            sendMessage(PC_email);

            //            string TO_email = rejectTO + "@gsp.net";
            //            sendMessage(TO_email);
            //            insertRejectReason();
            //            this.Response.Redirect("http://10.0.0.109/PostInspectionReport/PostInspectionGrid.aspx");

            //            #endregion
            //        }
            //        else if (Pstr == "00105296") //Major to Troop Commander to Troop Officer to Post Commander or Post Secretary
            //        {
            //            #region MesaageTo_PS_&_PC_&_TO_&_TC

            //            string PS_email = rejectPS + "@gsp.net";
            //            sendMessage(PS_email);                    

            //            string PC_email = rejectPC + "@gsp.net";
            //            sendMessage(PC_email);

            //            string TO_email = rejectTO + "@gsp.net";
            //            sendMessage(TO_email);

            //            string TC_email = rejectTC + "@gsp.net";
            //            sendMessage(TC_email);
            //            insertRejectReason();
            //            this.Response.Redirect("http://10.0.0.109/PostInspectionReport/PostInspectionGrid.aspx");

            //            #endregion
            //        }
            //    }
            //    connection2.Close();
            //}

            #endregion

        }

        private void sendMessage(string strEmailTo, string ReportID)
        {

            Utils utils = new Utils();
            utils.SendEmail(strEmailTo,ReportID);
            string str = "10.0.0.14";
            string from = "donotreply@gsp.net";
            //string messageText = "Inspection Report Test E-Mail\nPlease click the link below:\n\nhttp://10.0.0.109/PostInspectionReport/PostInspectionGrid.aspx  --- ReportID: " + ReportID;
            string messageText = "Inspection Report Test E-Mail\nPlease click the link below:\n\nhttp://localhost:51912/PostInspectionReport/PostInspectionGrid.aspx  --- ReportID: " + ReportID;
            string subject = "Inspection_Report_Approval_Email_Test";
            SmtpMail.SmtpServer = str;
            SmtpMail.Send(from, strEmailTo, subject, messageText);
        }

        private void sendMessageWithoutLink(string strEmailTo)
        {
            string str = "10.0.0.14";
            string from = "donotreply@gsp.net";
            string messageText = "Inspection Report Test E-Mail\nReport has been Rejected Post Secretary Will Submit\n\nREJECT REASON:\n" + txtbxreject.Text;
            string subject = "Inspection_Report_Reject_Email_Test";
            SmtpMail.SmtpServer = str;
            SmtpMail.Send(from, strEmailTo, subject, messageText);
        }

        #endregion


    }
}
