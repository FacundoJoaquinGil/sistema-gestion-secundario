Imports System.Globalization
Imports System.IO
Imports Newtonsoft.Json
Imports System.Linq


Public Class PortalAlumnos

    ' Propiedad pública para recibir el usuario logueado desde Login
    Public Property UsuarioActual As String
    Private db As RootDB
    Private currentMonth As Integer
    Private currentYear As Integer
    Private currentAlumno As AlumnoModel ' Alumno actualmente mostrado (renombrado)

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
        ' Creamos una fecha para el primer día del mes actual seleccionado
        Dim dt As New DateTime(currentYear, currentMonth, 1)

        ' Obtenemos el nombre del mes en español: "marzo 2025"
        Dim monthText As String = dt.ToString("MMMM yyyy", CultureInfo.CreateSpecificCulture("es-ES"))

        ' Capitalizar la primera letra: "Marzo 2025"
        If Not String.IsNullOrEmpty(monthText) Then
            monthText = Char.ToUpper(monthText(0)) & monthText.Substring(1)
        End If

        ' Asignar al label LblMesActual si existe
        If Me.Controls.ContainsKey("LblMesActual") Then
            CType(Me.Controls("LblMesActual"), Label).Text = monthText
        Else
            ' Si no existe, opcional: fallback a LblPromedioAsistencias para evitar que la info se pierda
            If Me.Controls.ContainsKey("LblPromedioAsistencias") Then
                CType(Me.Controls("LblPromedioAsistencias"), Label).Text = monthText
            End If
        End If
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
                ' Si hay registro explícito en el JSON
                If asistDict(dateThis) Then
                    lblDay.BackColor = Color.LightGreen
                    ToolTip1.SetToolTip(lblDay, $"Fecha: {dateThis:yyyy-MM-dd}{vbCrLf}Presente")
                Else
                    lblDay.BackColor = Color.LightCoral
                    ToolTip1.SetToolTip(lblDay, $"Fecha: {dateThis:yyyy-MM-dd}{vbCrLf}Ausente")
                End If
            Else
                ' No hay registro en JSON
                If dateThis.Date < DateTime.Today Then
                    ' Fecha pasada: solo marcar como ausente si es día de semana (Lun-Vie)
                    If dateThis.DayOfWeek <> DayOfWeek.Saturday AndAlso dateThis.DayOfWeek <> DayOfWeek.Sunday Then
                        ' Marcamos como ausente automáticamente (opcional: podés evitar escribir en asistDict si no querés modificarlo)
                        asistDict(dateThis) = False
                        lblDay.BackColor = Color.LightCoral
                        ToolTip1.SetToolTip(lblDay, $"Fecha: {dateThis:yyyy-MM-dd}{vbCrLf}Ausente (registrado automáticamente)")
                    Else
                        ' Fin de semana pasado: no se contabiliza como ausencia
                        lblDay.BackColor = Color.WhiteSmoke
                        ToolTip1.SetToolTip(lblDay, $"Fecha: {dateThis:yyyy-MM-dd}{vbCrLf}Fin de semana — no contabilizado")
                    End If
                Else
                    ' Fecha futura o hoy (sin registro) => sin registro
                    lblDay.BackColor = Color.LightGray
                    ToolTip1.SetToolTip(lblDay, $"Fecha: {dateThis:yyyy-MM-dd}{vbCrLf}Sin registro")
                End If
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

        ' ---- al final de DibujaCalendario() ----
        ' ---- Mostrar promedio mensual de asistencias ----
        If Me.Controls.ContainsKey("LblPromedioAsistencias") Then
            Dim promedio As Double = CalcularPromedioMensualAsistencias()
            Dim lblProm As Label = CType(Me.Controls("LblPromedioAsistencias"), Label)
            lblProm.Text = $"Promedio de asistencias: {promedio.ToString("F2")}%"
        End If
    End Sub


    ' Calcula el promedio de asistencias para un mes/año dado (por defecto usa currentMonth/currentYear)
    Private Function CalcularPromedioMensualAsistencias() As Double
        If currentAlumno Is Nothing OrElse currentAlumno.asistencias Is Nothing Then
            Return 0
        End If

        Dim asistDict As New Dictionary(Of DateTime, Boolean)
        For Each a In currentAlumno.asistencias
            Dim dt As DateTime
            If DateTime.TryParseExact(a.fecha, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, dt) Then
                asistDict(dt.Date) = a.presente
            End If
        Next

        Dim diasEnMes As Integer = DateTime.DaysInMonth(currentYear, currentMonth)
        Dim diasPresentes As Integer = 0
        Dim totalDias As Integer = 0

        For d As Integer = 1 To diasEnMes
            Dim fecha As New DateTime(currentYear, currentMonth, d)

            ' Solo cuenta días hábiles (lunes a viernes)
            If fecha.DayOfWeek = DayOfWeek.Saturday OrElse fecha.DayOfWeek = DayOfWeek.Sunday Then
                Continue For
            End If

            ' Si es el mes actual, no contar días futuros
            If currentYear = DateTime.Today.Year AndAlso currentMonth = DateTime.Today.Month AndAlso fecha > DateTime.Today Then
                Continue For
            End If

            totalDias += 1

            ' Si la fecha existe en asistencias
            If asistDict.ContainsKey(fecha) Then
                If asistDict(fecha) = True Then
                    diasPresentes += 1
                End If
            Else
                ' Si no hay registro y la fecha ya pasó, se toma como ausente
                If fecha.Date < DateTime.Today Then
                    ' nada, ya se considera ausente
                End If
            End If
        Next

        If totalDias = 0 Then Return 0

        Dim promedio As Double = (diasPresentes / totalDias) * 100
        Return Math.Round(promedio, 2)
    End Function



    ' Calcula el promedio de asistencias (porcentaje de días presentes)
    Private Function CalcularPromedioAsistencias() As Double
        ' Verificamos si hay un alumno cargado
        If currentAlumno Is Nothing OrElse currentAlumno.asistencias Is Nothing OrElse currentAlumno.asistencias.Count = 0 Then
            Return 0
        End If

        ' Contamos las asistencias
        Dim totalDias As Integer = currentAlumno.asistencias.Count
        Dim diasPresentes As Integer = currentAlumno.asistencias.Where(Function(a) a.presente).Count()

        ' Evitar división por cero
        If totalDias = 0 Then Return 0

        ' Promedio (porcentaje)
        Dim promedio As Double = (diasPresentes / totalDias) * 100

        Return Math.Round(promedio, 2)
    End Function


    Private Sub BtnVolver_Click(sender As Object, e As EventArgs) Handles BtnVolver.Click

        Dim volverLogin As New Login()
        volverLogin.Show()
        Me.Hide()

    End Sub

    Private Sub BtnNotas_Click(sender As Object, e As EventArgs) Handles BtnNotas.Click
        'Dim frmNotas As New NotasAlumnos(currentAlumno)
        'frmNotas.ShowDialog()
        Me.Hide()

    End Sub

End Class

' Clases que mappean el JSON
Public Class RootDB
    Public Property alumnos As List(Of AlumnoModel)
End Class

Public Class AlumnoModel
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
