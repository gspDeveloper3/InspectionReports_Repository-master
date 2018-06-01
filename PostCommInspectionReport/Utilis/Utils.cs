using System;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Threading;
using System.Web.Mail;

namespace PostCommInspectionReport.Utilis
{
    public class Utils
    {
        const string ReportLink = "http://10.0.0.109/postinspectionreport/PostInspectionGrid.aspx";
        private static string GetConnectionString()
        {
            return Properties.Settings.Default.userConnect;
        }

        private static string GetReportConnectionString()
        {
            return Properties.Settings.Default.spConnect;
        }

        #region InsertPostComInspectionReport
        public void InsertPostInspectionReport(string troop, string post, string city, string inpsectionDate, string inpsectionMonth,
      string inpsectionYear, string recordsAndReports,
      string empPerformanceMgt, string schAndManAllocation,
      string evidenceProp, string postStructCondition, string postCleanliness,
      string lawnCondition, string furnitureAndApplicances, string motorVechicles, string vehiclesPresent, string postGenerator, string weapons, string personnelPresent, string uniformAndAppearance, string militaryCourtesy, string demeanorAndMorale, string onTimeAndPreparedForInspection,
            int crashes_CM, int crashes_AL, int crashes_SMLY, int crashes_YTD, int crashes_YTD_LY,
            int fatalities_CM, int fatalities_AL, int fatalities_SMLY, int fatalities_YTD, int fatalities_YTD_LY,
            int arrest_CM, int arrest_AL, int arrest_SMLY, int arrest_YTD, int arrest_YTD_LY,
            int warnings_CM, int warnings_AL, int warnings_SMLY, int warnings_YTD, int warnings_YTD_LY,
            int dUIArrest_CM, int dUIArrest_AL, int dUIArrest_SMLY, int dUIArrest_YTD, int dUIArrest_YTD_LY,
            int vechileStops_CM, int vechileStops_AL, int vechileStops_SMLY, int vechileStops_YTD, int vechileStops_YTD_LY,
            int sergeantsAssigned_CM, int sergeantsAssigned_SMLY,
            int corporalsAssigned_CM, int corporalsAssigned_SMLY,
            int troopersAssigned_CM, int troopersAssigned_SMLY,
            int secretaryAssigned_CM, int secretaryAssigned_SMLY,
            int leave_CM, int leave_SMLY,
            int detached_CM, int detached_SMLY,
            string generalRemarks,
            string createdBy, DateTime createdDate, string modifiedBy, DateTime modifiedDate, string reportID, string reportStatus, string reportStage)
        {
            //string reportID = troop + post + city + inpsectionDate + inpsectionMonth + inpsectionYear;
            using (SqlConnection dbConnection = new SqlConnection(GetReportConnectionString()))
            {
                try
                {
                    //get connection                 
                    SqlCommand cmd = new SqlCommand("spInsertPostInspectionReport", dbConnection);

                    cmd.CommandType = CommandType.StoredProcedure;


                    cmd.Parameters.Add(new SqlParameter("@Troop", troop));
                    cmd.Parameters.Add(new SqlParameter("@Post", post));
                    cmd.Parameters.Add(new SqlParameter("@City", city));
                    cmd.Parameters.Add(new SqlParameter("@inpsectionDate", inpsectionDate));
                    cmd.Parameters.Add(new SqlParameter("@InspectionMonth", inpsectionMonth));
                    cmd.Parameters.Add(new SqlParameter("@InspectionYear", inpsectionYear));
                    //ADMINISTRATION AND SUPERVISION
                    cmd.Parameters.Add(new SqlParameter("@RecordsAndReports", recordsAndReports));
                    cmd.Parameters.Add(new SqlParameter("@EmpPerformanceMgt", empPerformanceMgt));
                    cmd.Parameters.Add(new SqlParameter("@SchAndManAllocation", schAndManAllocation));
                    cmd.Parameters.Add(new SqlParameter("@EvidenceAndPropertyMgt", evidenceProp));

                    //BUILDINGS AND GROUNDS
                    cmd.Parameters.Add(new SqlParameter("@PostStructral", postStructCondition));
                    cmd.Parameters.Add(new SqlParameter("@PostClean", postCleanliness));
                    cmd.Parameters.Add(new SqlParameter("@LawnCondition", lawnCondition));
                    cmd.Parameters.Add(new SqlParameter("@Furniture", furnitureAndApplicances));
                    //EQUIPMENT
                    cmd.Parameters.Add(new SqlParameter("@MotorVehPatrol", motorVechicles));
                    cmd.Parameters.Add(new SqlParameter("@VehPresent", vehiclesPresent));
                    cmd.Parameters.Add(new SqlParameter("@PostGenerator", postGenerator));
                    cmd.Parameters.Add(new SqlParameter("@Weapons", weapons));
                    //PERSONNEL
                    cmd.Parameters.Add(new SqlParameter("@PersonnelPresent", personnelPresent));
                    cmd.Parameters.Add(new SqlParameter("@uniformAndAppearance", uniformAndAppearance));
                    cmd.Parameters.Add(new SqlParameter("@military", militaryCourtesy));
                    cmd.Parameters.Add(new SqlParameter("@demeanor", demeanorAndMorale));
                    cmd.Parameters.Add(new SqlParameter("@OntimePrepared", onTimeAndPreparedForInspection));
                    //CRASHES
                    cmd.Parameters.Add(new SqlParameter("@Crashes_CM", crashes_CM));
                    cmd.Parameters.Add(new SqlParameter("@Crashes_AL", crashes_AL));
                    cmd.Parameters.Add(new SqlParameter("@Crashes_SMLY", crashes_SMLY));
                    cmd.Parameters.Add(new SqlParameter("@Crashes_YTD", crashes_YTD));
                    cmd.Parameters.Add(new SqlParameter("@Crashes_YTD_LY", crashes_YTD_LY));
                    //FATALITIES
                    cmd.Parameters.Add(new SqlParameter("@Fatalities_CM", fatalities_CM));
                    cmd.Parameters.Add(new SqlParameter("@Fatalities_AL", fatalities_AL));
                    cmd.Parameters.Add(new SqlParameter("@Fatalities_SMLY", fatalities_SMLY));
                    cmd.Parameters.Add(new SqlParameter("@Fatalities_YTD", fatalities_YTD));
                    cmd.Parameters.Add(new SqlParameter("@Fatalities_YTD_LY", fatalities_YTD_LY));
                    //ARRESTS
                    cmd.Parameters.Add(new SqlParameter("@Arrest_CM", arrest_CM));
                    cmd.Parameters.Add(new SqlParameter("@Arrest_AL", arrest_AL));
                    cmd.Parameters.Add(new SqlParameter("@Arrest_SMLY", arrest_SMLY));
                    cmd.Parameters.Add(new SqlParameter("@Arrest_YTD", arrest_YTD));
                    cmd.Parameters.Add(new SqlParameter("@Arrest_YTD_LY", arrest_YTD_LY));
                    //WARNINGS
                    cmd.Parameters.Add(new SqlParameter("@Warnings_CM", warnings_CM));
                    cmd.Parameters.Add(new SqlParameter("@Warnings_AL", warnings_AL));
                    cmd.Parameters.Add(new SqlParameter("@Warnings_SMLY", warnings_SMLY));
                    cmd.Parameters.Add(new SqlParameter("@Warnings_YTD", warnings_YTD));
                    cmd.Parameters.Add(new SqlParameter("@Warnings_YTD_LY", warnings_YTD_LY));

                    //DUI ARREST
                    cmd.Parameters.Add(new SqlParameter("@DUIArrest_CM", dUIArrest_CM));
                    cmd.Parameters.Add(new SqlParameter("@DUIArrest_AL", dUIArrest_AL));
                    cmd.Parameters.Add(new SqlParameter("@DUIArrest_SMLY", dUIArrest_SMLY));
                    cmd.Parameters.Add(new SqlParameter("@DUIArrest_YTD", dUIArrest_YTD));
                    cmd.Parameters.Add(new SqlParameter("@DUIArrest_YTD_LY", dUIArrest_YTD_LY));

                    //VEHICLE STOPS
                    cmd.Parameters.Add(new SqlParameter("@VechileStops_CM", vechileStops_CM));
                    cmd.Parameters.Add(new SqlParameter("@VechileStops_AL", vechileStops_AL));
                    cmd.Parameters.Add(new SqlParameter("@VechileStops_SMLY", vechileStops_SMLY));
                    cmd.Parameters.Add(new SqlParameter("@VechileStops_YTD", vechileStops_YTD));
                    cmd.Parameters.Add(new SqlParameter("@VechileStops_YTD_LY", vechileStops_YTD_LY));
                    //SERGEANTS
                    cmd.Parameters.Add(new SqlParameter("@SergeantsAssigned_CM", secretaryAssigned_CM));
                    cmd.Parameters.Add(new SqlParameter("@SergeantsAssigned_SMLY", secretaryAssigned_SMLY));

                    //CORPORALS
                    cmd.Parameters.Add(new SqlParameter("@CorporalsAssigned_CM", corporalsAssigned_CM));
                    cmd.Parameters.Add(new SqlParameter("@CorporalsAssigned_SMLY", corporalsAssigned_SMLY));

                    //TROOPERS
                    cmd.Parameters.Add(new SqlParameter("@TroopersAssigned_CM", troopersAssigned_CM));
                    cmd.Parameters.Add(new SqlParameter("@TroopersAssigned_SMLY", troopersAssigned_SMLY));

                    //SECRETARY
                    cmd.Parameters.Add(new SqlParameter("@SecretaryAssigned_CM", secretaryAssigned_CM));
                    cmd.Parameters.Add(new SqlParameter("@SecretaryAssigned_SMLY", secretaryAssigned_SMLY));

                    //LEAVE
                    cmd.Parameters.Add(new SqlParameter("@Leave_CM", leave_CM));
                    cmd.Parameters.Add(new SqlParameter("@Leave_SMLY", leave_SMLY));

                    //DETACHED
                    cmd.Parameters.Add(new SqlParameter("@Detached_CM", detached_CM));
                    cmd.Parameters.Add(new SqlParameter("@Detached_SMLY", detached_SMLY));

                    //GENERAL REMARKS
                    cmd.Parameters.Add(new SqlParameter("@GeneralRemarks", generalRemarks));

                    //Created,Modified, ReportId
                    cmd.Parameters.Add(new SqlParameter("@CreatedBy", createdBy));
                    cmd.Parameters.Add(new SqlParameter("@Created", createdDate));
                    cmd.Parameters.Add(new SqlParameter("@ModifiedBy", modifiedBy));
                    cmd.Parameters.Add(new SqlParameter("@Modified", modifiedDate));
                    cmd.Parameters.Add(new SqlParameter("@ReportID", reportID));
                    cmd.Parameters.Add(new SqlParameter("@ReportStatus", reportStatus));
                    cmd.Parameters.Add(new SqlParameter("@ReportStage", reportStage));

                    dbConnection.Open();
                    // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
                    cmd.ExecuteNonQuery().ToString();

                }
                catch (SqlException ex)
                {
                    //TODO ADD HANDLING 
                    throw ex;
                    //alert.message(ex);
                }
                finally
                {
                    dbConnection.Close();
                }

            }

        }
        #endregion

