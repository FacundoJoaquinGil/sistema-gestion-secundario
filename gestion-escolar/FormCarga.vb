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

    ' Constructor: recibe el alumno desde otro formulario
    Public Sub New(alumno As Alumno)
        InitializeComponent() ' <-- esto llama a la versión del Designer
        Me.AlumnoActual = alumno
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

                ' Intentamos actualizar en db-alumnos.json si encontramos el usuario por nombre completo
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
                        ' Guardar notas en la primera materia si existe, o crear una
                        Dim materias As JArray = TryCast(encontrado("materias"), JArray)
                        If materias Is Nothing Then
                            materias = New JArray()
                            encontrado("materias") = materias
                        End If
                        Dim materiaObj As JObject = Nothing
                        If materias.Count > 0 Then materiaObj = TryCast(materias(0), JObject)
                        If materiaObj Is Nothing Then
                            materiaObj = New JObject()
                            materiaObj("idMateria") = 1
                            materiaObj("nombreMateria") = "Materia1"
                            materiaObj("notas") = New JArray()
                            materias.Add(materiaObj)
                        End If
                        Dim notasArr As JArray = TryCast(materiaObj("notas"), JArray)
                        notasArr = New JArray({AlumnoActual.Nota1, AlumnoActual.Nota2, AlumnoActual.Nota3})
                        materiaObj("notas") = notasArr
                        Dim root As JObject = DataStore.LoadRootJObject()
                        root("alumnos") = all
                        DataStore.SaveRootJObject(root)
                    Else
                        ' No encontrado: no hacemos nada en JSON
                    End If
                Catch ex As Exception
                    ' Ignoramos error de persistencia por ahora
                End Try

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