﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.18052
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On

Imports System

Namespace My.Resources
    
    'This class was auto-generated by the StronglyTypedResourceBuilder
    'class via a tool like ResGen or Visual Studio.
    'To add or remove a member, edit your .ResX file then rerun ResGen
    'with the /str option, or rebuild your VS project.
    '''<summary>
    '''  A strongly-typed resource class, for looking up localized strings, etc.
    '''</summary>
    <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0"),  _
     Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute()>  _
    Friend Class Resource
        
        Private Shared resourceMan As Global.System.Resources.ResourceManager
        
        Private Shared resourceCulture As Global.System.Globalization.CultureInfo
        
        <Global.System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")>  _
        Friend Sub New()
            MyBase.New
        End Sub
        
        '''<summary>
        '''  Returns the cached ResourceManager instance used by this class.
        '''</summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Friend Shared ReadOnly Property ResourceManager() As Global.System.Resources.ResourceManager
            Get
                If Object.ReferenceEquals(resourceMan, Nothing) Then
                    Dim temp As Global.System.Resources.ResourceManager = New Global.System.Resources.ResourceManager("GmailTaskbar.Resource", GetType(Resource).Assembly)
                    resourceMan = temp
                End If
                Return resourceMan
            End Get
        End Property
        
        '''<summary>
        '''  Overrides the current thread's CurrentUICulture property for all
        '''  resource lookups using this strongly typed resource class.
        '''</summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Friend Shared Property Culture() As Global.System.Globalization.CultureInfo
            Get
                Return resourceCulture
            End Get
            Set
                resourceCulture = value
            End Set
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to messaggi non letti.
        '''</summary>
        Friend Shared ReadOnly Property ballonUnreadedMsg() As String
            Get
                Return ResourceManager.GetString("ballonUnreadedMsg", resourceCulture)
            End Get
        End Property

        '''<summary>
        '''  Looks up a localized string similar to Devi scrivere il nome utente e la password.
        '''</summary>
        Friend Shared ReadOnly Property configError() As String
            Get
                Return ResourceManager.GetString("configError", resourceCulture)
            End Get
        End Property

        '''<summary>
        '''  Looks up a localized string similar to Si è verificato un errore nella connessione al server, l&apos;applicazione verrà chiusa..
        '''</summary>
        Friend Shared ReadOnly Property errorConnection() As String
            Get
                Return ResourceManager.GetString("errorConnection", resourceCulture)
            End Get
        End Property

        '''<summary>
        '''  Looks up a localized string similar to Si è verificato un errore e l&apos;applicazione verrà chiusa..
        '''</summary>
        Friend Shared ReadOnly Property errorGeneric() As String
            Get
                Return ResourceManager.GetString("errorGeneric", resourceCulture)
            End Get
        End Property

        '''<summary>
        '''  Looks up a localized string similar to Si è verificato un errore nella lettura dei dati xml, l&apos;applicazione verrà chiusa..
        '''</summary>
        Friend Shared ReadOnly Property errorParseXml() As String
            Get
                Return ResourceManager.GetString("errorParseXml", resourceCulture)
            End Get
        End Property

        '''<summary>
        '''  Looks up a localized string similar to Controlla Gmail.
        '''</summary>
        Friend Shared ReadOnly Property menuCheckMail() As String
            Get
                Return ResourceManager.GetString("menuCheckMail", resourceCulture)
            End Get
        End Property

        '''<summary>
        '''  Looks up a localized string similar to Configurazione.
        '''</summary>
        Friend Shared ReadOnly Property menuConfiguration() As String
            Get
                Return ResourceManager.GetString("menuConfiguration", resourceCulture)
            End Get
        End Property

        '''<summary>
        '''  Looks up a localized string similar to Esci.
        '''</summary>
        Friend Shared ReadOnly Property menuExit() As String
            Get
                Return ResourceManager.GetString("menuExit", resourceCulture)
            End Get
        End Property

        '''<summary>
        '''  Looks up a localized string similar to Apri Gmail.
        '''</summary>
        Friend Shared ReadOnly Property menuOpenMail() As String
            Get
                Return ResourceManager.GetString("menuOpenMail", resourceCulture)
            End Get
        End Property
    End Class
End Namespace