        #region UpatePostComInspectionReport
        public void UpatePostComInspectionReport(string reportID, string troop, string post, string city, string recordsAndReports,
            string empPerformanceMgt, string schAndManAllocation,
            string evidenceProp, string postStructCondition, string postCleanliness,
            string lawnCondition, string furnitureAndApplicances, string motorVechicles, string vehiclesPresent, string postGenerator, string weapons, string personnelPresent, string uniformAndAppearance, string militaryCourtesy, string demeanorAndMorale, string onTimeAndPreparedForInspection, int crashes_CM, int crashes_AL, int crashes_SMLY, int crashes_YTD, int crashes_YTD_LY, int fatalities_CM, int fatalities_AL, int fatalities_SMLY, int fatalities_YTD, int fatalities_YTD_LY, int arrest_CM, int arrest_AL, int arrest_SMLY, int arrest_YTD, int arrest_YTD_LY, int warnings_CM, int warnings_AL, int warnings_SMLY, int warnings_YTD, int warnings_YTD_LY, int dUIArrest_CM, int dUIArrest_AL, int dUIArrest_SMLY, int dUIArrest_YTD, int dUIArrest_YTD_LY, int vechileStops_CM, int vechileStops_AL, int vechileStops_SMLY, int vechileStops_YTD, int vechileStops_YTD_LY, int sergeantsAssigned_CM, int sergeantsAssigned_SMLY, int corporalsAssigned_CM, int corporalsAssigned_SMLY, int troopersAssigned_CM, int troopersAssigned_SMLY,
            int secretaryAssigned_CM, int secretaryAssigned_SMLY, int leave_CM, int leave_SMLY, int detached_CM, int detached_SMLY, string generalRemarks, string modifiedBy, DateTime modifiedDate, string reportStatus, string reportStage)
        {
            using (SqlConnection dbConnection = new SqlConnection(GetReportConnectionString()))
            {
                try
                {
                    #region Parameter
                    //get connection                 
                    SqlCommand cmd = new SqlCommand("spUpdatePostInspectionReport", dbConnection);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@reportId", reportID));
                    cmd.Parameters.Add(new SqlParameter("@troop", troop));
                    cmd.Parameters.Add(new SqlParameter("@post", post));
                    cmd.Parameters.Add(new SqlParameter("@City", city));

                    //ADMINISTRATION AND SUPERVISION
                    cmd.Parameters.Add(new SqlParameter("@RecordsAndReports", recordsAndReports));
                    cmd.Parameters.Add(new SqlParameter("@EmpPerformanceMgt", empPerformanceMgt));
                    cmd.Parameters.Add(new SqlParameter("@SchAndManAllocation", schAndManAllocation));
                    cmd.Parameters.Add(new SqlParameter("@EvidenceAndPropertyMgt", evidenceProp));

                    //BUILDINGS AND GROUNDS
                    cmd.Parameters.Add(new SqlParameter("@PostStructral", postStructCondition));
                    cmd.Parameters.Add(new SqlParameter("@PostClean", postCleanliness));
                    cmd.Parameters.Add(new SqlParameter("@LawnCondition", lawnCondition));
                    cmd.Parameters.Add(new SqlParameter("@Furniture", furnitureAndApplicances));
                    //EQUIPMENT
                    cmd.Parameters.Add(new SqlParameter("@MotorVehPatrol", motorVechicles));
                    cmd.Parameters.Add(new SqlParameter("@VehPresent", vehiclesPresent));
                    cmd.Parameters.Add(new SqlParameter("@PostGenerator", postGenerator));
                    cmd.Parameters.Add(new SqlParameter("@Weapons", weapons));
                    //PERSONNEL
                    cmd.Parameters.Add(new SqlParameter("@PersonnelPresent", personnelPresent));
                    cmd.Parameters.Add(new SqlParameter("@uniformAndAppearance", uniformAndAppearance));
                    cmd.Parameters.Add(new SqlParameter("@military", militaryCourtesy));
                    cmd.Parameters.Add(new SqlParameter("@demeanor", demeanorAndMorale));
                    cmd.Parameters.Add(new SqlParameter("@OntimePrepared", onTimeAndPreparedForInspection));

                    //CRASHES
                    cmd.Parameters.Add(new SqlParameter("@Crashes_CM", crashes_CM));
                    cmd.Parameters.Add(new SqlParameter("@Crashes_AL", crashes_AL));
                    cmd.Parameters.Add(new SqlParameter("@Crashes_SMLY", crashes_SMLY));
                    cmd.Parameters.Add(new SqlParameter("@Crashes_YTD", crashes_YTD));
                    cmd.Parameters.Add(new SqlParameter("@Crashes_YTD_LY", crashes_YTD_LY));
                    //FATALITIES
                    cmd.Parameters.Add(new SqlParameter("@Fatalities_CM", fatalities_CM));
                    cmd.Parameters.Add(new SqlParameter("@Fatalities_AL", fatalities_AL));
                    cmd.Parameters.Add(new SqlParameter("@Fatalities_SMLY", fatalities_SMLY));
                    cmd.Parameters.Add(new SqlParameter("@Fatalities_YTD", fatalities_YTD));
                    cmd.Parameters.Add(new SqlParameter("@Fatalities_YTD_LY", fatalities_YTD_LY));
                    //ARRESTS
                    cmd.Parameters.Add(new SqlParameter("@Arrest_CM", arrest_CM));
                    cmd.Parameters.Add(new SqlParameter("@Arrest_AL", arrest_AL));
                    cmd.Parameters.Add(new SqlParameter("@Arrest_SMLY", arrest_SMLY));
                    cmd.Parameters.Add(new SqlParameter("@Arrest_YTD", arrest_YTD));
                    cmd.Parameters.Add(new SqlParameter("@Arrest_YTD_LY", arrest_YTD_LY));
                    //WARNINGS
                    cmd.Parameters.Add(new SqlParameter("@Warnings_CM", warnings_CM));
                    cmd.Parameters.Add(new SqlParameter("@Warnings_AL", warnings_AL));
                    cmd.Parameters.Add(new SqlParameter("@Warnings_SMLY", warnings_SMLY));
                    cmd.Parameters.Add(new SqlParameter("@Warnings_YTD", warnings_YTD));
                    cmd.Parameters.Add(new SqlParameter("@Warnings_YTD_LY", warnings_YTD_LY));

                    //DUI ARREST
                    cmd.Parameters.Add(new SqlParameter("@DUIArrest_CM", dUIArrest_CM));
                    cmd.Parameters.Add(new SqlParameter("@DUIArrest_AL", dUIArrest_AL));
                    cmd.Parameters.Add(new SqlParameter("@DUIArrest_SMLY", dUIArrest_SMLY));
                    cmd.Parameters.Add(new SqlParameter("@DUIArrest_YTD", dUIArrest_YTD));
                    cmd.Parameters.Add(new SqlParameter("@DUIArrest_YTD_LY", dUIArrest_YTD_LY));

                    //VEHICLE STOPS
                    cmd.Parameters.Add(new SqlParameter("@VechileStops_CM", vechileStops_CM));
                    cmd.Parameters.Add(new SqlParameter("@VechileStops_AL", vechileStops_AL));
                    cmd.Parameters.Add(new SqlParameter("@VechileStops_SMLY", vechileStops_SMLY));
                    cmd.Parameters.Add(new SqlParameter("@VechileStops_YTD", vechileStops_YTD));
                    cmd.Parameters.Add(new SqlParameter("@VechileStops_YTD_LY", vechileStops_YTD_LY));
                    //SERGEANTS
                    cmd.Parameters.Add(new SqlParameter("@SergeantsAssigned_CM", secretaryAssigned_CM));
                    cmd.Parameters.Add(new SqlParameter("@SergeantsAssigned_SMLY", secretaryAssigned_SMLY));

                    //CORPORALS
                    cmd.Parameters.Add(new SqlParameter("@CorporalsAssigned_CM", corporalsAssigned_CM));
                    cmd.Parameters.Add(new SqlParameter("@CorporalsAssigned_SMLY", corporalsAssigned_SMLY));

                    //TROOPERS
                    cmd.Parameters.Add(new SqlParameter("@TroopersAssigned_CM", troopersAssigned_CM));
                    cmd.Parameters.Add(new SqlParameter("@TroopersAssigned_SMLY", troopersAssigned_SMLY));

                    //SECRETARY
                    cmd.Parameters.Add(new SqlParameter("@SecretaryAssigned_CM", secretaryAssigned_CM));
                    cmd.Parameters.Add(new SqlParameter("@SecretaryAssigned_SMLY", secretaryAssigned_SMLY));

                    //LEAVE
                    cmd.Parameters.Add(new SqlParameter("@Leave_CM", leave_CM));
                    cmd.Parameters.Add(new SqlParameter("@Leave_SMLY", leave_SMLY));

                    //DETACHED
                    cmd.Parameters.Add(new SqlParameter("@Detached_CM", detached_CM));
                    cmd.Parameters.Add(new SqlParameter("@Detached_SMLY", detached_SMLY));

                    //GENERAL REMARKS
                    cmd.Parameters.Add(new SqlParameter("@GeneralRemarks", generalRemarks));
                    cmd.Parameters.Add(new SqlParameter("@ModifiedBy", modifiedBy));
                    cmd.Parameters.Add(new SqlParameter("@Modified", modifiedDate));
                    cmd.Parameters.Add(new SqlParameter("@ReportStatus", reportStatus));
                    cmd.Parameters.Add(new SqlParameter("@ReportStage", reportStage));


                    #endregion
                    dbConnection.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    //TODO: ADD EXCEPTION HANDLING
                    throw ex;
                }
                finally
                {
                    dbConnection.Close();
                }
            }
        }
        #endregion

