Imports System.Windows.Forms

Public Class FormMenu
    Inherits Form

    ' Controles del formulario
    Private WithEvents btnAgregarAlumno As Button
    Private WithEvents btnRefrescar As Button
    Private WithEvents dgvAlumnos As DataGridView

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub InitializeComponent()
        ' Propiedades del formulario
        Me.Text = "Panel de Gestión de Alumnos"
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.ClientSize = New Drawing.Size(900, 600)

        ' Botón Agregar Alumno
        btnAgregarAlumno = New Button()
        btnAgregarAlumno.Name = "btnAgregarAlumno"
        btnAgregarAlumno.Text = "Agregar Alumno"
        btnAgregarAlumno.Size = New Drawing.Size(120, 30)
        btnAgregarAlumno.Location = New Drawing.Point(10, 10)
        btnAgregarAlumno.Anchor = AnchorStyles.Top Or AnchorStyles.Left

        ' Botón Refrescar Lista
        btnRefrescar = New Button()
        btnRefrescar.Name = "btnRefrescar"
        btnRefrescar.Text = "Refrescar Lista"
        btnRefrescar.Size = New Drawing.Size(120, 30)
        btnRefrescar.Location = New Drawing.Point(140, 10)
        btnRefrescar.Anchor = AnchorStyles.Top Or AnchorStyles.Left

        ' DataGridView Alumnos
        dgvAlumnos = New DataGridView()
        dgvAlumnos.Name = "dgvAlumnos"
        dgvAlumnos.Location = New Drawing.Point(10, 50)
        dgvAlumnos.Size = New Drawing.Size(880, 540)
        dgvAlumnos.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        dgvAlumnos.AllowUserToAddRows = False
        dgvAlumnos.AllowUserToDeleteRows = False
        dgvAlumnos.ReadOnly = False
        dgvAlumnos.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvAlumnos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

        ' Agregar controles al formulario
        Me.Controls.Add(btnAgregarAlumno)
        Me.Controls.Add(btnRefrescar)
        Me.Controls.Add(dgvAlumnos)
    End Sub

    ' ----1. CUANDO EL FORMULARIO SE CARGA ----
    Private Sub FormMenu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Le damos un estilo más limpio al grid
        dgvAlumnos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvAlumnos.AllowUserToAddRows = False ' Ocultamos la fila vacía de abajo
        dgvAlumnos.ReadOnly = True ' Hacemos que no se pueda escribir en las celdas

        ' Preparamos las columnas y cargamos los datos
        ConfigurarGrid()

        ' --- Asistencia automática al abrir: si un alumno no tiene clases registradas, marcamos1 presente ---
        Dim anyMarked As Boolean = False
        For Each alumno As Alumno In DatosGlobales.ListaAlumnos
            If alumno.Presentes + alumno.Ausentes = 0 Then
                alumno.Presentes += 1
                anyMarked = True
            End If
        Next
        If anyMarked Then
            MessageBox.Show("Asistencia inicial: todos los alumnos sin registro fueron marcados como presentes.", "Asistencia automática")
        End If

        CargarDatosAlGrid()
    End Sub

    ' ----2. SUB-RUTINA PARA DEFINIR LAS COLUMNAS ----
    Private Sub ConfigurarGrid()
        dgvAlumnos.Columns.Clear()

        ' Columnas de Datos (las que leemos de la clase Alumno)
        dgvAlumnos.Columns.Add("Nombre", "Nombre del Alumno")
        dgvAlumnos.Columns.Add("Asistencia", "Asistencia (%)")
        dgvAlumnos.Columns.Add("Promedio", "Promedio")
        dgvAlumnos.Columns("Nombre").Width = 250 ' Hacemos la columna nombre más ancha

        ' === Columnas de Botones (Como en tu imagen) ===
        ' Botón para Cargar Notas
        Dim colNotas As New DataGridViewButtonColumn()
        colNotas.Name = "btnNotas"
        colNotas.Text = "Cargar Notas"
        colNotas.UseColumnTextForButtonValue = True ' Para que muestre "Cargar Notas" en cada fila
        dgvAlumnos.Columns.Add(colNotas)

        ' Botón para Asistencia (Presente)
        Dim colPresente As New DataGridViewButtonColumn()
        colPresente.Name = "btnPresente"
        colPresente.Text = "Marcar Presente"
        colPresente.UseColumnTextForButtonValue = True
        dgvAlumnos.Columns.Add(colPresente)

        ' Botón para Asistencia (Ausente)
        Dim colAusente As New DataGridViewButtonColumn()
        colAusente.Name = "btnAusente"
        colAusente.Text = "Marcar Ausente"
        colAusente.UseColumnTextForButtonValue = True
        dgvAlumnos.Columns.Add(colAusente)
    End Sub

    ' ----3. SUB-RUTINA PARA CARGAR/RECARGAR LOS ALUMNOS ----
    Private Sub CargarDatosAlGrid()
        dgvAlumnos.Rows.Clear() ' Limpiamos la tabla

        ' Leemos la lista GLOBAL de alumnos
        For Each alumno As Alumno In DatosGlobales.ListaAlumnos
            ' Usamos la función que creamos en la Clase Alumno
            Dim porcentaje As Double = alumno.PorcentajeAsistencia()
            Dim promedio As Double = alumno.CalcularPromedio()

            ' Agregamos la fila a la tabla
            ' Nota: No agregamos los botones aquí, solo los datos
            dgvAlumnos.Rows.Add(alumno.NombreCompleto, porcentaje.ToString("N2") & " %", promedio.ToString("N2"))
        Next
    End Sub

    ' ----4. MANEJAR LOS CLICS EN LOS BOTONES DEL GRID ----
    Private Sub dgvAlumnos_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvAlumnos.CellClick
        ' Si hacen clic en la cabecera (e.RowIndex = -1), no hacemos nada
        If e.RowIndex < 0 Then Return

        '1. Obtenemos el nombre del alumno de la fila en la que se hizo clic
        Dim nombreAlumno As String = dgvAlumnos.Rows(e.RowIndex).Cells("Nombre").Value.ToString()

        '2. Buscamos ese alumno en nuestra lista GLOBAL
        Dim alumnoSeleccionado As Alumno = DatosGlobales.ListaAlumnos.Find(Function(a) a.NombreCompleto = nombreAlumno)
        If alumnoSeleccionado Is Nothing Then Return

        '3. Vemos en qué COLUMNA (botón) se hizo clic
        Dim nombreColumna As String = dgvAlumnos.Columns(e.ColumnIndex).Name

        ' === Decidimos qué hacer ===
        Select Case nombreColumna
            Case "btnNotas"
                ' ABRE EL FORMULARIO DE CARGA DE NOTAS
                ' (FormCarga ahora recibe el alumno en su constructor)
                Dim formCarga As New FormCarga(alumnoSeleccionado)
                formCarga.ShowDialog()

            Case "btnPresente"
                ' CARGA ASISTENCIA (Presente)
                alumnoSeleccionado.Presentes += 1
                MessageBox.Show($"Se marcó 'Presente' a {alumnoSeleccionado.NombreCompleto}", "Asistencia")
                CargarDatosAlGrid() ' Recargamos la tabla para ver el cambio

            Case "btnAusente"
                ' CARGA ASISTENCIA (Ausente)
                alumnoSeleccionado.Ausentes += 1
                MessageBox.Show($"Se marcó 'Ausente' a {alumnoSeleccionado.NombreCompleto}", "Asistencia")
                CargarDatosAlGrid() ' Recargamos la tabla para ver el cambio
        End Select
    End Sub

    ' ----5. Botón de "Refrescar" ----
    Private Sub btnRefrescar_Click(sender As Object, e As EventArgs) Handles btnRefrescar.Click
        CargarDatosAlGrid()
    End Sub

    ' ----6. Botón Agregar Alumno ----
    Private Sub btnAgregarAlumno_Click(sender As Object, e As EventArgs) Handles btnAgregarAlumno.Click
        ' Abrimos el formulario que acabamos de crear
        Dim formAgregar As New FormAgregarAlumno()
        formAgregar.ShowDialog()

        ' Cuando se cierra, refrescamos la lista para ver al nuevo alumno
        CargarDatosAlGrid()
    End Sub
End Class