<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Registro.aspx.vb" Inherits="Laboratorio2.WebForm2" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="estilo_main.css" rel="stylesheet" type="text/css" />
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Registro</title>
  
<style type="text/css">
      .VeryPoor
       {
         background-color:red;
         }
          .Weak
        {
         background-color:orange;
         }
          .Average
         {
          background-color: yellow;   
         }
          .Good
         {
         background-color:#b4fa06;
         }
          .Strong
         {
         background-color:#10b40b;
         }
          .border
         {
          border: medium solid #800000;
          width:200px;                
         }
      </style>

</head>
<body>
    <form id="form1" runat="server">
     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div>
    
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="Label1" runat="server" Font-Overline="False" Font-Size="XX-Large" Text="Registro"></asp:Label>
        <br />
        <br />
        &nbsp;&nbsp;&nbsp;&nbsp;
        <br />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                Correo:
                <asp:TextBox ID="TextBox2" runat="server" AutoPostBack="True"></asp:TextBox>
                <asp:Image ID="Image1" runat="server" BorderStyle="None" Height="31px" ImageUrl="~/Images/ok.png" Visible="False" Width="38px" />
                <asp:Image ID="Image2" runat="server" BorderStyle="None" Height="30px" ImageUrl="~/Images/nook.png" Width="37px" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox2" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TextBox2" ErrorMessage="Introduce un correo válido" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
            </ContentTemplate>
        </asp:UpdatePanel>
        <br />
        Nombre:&nbsp;
        <asp:TextBox ID="TextBox9" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="TextBox9" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
        <br />
        <br />
        Apellido:&nbsp;
        <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBox3" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
        <br />
        <br />
        DNI:
        <asp:TextBox ID="TextBox4" runat="server" ToolTip="DNI con formato 12345678X"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TextBox4" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="TextBox4" ErrorMessage="Introduce un DNI válido (12345678)" ForeColor="Red" ValidationExpression="^\d{8}$"></asp:RegularExpressionValidator>
        <br />
        <br />
        Tipo Cuenta:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:RadioButton ID="RadioButton1" runat="server" GroupName="tipo" Text="Profesor" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:RadioButton ID="RadioButton2" runat="server" Checked="True" GroupName="tipo" Text="Alumno" />
        <br />
        <br />
        Password:
        <asp:TextBox ID="TextBox5" runat="server" TextMode="Password"></asp:TextBox>
        <ajaxToolkit:PasswordStrength ID="TextBox5_PasswordStrength" runat="server"  TargetControlID="TextBox5" StrengthIndicatorType="BarIndicator"
        RequiresUpperAndLowerCaseCharacters="true" MinimumLowerCaseCharacters="1"
        MinimumUpperCaseCharacters="1" MinimumSymbolCharacters="1"
        MinimumNumericCharacters="1" PreferredPasswordLength="8"
        BarBorderCssClass="border" TextStrengthDescriptionStyles="VeryPoor; Weak;
        Average;Good;Strong"  HelpStatusLabelID="Label3"/>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="TextBox5" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
        <br />
        <asp:Label ID="Label3" runat="server" Font-Bold="true" Font-Size="Medium" ForeColor="Brown"></asp:Label>
        <br />
        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="TextBox5" ErrorMessage="La contraseña debe tener al menos 1 minuscula, 1 mayuscula, 1 digito y entre 6-8 caracteres" ForeColor="Red" ValidationExpression="^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,8}$"></asp:RegularExpressionValidator>
        <br />
        <br />
        Repite Password:
        <asp:TextBox ID="TextBox6" runat="server" TextMode="Password"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="TextBox6" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="TextBox5" ControlToValidate="TextBox6" ErrorMessage="Las contraseñas deben coincidir" ForeColor="Red"></asp:CompareValidator>
        <br />
        <br />
        Pregunta secreta:&nbsp;
        <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="TextBox7" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
        <br />
        <br />
        Respuesta a la pregunta secreta: <asp:TextBox ID="TextBox8" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="TextBox8" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
        <br />
        <br />
        Captcha:<asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <asp:Image ID="Image3" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:TextBox ID="TextBox10" runat="server" Width="141px"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="TextBox10" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
        <br />
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <asp:Label ID="Label2" runat="server"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br /> &nbsp;<asp:Button ID="Button1" runat="server" Enabled="False" Text="Enviar" />
            </ContentTemplate>
        </asp:UpdatePanel>
        <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Inicio.aspx">Volver a la página de Inicio</asp:HyperLink>
    
        <br />
        <br />
    
    </div>
    </form>
</body>
</html>
