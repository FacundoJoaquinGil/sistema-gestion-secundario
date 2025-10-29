' Archivo: Alumno.vb
Public Class Alumno
    Public Property NombreCompleto As String
    Public Property Nota1 As Double
    Public Property Nota2 As Double
    Public Property Presentes As Integer
    Public Property Ausentes As Integer

    ' Constructor: Un atajo para crear un alumno nuevo solo con su nombre
    Public Sub New(nombre As String)
        Me.NombreCompleto = nombre
        ' Lo inicializamos con datos vacíos
        Me.Nota1 = 0
        Me.Nota2 = 0
        Me.Presentes = 0
        Me.Ausentes = 0
    End Sub

    ' Función que calcula el promedio (la usaremos en el futuro)
    Public Function CalcularPromedio() As Double
        Return (Nota1 + Nota2) / 2
    End Function

    ' AÑADIDA: Función que calcula el porcentaje de asistencia
    Public Function PorcentajeAsistencia() As Double
        Dim totalClases As Integer = Presentes + Ausentes
        If totalClases = 0 Then
            Return 100.0 ' Si no hay clases, tiene 100%
        End If
        Return (Presentes * 100.0) / totalClases
    End Function
End Class