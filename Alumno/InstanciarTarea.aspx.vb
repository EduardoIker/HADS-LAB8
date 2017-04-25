Imports System.Data.SqlClient

Public Class InstanciarTarea
    Inherits System.Web.UI.Page

    Dim conClsf As SqlConnection = New SqlConnection("Data Source=tcp:hads2017.database.windows.net,1433;Initial Catalog=HADS17_TAREAS;Persist Security Info=True;User ID=hads17;Password=Camellos17")
    Dim dap As New SqlDataAdapter
    Dim dst As New DataSet
    Dim tbl As New DataTable
    Dim rowTarAl As DataRow
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack Then
            dst = Session("datos")
        Else
            'Rellenar los campos no editables
            TextBox1.Text = Session("correo")
            TextBox2.Text = Request.QueryString("codigo")
            TextBox3.Text = Request.QueryString("hestimadas")
            'Para coger las tareas del alumno (instanciadas)
            dap = New SqlDataAdapter("select * from EstudiantesTareas where Email='" + Session("correo") + "'", conClsf)
            Dim bld As New SqlCommandBuilder(dap)
            dap.Fill(dst, "TareasAlumno")
            tbl = dst.Tables("TareasAlumno")
            GridView1.DataSource = tbl
            GridView1.DataBind()
            Session("datos") = dst
            Session("adaptador") = dap
        End If
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            'Crear una nueva fila en DataTable y visualizarla
            tbl = dst.Tables("TareasAlumno")
            Dim rowTarAl As DataRow = tbl.NewRow()
            rowTarAl("Email") = Session("correo")
            rowTarAl("CodTarea") = TextBox2.Text
            rowTarAl("HEstimadas") = TextBox3.Text
            rowTarAl("HReales") = TextBox4.Text
            tbl.Rows.Add(rowTarAl)
            GridView1.DataSource = tbl
            GridView1.DataBind()
            'Guardar los cambios en la BD
            dap = Session("adaptador")
            dap.Update(dst, "TareasAlumno")
            dst.AcceptChanges()
            Label4.Text = "Instanciada la tarea " & TextBox2.Text & " con " & TextBox4.Text & " hora(s)"
            'Deshabilitar el botón
            Button1.Enabled = False
        Catch ex As Exception
            Label4.Text = ex.Message
        End Try
    End Sub


    Protected Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles LinkButton1.Click
        Response.Redirect("../LogOut.aspx")
    End Sub
End Class