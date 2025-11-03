' Archivo: FormMenu.vb
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
        ' (Este código de diseño no cambia)
        Me.Text = "Panel de Gestión de Alumnos"
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.ClientSize = New Drawing.Size(900, 600)
        btnAgregarAlumno = New Button()
        btnAgregarAlumno.Name = "btnAgregarAlumno"
        btnAgregarAlumno.Text = "Agregar Alumno"
        btnAgregarAlumno.Size = New Drawing.Size(120, 30)
        btnAgregarAlumno.Location = New Drawing.Point(10, 10)
        btnAgregarAlumno.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        btnRefrescar = New Button()
        btnRefrescar.Name = "btnRefrescar"
        btnRefrescar.Text = "Refrescar Lista"
        btnRefrescar.Size = New Drawing.Size(120, 30)
        btnRefrescar.Location = New Drawing.Point(140, 10)
        btnRefrescar.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        dgvAlumnos = New DataGridView()
        dgvAlumnos.Name = "dgvAlumnos"
        dgvAlumnos.Location = New Drawing.Point(10, 50)
        dgvAlumnos.Size = New Drawing.Size(880, 540)
        dgvAlumnos.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        dgvAlumnos.AllowUserToAddRows = False
        dgvAlumnos.AllowUserToDeleteRows = False
        dgvAlumnos.ReadOnly = True ' Cambiado a True
        dgvAlumnos.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvAlumnos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        Me.Controls.Add(btnAgregarAlumno)
        Me.Controls.Add(btnRefrescar)
        Me.Controls.Add(dgvAlumnos)
    End Sub

    ' ----1. CUANDO EL FORMULARIO SE CARGA ----
    Private Sub FormMenu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Le damos un estilo más limpio al grid
        dgvAlumnos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvAlumnos.AllowUserToAddRows = False
        dgvAlumnos.ReadOnly = True

        ConfigurarGrid()
        CargarDatosAlGrid()
    End Sub

    ' ----2. SUB-RUTINA PARA DEFINIR LAS COLUMNAS ----
    ' --- AJUSTADA ---
    Private Sub ConfigurarGrid()
        dgvAlumnos.Columns.Clear()

        ' Columnas de Datos
        dgvAlumnos.Columns.Add("Nombre", "Nombre del Alumno")
        dgvAlumnos.Columns.Add("Asistencia", "Asistencia (%)")
        dgvAlumnos.Columns.Add("Promedio", "Promedio")
        dgvAlumnos.Columns("Nombre").Width = 200

        ' === Columnas de Botones (Como en tu imagen) ===
        ' --- NUEVO BOTÓN: Ver Detalle ---
        Dim colDetalle As New DataGridViewButtonColumn()
        colDetalle.Name = "btnDetalle"
        colDetalle.Text = "Ver Detalle"
        colDetalle.UseColumnTextForButtonValue = True
        dgvAlumnos.Columns.Add(colDetalle)

        ' Botón para Cargar Notas
        Dim colNotas As New DataGridViewButtonColumn()
        colNotas.Name = "btnNotas"
        colNotas.Text = "Cargar Notas"
        colNotas.UseColumnTextForButtonValue = True
        dgvAlumnos.Columns.Add(colNotas)

        ' Botón para Asistencia (Presente)
        Dim colPresente As New DataGridViewButtonColumn()
        colPresente.Name = "btnPresente"
        colPresente.Text = "Marcar Presente (Hoy)"
        colPresente.UseColumnTextForButtonValue = True
        dgvAlumnos.Columns.Add(colPresente)

        ' Botón para Asistencia (Ausente)
        Dim colAusente As New DataGridViewButtonColumn()
        colAusente.Name = "btnAusente"
        colAusente.Text = "Marcar Ausente (Hoy)"
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
            dgvAlumnos.Rows.Add(alumno.NombreCompleto, porcentaje.ToString("N2") & " %", promedio.ToString("N2"))
        Next
    End Sub

    ' ----4. MANEJAR LOS CLICS EN LOS BOTONES DEL GRID ----
    ' --- LÓGICA COMPLETAMENTE NUEVA ---
    Private Sub dgvAlumnos_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvAlumnos.CellClick
        If e.RowIndex < 0 Then Return ' Si hacen clic en la cabecera, no hacemos nada

        ' 1. Obtenemos el nombre del alumno de la fila
        Dim nombreAlumno As String = dgvAlumnos.Rows(e.RowIndex).Cells("Nombre").Value.ToString()

        ' 2. Buscamos a ese alumno en nuestra lista GLOBAL
        Dim alumnoSeleccionado As Alumno = DatosGlobales.ListaAlumnos.Find(Function(a) a.NombreCompleto = nombreAlumno)
        If alumnoSeleccionado Is Nothing Then Return

        ' 3. Vemos en qué COLUMNA (botón) se hizo clic
        Dim nombreColumna As String = dgvAlumnos.Columns(e.ColumnIndex).Name

        ' === Decidimos qué hacer ===
        Select Case nombreColumna
            Case "btnDetalle"
                ' ABRE EL NUEVO FORMULARIO DE DETALLE
                ' (Este es el archivo que OMITIMOS)
                Dim formDetalle As New FormDetalleAsistencia(alumnoSeleccionado)
                formDetalle.ShowDialog()

            Case "btnNotas"
                ' ABRE EL FORMULARIO DE CARGA DE NOTAS
                Dim formCarga As New FormCarga(alumnoSeleccionado)
                formCarga.ShowDialog()
                CargarDatosAlGrid() ' Recargamos por si cambió el promedio

            Case "btnPresente"
                ' CARGA ASISTENCIA (Presente)
                MarcarAsistencia(alumnoSeleccionado, EstadoAsistencia.Presente)

            Case "btnAusente"
                ' CARGA ASISTENCIA (Ausente)
                MarcarAsistencia(alumnoSeleccionado, EstadoAsistencia.Ausente)
        End Select
    End Sub

    ' --- NUEVA FUNCIÓN: Lógica para marcar asistencia HOY ---
    Private Sub MarcarAsistencia(alumno As Alumno, estado As EstadoAsistencia)
        Dim hoy As Date = Date.Now.Date ' Obtiene la fecha de hoy, sin la hora

        ' 1. Busca si ya existe un registro para HOY
        Dim registroHoy As AsistenciaRegistro = alumno.RegistrosAsistencia.Find(Function(r) r.Fecha.Date = hoy)

        If registroHoy IsNot Nothing Then
            ' 2. Si existe, preguntamos si quiere actualizar
            Dim msg As String = $"Ya se marcó '{registroHoy.Estado.ToString()}' para {alumno.NombreCompleto} el día de hoy. ¿Desea cambiarlo a '{estado.ToString()}'?"
            If MessageBox.Show(msg, "Actualizar Asistencia", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                registroHoy.Estado = estado
                MessageBox.Show("Asistencia actualizada.")
            Else
                Return ' El usuario canceló
            End If
        Else
            ' 3. Si no existe, creamos un registro nuevo
            Dim nuevoRegistro As New AsistenciaRegistro(hoy, estado)
            alumno.RegistrosAsistencia.Add(nuevoRegistro)
            MessageBox.Show($"Se marcó '{estado.ToString()}' a {alumno.NombreCompleto}", "Asistencia")
        End If

        ' 4. Refrescamos la tabla y guardamos en el JSON
        CargarDatosAlGrid()
        DatosGlobales.SaveToFile()
    End Sub

    ' ----5. Botón de "Refrescar" ----
    Private Sub btnRefrescar_Click(sender As Object, e As EventArgs) Handles btnRefrescar.Click
        CargarDatosAlGrid()
    End Sub

    ' ----6. Botón Agregar Alumno ----
    Private Sub btnAgregarAlumno_Click(sender As Object, e As EventArgs) Handles btnAgregarAlumno.Click
        ' (Este código no cambia)
        Dim formAgregar As New FormAgregarAlumno()
        formAgregar.ShowDialog()
        ' Cuando se cierra, refrescamos la lista y guardamos
        CargarDatosAlGrid()
        DatosGlobales.SaveToFile()
    End Sub
End Class