        public DataSet GetReortSearchData(string troop, string reportID, string post)
        {

            DataSet dataSet = new DataSet();
            using (SqlConnection dbConnect = new SqlConnection(GetReportConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("spGetReportDataFromSearch", dbConnect);
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (!troop.Equals(string.Empty)) { cmd.Parameters.Add(new SqlParameter("@Troop", troop)); }
                    if (!post.Equals(string.Empty)) { cmd.Parameters.Add(new SqlParameter("@Post", post)); }
                    if (!reportID.Equals(string.Empty)) { cmd.Parameters.Add(new SqlParameter("@ReportID", reportID.Trim())); }

                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    dataAdapter.Fill(dataSet);

                }
                catch (SqlException ex)
                {
                    //Add exception handling

                    throw ex;
                }
                finally
                {
                    dbConnect.Close();
                }
            }

            return dataSet;
        }

        public DataSet ListPostByTroop(string troop)
        {

            DataSet dataSet = new DataSet();
            using (SqlConnection dbConnect = new SqlConnection(GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SELECT PostId, Post FROM [dps_common].[dbo].[PostTroop] WHERE TROOP = '" + troop + "'", dbConnect);
                    cmd.CommandType = CommandType.Text;
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    dataAdapter.Fill(dataSet);

                }
                catch (SqlException ex)
                {
                    //Add exception handling

                    throw ex;
                }
                finally
                {
                    dbConnect.Close();
                }
            }

            return dataSet;
        }

