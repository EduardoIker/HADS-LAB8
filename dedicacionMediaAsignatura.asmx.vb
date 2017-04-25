Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Data.SqlClient

' Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class dedicacionMediaAsignatura
    Inherits System.Web.Services.WebService

    <WebMethod()> _
    Public Function obtenerDedicacionMediaAsignatura(ByVal asignatura As String) As Single
        Dim conexion As SqlConnection = New SqlConnection("Data Source=tcp:hads2017.database.windows.net,1433;Initial Catalog=HADS17_TAREAS;Persist Security Info=True;User ID=hads17;Password=Camellos17")
        Dim dap = New SqlDataAdapter("SELECT AVG(ET.HReales) FROM EstudiantesTareas AS ET INNER JOIN TareasGenericas AS TG ON ET.CodTarea=TG.Codigo WHERE TG.CodAsig='" & asignatura & "'", conexion)
        Dim dst = New DataSet
        dap.Fill(dst, "Resultado")
        Dim media As Single
        Try
            media = Convert.ToSingle(dst.Tables("Resultado").Rows(0)(0).ToString)
        Catch ex As Exception
            media = 0.0
        End Try
        Return media
    End Function

End Class