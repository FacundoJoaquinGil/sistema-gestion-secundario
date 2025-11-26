Imports System.IO
Imports System.Linq
Imports Newtonsoft.Json

Public Class NotasAlumno

    ' Propiedad pública que debes asignar antes de mostrar el form
    Public Property UsuarioActual As String
    Private currentAlumno As AlumnoModel

    ' Clases para mapear el JSON (se repiten aquí para que el form sea independiente)
    Public Class Materia
        Public Property idMateria As Integer
        Public Property nombreMateria As String
        Public Property notas As List(Of Double)
    End Class

    Public Class Asistencia
        Public Property fecha As String
        Public Property presente As Boolean
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

    Public Class RootDB
        Public Property alumnos As List(Of Alumno)
        Public Property profesores As List(Of Object)
    End Class

    Private data As RootDB
    Private alumnoActual As Alumno

    ' Evento Load del form
    Private Sub NotasAlumno_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Quitar color de selección en celdas
        dgvNotas.DefaultCellStyle.SelectionBackColor = dgvNotas.DefaultCellStyle.BackColor
        dgvNotas.DefaultCellStyle.SelectionForeColor = dgvNotas.DefaultCellStyle.ForeColor

        ' Quitar color de selección en los headers
        dgvNotas.ColumnHeadersDefaultCellStyle.SelectionBackColor = dgvNotas.ColumnHeadersDefaultCellStyle.BackColor
        dgvNotas.ColumnHeadersDefaultCellStyle.SelectionForeColor = dgvNotas.ColumnHeadersDefaultCellStyle.ForeColor

        ' Evitar uso de estilos visuales que fuerzan azul
        dgvNotas.EnableHeadersVisualStyles = False

        ' Opcional: evitar selección total de fila
        dgvNotas.SelectionMode = DataGridViewSelectionMode.CellSelect

        ' Verificar que UsuarioActual esté seteado
        If String.IsNullOrWhiteSpace(UsuarioActual) Then
            MessageBox.Show("No se recibió el usuario. Asegúrate de asignar la propiedad UsuarioActual antes de mostrar el formulario.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Me.Close()
            Return
        End If

        Dim rutaJson As String = Path.Combine(Application.StartupPath, "db-alumnos.json")
        If Not File.Exists(rutaJson) Then
            MessageBox.Show("No se encontró db-alumnos.json en: " & rutaJson, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
            Return
        End If

        Try
            Dim json As String = File.ReadAllText(rutaJson)
            data = JsonConvert.DeserializeObject(Of RootDB)(json)
        Catch ex As Exception
            MessageBox.Show("Error al leer/parsear db-alumnos.json: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
            Return
        End Try

        ' Buscar alumno por usuario (insensible a mayúsculas)
        alumnoActual = data.alumnos?.FirstOrDefault(Function(a) Not String.IsNullOrEmpty(a.usuario) AndAlso a.usuario.Equals(UsuarioActual, StringComparison.OrdinalIgnoreCase))

        If alumnoActual Is Nothing Then
            MessageBox.Show("No se encontró al alumno con usuario: " & UsuarioActual, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Me.Close()
            Return
        End If

        ' Mostrar nombre del alumno en el form (Label)
        lblNombreAlumno.Text = $"Notas de {alumnoActual.nombre} {alumnoActual.apellido}"

        cbMaterias.DropDownStyle = ComboBoxStyle.DropDownList   ' 🔒 evita la edición

        If alumnoActual.materias IsNot Nothing AndAlso alumnoActual.materias.Count > 0 Then
            cbMaterias.DataSource = alumnoActual.materias
            cbMaterias.DisplayMember = "nombreMateria"
            cbMaterias.ValueMember = "idMateria"
            cbMaterias.SelectedIndex = 0
        Else
            cbMaterias.DataSource = Nothing
            lblPromedio.Text = "Sin materias"
            dgvNotas.Rows.Clear()
        End If
    End Sub

    Private Sub cbMaterias_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbMaterias.SelectedIndexChanged
        If cbMaterias.SelectedItem Is Nothing OrElse alumnoActual Is Nothing Then
            Return
        End If

        Dim materiaSeleccionada As Materia = CType(cbMaterias.SelectedItem, Materia)

        dgvNotas.Rows.Clear()

        If materiaSeleccionada.notas Is Nothing OrElse materiaSeleccionada.notas.Count = 0 Then
            lblPromedio.Text = "Sin notas"
            lblPromedio.ForeColor = Color.Black
            Return
        End If

        ' Preparar los 3 valores (si faltan, quedan vacíos)
        Dim valor1 As String = String.Empty
        Dim valor2 As String = String.Empty
        Dim valor3 As String = String.Empty

        If materiaSeleccionada.notas.Count > 0 Then
            valor1 = materiaSeleccionada.notas(0).ToString("F2")
        End If
        If materiaSeleccionada.notas.Count > 1 Then
            valor2 = materiaSeleccionada.notas(1).ToString("F2")
        End If
        If materiaSeleccionada.notas.Count > 2 Then
            valor3 = materiaSeleccionada.notas(2).ToString("F2")
        End If

        ' Agregar una sola fila con los 3 trimestres
        dgvNotas.Rows.Add(valor1, valor2, valor3)

        ' Calcular promedio (siempre que haya al menos 1 nota)
        If materiaSeleccionada.notas Is Nothing OrElse Not materiaSeleccionada.notas.Any() Then
            lblPromedio.ForeColor = Color.Black
            lblPromedio.Text = "No hay notas para calcular el promedio."
            Return
        End If

        Dim promedio As Double = materiaSeleccionada.notas.Average()

        If promedio >= 6 Then
            lblPromedio.ForeColor = Color.LightGreen
            lblPromedio.Text = $"Promedio: {promedio.ToString("F2")} Aprobado"
        Else
            lblPromedio.ForeColor = Color.LightCoral
            lblPromedio.Text = $"Promedio: {promedio.ToString("F2")} Desaprobado"
        End If


        Try
            For colIndex As Integer = 0 To 2
                Dim cellValue = dgvNotas.Rows(0).Cells(colIndex).Value?.ToString()
                'Dim notaDbl As Double
                'If Not String.IsNullOrEmpty(cellValue) AndAlso Double.TryParse(cellValue, notaDbl) Then
                '    If notaDbl >= 6 Then
                '        dgvNotas.Rows(0).Cells(colIndex).Style.ForeColor = Color.Green
                '    Else
                '        dgvNotas.Rows(0).Cells(colIndex).Style.ForeColor = Color.Red
                '    End If
                'Else
                '    ' Vacío: color por defecto
                '    dgvNotas.Rows(0).Cells(colIndex).Style.ForeColor = Color.Black
                'End If
                dgvNotas.Rows(0).Cells(colIndex).Style.ForeColor = Color.Black
            Next
        Catch ex As Exception
            ' Si algo falla con estilo, no interrumpe la app
        End Try

    End Sub


    ' Botón Cerrar
    Private Sub btnCerrar_Click(sender As Object, e As EventArgs) Handles btnCerrar.Click
        Dim volverPortal As New PortalAlumnos()
        volverPortal.UsuarioActual = UsuarioActual
        volverPortal.Show()
        Me.Hide()
    End Sub

    Private Sub NotasAlumno_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Application.Exit()
    End Sub
End Class
