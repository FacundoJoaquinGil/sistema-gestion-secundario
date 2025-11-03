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
        lblMonthYear = New Label()
        ToolTip1 = New ToolTip(components)
        lblUsuario = New Label()
        CType(PictureBox1, ComponentModel.ISupportInitialize).BeginInit()
        flpCalendar.SuspendLayout()
        SuspendLayout()
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.BackColor = Color.Transparent
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
        PictureBox1.Location = New Point(256, 12)
        PictureBox1.Name = "PictureBox1"
        PictureBox1.Size = New Size(63, 63)
        PictureBox1.SizeMode = PictureBoxSizeMode.Zoom
        PictureBox1.TabIndex = 5
        PictureBox1.TabStop = False
        ' 
        ' flpCalendar
        ' 
        flpCalendar.Controls.Add(lblLegend)
        flpCalendar.Location = New Point(12, 169)
        flpCalendar.Name = "flpCalendar"
        flpCalendar.Size = New Size(760, 227)
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
        btnPrev.Font = New Font("Segoe UI Black", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnPrev.Location = New Point(88, 402)
        btnPrev.Name = "btnPrev"
        btnPrev.Size = New Size(45, 39)
        btnPrev.TabIndex = 8
        btnPrev.Text = "<"
        btnPrev.UseVisualStyleBackColor = True
        ' 
        ' btnNext
        ' 
        btnNext.Font = New Font("Segoe UI Black", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnNext.Location = New Point(541, 402)
        btnNext.Name = "btnNext"
        btnNext.Size = New Size(45, 39)
        btnNext.TabIndex = 9
        btnNext.Text = ">"
        btnNext.UseVisualStyleBackColor = True
        ' 
        ' lblMonthYear
        ' 
        lblMonthYear.AutoSize = True
        lblMonthYear.Font = New Font("Segoe UI Black", 11.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblMonthYear.Location = New Point(619, 139)
        lblMonthYear.Name = "lblMonthYear"
        lblMonthYear.Size = New Size(0, 20)
        lblMonthYear.TabIndex = 10
        ' 
        ' lblUsuario
        ' 
        lblUsuario.AutoSize = True
        lblUsuario.BackColor = Color.Transparent
        lblUsuario.Font = New Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblUsuario.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        lblUsuario.Location = New Point(12, 100)
        lblUsuario.Name = "lblUsuario"
        lblUsuario.Size = New Size(0, 30)
        lblUsuario.TabIndex = 11
        ' 
        ' PortalAlumnos
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        BackgroundImage = My.Resources.Resources.background
        ClientSize = New Size(784, 561)
        Controls.Add(lblUsuario)
        Controls.Add(lblMonthYear)
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
    Friend WithEvents lblMonthYear As Label
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents lblUsuario As Label
End Class
