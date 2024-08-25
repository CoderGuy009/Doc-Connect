Imports System.Data
Imports Oracle.ManagedDataAccess.Client ' Replace with Oracle.DataAccess.Client for older versions

Public Class Employee
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnBookAppointment_Click(sender As Object, e As EventArgs) Handles btnBookAppointment.Click
        ' Retrieve values from form controls

        Dim appointmentDate As Date = Date.Parse(Date.MinValue)
        Dim dependentName As String = dependent.Value
        Dim specialty As String = ddlSpecialty.SelectedValue
        Dim doctor As String = ddlDoctor.SelectedValue ' Get selected doctor's text
        Dim slotNo As Integer = Integer.Parse(ddlSlot.SelectedValue)

        ' Insert into Oracle database
        Dim connectionString As String = "User Id=SYS;Password=system;Data Source=Aniket10;DBA Privilege=SYSDBA" ' Replace with your Oracle connection string
        Using con As New OracleConnection(connectionString)

            Dim query As String = "INSERT INTO Appointments (AppointmentID,AppointmentDate, DependentName, Specialty, Doctor, SlotNo) " &
                                  "VALUES (:getmaxappointmentID+1,:AppointmentDate, :DependentName, :Specialty, :Doctor, :SlotNo)"

            Using cmd As New OracleCommand(query, con)
                cmd.Parameters.Add(":AppointmentID", OracleDbType.Int32).Value = GetMaxAppointmentID()
                cmd.Parameters.Add(":AppointmentDate", OracleDbType.Date).Value = appointmentDate
                cmd.Parameters.Add(":DependentName", OracleDbType.Varchar2).Value = dependentName
                cmd.Parameters.Add(":Specialty", OracleDbType.Varchar2).Value = specialty
                cmd.Parameters.Add(":Doctor", OracleDbType.Varchar2).Value = doctor
                cmd.Parameters.Add(":SlotNo", OracleDbType.Int32).Value = slotNo

                con.Open()
                cmd.ExecuteNonQuery()
            End Using
        End Using

        ' Optionally, display a message to the user
        lblMessage.Text = "Appointment booked successfully!"
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

End Class
