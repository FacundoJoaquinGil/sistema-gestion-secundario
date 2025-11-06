Imports System.Globalization
Imports System.IO
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Class PortalAlumnos

    ' Propiedad pública para recibir el usuario logueado desde Login
    Public Property UsuarioActual As String

    Private currentMonth As Integer
    Private currentYear As Integer
    Private currentAlumno As JObject ' ahora trabajamos con JObject obtenido desde DataStore

    Private Sub PortalAlumnos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Inicializamos mes actual
        Dim now = DateTime.Now
        currentMonth = now.Month
        currentYear = now.Year

        ' Verificar que se pasó el usuario desde Login
        If String.IsNullOrWhiteSpace(UsuarioActual) Then
            MessageBox.Show("No se recibió usuario desde el Login.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
            Return
        End If

        ' Obtenemos alumno desde DataStore
        Try
            Dim alumnoJ As JObject = DataStore.GetAlumnoByUsuario(UsuarioActual)
            If alumnoJ Is Nothing Then
                MessageBox.Show($"No se encontró el alumno con usuario '{UsuarioActual}'.", "Usuario no encontrado", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Me.Close()
                Return
            End If
            currentAlumno = alumnoJ
        Catch ex As Exception
            MessageBox.Show("Error al leer db-alumnos.json: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
            Return
        End Try

        ' Poner mensaje de bienvenida en lblUsuario (si existe)
        If Me.Controls.ContainsKey("lblUsuario") Then
            CType(Me.Controls("lblUsuario"), Label).Text = $"Bienvenido {currentAlumno("nombre")?.ToString()} {currentAlumno("apellido")?.ToString()}"
        End If

        ' Mostrar datos básicos del alumno en etiquetas (si existen)
        If Me.Controls.ContainsKey("lblNombre") Then
            CType(Me.Controls("lblNombre"), Label).Text = $"{currentAlumno("nombre")?.ToString()} {currentAlumno("apellido")?.ToString()}"
        End If
        If Me.Controls.ContainsKey("lblFechaNacimiento") Then
            CType(Me.Controls("lblFechaNacimiento"), Label).Text = currentAlumno("fechaNacimiento")?.ToString()
        End If

        lblLegend.Text = "Verde = Presente — Rojo = Ausente"
        UpdateMonthLabel()
        DibujaCalendario()
    End Sub

    Private Sub btnPrev_Click(sender As Object, e As EventArgs) Handles btnPrev.Click
        currentMonth -= 1
        If currentMonth < 1 Then
            currentMonth = 12
            currentYear -= 1
        End If
        UpdateMonthLabel()
        DibujaCalendario()
    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        currentMonth += 1
        If currentMonth > 12 Then
            currentMonth = 1
            currentYear += 1
        End If
        UpdateMonthLabel()
        DibujaCalendario()
    End Sub

    Private Sub UpdateMonthLabel()
        Dim dt As New DateTime(currentYear, currentMonth, 1)
        lblMonthYear.Text = dt.ToString("MMMM yyyy", CultureInfo.CreateSpecificCulture("es-ES")).ToUpperInvariant()
    End Sub

    Private Sub DibujaCalendario()
        flpCalendar.Controls.Clear()

        If currentAlumno Is Nothing Then
            Dim lbl As New Label With {
                .Text = "No hay alumno seleccionado.",
                .AutoSize = False,
                .Size = New Size(300, 40),
                .TextAlign = ContentAlignment.MiddleCenter
            }
            flpCalendar.Controls.Add(lbl)
            Return
        End If

        Dim asistDict As New Dictionary(Of DateTime, Boolean)
        Dim asistenciasArr As JArray = TryCast(currentAlumno("asistencias"), JArray)
        If asistenciasArr IsNot Nothing Then
            For Each a As JObject In asistenciasArr
                Dim dt As DateTime
                If DateTime.TryParseExact(a("fecha")?.ToString(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, dt) Then
                    asistDict(dt.Date) = If(a("presente") IsNot Nothing AndAlso CBool(a("presente")), True, False)
                End If
            Next
        End If

        Dim diasSemana As String() = {"Lun", "Mar", "Mié", "Jue", "Vie", "Sáb", "Dom"}
        For Each d In diasSemana
            Dim lbl As New Label With {
                .Text = d,
                .TextAlign = ContentAlignment.MiddleCenter,
                .AutoSize = False,
                .Size = New Size(60, 20),
                .Font = New Font("Segoe UI", 8, FontStyle.Bold)
            }
            flpCalendar.Controls.Add(lbl)
        Next

        Dim firstOfMonth As New DateTime(currentYear, currentMonth, 1)
        Dim dow As Integer = CInt(firstOfMonth.DayOfWeek)
        Dim offset As Integer = If(dow = 0, 6, dow - 1)

        For i As Integer = 1 To offset
            Dim emptyLbl As New Label With {
                .Text = "",
                .AutoSize = False,
                .Size = New Size(60, 60),
                .BorderStyle = BorderStyle.FixedSingle,
                .BackColor = Color.WhiteSmoke
            }
            flpCalendar.Controls.Add(emptyLbl)
        Next

        Dim daysInMonth As Integer = DateTime.DaysInMonth(currentYear, currentMonth)
        For day As Integer = 1 To daysInMonth
            Dim dateThis As New DateTime(currentYear, currentMonth, day)

            Dim lblDay As New Label With {
                .Text = day.ToString(),
                .AutoSize = False,
                .Size = New Size(60, 60),
                .TextAlign = ContentAlignment.MiddleCenter,
                .BorderStyle = BorderStyle.FixedSingle,
                .Font = New Font("Segoe UI", 9, FontStyle.Regular),
                .Margin = New Padding(2)
            }

            If asistDict.ContainsKey(dateThis) Then
                If asistDict(dateThis) Then
                    lblDay.BackColor = Color.LightGreen
                    ToolTip1.SetToolTip(lblDay, $"Fecha: {dateThis:yyyy-MM-dd}{vbCrLf}Presente")
                Else
                    lblDay.BackColor = Color.LightCoral
                    ToolTip1.SetToolTip(lblDay, $"Fecha: {dateThis:yyyy-MM-dd}{vbCrLf}Ausente")
                End If
            Else
                lblDay.BackColor = Color.LightGray
                ToolTip1.SetToolTip(lblDay, $"Fecha: {dateThis:yyyy-MM-dd}{vbCrLf}Sin registro")
            End If

            AddHandler lblDay.Click, Sub(s, ev)
                                         MessageBox.Show($"Alumno: {currentAlumno("nombre")?.ToString()} {currentAlumno("apellido")?.ToString()}" & vbCrLf &
                                                         $"Fecha: {dateThis:yyyy-MM-dd}" & vbCrLf &
                                                         $"Estado: " &
                                                         If(asistDict.ContainsKey(dateThis),
                                                            If(asistDict(dateThis), "Presente", "Ausente"),
                                                            "Sin registro"))
                                     End Sub

            flpCalendar.Controls.Add(lblDay)
        Next
    End Sub

    Private Sub BtnVolver_Click(sender As Object, e As EventArgs) Handles BtnVolver.Click

        Dim volverLogin As New Login()
        volverLogin.Show()
        Me.Hide()

    End Sub

End Class
