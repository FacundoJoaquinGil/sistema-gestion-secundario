Imports System.Globalization
Imports System.IO
Imports Newtonsoft.Json

Public Class PortalAlumnos

    ' Propiedad pública para recibir el usuario logueado desde Login
    Public Property UsuarioActual As String

    Private db As RootDB
    Private currentMonth As Integer
    Private currentYear As Integer
    Private currentAlumno As Alumno ' Alumno actualmente mostrado

    Private Sub PortalAlumnos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Inicializamos mes actual
        Dim now = DateTime.Now
        currentMonth = now.Month
        currentYear = now.Year

        ' Cargar DB
        Dim rutaJson As String = Path.Combine(Application.StartupPath, "db-alumnos.json")
        If Not File.Exists(rutaJson) Then
            MessageBox.Show("No se encontró db-alumnos.json en: " & rutaJson, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
            Return
        End If

        Dim json As String = File.ReadAllText(rutaJson)
        Try
            db = JsonConvert.DeserializeObject(Of RootDB)(json)
        Catch ex As Exception
            MessageBox.Show("Error parseando JSON: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
            Return
        End Try

        ' Verificar que se pasó el usuario desde Login
        If String.IsNullOrWhiteSpace(UsuarioActual) Then
            MessageBox.Show("No se recibió usuario desde el Login.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
            Return
        End If

        If db Is Nothing OrElse db.alumnos Is Nothing OrElse db.alumnos.Count = 0 Then
            MessageBox.Show("No hay alumnos cargados en la base de datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
            Return
        End If

        ' Buscar por campo 'usuario' (case-insensitive)
        currentAlumno = db.alumnos.FirstOrDefault(Function(a) _
            Not String.IsNullOrEmpty(a.usuario) AndAlso
            a.usuario.Equals(UsuarioActual, StringComparison.OrdinalIgnoreCase))

        If currentAlumno Is Nothing Then
            MessageBox.Show($"No se encontró el alumno con usuario '{UsuarioActual}'.", "Usuario no encontrado", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Me.Close()
            Return
        End If

        ' Poner mensaje de bienvenida en lblUsuario (si existe)
        If Me.Controls.ContainsKey("lblUsuario") Then
            CType(Me.Controls("lblUsuario"), Label).Text = $"Bienvenido {currentAlumno.nombre} {currentAlumno.apellido}"
        End If

        ' Mostrar datos básicos del alumno en etiquetas (si existen)
        If Me.Controls.ContainsKey("lblNombre") Then
            CType(Me.Controls("lblNombre"), Label).Text = $"{currentAlumno.nombre} {currentAlumno.apellido}"
        End If
        If Me.Controls.ContainsKey("lblFechaNacimiento") Then
            CType(Me.Controls("lblFechaNacimiento"), Label).Text = currentAlumno.fechaNacimiento
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
        If currentAlumno.asistencias IsNot Nothing Then
            For Each a In currentAlumno.asistencias
                Dim dt As DateTime
                If DateTime.TryParseExact(a.fecha, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, dt) Then
                    asistDict(dt.Date) = a.presente
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
                                         MessageBox.Show($"Alumno: {currentAlumno.nombre} {currentAlumno.apellido}" & vbCrLf &
                                                         $"Fecha: {dateThis:yyyy-MM-dd}" & vbCrLf &
                                                         $"Estado: " &
                                                         If(asistDict.ContainsKey(dateThis),
                                                            If(asistDict(dateThis), "Presente", "Ausente"),
                                                            "Sin registro"))
                                     End Sub

            flpCalendar.Controls.Add(lblDay)
        Next
    End Sub

End Class

' Clases que mappean el JSON
Public Class RootDB
    Public Property alumnos As List(Of Alumno)
End Class

Public Class Alumno
    Public Property id As Integer
    Public Property tipo As String
    Public Property usuario As String
    Public Property password As String
    Public Property nombre As String
    Public Property apellido As String
    Public Property fechaNacimiento As String
    Public Property materias As List(Of Materia)
    Public Property asistencias As List(Of Asistencia)
End Class

Public Class Materia
    Public Property idMateria As Integer
    Public Property nombreMateria As String
    Public Property notas As List(Of Double)
End Class

Public Class Asistencia
    Public Property fecha As String
    Public Property presente As Boolean
End Class