        public DataSet GetTroopCMDEmailList(string troop)
        {
            string employeeCode = "PSM022";
            DataSet dataSet = new DataSet();
            using (SqlConnection dbConnect = new SqlConnection(GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM [dps_common].[dbo].[ViewInspection] WHERE parentunit = '" + troop + "' AND EmployeeJobCode = '" + employeeCode + "'", dbConnect);
                    //  SqlCommand cmd = new SqlCommand("SELECT * FROM [dps_common].[dbo].[ViewInspection] WHERE EmployeeID='00803768'", dbConnect);
                    cmd.CommandType = CommandType.Text;
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    dataAdapter.Fill(dataSet);

                }
                catch (SqlException ex)
                {
                    //Add exception handling

                    throw ex;
                }
            }

            return dataSet;
        }

        public DataSet GetTroopOfficerEmailList(string troop)
        {
            string employeeCode_TO = "PSM021";
            string employeeCode_TC = "PSM022";
            DataSet dataSet = new DataSet();
            using (SqlConnection dbConnect = new SqlConnection(GetConnectionString()))
            {
                try
                {
                   
                    SqlCommand cmd = new SqlCommand("SELECT * FROM [dps_common].[dbo].[ViewInspection] WHERE parentunit = '" + troop + "' AND (EmployeeJobCode = '" + employeeCode_TO + "'" +" OR EmployeeJobCode = '" + employeeCode_TC + "')", dbConnect);
                    // SqlCommand cmd = new SqlCommand("SELECT * FROM [dps_common].[dbo].[ViewInspection] WHERE EmployeeID='00803768'", dbConnect);
                    cmd.CommandType = CommandType.Text;
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    dataAdapter.Fill(dataSet);

                }
                catch (SqlException ex)
                {
                    //Add exception handling

                    throw ex;
                }
            }

            return dataSet;
        }

