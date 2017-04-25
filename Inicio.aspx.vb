Imports AccesoDatos.accesoDatosSQL

Public Class WebForm1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        conectar()
        'System.Web.Security.FormsAuthentication.SignOut()
    End Sub

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        cerrarConexion()
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim resultado As Integer
        'Ciframos la contraseña para realizar la comparación con la obtenida de la BD
        Dim sha1 As New sha1.sha1
        Dim hassedPassword = sha1.getSHA1Hash(TextBox2.Text)
        resultado = login(TextBox1.Text, hassedPassword)
        If (resultado = 0) Then
            Session.Contents("correo") = TextBox1.Text
            Session.Contents("tipo") = "A"
            System.Web.Security.FormsAuthentication.SetAuthCookie("A", False)
            addUser(TextBox1.Text)
            Response.Redirect("Alumno/Alumno.aspx")
        ElseIf (resultado = 1) Then
            Session.Contents("correo") = TextBox1.Text
            Session.Contents("tipo") = "P"
            If TextBox1.Text.Equals("vadillo@ehu.es") Then
                System.Web.Security.FormsAuthentication.SetAuthCookie(TextBox1.Text, False)
            Else
                System.Web.Security.FormsAuthentication.SetAuthCookie("P", False)
            End If
            addUser(TextBox1.Text)
            Response.Redirect("Profesor/Profesor.aspx")
        ElseIf (resultado = 2) Then
            Session.Contents("correo") = TextBox1.Text
            Session.Contents("tipo") = "AD"
            System.Web.Security.FormsAuthentication.SetAuthCookie("AD", False)
            'addUser(TextBox1.Text) 'Al Administrador no lo añadimos
            Response.Redirect("Administrador/Administrador.aspx")
        ElseIf (resultado = -2) Then
            Label2.Text = "Datos de acceso incorrectos. Inténtalo de nuevo."
        Else
            Label2.Text = "Ha ocurrido un problema inesperado. Inténtalo de nuevo."
        End If
    End Sub

    Private Sub addUser(ByVal email As String)
        Application.Lock()
        Dim usuariosConectados As ArrayList
        usuariosConectados = Application.Contents("listaUsuarios")
        usuariosConectados.Add(email)
        Application.Contents("listaUsuarios") = usuariosConectados
        Application.UnLock()
    End Sub
End Class