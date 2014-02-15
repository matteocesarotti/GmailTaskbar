Imports System.Security.Cryptography
Imports System.IO
Imports System.Text
Imports System.Xml.Serialization
Imports System.Environment
Imports Gmail.Connector

Namespace Tools
    Public Class EncryptorDecryptor
        Private key() As Byte = {}
        Private IV() As Byte = {&H12, &H34, &H56, &H78, &H90, &HAB, &HCD, &HEF}
        Private Const EncryptionKey As String = "abcdefgh"

        Public Function Decrypt(ByVal stringToDecrypt As String) As String
            Try
                Dim inputByteArray(stringToDecrypt.Length) As Byte
                key = System.Text.Encoding.UTF8.GetBytes(Left(EncryptionKey, 8))
                Dim des As New DESCryptoServiceProvider
                inputByteArray = Convert.FromBase64String(stringToDecrypt)
                Dim ms As New MemoryStream
                Dim cs As New CryptoStream(ms, des.CreateDecryptor(key, IV), CryptoStreamMode.Write)
                cs.Write(inputByteArray, 0, inputByteArray.Length)
                cs.FlushFinalBlock()
                Dim encoding As System.Text.Encoding = System.Text.Encoding.UTF8
                Return encoding.GetString(ms.ToArray())
            Catch ex As Exception
                Return ""
            End Try
        End Function

        Public Function Encrypt(ByVal stringToEncrypt As String) As String
            Try
                key = System.Text.Encoding.UTF8.GetBytes(Left(EncryptionKey, 8))
                Dim des As New DESCryptoServiceProvider
                Dim inputByteArray() As Byte = Encoding.UTF8.GetBytes(stringToEncrypt)
                Dim ms As New MemoryStream
                Dim cs As New CryptoStream(ms, des.CreateEncryptor(key, IV), CryptoStreamMode.Write)
                cs.Write(inputByteArray, 0, inputByteArray.Length)
                cs.FlushFinalBlock()
                Return Convert.ToBase64String(ms.ToArray())
            Catch ex As Exception
                Return ""
            End Try
        End Function
    End Class

    Public Class ConfigurationManager
        Dim appData As String = GetFolderPath(SpecialFolder.ApplicationData) & "\CesaApp\GmailTaskbar"
        Dim fileConfigName As String = "\GmailTaskBar.xml"

        Public Sub New()
            ConfigFileExists()
        End Sub

        Private Function ConfigFileExists() As Boolean
            If Directory.Exists(appData) = False Then
                Directory.CreateDirectory(appData)
                Dim gg As New GmailConfiguration
                gg.GmailUrl = "https://mail.google.com/mail/feed/atom"
                gg.Timer = 30
                gg.AutoStartUp = False
                WriteXmlConfig(gg)
            End If
            Return File.Exists(appData & fileConfigName)
        End Function

        Public Sub WriteXmlConfig(ByVal configFile As GmailConfiguration)
            Dim serializer As New XmlSerializer(GetType(GmailConfiguration))
            Using writer As TextWriter = New StreamWriter(appData & fileConfigName)
                serializer.Serialize(writer, configFile)
            End Using
        End Sub

        Public Function ReadXmlConfig() As GmailConfiguration
            Dim serializer As New XmlSerializer(GetType(GmailConfiguration))
            Dim gg As GmailConfiguration = Nothing
            Using txtread As TextReader = New StreamReader(appData & fileConfigName)
                gg = serializer.Deserialize(txtread)
            End Using
            Return gg
        End Function
    End Class
End Namespace
