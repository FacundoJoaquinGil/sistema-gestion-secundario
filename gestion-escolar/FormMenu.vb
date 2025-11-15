Imports System.Windows.Forms
Imports System.IO
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Class FormMenu
    Inherits Form


    Private WithEvents btnAgregarAlumno As Button
    Private WithEvents btnRefrescar As Button
    Friend WithEvents BtnVolver2 As Button
    Private WithEvents dgvAlumnos As DataGridView

    ' Nueva propiedad: materia del profesor (si se abre por profesor)
    Public Property MateriaActual As String = String.Empty

    ' Lista interna de alumnos cargados desde db-alumnos.json (JObjects)
    Private AlumnosJson As New List(Of JObject)()

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormMenu))
        btnAgregarAlumno = New Button()
        btnRefrescar = New Button()
        dgvAlumnos = New DataGridView()
        BtnVolver2 = New Button()
        CType(dgvAlumnos, System.ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' btnAgregarAlumno
        ' 
        btnAgregarAlumno.BackColor = Color.MediumOrchid
        btnAgregarAlumno.Cursor = Cursors.Hand
        btnAgregarAlumno.FlatStyle = FlatStyle.Flat
        btnAgregarAlumno.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnAgregarAlumno.ForeColor = Color.White
        btnAgregarAlumno.Location = New Point(10, 10)
        btnAgregarAlumno.Name = "btnAgregarAlumno"
        btnAgregarAlumno.Size = New Size(120, 30)
        btnAgregarAlumno.TabIndex = 0
        btnAgregarAlumno.Text = "Agregar Alumno"
        btnAgregarAlumno.UseVisualStyleBackColor = False
        ' 
        ' btnRefrescar
        ' 
        btnRefrescar.BackColor = Color.MediumOrchid
        btnRefrescar.Cursor = Cursors.Hand
        btnRefrescar.FlatStyle = FlatStyle.Flat
        btnRefrescar.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnRefrescar.ForeColor = Color.White
        btnRefrescar.Location = New Point(140, 10)
        btnRefrescar.Name = "btnRefrescar"
        btnRefrescar.Size = New Size(120, 30)
        btnRefrescar.TabIndex = 1
        btnRefrescar.Text = "Refrescar Lista"
        btnRefrescar.UseVisualStyleBackColor = False
        ' 
        ' dgvAlumnos
        ' 
        dgvAlumnos.AllowUserToAddRows = False
        dgvAlumnos.AllowUserToDeleteRows = False
        dgvAlumnos.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        dgvAlumnos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvAlumnos.BackgroundColor = Color.Azure
        dgvAlumnos.GridColor = Color.White
        dgvAlumnos.Location = New Point(10, 50)
        dgvAlumnos.Name = "dgvAlumnos"
        dgvAlumnos.ReadOnly = True
        dgvAlumnos.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvAlumnos.Size = New Size(762, 453)
        dgvAlumnos.TabIndex = 2
        ' 
        ' BtnVolver2
        ' 
        BtnVolver2.BackColor = Color.MediumOrchid
        BtnVolver2.Cursor = Cursors.Hand
        BtnVolver2.FlatStyle = FlatStyle.Flat
        BtnVolver2.Font = New Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        BtnVolver2.ForeColor = Color.White
        BtnVolver2.Location = New Point(664, 514)
        BtnVolver2.Name = "BtnVolver2"
        BtnVolver2.Size = New Size(108, 35)
        BtnVolver2.TabIndex = 14
        BtnVolver2.Text = "VOLVER"
        BtnVolver2.UseVisualStyleBackColor = False
        ' 
        ' FormMenu
        ' 
        BackgroundImage = My.Resources.Resources.background
        ClientSize = New Size(784, 561)
        Controls.Add(BtnVolver2)
        Controls.Add(btnAgregarAlumno)
        Controls.Add(btnRefrescar)
        Controls.Add(dgvAlumnos)
        ForeColor = SystemColors.ActiveCaption
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Name = "FormMenu"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Panel de Gestión de Alumnos"
        CType(dgvAlumnos, System.ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
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
        AlumnosJson.Clear()

        Try
            Dim jAlumnos As JArray = DataStore.GetAlumnosByMateria(MateriaActual)
            For Each ja As JObject In jAlumnos
                AlumnosJson.Add(ja)

                ' Nombre
                Dim nombreFull As String = $"{ja("nombre")?.ToString()} {ja("apellido")?.ToString()}"

                ' Promedio: tomar la primera materia o la materia actual
                Dim promedio As Double = 0
                Dim materiasArr As JArray = TryCast(ja("materias"), JArray)
                If materiasArr IsNot Nothing AndAlso materiasArr.Count > 0 Then
                    Dim materiaMatch As JObject = Nothing
                    If Not String.IsNullOrWhiteSpace(MateriaActual) Then
                        For Each jm As JObject In materiasArr
                            If String.Equals(jm("nombreMateria")?.ToString(), MateriaActual, StringComparison.OrdinalIgnoreCase) Then
                                materiaMatch = jm
                                Exit For
                            End If
                        Next
                    End If
                    If materiaMatch Is Nothing Then materiaMatch = TryCast(materiasArr(0), JObject)
                    If materiaMatch IsNot Nothing Then
                        Dim notasArr As JArray = TryCast(materiaMatch("notas"), JArray)
                        If notasArr IsNot Nothing AndAlso notasArr.Count > 0 Then
                            Dim sum As Double = 0
                            For Each n In notasArr
                                sum += CDbl(n.ToString())
                            Next
                            promedio = sum / notasArr.Count
                        End If
                    End If
                End If

                ' Asistencia
                Dim porcentaje As Double = 100.0
                Dim asistArr As JArray = TryCast(ja("asistencias"), JArray)
                If asistArr IsNot Nothing AndAlso asistArr.Count > 0 Then
                    Dim totalPresentes As Integer = 0
                    For Each jas As JObject In asistArr
                        If jas("presente") IsNot Nothing AndAlso CBool(jas("presente")) Then totalPresentes += 1
                    Next
                    porcentaje = (totalPresentes * 100.0) / asistArr.Count
                End If

                dgvAlumnos.Rows.Add(nombreFull, porcentaje.ToString("N2") & " %", promedio.ToString("N2"))
            Next
        Catch ex As Exception
            MessageBox.Show("Error leyendo db-alumnos.json: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ----4. MANEJAR LOS CLICS EN LOS BOTONES DEL GRID ----
    ' --- LÓGICA COMPLETAMENTE NUEVA ---
    Private Sub dgvAlumnos_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvAlumnos.CellClick
        If e.RowIndex < 0 Then Return

        Dim nombreAlumno As String = dgvAlumnos.Rows(e.RowIndex).Cells("Nombre").Value.ToString()
        Dim index As Integer = e.RowIndex
        If index < 0 OrElse index >= AlumnosJson.Count Then
            MessageBox.Show("Índice inválido de alumno.")
            Return
        End If

        Dim alumnoJson As JObject = AlumnosJson(index)

        Dim nombreColumna As String = dgvAlumnos.Columns(e.ColumnIndex).Name

        Select Case nombreColumna
            Case "btnDetalle"
                ' Mostrar detalle: convertimos a objeto Alumno temporal
                Dim alumnoObj As New Alumno($"{alumnoJson("nombre")?.ToString()} {alumnoJson("apellido")?.ToString()}")
                Dim asistArr As JArray = TryCast(alumnoJson("asistencias"), JArray)
                If asistArr IsNot Nothing Then
                    For Each jas As JObject In asistArr
                        Dim dt As Date
                        If Date.TryParse(jas("fecha")?.ToString(), dt) Then
                            alumnoObj.RegistrosAsistencia.Add(New AsistenciaRegistro(dt, If(CBool(jas("presente")), EstadoAsistencia.Presente, EstadoAsistencia.Ausente)))
                        End If
                    Next
                End If
                Dim formDetalle As New FormDetalleAsistencia(alumnoObj)
                formDetalle.ShowDialog()

            Case "btnNotas"
                ' Abrir FormCarga para editar notas en JSON
                Try
                    Dim formCarga As New FormCarga(alumnoJson, If(String.IsNullOrWhiteSpace(MateriaActual), Nothing, MateriaActual))
                    formCarga.ShowDialog()
                    CargarDatosAlGrid()
                Catch ex As Exception
                    MessageBox.Show("Error al abrir editor de notas: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try

            Case "btnPresente"
                MarcarAsistenciaJson(alumnoJson, True)

            Case "btnAusente"
                MarcarAsistenciaJson(alumnoJson, False)
        End Select
    End Sub

    Private Sub MarcarAsistenciaJson(alumnoJson As JObject, presente As Boolean)
        Dim usuario As String = alumnoJson("usuario")?.ToString()
        If String.IsNullOrWhiteSpace(usuario) Then
            MessageBox.Show("No se puede identificar al alumno para guardar asistencia.")
            Return
        End If
        Dim fecha As Date = Date.Now.Date
        Try
            DataStore.UpdateAsistencia(usuario, fecha, presente)
            MessageBox.Show("Asistencia guardada.")
        Catch ex As Exception
            MessageBox.Show("Error al guardar asistencia: " & ex.Message)
        End Try
        CargarDatosAlGrid()
    End Sub

    ' ----5. Botón de "Refrescar" ----
    Private Sub btnRefrescar_Click(sender As Object, e As EventArgs) Handles btnRefrescar.Click
        CargarDatosAlGrid()
    End Sub

    ' ----6. Botón Agregar Alumno ----
    Private Sub btnAgregarAlumno_Click(sender As Object, e As EventArgs) Handles btnAgregarAlumno.Click
        Dim formAgregar As New FormAgregarAlumno(If(String.IsNullOrWhiteSpace(MateriaActual), Nothing, MateriaActual))
        formAgregar.ShowDialog()
        ' After dialog closes, refresh list in case a new student was added
        CargarDatosAlGrid()
    End Sub

    Private Sub BtnVolver2_Click(sender As Object, e As EventArgs) Handles BtnVolver2.Click
        Dim volverLogin As New Login()
        volverLogin.Show()
        Me.Hide()
    End Sub
End Class