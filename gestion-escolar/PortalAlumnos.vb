Imports System.Globalization
Imports System.IO
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports Newtonsoft.Json

Public Class PortalAlumnos
    Private db As RootDB
    Private currentMonth As Integer
    Private currentYear As Integer

    Private Sub PortalAlumnos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Inicializamos mes actual
        Dim now = DateTime.Now
        currentMonth = now.Month
        currentYear = now.Year

        ' Cargar DB
        Dim rutaJson As String = Path.Combine(Application.StartupPath, "db-alumnos.json")
        If Not File.Exists(rutaJson) Then
            MessageBox.Show("No se encontró db-alumnos.json en: " & rutaJson)
            Return
        End If

        Dim json As String = File.ReadAllText(rutaJson)
        Try
            db = JsonConvert.DeserializeObject(Of RootDB)(json)
        Catch ex As Exception
            MessageBox.Show("Error parseando JSON: " & ex.Message)
            Return
        End Try

        CargarComboAlumnos()
        lblLegend.Text = "Verde = Presente — Rojo = Ausente"
        UpdateMonthLabel()
        DibujaCalendario()
    End Sub

    Private Sub CargarComboAlumnos()
        cmbAlumnos.Items.Clear()
        If db Is Nothing OrElse db.alumnos Is Nothing Then Return

        For Each a In db.alumnos
            ' mostramos "Nombre Apellido" y guardamos el objeto en Tag
            cmbAlumnos.Items.Add(New ComboItem With {.Text = $"{a.nombre} {a.apellido}", .Value = a})
        Next

        If cmbAlumnos.Items.Count > 0 Then
            cmbAlumnos.SelectedIndex = 0
        End If
    End Sub

    Private Sub cmbAlumnos_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbAlumnos.SelectedIndexChanged
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

        If cmbAlumnos.SelectedItem Is Nothing Then Return
        Dim alumno As Alumno = CType(DirectCast(cmbAlumnos.SelectedItem, ComboItem).Value, Alumno)

        ' Construimos un diccionario de asistencias por fecha (solo del alumno seleccionado)
        Dim asistDict As New Dictionary(Of DateTime, Boolean)
        If alumno.asistencias IsNot Nothing Then
            For Each a In alumno.asistencias
                ' Usamos ParseExact para el formato YYYY-MM-DD
                Dim dt As DateTime
                If DateTime.TryParseExact(a.fecha, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, dt) Then
                    asistDict(dt.Date) = a.presente
                End If
            Next
        End If

        ' Cabeceras de semana (Lun, Mar, ...)
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

        ' Calcular offset para que el 1° caiga en el día correcto (consideramos Lunes como primer día)
        Dim firstOfMonth As New DateTime(currentYear, currentMonth, 1)
        ' DayOfWeek: Sunday=0, Monday=1 ...
        Dim dow As Integer = CInt(firstOfMonth.DayOfWeek)
        ' Convertir a 0-based index con Monday=0 ... Sunday=6
        Dim offset As Integer = If(dow = 0, 6, dow - 1)

        ' Agregamos celdas vacías para el offset
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

            ' Color según asistencia
            If asistDict.ContainsKey(dateThis) Then
                If asistDict(dateThis) Then
                    lblDay.BackColor = Color.LightGreen
                    toolTip1.SetToolTip(lblDay, $"Fecha: {dateThis:yyyy-MM-dd}{vbCrLf}Presente")
                Else
                    lblDay.BackColor = Color.LightCoral
                    toolTip1.SetToolTip(lblDay, $"Fecha: {dateThis:yyyy-MM-dd}{vbCrLf}Ausente")
                End If
            Else
                lblDay.BackColor = Color.LightGray
                toolTip1.SetToolTip(lblDay, $"Fecha: {dateThis:yyyy-MM-dd}{vbCrLf}Sin registro")
            End If

            ' Click para ver detalle (opcional)
            AddHandler lblDay.Click, Sub(s, ev)
                                         MessageBox.Show($"Alumno: {alumno.nombre} {alumno.apellido}" & vbCrLf &
                                                         $"Fecha: {dateThis:yyyy-MM-dd}" & vbCrLf &
                                                         $"Estado: " &
                                                         If(asistDict.ContainsKey(dateThis),
                                                            If(asistDict(dateThis), "Presente", "Ausente"),
                                                            "Sin registro"))
                                     End Sub

            flpCalendar.Controls.Add(lblDay)
        Next
    End Sub

    ' Clase auxiliar para poner objetos en ComboBox
    Private Class ComboItem
        Public Property Text As String
        Public Property Value As Object
        Public Overrides Function ToString() As String
            Return Text
        End Function
    End Class
End Class

' Clases que mappean el JSON
Public Class RootDB
    Public Property alumnos As List(Of Alumno)
End Class

Public Class Alumno
    Public Property id As Integer
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
