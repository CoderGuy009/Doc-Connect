<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Employee.aspx.vb" Inherits="WebApplication1.Employee" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Book Appointment</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            margin: 0;
            padding: 0;
            background: url('Hospital_2.jpg') no-repeat center center fixed;
            background-size: cover;
        }
        .header {
            background-color: white; /* Semi-transparent background */
            padding: 10px 20px;
            color: white;
            text-align: center;
            display: flex;
            align-items: center;
            justify-content: center;
            margin-bottom: 0; /* Adjusted margin */
        }
        .header img {
            height: 50px;
            margin-right: 10px;
        }
        .header h1 {
            margin: 0;
            font-size: 36px;
            font-weight: bold;
            color: darkblue; /* Changed text color to dark blue */
        }
        .header p {
            margin: 0;
            font-size: 16px;
            color: darkblue;
        }
        .blue-bar {
            background-color: #004080; /* Blue color */
            color: white;
            text-align: center;
            padding: 10px 0;
            font-size: 20px;
            font-weight: bold;
        }
        .container {
            width: 100%;
            max-width: 600px; /* Adjusted max-width */
            margin: 20px auto;
            padding: 10px; /* Adjusted padding */
            background-color: rgba(255, 255, 255, 0.9); /* Slightly transparent background */
            border: 1px solid #cccccc;
            border-radius: 10px;
            box-shadow: 0 0 10px rgba(0,0,0,0.1);
        }
        .form-section {
            padding: 5px; /* Adjusted padding */
            border: 1px solid #004080;
            border-radius: 5px;
            margin-bottom: 10px; /* Adjusted margin */
        }
        .form-section h2 {
            background-color: white; /* Changed background color to white */
            color: #004080; /* Changed text color to dark blue */
            padding: 5px; /* Adjusted padding */
            margin: -10px -10px 10px -10px;
            border-radius: 5px 5px 0 0;
            font-size: 16px; /* Adjusted font size */
        }
        .form-row {
            display: flex;
            align-items: center;
            margin-bottom: 10px;
            padding: 1px;
        }
        label {
            font-weight: bold;
            width: 150px;
            font-size: 12px; /* Adjusted font size */
            margin-right: 10px;
        }
        input[type="date"], input[type="text"], select, .form-control, .form-label {
            padding: 2px; /* Adjusted padding */
            border: 1px solid #ccc;
            border-radius: 5px;
            font-size: 10px; /* Adjusted font size */
        }
        .form-label {
            padding: 0; /* Remove padding for labels */
            margin: 0; /* Remove margin for labels */
            border: none; /* Remove border for labels */
            background: none; /* Remove background for labels */
            font-weight: bold;
            width: 150px;
            font-size: 12px;
        }
        .form-actions {
            text-align: center;
            font-size: 12px;
        }
        .form-actions button, .form-actions .asp:Button {
            width: 50%;
            padding: 5px; /* Adjusted padding */
            background-color: #4CAF50;
            color: white;
            font-size: 8px; /* Adjusted font size */
            border: none;
            cursor: pointer;
            border-radius: 5px;
            margin: 5px;
            font-weight: bold;
        }
        button:hover {
            opacity: 0.8;
        }
        .message {
            margin-top: 10px; /* Adjusted margin */
            font-weight: bold;
            text-align: center;
            font-size: 12px; /* Adjusted font size */
            text-align: center;
        }
        .asp-button {}
    </style>
    <script>
        document.addEventListener("DOMContentLoaded", function () {
            var today = new Date();
            var tomorrow = new Date();
            tomorrow.setDate(today.getDate() + 1);

            var formattedToday = today.toISOString().split('T')[0];
            var formattedTomorrow = tomorrow.toISOString().split('T')[0];

            var dateInput = document.getElementById("txt_date");
            dateInput.setAttribute("min", formattedToday);
            dateInput.setAttribute("max", formattedTomorrow);
        });
    </script>
