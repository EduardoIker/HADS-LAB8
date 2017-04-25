<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Estadisticas.aspx.vb" Inherits="Laboratorio2.Estadisticas" %>

<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="estilo_main.css" rel="stylesheet" type="text/css" />
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Estadisticas</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div>
    
        <asp:Panel ID="Panel1" runat="server" BackColor="Silver" Height="61px">
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:LinkButton ID="LinkButton1" runat="server">Cerrar Sesión</asp:LinkButton>
            <ajaxToolkit:ConfirmButtonExtender ID="LinkButton1_ConfirmButtonExtender" runat="server" TargetControlID="LinkButton1" 
            ConfirmText="¿Estas seguro de que quieres cerrar sesion?"/>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="Label1" runat="server" Text="ESTADISTICAS" Font-Bold="True" Font-Size="XX-Large"></asp:Label>
        </asp:Panel>
    
    </div>
        <br />
        <asp:Label ID="Label2" runat="server" Font-Bold="True" Text="Selecciona una asignatura:"></asp:Label>
        <br />
        <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="SqlDataSource1" DataTextField="codigoasig" DataValueField="codigoasig" Height="19px" Width="164px" AutoPostBack="True">
        </asp:DropDownList>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:HADS17_TAREASConnectionString %>" SelectCommand="getAsignaturasProfesor" SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:SessionParameter Name="profesor" SessionField="correo" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
        <br />
        <br />
        <asp:Label ID="Label4" runat="server" Font-Bold="True" Text="Selecciona un alumno:"></asp:Label>
        <br />
        <asp:ListBox ID="ListBox1" runat="server" DataSourceID="SqlDataSource3" DataTextField="Email" DataValueField="Email" Height="85px" Width="170px" AutoPostBack="True"></asp:ListBox>
        <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:HADS17_TAREASConnectionString %>" SelectCommand="getAlumnosDeGrupo" SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:SessionParameter Name="correo" SessionField="correo" Type="String" />
                <asp:ControlParameter ControlID="DropDownList1" Name="asignatura" PropertyName="SelectedValue" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
        <br />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <br />
        <asp:Chart ID="Chart1" runat="server" DataSourceID="SqlDataSource6" AlternateText="Este alumno no ha instanciado aún ninguna tarea" Height="323px">
            <series>
                <asp:Series ChartType="StackedColumn" Name="Series1" YValuesPerPoint="6" XValueMember="CodTarea" YValueMembers="HReales" IsValueShownAsLabel="True">
                </asp:Series>
            </series>
            <chartareas>
                <asp:ChartArea Name="ChartArea1">
                </asp:ChartArea>
            </chartareas>
        </asp:Chart>
        <asp:Chart ID="Chart2" runat="server" DataSourceID="SqlDataSource6" Height="323px" style="z-index: 1; left: 315px; top: 382px; position: absolute; height: 323px; width: 300px">
            <Series>
                <asp:Series Name="Series1" ChartType="StackedColumn" Color="GreenYellow" IsValueShownAsLabel="True" XValueMember="CodTarea" YValueMembers="HEstimadas">
                </asp:Series>
            </Series>
            <ChartAreas>
                <asp:ChartArea Name="ChartArea1">
                </asp:ChartArea>
            </ChartAreas>
        </asp:Chart>
        <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [Dedicación real / Tarea]&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; [Dedicación Estimada / Tarea]<br />
        <asp:SqlDataSource ID="SqlDataSource6" runat="server" ConnectionString="<%$ ConnectionStrings:HADS17_TAREASConnectionString %>" SelectCommand="getDedicacionTareas" SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:ControlParameter ControlID="DropDownList1" Name="codasig" PropertyName="SelectedValue" Type="String" />
                <asp:ControlParameter ControlID="ListBox1" Name="email" PropertyName="SelectedValue" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
        <br />
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Profesor/Profesor.aspx">Volver al Menu Profesor</asp:HyperLink>
        <br />
    </form>
</body>
</html>
