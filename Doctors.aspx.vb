Imports System.Data
Imports Oracle.ManagedDataAccess.Client

Public Class Doctors
    Inherits System.Web.UI.Page

    Private Const connectionString As String = "User Id=SYS;Password=system;Data Source=Aniket10;DBA Privilege=SYSDBA"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ' Any initial load logic if needed
            Dim updater As New Doctors()
            updater.AddStatusColumnAndSetDefault()
        End If
    End Sub

    Protected Sub btnViewProfile_Click(sender As Object, e As EventArgs)
        Dim doctorID As String = txtDoctorID.Text.Trim()

        If Not String.IsNullOrEmpty(doctorID) Then
            LoadDoctorProfile(doctorID)
            Dim doctorName As String = GetDoctorNameByID(doctorID)
            If Not String.IsNullOrEmpty(doctorName) Then
                LoadDoctorAppointments(doctorName)
            End If
        End If
    End Sub

    Private Function GetDoctorNameByID(doctorID As String) As String
        Dim doctorName As String = String.Empty
        Dim query As String = "SELECT Name FROM Doctors WHERE ID = :doctorID"

        Using conn As New OracleConnection(connectionString)
            Using cmd As New OracleCommand(query, conn)
                cmd.Parameters.Add(New OracleParameter("ID", doctorID))
                conn.Open()
                Dim result As Object = cmd.ExecuteScalar()
                If result IsNot DBNull.Value Then
                    doctorName = result.ToString()
                End If
            End Using
        End Using

        Return doctorName
    End Function

    Private Sub LoadDoctorProfile(doctorID As String)
        Dim query As String = "SELECT Name, Specialisation, PhoneNo FROM Doctors WHERE ID = :DoctorID"

        Using conn As New OracleConnection(connectionString)
            Using cmd As New OracleCommand(query, conn)
                cmd.Parameters.Add(New OracleParameter("ID", doctorID))
                conn.Open()

                Using reader As OracleDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                        lblDoctorProfile.Text = "Doctor Name: " & reader("Name").ToString() & "<br />" &
                                                "Specialisation: " & reader("Specialisation").ToString() & "<br />" &
                                                "Phone No: " & reader("PhoneNo").ToString()
                    Else
                        lblDoctorProfile.Text = "No doctor found with ID: " & doctorID
                    End If
                End Using
            End Using
        End Using
    End Sub

    Private Sub LoadDoctorAppointments(doctorName As String)
        Dim query As String = "SELECT e.AppointmentID, e.DependentName, e.AppointmentDate, e.Slot,e.Status FROM Appointments e, Doctors d WHERE d.Name = e.Doctor AND d.Name =:doctorName and e.Doctor =:doctorName"

        Using conn As New OracleConnection(connectionString)
            Using cmd As New OracleCommand(query, conn)
                cmd.Parameters.Add(New OracleParameter("Name", doctorName))
                conn.Open()

                Using adapter As New OracleDataAdapter(cmd)
                    Dim appointments As New DataTable()
                    adapter.Fill(appointments)
                    gvAppointments.DataSource = appointments
                    gvAppointments.DataBind()
                End Using
            End Using
        End Using
    End Sub

    Protected Sub chkStatus_CheckedChanged(sender As Object, e As EventArgs)
        Dim chkStatus As CheckBox = CType(sender, CheckBox)
        Dim Row As GridViewRow = CType(chkStatus.NamingContainer, GridViewRow)
        Dim appointmentID As String = gvAppointments.DataKeys(Row.RowIndex).Value.ToString()
        Dim status As String = If(chkStatus.Checked, "Yes", "No")

        UpdateAppointmentStatus(appointmentID, status)
    End Sub

    Private Sub UpdateAppointmentStatus(appointmentID As String, status As String)
        Dim query As String = "UPDATE Appointments SET Status = :Status WHERE AppointmentID = :AppointmentID"

        Using conn As New OracleConnection(connectionString)
            Using cmd As New OracleCommand(query, conn)
                cmd.Parameters.Add(New OracleParameter("Status", status))
                cmd.Parameters.Add(New OracleParameter("AppointmentID", appointmentID))
                conn.Open()
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Public Sub AddStatusColumnAndSetDefault()
        Using conn As New OracleConnection(connectionString)
            conn.Open()

            ' Add the Status column if it does not exist
            Dim addColumnQuery As String = "ALTER TABLE Appointments ADD (Status VARCHAR2(10) DEFAULT 'No' NOT NULL)"

            Using cmd As New OracleCommand(addColumnQuery, conn)
                Try
                    cmd.ExecuteNonQuery()
                Catch ex As OracleException When ex.Number = 1430 ' ORA-01430: column being added already exists
                    ' Column already exists, do nothing
                End Try
            End Using

            ' Update all rows to set Status to 'No' if they are NULL
            Dim updateColumnQuery As String = "UPDATE Appointments SET Status = 'No' WHERE Status IS NULL"

            Using cmd As New OracleCommand(updateColumnQuery, conn)
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Protected Sub gvAppointments_RowDataBound(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(0).Text = (e.Row.RowIndex + 1).ToString()
        End If
    End Sub
End Class
