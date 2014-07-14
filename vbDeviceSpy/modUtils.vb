﻿Imports OpenSource.UPnP
Imports System.Net.NetworkInformation
Imports System.Net

Module modUtils

    Function CreateTreeNode(device As UPnPDevice, bForceParent As Boolean) As TreeNode
        Dim TempList As SortedList = New SortedList()
        If bForceParent Then
            '// force escalation to parent device
            If device.ParentDevice IsNot Nothing Then
                device = device.ParentDevice
            End If

        End If

        Dim Parent As TreeNode = New TreeNode(device.FriendlyName, 1, 1)
        Parent.Tag = device
        Parent.Name = device.UniqueDeviceName

        For cid As Integer = 0 To device.Services.Length - 1
            Dim Child As TreeNode = New TreeNode(device.Services(cid).ServiceURN, 2, 2)
            Child.Tag = device.Services(cid)
            Child.Name = device.Services(cid).ServiceID
            Dim stateVarNode As TreeNode = New TreeNode("State variables", 6, 6)
            Child.Nodes.Add(stateVarNode)
            Dim varList As UPnPStateVariable() = device.Services(cid).GetStateVariables()
            TempList.Clear()
            Dim array As UPnPStateVariable() = varList
            For i As Integer = 0 To array.Length - 1
                Dim var As UPnPStateVariable = array(i)
                Dim varNode As TreeNode = New TreeNode(var.Name, 5, 5)
                varNode.Tag = var
                TempList.Add(var.Name, varNode)
            Next
            Dim sve As IDictionaryEnumerator = TempList.GetEnumerator()
            While sve.MoveNext()
                stateVarNode.Nodes.Add(CType(sve.Value, TreeNode))
            End While
            TempList.Clear()
            Dim actions As UPnPAction() = device.Services(cid).GetActions()
            For i As Integer = 0 To actions.Length - 1
                Dim action As UPnPAction = actions(i)
                Dim argsstr As String = ""
                Dim argumentList As UPnPArgument() = action.ArgumentList
                For j As Integer = 0 To argumentList.Length - 1
                    Dim arg As UPnPArgument = argumentList(j)
                    If Not arg.IsReturnValue Then
                        If argsstr <> "" Then
                            argsstr += ", "
                        End If
                        argsstr = argsstr + arg.RelatedStateVar.ValueType + " " + arg.Name
                    End If
                Next
                Dim methodNode As TreeNode = New TreeNode(action.Name + "(" + argsstr + ")", 4, 4)
                methodNode.Tag = action
                TempList.Add(action.Name, methodNode)
            Next
            Dim ide As IDictionaryEnumerator = TempList.GetEnumerator()
            While ide.MoveNext()
                Child.Nodes.Add(CType(ide.Value, TreeNode))
            End While
            Parent.Nodes.Add(Child)
        Next
        For cid As Integer = 0 To device.EmbeddedDevices.Length - 1
            Dim Child As TreeNode = ProcessEmbeddedDevice(device.EmbeddedDevices(cid))
            Child.Tag = device.EmbeddedDevices(cid)
            Child.Name = device.EmbeddedDevices(cid).UniqueDeviceName
            Parent.Nodes.Add(Child)
        Next
        Return Parent
    End Function

    Function ProcessEmbeddedDevice(device As UPnPDevice) As TreeNode
        Dim TempList As SortedList = New SortedList()
        Dim Parent As TreeNode = New TreeNode(device.FriendlyName, 1, 1)
        Parent.Tag = device
        Parent.Name = device.UniqueDeviceName

        For cid As Integer = 0 To device.Services.Length - 1
            Dim Child As TreeNode = New TreeNode(device.Services(cid).ServiceURN, 2, 2)
            Child.Tag = device.Services(cid)
            Child.Name = device.Services(cid).ServiceID

            Dim stateVarNode As TreeNode = New TreeNode("State variables", 6, 6)
            Child.Nodes.Add(stateVarNode)
            Dim varList As UPnPStateVariable() = device.Services(cid).GetStateVariables()
            TempList.Clear()
            Dim array As UPnPStateVariable() = varList
            For i As Integer = 0 To array.Length - 1
                Dim var As UPnPStateVariable = array(i)
                Dim varNode As TreeNode = New TreeNode(var.Name, 5, 5)
                varNode.Tag = var
                TempList.Add(var.Name, varNode)
            Next
            Dim sve As IDictionaryEnumerator = TempList.GetEnumerator()
            While sve.MoveNext()
                stateVarNode.Nodes.Add(CType(sve.Value, TreeNode))
            End While
            TempList.Clear()
            Dim actions As UPnPAction() = device.Services(cid).GetActions()
            For i As Integer = 0 To actions.Length - 1
                Dim action As UPnPAction = actions(i)
                Dim argsstr As String = ""
                Dim argumentList As UPnPArgument() = action.ArgumentList
                For j As Integer = 0 To argumentList.Length - 1
                    Dim arg As UPnPArgument = argumentList(j)
                    If Not arg.IsReturnValue Then
                        If argsstr <> "" Then
                            argsstr += ", "
                        End If
                        argsstr = argsstr + arg.RelatedStateVar.ValueType + " " + arg.Name
                    End If
                Next
                Dim methodNode As TreeNode = New TreeNode(action.Name + "(" + argsstr + ")", 4, 4)
                methodNode.Tag = action
                TempList.Add(action.Name, methodNode)
            Next
            Dim ide As IDictionaryEnumerator = TempList.GetEnumerator()
            While ide.MoveNext()
                Child.Nodes.Add(CType(ide.Value, TreeNode))
            End While
            Parent.Nodes.Add(Child)
        Next
        For cid As Integer = 0 To device.EmbeddedDevices.Length - 1
            Dim Child As TreeNode = ProcessEmbeddedDevice(device.EmbeddedDevices(cid))
            Child.Tag = device.EmbeddedDevices(cid)
            Child.Name = device.EmbeddedDevices(cid).UniqueDeviceName
            Parent.Nodes.Add(Child)
        Next
        Return Parent
    End Function

    Public Function SetListInfo(infoObject As Object) As ArrayList
        Dim Items As ArrayList = New ArrayList()
        If infoObject Is Nothing Then
            Items.Add(New ListViewItem(New String() {"Product name", "Device Spy"}))
            Items.Add(New ListViewItem(New String() {"Version", AutoUpdate.VersionString}))
        Else
            If infoObject.[GetType]() Is GetType(UPnPDevice) Then
                Dim d As UPnPDevice = CType(infoObject, UPnPDevice)
                Items.Add(New ListViewItem(New String() {"Friendly name", d.FriendlyName}))
                Items.Add(New ListViewItem(New String() {"Unique device name", d.UniqueDeviceName}))
                Items.Add(New ListViewItem(New String() {"Has presentation", d.HasPresentation.ToString()}))
                If d.LocationURL IsNot Nothing Then
                    Items.Add(New ListViewItem(New String() {"Location URL", d.LocationURL.ToString()}))
                End If

                Items.Add(New ListViewItem(New String() {"Manufacturer", d.Manufacturer}))
                Items.Add(New ListViewItem(New String() {"Manufacturer URL", d.ManufacturerURL}))
                Items.Add(New ListViewItem(New String() {"Model description", d.ModelDescription}))
                Items.Add(New ListViewItem(New String() {"Model name", d.ModelName}))
                Items.Add(New ListViewItem(New String() {"Model number", d.ModelNumber}))
                If d.ModelURL IsNot Nothing Then
                    Items.Add(New ListViewItem(New String() {"Model URL", d.ModelURL.ToString()}))
                End If
                Items.Add(New ListViewItem(New String() {"Product code", d.ProductCode}))
                Items.Add(New ListViewItem(New String() {"Proprietary type", d.ProprietaryDeviceType}))
                Items.Add(New ListViewItem(New String() {"Serial number", d.SerialNumber}))
                Items.Add(New ListViewItem(New String() {"Services", d.Services.Length.ToString()}))
                Items.Add(New ListViewItem(New String() {"Embedded devices", d.EmbeddedDevices.Length.ToString()}))
                Items.Add(New ListViewItem(New String() {"Base URL", d.BaseURL.ToString()}))
                Items.Add(New ListViewItem(New String() {"Device URN", d.DeviceURN}))
                Items.Add(New ListViewItem(New String() {"Expiration timeout", d.ExpirationTimeout.ToString()}))
                Items.Add(New ListViewItem(New String() {"Version", d.Major.ToString() + "." + d.Minor.ToString()}))
                Items.Add(New ListViewItem(New String() {"Remote endpoint", d.RemoteEndPoint.ToString()}))
                Items.Add(New ListViewItem(New String() {"Standard type", d.StandardDeviceType}))
                If d.Icon IsNot Nothing Then
                    Items.Add(New ListViewItem(New String() {"Device icon", String.Concat(New Object() {"Present, ", d.Icon.Width, "x", d.Icon.Height})}))
                Else
                    Items.Add(New ListViewItem(New String() {"Device icon", "None"}))
                End If
                If d.InterfaceToHost IsNot Nothing Then
                    Items.Add(New ListViewItem(New String() {"Interface to host", d.InterfaceToHost.ToString()}))
                Else
                    Items.Add(New ListViewItem(New String() {"Interface to host", "(Embedded device)"}))
                End If
                Dim deviceURL As String = ""
                Try
                    If d.PresentationURL <> Nothing Then
                        If d.PresentationURL.StartsWith("/") Then
                            deviceURL = String.Concat(New String() {"http://", d.RemoteEndPoint.Address.ToString(), ":", d.RemoteEndPoint.Port.ToString(), d.PresentationURL})
                        Else
                            If Not d.PresentationURL.ToUpper().StartsWith("HTTP://") Then
                                deviceURL = String.Concat(New String() {"http://", d.RemoteEndPoint.Address.ToString(), ":", d.RemoteEndPoint.Port.ToString(), "/", d.PresentationURL})
                            Else
                                deviceURL = d.PresentationURL
                            End If
                        End If
                    End If
                Catch 'ex_65A As Object
                End Try
                Items.Add(New ListViewItem(New String() {"Presentation URL", deviceURL}))
            Else
                If infoObject.[GetType]() Is GetType(UPnPService) Then
                    Dim s As UPnPService = CType(infoObject, UPnPService)
                    Items.Add(New ListViewItem(New String() {"Parent UDN", s.ParentDevice.DeviceURN}))
                    Items.Add(New ListViewItem(New String() {"Version", s.Major.ToString() + "." + s.Minor.ToString()}))
                    Items.Add(New ListViewItem(New String() {"Methods", s.Actions.Count.ToString()}))
                    Items.Add(New ListViewItem(New String() {"State variables", s.GetStateVariables().Length.ToString()}))
                    Items.Add(New ListViewItem(New String() {"Service ID", s.ServiceID}))
                    Items.Add(New ListViewItem(New String() {"Service URL", CStr(New UPnPDebugObject(s).GetField("SCPDURL"))}))
                    Dim deviceURL As String = Nothing
                    Try
                        If s.ParentDevice.PresentationURL <> Nothing Then
                            If s.ParentDevice.PresentationURL.ToLower().StartsWith("http://") OrElse s.ParentDevice.PresentationURL.ToLower().StartsWith("https://") Then
                                deviceURL = s.ParentDevice.PresentationURL
                            Else
                                If s.ParentDevice.PresentationURL.StartsWith("/") Then
                                    deviceURL = String.Concat(New String() {"http://", s.ParentDevice.RemoteEndPoint.Address.ToString(), ":", s.ParentDevice.RemoteEndPoint.Port.ToString(), s.ParentDevice.PresentationURL})
                                Else
                                    deviceURL = String.Concat(New String() {"http://", s.ParentDevice.RemoteEndPoint.Address.ToString(), ":", s.ParentDevice.RemoteEndPoint.Port.ToString(), "/", s.ParentDevice.PresentationURL})
                                End If
                            End If
                        End If
                    Catch 'ex_95F As Object
                    End Try
                    If deviceURL <> Nothing Then
                        Items.Add(New ListViewItem(New String() {"Parent presentation URL", deviceURL}))
                    End If
                Else
                    If infoObject.[GetType]() Is GetType(UPnPAction) Then
                        'Me.listInfo.Sorting = SortOrder.None
                        Dim a As UPnPAction = CType(infoObject, UPnPAction)
                        Items.Add(New ListViewItem(New String() {"Action name", a.Name}))
                        If Not a.HasReturnValue Then
                            Items.Add(New ListViewItem(New String() {"Return argument", "<none>"}))
                        Else
                            Items.Add(New ListViewItem(New String() {"Return argument ASV", a.GetRetArg().RelatedStateVar.Name}))
                            Items.Add(New ListViewItem(New String() {"Return Type", a.GetRetArg().RelatedStateVar.ValueType}))
                        End If
                        Dim argnum As Integer = 1
                        Dim argumentList As UPnPArgument() = a.ArgumentList
                        For i As Integer = 0 To argumentList.Length - 1
                            Dim arg As UPnPArgument = argumentList(i)
                            If Not arg.IsReturnValue Then
                                Dim dataType As String = arg.DataType
                                If dataType Is Nothing OrElse dataType = "" Then
                                End If
                                Items.Add(New ListViewItem(New String() {"Argument " + argnum, "(" + arg.RelatedStateVar.ValueType + ") " + arg.Name}))
                                Items.Add(New ListViewItem(New String() {"Argument " + argnum + " ASV", arg.RelatedStateVar.Name}))
                                argnum += 1
                            End If
                        Next
                    Else
                        If infoObject.[GetType]() Is GetType(UPnPStateVariable) Then
                            Dim var As UPnPStateVariable = CType(infoObject, UPnPStateVariable)
                            Items.Add(New ListViewItem(New String() {"Variable name", var.Name}))
                            Items.Add(New ListViewItem(New String() {"Evented", var.SendEvent.ToString()}))
                            Items.Add(New ListViewItem(New String() {"Data type", var.ValueType}))
                            Try
                                Items.Add(New ListViewItem(New String() {"Last known value", var.Value.ToString()}))
                            Catch ex_C88 As Exception
                                Items.Add(New ListViewItem(New String() {"Last known value", "<unknown>"}))
                            End Try
                            If var.Minimum IsNot Nothing AndAlso var.Maximum Is Nothing Then
                                If var.[Step] IsNot Nothing Then
                                    Items.Add(New ListViewItem(New String() {"Value range", "Not below " + var.Minimum.ToString() + ", Step " + var.[Step].ToString()}))
                                Else
                                    Items.Add(New ListViewItem(New String() {"Value range", "Not below " + var.Minimum.ToString()}))
                                End If
                            Else
                                If var.Minimum Is Nothing AndAlso var.Maximum IsNot Nothing Then
                                    If var.[Step] IsNot Nothing Then
                                        Items.Add(New ListViewItem(New String() {"Value range", "Not above " + var.Maximum.ToString() + ", Step " + var.[Step].ToString()}))
                                    Else
                                        Items.Add(New ListViewItem(New String() {"Value range", "Not above " + var.Maximum.ToString()}))
                                    End If
                                Else
                                    If var.Minimum IsNot Nothing OrElse var.Maximum IsNot Nothing Then
                                        If var.[Step] IsNot Nothing Then
                                            Items.Add(New ListViewItem(New String() {"Value range", String.Concat(New String() {"From ", var.Minimum.ToString(), " to ", var.Maximum.ToString(), ", Step ", var.[Step].ToString()})}))
                                        Else
                                            Items.Add(New ListViewItem(New String() {"Value range", "From " + var.Minimum.ToString() + " to " + var.Maximum.ToString()}))
                                        End If
                                    End If
                                End If
                            End If
                            If var.AllowedStringValues IsNot Nothing AndAlso var.AllowedStringValues.Length > 0 Then
                                Dim AllowedValues As String = ""
                                Dim allowedStringValues As String() = var.AllowedStringValues
                                For i As Integer = 0 To allowedStringValues.Length - 1
                                    Dim x As String = allowedStringValues(i)
                                    If AllowedValues <> "" Then
                                        AllowedValues += ", "
                                    End If
                                    AllowedValues += x
                                Next
                                Items.Add(New ListViewItem(New String() {"Allowed values", AllowedValues}))
                            End If
                            If var.DefaultValue IsNot Nothing Then
                                Items.Add(New ListViewItem(New String() {"Default value", UPnPService.SerializeObjectInstance(var.DefaultValue)}))
                            End If
                        End If
                    End If
                End If
            End If
        End If
        Return Items
    End Function

    '// a utility routine that chacks for a serviceID within a device -- walking up and down the device tree looking at parents and children.
    Public Function CheckForService(device As UPnPDevice, serviceID As String) As Boolean
        If Not device.ParentDevice Is Nothing Then
            Return CheckForService(device.ParentDevice, serviceID)
        Else    '// now let's check for AVTransport somewhere in the tree
            For Each service As UPnPService In device.Services
                If service.ServiceID = serviceID Then
                    Return True
                End If
            Next
            For Each childDevice As UPnPDevice In device.EmbeddedDevices
                For Each service As UPnPService In childDevice.Services
                    If service.ServiceID = serviceID Then
                        Return True
                    End If
                Next
            Next

            Return False
        End If
    End Function

    Public Function GetCategoryNode(RootNodes As Dictionary(Of String, TreeNode), Categories As Dictionary(Of String, String), device As UPnPDevice, exception As String) As TreeNode

        If Categories.ContainsKey(device.DeviceURN) Then
            Return RootNodes(device.DeviceURN)
        Else
            For Each childDevice As UPnPDevice In device.EmbeddedDevices
                Return GetCategoryNode(RootNodes, Categories, childDevice, exception)
            Next
        End If
        Return RootNodes(exception)
    End Function

    Public Function GetManagedCategoryNode(ManagedRootNodes As Dictionary(Of String, TreeNode), Categories As Dictionary(Of String, String), device As UPnPDevice, exception As String) As TreeNode
        If Categories.ContainsKey(device.User) Then
            Return ManagedRootNodes(device.User)
        Else
            Return ManagedRootNodes(exception)
        End If
    End Function


