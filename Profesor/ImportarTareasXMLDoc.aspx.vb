Imports System.Data.SqlClient
Imports System.IO
Imports System.Xml

Public Class WebForm9
    Inherits System.Web.UI.Page

    Dim conClsf As SqlConnection = New SqlConnection("Data Source=tcp:hads2017.database.windows.net,1433;Initial Catalog=HADS17_TAREAS;Persist Security Info=True;User ID=hads17;Password=Camellos17")
    Dim dap1 As New SqlDataAdapter
    Dim dap2 As New SqlDataAdapter
    Dim dst As New DataSet
    Dim tbl As New DataTable
    Dim row As DataRow

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack Then
            dst = Session("datos")
            dap2 = Session("adaptador")
        Else
            'Para coger las asignaturas
            dap1 = New SqlDataAdapter("select distinct codigoasig from GruposClase where codigo in (select codigogrupo from ProfesoresGrupo where email='" + Session("correo") + "')", conClsf)
            Dim bld As New SqlCommandBuilder(dap1)
            dap1.Fill(dst, "Asignaturas")
            tbl = dst.Tables("Asignaturas")
            DropDownList1.DataSource = tbl
            DropDownList1.DataTextField = "codigoasig" 'Nombre de la columna de la tabla
            DropDownList1.DataBind()
            'Obtener las tareas y almacenarlas en otra DataTable del DataSet
            dap2 = New SqlDataAdapter("select * from TareasGenericas", conClsf)
            Dim bld2 As New SqlCommandBuilder(dap2)
            dap2.Fill(dst, "Tareas")
            Session("adaptador") = dap2
            Session("datos") = dst
            'Mostrar las tareas en el control XML a traves de una transformación XSLT
            Dim fileLoc = "../App_Data/" & DropDownList1.SelectedValue & ".xml"
            If File.Exists(Server.MapPath(fileLoc)) Then
                Xml1.DocumentSource = Server.MapPath(fileLoc)
                Xml1.TransformSource = Server.MapPath("../App_Data/XSLTFileCod.xsl")
            Else
                Label5.Text = "Ha ocurrido un error en la lectura del XML"
                Label5.ForeColor = Drawing.Color.Red
            End If
        End If
    End Sub

    Protected Sub DropDownList1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList1.SelectedIndexChanged
        'Mostrar las tareas en el control XML a traves de una transformación XSLT de la nueva asignatura seleccionada
        Dim fileLoc = "../App_Data/" & DropDownList1.SelectedValue & ".xml"
        Label5.Text = "Lista de tareas de la asignatura seleccionada:"
        Label5.ForeColor = Drawing.Color.Black
        If File.Exists(Server.MapPath(fileLoc)) Then
            Xml1.DocumentSource = Server.MapPath(fileLoc)
            Xml1.TransformSource = Server.MapPath("../App_Data/XSLTFileCod.xsl")
        Else
            Label5.Text = "Ha ocurrido un error en la lectura del XML"
            Label5.ForeColor = Drawing.Color.Red
        End If
        Label6.Text = ""
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim xd As New XmlDocument
        Dim fileLoc = "../App_Data/" & DropDownList1.SelectedValue & ".xml"
        If File.Exists(Server.MapPath(fileLoc)) Then
            xd.Load(Server.MapPath(fileLoc))
            Dim LasTareas As XmlNodeList
            LasTareas = xd.GetElementsByTagName("tarea")
            Dim i As Integer
            Dim tbl As New DataTable
            tbl = Session("datos").Tables("Tareas")
            For i = 0 To LasTareas.Count - 1
                Dim dr As DataRow = tbl.NewRow()
                dr("Codigo") = LasTareas(i).ChildNodes(0).ChildNodes(0).Value
                dr("Descripcion") = LasTareas(i).ChildNodes(1).ChildNodes(0).Value
                dr("CodAsig") = DropDownList1.SelectedValue
                dr("HEstimadas") = CInt(LasTareas(i).ChildNodes(2).ChildNodes(0).Value)
                dr("Explotacion") = LasTareas(i).ChildNodes(3).ChildNodes(0).Value
                dr("TipoTarea") = LasTareas(i).ChildNodes(4).ChildNodes(0).Value
                tbl.Rows.Add(dr)
            Next
            'Guardar los datos en la BD
            dap2 = Session("adaptador")
            Try
                dap2.Update(dst, "Tareas")
                dst.AcceptChanges()
                Label6.ForeColor = Drawing.Color.Black
                Label6.Text = "Tareas guardadas correctamente en la BD"
            Catch ex As Exception
                Label6.ForeColor = Drawing.Color.Red
                Label6.Text = "Error. Tareas ya importadas"
            End Try
        Else
            Label6.Text = "Ha ocurrido un error al cargar el fichero XML"
        End If

    End Sub

    Protected Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles LinkButton1.Click
        Response.Redirect("../LogOut.aspx")
    End Sub

    Protected Sub LinkButton2_Click(sender As Object, e As EventArgs) Handles LinkButton2.Click
        Dim fileLoc = "../App_Data/" & DropDownList1.SelectedValue & ".xml"
        If File.Exists(Server.MapPath(fileLoc)) Then
            Xml1.DocumentSource = Server.MapPath(fileLoc)
            Xml1.TransformSource = Server.MapPath("../App_Data/XSLTFileCod.xsl")
        Else
            Label5.Text = "Ha ocurrido un error en la lectura del XML"
            Label5.ForeColor = Drawing.Color.Red
        End If
    End Sub


    Protected Sub LinkButton3_Click(sender As Object, e As EventArgs) Handles LinkButton3.Click
        Dim fileLoc = "../App_Data/" & DropDownList1.SelectedValue & ".xml"
        If File.Exists(Server.MapPath(fileLoc)) Then
            Xml1.DocumentSource = Server.MapPath(fileLoc)
            Xml1.TransformSource = Server.MapPath("../App_Data/XSLTFileDes.xsl")
        Else
            Label5.Text = "Ha ocurrido un error en la lectura del XML"
            Label5.ForeColor = Drawing.Color.Red
        End If
    End Sub

    Protected Sub LinkButton4_Click(sender As Object, e As EventArgs) Handles LinkButton4.Click
        Dim fileLoc = "../App_Data/" & DropDownList1.SelectedValue & ".xml"
        If File.Exists(Server.MapPath(fileLoc)) Then
            Xml1.DocumentSource = Server.MapPath(fileLoc)
            Xml1.TransformSource = Server.MapPath("../App_Data/XSLTFileHEst.xsl")
        Else
            Label5.Text = "Ha ocurrido un error en la lectura del XML"
            Label5.ForeColor = Drawing.Color.Red
        End If
    End Sub
End Class