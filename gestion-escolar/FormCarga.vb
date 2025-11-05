' Archivo: FormCarga.vb
Imports System
Imports System.Windows.Forms
Imports System.ComponentModel

Partial Public Class FormCarga
    Inherits Form

    ' Variable local para guardar el alumno que recibimos
    Private AlumnoActual As Alumno

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

                DatosGlobales.SaveToFile()

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