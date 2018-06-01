<%@ Page Title="Post Monthly Inspection Report" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PostInspectionReport.aspx.cs" Inherits="PostCommInspectionReport.PostInspectionReport" %>

<%@ Register Assembly="ASPNetSpell" Namespace="ASPNetSpell" TagPrefix="ASPNetSpell" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .divTable {
            display: table;
            width: 100%;
            
        }

        .divTableRow {
            display: table-row;
            background-color: #FFFFFF;
        }

        .divTableCell, .divTableHead {
            border: 1px solid #999999;
            display: table-cell;
            padding: 3px 10px;
        }

        .divTableCellScore {
            border: 1px solid #999999;
            display: table-cell;
            padding: 3px 10px;
            width: 207px;
        }

        .divTableBody {
            display: table-row-group;
            
        }

        .divTableCellTitle {
            font-weight: bold;
            margin-left: 7px;
        }

        h2 {
            font-size: 1.3em;
            color: #900000;
        }

        h3 {
            font-size: 1.0em;
            margin-top: 27px;
        }

        .button {
            background-color: #484848;
            border: none;
            color: white;
            padding: 7px 17px;
            text-align: center;
            text-decoration: none;
            display: inline-block;
            font-size: 16px;
            margin: 4px 2px;
            transition-duration: 0.4s;
            cursor: pointer;
        }

            .button:hover {
                background-color: #808080;
            }
        /**/
        .tableComparisons {
            display: none;
        }
    </style>

    <script type="text/javascript">
        function keyUP(txt) {            
            var ppR = document.getElementById("ContentPlaceHolder1_txtbxreject");
            if (ppR != "") {
                document.getElementById("ContentPlaceHolder1_lblGeneralRemarkRequired").textContent = "";
            }
            else {
                document.getElementById("ContentPlaceHolder1_lblGeneralRemarkRequired").style.display = 'block';
                //document.getElementById("ContentPlaceHolder1_lblGeneralRemarkRequired").textContent = "(*) Required";
            }
        }
    </script>
    
    <script type="text/javascript">
        function performCheck() {
            window.Page_ClientValidate("RadioButtonValidations");
            if (window.Page_IsValid) {
                alert('it is valid');
                return true;
            }
            else {
                alert('No valid');
                return false;
            }
        }
    </script>
    
    <script type="text/javascript">
        function isAllRadioBtnChecked() {
            var recordsAndReports = getElementsByName("ContentPlaceHolder1_rdGroupRecordsReports")[0];
        }
    </script>
     
    <h1 id="hdrLabel" runat="server" style="margin-left:100px">POST INSPECTION REPORT</h1>
    <p>
        <span style="margin-left:100px">Report ID:</span><asp:Label ID="lblReportId" runat="server" />
    </p>
    
    <table class="table table-bordered" style="border-color:black; background-color:white; width:1200px; margin: 0 auto;">
        <tbody>
            <tr>
                <td><span class="divTableCellTitle">Troop:&emsp;</span><asp:TextBox ID="txtTroop" runat="server" Width="150px" CssClass="form-control" Enabled="false" /></td>
                <td><span class="divTableCellTitle">Post:&emsp;</span><asp:DropDownList ID="ddlPost" runat="server" OnSelectedIndexChanged="ddlPost_SelectedIndexChanged" AppendDataBoundItems="true" Width="150px" CssClass="form-control" AutoPostBack="true" />
                    <asp:TextBox runat="server" ID="txtPost" CssClass="form-control" Width="250px" Visible="False"></asp:TextBox>
                </td>
               
                <td><span class="divTableCellTitle">City:&emsp;</span><asp:TextBox ID="txtCity" runat="server" Width="150px" CssClass="form-control" Enabled="false" /></td>
                <td><span class="divTableCellTitle">Date:&emsp;</span><asp:TextBox ID="txtDate" runat="server" Width="150px" CssClass="form-control" /><ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDate" /></td>
            </tr>
        </tbody>
    </table>

    <div id="divTable" style="margin: 0 auto;">
        
        <asp:Button ID="btnTableGenerate" runat="server" style="float:right; margin-right:100px;" Text="Generate Table" OnClick="btnTableGenereate_Click" Width="150px" CssClass="btn btn-primary" CausesValidation="False" /><br />
        <h2 style="margin-left:100px">Administration and Supervision:&nbsp;</h2>

        <table class="table table-bordered" style="border-color:black; background-color:white; width:1200px; margin: 0 auto;" id="table1">
            <tbody>
                <tr>
                    <td><label>RATE EACH OF THE FOLLOWING SUBCATEGORIES INDEPENDENTLY</label></td>
                    <td class="form-inline">
                        <label>Excellent</label>
                        &emsp;&emsp;<label>Very Satisfactory</label>
                        &emsp;&emsp;<label>Satisfactory</label>
                        &emsp;&emsp;<label>UnSatisfactory</label>                    
                    </td>
                </tr>
                <tr>
                    <td>Records and Reports &emsp;<asp:Label ID="lblRecordsRequire" ForeColor="#F44336" runat="server" style="font-weight:bolder" Visible="False">(*) Required</asp:Label></td>
                    <td class="form-inline">
                        <asp:RadioButtonList ID="rdGroupRecordsReports" runat="server" RepeatDirection="Horizontal" CellPadding="1" CellSpacing="1" Width="100%" ValidationGroup="RadioButtonValidations" >
                            <asp:ListItem  Value="Excellent">Excellent</asp:ListItem>
                            <asp:ListItem  Value="Very Satisfactory">Very Satisfactory</asp:ListItem>
                            <asp:ListItem Value="Satisfactory">Satisfactory</asp:ListItem>
                            <asp:ListItem Value="UnSatisfactory">UnSatisfactory</asp:ListItem>
                        </asp:RadioButtonList>  <asp:RequiredFieldValidator ID="rfvRecordsReports" runat="server" 
                                                                            ControlToValidate="rdGroupRecordsReports"  ForeColor="#F44336" ErrorMessage="Required Field"
                                                                            ValidationGroup="RadioButtonValidations" />
            
                    </td>
                </tr>
                <tr>
                    <td>Employee Performance Management &emsp;<asp:Label ID="lblEmployeePerMGTRequired" ForeColor="#F44336" runat="server" style="font-weight:bolder" Visible="False">(*) Required</asp:Label></td>
                    <td class="form-inline">
                        <asp:RadioButtonList ID="rdGroupEmployeePerformMGT" runat="server" RepeatDirection="Horizontal" CellPadding="1" CellSpacing="1" Width="100%" ValidationGroup="RadioButtonValidations" >
                            <asp:ListItem  Value="Excellent">Excellent</asp:ListItem>
                            <asp:ListItem  Value="Very Satisfactory">Very Satisfactory</asp:ListItem>
                            <asp:ListItem Value="Satisfactory">Satisfactory</asp:ListItem>
                            <asp:ListItem Value="UnSatisfactory">UnSatisfactory</asp:ListItem>
                        </asp:RadioButtonList>  <asp:RequiredFieldValidator ID="rfvEmployeePerfom" runat="server" 
                                                                            ControlToValidate="rdGroupEmployeePerformMGT" ForeColor="#F44336"  ErrorMessage="Required Field"
                                                                            ValidationGroup="RadioButtonValidations" />
                     
                    </td>
                </tr>
                <tr>
                    <td>Scheduling and Manpower Allocation &emsp;<asp:Label ID="lblSchedManRequired" ForeColor="#F44336" runat="server" style="font-weight:bolder" Visible="False">(*) Required</asp:Label></td>
                    <td class="form-inline">
                        <asp:RadioButtonList ID="rdGroupScheduleManPower" runat="server" RepeatDirection="Horizontal" CellPadding="1" CellSpacing="1" Width="100%" ValidationGroup="RadioButtonValidations" >
                            <asp:ListItem  Value="Excellent">Excellent</asp:ListItem>
                            <asp:ListItem  Value="Very Satisfactory">Very Satisfactory</asp:ListItem>
                            <asp:ListItem Value="Satisfactory">Satisfactory</asp:ListItem>
                            <asp:ListItem Value="UnSatisfactory">UnSatisfactory</asp:ListItem>
                        </asp:RadioButtonList> <asp:RequiredFieldValidator ID="rfvScheduleMan" runat="server" 
                                                                           ControlToValidate="rdGroupScheduleManPower" ForeColor="#F44336"  ErrorMessage="Required Field"
                                                                           ValidationGroup="RadioButtonValidations" />
                        <%--<h2 style="margin-left:100px">General Remarks</h2>--%>
                    </td>
                </tr>
                <tr>
                    <td>Evidence and Property Management &emsp;<asp:Label ID="lblEvidenceRequired" ForeColor="#F44336" runat="server" style="font-weight:bolder" Visible="False">(*) Required</asp:Label></td>
                    <td class="form-inline">
                        <asp:RadioButtonList ID="rdGroupEvidencePropMgt" runat="server" RepeatDirection="Horizontal" CellPadding="1" CellSpacing="1" Width="100%" ValidationGroup="RadioButtonValidations" >
                            <asp:ListItem  Value="Excellent">Excellent</asp:ListItem>
                            <asp:ListItem  Value="Very Satisfactory">Very Satisfactory</asp:ListItem>
                            <asp:ListItem Value="Satisfactory">Satisfactory</asp:ListItem>
                            <asp:ListItem Value="UnSatisfactory">UnSatisfactory</asp:ListItem>
                        </asp:RadioButtonList><asp:RequiredFieldValidator ID="rfvEvidence" runat="server" 
                                                                            ControlToValidate="rdGroupEvidencePropMgt"  ForeColor="#F44336" ErrorMessage="Required Field"
                                                                            ValidationGroup="RadioButtonValidations" />
           
                    </td>
                </tr>
            </tbody>
        </table>
        <p></p>

        <%--<h2 style="margin-left:100px">General Remarks</h2>--%>
        <h2 style="margin-left:100px">Buildings and Grounds:&nbsp;</h2>

        <table class="table table-bordered" style="border-color:black; background-color:white; width:1200px; margin: 0 auto;">
            <tbody>
                <tr>
                    <td><label>RATE EACH OF THE FOLLOWING SUBCATEGORIES INDEPENDENTLY</label></td>
                    <td class="form-inline">
                        <label>Excellent</label>
                        &emsp;&emsp;<label>Very Satisfactory</label>
                        &emsp;&emsp;<label>Satisfactory</label>
                        &emsp;&emsp;<label>UnSatisfactory</label>                    
                    </td>
                </tr>
                <tr>
                    <td>Post Structural Condition &emsp;<asp:Label ID="lblPostStructRequired" ForeColor="#F44336" runat="server" style="font-weight:bolder" Visible="False">(*) Required</asp:Label></td>
                    <td class="form-inline">
                        <asp:RadioButtonList ID="rdGroupPostStructural" runat="server" RepeatDirection="Horizontal" CellPadding="1" CellSpacing="1" Width="100%" ValidationGroup="RadioButtonValidations" >
                            <asp:ListItem  Value="Excellent">Excellent</asp:ListItem>
                            <asp:ListItem  Value="Very Satisfactory">Very Satisfactory</asp:ListItem>
                            <asp:ListItem Value="Satisfactory">Satisfactory</asp:ListItem>
                            <asp:ListItem Value="UnSatisfactory">UnSatisfactory</asp:ListItem>
                        </asp:RadioButtonList>  <asp:RequiredFieldValidator ID="rfVPostStruct" runat="server" 
                                                                            ControlToValidate="rdGroupPostStructural"  ForeColor="#F44336" ErrorMessage="Required Field"
                                                                            ValidationGroup="RadioButtonValidations" />
             
                    </td>
                </tr>
                <tr>
                    <td>Post Cleanliness and Maintenance &emsp;<asp:Label ID="lblPostCleanRequired" ForeColor="#F44336" runat="server" style="font-weight:bolder" Visible="False">(*) Required</asp:Label></td>
                    <td class="form-inline">
                        <asp:RadioButtonList ID="rdGrouptPostCleanliness" runat="server" RepeatDirection="Horizontal" CellPadding="1" CellSpacing="1" Width="100%" ValidationGroup="RadioButtonValidations" >
                            <asp:ListItem  Value="Excellent">Excellent</asp:ListItem>
                            <asp:ListItem  Value="Very Satisfactory">Very Satisfactory</asp:ListItem>
                            <asp:ListItem Value="Satisfactory">Satisfactory</asp:ListItem>
                            <asp:ListItem Value="UnSatisfactory">UnSatisfactory</asp:ListItem>
                        </asp:RadioButtonList> <asp:RequiredFieldValidator ID="rfvPostClean" runat="server" 
                                                                           ControlToValidate="rdGrouptPostCleanliness"  ForeColor="#F44336" ErrorMessage="Required Field"
                                                                           ValidationGroup="RadioButtonValidations" />
                        <%--<h2 style="margin-left:100px">General Remarks</h2>--%>
                    </td>
                </tr>
                <tr>
                    <td>Lawn Condition &emsp;<asp:Label ID="lblLawnRequired" ForeColor="#F44336" runat="server" style="font-weight:bolder" Visible="False">(*) Required</asp:Label></td>
                    <td class="form-inline">
                        <asp:RadioButtonList ID="rdGroupLawnCondition" runat="server" RepeatDirection="Horizontal" CellPadding="1" CellSpacing="1" Width="100%" ValidationGroup="RadioButtonValidations" >
                            <asp:ListItem  Value="Excellent">Excellent</asp:ListItem>
                            <asp:ListItem  Value="Very Satisfactory">Very Satisfactory</asp:ListItem>
                            <asp:ListItem Value="Satisfactory">Satisfactory</asp:ListItem>
                            <asp:ListItem Value="UnSatisfactory">UnSatisfactory</asp:ListItem>
                        </asp:RadioButtonList>
                        <asp:RequiredFieldValidator ID="rfvRDLawn" runat="server" 
                                                    ControlToValidate="rdGroupLawnCondition"  ForeColor="#F44336" ErrorMessage="Required Field"
                                                    ValidationGroup="RadioButtonValidations" />
                        
                    </td>
                </tr>
                <tr>
                    <td>Furniture and Appliances &emsp;<asp:Label ID="lblFurnitureRequired" ForeColor="#F44336" runat="server" style="font-weight:bolder" Visible="False">(*) Required</asp:Label></td>
                    <td class="form-inline">
                        <asp:RadioButtonList ID="rdGroupFurniture" runat="server" RepeatDirection="Horizontal" CellPadding="1" CellSpacing="1" Width="100%" ValidationGroup="RadioButtonValidations" >
                            <asp:ListItem  Value="Excellent">Excellent</asp:ListItem>
                            <asp:ListItem  Value="Very Satisfactory">Very Satisfactory</asp:ListItem>
                            <asp:ListItem Value="Satisfactory">Satisfactory</asp:ListItem>
                            <asp:ListItem Value="UnSatisfactory">UnSatisfactory</asp:ListItem>
                        </asp:RadioButtonList>
                        <asp:RequiredFieldValidator ID="rfvFurniture" runat="server" 
                                                    ControlToValidate="rdGroupFurniture"  ForeColor="#F44336" ErrorMessage="Required Field"
                                                    ValidationGroup="RadioButtonValidations" />
                        <%--<h2 style="margin-left:100px">General Remarks</h2>--%>
                    </td>
                </tr>
            </tbody>
        </table><p></p>

        <div class="form-inline">
            <h2 style="float:left; margin-left:100px;"> Equipment:</h2>
            <asp:TextBox ID="txtbxVechilesInspected" runat="server" TextMode="MultiLine" Width="400px" CssClass="form-control"  style="float:right; margin-right: 400px;" /><asp:RequiredFieldValidator ID="rfvVechilesInspected" ControlToValidate="txtbxVechilesInspected" runat="server" ForeColor="Red" ErrorMessage="(*) Required" /><span style="float:right; margin-right:10px; margin-top:12px;">Vehicles Inspected:</span>
        </div>
        <p></p><br /><p></p><br /><p></p>
        

        <table class="table table-bordered" style="border-color:black; background-color:white; width:1200px; margin: 0 auto;">
            <tbody>
                <tr>
                    <td><label>RATE EACH OF THE FOLLOWING SUBCATEGORIES INDEPENDENTLY</label></td>
                    <td class="form-inline">
                        <label>Excellent</label>
                        &emsp;&emsp;<label>Very Satisfactory</label>
                        &emsp;&emsp;<label>Satisfactory</label>
                        &emsp;&emsp;<label>UnSatisfactory</label>                    
                    </td>
                </tr>
                <tr>
                    <td>Motor Vehicles (Maintenance and Readiness for Patrol) &emsp;<asp:Label ID="lblMotorVehRequired" ForeColor="#F44336" runat="server" style="font-weight:bolder" Visible="False">(*) Required</asp:Label></td>
                    <asp:Label ID="lblMotorVehic" runat="server" ForeColor="Red" Text="Label" Visible="False"></asp:Label>
                    <td class="form-inline">
                        <asp:RadioButtonList ID="rdGroupMotorVehicles" runat="server" RepeatDirection="Horizontal" CellPadding="1" CellSpacing="1" Width="100%" ValidationGroup="RadioButtonValidations" >
                            <asp:ListItem  Value="Excellent">Excellent</asp:ListItem>
                            <asp:ListItem  Value="Very Satisfactory">Very Satisfactory</asp:ListItem>
                            <asp:ListItem Value="Satisfactory">Satisfactory</asp:ListItem>
                            <asp:ListItem Value="UnSatisfactory">UnSatisfactory</asp:ListItem>
                        </asp:RadioButtonList> <asp:RequiredFieldValidator ID="rfvVehicleMaint" runat="server" 
                                                                           ControlToValidate="rdGroupMotorVehicles"  ForeColor="#F44336" ErrorMessage="Required Field"
                                                                           ValidationGroup="RadioButtonValidations" />
                        <%--<h2 style="margin-left:100px">General Remarks</h2>--%>
                    </td>
                </tr>
                <tr>
                    <td>Post Generator (Maintenance and Test Operations) &emsp;<asp:Label ID="lblPostGenRequired" ForeColor="#F44336" runat="server" style="font-weight:bolder" Visible="False">(*) Required</asp:Label></td><asp:Label ID="lblPostGenerator" runat="server" ForeColor="Red" Text="Label" Visible="False"></asp:Label>
                    <td class="form-inline">
                        <asp:RadioButtonList ID="rdGroupPostGenerator" runat="server" RepeatDirection="Horizontal" CellPadding="1" CellSpacing="1" Width="100%" ValidationGroup="RadioButtonValidations" >
                            <asp:ListItem  Value="Excellent">Excellent</asp:ListItem>
                            <asp:ListItem  Value="Very Satisfactory">Very Satisfactory</asp:ListItem>
                            <asp:ListItem Value="Satisfactory">Satisfactory</asp:ListItem>
                            <asp:ListItem Value="UnSatisfactory">UnSatisfactory</asp:ListItem>
                        </asp:RadioButtonList> <asp:RequiredFieldValidator ID="rfvPostGen" runat="server" 
                                                                           ControlToValidate="rdGroupPostGenerator"  ForeColor="#F44336" ErrorMessage="Required Field"
                                                                           ValidationGroup="RadioButtonValidations" />
                    </td>
                </tr>
                <tr>
                    <td>Weapons &emsp;<asp:Label ID="lblWeaponsRequired" ForeColor="#F44336" runat="server" style="font-weight:bolder" Visible="False">(*) Required</asp:Label></td><asp:Label ID="lblWeapons" runat="server" ForeColor="Red" Text="Label" Visible="False"></asp:Label>
                    <td class="form-inline">
                        <asp:RadioButtonList ID="rdGroupWeapons" runat="server" RepeatDirection="Horizontal" CellPadding="1" CellSpacing="1" Width="100%" ValidationGroup="RadioButtonValidations" >
                            <asp:ListItem  Value="Excellent">Excellent</asp:ListItem>
                            <asp:ListItem  Value="Very Satisfactory">Very Satisfactory</asp:ListItem>
                            <asp:ListItem Value="Satisfactory">Satisfactory</asp:ListItem>
                            <asp:ListItem Value="UnSatisfactory">UnSatisfactory</asp:ListItem>
                        </asp:RadioButtonList><asp:RequiredFieldValidator ID="rfvWeapons" runat="server" 
                                                                          ControlToValidate="rdGroupWeapons"  ForeColor="#F44336" ErrorMessage="Required Field"
                                                                          ValidationGroup="RadioButtonValidations" />
                        <%--<h2 style="margin-left:100px">General Remarks</h2>--%>
                    </td>
                </tr>
            </tbody>
        </table><br />

        <div class="form-inline">
            <h2 style="float:left; margin-left:100px;"> Personnel:</h2>
            <asp:TextBox ID="tbxPersonnelPresent" runat="server" TextMode="MultiLine" Width="400px" CssClass="form-control"  style="float:right; margin-right: 400px;" /><span style="float:right; margin-right:10px; margin-top:12px;">Personnel Present:</span><asp:RequiredFieldValidator ID="rfvPersonnelPresent" ControlToValidate="tbxPersonnelPresent" runat="server" ForeColor="Red" ErrorMessage="(*) Required" />
        </div>
        <p></p><br /><p></p><br /><p></p>

        <table class="table table-bordered" style="border-color:black; background-color:white; width:1200px; margin: 0 auto;">
            <tbody>
                <tr>
                    <td><label>RATE EACH OF THE FOLLOWING SUBCATEGORIES INDEPENDENTLY</label></td>
                    <td class="form-inline">
                        <label>Excellent</label>
                        &emsp;&emsp;<label>Very Satisfactory</label>
                        &emsp;&emsp;<label>Satisfactory</label>
                        &emsp;&emsp;<label>UnSatisfactory</label>                    
                    </td>
                </tr>
                <tr>
                    <td>Uniforms and Appearance &emsp;<asp:Label ID="lblOntimeRequire" ForeColor="#F44336" runat="server" style="font-weight:bolder" Visible="False">(*) Required</asp:Label></td>
                    <asp:Label ID="lblUniforms" runat="server" ForeColor="Red" Text="Label" Visible="False"></asp:Label>
                    <td class="form-inline">
                        <asp:RadioButtonList ID="rdGroupUniforms" runat="server" RepeatDirection="Horizontal" CellPadding="1" CellSpacing="1" Width="100%" >
                            <asp:ListItem  Value="Excellent">Excellent</asp:ListItem>
                            <asp:ListItem  Value="Very Satisfactory">Very Satisfactory</asp:ListItem>
                            <asp:ListItem Value="Satisfactory">Satisfactory</asp:ListItem>
                            <asp:ListItem Value="UnSatisfactory">UnSatisfactory</asp:ListItem>
                        </asp:RadioButtonList><asp:RequiredFieldValidator ID="rfvUniforms" runat="server" 
                                                                          ControlToValidate="rdGroupUniforms"  ForeColor="#F44336" ErrorMessage="Required Field"
                                                                          ValidationGroup="RadioButtonValidations" />
                        
                    </td>
                </tr>
                <tr>
                    <td>Military Courtesy &emsp;<asp:Label ID="Label17" ForeColor="#F44336" runat="server" style="font-weight:bolder" Visible="False">(*) Required</asp:Label></td>
                    <asp:Label ID="lblMilitary" runat="server" ForeColor="Red" Text="Label" Visible="False"></asp:Label>
                    <td class="form-inline">
                        <asp:RadioButtonList ID="rdGroupMilitaryCourtesy" runat="server" RepeatDirection="Horizontal" CellPadding="1" CellSpacing="1" Width="100%" >
                            <asp:ListItem  Value="Excellent">Excellent</asp:ListItem>
                            <asp:ListItem  Value="Very Satisfactory">Very Satisfactory</asp:ListItem>
                            <asp:ListItem Value="Satisfactory">Satisfactory</asp:ListItem>
                            <asp:ListItem Value="UnSatisfactory">UnSatisfactory</asp:ListItem>
                        </asp:RadioButtonList><asp:RequiredFieldValidator ID="rfvMilitaryCourt" runat="server" 
                                                                          ControlToValidate="rdGroupMilitaryCourtesy"  ForeColor="#F44336" ErrorMessage="Required Field"
                                                                          ValidationGroup="RadioButtonValidations" />
                        <%--<h2 style="margin-left:100px">General Remarks</h2>--%>
                    </td>
                </tr>
                <tr>
                    <td>Demeanor and Morale &emsp;<asp:Label ID="lblDemeanorRequired" ForeColor="#F44336" runat="server" style="font-weight:bolder" Visible="False">(*) Required</asp:Label></td>
                    <asp:Label ID="lblDemeanor" runat="server" ForeColor="Red" Text="Label" Visible="False"></asp:Label>
                    <td class="form-inline">
                        <asp:RadioButtonList ID="rdGroupDemeanor" runat="server" RepeatDirection="Horizontal" CellPadding="1" CellSpacing="1" Width="100%" >
                            <asp:ListItem  Value="Excellent">Excellent</asp:ListItem>
                            <asp:ListItem  Value="Very Satisfactory">Very Satisfactory</asp:ListItem>
                            <asp:ListItem Value="Satisfactory">Satisfactory</asp:ListItem>
                            <asp:ListItem Value="UnSatisfactory">UnSatisfactory</asp:ListItem>
                        </asp:RadioButtonList><asp:RequiredFieldValidator ID="rfvDemeanor" runat="server" 
                                                                          ControlToValidate="rdGroupDemeanor"  ForeColor="#F44336" ErrorMessage="Required Field"
                                                                          ValidationGroup="RadioButtonValidations" />
                        
                    </td>
                </tr>
                <tr>
                    <td>On Time and Prepared for Inspection &emsp;<asp:Label ID="lblOntimeRequired" ForeColor="#F44336" runat="server" style="font-weight:bolder" Visible="False">(*) Required</asp:Label></td>
                    <asp:Label ID="lblOntime" runat="server" ForeColor="Red" Text="Label" Visible="False"></asp:Label>
                    <td class="form-inline">
                        <asp:RadioButtonList ID="rdGroupOnTimePrepared" runat="server" RepeatDirection="Horizontal" CellPadding="1" CellSpacing="1" Width="100%" >
                            <asp:ListItem  Value="Excellent">Excellent</asp:ListItem>
                            <asp:ListItem  Value="Very Satisfactory">Very Satisfactory</asp:ListItem>
                            <asp:ListItem Value="Satisfactory">Satisfactory</asp:ListItem>
                            <asp:ListItem Value="UnSatisfactory">UnSatisfactory</asp:ListItem>
                        </asp:RadioButtonList><asp:RequiredFieldValidator ID="rfvOntime" runat="server" 
                                                                          ControlToValidate="rdGroupOnTimePrepared"  ForeColor="#F44336" ErrorMessage="Required Field"
                                                                          ValidationGroup="RadioButtonValidations" />
                        <%--<h2 style="margin-left:100px">General Remarks</h2>--%>
                    </td>
                </tr>
            </tbody>
        </table><br />

        <h2 style="margin-left:100px">Activities and Personnel Comparisons</h2><br />
        <div class="form-inline">
            <div style="float:left; margin-left:100px">
                <table class="table table-bordered" style="border-color:black; background-color:white; margin: 0 auto;">
                    <tbody>
                        <tr>
                            <td>Activites</td>
                            <td>Current Month</td>
                            <td style="width:70px">Same Month Last Year</td>
                            <td style="width:70px">YTD Current Year</td>
                            <td style="width:70px">YTD Last Year</td>
                            <td style="width:70px">Change From LY</td>
                            <td style="width:70px">Leader Activity</td>
                        </tr>
                        <tr>
                            <td>Crashes</td>
                            <td><asp:TextBox ID="txtbxCrashes_CM" runat="server" Width="70px" CssClass="form-control" onkeypress="return isNumberKey(event)" /></td>
                            <td><asp:TextBox ID="txtbxCrashes_SMLY" runat="server" Width="70px" CssClass="form-control"  onkeypress="return isNumberKey(event)" /></td>
                            <td><asp:TextBox ID="txtbxCrashes_YTD" runat="server" Width="70px" CssClass="form-control"  AutoPostBack="True"  OnTextChanged="txtbxCrashes_YTD_OnTextChanged" onkeypress="return isNumberKey(event)" /></td>

                            <td><asp:TextBox ID="txtbxCrashes_YTD_LY" runat="server" Width="70px" CssClass="form-control" AutoPostBack="True"  OnTextChanged="txtbxCrashes_YTD_OnTextChanged" onkeypress="return isNumberKey(event)" /></td>
                            <td><asp:TextBox ID="txtCrashesChange" runat="server" Width="70px" CssClass="form-control" Enabled="False"/></td>
                            <td><asp:TextBox ID="txtbxCrashes_AL" runat="server" Width="70px" CssClass="form-control" onkeypress="return isNumberKey(event)" /></td>
                        </tr>
                        <tr>
                            <td>Fatalities</td>
                            <td><asp:TextBox ID="txtbxFatalities_CM" runat="server" Width="70px" CssClass="form-control" onkeypress="return isNumberKey(event)" /></td>
                            <td><asp:TextBox ID="txtbxFatalities_SMLY" runat="server" Width="70px" CssClass="form-control" onkeypress="return isNumberKey(event)" /></td>
                            <td><asp:TextBox ID="txtbxFatalities_YTD" runat="server" Width="70px" CssClass="form-control"  onkeypress="return isNumberKey(event)"  OnTextChanged="txtbxFatalities_YTD_LY_OnTextChanged"  AutoPostBack="True"/></td>
                            <td><asp:TextBox ID="txtbxFatalities_YTD_LY" runat="server" Width="70px" CssClass="form-control"  onkeypress="return isNumberKey(event)" OnTextChanged="txtbxFatalities_YTD_LY_OnTextChanged" AutoPostBack="True" /></td>
                            <td><asp:TextBox ID="txtbxFatalitiesChange" runat="server" Width="70px" CssClass="form-control" onkeypress="return isNumberKey(event)" Enabled="False" /></td>
                            <td><asp:TextBox ID="txtbxFatalities_AL" runat="server" Width="70px" CssClass="form-control" onkeypress="return isNumberKey(event)" /></td>
                        </tr>
                        <tr>
                            <td>Arrests</td>
                            <td><asp:TextBox ID="txtbxArrests_CM" runat="server" Width="70px" CssClass="form-control" onkeypress="return isNumberKey(event)" /></td>
                            <td><asp:TextBox ID="txtbxArrests_SMLY" runat="server" Width="70px" CssClass="form-control" onkeypress="return isNumberKey(event)" /></td>
                            <td><asp:TextBox ID="txtbxArrests_YTD" runat="server" Width="70px" OnTextChanged="txtbxArrests_YTD_OnTextChanged" AutoPostBack="True" CssClass="form-control" onkeypress="return isNumberKey(event)" /></td>
                            <td><asp:TextBox ID="txtbxArrests_YTD_LY" runat="server" Width="70px" CssClass="form-control" OnTextChanged="txtbxArrests_YTD_OnTextChanged" AutoPostBack="True"  onkeypress="return isNumberKey(event)" /></td>
                            <td><asp:TextBox ID="txtArrestChange" runat="server" Width="70px" CssClass="form-control" Enabled="False" /></td>
                            <td><asp:TextBox ID="txtbxArrests_AL" runat="server" Width="70px" CssClass="form-control" onkeypress="return isNumberKey(event)" /></td>
                        </tr>
                        <tr>
                            <td>Warnings</td>
                            <td><asp:TextBox ID="txtbxWarnings_CM" runat="server" Width="70px" CssClass="form-control" onkeypress="return isNumberKey(event)" /></td>
                            <td> <asp:TextBox ID="txtbxWarnings_SMLY" runat="server" Width="70px" CssClass="form-control" onkeypress="return isNumberKey(event)" /></td>
                            <td><asp:TextBox ID="txtbxWarnings_YTD" runat="server" Width="70px" CssClass="form-control" AutoPostBack="True" OnTextChanged="txtbxWarnings_YTD_OnTextChanged" onkeypress="return isNumberKey(event)" /></td>
                            <td><asp:TextBox ID="txtbxWarnings_YTD_LY" runat="server" Width="70px" CssClass="form-control"  AutoPostBack="True" OnTextChanged="txtbxWarnings_YTD_OnTextChanged" onkeypress="return isNumberKey(event)" /></td>
                            <td><asp:TextBox ID="txtWarningsChange" runat="server" Width="70px" CssClass="form-control" Enabled="False" /></td>
                            <td><asp:TextBox ID="txtbxWarnings_AL" runat="server" Width="70px" CssClass="form-control" onkeypress="return isNumberKey(event)" /></td>
                        </tr> 
                        <tr>
                            <td>DUI Arrest</td>
                            <td><asp:TextBox ID="txtbxDUIArrest_CM" runat="server" Width="70px" CssClass="form-control" onkeypress="return isNumberKey(event)" /></td>
                            <td><asp:TextBox ID="txtbxDUIArrest_SMLY" runat="server" Width="70px" CssClass="form-control" onkeypress="return isNumberKey(event)" /></td>
                            <td><asp:TextBox ID="txtbxDUIArrest_YTD" runat="server" Width="70px" CssClass="form-control" AutoPostBack="True" OnTextChanged="txtbxDUIArrest_YTD_OnTextChanged" onkeypress="return isNumberKey(event)" /></td>
                            <td><asp:TextBox ID="txtbxDUIArrest_YTD_LY" runat="server" Width="70px" CssClass="form-control"  AutoPostBack="True" OnTextChanged="txtbxDUIArrest_YTD_OnTextChanged" onkeypress="return isNumberKey(event)" /></td>
                            <td><asp:TextBox ID="txtDUIArrestChange" runat="server" Width="70px" CssClass="form-control"  Enabled="False" /></td>
                            <td><asp:TextBox ID="txtbxDUIArrest_AL" runat="server" Width="70px" CssClass="form-control" onkeypress="return isNumberKey(event)" /></td>
                        </tr> 
                        <tr>
                            <td>Vechile Stops</td>
                            <td><asp:TextBox ID="txtbxVechileStops_CM" runat="server" Width="70px" CssClass="form-control" onkeypress="return isNumberKey(event)" /></td>
                            <td><asp:TextBox ID="txtbxVechileStops_SMLY" runat="server" Width="70px" CssClass="form-control" onkeypress="return isNumberKey(event)" /></td>
                            <td><asp:TextBox ID="txtbxVechileStops_YTD" runat="server" Width="70px" CssClass="form-control" AutoPostBack="True" OnTextChanged="txtbxVechileStops_YTD_OnTextChanged" onkeypress="return isNumberKey(event)" /></td>
                            <td><asp:TextBox ID="txtbxVechileStops_YTD_LY" runat="server" Width="70px" CssClass="form-control" AutoPostBack="True" OnTextChanged="txtbxVechileStops_YTD_OnTextChanged" onkeypress="return isNumberKey(event)" /></td>
                            <td><asp:TextBox ID="txtVechileChange" runat="server" Width="70px" CssClass="form-control" Enabled="False" /></td>
                            <td><asp:TextBox ID="txtbxVechileStops_AL" runat="server" Width="70px" CssClass="form-control" onkeypress="return isNumberKey(event)" /></td>
                        </tr>                                                                                
                    </tbody>
                </table>
            </div>
            &emsp;
            <div style="float:right; margin-right:300px">
                <table class="table table-bordered" id="gentable" style="border-color:black; background-color:white; margin: 0 auto;">
                    <tbody>
                        <tr>
                            <td># Personnel Assigned</td>
                            <td style="width:70px">Current Month</td>
                            <td style="width:70px">Same Month Last Year</td>                            
                        </tr>
                        <tr>
                            <td>Sergeants</td>
                            <td><asp:TextBox ID="txtbxSergeantsAssigned_CM" runat="server" Width="70px" CssClass="form-control" onkeypress="return isNumberKey(event)" CausesValidation="true" /></td>
                            <td><asp:TextBox ID="txtbxSergeantsAssigned_SMLY" runat="server" Width="70px" CssClass="form-control" onkeypress="return isNumberKey(event)" /></td>
                        </tr>
                        <tr>
                            <td>Corporals</td>
                            <td><asp:TextBox ID="txtbxCorporalsAssigned_CM" runat="server" Width="70px" CssClass="form-control" onkeypress="return isNumberKey(event)" CausesValidation="true" /></td>
                            <td><asp:TextBox ID="txtbxCorporalsAssigned_SMLY" runat="server" Width="70px" CssClass="form-control" onkeypress="return isNumberKey(event)" /></td>
                        </tr>
                        <tr>
                            <td>Troopers</td>
                            <td><asp:TextBox ID="txtbxTroopersAssigned_CM" runat="server" Width="70px" CssClass="form-control" onkeypress="return isNumberKey(event)" ausesValidation="true" /></td>
                            <td><asp:TextBox ID="txtbxTroopersAssigned_SMLY" runat="server" Width="70px" CssClass="form-control" onkeypress="return isNumberKey(event)" /></td>
                        </tr>
                        <tr>
                            <td>Secretary</td>
                            <td><asp:TextBox ID="txtbxSecretaryAssigned_CM" runat="server" Width="70px" CssClass="form-control" onkeypress="return isNumberKey(event)" CausesValidation="true" /></td>
                            <td><asp:TextBox ID="txtbxSecretaryAssigned_SMLY" runat="server" Width="70px" CssClass="form-control" onkeypress="return isNumberKey(event)" /></td>
                        </tr>
                        <tr>
                            <td>Leave</td>
                            <td><asp:TextBox ID="txtbxLeave_CM" runat="server" Width="70px" CssClass="form-control" onkeypress="return isNumberKey(event)" CausesValidation="true" /></td>
                            <td><asp:TextBox ID="txtbxLeave_SMLY" runat="server" Width="70px" CssClass="form-control" onkeypress="return isNumberKey(event)" /></td>
                        </tr>
                        <tr>
                            <td>Detached</td>
                            <td><asp:TextBox ID="txtbxDetached_CM" runat="server" Width="70px" CssClass="form-control" CausesValidation="true" onkeypress="return isNumberKey(event)" /></td>
                            <td><asp:TextBox ID="txtbxDetached_SMLY" runat="server" Width="70px" CssClass="form-control" onkeypress="return isNumberKey(event)" /></td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div><br />
        
        <div class="form-inline" style="margin-left:100px"><br /><p></p><br /><p></p><br /><p></p><br /><p></p><br /><p></p><br /><p></p><br /><p></p><br /><p></p><br /><p></p><br /><p></p><br /><p></p><br /><p></p>
            <%--<h2 style="margin-left:100px">General Remarks</h2>--%>
            <asp:Label ID="lblGeneralRemarks" runat="server" Text="General Remarks" style="font-size:1.3em; color:#900000;"></asp:Label><br />
            <asp:TextBox ID="txtbxGeneralRemarks" runat="server" TextMode="MultiLine" CssClass="form-control" style="width:1000px; max-width:1000px; height:100px;"   spellcheck="true" AutoCompleteType="Notes" ></asp:TextBox><asp:RequiredFieldValidator ID="rfvGenRemarks" ControlToValidate="txtbxGeneralRemarks" runat="server" ForeColor="Red" ErrorMessage="(*) Required" />
            <asp:Label ID="lblGeneralRemarkRequired" runat="server" Text="(*) Required" ForeColor="Red" Font-Bold="true" Style="display: none; float:right; margin-right:200px;"></asp:Label>
            <asp:TextBox ID="txtbxreject" runat="server" TextMode="MultiLine" CssClass="form-control" style="width:1000px; max-width:1000px; height:80px; "   spellcheck="true" AutoCompleteType="Notes" onkeyup="keyUP()" Visible="false" ></asp:TextBox>       
        </div>

    </div>
    <div style="margin-left:100px">
        <h2 class="control-label" id="lblEmail" runat="server">Email To:</h2>
        <asp:DropDownList ID="ddlEmail" runat="server" Enabled="False" AppendDataBoundItems="true" Width="250px" CssClass="form-control" /><asp:RequiredFieldValidator ID="rfvEmail" InitialValue="-1" ControlToValidate="ddlEmail" runat="server" ForeColor="Red" ErrorMessage="(*) Required" />
    </div>
    <div style="margin: 0px auto; width: 617px; padding-bottom: 17px; padding-top: 17px;">
        <asp:Button ID="btnSubmit" runat="server" Text="Approve" OnClick="btnSubmit_Click"  Visible="False" Width="177px" CssClass="btn btn-primary" ToolTip="Submit Post Inspect" />
        <asp:Button ID="btnSave" runat="server" Text="Save" Width="177px" CssClass="btn btn-primary" OnClick="btnSave_Click" ToolTip="Save Inpsection Data" />
        <asp:Button ID="btnExit" runat="server" Text="Exit without Saving" OnClick="btnExit_Click" CausesValidation="False" Width="177px" CssClass="btn btn-danger" />
        <asp:Button ID="btnRejct" CausesValidation="true" runat="server" Text="Reject" OnClick="btnRejct_OnClick"  Width="177px" CssClass="btn btn-danger" />
        <asp:Button ID="btnPrint" Text="Print Report" CssClass="btn btn-success" OnClick="btnPrint_OnClick" Visible="False" CausesValidation="False" runat="server"/>
    </div>
    <div id="divConfirm" runat="server" style="margin: 0px auto; width: 357px; padding-bottom: 7px;">
        <asp:Label ID="lblConfirm" runat="server" Text="POST INSPECTION REPORT SUBMITTED SUCCESSFULLY" Font-Size="1.2em" ForeColor="Red" />
    </div>
    <div id="divSaved" runat="server" style="margin: 0px auto; width: 357px; padding-bottom: 7px;">
        <asp:Label ID="lblSavedSuccess" runat="server" Text="POST INSPECTION REPORT SAVED SUCCESSFULLY" Font-Size="1.2em" ForeColor="Red" />
    </div>
</asp:Content>
