<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master"
    AutoEventWireup="true"
    CodeBehind="TeacherSubject.aspx.cs"
    Inherits="Attendence_System.TeacherSubject" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<center>
<div style="background-image:url('Images1/Admin_dashboard.jpg'); width:1200px;">

<table align="center" style="height:340px; width:392px;">

<tr>
<td colspan="2" align="center">
<h2>Allocate Subject</h2>
<br />
</td>
</tr>

<tr>
<td><b>Teacher :</b></td>
<td>

<asp:DropDownList ID="DropDownList1"
    runat="server"
    Width="197px"
    Height="40px"
    AutoPostBack="True">
</asp:DropDownList>

<asp:RequiredFieldValidator
    ID="RequiredFieldValidator1"
    runat="server"
    ControlToValidate="DropDownList1"
    InitialValue="0"
    ErrorMessage="Select Teacher"
    ForeColor="Red">*</asp:RequiredFieldValidator>

</td>
</tr>

<tr>
<td><b>Course :</b></td>
<td>

<asp:DropDownList ID="DropDownList2"
    runat="server"
    Width="197px"
    Height="40px"
    AutoPostBack="True">
</asp:DropDownList>

<asp:RequiredFieldValidator
    ID="RequiredFieldValidator2"
    runat="server"
    ControlToValidate="DropDownList2"
    InitialValue="0"
    ErrorMessage="Select Course"
    ForeColor="Red">*</asp:RequiredFieldValidator>

</td>
</tr>

<tr>
<td><b>Year :</b></td>
<td>

<asp:DropDownList
    ID="DropDownList3"
    runat="server"
    Width="197px"
    Height="40px"
    AutoPostBack="True"
    OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged">

    <asp:ListItem Value="0">Select Year</asp:ListItem>
    <asp:ListItem Value="1">First Year</asp:ListItem>
    <asp:ListItem Value="2">Second Year</asp:ListItem>
    <asp:ListItem Value="3">Third Year</asp:ListItem>

</asp:DropDownList>

<asp:RequiredFieldValidator
    ID="RequiredFieldValidator3"
    runat="server"
    ControlToValidate="DropDownList3"
    InitialValue="0"
    ErrorMessage="Select Year"
    ForeColor="Red">*</asp:RequiredFieldValidator>

</td>
</tr>

<tr>
<td><b>Semester :</b></td>
<td>

<asp:DropDownList
    ID="DropDownList5"
    runat="server"
    Width="197px"
    Height="40px"
    AutoPostBack="True"
    OnSelectedIndexChanged="DropDownList5_SelectedIndexChanged">

    <asp:ListItem Value="0">Select Semester</asp:ListItem>
    <asp:ListItem Value="1">Semester 1</asp:ListItem>
    <asp:ListItem Value="2">Semester 2</asp:ListItem>
    <asp:ListItem Value="3">Semester 3</asp:ListItem>
    <asp:ListItem Value="4">Semester 4</asp:ListItem>
    <asp:ListItem Value="5">Semester 5</asp:ListItem>
    <asp:ListItem Value="6">Semester 6</asp:ListItem>

</asp:DropDownList>

<asp:RequiredFieldValidator
    ID="RequiredFieldValidator4"
    runat="server"
    ControlToValidate="DropDownList5"
    InitialValue="0"
    ErrorMessage="Select Semester"
    ForeColor="Red">*</asp:RequiredFieldValidator>

</td>
</tr>

<tr>
<td><b>Subject :</b></td>
<td>

<asp:DropDownList
    ID="DropDownList4"
    runat="server"
    Width="197px"
    Height="40px"
    AutoPostBack="True">

    <asp:ListItem Value="0">Select Subject</asp:ListItem>
    <asp:ListItem Value="1">.NET</asp:ListItem>
    <asp:ListItem Value="2">C#</asp:ListItem>
    <asp:ListItem Value="3">Java</asp:ListItem>
    <asp:ListItem Value="4">JavaScript</asp:ListItem>
    <asp:ListItem Value="5">Python</asp:ListItem>
    <asp:ListItem Value="6">Data Analysis</asp:ListItem>

</asp:DropDownList>

<asp:RequiredFieldValidator
    ID="RequiredFieldValidator5"
    runat="server"
    ControlToValidate="DropDownList4"
    InitialValue="0"
    ErrorMessage="Select Subject"
    ForeColor="Red">*</asp:RequiredFieldValidator>

</td>
</tr>

<tr>
<td colspan="2" align="center">

<asp:HiddenField
    ID="HiddenField1"
    runat="server" />

<asp:Button
    ID="Button1"
    runat="server"
    Text="Save"
    Width="100px"
    Height="40px"
    OnClick="Button1_Click" />

</td>
</tr>

<tr>
<td colspan="2" align="center">

<asp:Label
    ID="Label1"
    runat="server"
    Font-Bold="True">
</asp:Label>

</td>
</tr>

<tr>
<td colspan="2">

<asp:ValidationSummary
    ID="ValidationSummary1"
    runat="server"
    ForeColor="Red" />

</td>
</tr>

<tr>
<td colspan="2" align="center">

<asp:GridView
    ID="GridView1"
    runat="server"
    AutoGenerateColumns="False"
    DataKeyNames="ID"
    OnRowCommand="GridView1_RowCommand">

    <Columns>

        <asp:BoundField DataField="Teacher" HeaderText="Teacher" />
        <asp:BoundField DataField="Course" HeaderText="Course" />
        <asp:BoundField DataField="Year" HeaderText="Year" />
        <asp:BoundField DataField="Sem" HeaderText="Semester" />
        <asp:BoundField DataField="Subject" HeaderText="Subject" />

        <asp:TemplateField HeaderText="Edit">
            <ItemTemplate>

                <asp:LinkButton
                    ID="lnkEdit"
                    runat="server"
                    Text="Edit"
                    CommandName="EditRecord"
                    CommandArgument='<%# Eval("ID") %>' />

            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Delete">
            <ItemTemplate>

                <asp:LinkButton
                    ID="lnkDelete"
                    runat="server"
                    Text="Delete"
                    ForeColor="Red"
                    OnClientClick="return confirm('Delete this record?');"
                    CommandName="DeleteRecord"
                    CommandArgument='<%# Eval("ID") %>' />

            </ItemTemplate>
        </asp:TemplateField>

    </Columns>

</asp:GridView>

</td>
</tr>

</table>

<br />
<br />
<br />
<br />
<br />
<br />
<br />

</div>
</center>

</asp:Content>