<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class NotasAlumno
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(NotasAlumno))
        btnCerrar = New Button()
        lblNombreAlumno = New Label()
        cbMaterias = New ComboBox()
        dgvNotas = New DataGridView()
        lblPromedio = New Label()
        Column1 = New DataGridViewTextBoxColumn()
        Column2 = New DataGridViewTextBoxColumn()
        CType(dgvNotas, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' btnCerrar
        ' 
        btnCerrar.BackColor = Color.MediumOrchid
        btnCerrar.Cursor = Cursors.Hand
        btnCerrar.FlatStyle = FlatStyle.Flat
        btnCerrar.Font = New Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnCerrar.ForeColor = Color.White
        btnCerrar.Location = New Point(664, 514)
        btnCerrar.Name = "btnCerrar"
        btnCerrar.Size = New Size(108, 35)
        btnCerrar.TabIndex = 14
        btnCerrar.Text = "VOLVER"
        btnCerrar.UseVisualStyleBackColor = False
        ' 
        ' lblNombreAlumno
        ' 
        lblNombreAlumno.AutoSize = True
        lblNombreAlumno.BackColor = Color.Azure
        lblNombreAlumno.Font = New Font("Segoe UI Black", 11.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblNombreAlumno.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        lblNombreAlumno.Location = New Point(12, 9)
        lblNombreAlumno.Name = "lblNombreAlumno"
        lblNombreAlumno.Size = New Size(76, 20)
        lblNombreAlumno.TabIndex = 17
        lblNombreAlumno.Text = "Alumno: "
        ' 
        ' cbMaterias
        ' 
        cbMaterias.FormattingEnabled = True
        cbMaterias.Location = New Point(12, 49)
        cbMaterias.Name = "cbMaterias"
        cbMaterias.Size = New Size(121, 23)
        cbMaterias.TabIndex = 18
        ' 
        ' dgvNotas
        ' 
        dgvNotas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvNotas.Columns.AddRange(New DataGridViewColumn() {Column1, Column2})
        dgvNotas.Location = New Point(12, 93)
        dgvNotas.Name = "dgvNotas"
        dgvNotas.Size = New Size(760, 150)
        dgvNotas.TabIndex = 19
        ' 
        ' lblPromedio
        ' 
        lblPromedio.AutoSize = True
        lblPromedio.BackColor = Color.Azure
        lblPromedio.Font = New Font("Segoe UI Black", 11.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblPromedio.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        lblPromedio.Location = New Point(12, 258)
        lblPromedio.Name = "lblPromedio"
        lblPromedio.Size = New Size(83, 20)
        lblPromedio.TabIndex = 20
        lblPromedio.Text = "promedio"
        ' 
        ' Column1
        ' 
        Column1.HeaderText = "Column1"
        Column1.Name = "Column1"
        ' 
        ' Column2
        ' 
        Column2.HeaderText = "Column2"
        Column2.Name = "Column2"
        ' 
        ' NotasAlumno
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        BackgroundImage = My.Resources.Resources.background
        ClientSize = New Size(784, 561)
        Controls.Add(lblPromedio)
        Controls.Add(dgvNotas)
        Controls.Add(cbMaterias)
        Controls.Add(lblNombreAlumno)
        Controls.Add(btnCerrar)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Name = "NotasAlumno"
        StartPosition = FormStartPosition.CenterScreen
        Text = "NotasAlumno"
        CType(dgvNotas, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents btnCerrar As Button
    Friend WithEvents lblNombreAlumno As Label
    Friend WithEvents cbMaterias As ComboBox
    Friend WithEvents dgvNotas As DataGridView
    Friend WithEvents lblPromedio As Label
    Friend WithEvents Column1 As DataGridViewTextBoxColumn
    Friend WithEvents Column2 As DataGridViewTextBoxColumn
End Class
