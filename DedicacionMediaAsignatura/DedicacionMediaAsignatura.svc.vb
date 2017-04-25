Imports System.Data.SqlClient

' NOTA: puede usar el comando "Cambiar nombre" del menú contextual para cambiar el nombre de clase "Service1" en el código, en svc y en el archivo de configuración.
' NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Service1.svc o Service1.svc.vb en el Explorador de soluciones e inicie la depuración.
Public Class DedicacionMediaAsignatura
    Implements IDedicacionMediaAsignatura

    Public Sub New()
    End Sub

    Function obtenerDedicacionMediaNoPresencial(ByVal asignatura As String) As Single Implements IDedicacionMediaAsignatura.obtenerDedicacionMediaNoPresencial
        Dim conexion As SqlConnection = New SqlConnection("Data Source=tcp:hads2017.database.windows.net,1433;Initial Catalog=HADS17_TAREAS;Persist Security Info=True;User ID=hads17;Password=Camellos17")
        Dim dap = New SqlDataAdapter("SELECT AVG(ET.HReales) FROM EstudiantesTareas AS ET INNER JOIN TareasGenericas AS TG ON ET.CodTarea=TG.Codigo WHERE TG.CodAsig='" & asignatura & "'", conexion)
        Dim dst = New DataSet
        dap.Fill(dst, "Resultado")
        Dim media As Single
        media = Convert.ToSingle(dst.Tables("Resultado").Rows(0)(0).ToString)
        Return media
    End Function

End Class
