Public Class Profesor
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    
    Protected Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim usuariosConectados As ArrayList
        usuariosConectados = Application.Contents("listaUsuarios")
        ListBox1.Items.Clear()
        ListBox2.Items.Clear()
        For Each User As String In usuariosConectados
            If User.Contains("ikasle") Then
                ListBox1.Items.Add(User)
            Else
                ListBox2.Items.Add(User)
            End If
        Next
        Label7.Text = ListBox1.Items.Count
        Label10.Text = ListBox2.Items.Count
    End Sub
End Class