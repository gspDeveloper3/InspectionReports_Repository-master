<%@ Page Title="COMMUNICATIONS MONTHLY INSPECTION" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ComInspectionReport.aspx.cs" Inherits="PostCommInspectionReport.ComInspectionReport.ComInspectionReport" %>





<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%--<head>
    <title></title>
    <link href="css/bootstrap.css" rel="stylesheet" />
    <script src="Scripts/jquery-1.9.1.js"></script>
    <script src="Scripts/jquery-ui-1.8.24.js"></script>
    <script src="Scripts/moment.js"></script>
    <link href="css/theme.css" rel="stylesheet" />
    <link href="../Styles.css" rel="stylesheet" />
    <%--<link href="css/site.css" rel="stylesheet" />--%>
    <%--    <link href="css/pikaday.css" rel="stylesheet" />
    <script src="Scripts/pikaday.js"></script>
    <script src="Scripts/bootbox.js"></script>--%>


    <h1 id="hdrLabel" runat="server" style="margin-left: 100px">COM CENTER INSPECTION REPORT</h1>
    <p>
        <span style="margin-left: 100px">Report ID:</span><asp:Label ID="reportid" runat="server" />
    </p>

    <asp:Label ID="approvedMessage" runat="server" Style="float: right; font-weight: bolder"></asp:Label>
    <div style="visibility: hidden; background-color: #99ffff; float: right" id="myDivID">Hello</div>

    <table class="table table-bordered" style="border-color: black; background-color: white; width: 1200px; margin: 0 auto;">
        <tbody>
            <tr>
                <td><span class="divTableCellTitle">TROOP:&emsp;</span><asp:Label ID="trooper" runat="server" Width="150px" CssClass="form-control" Enabled="false" /></td>
                <td><span class="divTableCellTitle">POST:&emsp;</span><asp:Label ID="post" runat="server" Text="COMMUNICATIONS" Width="150px" CssClass="form-control" /></td>
                <td><span class="divTableCellTitle">CITY:&emsp;</span><asp:Label ID="citymain" runat="server" Width="150px" CssClass="form-control" /></td>
                <td class="form-inline">
                    <label style="margin-left: 60px;">DATE: </label>
                    &emsp;<asp:TextBox ID="datevalue" ReadOnly="true" runat="server" Width="140px" CssClass="form-control"></asp:TextBox>
                    <script type="text/javascript">
                        var picker = new Pikaday({
                            field: document.getElementById("datevalue"),
                            format: 'MM-DD-YYYY',
                            firstDay: 1,
                            minDate: new Date('01-01-2000'),
                            maxDate: new Date('12-31-2099'),
                            yeaerRange: [2000, 2099],
                            numberofMonth: 1,
                            theme: 'light-theme'
                        });
                    </script>
                </td>
            </tr>
        </tbody>
    </table>

    <div id="divTable" style="margin: 0 auto;">

        <asp:Button ID="Button1" runat="server" Text="Generate Report" CssClass="btn btn-primary" Style="float: right; margin-right: 100px;" Width="150px" OnClick="Button1_Click" />

        <asp:Label ID="GenError" runat="server" Text="Record Already Created" Style="margin-left: 750px" Font-Bold="true" ForeColor="Red" Visible="false"></asp:Label>
        <br />
        <h2 style="margin-left: 100px">Administration and Supervision:&nbsp;</h2>

        <table class="table table-bordered" style="border-color: black; background-color: white; width: 1200px; margin: 0 auto;" id="table1">
            <tbody>
                <tr>
                    <td>
                        <label>RATE EACH OF THE FOLLOWING SUBCATEGORIES INDEPENDENTLY</label></td>
                    <td class="form-inline">
                        <label>Excellent</label>
                        &emsp;&emsp;<label>Very Satisfactory</label>
                        &emsp;&emsp;<label>Satisfactory</label>
                        &emsp;&emsp;<label>UnSatisfactory</label>
                    </td>
                </tr>
                <tr>
                    <td>Records and Reports &emsp;<asp:Label ID="recordRequired" ForeColor="#F44336" runat="server" Style="font-weight: bolder"></asp:Label></td>
                    <td class="form-inline">&emsp;&emsp;<asp:RadioButton ID="record" runat="server" GroupName="selection1" onclick="recordCheck()" />
                        &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;<asp:RadioButton ID="r1" runat="server" GroupName="selection1" onclick="recordCheck()" />
                        &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;<asp:RadioButton ID="r2" runat="server" GroupName="selection1" onclick="recordCheck()" />
                        &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;<asp:RadioButton ID="r3" runat="server" GroupName="selection1" onclick="recordCheck()" />
                    </td>
                </tr>
                <tr>
                    <td>Employee Performance Management &emsp;<asp:Label ID="employeeRequired" ForeColor="#F44336" runat="server" Style="font-weight: bolder"></asp:Label></td>
                    <td class="form-inline">&emsp;&emsp;<asp:RadioButton ID="employee" runat="server" GroupName="selection2" onclick="employeeCheck()" />
                        &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;<asp:RadioButton ID="e1" runat="server" GroupName="selection2" onclick="employeeCheck()" />
                        &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;<asp:RadioButton ID="e2" runat="server" GroupName="selection2" onclick="employeeCheck()" />
                        &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;<asp:RadioButton ID="e3" runat="server" GroupName="selection2" onclick="employeeCheck()" />
                    </td>
                </tr>
                <tr>
                    <td>Scheduling and Manpower Allocation &emsp;<asp:Label ID="manpowerRequired" ForeColor="#F44336" runat="server" Style="font-weight: bolder"></asp:Label></td>
                    <td class="form-inline">&emsp;&emsp;<asp:RadioButton ID="manpower" runat="server" GroupName="selection3" onclick="manpowerCheck()" />
                        &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;<asp:RadioButton ID="m1" runat="server" GroupName="selection3" onclick="manpowerCheck()" />
                        &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;<asp:RadioButton ID="m2" runat="server" GroupName="selection3" onclick="manpowerCheck()" />
                        &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;<asp:RadioButton ID="m3" runat="server" GroupName="selection3" onclick="manpowerCheck()" />
                    </td>
                </tr>
            </tbody>
        </table>
        <p></p>

        <asp:Label ID="adminRequired" ForeColor="#F44336" runat="server" Style="font-weight: bolder" Visible="false"></asp:Label>

        <p></p>

        <h2 style="margin-left: 100px">Buildings and Grounds:&nbsp;</h2>

        <asp:Label ID="buildRequired" ForeColor="#F44336" runat="server" Style="font-weight: bolder" Visible="false"></asp:Label>

        <table class="table table-bordered" style="border-color: black; background-color: white; width: 1200px; margin: 0 auto;" id="table2">
            <tbody>
                <tr>
                    <td>
                        <label>RATE EACH OF THE FOLLOWING SUBCATEGORIES INDEPENDENTLY</label>
                    </td>
                    <td>
                        <div class="form-inline">
                            <label>Excellent</label>
                            &emsp;&emsp;<label>Very Satisfactory</label>
                            &emsp;&emsp;<label>Satisfactory</label>
                            &emsp;&emsp;<label>UnSatisfactory</label>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>Communication Center Cleaniness and Maintenance &emsp;<asp:Label ID="comRequired" ForeColor="#F44336" runat="server" Style="font-weight: bolder"></asp:Label></td>
                    <td>&emsp;&emsp;<asp:RadioButton ID="com" runat="server" GroupName="selection4" onclick="comCheck()" />
                        &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;<asp:RadioButton ID="c1" runat="server" GroupName="selection4" onclick="comCheck()" />
                        &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;<asp:RadioButton ID="c2" runat="server" GroupName="selection4" onclick="comCheck()" />
                        &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;<asp:RadioButton ID="c3" runat="server" GroupName="selection4" onclick="comCheck()" />
                    </td>
                </tr>
                <tr>
                    <td>Furniture and Appliance &emsp;<asp:Label ID="furRequired" ForeColor="#F44336" runat="server" Style="font-weight: bolder"></asp:Label></td>
                    <td>&emsp;&emsp;<asp:RadioButton ID="fur" runat="server" GroupName="selection5" onclick="furCheck()" />
                        &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;<asp:RadioButton ID="f1" runat="server" GroupName="selection5" onclick="furCheck()" />
                        &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;<asp:RadioButton ID="f2" runat="server" GroupName="selection5" onclick="furCheck()" />
                        &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;<asp:RadioButton ID="f3" runat="server" GroupName="selection5" onclick="furCheck()" />
                    </td>
                </tr>
            </tbody>
        </table>
        <p></p>
        <h2 style="margin-left: 100px">Equiptment:&nbsp;</h2>

        <asp:Label ID="equipRequired" ForeColor="#F44336" runat="server" Style="font-weight: bolder" Visible="false"></asp:Label>


        <table class="table table-bordered" style="border-color: black; background-color: white; width: 1200px; margin: 0 auto;" id="table3">
            <tbody>
                <tr>
                    <td>
                        <label>RATE EACH OF THE FOLLOWING SUBCATEGORIES INDEPENDENTLY</label>
                    </td>
                    <td>
                        <div class="form-inline">
                            <label>Excellent</label>
                            &emsp;&emsp;<label>Very Satisfactory</label>
                            &emsp;&emsp;<label>Satisfactory</label>
                            &emsp;&emsp;<label>UnSatisfactory</label>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="form-inline">Radio Equipment &emsp;<asp:Label ID="radioRquired" ForeColor="#F44336" runat="server" Style="font-weight: bolder"></asp:Label>
                    </td>
                    <td>&emsp;&emsp;<asp:RadioButton ID="radio" runat="server" GroupName="selection6" onclick="radioCheck()" />
                        &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;<asp:RadioButton ID="ra1" runat="server" GroupName="selection6" onclick="radioCheck()" />
                        &emsp; &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;<asp:RadioButton ID="ra2" runat="server" GroupName="selection6" onclick="radioCheck()" />
                        &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;<asp:RadioButton ID="ra3" runat="server" GroupName="selection6" onclick="radioCheck()" />
                    </td>

                </tr>
                <tr>
                    <td class="form-inline">Computer, Printer, etc : &emsp;<asp:Label ID="computerRequired" ForeColor="#F44336" runat="server" Style="font-weight: bolder"></asp:Label>

                    </td>
                    <td>&emsp;&emsp;<asp:RadioButton ID="computer" runat="server" GroupName="selection7" onclick="computerCheck()" />
                        &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;<asp:RadioButton ID="co1" runat="server" GroupName="selection7" onclick="computerCheck()" />
                        &emsp; &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;<asp:RadioButton ID="co2" runat="server" GroupName="selection7" onclick="computerCheck()" />
                        &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;<asp:RadioButton ID="co3" runat="server" GroupName="selection7" onclick="computerCheck()" />
                    </td>
                </tr>
            </tbody>
        </table>
        <br />

        <div class="form-inline">
            <h2 style="float: left; margin-left: 100px;">Personnel:</h2>
            <asp:TextBox ID="pp" runat="server" TextMode="MultiLine" Width="400px" CssClass="form-control" Style="float: right; margin-right: 400px;" /><span style="float: right; margin-right: 10px; margin-top: 12px;">Personnel Present:</span>
            <asp:Label ID="personReequired" ForeColor="#F44336" runat="server" Style="font-weight: bolder" Visible="false"></asp:Label>
            <asp:Label ID="ppRequired" ForeColor="#F44336" runat="server" Style="font-weight: bolder">
            </asp:Label>
        </div>
        <p></p>
        <br />
        <p></p>
        <br />
        <p></p>

        <table class="table table-bordered" style="border-color: black; background-color: white; width: 1200px; margin: 0 auto;" id="table4">
            <tbody>
                <tr>
                    <td>
                        <label>RATING EACH OF THE FOLLOWING SUBCATEGORIES INDEPENDENTLY</label></td>
                    <td>
                        <div class="form-inline">
                            <label>Excellent</label>
                            &emsp;&emsp;<label>Very Satisfactory</label>
                            &emsp;&emsp;<label>Satisfactory</label>
                            &emsp;&emsp;<label>UnSatisfactory</label>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>Uniforms and Appearance &emsp;<asp:Label ID="uniRequired" ForeColor="#F44336" runat="server" Style="font-weight: bolder"></asp:Label>
                    </td>
                    <td>&emsp;&emsp;<asp:RadioButton ID="uni" runat="server" GroupName="selection8" onclick="uniCheck()" />
                        &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;<asp:RadioButton ID="u1" runat="server" GroupName="selection8" onclick="uniCheck()" />
                        &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;<asp:RadioButton ID="u2" runat="server" GroupName="selection8" onclick="uniCheck()" />
                        &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;<asp:RadioButton ID="u3" runat="server" GroupName="selection8" onclick="uniCheck()" />
                    </td>
                </tr>
                <tr>
                    <td>Military Courtesy &emsp;<asp:Label ID="milRequired" ForeColor="#F44336" runat="server" Style="font-weight: bolder"></asp:Label>
                    </td>
                    <td>&emsp;&emsp;<asp:RadioButton ID="mil" runat="server" GroupName="selection9" onclick="milCheck()" />
                        &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;<asp:RadioButton ID="mi1" runat="server" GroupName="selection9" onclick="milCheck()" />
                        &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;<asp:RadioButton ID="mi2" runat="server" GroupName="selection9" onclick="milCheck()" />
                        &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;<asp:RadioButton ID="mi3" runat="server" GroupName="selection9" onclick="milCheck()" />
                    </td>
                </tr>
                <tr>
                    <td>Demeanor and Morale &emsp;<asp:Label ID="demRequired" ForeColor="#F44336" runat="server" Style="font-weight: bolder"></asp:Label>
                    </td>
                    <td>&emsp;&emsp;<asp:RadioButton ID="dem" runat="server" GroupName="selection10" onclick="demCheck()" />
                        &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;<asp:RadioButton ID="d1" runat="server" GroupName="selection10" onclick="demCheck()" />
                        &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;<asp:RadioButton ID="d2" runat="server" GroupName="selection10" onclick="demCheck()" />
                        &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;<asp:RadioButton ID="d3" runat="server" GroupName="selection10" onclick="demCheck()" />
                    </td>
                </tr>
                <tr>
                    <td>On Time and Prepared for Inspection &emsp;<asp:Label ID="timeRequired" ForeColor="#F44336" runat="server" Style="font-weight: bolder"></asp:Label>
                    </td>
                    <td>
                        <div class="form-inline">
                            &emsp;&emsp;<asp:RadioButton ID="time" runat="server" GroupName="selection11" onclick="timeCheck()" />
                            &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;<asp:RadioButton ID="t1" runat="server" GroupName="selection11" onclick="timeCheck()" />
                            &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;<asp:RadioButton ID="t2" runat="server" GroupName="selection11" onclick="timeCheck()" />
                            &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;<asp:RadioButton ID="t3" runat="server" GroupName="selection11" onclick="timeCheck()" />
                        </div>
                    </td>
                </tr>
            </tbody>
        </table>
        <br />

        <div class="form-inline">
            <%--           <h2 style="margin-left:100px">AActivities and Personal Comparisons:</h2><br/>--%>
            <%--<input type="button" id="formsave" class="btn btn-primary" value="Save and Gen. Table" style="float:right" onclick="saveandgen();"/>--%>

            <%--        <asp:Button ID="formsave" runat="server" Text="Save and Gen. Table" CssClass="btn btn-primary" style="float:right" Visible="false" ValidationGroup="saverecord" OnClick="formsave_Click1" />

        </div>--%>

            <%--This CODE will Handle save and table processing--%>

            <div id="divLoading" style="margin: 0px; padding: 0px; position: fixed; right: 0px; top: 0px; width: 100%; height: 100%; background-color: #666666; z-index: 30001; opacity: .8; filter: alpha(opacity=70); display: none" onload="">
                <p style="position: absolute; top: 30%; left: 35%; color: White; font-size: larger; font-style: oblique; font-weight: bolder">
                    Saving and Generating Table...
                    <img src="../../Images/ajax-loading.gif" />
                </p>
            </div>

            <h2 style="margin-left: 100px">Activities and Personnel Comparisons</h2>
            <br />
            <table class="table table-bordered" style="border-color: black; background-color: white; width: 1200px; margin: 0 auto;" id="gentable">
                <tbody>
                    <tr style="font-weight: bolder">
                        <td>Activites</td>
                        <td>Current Month</td>
                        <td>Last Month</td>
                        <td>Year-to-Date Current Year</td>
                        <td>Same Month Last Year</td>
                        <td>Year-to-Date Last Year</td>
                        <td># Personnel Assigned</td>
                        <td style="width: 50px">Current Month</td>
                        <td style="width: 50px">Same Month Last Year</td>
                    </tr>
                    <tr>
                        <td style="font-weight: bolder">TOTAL Calls</td>
                        <td>
                            <asp:TextBox ID="totalcalls_cm" runat="server" CssClass="form-control" Style="width: 80px" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="totalcalls_lm" runat="server" CssClass="form-control" Style="width: 80px" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="totalcalls_ytd_cy" runat="server" CssClass="form-control" Style="width: 80px" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="totalcalls_smly" runat="server" CssClass="form-control" Style="width: 80px" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="totalcalls_ytd_ly" runat="server" CssClass="form-control" Style="width: 80px" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                        <td style="font-weight: bolder">Chief</td>
                        <td>
                            <asp:TextBox ID="totalcalls_cm2" runat="server" CssClass="form-control" Style="width: 45px" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="totalcalls_smly2" runat="server" CssClass="form-control" Style="width: 45px" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="font-weight: bolder">Abandoned Vehicles Entry</td>
                        <td>
                            <asp:TextBox ID="abandonedveh_cm" runat="server" CssClass="form-control" Style="width: 80px" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="abandonedveh_lm" runat="server" CssClass="form-control" Style="width: 80px" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="abandonedveh_ytd_cy" runat="server" CssClass="form-control" Style="width: 80px" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="abandonedveh_smly" runat="server" CssClass="form-control" Style="width: 80px" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="abandonedveh_ytd_ly" runat="server" CssClass="form-control" Style="width: 80px" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                        <td style="font-weight: bolder">Seniors</td>
                        <td>
                            <asp:TextBox ID="abandonedveh_cm2" runat="server" CssClass="form-control" Style="width: 45px" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="abandonedveh_smly2" runat="server" CssClass="form-control" Style="width: 45px" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="font-weight: bolder">Pursuits</td>
                        <td>
                            <asp:TextBox ID="chases_cm" runat="server" CssClass="form-control" Style="width: 80px" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="chases_lm" runat="server" CssClass="form-control" Style="width: 80px" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="chases_ytd_cy" runat="server" CssClass="form-control" Style="width: 80px" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="chases_smly" runat="server" CssClass="form-control" Style="width: 80px" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="chases_ytd_ly" runat="server" CssClass="form-control" Style="width: 80px" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                        <td style="font-weight: bolder">Dispatcher 2</td>
                        <td>
                            <asp:TextBox ID="chases_cm2" runat="server" CssClass="form-control" Style="width: 45px" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="chases_smly2" runat="server" CssClass="form-control" Style="width: 45px" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="font-weight: bolder">Controlled Burns</td>
                        <td>
                            <asp:TextBox ID="controlledburns_cm" runat="server" CssClass="form-control" Style="width: 80px" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="controlledburns_lm" runat="server" CssClass="form-control" Style="width: 80px" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="controlledburns_ytd_cy" runat="server" CssClass="form-control" Style="width: 80px" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="controlledburns_smly" runat="server" CssClass="form-control" Style="width: 80px" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="controlledburns_ytd_ly" runat="server" CssClass="form-control" Style="width: 80px" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                        <td style="font-weight: bolder">Dispatcher 1</td>
                        <td>
                            <asp:TextBox ID="controlledburns_cm2" runat="server" CssClass="form-control" Style="width: 45px" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="controlledburns_smly2" runat="server" CssClass="form-control" Style="width: 45px" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="font-weight: bolder">Criminal & Driver Hx Req</td>
                        <td>
                            <asp:TextBox ID="criminaldriverhx_cm" runat="server" CssClass="form-control" Style="width: 80px" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="criminaldriverhx_lm" runat="server" CssClass="form-control" Style="width: 80px" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="criminaldriverhx_ytd_cy" runat="server" CssClass="form-control" Style="width: 80px" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="criminaldriverhx_smly" runat="server" CssClass="form-control" Style="width: 80px" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="criminaldriverhx_ytd_ly" runat="server" CssClass="form-control" Style="width: 80px" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                        <td style="font-weight: bolder">GCIC Certifications and Re-Certifications Current Month 2</td>
                        <td style="border-left-color: white; border-left-width: 1.5px"></td>
                        <td>
                            <asp:TextBox ID="criminaldriverhx_smly2" runat="server" CssClass="form-control" Style="width: 45px" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="font-weight: bolder">DNR Cells</td>
                        <td>
                            <asp:TextBox ID="dnrcalls_cm" runat="server" CssClass="form-control" Style="width: 80px" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="dnrcalls_lm" runat="server" CssClass="form-control" Style="width: 80px" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="dnrcalls_ytd_cy" runat="server" CssClass="form-control" Style="width: 80px" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="dnrcalls_smly" runat="server" CssClass="form-control" Style="width: 80px" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="dnrcalls_ytd_ly" runat="server" CssClass="form-control" Style="width: 80px" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                        <td style="border-top-color: white; border-top-width: 1.5px"></td>
                        <td style="border-top-color: white; border-top-width: 1.5px; border-left-color: white; border-left-width: 1.5px"></td>
                        <td style="border-top-color: white; border-top-width: 1.5px"></td>
                    </tr>
                    <tr>
                        <td style="font-weight: bolder">Equip Issues</td>
                        <td>
                            <asp:TextBox ID="equipissue_cm" runat="server" CssClass="form-control" Style="width: 80px" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="equipissue_lm" runat="server" CssClass="form-control" Style="width: 80px" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="equipissue_ytd_cy" runat="server" CssClass="form-control" Style="width: 80px" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="equipissue_smly" runat="server" CssClass="form-control" Style="width: 80px" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="equipissue_ytd_ly" runat="server" CssClass="form-control" Style="width: 80px" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                        <td style="border-top-color: white; border-top-width: 1.5px"></td>
                        <td style="border-top-color: white; border-top-width: 1.5px; border-left-color: white; border-left-width: 1.5px"></td>
                        <td style="border-top-color: white; border-top-width: 1.5px"></td>
                    </tr>
                    <tr>
                        <td style="font-weight: bolder">Flight Plans</td>
                        <td>
                            <asp:TextBox ID="flightplans_cm" runat="server" CssClass="form-control" Style="width: 80px" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="flightplans_lm" runat="server" CssClass="form-control" Style="width: 80px" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="flightplans_ytd_cy" runat="server" CssClass="form-control" Style="width: 80px"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="flightplans_smly" runat="server" CssClass="form-control" Style="width: 80px" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="flightplans_ytd_ly" runat="server" CssClass="form-control" Style="width: 80px" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                        <td>
                            <label style=""><u>GCIC HITS Breakdown</u></label></td>
                        <td style="border-right-color: white; border-right-width: 1.5px; border-left-color: white; border-left-width: 1.5px;"></td>
                        <td style="border-left-color: white;"></td>
                    </tr>
                    <tr>
                        <td style="font-weight: bolder">GCIC HITS</td>
                        <td>
                            <asp:TextBox ID="gcichits_cm" runat="server" CssClass="form-control" Style="width: 80px" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="gcichits_lm" runat="server" CssClass="form-control" Style="width: 80px" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="gcichits_ytd_cy" runat="server" CssClass="form-control" Style="width: 80px" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="gcichits_smly" runat="server" CssClass="form-control" Style="width: 80px" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="gcichits_ytd_ly" runat="server" CssClass="form-control" Style="width: 80px" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                        <td class="form-inline">
                            <label>
                                <asp:TextBox ID="gcichits_wantedperson" runat="server" CssClass="form-control" Style="width: 45px" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                Wanted Person</label></td>
                        <td style="border-right-color: white; border-right-width: 1.5px; border-left-color: white; border-left-width: 1.5px;"></td>
                        <td style="border-left-color: white;"></td>
                    </tr>
                    <tr>
                        <td style="font-weight: bolder">Motorist Assist</td>
                        <td>
                            <asp:TextBox ID="motoassist_cm" runat="server" CssClass="form-control" Style="width: 80px" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="motoassist_lm" runat="server" CssClass="form-control" Style="width: 80px" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="motoassist_ytd_cy" runat="server" CssClass="form-control" Style="width: 80px" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="motoassist_smly" runat="server" CssClass="form-control" Style="width: 80px" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="motoassist_ytd_ly" runat="server" CssClass="form-control" Style="width: 80px" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                        <td class="form-inline">
                            <label>
                                <asp:TextBox ID="motoassist_stolevehicle" runat="server" CssClass="form-control" Style="width: 45px" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                Stolen Vehicles</label></td>
                        <td style="border-right-color: white; border-right-width: 1.5px; border-left-color: white; border-left-width: 1.5px;"></td>
                        <td style="border-left-color: white;"></td>
                    </tr>
                    <tr>
                        <td style="font-weight: bolder">Open Records</td>
                        <td>
                            <asp:TextBox ID="openrecord_cm" runat="server" CssClass="form-control" Style="width: 80px" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="openrecord_lm" runat="server" CssClass="form-control" Style="width: 80px" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="openrecord_ytd_cy" runat="server" CssClass="form-control" Style="width: 80px" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="openrecord_smly" runat="server" CssClass="form-control" Style="width: 80px" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="openrecord_ytd_ly" runat="server" CssClass="form-control" Style="width: 80px" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                        <td class="form-inline">
                            <label>
                                <asp:TextBox ID="openrecord_stoleguns" runat="server" CssClass="form-control" Style="width: 45px" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                Stolen Guns</label></td>
                        <td style="border-right-color: white; border-right-width: 1.5px; border-left-color: white; border-left-width: 1.5px;"></td>
                        <td style="border-left-color: white;"></td>
                    </tr>
                    <tr>
                        <td style="font-weight: bolder">Relays</td>
                        <td>
                            <asp:TextBox ID="relays_cm" runat="server" CssClass="form-control" Style="width: 80px" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="relays_lm" runat="server" CssClass="form-control" Style="width: 80px" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="relays_ytd_cy" runat="server" CssClass="form-control" Style="width: 80px" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="relays_smly" runat="server" CssClass="form-control" Style="width: 80px" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="relays_ytd_ly" runat="server" CssClass="form-control" Style="width: 80px" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                        <td class="form-inline">
                            <label>
                                <asp:TextBox ID="relays_other" runat="server" CssClass="form-control" Style="width: 45px" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                Other</label></td>
                        <td style="border-right-color: white; border-right-width: 1.5px; border-left-color: white; border-left-width: 1.5px;"></td>
                        <td style="border-left-color: white;"></td>
                    </tr>
                    <tr>
                        <td style="font-weight: bolder">Road Checks</td>
                        <td>
                            <asp:TextBox ID="roadchecks_cm" runat="server" CssClass="form-control" Style="width: 80px" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="roadchecks_lm" runat="server" CssClass="form-control" Style="width: 80px" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="roadchecks_ytd_cy" runat="server" CssClass="form-control" Style="width: 80px" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="roadchecks_smly" runat="server" CssClass="form-control" Style="width: 80px" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="roadchecks_ytd_ly" runat="server" CssClass="form-control" Style="width: 80px" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                        <td>
                            <label></label>
                        </td>
                        <td style="border-right-color: white; border-right-width: 1.5px; border-left-color: white; border-left-width: 1.5px;"></td>
                        <td style="border-left-color: white;"></td>
                    </tr>
                    <tr>
                        <td style="font-weight: bolder">Traffic Stops</td>
                        <td>
                            <asp:TextBox ID="trafficstops_cm" runat="server" CssClass="form-control" Style="width: 80px" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="trafficstops_lm" runat="server" CssClass="form-control" Style="width: 80px" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="trafficstops_ytd_cy" runat="server" CssClass="form-control" Style="width: 80px" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="trafficstops_smly" runat="server" CssClass="form-control" Style="width: 80px" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="trafficstops_ytd_ly" runat="server" CssClass="form-control" Style="width: 80px" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                        <td>
                            <label></label>
                        </td>
                        <td style="border-right-color: white; border-right-width: 1.5px; border-left-color: white; border-left-width: 1.5px;"></td>
                        <td style="border-left-color: white;"></td>
                    </tr>
                    <tr>
                        <td style="font-weight: bolder">Towed Vehicles</td>
                        <td>
                            <asp:TextBox ID="towedveh_cm" runat="server" CssClass="form-control" Style="width: 80px" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="towedveh_lm" runat="server" CssClass="form-control" Style="width: 80px" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="towedveh_ytd_cy" runat="server" CssClass="form-control" Style="width: 80px" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="towedveh_smly" runat="server" CssClass="form-control" Style="width: 80px" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="towedveh_ytd_ly" runat="server" CssClass="form-control" Style="width: 80px" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                        <td>
                            <label></label>
                        </td>
                        <td style="border-right-color: white; border-right-width: 1.5px; border-left-color: white; border-left-width: 1.5px;"></td>
                        <td style="border-left-color: white;"></td>
                    </tr>
                </tbody>
            </table>
        </div>

        <%-- end comparisons --%>

        <div class="form-inline" style="margin-left: 100px">
            <br />
            <p></p>
            <br />
            <label style="padding: 6px; font-weight: bolder">GENERAL REMARKS:  </label>
            <asp:Label ID="remarksRequired" ForeColor="#F44336" runat="server" Style="font-weight: bolder"></asp:Label>
            <div class="form-inline">
                <asp:TextBox ID="remarks" runat="server" TextMode="MultiLine" CssClass="form-control" Style="width: 1080px; max-width: 1080px; height: 100px; padding: 10px;" spellcheck="true" AutoCompleteType="Notes" onkeyup="keyUP2()"></asp:TextBox>
            </div>
            <p></p>
            &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;<asp:Label ID="rejectrequired" runat="server" Text="*Required" Visible="false" ForeColor="Red" Font-Bold="true"></asp:Label>
            <asp:Label ID="person2Required" ForeColor="#F44336" runat="server" Style="margin-left: 55px; font-weight: bolder"></asp:Label>
            <div class="form-inline" style="width: 1080px; margin: 0 auto;">
                <asp:Label ID="officer" runat="server" Text="Officer:"></asp:Label>
                <asp:DropDownList ID="person2" runat="server" CssClass="form-control" onchange="person2disappear()">
                </asp:DropDownList>
                <asp:Label ID="Label4" runat="server" Text="Reject Reason:" Visible="false"></asp:Label>&emsp;<asp:TextBox ID="reason" runat="server" TextMode="MultiLine" CssClass="form-control" Style="width: 600px; max-width: 600px; height: 40px; padding: 10px;" spellcheck="true" AutoCompleteType="Notes" onkeyup="keyUP3()" Visible="false"></asp:TextBox>
                <asp:Button ID="reject" runat="server" Text="Reject" Style="float: right; margin-left: 20px" Width="80px" Font-Bold="true" CssClass="btn btn-primary" Enabled="true" OnClick="reject_Click" />
                <%--OnClick="reject_Click"--%>
                <asp:Button ID="approve" runat="server" Text="Approve" Style="float: right; margin-left: 20px" Width="80px" Font-Bold="true" CssClass="btn btn-primary" ValidationGroup="saveRecord" OnClick="approve_Click" />
                <asp:Button ID="formupdate" runat="server" Text="Submit For Approval" Style="float: right; margin-left: 20px" CssClass="btn btn-primary" ValidationGroup="saveRecord" OnClick="formupdate_Click" />
                <asp:Button ID="troopcom" runat="server" Text="Submit For Approval" Style="float: right; margin-left: 20px" CssClass="btn btn-primary" ValidationGroup="saveRecord" OnClick="troopcom_Click" />
                <asp:Button ID="save" runat="server" Text="Save and Submit" Style="float: right; margin-left: 20px" CssClass="btn btn-primary" ValidationGroup="saveRecord" OnClick="save_Click" />
            </div>

        </div>
        <br />
        <p></p>
    </div>
</asp:Content>
