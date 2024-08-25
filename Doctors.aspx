<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Doctors.aspx.vb" Inherits="WebApplication1.Doctors" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Doctor Appointments - IOCL Hospital Management</title>
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
            font-size: 12px;
            font-weight: bold;

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
            
            align-items: center;
            margin-bottom: 10px;
        }
        label {
            font-weight: bold;
            width: 120px;
            font-size: 12px; /* Adjusted font size */
            margin-right: 10px;
        }
        input[type="date"], input[type="text"], select, .form-control, .form-label {
            
            padding: 2px; /* Adjusted padding */
            border: 1px solid #ccc;
            border-radius: 5px;
            font-size: 12px; /* Adjusted font size */
            width: 10%; /* Set width to 30% */
        }
        .form-label {
            padding: 0; /* Remove padding for labels */
            margin: 0; /* Remove margin for labels */
            border: none; /* Remove border for labels */
            background: none; /* Remove background for labels */
        }
        .form-actions {
            text-align: center;
        }
        .form-actions button, .form-actions  {
            width: 48%;
            padding: 5px; /* Adjusted padding */
            background-color: #4CAF50;
            color: white;
            font-size: 14px; /* Adjusted font size */
            border: none;
            cursor: pointer;
            border-radius: 5px;
            margin: 5px;
        }
        .form-actions button.clear {
            background-color: #f44336;
        }
        button:hover {
            opacity: 0.8;
        }
        .message {
            margin-top: 10px; /* Adjusted margin */
            font-weight: bold;
            text-align: center;
            font-size: 12px; /* Adjusted font size */
        }
        .gridview {
            width: 100%;
            border-collapse: collapse;
            margin-top: 20px;
        }
        .gridview th, .gridview td {
            border: 1px solid #004080;
            padding: 8px;
            text-align: center;
            font-size: 12px;
            font-weight: bold;
        }
        .gridview th {
            background-color: #004080;
            color: white;
        }
        .gridview tr:nth-child(even) {
            background-color: #f2f2f2;
        }
        .gridview tr:hover {
            background-color: #ddd;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="header">
            <img src="IOCL.png" alt="Logo">
            <div>
                <h1>IndianOil</h1>
                <p>Hospital Management System</p>
            </div>
        </div>
        <div class="blue-bar">Doctor Dashboard</div>

        <div class="container">
            <div class="doctor-id-container form-section">
                <h2>Enter Your Doctor ID</h2>
                <div class="form-row">
                    <label for="txtDoctorID">Doctor ID:</label>
                    <asp:TextBox ID="txtDoctorID" runat="server" />
                </div>
                <div>
                    <asp:Button ID="btnViewProfile" runat="server" Text="View Profile" OnClick="btnViewProfile_Click" />
                </div>
            </div>

            <div class="form-section">
                <h2>Doctor Profile</h2>
                <asp:Label ID="lblDoctorProfile" runat="server" Text="" />
            </div>

            <div class="form-section">
                <h2>Appointments</h2>
                
                <asp:GridView ID="gvAppointments" runat="server" AutoGenerateColumns="False" DataKeyNames="AppointmentID" CssClass="gridview" OnRowDataBound="gvAppointments_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="AppointmentID" HeaderText="Appointment ID" />
                        <asp:BoundField DataField="DependentName" HeaderText="Patient Name" />
                        <asp:BoundField DataField="AppointmentDate" HeaderText="Appointment Date" DataFormatString="{0:dd-MM-yyyy}" />
                        <asp:BoundField DataField="Slot" HeaderText="Slot" />
                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkStatus" runat="server" Checked='<%# Eval("Status").ToString() = "Yes" %>' AutoPostBack="True" OnCheckedChanged="chkStatus_CheckedChanged" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </form>
</body>
</html>