</head>
<body>
    <div class="header">
        <img src="IOCL.png" alt="Logo">
        <div>
            <h1>IndianOil</h1>
            <p>Hospital Management System</p>
        </div>
    </div>
    <div class="blue-bar">Employee Dashboard</div>

    <div class="container">
        <form id="Form1" runat="server" onsubmit="return validateForm()">
            <h2>Patient Info</h2> <!-- Added heading for Patient Info -->
            <div class="form-section">
                <div class="form-row">
                    <label for="patientID">Patient ID:</label>
                    <asp:TextBox ID="txtPatientID" runat="server" CssClass="form-control" Width="84px" Height="17px"></asp:TextBox>
                    <asp:Button ID="btnFetchPatientName" runat="server" Text="Fetch Name" CssClass="asp-button" OnClick="btnFetchPatientName_Click" Height="22px" Font-Bold="True" Font-Size="8pt" />
                </div>
                <div class="form-row">
                    <label for="patientName">Patient Name:</label>
                    <asp:DropDownList ID="ddlPatientNames" runat="server" CssClass="form-control" />
                </div>
                <div class="form-row">
                    <label for="category">Category:</label>
                    <asp:Label ID="lblCategory" runat="server" CssClass="form-label"></asp:Label>
                </div>
                <div class="form-row">
                    <label for="relation">Relation:</label>
                    <asp:Label ID="lblRelation" runat="server" CssClass="form-label"></asp:Label>
                </div>
                <div class="form-row">
                    <label for="gender">Gender:</label>
                    <asp:Label ID="lblGender" runat="server" CssClass="form-label"></asp:Label>
                </div>
                <div class="form-row">
                    <label for="age">Age:</label>
                    <asp:Label ID="lblAge" runat="server" CssClass="form-label"></asp:Label>
                </div>
                <div class="form-row">
                    <asp:Button ID="btnFetchPatientInfo" runat="server" Text="Fetch Details" CssClass="asp-button" OnClick="btnFetchPatientInfo_Click" Height="21px" Font-Bold="True" Font-Size="8pt" Width="110px" />
                </div>
                <div>
                    <asp:Label ID="lblMessage1" runat="server" CssClass="message"></asp:Label>
                </div>
            </div>
            
            <h2>Appointment Details</h2> <!-- Added heading for Appointment Details -->
            <div class="form-section">
                <div class="form-row">
                    <label for="date">Date:</label>
                    <input type="date" id="txt_date" name="date" runat="server" class="form-control">
                </div>
                <div class="form-row">
                    <label for="specialty">Specialisation:</label>
                    <asp:DropDownList ID="ddlSpecialty" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Select Specialty" Value="" />
                        <asp:ListItem Text="Cardiology" Value="Cardiology" />
                        <asp:ListItem Text="Neurology" Value="Neurology" />
                        <asp:ListItem Text="Pediatrics" Value="Pediatrics" />
                        <asp:ListItem Text="Orthopedics" Value="Orthopedics" />
                        <asp:ListItem Text="Medicine" Value="Medicine" />
                    </asp:DropDownList>
                    <asp:Button ID="btnFetchDoctors" runat="server" Text="Fetch Doctors" CssClass="asp-button" OnClick="btnFetchDoctors_Click" Height="20px" Width="98px" Font-Bold="True" Font-Size="8pt" />
                </div>
                <div class="form-row">
                    <label for="doctor">Doctor Name:</label>
                    <asp:DropDownList ID="ddlDoctor" runat="server" AutoPostBack="False" CssClass="form-control"></asp:DropDownList>
                    <asp:Button ID="btnFetchSlots" runat="server" Text="Fetch Slots" CssClass="asp-button" OnClick="btnFetchSlots_Click" Height="20px" Width="86px" Font-Bold="True" Font-Size="8pt" />
                </div>
                <div class="form-row">
                    <label for="slot">Preferred Slot:</label>
                    <asp:DropDownList ID="ddlSlot" runat="server" AutoPostBack="False" CssClass="form-control"></asp:DropDownList>
                </div>
                <div class="form-actions">
                    <asp:Button ID="btnBookAppointment" runat="server" Text="Book Appointment" CssClass="asp-button" OnClick="btnBookAppointment_Click" BackColor="#3399FF" BorderColor="#3399FF" Font-Bold="True" />
                </div>
                <asp:Label ID="lblMessage" runat="server" CssClass="message" />
            </div>
        </form>
    </div>
</body>
</html>
