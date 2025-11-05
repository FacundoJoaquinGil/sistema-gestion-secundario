Imports System.IO
Imports System.Linq
Imports Newtonsoft.Json

Public Class Login

    ' Clases para mapear el JSON
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

    Public Class Profesor
        Public Property nombre As String
        Public Property apellido As String
        Public Property tipo As String
        Public Property usuario As String
        Public Property password As String
        Public Property materia As String
    End Class

    Public Class RootDB
        Public Property alumnos As List(Of Alumno)
        Public Property profesores As List(Of Profesor)
    End Class

    ' Variable para los datos cargados desde el JSON
    Private data As RootDB

    ' ---- Evento Load: cargamos JSON ----
    Private Sub Login_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim rutaJson As String = Path.Combine(Application.StartupPath, "db-alumnos.json")

        If Not File.Exists(rutaJson) Then
            MessageBox.Show("No se encontró db-alumnos.json en: " & rutaJson, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Try
            Dim json As String = File.ReadAllText(rutaJson)
            data = JsonConvert.DeserializeObject(Of RootDB)(json)
        Catch ex As Exception
            MessageBox.Show("Error al leer/parsear db-alumnos.json: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            data = Nothing
        End Try
    End Sub

    ' ---- Botón Login ----
    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        Dim user As String = TextBox1.Text.Trim()
        Dim pass As String = txtPassword.Text.Trim()

        If data Is Nothing Then
            MessageBox.Show("No se pudieron cargar los datos. Verifica el archivo JSON.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        ' Validaciones básicas
        If String.IsNullOrWhiteSpace(user) OrElse String.IsNullOrWhiteSpace(pass) Then
            MessageBox.Show("Por favor ingresa usuario y contraseña.", "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Buscar en alumnos (usuario no sensible a mayúsculas)
        Dim alumno = data.alumnos?.FirstOrDefault(Function(a) _
            Not String.IsNullOrEmpty(a.usuario) AndAlso
            a.usuario.Equals(user, StringComparison.OrdinalIgnoreCase) AndAlso
            a.password = pass)

        If alumno IsNot Nothing Then
            ' Login exitoso como alumno
            MessageBox.Show($"Bienvenido {alumno.nombre} {alumno.apellido}", "Ingreso correcto", MessageBoxButtons.OK, MessageBoxIcon.Information)

            Dim portal As New PortalAlumnos()
            portal.UsuarioActual = alumno.usuario ' pasar el identificador que usa PortalAlumnos
            portal.Show()
            Me.Hide()
            Return
        End If


        Dim profesor = data.profesores?.FirstOrDefault(Function(p) _
            Not String.IsNullOrEmpty(p.usuario) AndAlso
            p.usuario.Equals(user, StringComparison.OrdinalIgnoreCase) AndAlso
            p.password = pass)

        If profesor IsNot Nothing Then

            MessageBox.Show($"Bienvenido profesor {profesor.nombre} {profesor.apellido} - Materia: {If(String.IsNullOrEmpty(profesor.materia), "(sin materia)", profesor.materia)}", "Ingreso correcto", MessageBoxButtons.OK, MessageBoxIcon.Information)

            Dim portalProf As New FormMenu()

            'portalProf.UsuarioActual = profesor.usuario //por si quiero mostrar el nombre del profe

            portalProf.Show()
            Me.Hide()

            Return
        End If

        MessageBox.Show("Usuario o contraseña incorrectos", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    End Sub

End Class
