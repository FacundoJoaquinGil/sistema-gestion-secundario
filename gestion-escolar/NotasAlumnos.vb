'Imports System.Globalization
'Imports System.Linq
'Imports System.Windows.Forms

'' DTOs simples (añadelos si no los tenés)
'Public Class Materia
'    Public Property idMateria As Integer
'    Public Property nombreMateria As String
'    Public Property notas As List(Of Decimal)
'End Class

'Public Class Alumno
'    Public Property idAlumno As Integer
'    Public Property nombre As String
'    Public Property materias As List(Of Materia)
'End Class

'Partial Public Class NotasAlumnos
'    Inherits Form

'    Private flpMaterias As FlowLayoutPanel
'    Private panelBottom As Panel
'    Private LblPromedioAsistencias As Label
'    Private BtnCerrar As Button

'    Private currentAlumno As Alumno

'    Public Sub New(alumno As Alumno)
'        InitializeComponent()
'        Me.currentAlumno = alumno
'        PopulateFromAlumno()
'    End Sub

'    Private Sub InitializeComponent()
'        Me.flpMaterias = New FlowLayoutPanel()
'        Me.panelBottom = New Panel()
'        Me.LblPromedioAsistencias = New Label()
'        Me.BtnCerrar = New Button()

'        ' Form
'        Me.Text = "Notas del alumno"
'        Me.Size = New Drawing.Size(760, 520)
'        Me.StartPosition = FormStartPosition.CenterParent

'        ' FlowLayoutPanel para materias
'        Me.flpMaterias.Name = "flpMaterias"
'        Me.flpMaterias.Dock = DockStyle.Fill
'        Me.flpMaterias.AutoScroll = True
'        Me.flpMaterias.FlowDirection = FlowDirection.TopDown
'        Me.flpMaterias.WrapContents = False
'        Me.flpMaterias.Padding = New Padding(10)

'        ' Panel bottom con promedio y cerrar
'        Me.panelBottom.Dock = DockStyle.Bottom
'        Me.panelBottom.Height = 60
'        Me.panelBottom.Padding = New Padding(10)

'        ' Label promedio general (usa el nombre que pediste)
'        Me.LblPromedioAsistencias.Name = "LblPromedioAsistencias"
'        Me.LblPromedioAsistencias.AutoSize = False
'        Me.LblPromedioAsistencias.TextAlign = Drawing.ContentAlignment.MiddleLeft
'        Me.LblPromedioAsistencias.Dock = DockStyle.Left
'        Me.LblPromedioAsistencias.Width = 520
'        Me.LblPromedioAsistencias.Font = New Drawing.Font("Segoe UI", 10, Drawing.FontStyle.Bold)

'        ' Botón cerrar
'        Me.BtnCerrar.Text = "Cerrar"
'        Me.BtnCerrar.Dock = DockStyle.Right
'        Me.BtnCerrar.Width = 100
'        AddHandler Me.BtnCerrar.Click, AddressOf BtnCerrar_Click

'        ' Añadir controles
'        Me.panelBottom.Controls.Add(Me.LblPromedioAsistencias)
'        Me.panelBottom.Controls.Add(Me.BtnCerrar)
'        Me.Controls.Add(Me.flpMaterias)
'        Me.Controls.Add(Me.panelBottom)
'    End Sub

'    Private Sub BtnCerrar_Click(sender As Object, e As EventArgs)
'        Me.Close()
'    End Sub

'    ' Poblado desde el objeto Alumno
'    Private Sub PopulateFromAlumno()
'        Me.flpMaterias.Controls.Clear()

'        If Me.currentAlumno Is Nothing OrElse Me.currentAlumno.materias Is Nothing OrElse Me.currentAlumno.materias.Count = 0 Then
'            Dim lblEmpty As New Label() With {
'                .AutoSize = False,
'                .Height = 30,
'                .TextAlign = Drawing.ContentAlignment.MiddleCenter,
'                .Text = "No hay materias para mostrar."
'            }
'            Me.flpMaterias.Controls.Add(lblEmpty)
'            Me.LblPromedioAsistencias.Text = "Promedio general: Sin notas"
'            Return
'        End If

'        ' Para promedio general: reunir todas las notas
'        Dim todasNotas As New List(Of Decimal)

