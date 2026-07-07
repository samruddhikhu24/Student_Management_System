<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Attendence_System.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Attendance Management System</title>

    <link href="CSS/Style.css" rel="stylesheet" />
</head>

<body class="login-body">

    <form id="form1" runat="server">

        <div class="login-box">

            <!-- Logo/Image -->
            <div style="text-align:center; margin-bottom:20px;">

                <asp:Image ID="Img1"
                    runat="server"
                    ImageUrl="Images1/student_management.png"
                    Width="320px"
                    Height="170px" />

            </div>

            <h2>Teacher &amp; Admin Login</h2>

            <table style="width:100%;">

                <tr>
                    <td style="width:45%;">
                        <b>Email ID/User ID</b>
                    </td>

                    <td>

                        <asp:TextBox ID="TextBox1"
                            runat="server"
                            placeholder="Enter Email/User ID">
                        </asp:TextBox>

                        <br />

                        <asp:RequiredFieldValidator
                            ID="RequiredFieldValidator1"
                            runat="server"
                            ControlToValidate="TextBox1"
                            ErrorMessage="Enter User ID"
                            ForeColor="Red">
                        </asp:RequiredFieldValidator>

                    </td>
                </tr>

                <tr>
                    <td>
                        <b>Password</b>
                    </td>

                    <td>

                        <asp:TextBox
                            ID="TextBox2"
                            runat="server"
                            TextMode="Password"
                            placeholder="Enter Password">
                        </asp:TextBox>

                        <br />

                        <asp:RequiredFieldValidator
                            ID="RequiredFieldValidator2"
                            runat="server"
                            ControlToValidate="TextBox2"
                            ErrorMessage="Enter Password"
                            ForeColor="Red">
                        </asp:RequiredFieldValidator>

                    </td>
                </tr>

                <tr>

                    <td colspan="2" style="text-align:center; padding-top:20px;">

                        <asp:Button
                            ID="Button1"
                            runat="server"
                            CssClass="btn"
                            Width="250px"
                            Text="Login"
                            OnClick="Button1_Click" />

                    </td>

                </tr>

                <tr>

                    <td colspan="2" style="text-align:center;">

                        <asp:Label
                            ID="Label1"
                            runat="server"
                            Font-Bold="true"
                            ForeColor="Red">
                        </asp:Label>

                    </td>

                </tr>

            </table>

        </div>

    </form>

</body>

</html>