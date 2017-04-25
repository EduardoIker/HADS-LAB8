Imports AccesoDatos.accesoDatosSQL
Imports System.Security

Public Class WebForm2
    Inherits System.Web.UI.Page
    Dim gc As captcha.CaptchaService
    Dim Tokan As String


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        conectar()
        If Not Page.IsPostBack Then 'Si la página NO ESTA DE VUELTA
            gc = New captcha.CaptchaService 'Crear instancia del servicio
            Dim theCaptcha As captcha.GenerateCaptcha = gc.Generate() 'Generar el captcha
            Image3.ImageUrl = theCaptcha.ImageUrl 'Mostrar la imagen del captcha
            Tokan = theCaptcha.Tokan 'Almacenar el Token
            Application.Contents("tokan") = Tokan 'Guardar el token en Application Contents
            Application.Contents("gc") = gc 'Guardar la instancia del servicio en Application Contents
        Else
            Tokan = Application.Contents("tokan") 'Recuperar el token y la instancia del servicio
            gc = Application.Contents("gc")
        End If
    End Sub

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        cerrarConexion()
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim nombre As String
        Dim apellidos As String
        Dim dni As String
        Dim password As String
        Dim pregunta As String
        Dim respuesta As String
        Dim correo As String
        Dim res As String
        Dim rol = "A"

        nombre = TextBox9.Text
        apellidos = TextBox3.Text
        password = TextBox5.Text
        ' Calcular el hash 
        Dim sha1 As New sha1.sha1
        Dim hassedPassword = sha1.getSHA1Hash(password)
        pregunta = TextBox7.Text
        correo = TextBox2.Text
        dni = TextBox4.Text
        respuesta = TextBox8.Text
        If (RadioButton1.Checked) Then
            rol = "P"
        End If
        If gc.Validate(Tokan, TextBox10.Text) Then 'Validar el Captcha
            res = insertar(correo, nombre, apellidos, pregunta, respuesta, dni, hassedPassword, rol)
            Label2.Text = res
        Else
            Label2.Text = "Error al solucionar el captcha. Inténtalo de nuevo."
            Dim captcha = gc.Generate() 'Regenerar el captcha en caso de fallo en el previo
            Image3.ImageUrl = captcha.ImageUrl 'Mostrar la nueva imagen del captcha
            Tokan = captcha.Tokan 'Almacenar el nuevo token (tanto en la variable como en Application)
            Application.Contents("tokan") = Tokan
        End If
    End Sub

    Protected Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        Dim cm As New comprobarMatricula.Matriculas
        Dim resultado = cm.comprobar(TextBox2.Text)
        If resultado.Equals("SI") Then
            Image1.Visible = True
            Image2.Visible = False
            Button1.Enabled = True
        Else
            Image1.Visible = False
            Image2.Visible = True
            Button1.Enabled = False
        End If
    End Sub
End Class