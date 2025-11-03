' Archivo: Alumno.vb
Imports System.Linq
' --- NUEVO: Definimos los estados posibles ---
' (Lo ponemos fuera de la clase para que sea accesible por otros formularios)
Public Enum EstadoAsistencia
    Presente
    Ausente
End Enum

Public Class Alumno
    Public Property NombreCompleto As String
    Public Property Nota1 As Double
    Public Property Nota2 As Double
    Public Property Nota3 As Double

    ' --- REEMPLAZADO: Ya no usamos contadores simples ---
    ' --- NUEVO: Una lista para guardar cada día ---
    Public Property RegistrosAsistencia As New List(Of AsistenciaRegistro)()

    ' Constructor
    Public Sub New(nombre As String)
        Me.NombreCompleto = nombre
        Me.Nota1 = 0
        Me.Nota2 = 0
        Me.Nota3 = 0
        ' (La lista RegistrosAsistencia se inicializa sola)
    End Sub

    ' --- AJUSTADO: Ahora calcula el promedio ---
    Public Function CalcularPromedio() As Double
        If Nota1 = 0 AndAlso Nota2 = 0 AndAlso Nota3 = 0 Then
            Return 0.0
        End If
        Return (Nota1 + Nota2 + Nota3) / 3
    End Function

    ' --- AJUSTADO: Ahora calcula la asistencia desde la LISTA ---
    Public Function PorcentajeAsistencia() As Double
        If RegistrosAsistencia.Count = 0 Then
            Return 100.0 ' Si no hay registros, tiene 100%
        End If

        ' Contamos cuántos registros son "Presente"
        Dim totalPresentes As Integer = RegistrosAsistencia.Where(Function(r) r.Estado = EstadoAsistencia.Presente).Count()

        ' Devolvemos el porcentaje
        Return (totalPresentes * 100.0) / RegistrosAsistencia.Count
    End Function

    ' Constructor sin argumentos (necesario para la serialización JSON)
    Public Sub New()
    End Sub
End Class

' --- NUEVA CLASE: El "molde" para un registro de asistencia ---
Public Class AsistenciaRegistro
    Public Property Fecha As Date
    Public Property Estado As EstadoAsistencia

    Public Sub New(fecha As Date, estado As EstadoAsistencia)
        Me.Fecha = fecha
        Me.Estado = estado
    End Sub

    ' Constructor sin argumentos (necesario para la serialización JSON)
    Public Sub New()
    End Sub
End Class