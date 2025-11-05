<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormCarga
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

    'Requerido por el Dise?ador de Windows Forms
    Private components As System.ComponentModel.IContainer

    Friend WithEvents lblNombreAlumno As System.Windows.Forms.Label
    Friend WithEvents lblNota1 As System.Windows.Forms.Label
    Friend WithEvents txtNota1 As System.Windows.Forms.TextBox
    Friend WithEvents lblNota2 As System.Windows.Forms.Label
    Friend WithEvents txtNota2 As System.Windows.Forms.TextBox
    Friend WithEvents lblNota3 As System.Windows.Forms.Label
    Friend WithEvents txtNota3 As System.Windows.Forms.TextBox
    Friend WithEvents btnGuardarNotas As System.Windows.Forms.Button

    'NOTA: el Dise?ador necesita el siguiente procedimiento
    'Se puede modificar usando el Dise?ador de Windows Forms. 
    'No lo modifique con el editor de c?digo.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.lblNombreAlumno = New System.Windows.Forms.Label()
        Me.lblNota1 = New System.Windows.Forms.Label()
        Me.txtNota1 = New System.Windows.Forms.TextBox()
        Me.lblNota2 = New System.Windows.Forms.Label()
        Me.txtNota2 = New System.Windows.Forms.TextBox()
        Me.lblNota3 = New System.Windows.Forms.Label()
        Me.txtNota3 = New System.Windows.Forms.TextBox()
        Me.btnGuardarNotas = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'lblNombreAlumno
        '
        Me.lblNombreAlumno.AutoSize = True
        Me.lblNombreAlumno.Location = New System.Drawing.Point(12, 15)
        Me.lblNombreAlumno.Name = "lblNombreAlumno"
        Me.lblNombreAlumno.Size = New System.Drawing.Size(200, 20)
        Me.lblNombreAlumno.TabIndex = 0
        Me.lblNombreAlumno.Text = "Cargando notas para: ..."
        '
        'lblNota1
        '
        Me.lblNota1.AutoSize = True
        Me.lblNota1.Location = New System.Drawing.Point(12, 55)
        Me.lblNota1.Name = "lblNota1"
        Me.lblNota1.Size = New System.Drawing.Size(53, 20)
        Me.lblNota1.TabIndex = 1
        Me.lblNota1.Text = "Nota1:"
        '
        'txtNota1
        '
        Me.txtNota1.Location = New System.Drawing.Point(80, 52)
        Me.txtNota1.Name = "txtNota1"
        Me.txtNota1.Size = New System.Drawing.Size(100, 27)
        Me.txtNota1.TabIndex = 2
        '
        'lblNota2
        '
        Me.lblNota2.AutoSize = True
        Me.lblNota2.Location = New System.Drawing.Point(12, 95)
        Me.lblNota2.Name = "lblNota2"
        Me.lblNota2.Size = New System.Drawing.Size(53, 20)
        Me.lblNota2.TabIndex = 3
        Me.lblNota2.Text = "Nota2:"
        '
        'txtNota2
        '
        Me.txtNota2.Location = New System.Drawing.Point(80, 92)
        Me.txtNota2.Name = "txtNota2"
        Me.txtNota2.Size = New System.Drawing.Size(100, 27)
        Me.txtNota2.TabIndex = 4
        '
        'lblNota3
        '
        Me.lblNota3.AutoSize = True
        Me.lblNota3.Location = New System.Drawing.Point(12, 135)
        Me.lblNota3.Name = "lblNota3"
        Me.lblNota3.Size = New System.Drawing.Size(53, 20)
        Me.lblNota3.TabIndex = 5
        Me.lblNota3.Text = "Nota3:"
        '
        'txtNota3
        '
        Me.txtNota3.Location = New System.Drawing.Point(80, 132)
        Me.txtNota3.Name = "txtNota3"
        Me.txtNota3.Size = New System.Drawing.Size(100, 27)
        Me.txtNota3.TabIndex = 6
        '
        'btnGuardarNotas
        '
        Me.btnGuardarNotas.Location = New System.Drawing.Point(80, 175)
        Me.btnGuardarNotas.Name = "btnGuardarNotas"
        Me.btnGuardarNotas.Size = New System.Drawing.Size(120, 30)
        Me.btnGuardarNotas.TabIndex = 7
        Me.btnGuardarNotas.Text = "Guardar Notas"
        Me.btnGuardarNotas.UseVisualStyleBackColor = True
        '
        'FormCarga
        '
        Me.ClientSize = New System.Drawing.Size(320, 230)
        Me.Controls.Add(Me.lblNombreAlumno)
        Me.Controls.Add(Me.lblNota1)
        Me.Controls.Add(Me.txtNota1)
        Me.Controls.Add(Me.lblNota2)
        Me.Controls.Add(Me.txtNota2)
        Me.Controls.Add(Me.lblNota3)
        Me.Controls.Add(Me.txtNota3)
        Me.Controls.Add(Me.btnGuardarNotas)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FormCarga"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Cargar Notas"
        Me.ResumeLayout(False)
        Me.PerformLayout()
    End Sub
End Class