#Region "IP Utilities"
    Public Function GetValidIPAddresses() As Collection
        Dim ipadds As New Collection
        For Each networkCard As NetworkInterface In NetworkInterface.GetAllNetworkInterfaces
            ' Find network cards with gateway information (this may show more than one network card depending on computer)
            For Each gatewayAddr As GatewayIPAddressInformation In networkCard.GetIPProperties.GatewayAddresses
                ' if gateway address is NOT 0.0.0.0 and the network card status is UP then we've found the main network card
                If gatewayAddr.Address.ToString <> "0.0.0.0" And networkCard.OperationalStatus.ToString() = "Up" Then
                    ' Get IP Address(es) and subnet(s) information
                    Dim IpAddressAndSubnet As UnicastIPAddressInformation
                    For Each IpAddressAndSubnet In networkCard.GetIPProperties.UnicastAddresses
                        If Not ipadds.Contains(IpAddressAndSubnet.Address.ToString) Then
                            ipadds.Add(IpAddressAndSubnet.Address, IpAddressAndSubnet.Address.ToString)
                        End If
                    Next
                End If
            Next
        Next
        Return ipadds
    End Function

    Private Function GetPrimaryNic()
        ' DESCRIPTION: this function  will provide networking details for primary network card

        Dim PrimaryNic As New Collection

        For Each networkCard As NetworkInterface In NetworkInterface.GetAllNetworkInterfaces

            ' Find network cards with gateway information (this may show more than one network card depending on computer)
            For Each gatewayAddr As GatewayIPAddressInformation In networkCard.GetIPProperties.GatewayAddresses

                ' if gateway address is NOT 0.0.0.0 and the network card status is UP then we've found the main network card
                If gatewayAddr.Address.ToString <> "0.0.0.0" And networkCard.OperationalStatus.ToString() = "Up" Then
                    PrimaryNic.Add("Interface GUID: " & networkCard.Id)
                    PrimaryNic.Add("Name:".PadRight(15) & networkCard.Name)
                    PrimaryNic.Add("Description:".PadRight(15) & networkCard.Description)
                    PrimaryNic.Add("Status:".PadRight(15) & networkCard.OperationalStatus.ToString)
                    PrimaryNic.Add("Status:".PadRight(15) & (networkCard.Speed / 1000000).ToString("#,000") & " Mbps")
                    PrimaryNic.Add("MAC Address:".PadRight(15) & networkCard.GetPhysicalAddress.ToString)

                    ' Get IP Address(es) and subnet(s) information
                    Dim IpAddressAndSubnet As UnicastIPAddressInformation

                    For Each IpAddressAndSubnet In networkCard.GetIPProperties.UnicastAddresses
                        PrimaryNic.Add("IP Address:".PadRight(15) & IpAddressAndSubnet.Address.ToString)
                        PrimaryNic.Add("Subnet:".PadRight(15) & IpAddressAndSubnet.IPv4Mask.ToString)
                    Next

                    ' Get IP gateway information
                    PrimaryNic.Add("Gateway:".PadRight(15) & gatewayAddr.Address.ToString)

                    ' Get IP DNS information
                    Dim DnsAddress As IPAddress

                    For Each DnsAddress In networkCard.GetIPProperties.DnsAddresses
                        PrimaryNic.Add("DNS entry:".PadRight(15) & DnsAddress.ToString)
                    Next

                    ' Other IP information
                    Dim IPProp As IPInterfaceProperties = networkCard.GetIPProperties

                    If Not IPProp Is Nothing Then
                        PrimaryNic.Add("DNS Enabled:".PadRight(15) & IPProp.IsDnsEnabled.ToString)
                        PrimaryNic.Add("Dynamic DNS:".PadRight(15) & IPProp.IsDynamicDnsEnabled.ToString)
                    End If

                    Dim IPv4 As IPv4InterfaceProperties = networkCard.GetIPProperties.GetIPv4Properties

                    If Not IPv4 Is Nothing Then
                        PrimaryNic.Add("DHCP Enabled:".PadRight(15) & IPv4.IsDhcpEnabled.ToString)
                        PrimaryNic.Add("MTU Setting:".PadRight(15) & IPv4.Mtu.ToString)
                        PrimaryNic.Add("Uses WINS:".PadRight(15) & IPv4.UsesWins.ToString)
                    End If

                End If
            Next
        Next

        Return PrimaryNic

    End Function
#End Region
End Module
