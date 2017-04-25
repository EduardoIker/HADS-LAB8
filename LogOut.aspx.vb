Public Class LogOut
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Eliminar el correo del usuario guardado en el objeto Application
        Dim email As String = Session.Contents("correo")
        Application.Lock()
        Dim usuariosConectados As ArrayList
        usuariosConectados = Application.Contents("listaUsuarios")
        usuariosConectados.Remove(email)
        Application.Contents("listaUsuarios") = usuariosConectados
        Application.UnLock()
        Session.Abandon()
        FormsAuthentication.SignOut()
        Response.Redirect("Inicio.aspx")
    End Sub

End Class