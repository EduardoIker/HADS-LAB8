Imports System.Data.SqlClient

Public Class WebForm6
    Inherits System.Web.UI.Page

    Dim conClsf As SqlConnection = New SqlConnection("Data Source=tcp:hads2017.database.windows.net,1433;Initial Catalog=HADS17_TAREAS;Persist Security Info=True;User ID=hads17;Password=Camellos17")
    Dim dap As New SqlDataAdapter
    Dim dst As New DataSet
    Dim tbl As New DataTable
    Dim row As DataRow

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack Then
            dst = Session("datos")
        Else
            'Para coger las asignaturas
            dap = New SqlDataAdapter("select distinct codigoasig from EstudiantesGrupo as EG inner join GruposClase as GC on EG.Grupo=GC.codigo where Email='" + Session("correo") + "'", conClsf)
            Dim bld As New SqlCommandBuilder(dap)
            dap.Fill(dst, "Asignaturas")
            tbl = dst.Tables("Asignaturas")
            DropDownList1.DataSource = tbl
            DropDownList1.DataTextField = "codigoasig" 'Nombre de la columna de la tabla
            DropDownList1.DataBind()
            'Para coger las tareas de la asignatura por defecto
            Dim valor_defecto = DropDownList1.SelectedValue()
            dap = New SqlDataAdapter("select Codigo, Descripcion, HEstimadas, TipoTarea from TareasGenericas where CodAsig='" & valor_defecto & "' and Codigo not in (select distinct CodTarea from EstudiantesTareas where Email='" & Session("correo") & "')", conClsf)
            bld = New SqlCommandBuilder(dap)
            dap.Fill(dst, "Tareas")
            tbl = dst.Tables("Tareas")
            If tbl.Rows.Count = 0 Then
                Label5.Text = "No existen tareas para la asignatura"
            Else
                Label5.Text = ""
                GridView1.DataSource = tbl
                GridView1.DataBind()
            End If
            Session("datos") = dst
        End If
    End Sub

    Protected Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles LinkButton1.Click
        Response.Redirect("../LogOut.aspx")
    End Sub

    Protected Sub DropDownList1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList1.SelectedIndexChanged
        GridView1.DataSource = Nothing
        GridView1.DataBind()
        Dim valor_defecto = DropDownList1.SelectedValue()
        dap = New SqlDataAdapter("select Codigo, Descripcion, HEstimadas, TipoTarea from TareasGenericas where CodAsig='" & valor_defecto & "' and Codigo not in (select distinct CodTarea from EstudiantesTareas where Email='" & Session("correo") & "')", conClsf)
        Dim bld As New SqlCommandBuilder(dap)
        dst = New DataSet()
        dap.Fill(dst, "Tareas")
        tbl = New DataTable()
        tbl = dst.Tables("Tareas")
        Session("datos") = dst 'Volver a guardar el DataSet
        If tbl.Rows.Count = 0 Then
            Label5.Text = "No existen tareas para la asignatura"
        Else
            Label5.Text = ""
            GridView1.DataSource = tbl
            GridView1.DataBind()
        End If
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView1.SelectedIndexChanged
        Dim codigo = GridView1.Rows(GridView1.SelectedIndex).Cells(1).Text
        Dim horas_estimadas = GridView1.Rows(GridView1.SelectedIndex).Cells(3).Text
        Response.Redirect("InstanciarTarea.aspx?codigo=" & codigo & "&hestimadas=" & horas_estimadas)
    End Sub

    Protected Sub GridView1_Sorting(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
        tbl = New DataTable()
        tbl = Session("datos").Tables("Tareas")
        Dim vista As New DataView(tbl)
        vista.Sort = e.SortExpression
        GridView1.DataSource = vista
        GridView1.DataBind()
    End Sub
End Class