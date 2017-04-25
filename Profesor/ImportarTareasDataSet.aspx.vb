Imports System.IO
Imports System.Data.SqlClient

Public Class ImportarTareasDataSet
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
            Dim bld1 As New SqlCommandBuilder(dap1)
            dap1.Fill(dst, "Asignaturas")
            tbl = dst.Tables("Asignaturas")
            DropDownList1.DataSource = tbl
            DropDownList1.DataTextField = "codigoasig" 'Nombre de la columna de la tabla
            DropDownList1.DataBind()
            'Obtener las tareas y almacenarlas en otra DataTable del DataSet
            dap2 = New SqlDataAdapter("select * from TareasGenericas", conClsf)
            Dim bld2 As New SqlCommandBuilder(dap2)
            Session("adaptador") = dap2
            Session("datos") = dst
            'Mostrar las tareas en el control XML a traves de una transformación XSLT
            Dim fileLoc = "../App_Data/" & DropDownList1.SelectedValue & ".xml"
            If File.Exists(Server.MapPath(fileLoc)) Then
                Xml1.DocumentSource = Server.MapPath(fileLoc)
                Xml1.TransformSource = Server.MapPath("../App_Data/XSLTFile.xsl")
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
            Xml1.TransformSource = Server.MapPath("../App_Data/XSLTFile.xsl")
        Else
            Label5.Text = "Ha ocurrido un error en la lectura del XML"
            Label5.ForeColor = Drawing.Color.Red
        End If
        Label6.Text = ""
    End Sub

    Protected Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles LinkButton1.Click
        Response.Redirect("../LogOut.aspx")
    End Sub

    
    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim dstXML As New DataSet
        'Cargar datos del XML
        Dim fileLoc = "../App_Data/" & DropDownList1.SelectedValue & ".xml"
        dstXML.ReadXml(Server.MapPath(fileLoc))
        'Obtener la DataTable
        Dim tblXML As New DataTable
        tblXML = dstXML.Tables(0)
        'Dar "formato" a la tabla para que se ajuste a la de la BD
        tblXML.TableName = "TareasXML"
        tblXML.Columns(0).ColumnName = "Codigo"
        tblXML.Columns(1).ColumnName = "Descripcion"
        tblXML.Columns(2).ColumnName = "HEstimadas"
        tblXML.Columns(3).ColumnName = "Explotacion"
        tblXML.Columns(4).ColumnName = "TipoTarea"
        'Añadir columna CodAsig
        Dim columna As New DataColumn()
        columna.ColumnName = "CodAsig"
        columna.DefaultValue = DropDownList1.SelectedValue
        tblXML.Columns.Add(columna)
        'Guardar las tareas en la BD
        Try
            dap2.Update(dstXML, "TareasXML")
            dstXML.AcceptChanges()
            Label6.ForeColor = Drawing.Color.Black
            Label6.Text = "Tareas guardadas correctamente en la BD"
        Catch ex As SqlException
            Label6.ForeColor = Drawing.Color.Red
            Label6.Text = "Error. Existen tareas ya importadas"
        Catch ex As Exception
            Label6.ForeColor = Drawing.Color.Red
            Label6.Text = "Ha ocurrido un error inesperado"
        End Try
    End Sub
End Class