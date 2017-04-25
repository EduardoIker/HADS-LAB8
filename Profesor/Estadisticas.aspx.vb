Public Class Estadisticas
    Inherits System.Web.UI.Page

    Protected Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles LinkButton1.Click
        Response.Redirect("../LogOut.aspx")
    End Sub

End Class