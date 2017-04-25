Imports AccesoDatos.accesoDatosSQL

Public Class WebForm3
    Inherits System.Web.UI.Page

    Private Shared correo As String
    Private Shared respuestaSeguridad As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        conectar()
    End Sub

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        cerrarConexion()
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim resultado As Integer
        correo = TextBox1.Text
        Dim respuesta(2) As String
        resultado = comprobarCorreo(correo)
        If (resultado) Then
            respuesta = obtenerPreguntaRespuesta(correo)
            If IsNothing(respuesta) Then
                Label7.Text = "El correo no existe o ha ocurrido un error inesperado"
                Exit Sub
            End If
            Label2.Text = respuesta(1)
            respuestaSeguridad = respuesta(2)
            'Poner visible la siguiente sección de la página
            Label1.Visible = True
            Label2.Visible = True
            Label3.Visible = True
            TextBox2.Visible = True
            Button2.Visible = True
        Else
            Label7.Text = "El correo no existe o ha ocurrido un error inesperado"
        End If
    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If (TextBox2.Text.Equals(respuestaSeguridad)) Then
            Label4.Visible = True
            Label5.Visible = True
            TextBox3.Visible = True
            TextBox4.Visible = True
            Button3.Visible = True
        Else
            Label8.Text = "Respuesta incorrecta"
        End If
    End Sub

    Protected Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim password As String
        Dim passwordrepetido As String
        Dim resultado As Integer
        password = TextBox3.Text
        passwordrepetido = TextBox4.Text
        If (password.Equals(passwordrepetido)) Then
            Dim sha1 As New sha1.sha1
            Dim hassedPassword = sha1.getSHA1Hash(password)
            resultado = guardarPassword(hassedPassword, correo)
            If (resultado = 0) Then
                Label9.Text = "Contraseña cambiada correctamente"
            Else
                Label9.Text = "Error al cambiar la contraseña. Intentalo de nuevo."
            End If
        End If
    End Sub
End Class