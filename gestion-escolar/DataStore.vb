' Archivo: DataStore.vb
Imports System.IO
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Module DataStore
    Private ReadOnly Property DbPath As String
        Get
            Return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "db-alumnos.json")
        End Get
    End Property

    Public Function LoadRootJObject() As JObject
        If Not File.Exists(DbPath) Then
            Throw New FileNotFoundException("db-alumnos.json no encontrado en: " & DbPath)
        End If
        Dim json = File.ReadAllText(DbPath)
        Return JObject.Parse(json)
    End Function

    Public Sub SaveRootJObject(root As JObject)
        Dim text = root.ToString(Formatting.Indented)
        File.WriteAllText(DbPath, text)
    End Sub

    Public Function GetAlumnosJArray() As JArray
        Dim root = LoadRootJObject()
        Dim arr As JArray = TryCast(root("alumnos"), JArray)
        If arr Is Nothing Then Return New JArray()
        Return arr
    End Function

    Public Function GetProfesoresJArray() As JArray
        Dim root = LoadRootJObject()
        Dim arr As JArray = TryCast(root("profesores"), JArray)
        If arr Is Nothing Then Return New JArray()
        Return arr
    End Function

    Public Function GetAlumnosByMateria(materia As String) As JArray
        Dim all = GetAlumnosJArray()
        If String.IsNullOrWhiteSpace(materia) Then Return New JArray(all)
        Dim result As New JArray()
        For Each a As JObject In all
            Dim materias = TryCast(a("materias"), JArray)
            If materias Is Nothing Then Continue For
            For Each m As JObject In materias
                If String.Equals(m("nombreMateria")?.ToString(), materia, StringComparison.OrdinalIgnoreCase) Then
                    result.Add(a)
                    Exit For
                End If
            Next
        Next
        Return result
    End Function

    Public Function GetAlumnoByUsuario(usuario As String) As JObject
        If String.IsNullOrWhiteSpace(usuario) Then Return Nothing
        Dim all = GetAlumnosJArray()
        For Each a As JObject In all
            If String.Equals(a("usuario")?.ToString(), usuario, StringComparison.OrdinalIgnoreCase) Then
                Return a
            End If
        Next
        Return Nothing
    End Function

    Public Sub UpdateAsistencia(usuario As String, fecha As Date, presente As Boolean)
        Dim root = LoadRootJObject()
        Dim jAlumnos As JArray = TryCast(root("alumnos"), JArray)
        If jAlumnos Is Nothing Then Throw New Exception("Formato JSON inválido: falta 'alumnos'.")

        Dim target As JObject = Nothing
        For Each ja As JObject In jAlumnos
            If String.Equals(ja("usuario")?.ToString(), usuario, StringComparison.OrdinalIgnoreCase) Then
                target = ja
                Exit For
            End If
        Next

        If target Is Nothing Then Throw New Exception("No se encontró alumno con usuario: " & usuario)

        Dim asistArr As JArray = TryCast(target("asistencias"), JArray)
        If asistArr Is Nothing Then
            asistArr = New JArray()
            target("asistencias") = asistArr
        End If

        Dim fechaStr As String = fecha.ToString("yyyy-MM-dd")
        Dim encontrado As JObject = Nothing
        For Each jas As JObject In asistArr
            If String.Equals(jas("fecha")?.ToString(), fechaStr, StringComparison.OrdinalIgnoreCase) Then
                encontrado = jas
                Exit For
            End If
        Next

        If encontrado IsNot Nothing Then
            encontrado("presente") = presente
        Else
            Dim nuevo As New JObject()
            nuevo("fecha") = fechaStr
            nuevo("presente") = presente
            asistArr.Add(nuevo)
        End If

        SaveRootJObject(root)
    End Sub
End Module
