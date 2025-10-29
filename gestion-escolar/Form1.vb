Partial Class Form1
    ' Archivo: Form1.vb (dentro de la clase Form1)
    Public Sub New()
        ' Ensure designer InitializeComponent runs
        InitializeComponent()
        ' Wire events via AddHandler to avoid Handles/WithEvents compiler issues
        AddHandler Me.Load, AddressOf Form1_Load
        AddHandler btnLogin.Click, AddressOf btnLogin_Click
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs)
        ' Cargar datos persistentes desde disco (si existen)
        DatosGlobales.LoadFromFile()

        ' --- CREAMOS LOS DATOS FICTICIOS ---
        ' Solo los creamos si la lista está vacía (para que no se dupliquen)
        If DatosGlobales.ListaAlumnos.Count = 0 Then
            ' Usamos la "plantilla" (Clase) que creamos
            Dim alumno1 As New Alumno("Ana López")
            Dim alumno2 As New Alumno("Carlos Gómez")
            Dim alumno3 As New Alumno("María Torres")

            ' Los guardamos en nuestro "almacén" (Módulo)
            DatosGlobales.ListaAlumnos.Add(alumno1)
            DatosGlobales.ListaAlumnos.Add(alumno2)
            DatosGlobales.ListaAlumnos.Add(alumno3)

            ' Guardamos los datos iniciales a disco para persistencia
            DatosGlobales.SaveToFile()

            ' Opcional: Mostramos un mensaje para saber que funcionó
            MessageBox.Show("Datos ficticios de 3 alumnos cargados en memoria.")
        End If
    End Sub

    ' Este es el evento click de tu botón "INGRESAR"
    Private Sub btnLogin_Click(sender As Object, e As EventArgs)
        '1. Verificamos la contraseña. ¡Ahora sí, la correcta!
        If txtPassword.Text = "profe123" Then
            '2. Si es correcta, creamos el Formulario de Menú
            ' (Asegúrate de haber creado el archivo FormMenu.vb)
            Dim formMenu As New FormMenu()

            '3. Mostramos el menú
            formMenu.Show()

            '4. Ocultamos este formulario de Login
            Me.Hide()

        Else
            '5. Si la contraseña es incorrecta
            MessageBox.Show("Contraseña incorrecta. Intente de nuevo.", "Acceso Denegado", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtPassword.Clear() ' Limpiamos el campo de contraseña
            txtPassword.Focus() ' Dejamos el cursor listo para escribir de nuevo
        End If
    End Sub

End Class
