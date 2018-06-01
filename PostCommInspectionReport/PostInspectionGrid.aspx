<%@ Page Title="Reports" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PostInspectionGrid.aspx.cs" Inherits="PostCommInspectionReport.PostInspectionGrid" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
    <h2>SEARCH POST INSPECTION DATA</h2>

    <div style="margin-bottom: 27px;">
        <span>Search By: </span>
        <asp:DropDownList ID="ddlSearch" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSearch_OnSelectedIndexChanged" CssClass="dropdown form-control">
            <asp:ListItem Text="-- Select One --" Value="-1" Selected="True" />
            <asp:ListItem Text="Report Id" Value="reportID" />
            <asp:ListItem Text="Post" Value="post" />
            <asp:ListItem Text="Troop" Value="troop" />
        </asp:DropDownList>&nbsp;&nbsp;&nbsp;<asp:TextBox ID="tbxSearch" runat="server" Width="125px" CssClass="form-control" Enabled="false" />
        &nbsp;&nbsp;&nbsp;<asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn-primary form-control" OnClick="btnSearch_OnClick"  />
        &nbsp;&nbsp;&nbsp;<asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn-danger form-control" OnClick="btnClear_OnClick" />
        <span style="margin-left: 57px;">
            <asp:Label ID="lblSearchErrorMst" runat="server" ForeColor="Red" Visible="False" Font-Size="1.1em" /></span>
    </div>
   

    <table>
        <tr>
            <td style="height: 117px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<h2> Post Inspection Reports Available: <asp:label ID="lblTroop" runat="server" CssClass="control-label"></asp:label></h2>
                <p>
                    <asp:RadioButtonList ID="rbGridSortList" runat="server" AutoPostBack="True" CellPadding="2" CellSpacing="2" RepeatDirection="Horizontal" OnSelectedIndexChanged="rbGridSortList_OnSelectedIndexChanged">
                        <asp:ListItem Value="1" Selected="True">Pending</asp:ListItem>
                        <asp:ListItem  Value="2">Approved</asp:ListItem>
                        <asp:ListItem Value="3">Rejected</asp:ListItem>
                        <asp:ListItem Value="4">Incomplete</asp:ListItem>
                        <%--<asp:ListItem Value="4" Selected="True">All</asp:ListItem>--%>
                    </asp:RadioButtonList>
                </p></td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gridPostInspection" 
                     runat="server" 
                     AutoGenerateColumns="False" 
                     CellPadding="7" 
                     ForeColor="#333333" 
                     AllowPaging="True" 
                     AllowSorting="True" 
                     SortDirection="Descending" 
                     BorderWidth="1px" 
                     CellSpacing="7" 
                     ShowHeaderWhenEmpty="True" 
                     CssClass="table" 
                     OnPageIndexChanging="gridPostInspection_OnPageIndexChanging"
                     DataKeyNames="ReportStatus" >
                    <%-- DataSourceID="SqlDataSource1"--%>
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <%--<asp:BoundField HeaderText="ID" DataField="ID" InsertVisible="False" ReadOnly="True" SortExpression="ID" />--%>
                       <%-- <asp:BoundField HeaderText="ID" DataField="ID" InsertVisible="False" ReadOnly="True" SortExpression="ID"  /> --%>
                        <asp:BoundField HeaderText="Troop" DataField="Troop" />
                        <asp:BoundField HeaderText="ReportId" DataField="ReportId"  /> 
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lblReportIDLink" runat="server" Text="View" OnClick="lblReportIDLink_OnClick"  CommandArgument='<%# Eval("ReportId").ToString() %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Post" DataField="Post"/>
                        <asp:BoundField HeaderText="City" DataField="City" />
                        <asp:BoundField HeaderText="CreatedBy" DataField="CreatedBy"/>
                        <asp:BoundField HeaderText="Created" DataField="Created" />
                        <asp:BoundField HeaderText="ModifiedBy" DataField="ModifiedBy"/>
                        <asp:BoundField DataField="Modified" HeaderText="Modified" />
                       
                       <%-- <asp:BoundField DataField="ReportId" HeaderText="ReportId" SortExpression="ReportId"  />--%>
                        <asp:BoundField DataField="ReportStatus" HeaderText="ReportStatus"  />
                       
                        <asp:BoundField DataField="ReportStage" HeaderText="ReportStage" />
                       
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
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:PostCommInspectionReport.Properties.Settings.spConnect %>" SelectCommand="SELECT DISTINCT ID, Troop, Post, City, CreatedBy, Created, ModifiedBy, Modified, ReportId, ReportStatus, ReportStage FROM PostInspectionReport WHERE (Troop = @Troop) ORDER BY Modified DESC">
                    <SelectParameters>
                        <asp:SessionParameter Name="Troop" SessionField="Troop" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
    </table>

</asp:Content>