        public DataSet GetMajorEmailList()
        {
            string postSec = "17817";
            string postionNumber = "00105296";
            //string postCMDR = "PSM020";
            //string troopOff = "PSM021";
            //string troopCMDR = "PSM021";
            DataSet dataSet = new DataSet();
            using (SqlConnection dbConnect = new SqlConnection(GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd =
                        new SqlCommand(
                            "SELECT * FROM [dps_common].[dbo].[ViewInspection] WHERE EmployeeJobCode = '" + postSec +
                            "' AND EmployeePostionNUmber='" + postionNumber + "'", dbConnect);
                    //+ "OR EmployeeJobCode = '" + postCMDR + "'"
                    //+ "OR EmployeeJobCode = '" + troopOff + "'"
                    //+ "OR EmployeeJobCode = '" + troopCMDR + "'", dbConnect);
                    // SqlCommand cmd = new SqlCommand("SELECT * FROM [dps_common].[dbo].[ViewInspection] WHERE EmployeeID='00803768'", dbConnect);
                    cmd.CommandType = CommandType.Text;
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    dataAdapter.Fill(dataSet);

                }
                catch (SqlException ex)
                {
                    //Add exception handling

                    throw ex;
                }
            }

            return dataSet;
        }

        public void SendEmail(string strEmailTo, string reportID)
        {

            string strSmtpServer = "10.0.0.14",
                strEmailFrom = "donotreply@gsp.net",
                strEmailBody =
                    string.Format(
                        "A Post InspectionReport requires your attention.  Please visit" + " " + "" + "{0}" + "" + " " + "the ReportID is:" + " " + "{1}" + " ",
                        ReportLink, reportID);
            string strEmailSubject = "Inspection_Report_Approval_Email";
            SmtpClient clientEmail = new SmtpClient(strSmtpServer);
            ThreadStart threadStart = delegate()
            {
                clientEmail.Send(strEmailFrom, strEmailTo, strEmailSubject, strEmailBody);
            };
            Thread thread = new Thread(threadStart);
            thread.Start();
            //SmtpMail.SmtpServer = strSmtpServer;
            //SmtpMail.Send(strEmailFrom, strEmailTo, strEmailSubject, strEmailBody);
            // SmtpMail.Send(strEmailFrom, strEmailTo, strEmailSubject, strEmailBody);
        }

