' Archivo: DatosGlobales.vb
Imports System.IO
Imports System.Text.Json

Module DatosGlobales
    ' Esta lista guardará TODOS los alumnos mientras la app esté abierta
    Public ListaAlumnos As New List(Of Alumno)()

    Private ReadOnly Property DataFilePath As String
        Get
            ' Guarda el JSON en la misma carpeta que el .exe
            Return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "alumnos.json")
        End Get
    End Property

    Public Sub LoadFromFile()
        Try
            If File.Exists(DataFilePath) Then
                Dim json As String = File.ReadAllText(DataFilePath)
                Dim list As List(Of Alumno) = JsonSerializer.Deserialize(Of List(Of Alumno))(json)
                If list IsNot Nothing Then
                    ListaAlumnos = list
                    ' Aseguramos que existan las asistencias de muestra en memoria (si faltan)
                    AgregarAsistenciasDeMuestra()
                Else
                    ListaAlumnos = New List(Of Alumno)()
                    ' Si no hay datos, podemos poblar asistencias de ejemplo en memoria
                    AgregarAsistenciasDeMuestra()
                End If
            Else
                ListaAlumnos = New List(Of Alumno)()
                ' Si no existe el archivo, añadimos datos de ejemplo en memoria
                AgregarAsistenciasDeMuestra()
            End If
        Catch ex As Exception
            ' Si falla la lectura/parseo, iniciamos con lista vacía y datos de ejemplo
            ListaAlumnos = New List(Of Alumno)()
            AgregarAsistenciasDeMuestra()
        End Try
    End Sub

    Public Sub SaveToFile()
        Try
            Dim options As New JsonSerializerOptions With {
                .WriteIndented = True
            }
            Dim json As String = JsonSerializer.Serialize(ListaAlumnos, options)
            File.WriteAllText(DataFilePath, json)
        Catch ex As Exception
            ' Ignoramos errores de escritura por ahora
        End Try
    End Sub

    ' Nuevo: agrega asistencias en memoria desde25/10/2025 hasta30/10/2025
    Public Sub AgregarAsistenciasDeMuestra()
        ' Si no hay alumnos, creamos algunos de ejemplo
        If ListaAlumnos.Count = 0 Then
            ListaAlumnos.Add(New Alumno("Juan Pérez"))
            ListaAlumnos.Add(New Alumno("María Gómez"))
        End If

        Dim fechaInicio As Date = New Date(2025, 10, 25)
        Dim fechaFin As Date = New Date(2025, 10, 30)

        For Each alumno In ListaAlumnos
            ' Evitar duplicados: si ya tiene registros en ese rango, saltarlos
            Dim tieneRegistrosEnRango As Boolean = alumno.RegistrosAsistencia.Any(Function(r) r.Fecha >= fechaInicio AndAlso r.Fecha <= fechaFin)
            If tieneRegistrosEnRango Then
                Continue For
            End If

            Dim d As Date = fechaInicio
            While d <= fechaFin
                ' Patrón de ejemplo: días impares -> Presente, pares -> Ausente
                Dim estado As EstadoAsistencia = If(d.Day Mod 2 = 1, EstadoAsistencia.Presente, EstadoAsistencia.Ausente)
                alumno.RegistrosAsistencia.Add(New AsistenciaRegistro(d, estado))
                d = d.AddDays(1)
            End While
        Next
    End Sub
End Module