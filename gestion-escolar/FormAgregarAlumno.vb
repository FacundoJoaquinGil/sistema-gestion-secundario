Imports System
Imports System.Windows.Forms
Imports System.ComponentModel

Public Class FormAgregarAlumno
 Inherits Form

 Private components As IContainer
 Private lblNombre As Label
 Private txtNombreNuevo As TextBox
 Private WithEvents btnGuardarNuevo As Button

 Public Sub New()
 InitializeComponent()
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

 Private Sub InitializeComponent()
 components = New Container()
 lblNombre = New Label()
 txtNombreNuevo = New TextBox()
 btnGuardarNuevo = New Button()

 SuspendLayout()
 '
 ' lblNombre
 '
 lblNombre.AutoSize = True
 lblNombre.Location = New Drawing.Point(12,15)
 lblNombre.Name = "lblNombre"
 lblNombre.Size = New Drawing.Size(110,20)
 lblNombre.TabIndex =0
 lblNombre.Text = "Nombre Completo:"
 '
 ' txtNombreNuevo
 '
 txtNombreNuevo.Location = New Drawing.Point(130,12)
 txtNombreNuevo.Name = "txtNombreNuevo"
 txtNombreNuevo.Size = New Drawing.Size(220,27)
 txtNombreNuevo.TabIndex =1
 '
 ' btnGuardarNuevo
 '
 btnGuardarNuevo.Location = New Drawing.Point(130,50)
 btnGuardarNuevo.Name = "btnGuardarNuevo"
 btnGuardarNuevo.Size = New Drawing.Size(120,30)
 btnGuardarNuevo.TabIndex =2
 btnGuardarNuevo.Text = "Guardar"
 btnGuardarNuevo.UseVisualStyleBackColor = True
 '
 ' FormAgregarAlumno
 '
 ClientSize = New Drawing.Size(380,100)
 Controls.Add(lblNombre)
 Controls.Add(txtNombreNuevo)
 Controls.Add(btnGuardarNuevo)
 FormBorderStyle = FormBorderStyle.FixedDialog
 MaximizeBox = False
 MinimizeBox = False
 Name = "FormAgregarAlumno"
 StartPosition = FormStartPosition.CenterParent
 Text = "Agregar Alumno"
 ResumeLayout(False)
 PerformLayout()
 End Sub

 Private Sub btnGuardarNuevo_Click(sender As Object, e As EventArgs) Handles btnGuardarNuevo.Click
 If String.IsNullOrWhiteSpace(txtNombreNuevo.Text) Then
 MessageBox.Show("Debe ingresar un nombre.")
 Return
 End If

 Dim nuevoNombre As String = txtNombreNuevo.Text.Trim()
 ' Validación: evitar nombres duplicados (case-insensitive)
 If DatosGlobales.ListaAlumnos.Exists(Function(a) a.NombreCompleto.Trim().ToLower() = nuevoNombre.ToLower()) Then
 MessageBox.Show("Ya existe un alumno con ese nombre.")
 Return
 End If

 '1. Creamos el nuevo alumno
 Dim nuevoAlumno As New Alumno(nuevoNombre)

 '2. Lo agregamos a la lista GLOBAL
 DatosGlobales.ListaAlumnos.Add(nuevoAlumno)

 '3. Guardamos en disco
 DatosGlobales.SaveToFile()

 MessageBox.Show("Alumno agregado con éxito.")
 Me.Close() ' Cerramos esta ventana
 End Sub
End Class
