function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
        alert("Please Enter Only Numeric Values:");
        return false;
    }

    return true;
}

//////////////////////////////////////////////////////////////////////////////////
function ToggleSpellCheck() {
    var area = document.getElementById("txtbxGeneralRemarks");

    if ('spellcheck' in area) {
        area.spellcheck = !area.spellcheck;
    } else {
        alert("Your browser doesn't support this example!");
    }
}
//////////////////////////////////////////////////////////////////////////////////////lblRecordsReports



/////////////////////////////////////////////////////////////////////////////
function employeeCheck() {
    var requiredText = "Required";
    var noText = String.empty;
    var x = document.getElementById("ContentPlaceHolder1_lblEmployeePerformMGT");
    if (document.getElementById("ContentPlaceHolder1_lblEmployeePerformMGT").textContent === requiredText) {
        x.style.display = "none";
    } else {
        document.getElementById("ContentPlaceHolder1_tbxPersonnelPresent").focus();
    }
}
//////////////////////////////////////////////////////////////////////////////////
function manPowerCheck() {
    var x = document.getElementById("ContentPlaceHolder1_lblScheduleManpower");
    if (document.getElementById("ContentPlaceHolder1_lblScheduleManpower").textContent === "Required") {
        x.style.display = "none";
        //document.getElementById("formsave").focus();
    } else {
        document.getElementById("ContentPlaceHolder1_tbxPersonnelPresent").focus();
    }
}
////////////////////////////////////////////////////////////////////////////////////
function evidenceCheck() {
    var x = document.getElementById("ContentPlaceHolder1_lblEvidence");
    if (document.getElementById("ContentPlaceHolder1_lblEvidence").textContent === "Required") {
        x.style.display = "none";
    } else {
        document.getElementById("ContentPlaceHolder1_tbxPersonnelPresent").focus();
    }
}
////////////////////////////////////////////////////////////////////////////////////
function buildingGroundsCheck() {
    var x = document.getElementById("ContentPlaceHolder1_lblPostStruct");
    if (document.getElementById("ContentPlaceHolder1_lblPostStruct").textContent === "Required") {
        x.style.display = "none";
    } else {
        document.getElementById("ContentPlaceHolder1_tbxPersonnelPresent").focus();
    }
}
////////////////////////////////////////////////////////////////////////////////////
function postCleanCheck() {
    var x = document.getElementById("ContentPlaceHolder1_lblPostClean");
    if (document.getElementById("ContentPlaceHolder1_lblPostClean").textContent === "Required") {
        x.style.display = "none";
    } else {
        document.getElementById("ContentPlaceHolder1_tbxPersonnelPresent").focus();
    }
}
////////////////////////////////////////////////////////////////////////////////////
function postLawnCheck() {
    var x = document.getElementById("ContentPlaceHolder1_lblLawn");
    if (document.getElementById("ContentPlaceHolder1_lblLawn").textContent === "Required") {
        x.style.display = "none";
    } else {
        document.getElementById("ContentPlaceHolder1_tbxPersonnelPresent").focus();
    }
}
////////////////////////////////////////////////////////////////////////////////////
function furnitureCheck() {
    var x = document.getElementById("ContentPlaceHolder1_lblFurniture");
    if (document.getElementById("ContentPlaceHolder1_lblFurniture").textContent === "Required") {
        x.style.display = "none";
    } else {
        document.getElementById("ContentPlaceHolder1_tbxPersonnelPresent").focus();
    }
}
////////////////////////////////////////////////////////////////////////////////////
function vehicleMaintCheck() {
    var x = document.getElementById("ContentPlaceHolder1_lblMotorVehic");
    if (document.getElementById("ContentPlaceHolder1_lblMotorVehic").textContent === "Required") {
        x.style.display = "none";
    } else {
        document.getElementById("ContentPlaceHolder1_tbxPersonnelPresent").focus();
    }
}
////////////////////////////////////////////////////////////////////////////////////
function postGeneratorCheck() {
    var x = document.getElementById('ContentPlaceHolder1_lblPostGenerator');
    if (document.getElementById('ContentPlaceHolder1_lblPostGenerator').textContent === "Required") {
        x.style.display = "none";
    } else {
        document.getElementById("ContentPlaceHolder1_tbxPersonnelPresent").focus();
    }
}
////////////////////////////////////////////////////////////////////////////////////
function weaponsCheck() {
    var x = document.getElementById("ContentPlaceHolder1_lblWeapons");
    if (document.getElementById("ContentPlaceHolder1_lblWeapons").textContent === "Required") {
        x.style.display = "none";
    } else {
        document.getElementById("ContentPlaceHolder1_tbxPersonnelPresent").focus();
    }
}
////////////////////////////////////////////////////////////////////////////////////
function uniformsCheck() {
    var x = document.getElementById("ContentPlaceHolder1_lblUniforms");
    if (document.getElementById("ContentPlaceHolder1_lblUniforms").textContent === "Required") {
        x.style.display = "none";
    } else {
        document.getElementById("ContentPlaceHolder1_tbxPersonnelPresent").focus();
    }
}
////////////////////////////////////////////////////////////////////////////////////
function militaryCheck() {
    var x = document.getElementById("ContentPlaceHolder1_lblMilitary");
    if (document.getElementById("ContentPlaceHolder1_lblMilitary").textContent === "Required") {
        x.style.display = "none";
    } else {
        document.getElementById("ContentPlaceHolder1_tbxPersonnelPresent").focus();
    }
}
////////////////////////////////////////////////////////////////////////////////////
function demeanorCheck() {
    var x = document.getElementById('ContentPlaceHolder1_lblDemeanor');
    if (document.getElementById('ContentPlaceHolder1_lblDemeanor').textContent === "Required") {
        x.style.display = "none";
    } else {
        document.getElementById("tbxPersonnelPresent").focus();
    }
}
////////////////////////////////////////////////////////////////////////////////////
function onTimeCheck() {
    var x = document.getElementById("ContentPlaceHolder1_lblOntime");
    if (document.getElementById("ContentPlaceHolder1_lblOntime").textContent === "Required") {
        x.style.display = "none";
    } else {
        document.getElementById("ContentPlaceHolder1_tbxPersonnelPresent").focus();
    }
}
//Communication Rep

