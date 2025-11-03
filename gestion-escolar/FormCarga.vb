' Archivo: FormCarga.vb
Imports System
Imports System.Windows.Forms
Imports System.ComponentModel

' (Asegúrate de que esta es la ÚNICA línea "Partial Public Class FormCarga")
Partial Public Class FormCarga
    Inherits Form

    Private components As IContainer
    Private lblNombreAlumno As Label
    Private lblNota1 As Label
    Private txtNota1 As TextBox
    Private lblNota2 As Label
    Private txtNota2 As TextBox
    ' --- Controles para Nota 3 ---
    Private lblNota3 As Label
    Private txtNota3 As TextBox
    ' --- Fin Controles Nota 3 ---
    Private WithEvents btnGuardarNotas As Button

    ' Variable local para guardar el alumno que recibimos
    Private AlumnoActual As Alumno

    ' ----1. EL "CONSTRUCTOR" ----
    ' Recibimos el alumno desde el Dashboard (FormMenu)
    Public Sub New(alumno As Alumno)
        ' Esto es obligatorio, no lo toques
        InitializeComponent()

        ' Guardamos el alumno en nuestra variable local
        Me.AlumnoActual = alumno
    End Sub

    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    ' --- ESTE ES EL MÉTODO DE DISEÑO ACTUALIZADO ---
    Private Sub InitializeComponent()
        components = New Container()
        lblNombreAlumno = New Label()
        lblNota1 = New Label()
        txtNota1 = New TextBox()
        lblNota2 = New Label()
        txtNota2 = New TextBox()
        btnGuardarNotas = New Button()
        ' --- Instanciar controles ---
        lblNota3 = New Label()
        txtNota3 = New TextBox()

        SuspendLayout()
        '
        ' lblNombreAlumno
        '
        lblNombreAlumno.AutoSize = True
        lblNombreAlumno.Location = New Drawing.Point(12, 15)
        lblNombreAlumno.Name = "lblNombreAlumno"
        lblNombreAlumno.Size = New Drawing.Size(200, 20)
        lblNombreAlumno.TabIndex = 0
        lblNombreAlumno.Text = "Cargando notas para: ..."
        '
        ' lblNota1
        '
        lblNota1.AutoSize = True
        lblNota1.Location = New Drawing.Point(12, 55)
        lblNota1.Name = "lblNota1"
        lblNota1.Size = New Drawing.Size(53, 20)
        lblNota1.TabIndex = 1
        lblNota1.Text = "Nota1:"
        '
        ' txtNota1
        '
        txtNota1.Location = New Drawing.Point(80, 52)
        txtNota1.Name = "txtNota1"
        txtNota1.Size = New Drawing.Size(100, 27)
        txtNota1.TabIndex = 2
        '
        ' lblNota2
        '
        lblNota2.AutoSize = True
        lblNota2.Location = New Drawing.Point(12, 95)
        lblNota2.Name = "lblNota2"
        lblNota2.Size = New Drawing.Size(53, 20)
        lblNota2.TabIndex = 3
        lblNota2.Text = "Nota2:"
        '
        ' txtNota2
        '
        txtNota2.Location = New Drawing.Point(80, 92)
        txtNota2.Name = "txtNota2"
        txtNota2.Size = New Drawing.Size(100, 27)
        txtNota2.TabIndex = 4
        '
        ' lblNota3
        '
        lblNota3.AutoSize = True
        lblNota3.Location = New Drawing.Point(12, 135)
        lblNota3.Name = "lblNota3"
        lblNota3.Size = New Drawing.Size(53, 20)
        lblNota3.TabIndex = 5
        lblNota3.Text = "Nota3:"
        '
        ' txtNota3
        '
        txtNota3.Location = New Drawing.Point(80, 132)
        txtNota3.Name = "txtNota3"
        txtNota3.Size = New Drawing.Size(100, 27)
        txtNota3.TabIndex = 6
        '
        ' btnGuardarNotas
        '
        btnGuardarNotas.Location = New Drawing.Point(80, 180)
        btnGuardarNotas.Name = "btnGuardarNotas"
        btnGuardarNotas.Size = New Drawing.Size(120, 30)
        btnGuardarNotas.TabIndex = 7
        btnGuardarNotas.Text = "Guardar Notas"
        btnGuardarNotas.UseVisualStyleBackColor = True
        '
        ' FormCarga
        '
        ClientSize = New Drawing.Size(320, 240)
        Controls.Add(lblNombreAlumno)
        Controls.Add(lblNota1)
        Controls.Add(txtNota1)
        Controls.Add(lblNota2)
        Controls.Add(txtNota2)
        Controls.Add(lblNota3)
        Controls.Add(txtNota3)
        Controls.Add(btnGuardarNotas)
        FormBorderStyle = FormBorderStyle.FixedDialog
        MaximizeBox = False
        MinimizeBox = False
        Name = "FormCarga"
        StartPosition = FormStartPosition.CenterParent
        Text = "Cargar Notas"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    ' ----2. CUANDO EL FORMULARIO SE CARGA ----
    Private Sub FormCarga_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Verificamos que el alumno exista
        If AlumnoActual IsNot Nothing Then
            ' Mostramos el nombre en el Label
            lblNombreAlumno.Text = $"Cargando notas para: {AlumnoActual.NombreCompleto}"

            ' Rellenamos los TextBox con las notas que ya tiene
            txtNota1.Text = AlumnoActual.Nota1.ToString()
            txtNota2.Text = AlumnoActual.Nota2.ToString()
            txtNota3.Text = AlumnoActual.Nota3.ToString()
        End If
    End Sub

    ' ----3. BOTÓN GUARDAR ----
    Private Sub btnGuardarNotas_Click(sender As Object, e As EventArgs) Handles btnGuardarNotas.Click
        If AlumnoActual IsNot Nothing Then
            Try
                ' Actualizamos el OBJETO en la lista GLOBAL
                AlumnoActual.Nota1 = Convert.ToDouble(txtNota1.Text)
                AlumnoActual.Nota2 = Convert.ToDouble(txtNota2.Text)
                AlumnoActual.Nota3 = Convert.ToDouble(txtNota3.Text)

                ' Guardamos cambios a disco
                DatosGlobales.SaveToFile()

                MessageBox.Show("Notas guardadas correctamente.")
                Me.Close() ' Cerramos el formulario
            Catch ex As Exception
                MessageBox.Show("Ingrese valores numéricos válidos para las notas.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        Else
            MessageBox.Show("Error: No se seleccionó ningún alumno.")
        End If
    End Sub ' <-- ESTE 'END SUB' FALTABA

End Class ' <-- ESTE 'END CLASS' FALTABA
