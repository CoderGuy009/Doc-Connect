Imports System.Globalization
Imports Oracle.ManagedDataAccess.Client

Public Class Employee
    Inherits System.Web.UI.Page

    Protected Sub btnFetchPatientName_Click(sender As Object, e As EventArgs) Handles btnFetchPatientName.Click
        ' Code to fetch patient details when btnFetchButton is clicked
        Dim patientID As Integer
        If Integer.TryParse(txtPatientID.Text, patientID) AndAlso patientID > 0 Then
            LoadPatientNames(patientID)
        End If
    End Sub

    Protected Sub btnBookAppointment_Click(sender As Object, e As EventArgs) Handles btnBookAppointment.Click
        ' Code for booking appointment
        If txt_date.Value <> "" AndAlso ddlSpecialty.Text <> "Select Specialty" AndAlso ddlDoctor.Text <> "Select Doctor" AndAlso ddlSlot.Text <> "Select Slot" Then
            Dim appointmentDate As Date = Date.Parse(txt_date.Value, CultureInfo.InvariantCulture)
            Dim patientID As Integer = Integer.Parse(txtPatientID.Text)
            Dim dependentName As String = ddlPatientNames.SelectedValue
            Dim specialisation As String = ddlSpecialty.SelectedValue
            Dim doctor As String = ddlDoctor.SelectedValue
            Dim slot As String = ddlSlot.SelectedValue

            ' Insert into Oracle database
            Dim connectionString As String = "User Id=SYS;Password=system;Data Source=Aniket10;DBA Privilege=SYSDBA" ' Replace with your Oracle connection string
            Using con As New OracleConnection(connectionString)
                Dim query As String = "INSERT INTO Appointments (AppointmentID, DependentName, AppointmentDate, Specialty, Doctor, Slot) " &
                                      "VALUES (:AppointmentID, :DependentName, :AppointmentDate, :Specialty, :Doctor, :Slot)"

                Using cmd As New OracleCommand(query, con)
                    cmd.Parameters.Add(":AppointmentID", OracleDbType.Int32).Value = GetMaxAppointmentID() + 1
                    cmd.Parameters.Add(":DependentName", OracleDbType.Varchar2).Value = dependentName
                    cmd.Parameters.Add(":AppointmentDate", OracleDbType.Date).Value = appointmentDate
                    cmd.Parameters.Add(":Specialty", OracleDbType.Varchar2).Value = specialisation
                    cmd.Parameters.Add(":Doctor", OracleDbType.Varchar2).Value = doctor
                    cmd.Parameters.Add(":Slot", OracleDbType.Varchar2).Value = slot

                    Try
                        con.Open()
                        cmd.ExecuteNonQuery()
                        lblMessage.Text = "Appointment booked successfully!"
                    Catch ex As Exception
                        lblMessage.Text = "Error booking appointment: " & ex.Message
                    End Try
                End Using
            End Using
        Else
            lblMessage.Text = "Please select all fields correctly."
        End If
    End Sub

    Private Function GetMaxAppointmentID() As Integer
        Dim maxAppointmentID As Integer = 0
        Dim query As String = "SELECT MAX(AppointmentID) FROM Appointments"
        Dim connectionString As String = "User Id=SYS;Password=system;Data Source=Aniket10;DBA Privilege=SYSDBA"
        Using conn As New OracleConnection(connectionString)
            Using cmd As New OracleCommand(query, conn)
                conn.Open()
                Dim result As Object = cmd.ExecuteScalar()
                If result IsNot DBNull.Value Then
                    maxAppointmentID = Convert.ToInt32(result)
                End If
            End Using
        End Using

        Return maxAppointmentID
    End Function

    Private Sub LoadPatientNames(patientID As Integer)
        Dim query As String = "SELECT Name FROM Dependents WHERE Id = :patientID"

        Dim connectionString As String = "User Id=SYS;Password=system;Data Source=Aniket10;DBA Privilege=SYSDBA"
        Using conn As New OracleConnection(connectionString)
            Using cmd As New OracleCommand(query, conn)
                cmd.Parameters.Add("patientID", OracleDbType.Int32).Value = patientID
                conn.Open()

                Using reader As OracleDataReader = cmd.ExecuteReader()
                    ddlPatientNames.Items.Clear()
                    If reader.HasRows Then
                        While reader.Read()
                            Dim listItem As New ListItem()
                            listItem.Text = reader("Name").ToString()
                            listItem.Value = reader("Name").ToString()
                            ddlPatientNames.Items.Add(listItem)
                        End While
                        lblMessage1.Text = "Patient Names loaded successfully"
                    Else
                        ' Clear fields if patient not found
                        lblMessage1.Text = "Patient not found."
                    End If
                End Using
            End Using
        End Using
    End Sub

    Private Sub LoadPatientDetails(dependentName As String)
        Dim query As String = "SELECT Category, Relation, Gender, Age FROM Dependents WHERE Name = :dependentName"

        Dim connectionString As String = "User Id=SYS;Password=system;Data Source=Aniket10;DBA Privilege=SYSDBA"
        Using conn As New OracleConnection(connectionString)
            Using cmd As New OracleCommand(query, conn)
                cmd.Parameters.Add("dependentName", OracleDbType.Varchar2).Value = dependentName
                conn.Open()

                Using reader As OracleDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                        lblCategory.Text = reader("Category").ToString()
                        lblRelation.Text = reader("Relation").ToString()
                        lblGender.Text = reader("Gender").ToString()
                        lblAge.Text = reader("Age").ToString()
                        lblMessage1.Text = "Patient Details loaded successfully!!"
                    Else
                        ' Clear fields if patient not found
                        lblCategory.Text = String.Empty
                        lblRelation.Text = String.Empty
                        lblGender.Text = String.Empty
                        lblAge.Text = String.Empty
                        lblMessage1.Text = "Patient details not found"
                    End If
                End Using
            End Using
        End Using
    End Sub

    Protected Sub btnFetchDoctors_Click(sender As Object, e As EventArgs) Handles btnFetchDoctors.Click
        ' Load doctors based on selected specialty
        If ddlSpecialty.SelectedValue <> "Select Specialty" Then
            LoadDoctorsBySpecialty(ddlSpecialty.SelectedValue)
        End If
    End Sub

    Private Sub LoadDoctorsBySpecialty(specialty As String)
        Dim query As String = "SELECT Name FROM Doctors WHERE Specialisation = :specialty"

        Dim connectionString As String = "User Id=SYS;Password=system;Data Source=Aniket10;DBA Privilege=SYSDBA"
        Using conn As New OracleConnection(connectionString)
            Using cmd As New OracleCommand(query, conn)
                cmd.Parameters.Add("specialisation", OracleDbType.Varchar2).Value = specialty
                conn.Open()

                Using reader As OracleDataReader = cmd.ExecuteReader()
                    ddlDoctor.Items.Clear()
                    If reader.HasRows Then
                        While reader.Read()
                            Dim listItem As New ListItem()
                            listItem.Text = reader("Name").ToString()
                            listItem.Value = reader("Name").ToString()
                            ddlDoctor.Items.Add(listItem)
                        End While
                    Else
                        ddlDoctor.Items.Add(New ListItem("No doctors available", "No doctors available"))
                    End If
                End Using
                conn.Close()

            End Using
        End Using
    End Sub

    Protected Sub btnFetchSlots_Click(sender As Object, e As EventArgs) Handles btnFetchSlots.Click
        ' Load available slots based on selected doctor and selected appointment date
        If ddlDoctor.SelectedValue <> "Select Doctor" AndAlso txt_date.Value <> "" Then
            Dim appointmentDate As Date = Date.Parse(txt_date.Value, CultureInfo.InvariantCulture)
            LoadAvailableSlots(ddlDoctor.SelectedValue, appointmentDate)
        End If
    End Sub


    Private Sub LoadAvailableSlots(doctor As String, appointmentDate As Date)
        Dim query As String = "SELECT * FROM Schedule WHERE Name =:doctor AND Day =:DayOfWeek"
        Dim connectionString As String = "User Id=SYS;Password=system;Data Source=Aniket10;DBA Privilege=SYSDBA"

        Using conn As New OracleConnection(connectionString)
            Using cmd As New OracleCommand(query, conn)
                cmd.Parameters.Add("doctor", OracleDbType.Varchar2).Value = doctor
                cmd.Parameters.Add("DayOfWeek", OracleDbType.Varchar2).Value = appointmentDate.DayOfWeek.ToString().Substring(0, 3).TrimEnd()

                conn.Open()

                ' Debugging: Print query and parameters
                'Response.Write("<script>console.log('Query: " & query & "')</script>")
                'Response.Write("<script>console.log('Name: " & doctor & "')</script>")
                'Response.Write("<script>console.log('Day: " & appointmentDate.DayOfWeek.ToString() & "')</script>")

                Using reader As OracleDataReader = cmd.ExecuteReader()
                    ddlSlot.Items.Clear()
                    If reader.HasRows Then
                        While reader.Read()
                            Dim listItem As New ListItem()
                            listItem.Text = reader("Slot").ToString()
                            listItem.Value = reader("Slot").ToString()
                            ddlSlot.Items.Add(listItem)
                        End While
                    Else
                        ddlSlot.Items.Add(New ListItem("No slots available", "No slots available"))
                    End If
                End Using
            End Using
        End Using
    End Sub



    Protected Sub ddlPatientNames_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPatientNames.SelectedIndexChanged
        ' Load patient details when a name is selected from the dropdown list
        If ddlPatientNames.SelectedValue <> "" Then
            LoadPatientDetails(ddlPatientNames.SelectedValue)
        End If
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        ' Ensure that the dropdown list events are wired up correctly
        If Not IsPostBack Then
            ddlPatientNames.AutoPostBack = True
        End If
    End Sub

    Protected Sub btnFetchPatientInfo_Click(sender As Object, e As EventArgs) Handles btnFetchPatientInfo.Click
        Dim dependentName As String = ddlPatientNames.SelectedValue
        If Not String.IsNullOrEmpty(dependentName) Then
            LoadPatientDetails(dependentName)
        End If
    End Sub
End Class
