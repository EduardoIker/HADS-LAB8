Imports AccesoDatos.accesoDatosSQL

Public Class WebForm4
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        conectar()
        Dim email As String
        Dim numConf As Integer
        Dim resultado As String
        email = Request.QueryString("mbr")
        numConf = Request.QueryString("numconf")
        resultado = activar(email, numConf)
        If (resultado = 0) Then
            Label2.Text = "Su cuenta ha sido activada"
        ElseIf (resultado = 1) Then
            Label1.Text = "Error :'("
            Label2.Text = "Error en el acceso a la BD de la aplicación. Inténtalo de nuevo."
        Else
            Label1.Text = "Error :'("
            Label2.Text = "El correo o el número de validación no coinciden con los registrados."
        End If
    End Sub

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        cerrarConexion()
    End Sub

End Class