'        For Each mat In Me.currentAlumno.materias
'            Dim gb As New GroupBox()
'            gb.Text = If(String.IsNullOrEmpty(mat.nombreMateria), $"Materia {mat.idMateria}", mat.nombreMateria)
'            gb.AutoSize = True
'            gb.Width = Me.flpMaterias.ClientSize.Width - 40
'            gb.Padding = New Padding(8)
'            gb.Margin = New Padding(6)

'            ' Panel interno para las notas (horizontal)
'            Dim pnlNotas As New FlowLayoutPanel()
'            pnlNotas.FlowDirection = FlowDirection.LeftToRight
'            pnlNotas.WrapContents = False
'            pnlNotas.AutoSize = True
'            pnlNotas.Height = 48
'            pnlNotas.Dock = DockStyle.Top
'            pnlNotas.Padding = New Padding(4)

'            If mat.notas IsNot Nothing AndAlso mat.notas.Count > 0 Then
'                For Each n As Decimal In mat.notas
'                    Dim lblNota As New Label()
'                    lblNota.AutoSize = False
'                    lblNota.Width = 60
'                    lblNota.Height = 32
'                    lblNota.TextAlign = Drawing.ContentAlignment.MiddleCenter
'                    lblNota.Margin = New Padding(4)
'                    lblNota.Font = New Drawing.Font("Segoe UI", 9, Drawing.FontStyle.Regular)

'                    ' Formateo a 1 decimal
'                    lblNota.Text = n.ToString("0.0", CultureInfo.InvariantCulture)

'                    ' Color según aprobado/desaprobado. Se considera aprobado >= 6
'                    If n >= 6D Then
'                        lblNota.BackColor = Drawing.Color.LightGreen
'                        lblNota.ForeColor = Drawing.Color.Black
'                    Else
'                        lblNota.BackColor = Drawing.Color.LightCoral
'                        lblNota.ForeColor = Drawing.Color.Black
'                    End If

'                    ' Borde y estética
'                    lblNota.BorderStyle = BorderStyle.FixedSingle

'                    pnlNotas.Controls.Add(lblNota)
'                Next

'                ' Promedio de la materia
'                Dim promedioMateria As Decimal = Math.Round(mat.notas.Average(), 1)
'                Dim lblPromMat As New Label()
'                lblPromMat.AutoSize = True
'                lblPromMat.Text = $"Promedio: {promedioMateria.ToString("0.0", CultureInfo.InvariantCulture)}"
'                lblPromMat.Font = New Drawing.Font("Segoe UI", 9, Drawing.FontStyle.Bold)
'                lblPromMat.Margin = New Padding(8, 6, 4, 6)
'                ' Color del texto del promedio según aprobado
'                lblPromMat.ForeColor = If(promedioMateria >= 6D, Drawing.Color.DarkGreen, Drawing.Color.DarkRed)

'                gb.Controls.Add(pnlNotas)
'                gb.Controls.Add(lblPromMat)

'                ' Añadir notas al acumulador general
'                todasNotas.AddRange(mat.notas)
'            Else
'                ' Si no tiene notas
'                Dim lblNoNotas As New Label()
'                lblNoNotas.AutoSize = True
'                lblNoNotas.Text = "Sin notas"
'                lblNoNotas.Font = New Drawing.Font("Segoe UI", 9, Drawing.FontStyle.Italic)
'                lblNoNotas.Margin = New Padding(4)
'                gb.Controls.Add(lblNoNotas)
'            End If

'            Me.flpMaterias.Controls.Add(gb)
'        Next

'        ' Promedio general
'        If todasNotas.Count > 0 Then
'            Dim promedioGeneral As Decimal = Math.Round(todasNotas.Average(), 1)
'            Me.LblPromedioAsistencias.Text = $"Promedio general: {promedioGeneral.ToString("0.0", CultureInfo.InvariantCulture)}"
'            Me.LblPromedioAsistencias.ForeColor = If(promedioGeneral >= 6D, Drawing.Color.DarkGreen, Drawing.Color.DarkRed)
'        Else
'            Me.LblPromedioAsistencias.Text = "Promedio general: Sin notas"
'            Me.LblPromedioAsistencias.ForeColor = Drawing.Color.Black
'        End If
'    End Sub

'    ' Método público para actualizar si se cambia el alumno en runtime
'    Public Sub UpdateAlumno(alumno As Alumno)
'        Me.currentAlumno = alumno
'        PopulateFromAlumno()
'    End Sub

'End Class