function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
        alert("Enter Only Numeric Values(0-9):");
        return false;
    }

    return true;
}

function message() {
    alert("Summited for Apporval");
}

function keyUP3() {
    if (document.getElementById("reason").textContent == "") {
        document.getElementById("rejectrequired").textContent = "*Required"
    }
    else {
        document.getElementById("rejectrequired").textContent = ""
    }
}

function keyUP2() {
    if (document.getElementById("remarks").textContent == "") {
        document.getElementById("remarksRequired").textContent = "*Required"
    }
    else {
        document.getElementById("remarksRequired").textContent = ""
    }
}

function keyUP() {
    if (document.getElementById("pp").textContent == "") {
        document.getElementById("ppRequired").textContent = "*Required"
    }
    else {
        document.getElementById("ppRequired").textContent = ""
    }
}

function uniCheck() {
    if (document.getElementById('uniRequired').textContent == "*Required") {
        document.getElementById('uniRequired').textContent = ""
        //document.getElementById("formsave").focus();
    }
    else {
        //document.getElementById("formsave").focus();
    }
}

function milCheck() {
    if (document.getElementById('milRequired').textContent == "*Required") {
        document.getElementById('milRequired').textContent = ""
        //document.getElementById("formsave").focus();
    }
    else {
        //document.getElementById("formsave").focus();
    }
}

function demCheck() {
    if (document.getElementById('demRequired').textContent == "*Required") {
        document.getElementById('demRequired').textContent = ""
        //document.getElementById("formsave").focus();
    }
    else {
        //document.getElementById("formsave").focus();
    }
}

function timeCheck() {
    if (document.getElementById('timeRequired').textContent == "*Required") {
        document.getElementById('timeRequired').textContent = ""
        //document.getElementById("formsave").focus();
    }
    else {
        //document.getElementById("formsave").focus();
    }
}

function person2disappear() {
    if (document.getElementById("person2Required").textContent == "*Required") {
        document.getElementById("person2Required").textContent = "";
        //document.getElementById("formupdate").focus();
    }
    else {
        //document.getElementById("save").focus();
        var ddl = document.getElementById("<%=person2.ClientID%>");
        if (ddl.options[ddl.selectedIndex].text == "SELECT") {
            document.getElementById("person2Required").textContent = "*Required";
            //document.getElementById("formupdate").focus();
        }
    }
}

