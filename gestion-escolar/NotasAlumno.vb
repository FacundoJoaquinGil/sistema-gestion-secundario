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
        lblNombreAlumno.Text = $"{alumnoActual.nombre} {alumnoActual.apellido}"

        ' Cargar materias en ComboBox
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

    ' Cuando cambie la materia seleccionada
    Private Sub cbMaterias_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbMaterias.SelectedIndexChanged
        If cbMaterias.SelectedItem Is Nothing OrElse alumnoActual Is Nothing Then
            Return
        End If

        Dim materiaSeleccionada As Materia = CType(cbMaterias.SelectedItem, Materia)

        ' Limpiar DataGridView
        dgvNotas.Rows.Clear()

        If materiaSeleccionada.notas Is Nothing OrElse materiaSeleccionada.notas.Count = 0 Then
            lblPromedio.Text = "Sin notas"
            Return
        End If

        ' Rellenar DataGridView con las notas (Nro, Nota)
        Dim i As Integer = 1
        For Each n As Double In materiaSeleccionada.notas
            dgvNotas.Rows.Add(i, n.ToString("F2"))
            i += 1
        Next

        ' Calcular promedio (seguro y con control)
        Dim promedio As Double = 0
        Try
            promedio = materiaSeleccionada.notas.Average()
            lblPromedio.Text = "Promedio: " & Math.Round(promedio, 2).ToString("F2")
        Catch ex As Exception
            lblPromedio.Text = "Error calculando promedio"
        End Try

    End Sub

    ' Botón Cerrar
    Private Sub btnCerrar_Click(sender As Object, e As EventArgs) Handles btnCerrar.Click
        Me.Close()
    End Sub

End Class
