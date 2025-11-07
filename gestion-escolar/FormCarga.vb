' Archivo: FormCarga.vb
Imports System
Imports System.Windows.Forms
Imports System.ComponentModel
Imports Newtonsoft.Json.Linq

Partial Public Class FormCarga
    Inherits Form

    ' Variable local para guardar el alumno que recibimos
    Private AlumnoActual As Alumno
    Private AlumnoUsuario As String ' opcional, para identificar en JSON
    Private MateriaNombre As String = String.Empty ' opcional: materia a editar cuando viene desde JSON

    ' Constructor: recibe el alumno desde otro formulario (modelo Alumno)
    Public Sub New(alumno As Alumno)
        InitializeComponent() ' <-- esto llama a la versión del Designer
        Me.AlumnoActual = alumno
        Me.AlumnoUsuario = String.Empty
    End Sub

    ' Nuevo constructor: recibe un JObject (alumno desde db-alumnos.json)
    Public Sub New(alumnoJ As JObject, Optional materia As String = Nothing)
        InitializeComponent()
        Dim nombreFull As String = $"{alumnoJ("nombre")?.ToString()} {alumnoJ("apellido")?.ToString()}".Trim()
        Me.AlumnoActual = New Alumno(nombreFull)
        Me.AlumnoUsuario = alumnoJ("usuario")?.ToString()
        Me.MateriaNombre = If(String.IsNullOrWhiteSpace(materia), String.Empty, materia)

        ' Cargar notas desde la materia indicada o primera materia
        Dim materiasArr As JArray = TryCast(alumnoJ("materias"), JArray)
        Dim materiaMatch As JObject = Nothing
        If materiasArr IsNot Nothing AndAlso materiasArr.Count > 0 Then
            If Not String.IsNullOrWhiteSpace(materia) Then
                For Each jm As JObject In materiasArr
                    If String.Equals(jm("nombreMateria")?.ToString(), materia, StringComparison.OrdinalIgnoreCase) Then
                        materiaMatch = jm
                        Exit For
                    End If
                Next
            End If
            If materiaMatch Is Nothing Then materiaMatch = TryCast(materiasArr(0), JObject)
            If materiaMatch IsNot Nothing Then
                Dim notasArr As JArray = TryCast(materiaMatch("notas"), JArray)
                If notasArr IsNot Nothing Then
                    If notasArr.Count > 0 Then Me.AlumnoActual.Nota1 = CDbl(notasArr(0).ToString())
                    If notasArr.Count > 1 Then Me.AlumnoActual.Nota2 = CDbl(notasArr(1).ToString())
                    If notasArr.Count > 2 Then Me.AlumnoActual.Nota3 = CDbl(notasArr(2).ToString())
                End If
            End If
        End If
    End Sub

    ' Cuando el formulario se carga
    Private Sub FormCarga_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If AlumnoActual IsNot Nothing Then
            lblNombreAlumno.Text = $"Cargando notas para: {AlumnoActual.NombreCompleto}"
            txtNota1.Text = AlumnoActual.Nota1.ToString()
            txtNota2.Text = AlumnoActual.Nota2.ToString()
            txtNota3.Text = AlumnoActual.Nota3.ToString()
        End If
    End Sub

    ' Botón Guardar
    Private Sub btnGuardarNotas_Click(sender As Object, e As EventArgs) Handles btnGuardarNotas.Click
        If AlumnoActual IsNot Nothing Then
            Try
                AlumnoActual.Nota1 = Convert.ToDouble(txtNota1.Text)
                AlumnoActual.Nota2 = Convert.ToDouble(txtNota2.Text)
                AlumnoActual.Nota3 = Convert.ToDouble(txtNota3.Text)

                ' Si tenemos usuario (viene de JSON), actualizamos usando DataStore
                If Not String.IsNullOrWhiteSpace(AlumnoUsuario) Then
                    Dim notas() As Double = {AlumnoActual.Nota1, AlumnoActual.Nota2, AlumnoActual.Nota3}
                    DataStore.UpdateNotas(AlumnoUsuario, notas, MateriaNombre)
                Else
                    ' Si no viene de JSON, intentamos encontrar por nombre completo en JSON y actualizar ahí
                    Try
                        Dim all As JArray = DataStore.GetAlumnosJArray()
                        Dim encontrado As JObject = Nothing
                        For Each a As JObject In all
                            Dim nombreFull As String = $"{a("nombre")?.ToString()} {a("apellido")?.ToString()}".Trim()
                            If String.Equals(nombreFull, AlumnoActual.NombreCompleto, StringComparison.OrdinalIgnoreCase) Then
                                encontrado = a
                                Exit For
                            End If
                        Next
                        If encontrado IsNot Nothing Then
                            ' Usar DataStore.UpdateNotas con el usuario encontrado
                            Dim usuario As String = encontrado("usuario")?.ToString()
                            If Not String.IsNullOrWhiteSpace(usuario) Then
                                Dim notasArr() As Double = {AlumnoActual.Nota1, AlumnoActual.Nota2, AlumnoActual.Nota3}
                                DataStore.UpdateNotas(usuario, notasArr)
                            End If
                        End If
                    Catch ex As Exception
                        ' Ignorar error de persistencia
                    End Try
                End If

                MessageBox.Show("Notas guardadas correctamente.")
                Me.Close()
            Catch ex As Exception
                MessageBox.Show("Ingrese valores numéricos válidos para las notas.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        Else
            MessageBox.Show("Error: No se seleccionó ningún alumno.")
        End If
    End Sub

End Class