        public bool IsReportInDataBase(string reportID, int rptType)
        {
            using (SqlConnection dbConnect = new SqlConnection(GetReportConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("spCheckIfReportExist", dbConnect);
                    cmd.CommandType = CommandType.StoredProcedure;

                    dbConnect.Open();

                    if (!reportID.Equals(string.Empty))
                    {
                        cmd.Parameters.Add(new SqlParameter("@ReportID", reportID));
                        cmd.Parameters.Add(new SqlParameter("@ReportType", rptType));
                    }
                    else
                    {
                        return false;
                    }


                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        return true;
                    }
                }
                catch (SqlException ex)
                {
                    //Add exception handling

                    throw ex;
                }
                finally
                {
                    dbConnect.Close();
                }

            }

            return false;
        }

        public string CheckReportStatus(string reportId)
        {
            var status = string.Empty;
            using (SqlConnection dbConnect = new SqlConnection(GetReportConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SELECT ReportStatus FROM [InspectionReports].[dbo].[PostInspectionReport] WHERE ReportId = '" + reportId + "'", dbConnect);
                    cmd.CommandType = CommandType.Text;

                    dbConnect.Open();
                    //cmd.Parameters.Add(new SqlParameter("@ReportID", reportId));
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {

                        status = reader["ReportStatus"].ToString();

                    }
                }
                catch (SqlException ex)
                {
                    //Add exception handling

                    throw ex;
                }
                finally
                {
                    dbConnect.Close();
                }
                return status;
            }
        }

