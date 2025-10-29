Imports System.IO
Imports System.Text.Json

' Archivo: DatosGlobales.vb
Module DatosGlobales
    ' Esta lista guardará TODOS los alumnos mientras la app esté abierta
    Public ListaAlumnos As New List(Of Alumno)()

    Private ReadOnly Property DataFilePath As String
        Get
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
                Else
                    ListaAlumnos = New List(Of Alumno)()
                End If
            Else
                ListaAlumnos = New List(Of Alumno)()
            End If
        Catch ex As Exception
            ' Si falla la lectura/parseo, iniciamos con lista vacía
            ListaAlumnos = New List(Of Alumno)()
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
End Module