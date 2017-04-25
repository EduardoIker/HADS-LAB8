﻿'------------------------------------------------------------------------------
' <auto-generated>
'     Este código fue generado por una herramienta.
'     Versión de runtime:4.0.30319.42000
'
'     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
'     se vuelve a generar el código.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict Off
Option Explicit On

Imports System
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Xml.Serialization

'
'Microsoft.VSDesigner generó automáticamente este código fuente, versión=4.0.30319.42000.
'
Namespace captcha1
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1087.0"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code"),  _
     System.Web.Services.WebServiceBindingAttribute(Name:="CaptchaServiceSoap", [Namespace]:="http://tempuri.org/")>  _
    Partial Public Class CaptchaService
        Inherits System.Web.Services.Protocols.SoapHttpClientProtocol
        
        Private GenerateOperationCompleted As System.Threading.SendOrPostCallback
        
        Private ValidateOperationCompleted As System.Threading.SendOrPostCallback
        
        Private useDefaultCredentialsSetExplicitly As Boolean
        
        '''<remarks/>
        Public Sub New()
            MyBase.New
            Me.Url = Global.Laboratorio2.My.MySettings.Default.Laboratorio2_captcha1_CaptchaService
            If (Me.IsLocalFileSystemWebService(Me.Url) = true) Then
                Me.UseDefaultCredentials = true
                Me.useDefaultCredentialsSetExplicitly = false
            Else
                Me.useDefaultCredentialsSetExplicitly = true
            End If
        End Sub
        
        Public Shadows Property Url() As String
            Get
                Return MyBase.Url
            End Get
            Set
                If (((Me.IsLocalFileSystemWebService(MyBase.Url) = true)  _
                            AndAlso (Me.useDefaultCredentialsSetExplicitly = false))  _
                            AndAlso (Me.IsLocalFileSystemWebService(value) = false)) Then
                    MyBase.UseDefaultCredentials = false
                End If
                MyBase.Url = value
            End Set
        End Property
        
        Public Shadows Property UseDefaultCredentials() As Boolean
            Get
                Return MyBase.UseDefaultCredentials
            End Get
            Set
                MyBase.UseDefaultCredentials = value
                Me.useDefaultCredentialsSetExplicitly = true
            End Set
        End Property
        
        '''<remarks/>
        Public Event GenerateCompleted As GenerateCompletedEventHandler
        
        '''<remarks/>
        Public Event ValidateCompleted As ValidateCompletedEventHandler
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/Generate", RequestNamespace:="http://tempuri.org/", ResponseNamespace:="http://tempuri.org/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function Generate() As GenerateCaptcha
            Dim results() As Object = Me.Invoke("Generate", New Object(-1) {})
            Return CType(results(0),GenerateCaptcha)
        End Function
        
        '''<remarks/>
        Public Overloads Sub GenerateAsync()
            Me.GenerateAsync(Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub GenerateAsync(ByVal userState As Object)
            If (Me.GenerateOperationCompleted Is Nothing) Then
                Me.GenerateOperationCompleted = AddressOf Me.OnGenerateOperationCompleted
            End If
            Me.InvokeAsync("Generate", New Object(-1) {}, Me.GenerateOperationCompleted, userState)
        End Sub
        
        Private Sub OnGenerateOperationCompleted(ByVal arg As Object)
            If (Not (Me.GenerateCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent GenerateCompleted(Me, New GenerateCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/Validate", RequestNamespace:="http://tempuri.org/", ResponseNamespace:="http://tempuri.org/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function Validate(ByVal tokan As String, ByVal inputtext As String) As Boolean
            Dim results() As Object = Me.Invoke("Validate", New Object() {tokan, inputtext})
            Return CType(results(0),Boolean)
        End Function
        
        '''<remarks/>
        Public Overloads Sub ValidateAsync(ByVal tokan As String, ByVal inputtext As String)
            Me.ValidateAsync(tokan, inputtext, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub ValidateAsync(ByVal tokan As String, ByVal inputtext As String, ByVal userState As Object)
            If (Me.ValidateOperationCompleted Is Nothing) Then
                Me.ValidateOperationCompleted = AddressOf Me.OnValidateOperationCompleted
            End If
            Me.InvokeAsync("Validate", New Object() {tokan, inputtext}, Me.ValidateOperationCompleted, userState)
        End Sub
        
        Private Sub OnValidateOperationCompleted(ByVal arg As Object)
            If (Not (Me.ValidateCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent ValidateCompleted(Me, New ValidateCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        Public Shadows Sub CancelAsync(ByVal userState As Object)
            MyBase.CancelAsync(userState)
        End Sub
        
        Private Function IsLocalFileSystemWebService(ByVal url As String) As Boolean
            If ((url Is Nothing)  _
                        OrElse (url Is String.Empty)) Then
                Return false
            End If
            Dim wsUri As System.Uri = New System.Uri(url)
            If ((wsUri.Port >= 1024)  _
                        AndAlso (String.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) = 0)) Then
                Return true
            End If
            Return false
        End Function
    End Class
    
    '''<comentarios/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1087.0"),  _
     System.SerializableAttribute(),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code"),  _
     System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://tempuri.org/")>  _
    Partial Public Class GenerateCaptcha
        
        Private tokanField As String
        
        Private imageUrlField As String
        
        '''<comentarios/>
        Public Property Tokan() As String
            Get
                Return Me.tokanField
            End Get
            Set
                Me.tokanField = value
            End Set
        End Property
        
        '''<comentarios/>
        Public Property ImageUrl() As String
            Get
                Return Me.imageUrlField
            End Get
            Set
                Me.imageUrlField = value
            End Set
        End Property
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1087.0")>  _
    Public Delegate Sub GenerateCompletedEventHandler(ByVal sender As Object, ByVal e As GenerateCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1087.0"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class GenerateCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs
        
        Private results() As Object
        
        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub
        
        '''<remarks/>
        Public ReadOnly Property Result() As GenerateCaptcha
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(0),GenerateCaptcha)
            End Get
        End Property
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1087.0")>  _
    Public Delegate Sub ValidateCompletedEventHandler(ByVal sender As Object, ByVal e As ValidateCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1087.0"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class ValidateCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs
        
        Private results() As Object
        
        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub
        
        '''<remarks/>
        Public ReadOnly Property Result() As Boolean
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(0),Boolean)
            End Get
        End Property
    End Class
End Namespace