        #region UnusedMethods
        /*
        public DataSet GetEmailListByEmployeeCode(string post, string employeeCode)
        {
            DataSet dataSet = new DataSet();
            using (SqlConnection dbConnect = new SqlConnection(GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM [dps_common].[dbo].[ViewInspection] WHERE unit = '" + post + "' AND EmployeeJobCode = '" + employeeCode + "'", dbConnect);
                    cmd.CommandType = CommandType.Text;
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    dataAdapter.Fill(dataSet);

                }
                catch (SqlException ex)
                {
                    //Add exception handling

                    throw ex;
                }
            }

            return dataSet;
        }
         * 
         *     public DataSet ListPostByTroop(int postID)
        {

            DataSet dataSet = new DataSet();
            using (SqlConnection dbConnect = new SqlConnection(GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SELECT City FROM ViewInspection WHERE PostId = '" + postID + "'", dbConnect);

                    cmd.CommandType = CommandType.Text;

                    //if (troop != string.Empty) { cmd.Parameters.Add(new SqlParameter("@Troop", troop)); }

                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    dataAdapter.Fill(dataSet);

                }
                catch (SqlException ex)
                {
                    //Add exception handling
                    throw ex;
                }
                finally
                {
                    dbConnect.Close();
                }
            }

            return dataSet;
        }
         * 
         * 
            public List<string> AuthorizedEmployeeJobCodeList()
        {                                             
                 List<string> jobCodeList = new List<string> { "GST051", "PSM020", "PSM021", "PSM022", "17817" };

                 return jobCodeList;
        }
      
         * 
         * 
           public DataSet GetPostCMDREmailList(string troop)
        {
            string employeeCode = "PSM020";
            DataSet dataSet = new DataSet();
            using (SqlConnection dbConnect = new SqlConnection(GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM [dps_common].[dbo].[ViewInspection] WHERE parentunit = '" + troop + "' AND EmployeeJobCode = '" + employeeCode + "'", dbConnect);
                    //SqlCommand cmd = new SqlCommand("SELECT * FROM [dps_common].[dbo].[ViewInspection] WHERE EmployeeID='00803768'", dbConnect);
                    cmd.CommandType = CommandType.Text;
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    dataAdapter.Fill(dataSet);

                }
                catch (SqlException ex)
                {
                    //Add exception handling

                    throw ex;
                }
            }

            return dataSet;
        }
         * 
         *      public string GetReportLink(string troop, string post, string city, string reportID)
        {
            string reportLink = "";
            using (SqlConnection dbConnection = new SqlConnection(GetReportConnectionString()))
            {
                try
                {
                    //get connection                 
                    SqlCommand cmd = new SqlCommand("spGetReportIDForEmailLink", dbConnection);

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@Troop", troop));
                    cmd.Parameters.Add(new SqlParameter("@Post", post));
                    cmd.Parameters.Add(new SqlParameter("@City", city));
                    cmd.Parameters.Add(new SqlParameter("@ReportID", reportID));

                }
                catch (SqlException ex)
                {
                    //Add exception handling

                    throw ex;
                }
                finally
                {
                    dbConnection.Close();

                }
            }
            return reportLink;
        }
       
        public DataSet GetEmailList(string post)
        {
            DataSet dataSet = new DataSet();
            using (SqlConnection dbConnect = new SqlConnection(GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM [dps_common].[dbo].[ViewInspection] WHERE unit = '" + post + "'", dbConnect);
                    cmd.CommandType = CommandType.Text;
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    dataAdapter.Fill(dataSet);

                }
                catch (SqlException ex)
                {
                    //Add exception handling

                    throw ex;
                }
            }

            return dataSet;
        }
        

        public DataSet GetPostSecEmailList(string post)
        {
            string employeeCode = "GST051";
            DataSet dataSet = new DataSet();
            using (SqlConnection dbConnect = new SqlConnection(GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM [dps_common].[dbo].[ViewInspection] WHERE unit = '" + post + "' AND EmployeeJobCode = '" + employeeCode + "'", dbConnect);
                    // SqlCommand cmd = new SqlCommand("SELECT * FROM [dps_common].[dbo].[ViewInspection] WHERE EmployeeID='00803768'", dbConnect);
                    cmd.CommandType = CommandType.Text;
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    dataAdapter.Fill(dataSet);

                }
                catch (SqlException ex)
                {
                    //Add exception handling

                    throw ex;
                }
            }

            return dataSet;
        } 
        */
        #endregion
    }

}