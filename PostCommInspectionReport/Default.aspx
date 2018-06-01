<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PostCommInspectionReport.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .detailsContainer {
            overflow: hidden;
            width: 97%;
            margin-top: 27px;
        }

        .detailsLeft {
            float: left;
            width: 1007px;
            margin-left: 37px;
        }

        .detailsRight {
        }
    </style>

    <h1>WELCOME TO POST INSPECTION REPORTING!</h1>

    <div class="detailsContainer">
        <div class="detailsLeft">
            <h3>User verbiage goes here:</h3>
        </div>
        <div class="detailsRight">
            <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/underConstruction.jpg" />
        </div>
    </div>
</asp:Content>
