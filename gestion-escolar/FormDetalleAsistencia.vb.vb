' Archivo: FormDetalleAsistencia.vb
Imports System.Windows.Forms

Public Class FormDetalleAsistencia
    Inherits Form

    ' Controles
    Private dgvDetalle As DataGridView
    Private lblNombre As Label

    ' Variable local
    Private AlumnoActual As Alumno

    Public Sub New(alumno As Alumno)
        InitializeComponent()
        Me.AlumnoActual = alumno
    End Sub

    Private Sub InitializeComponent()
        ' Propiedades del formulario
        Me.Text = "Detalle de Asistencia"
        Me.StartPosition = FormStartPosition.CenterParent
        Me.ClientSize = New Drawing.Size(400, 500)
        Me.FormBorderStyle = FormBorderStyle.FixedDialog
        Me.MaximizeBox = False

        ' Label para el nombre
        lblNombre = New Label()
        lblNombre.Name = "lblNombre"
        lblNombre.AutoSize = True
        lblNombre.Font = New Drawing.Font("Arial", 12, Drawing.FontStyle.Bold)
        lblNombre.Location = New Drawing.Point(10, 10)
        lblNombre.Text = "Alumno: ..."

        ' DataGridView (Tabla)
        dgvDetalle = New DataGridView()
        dgvDetalle.Name = "dgvDetalle"
        dgvDetalle.Location = New Drawing.Point(10, 40)
        dgvDetalle.Size = New Drawing.Size(380, 450)
        dgvDetalle.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        dgvDetalle.AllowUserToAddRows = False
        dgvDetalle.ReadOnly = True
        dgvDetalle.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

        ' Agregar controles
        Me.Controls.Add(lblNombre)
        Me.Controls.Add(dgvDetalle)
    End Sub

    Private Sub FormDetalleAsistencia_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lblNombre.Text = "Alumno: " & AlumnoActual.NombreCompleto

        ' Configurar columnas del Grid
        dgvDetalle.Columns.Clear()
        dgvDetalle.Columns.Add("Fecha", "Fecha")
        dgvDetalle.Columns.Add("Estado", "Estado")

        ' Formatear la columna de fecha
        dgvDetalle.Columns("Fecha").DefaultCellStyle.Format = "dddd, dd/MM/yyyy"
        dgvDetalle.Columns("Fecha").Width = 200

        ' Cargar datos
        For Each registro As AsistenciaRegistro In AlumnoActual.RegistrosAsistencia
            ' Ordenamos por fecha más reciente primero (opcional pero útil)
            dgvDetalle.Rows.Insert(0, registro.Fecha, registro.Estado.ToString())
        Next
    End Sub
End Class