function Officer() {
    if (document.getElementById("person2Required").textContent == "*Required") {
        document.getElementById("person2Required").textContent = "";
        //document.getElementById("formsave").focus();
    }
    else {
        //document.getElementById("formsave").focus();
        var ddl = document.getElementById("<%=person2.ClientID%>");
        if (ddl.options[ddl.selectedIndex].text == "SELECT") {
            document.getElementById("person2Required").textContent = "*Required";
            //document.getElementById("formsave").focus();
        }
    }

}

function radioCheck() {
    if (document.getElementById('radioRquired').textContent == "*Required") {
        document.getElementById('radioRquired').textContent = ""
        //document.getElementById("formsave").focus();
    }
    else {
        //document.getElementById("formsave").focus();
    }
}

function computerCheck() {
    if (document.getElementById('computerRequired').textContent == "*Required") {
        document.getElementById('computerRequired').textContent = ""
        //document.getElementById("formsave").focus();
    }
    else {
        //document.getElementById("formsave").focus();
    }
}

function furCheck() {
    if (document.getElementById('furRequired').textContent == "*Required") {
        document.getElementById('furRequired').textContent = ""
        //document.getElementById("formsave").focus();
    }
    else {
        //document.getElementById("formsave").focus();
    }
}

function admindisappear() {
    if (document.getElementById("adminRequired").textContent == "*Required") {
        document.getElementById("adminRequired").textContent = "";
        //document.getElementById("formsave").focus();
    }
    else {
        //document.getElementById("formsave").focus();
        //<%--      var ddl = document.getElementById("<%=admin.ClientID%>");--%>
        //      if (ddl.options[ddl.selectedIndex].text == "SELECT") {
        //          document.getElementById("adminRequired").textContent = "*Required";
        //document.getElementById("formsave").focus();
    }
}


function recordCheck() {
    if (document.getElementById('recordRequired').textContent == "*Required") {
        document.getElementById('recordRequired').textContent = ""
        //document.getElementById("formsave").focus();
    }
    else {
        //document.getElementById("formsave").focus();
    }
}

function employeeCheck() {
    if (document.getElementById('employeeRequired').textContent == "*Required") {
        document.getElementById('employeeRequired').textContent = ""
        //document.getElementById("formsave").focus();
    }
    else {
        //document.getElementById("formsave").focus();
    }
}

function manpowerCheck() {
    if (document.getElementById('manpowerRequired').textContent == "*Required") {
        document.getElementById('manpowerRequired').textContent = ""
        //document.getElementById("formsave").focus();
    }
    else {
        //document.getElementById("formsave").focus();
    }
}

function builddisappear() {
    if (document.getElementById("buildRequired").textContent == "*Required") {
        document.getElementById("buildRequired").textContent = "";
        //document.getElementById("formsave").focus();
    }
    else {
        //document.getElementById("formsave").focus();
        //<%--      var ddl = document.getElementById("<%=build.ClientID%>");--%>
        //      if (ddl.options[ddl.selectedIndex].text == "SELECT") {
        //          document.getElementById("buildRequired").textContent = "*Required";
        //          //document.getElementById("formsave").focus();
        //      }
    }
}

function comCheck() {
    if (document.getElementById('comRequired').textContent == "*Required") {
        document.getElementById('comRequired').textContent = ""
        //document.getElementById("formsave").focus();
    }
    else {
        //document.getElementById("formsave").focus();
    }
}

function keyUP(txt) {

    var ppR = document.getElementById("pp").textContent;
    if (ppR != " ") {
        document.getElementById("ppRequired").textContent = " ";
    }
    else {
        document.getElementById("ppRequired").textContent = "*required";
    }
}

function keyUP_Remarks(txt) {
    var Remarks = document.getElementById("remarks").textContent;
    if (Remarks != "") {
        document.getElementById("remarks").textContent = " ";
    }
    else {
        document.getElementById("remarks").textContent = "*required";
    }
}

function rejectreason() {
    bootbox.prompt({
        title: "Enter Reject Reason/Remark:",
        inputType: 'textarea',
        callback: function (result) {
            if (result == true) {
                document.getElementById('remarks').textContent = result;
            }
            else {

            }
        }
    });
}

function calFix() {
    var test = document.getElementById('<%= datevalue.ClientID %>');
}

