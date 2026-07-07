<%@ Page Title="Admin Home" Language="C#" MasterPageFile="~/Admin.Master"
    AutoEventWireup="true"
    CodeBehind="Admin_Home.aspx.cs"
    Inherits="Attendence_System.Admin_Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style>
        .dashboard {
            width: 100%;
            max-width: 1200px;
            margin: 20px auto;
            border-radius: 10px;
            overflow: hidden;
            box-shadow: 0px 5px 15px #cccccc;
        }

        .banner {
            width: 100%;
            height: 500px;
            background-image: url('Images1/Admin_dashboard.jpg');
            background-size: cover;
            background-position: center;
            background-repeat: no-repeat;
            position: relative;
        }

        .welcome {
            position: absolute;
            top: 40px;
            left: 40px;
            color: white;
            background: rgba(0,0,0,.5);
            padding: 15px 25px;
            border-radius: 8px;
            font-size: 36px;
            font-weight: bold;
        }

        .cards {
            display: flex;
            justify-content: center;
            gap: 20px;
            margin: 30px 0;
            flex-wrap: wrap;
        }

        .card {
            width: 180px;
            padding: 20px;
            text-align: center;
            background: white;
            border-radius: 8px;
            box-shadow: 0 0 10px #cccccc;
        }

        .card h3 {
            color: #0d47a1;
            margin-bottom: 15px;
        }

        .btn {
            width: 140px;
            height: 40px;
            background: #1565c0;
            color: white;
            border: none;
            font-weight: bold;
            cursor: pointer;
            border-radius: 5px;
        }

        .btn:hover {
            background: #0d47a1;
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="dashboard">

        <div class="banner">
            <div class="welcome">
                Welcome Admin
            </div>
        </div>

        

    </div>

</asp:Content>