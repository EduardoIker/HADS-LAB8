<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="TareasProfesor.aspx.vb" Inherits="Laboratorio2.TareasProfesor" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="estilo_main.css" rel="stylesheet" type="text/css" />
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Tareas Profesor</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Panel ID="Panel1" runat="server" BackColor="#CCCCCC" Height="79px">
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:LinkButton ID="LinkButton1" runat="server">Cerrar Sesión</asp:LinkButton>
            <ajaxToolkit:ConfirmButtonExtender ID="LinkButton1_ConfirmButtonExtender" runat="server" TargetControlID="LinkButton1" 
            ConfirmText="¿Estas seguro de que quieres cerrar sesion?"/>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="Label1" runat="server" Text="Profesor" Font-Bold="True" Font-Size="XX-Large"></asp:Label>
            <br />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="XX-Large" Text="Gestión de Tareas Genéricas"></asp:Label>
        </asp:Panel>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <br />
                <asp:Label ID="Label3" runat="server" Font-Bold="True" Text="Seleccionar asignatura:"></asp:Label>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" DataSourceID="SqlDataSource1" DataTextField="codigoasig" DataValueField="codigoasig" Height="31px" Width="147px">
                            <asp:ListItem></asp:ListItem>
                        </asp:DropDownList>
                    </ContentTemplate>
        </asp:UpdatePanel>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:HADS17_TAREASConnectionString %>" SelectCommand="getAsignaturasProfesor" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:SessionParameter Name="profesor" SessionField="correo" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:Button ID="Button1" runat="server" Height="38px" PostBackUrl="~/Profesor/InsertarTarea.aspx" Text="Insertar Nueva Tarea" Width="140px" />
                <br />
        <br />
        <div id="up_container">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="Codigo" DataSourceID="SqlDataSource2" ForeColor="#333333" GridLines="None">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:CommandField ShowEditButton="True" />
                        <asp:BoundField DataField="Codigo" HeaderText="Codigo" ReadOnly="True" SortExpression="Codigo" />
                        <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" SortExpression="Descripcion" />
                        <asp:BoundField DataField="CodAsig" HeaderText="CodAsig" SortExpression="CodAsig" />
                        <asp:BoundField DataField="HEstimadas" HeaderText="HEstimadas" SortExpression="HEstimadas" />
                        <asp:CheckBoxField DataField="Explotacion" HeaderText="Explotacion" SortExpression="Explotacion" />
                        <asp:BoundField DataField="TipoTarea" HeaderText="TipoTarea" SortExpression="TipoTarea" />
                    </Columns>
                    <EditRowStyle BackColor="#2461BF" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EFF3FB" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                </asp:GridView>
                <ajaxToolkit:DragPanelExtender ID="GridView1_DragPanelExtender" runat="server" BehaviorID="GridView1_DragPanelExtender" DragHandleID="GridView1" TargetControlID="GridView1" />
            </ContentTemplate>
        </asp:UpdatePanel>
            </div>
        <ajaxToolkit:UpdatePanelAnimationExtender ID="UpdatePanel1_UpdatePanelAnimationExtender" runat="server" BehaviorID="animation" TargetControlID="UpdatePanel1">
            <Animations>
                <OnUpdating>
                    <Sequence>
                        <%-- Store the original height of the panel --%>
                        <ScriptAction Script="var b = $find('animation'); b._originalHeight = b._element.offsetHeight;" />
                        
                        <StyleAction Attribute="overflow" Value="hidden" />
                        
                        <%-- Do each of the selected effects --%>
                        <Parallel duration=".80" Fps="30">   
                                <FadeOut AnimationTarget="up_container" minimumOpacity=".2" />
                                <Resize Height="0" />
                        </Parallel>
                    </Sequence>
                </OnUpdating>
                <OnUpdated>
                    <Sequence>
                        <%-- Do each of the selected effects --%>
                        <Parallel duration=".80" Fps="30">
                                <FadeIn AnimationTarget="up_container" minimumOpacity=".2" />
                                <%-- Get the stored height --%>
                                <Resize HeightScript="$find('animation')._originalHeight" />
                        </Parallel>           
                    </Sequence>
                </OnUpdated>
          </Animations>
        </ajaxToolkit:UpdatePanelAnimationExtender>
        <br /> 
    </div>
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:HADS17_TAREASConnectionString %>" SelectCommand="getTareas" SelectCommandType="StoredProcedure" UpdateCommand="UPDATE TareasGenericas SET Descripcion = @Descripcion, CodAsig = @CodAsig, HEstimadas = @HEstimadas, Explotacion = @Explotacion, TipoTarea = @TipoTarea WHERE (Codigo = @Codigo)">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="DropDownList1" Name="asignatura" PropertyName="SelectedValue" Type="String" />
                    </SelectParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="Descripcion" />
                        <asp:Parameter Name="CodAsig" />
                        <asp:Parameter Name="HEstimadas" />
                        <asp:Parameter Name="Explotacion" />
                        <asp:Parameter Name="TipoTarea" />
                        <asp:Parameter Name="Codigo" />
                    </UpdateParameters>
                </asp:SqlDataSource>
        <br />
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Profesor/Profesor.aspx">Volver al menu del profesor</asp:HyperLink>
    </form>
</body>
</html>