Imports System
Imports System.Windows.Forms
Imports System.ComponentModel
Imports Newtonsoft.Json.Linq

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

 ' Dividir en nombre y apellido
 Dim partes() As String = nuevoNombre.Split(New Char() {" "c}, StringSplitOptions.RemoveEmptyEntries)
 Dim nombre As String = partes(0)
 Dim apellido As String = If(partes.Length >1, String.Join(" ", partes,1, partes.Length -1), String.Empty)

 Try
 ' Cargar lista de alumnos desde JSON
 Dim all As JArray = DataStore.GetAlumnosJArray()
 ' Verificar duplicados por nombre+apellido
 For Each a As JObject In all
 If String.Equals(a("nombre")?.ToString()?.Trim(), nombre, StringComparison.OrdinalIgnoreCase) AndAlso _
 String.Equals(a("apellido")?.ToString()?.Trim(), apellido, StringComparison.OrdinalIgnoreCase) Then
 MessageBox.Show("Ya existe un alumno con ese nombre.")
 Return
 End If
 Next

 ' Calcular nuevo id
 Dim maxId As Integer =0
 For Each a As JObject In all
 Dim idToken = a("id")
 If idToken IsNot Nothing Then
 Dim idVal As Integer
 If Integer.TryParse(idToken.ToString(), idVal) Then
 If idVal > maxId Then maxId = idVal
 End If
 End If
 Next
 Dim nuevoId As Integer = maxId +1

 ' Generar usuario simple
 Dim usuarioBase As String = (nombre & apellido).ToLower().Replace(" ", "")
 Dim usuario As String = usuarioBase & nuevoId.ToString()

 ' Crear JObject nuevo alumno
 Dim nuevo As New JObject()
 nuevo("id") = nuevoId
 nuevo("tipo") = "alumno"
 nuevo("usuario") = usuario
 nuevo("password") = "1234"
 nuevo("nombre") = nombre
 nuevo("apellido") = apellido
 nuevo("fechaNacimiento") = Date.Now.ToString("yyyy-MM-dd")
 nuevo("materias") = New JArray()
 nuevo("asistencias") = New JArray()

 all.Add(nuevo)
 ' Guardar de vuelta
 Dim root As JObject = DataStore.LoadRootJObject()
 root("alumnos") = all
 DataStore.SaveRootJObject(root)

 MessageBox.Show("Alumno agregado con éxito. Usuario: " & usuario)
 Me.Close()
 Catch ex As Exception
 MessageBox.Show("Error al agregar alumno: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
 End Try
 End Sub
End Class
