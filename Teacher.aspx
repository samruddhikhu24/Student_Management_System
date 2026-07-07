<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master"
    AutoEventWireup="true"
    CodeBehind="Teacher.aspx.cs"
    Inherits="Attendence_System.Teacher" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

<style>

.teacher-page{
    width:1200px;
    min-height:850px;
    margin:20px auto;

    background-image:url('Images1/teacher_dashboard.jpg');
    background-repeat:no-repeat;
    background-size:cover;
    background-position:center;

    position:relative;
    border-radius:10px;
}

.teacher-content{

    width:430px;

    background:rgba(255,255,255,.90);

    margin:auto;

    padding:25px;

    position:absolute;

    top:40px;

    left:50%;

    transform:translateX(-50%);

    border-radius:10px;

    box-shadow:0 0 15px gray;
}

.teacher-content table{

    width:100%;
}

.teacher-content td{

    padding:8px;
}

.teacher-content input[type=text],
.teacher-content input[type=password],
.teacher-content input[type=email]{

    width:100%;
    height:38px;
}

.addbtn{

    width:140px;
    height:45px;
    font-size:17px;
    font-weight:bold;
}

.grid{

    width:90%;
    margin:25px auto;
}

</style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="teacher-page">

<div class="teacher-content">

<h2 style="text-align:center;">Add Teacher</h2>

<table>

<tr>
<td><b>First Name</b></td>
<td>
<asp:TextBox ID="TextBox2" runat="server" placeholder="Teacher's First Name"></asp:TextBox>
<asp:RequiredFieldValidator ID="RequiredFieldValidator2"
runat="server"
ControlToValidate="TextBox2"
ErrorMessage="*"
ForeColor="Red" />
</td>
</tr>

<tr>
<td><b>Last Name</b></td>
<td>
<asp:TextBox ID="TextBox4" runat="server" placeholder="Teacher's Last Name"></asp:TextBox>
<asp:RequiredFieldValidator ID="RequiredFieldValidator4"
runat="server"
ControlToValidate="TextBox4"
ErrorMessage="*"
ForeColor="Red" />
</td>
</tr>

<tr>
<td><b>Email</b></td>
<td>
<asp:TextBox ID="TextBox3"
runat="server"
TextMode="Email"
placeholder="Teacher Email"></asp:TextBox>
<asp:RequiredFieldValidator ID="RequiredFieldValidator3"
runat="server"
ControlToValidate="TextBox3"
ErrorMessage="*"
ForeColor="Red" />
</td>
</tr>

<tr>
<td><b>Password</b></td>
<td>
<asp:TextBox ID="TextBox1"
runat="server"
TextMode="Password"
placeholder="Password"></asp:TextBox>
<asp:RequiredFieldValidator ID="RequiredFieldValidator1"
runat="server"
ControlToValidate="TextBox1"
ErrorMessage="*"
ForeColor="Red" />
</td>
</tr>

<tr>
<td colspan="2" align="center">

<asp:Button ID="Button1"
runat="server"
Text="Add Teacher"
CssClass="addbtn"
OnClick="Button1_Click" />

</td>
</tr>

<tr>
<td colspan="2">

<asp:Label ID="Label1"
runat="server"
ForeColor="Green"
Font-Bold="true"></asp:Label>

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

</table>

</div>

</div>

<div class="grid">
    <asp:HiddenField ID="HiddenField1" runat="server" />
<asp:GridView ID="GridView1"
    runat="server"
    AutoGenerateColumns="False"
    DataKeyNames="TeacherID"
    Width="100%"
    OnRowCommand="GridView1_RowCommand">

    <Columns>

        <asp:BoundField DataField="TeacherID" HeaderText="ID" />

        <asp:BoundField DataField="TeacherName" HeaderText="Teacher Name" />

        <asp:BoundField DataField="USERNAME" HeaderText="Email" />

        <asp:ButtonField
            Text="Edit"
            CommandName="EditTeacher"
            ButtonType="Button" />

        <asp:ButtonField
            Text="Delete"
            CommandName="DeleteTeacher"
            ButtonType="Button" />

    </Columns>

</asp:GridView>

<br />

<asp:GridView
ID="GridViewTeacher"
runat="server"
Width="100%">
</asp:GridView>

<br />

<asp:GridView
ID="GridViewCourse"
runat="server"
Width="100%">
</asp:GridView>

<br />

<asp:GridView
ID="GridViewSubject"
runat="server"
Width="100%">
</asp:GridView>

</div>

</asp:Content>