<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PortalAlumnos
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
        components = New ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PortalAlumnos))
        Label2 = New Label()
        PictureBox1 = New PictureBox()
        flpCalendar = New FlowLayoutPanel()
        lblLegend = New Label()
        btnPrev = New Button()
        btnNext = New Button()
        LblPromedioAsistencias = New Label()
        ToolTip1 = New ToolTip(components)
        lblUsuario = New Label()
        BtnVolver = New Button()
        BtnNotas = New Button()
        LblAsistencias = New Label()
        LblMesActual = New Label()
        LblDiaHoy = New Label()
        CType(PictureBox1, ComponentModel.ISupportInitialize).BeginInit()
        flpCalendar.SuspendLayout()
        SuspendLayout()
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.BackColor = Color.Azure
        Label2.Font = New Font("Segoe UI", 20.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label2.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        Label2.Location = New Point(12, 30)
        Label2.Name = "Label2"
        Label2.Size = New Size(238, 37)
        Label2.TabIndex = 4
        Label2.Text = "Portal Estudiante"
        ' 
        ' PictureBox1
        ' 
        PictureBox1.BackColor = Color.Transparent
        PictureBox1.Image = My.Resources.Resources.usuario
        PictureBox1.Location = New Point(257, 28)
        PictureBox1.Name = "PictureBox1"
        PictureBox1.Size = New Size(44, 42)
        PictureBox1.SizeMode = PictureBoxSizeMode.Zoom
        PictureBox1.TabIndex = 5
        PictureBox1.TabStop = False
        ' 
        ' flpCalendar
        ' 
        flpCalendar.BackColor = Color.Azure
        flpCalendar.BorderStyle = BorderStyle.FixedSingle
        flpCalendar.Controls.Add(lblLegend)
        flpCalendar.Location = New Point(12, 150)
        flpCalendar.Name = "flpCalendar"
        flpCalendar.Size = New Size(480, 350)
        flpCalendar.TabIndex = 7
        ' 
        ' lblLegend
        ' 
        lblLegend.AutoSize = True
        lblLegend.Location = New Point(3, 0)
        lblLegend.Name = "lblLegend"
        lblLegend.Size = New Size(41, 15)
        lblLegend.TabIndex = 0
        lblLegend.Text = "Label1"
        ' 
        ' btnPrev
        ' 
        btnPrev.BackColor = Color.MediumOrchid
        btnPrev.Cursor = Cursors.Hand
        btnPrev.FlatStyle = FlatStyle.Flat
        btnPrev.Font = New Font("Courier New", 18F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnPrev.ForeColor = Color.White
        btnPrev.Location = New Point(12, 506)
        btnPrev.Name = "btnPrev"
        btnPrev.Size = New Size(45, 39)
        btnPrev.TabIndex = 8
        btnPrev.Text = "<"
        btnPrev.UseVisualStyleBackColor = False
        ' 
        ' btnNext
        ' 
        btnNext.BackColor = Color.MediumOrchid
        btnNext.Cursor = Cursors.Hand
        btnNext.FlatStyle = FlatStyle.Flat
        btnNext.Font = New Font("Courier New", 18F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnNext.ForeColor = Color.White
        btnNext.Location = New Point(447, 506)
        btnNext.Name = "btnNext"
        btnNext.Size = New Size(45, 39)
        btnNext.TabIndex = 9
        btnNext.Text = ">"
        btnNext.UseVisualStyleBackColor = False
        ' 
        ' LblPromedioAsistencias
        ' 
        LblPromedioAsistencias.AutoSize = True
        LblPromedioAsistencias.BackColor = Color.Azure
        LblPromedioAsistencias.Font = New Font("Segoe UI Black", 11.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        LblPromedioAsistencias.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        LblPromedioAsistencias.Location = New Point(498, 179)
        LblPromedioAsistencias.Name = "LblPromedioAsistencias"
        LblPromedioAsistencias.Size = New Size(83, 20)
        LblPromedioAsistencias.TabIndex = 10
        LblPromedioAsistencias.Text = "Promedio"
        ' 
        ' lblUsuario
        ' 
        lblUsuario.AutoSize = True
        lblUsuario.BackColor = Color.Azure
        lblUsuario.Font = New Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblUsuario.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        lblUsuario.Location = New Point(12, 117)
        lblUsuario.Name = "lblUsuario"
        lblUsuario.Size = New Size(0, 30)
        lblUsuario.TabIndex = 11
        ' 
        ' BtnVolver
        ' 
        BtnVolver.BackColor = Color.MediumOrchid
        BtnVolver.Cursor = Cursors.Hand
        BtnVolver.FlatStyle = FlatStyle.Flat
        BtnVolver.Font = New Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        BtnVolver.ForeColor = Color.White
        BtnVolver.Location = New Point(665, 516)
        BtnVolver.Name = "BtnVolver"
        BtnVolver.Size = New Size(108, 35)
        BtnVolver.TabIndex = 13
        BtnVolver.Text = "VOLVER"
        BtnVolver.UseVisualStyleBackColor = False
        ' 
        ' BtnNotas
        ' 
        BtnNotas.BackColor = Color.MediumOrchid
        BtnNotas.Cursor = Cursors.Hand
        BtnNotas.FlatStyle = FlatStyle.Flat
        BtnNotas.Font = New Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        BtnNotas.ForeColor = Color.White
        BtnNotas.Location = New Point(637, 46)
        BtnNotas.Name = "BtnNotas"
        BtnNotas.Size = New Size(135, 35)
        BtnNotas.TabIndex = 14
        BtnNotas.Text = "Ver Mis Notas"
        BtnNotas.UseVisualStyleBackColor = False
        ' 
        ' LblAsistencias
        ' 
        LblAsistencias.AutoSize = True
        LblAsistencias.BackColor = Color.Transparent
        LblAsistencias.Font = New Font("Segoe UI", 20.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        LblAsistencias.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        LblAsistencias.Location = New Point(12, 478)
        LblAsistencias.Name = "LblAsistencias"
        LblAsistencias.Size = New Size(0, 37)
        LblAsistencias.TabIndex = 15
        ' 
        ' LblMesActual
        ' 
        LblMesActual.AutoSize = True
        LblMesActual.BackColor = Color.Azure
        LblMesActual.Font = New Font("Segoe UI Black", 11.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        LblMesActual.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        LblMesActual.Location = New Point(498, 150)
        LblMesActual.Name = "LblMesActual"
        LblMesActual.Size = New Size(39, 20)
        LblMesActual.TabIndex = 16
        LblMesActual.Text = "Mes"
        ' 
        ' LblDiaHoy
        ' 
        LblDiaHoy.AutoSize = True
        LblDiaHoy.BackColor = Color.Azure
        LblDiaHoy.Font = New Font("Segoe UI Black", 11.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        LblDiaHoy.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        LblDiaHoy.Location = New Point(498, 210)
        LblDiaHoy.Name = "LblDiaHoy"
        LblDiaHoy.Size = New Size(90, 20)
        LblDiaHoy.TabIndex = 17
        LblDiaHoy.Text = "Dia de Hoy"
        ' 
        ' PortalAlumnos
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        BackgroundImage = My.Resources.Resources.background
        ClientSize = New Size(784, 561)
        Controls.Add(LblDiaHoy)
        Controls.Add(LblMesActual)
        Controls.Add(LblAsistencias)
        Controls.Add(BtnNotas)
        Controls.Add(BtnVolver)
        Controls.Add(lblUsuario)
        Controls.Add(LblPromedioAsistencias)
        Controls.Add(btnNext)
        Controls.Add(btnPrev)
        Controls.Add(flpCalendar)
        Controls.Add(PictureBox1)
        Controls.Add(Label2)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Name = "PortalAlumnos"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Alumnos"
        CType(PictureBox1, ComponentModel.ISupportInitialize).EndInit()
        flpCalendar.ResumeLayout(False)
        flpCalendar.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label2 As Label
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents flpCalendar As FlowLayoutPanel
    Friend WithEvents btnPrev As Button
    Friend WithEvents lblLegend As Label
    Friend WithEvents btnNext As Button
    Friend WithEvents LblPromedioAsistencias As Label
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents lblUsuario As Label
    Friend WithEvents BtnVolver As Button
    Friend WithEvents BtnNotas As Button
    Friend WithEvents LblAsistencias As Label
    Friend WithEvents LblMesActual As Label
    Friend WithEvents LblDiaHoy As Label
End Class
