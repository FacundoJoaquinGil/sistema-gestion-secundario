' Archivo: DataStore.vb
Imports System.IO
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Module DataStore
    ' Runtime path (copy used when app deployed)
    Private ReadOnly Property RuntimeDbPath As String
        Get
            Return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "db-alumnos.json")
        End Get
    End Property

    ' Cached project file path (if found)
    Private _projectDbPath As String = Nothing

    Private Function FindProjectDbPath() As String
        If _projectDbPath IsNot Nothing Then
            Return _projectDbPath
        End If

        Try
            Dim dir As DirectoryInfo = New DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory)
            While dir IsNot Nothing
                Dim candidate As String = System.IO.Path.Combine(dir.FullName, "db-alumnos.json")
                If File.Exists(candidate) Then
                    _projectDbPath = candidate
                    Return _projectDbPath
                End If
                dir = dir.Parent
            End While
        Catch
            ' ignore
        End Try

        Return Nothing
    End Function

    ' New: try to find project directory by locating the project file 'gestion-escolar.vbproj'
    Private Function FindProjectDirByProjFile() As String
        Try
            Dim dir As DirectoryInfo = New DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory)
            While dir IsNot Nothing
                Dim projCandidate As String = System.IO.Path.Combine(dir.FullName, "gestion-escolar.vbproj")
                If File.Exists(projCandidate) Then
                    Return dir.FullName
                End If
                dir = dir.Parent
            End While
        Catch
        End Try
        Return Nothing
    End Function

    ' Determine which path to use for persisting project file (prefer explicit project folder's db-alumnos.json)
    Private Function GetPreferredDbPath() As String
        ' First try to find project dir by the vbproj filename
        Dim projDir = FindProjectDirByProjFile()
        If Not String.IsNullOrWhiteSpace(projDir) Then
            Dim projectJson As String = System.IO.Path.Combine(projDir, "db-alumnos.json")
            If File.Exists(projectJson) Then
                Return projectJson
            End If
            ' if file doesn't exist yet, still prefer this location for saving
            Return projectJson
        End If

        ' Fallback: try to find db-alumnos.json by walking up from runtime
        Dim alt As String = FindProjectDbPath()
        If Not String.IsNullOrWhiteSpace(alt) Then
            Return alt
        End If

        ' Last fallback: runtime copy
        Return RuntimeDbPath
    End Function

    Public Function LoadRootJObject() As JObject
        ' Prefer project file if exists; otherwise use runtime path
        Dim path As String = GetPreferredDbPath()
        If Not File.Exists(path) Then
            Throw New FileNotFoundException("db-alumnos.json no encontrado en: " & path)
        End If
        Dim json = File.ReadAllText(path)
        Return JObject.Parse(json)
    End Function

    Public Sub SaveRootJObject(root As JObject)
        Dim text = root.ToString(Formatting.Indented)
        Dim path As String = GetPreferredDbPath()

        ' Guardar en el archivo preferido (proyecto si existe)
        Dim savedProjectPath As String = Nothing
        Dim savedRuntimePath As String = Nothing
        Try
            ' Ensure directory exists
            Dim dir = System.IO.Path.GetDirectoryName(path)
            If Not String.IsNullOrWhiteSpace(dir) AndAlso Not Directory.Exists(dir) Then
                Directory.CreateDirectory(dir)
            End If
            File.WriteAllText(path, text)
            savedProjectPath = path
        Catch
            ' Ignorar fallo al escribir en el archivo preferido
        End Try

        ' Asegurar que la copia runtime también se actualice
        Try
            File.WriteAllText(RuntimeDbPath, text)
            savedRuntimePath = RuntimeDbPath
        Catch
            ' Ignorar fallo al escribir la copia runtime
        End Try

        ' Mostrar confirmación con las rutas escritas
        Dim msg As String = "Se guardó db-alumnos.json."
        If Not String.IsNullOrWhiteSpace(savedProjectPath) Then
            msg &= vbCrLf & "Archivo del proyecto actualizado en: " & savedProjectPath
        End If
        If Not String.IsNullOrWhiteSpace(savedRuntimePath) Then
            msg &= vbCrLf & "Copia runtime actualizada en: " & savedRuntimePath
        End If
        If String.IsNullOrWhiteSpace(savedProjectPath) AndAlso String.IsNullOrWhiteSpace(savedRuntimePath) Then
            msg &= vbCrLf & "No se pudo guardar el archivo."
        End If

        MessageBox.Show(msg, "Confirmación de guardado", MessageBoxButtons.OK, MessageBoxIcon.Information)
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

    ' Nuevo: actualiza/crea las notas de un alumno. Si se especifica materiaNombre, se usa esa materia; si no, se usa la primera materia o se crea una.
    Public Sub UpdateNotas(usuario As String, notas() As Double, Optional materiaNombre As String = Nothing)
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

        Dim materiasArr As JArray = TryCast(target("materias"), JArray)
        If materiasArr Is Nothing Then
            materiasArr = New JArray()
            target("materias") = materiasArr
        End If

        Dim materiaObj As JObject = Nothing
        If Not String.IsNullOrWhiteSpace(materiaNombre) Then
            For Each m As JObject In materiasArr
                If String.Equals(m("nombreMateria")?.ToString(), materiaNombre, StringComparison.OrdinalIgnoreCase) Then
                    materiaObj = m
                    Exit For
                End If
            Next
        End If

        If materiaObj Is Nothing AndAlso materiasArr.Count > 0 Then
            materiaObj = TryCast(materiasArr(0), JObject)
        End If

        If materiaObj Is Nothing Then
            materiaObj = New JObject()
            materiaObj("idMateria") = 1
            materiaObj("nombreMateria") = If(String.IsNullOrWhiteSpace(materiaNombre), "Materia1", materiaNombre)
            materiaObj("notas") = New JArray()
            materiasArr.Add(materiaObj)
        End If

        Dim notasArr As New JArray()
        For Each n As Double In notas
            notasArr.Add(n)
        Next
        materiaObj("notas") = notasArr

        SaveRootJObject(root)
    End Sub
End Module
