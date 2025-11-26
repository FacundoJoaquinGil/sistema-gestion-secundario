<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class NotasAlumno
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(NotasAlumno))
        btnCerrar = New Button()
        lblNombreAlumno = New Label()
        cbMaterias = New ComboBox()
        dgvNotas = New DataGridView()
        Column1 = New DataGridViewTextBoxColumn()
        Column2 = New DataGridViewTextBoxColumn()
        Column3 = New DataGridViewTextBoxColumn()
        lblPromedio = New Label()
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
        btnCerrar.Location = New Point(664, 502)
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
        lblNombreAlumno.Font = New Font("Arial", 18.0F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblNombreAlumno.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        lblNombreAlumno.Location = New Point(12, 42)
        lblNombreAlumno.Name = "lblNombreAlumno"
        lblNombreAlumno.Size = New Size(117, 29)
        lblNombreAlumno.TabIndex = 17
        lblNombreAlumno.Text = "Alumno: "
        ' 
        ' cbMaterias
        ' 
        cbMaterias.BackColor = Color.Azure
        cbMaterias.Font = New Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        cbMaterias.FormattingEnabled = True
        cbMaterias.Location = New Point(12, 109)
        cbMaterias.Name = "cbMaterias"
        cbMaterias.Size = New Size(200, 28)
        cbMaterias.TabIndex = 18
        ' 
        ' dgvNotas
        ' 
        dgvNotas.AllowUserToAddRows = False
        dgvNotas.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = Color.FromArgb(CByte(250), CByte(250), CByte(250))
        dgvNotas.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        dgvNotas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvNotas.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
        dgvNotas.BorderStyle = BorderStyle.None
        DataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = Color.Azure
        DataGridViewCellStyle2.Font = New Font("Segoe UI", 12.0F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle2.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        DataGridViewCellStyle2.SelectionBackColor = Color.LightSkyBlue
        DataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = DataGridViewTriState.True
        dgvNotas.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        dgvNotas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvNotas.Columns.AddRange(New DataGridViewColumn() {Column1, Column2, Column3})
        DataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle6.BackColor = SystemColors.Window
        DataGridViewCellStyle6.Font = New Font("Segoe UI", 11.0F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle6.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        DataGridViewCellStyle6.SelectionBackColor = SystemColors.Highlight
        DataGridViewCellStyle6.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = DataGridViewTriState.False
        dgvNotas.DefaultCellStyle = DataGridViewCellStyle6
        dgvNotas.EnableHeadersVisualStyles = False
        dgvNotas.GridColor = Color.LightGray
        dgvNotas.Location = New Point(12, 143)
        dgvNotas.Name = "dgvNotas"
        dgvNotas.ReadOnly = True
        dgvNotas.RowHeadersVisible = False
        dgvNotas.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvNotas.Size = New Size(760, 49)
        dgvNotas.TabIndex = 19
        ' 
        ' Column1
        ' 
        DataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter
        Column1.DefaultCellStyle = DataGridViewCellStyle3
        Column1.HeaderText = "1er trimestre"
        Column1.Name = "Column1"
        Column1.ReadOnly = True
        Column1.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' Column2
        ' 
        DataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter
        Column2.DefaultCellStyle = DataGridViewCellStyle4
        Column2.HeaderText = "2do trimestre"
        Column2.Name = "Column2"
        Column2.ReadOnly = True
        Column2.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' Column3
        ' 
        DataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleCenter
        Column3.DefaultCellStyle = DataGridViewCellStyle5
        Column3.HeaderText = "3er trimestre"
        Column3.Name = "Column3"
        Column3.ReadOnly = True
        Column3.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' lblPromedio
        ' 
        lblPromedio.AutoSize = True
        lblPromedio.BackColor = Color.Azure
        lblPromedio.Font = New Font("Arial", 20.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblPromedio.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        lblPromedio.Location = New Point(12, 231)
        lblPromedio.Name = "lblPromedio"
        lblPromedio.Size = New Size(142, 32)
        lblPromedio.TabIndex = 20
        lblPromedio.Text = "promedio"
        ' 
        ' NotasAlumno
        ' 
        AutoScaleDimensions = New SizeF(7.0F, 15.0F)
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
        Text = "Notas del Alumno"
        CType(dgvNotas, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents btnCerrar As Button
    Friend WithEvents lblNombreAlumno As Label
    Friend WithEvents dgvNotas As DataGridView
    Friend WithEvents lblPromedio As Label
    Friend WithEvents Column1 As DataGridViewTextBoxColumn
    Friend WithEvents Column2 As DataGridViewTextBoxColumn
    Friend WithEvents Column3 As DataGridViewTextBoxColumn
    Private WithEvents cbMaterias As ComboBox
End Class
