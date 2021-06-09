Imports System
Imports System.Data
Imports System.Data.OracleClient
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Collections.Specialized
Imports NAICLdapSec

Public Class Form1



    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim sUserID As String = txtUserID.Text
        Dim sPassword As String = txtPwd.Text
        Dim sAppID As String = txtAppID.Text
        Dim sDB As String = txtDB.Text
        Console.WriteLine("Realm: " & sDB.Substring(4))
        Dim sRealm As String = sDB.Substring(4)
        sRealm = "dvlp"
        'getPassword (for application)
        Dim oClientPwdService As New ClientServerPasswordService(sRealm)


        'Dim realmName As String = "qa"
        'Dim userPassword As String = "grapes11"
        'Dim userID As String = "SVOINBSK"
        'Dim applicationId As String = "SVOINBASKET_APP"
        'Dim oClientPwdService As New ClientServerPasswordService(realmName.ToLower())

        'Dim passWord As String = oClientPwdService.getPassword(applicationId, userID, userPassword)

        'If passWord Is Nothing Then
        '    Console.WriteLine("ERROR: Problem getting the password " _
        '         & "from the password file.")
        'End If
        'Console.WriteLine("passWord: " & passWord)


        Environment.SetEnvironmentVariable("NAICPWD", "S:/Oracle8/dvlp")

        Dim sPwd As String = oClientPwdService.getPassword(sAppID, sUserID, sPassword)
        Console.WriteLine(sUserID + " has permission to the password " + sPwd + " for application ID: " + sAppID.ToUpper)

        Console.WriteLine(sAppID & " was able to connect to " & txtDB.Text & "  " & openConnection(txtDB.Text, sAppID, sPwd))




        Dim oUser As New User
        Dim oUserService As New UserService(oClientPwdService.Connection)


        'oUser = oUserService.findUserByUserId(sUserID)
        oUser = NAICLdapSec.UserService.getUserWithApplicationRoles(sUserID, "Portal")
        Dim sRole As String = "NP_MKT_CAD_PR"
        Console.WriteLine(sUserID + " has the " + sRole + " Role: " + oUser.hasRole(sRole).ToString)

        Dim myCol As New ListDictionary()

        Console.WriteLine("---------------------------TESTING FINDUSERS-----------------")
        'findUsers
        Dim rtn As New Collection(Of System.Object)
        Console.WriteLine("findUsers: ou=users,o=data", "uid=nj*")
        Try
            rtn = oUserService.findUsers("ou=users,o=data", "uid=nj*")
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try

        For Each item As Object In rtn
            oUser = CType(item, NAICLdapSec.User)
            'Console.WriteLine("UserName: " & oUser.UserName())
            'If (oUser.UserName().Trim.ToUpper = "NJ824") Then
            '    Console.WriteLine(" ")
            'End If
            Console.WriteLine("EMailAddress: " & oUser.EmailAddress())
            Console.WriteLine("State: " & oUser.State())
            Console.WriteLine("isActive(): " & oUser.IsEnabled())
        Next item
        Console.WriteLine(" ")
        Dim i As Integer
        'If sRealm.Equals("dvlp") Then
        '    'This test ID only exists in DVLP....
        '    sUserID = "dlttest123"

        '    'findUserByUserID()
        '    Console.WriteLine("findUserByUserId: " & sUserID)
        '    oUser = oUserService.findUserByUserId(sUserID)
        '    Dim sMailAddress As String = oUser.EmailAddress()
        '    Console.WriteLine("EMailAddress: " & sMailAddress)
        '    Dim sMailAddress2 As String = oUser.EmailAddress2()
        '    Console.WriteLine("EMailAddress2: " & sMailAddress2)
        '    Dim sMailAddress3 As String = oUser.EmailAddress3()
        '    Console.WriteLine("EMailAddress3: " & sMailAddress3)
        '    Dim sMailAddress4 As String = oUser.EmailAddress4()
        '    Console.WriteLine("EMailAddress4: " & sMailAddress4)
        '    Dim sState As String = oUser.State()
        '    Console.WriteLine("State: " & sState)
        '    Dim sLastName As String = oUser.LastName()
        '    Console.WriteLine("LastName: " & sLastName)
        '    Dim sDBList As IList = oUser.Databases()
        '    Dim sTemp As String = ""
        '    For i = 0 To sDBList.Count - 1
        '        sTemp = CStr(sDBList(i))
        '    Next

        '    Dim empType As String = oUser.EmployeeType()
        '    Console.WriteLine("empType: " & empType)

        '    Dim userAccountControl As Boolean = oUser.UserAccountControl()
        '    Console.WriteLine("userAccountControl: " & userAccountControl)

        '    Dim PasswordCreationDate As Date = oUser.PasswordCreationDate
        '    Console.WriteLine("passwordcreationdate: " & PasswordCreationDate.ToString())

        '    Dim webService As Boolean = oUser.WebService()
        '    Console.WriteLine("webService: " & webService)

        '    Dim isEnabled As Boolean = oUser.IsEnabled()
        '    Console.WriteLine("isEnabled: " & isEnabled)
        'Else
        'sUserID = "SS714"
        Console.WriteLine("--------------------TESTING FIND USER BY USERID-----------------")
        oUser = oUserService.findUserByUserId(sUserID)
        'End If

        If (oUser IsNot Nothing) Then
            txtInfo.Text += "full name = " + oUser.FirstName + " " + oUser.LastName + vbCrLf
            'txtInfo.Text += "username = " + oUser.UserName + vbCrLf
            txtInfo.Text += "cn = " + oUser.Cn + vbCrLf
            txtInfo.Text += "dn = " + oUser.DistinguishedName + vbCrLf
            txtInfo.Text += "email = " + oUser.EmailAddress + vbCrLf
        End If



        'loadRolesForDatabase()
        Console.WriteLine("--------------------TESTING LOAD ROLES FOR DATABASE---------------")
        oUserService.loadRolesForDatabase(oUser, sDB)
        Dim dbRoles As New System.Collections.ObjectModel.Collection(Of NAICLdapSec.Role)
        dbRoles = oUser.Roles()
        Console.WriteLine("loadRolesForDatabase::dbRoles(count): " & sDB & ":" & dbRoles.Count)
        For i = 0 To dbRoles.Count - 1
            Console.WriteLine("Database: " + dbRoles.Item(i).Database + "  Name: " + dbRoles.Item(i).Name)
        Next

        Dim appRoles As New System.Collections.ObjectModel.Collection(Of NAICLdapSec.Role)

        Console.WriteLine("---------------------------TESTING STATIC METHOD-----------------")
        Dim oUser2 As New User
        oUser2 = NAICLdapSec.UserService.getUserWithDatabaseRoles("npuser7", "OLTPDVLP")
        dbRoles = oUser2.Roles()
        Console.WriteLine("loadRolesForDatabase::dbRoles(count): " & sDB & ":" & dbRoles.Count)
        For i = 0 To dbRoles.Count - 1
            Console.WriteLine("Database: " + dbRoles.Item(i).Database + "  Name: " + dbRoles.Item(i).Name)
        Next

        Console.WriteLine("---------------------------TESTING STATIC METHOD-----------------")

        oUser2 = NAICLdapSec.UserService.getUserWithApplicationRoles("npuser7", "Portal")
        appRoles = oUser2.Roles
        For i = 0 To appRoles.Count - 1
            Console.WriteLine("Application: " + appRoles.Item(i).Application + "  Name: " + appRoles.Item(i).Name)
        Next

        Dim appIds(0 To 1) As String
        appIds(0) = "PORTAL"
        appIds(1) = "SPL"
        oUser2 = NAICLdapSec.UserService.getUserWithApplicationsRoles("deye", appIds)
        appRoles = oUser2.Roles
        For i = 0 To appRoles.Count - 1
            Console.WriteLine("Name: " + appRoles.Item(i).Name)
            Console.WriteLine("Application: " + appRoles.Item(i).Application)
        Next

        Dim psservice As New PasswordService()
        Dim password = psservice.getPassword("SVCReporting")
        Console.WriteLine(password)

        Console.WriteLine("--------------------TESTING LOAD ROLES FOR APPLICATION---------------")
        'loadRolesForApplication()
        oUserService.loadRolesForApplication(oUser, sAppID)

        appRoles = oUser.Roles
        Console.WriteLine("loadRolesForApplication::appRoles(count): " & sAppID & ":" & appRoles.Count)
        For i = 0 To appRoles.Count - 1
            Console.WriteLine("Name: " + appRoles.Item(i).Name)
            Console.WriteLine("Application: " + appRoles.Item(i).Application)
        Next
        Console.WriteLine("---------------------------")
        oUserService.Connection.disconnect()

    End Sub

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        txtUserID.Text = "gmcdaniel"
        txtAppID.Text = "fdracod"
        txtDB.Text = "oltpdvlp"
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFindByCn.Click
        Dim sRealm As String = "dvlp"
        Dim oClientPwdService As New ClientServerPasswordService(sRealm)
        Dim oUser As New User
        Dim oUserService As New UserService(oClientPwdService.Connection)

        Dim rtn As New Collection(Of System.Object)
        Console.WriteLine("findUserByCn: " + txtCn.Text)
        Try
            oUser = oUserService.findUserByCn(txtCn.Text)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        txtInfo.Text += vbCrLf + oUser.LastName + ", " + oUser.FirstName + vbCrLf
        'txtInfo.Text += oUser.UserName + vbCrLf

    End Sub

    Private Sub Label7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label7.Click

    End Sub

    Private Sub Multiple_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim sUserID As String = txtUser2.Text
        Dim sPassword As String = txtPwd2.Text
        Dim sAppID As String = txtAppIds.Text

        Dim sRealm As String = "dvlp"
        'getPassword (for application)
        Dim oClientPwdService As New ClientServerPasswordService(sRealm)

        Dim oUser As New User
        Dim oUserService As New UserService(oClientPwdService.Connection)

        oUser = oUserService.findUserByUserId(sUserID)
        'End If

        txtInfo.Text += "full name = " + oUser.FirstName + " " + oUser.LastName + vbCrLf
        'txtInfo.Text += "username = " + oUser.UserName + vbCrLf
        txtInfo.Text += "cn = " + oUser.Cn + vbCrLf
        txtInfo.Text += "dn = " + oUser.DistinguishedName + vbCrLf
        txtInfo.Text += "email = " + oUser.EmailAddress + vbCrLf

        'loadRolesForApplications()

        If (sAppID.Contains(",")) Then
            'Dim appIdsSet As New HashSet(Of String)
            'Dim appIds As String() = sAppID.Split(",")
            'Dim id As String
            'For Each id In appIds
            '    appIdsSet.Add(id)
            'Next
            'oUserService.loadRolesForApplications(oUser, appIdsSet)
        Else
            oUserService.loadRolesForApplication(oUser, sAppID)
        End If

        Dim i As Integer

        Dim appRoles As New System.Collections.ObjectModel.Collection(Of NAICLdapSec.Role)
        appRoles = oUser.Roles
        Console.WriteLine("loadRolesForApplication::appRoles(count): " & appRoles.Count)
        For i = 0 To appRoles.Count - 1
            Console.WriteLine("Name: " + appRoles.Item(i).Name)
            Console.WriteLine("Application: " + appRoles.Item(i).Application)
        Next
        Console.WriteLine("---------------------------")
        oUserService.Connection.disconnect()

    End Sub

    Public Function openConnection(ByVal Db As String, ByVal userID As String, ByVal userPwd As String) As Boolean
        Dim oCn As OracleConnection = Nothing
        Dim bFlag As Boolean = False
        Try
            Dim ConnectionString As String = "Data Source=" & Db & ";User Id=" & userID & ";Password=" & userPwd & ";"
            oCn = New OracleConnection(ConnectionString)
            oCn.Open()
            bFlag = True
        Catch e As OracleException
            Console.WriteLine("Oracle Exception in openConnection(): " & e.Message.ToString)
            bFlag = False
        Catch ex As Exception
            Console.WriteLine("Exception in openConnection(): " & ex.Message.ToString)
            bFlag = False
        Finally
            If (oCn.State = ConnectionState.Open) Then
                oCn.Close()
            End If
        End Try
        Return bFlag
    End Function

End Class
