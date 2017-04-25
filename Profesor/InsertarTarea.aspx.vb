Imports System.Data.SqlClient

Public Class InsertarTarea
    Inherits System.Web.UI.Page

    Dim conClsf As SqlConnection = New SqlConnection("Data Source=tcp:hads2017.database.windows.net,1433;Initial Catalog=HADS17_TAREAS;Persist Security Info=True;User ID=hads17;Password=Camellos17")
    Dim dap As New SqlDataAdapter
    Dim dst As New DataSet
    Dim tbl As New DataTable
    Dim rowTar As DataRow
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack Then
            dst = Session("datos")
        Else
            'Para coger las tareas del alumno (instanciadas)
            dap = New SqlDataAdapter("select * from TareasGenericas", conClsf)
            Dim bld As New SqlCommandBuilder(dap)
            dap.Fill(dst, "Tareas")
            tbl = dst.Tables("TareasAlumno")
            Session("datos") = dst
            Session("adaptador") = dap
        End If
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            'Crear una nueva fila en DataTable y visualizarla
            tbl = dst.Tables("Tareas")
            Dim rowTar As DataRow = tbl.NewRow()
            rowTar("Codigo") = TextBox1.Text
            rowTar("Descripcion") = TextBox2.Text
            rowTar("CodAsig") = DropDownList1.SelectedValue()
            rowTar("HEstimadas") = TextBox3.Text
            rowTar("Explotacion") = True
            rowTar("TipoTarea") = DropDownList2.SelectedValue()
            tbl.Rows.Add(rowTar)
            'Guardar los cambios en la BD
            dap = Session("adaptador")
            dap.Update(dst, "Tareas")
            dst.AcceptChanges()
            Label8.Text = "Creada la tarea " & TextBox1.Text & " de tipo " & DropDownList2.SelectedValue() & " para la asignatura" & DropDownList1.SelectedValue()

        Catch ex As Exception
            Label8.Text = ex.Message
        End Try
    End Sub

    Protected Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles LinkButton1.Click
        Response.Redirect("../LogOut.aspx")
    End Sub
End Class