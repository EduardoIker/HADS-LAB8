Public Class coordinador
    Inherits System.Web.UI.Page

    Protected Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles LinkButton1.Click
        Response.Redirect("../../LogOut.aspx")
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim odma As New dedicacionMediaAsignatura
        Dim resultado As Single = odma.obtenerDedicacionMediaAsignatura(TextBox1.Text)
        Label6.Text = resultado.ToString
    End Sub
End Class