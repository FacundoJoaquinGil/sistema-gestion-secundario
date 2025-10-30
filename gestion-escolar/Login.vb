Public Class Login


    Private usuarios As (Tipo As String, Usuario As String, Password As String)() = {
        ("alumno", "alumno1", "1234"),
        ("alumno", "alumno2", "abcd"),
        ("profesor", "profesor1", "admin123"),
        ("profesor", "profesor2", "clave555")
    }

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim user As String = TextBox1.Text
        Dim pass As String = TextBox2.Text

        Dim PortalAlumnos As New PortalAlumnos()

        Dim encontrado = usuarios.FirstOrDefault(Function(u) u.Usuario = user AndAlso u.Password = pass)

        If encontrado.Usuario IsNot Nothing Then

            If encontrado.Tipo = "alumno" Then
                PortalAlumnos.Show()
                MessageBox.Show($"Bienvenido {user}")
                Me.Hide()

            ElseIf encontrado.Tipo = "profesor" Then
                MessageBox.Show("Bienvenido profesor")

            End If
        Else
            MessageBox.Show("Usuario o contraseña incorrectos")
        End If

    End Sub

End Class

