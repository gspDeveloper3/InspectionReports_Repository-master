<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ComInspectionGrid.aspx.cs" Inherits="PostCommInspectionReport.ComInspectionReport.ComInspectionGrid" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <table>
        <tr>
            <td style="height: 117px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<h2>Communication Inspection Reports Available:
                <asp:Label ID="lblTroop" runat="server" CssClass="control-label"></asp:Label></h2>
                <p>
                    <asp:RadioButtonList ID="rbGridSortList" runat="server" AutoPostBack="True" CellPadding="2" CellSpacing="2" RepeatDirection="Horizontal" OnSelectedIndexChanged="rbGridSortList_OnSelectedIndexChanged">
                        <asp:ListItem Value="1" Selected="True">Pending</asp:ListItem>
                        <asp:ListItem Value="2">Approved</asp:ListItem>
                        <asp:ListItem Value="3">Rejected</asp:ListItem>
                        <asp:ListItem Value="4">Incomplete</asp:ListItem>
                        <%--<asp:ListItem Value="4" Selected="True">All</asp:ListItem>--%>
                    </asp:RadioButtonList>
                </p>
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView runat="server" ID="GridView1"
                    AutoGenerateColumns="False"
                    DataKeyNames="ReportID"
                    DataSourceID="SqlDataSource1"
                    OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
                    CellPadding="7"
                    ForeColor="#333333"
                    AllowPaging="True"
                    AllowSorting="True"
                    SortDirection="Descending"
                    BorderWidth="1px"
                    CellSpacing="7"
                    ShowHeaderWhenEmpty="True"
                    CssClass="table">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderText="Record ID">
                            <ItemTemplate>
                                <%--<asp:LinkButton ID="MainLink" runat="server" Text= "Record ID" CommandName="ReportID" CommandArgument='<%#Bind("R_ID ") %>'></asp:LinkButton>--%>
                                <asp:LinkButton ID="MainLink" runat="server" Text='<%# Eval("ReportID") %>' OnClick="MainLink_Click"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="CreatedBy" HeaderText="Created By" SortExpression="CreatedBy" />
                        <asp:BoundField DataField="CreatedDate" HeaderText="Created Date" SortExpression="CreatedDate" />
                        <asp:BoundField DataField="ApprovalOfficer" HeaderText="Approval Officer" SortExpression="ApprovalOfficer" />
                        <asp:BoundField DataField="ApprovalStatus" HeaderText="Approval Status" SortExpression="ApprovalStatus" />
                        <asp:BoundField DataField="ReportStage" HeaderText="Report Stage" SortExpression="ReportStage" />
                    </Columns>
                    <EditRowStyle BackColor="#2461BF" />
                    <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle ForeColor="White" HorizontalAlign="Center" BackColor="#2461BF" />
                    <RowStyle BackColor="#EFF3FB" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:PostCommInspectionReport.Properties.Settings.spConnect %>" SelectCommand="SELECT [ReportID], [CreatedBy], [CreatedDate], [ApprovalOfficer], [ApprovalStatus], [ReportStage] FROM [CommInspectionReport] ORDER BY [ApprovalStatus] DESC, [CreatedDate] DESC" ProviderName="<%$ ConnectionStrings:PostCommInspectionReport.Properties.Settings.userConnect.ProviderName %>"></asp:SqlDataSource>
            </td>
        </tr>
    </table>
</asp:Content>
