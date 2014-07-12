Imports OpenSource.UPnP
Imports OpenSource.Utilities
Imports System
Imports System.Collections
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Drawing
Imports System.Globalization
Imports System.IO
Imports System.Reflection
Imports System.Threading
Imports System.Windows.Forms

Public Class Form1
    Public Delegate Sub UpdateTreeDelegate(node As TreeNode)
    Private SubscribeTime As Integer = 300
    Protected scp As UPnPSmartControlPoint
    Protected UPnpRoot As TreeNode = New TreeNode("Devices", 0, 0)
    Private ForceDeviceList As Hashtable = New Hashtable()
    Private df As UPnPDeviceFactory
    Private cp As UPnPControlPoint

    Private Sub Form1_HandleDestroyed(sender As Object, e As EventArgs) Handles Me.HandleDestroyed
        'Me.deviceTree.Nodes.Add(Me.UPnpRoot)

        Me.scp = New UPnPSmartControlPoint(New UPnPSmartControlPoint.DeviceHandler(AddressOf Me.HandleAddedDevice))
        AddHandler scp.OnRemovedDevice, AddressOf Me.HandleRemovedDevice

    End Sub

    Protected Sub HandleAddedDevice(sender As UPnPSmartControlPoint, device As UPnPDevice)
        Me.HandleCreate(device, device.BaseURL)
    End Sub

    Protected Sub HandleRemovedDevice(sender As UPnPSmartControlPoint, device As UPnPDevice)
        MyBase.Invoke(New UPnPSmartControlPoint.DeviceHandler(AddressOf Me.HandleRemovedDeviceEx), New Object() {sender, device})
    End Sub

    '// This is a callback that is called when a Forced Add succeeds and a device is found
    Private Sub HandleForceAddDevice(sender As OpenSource.UPnP.UPnPDeviceFactory, device As OpenSource.UPnP.UPnPDevice, URL As System.Uri)

        Me.HandleCreate(device, device.BaseURL)
    End Sub

    Private Sub HandleControlPointAddDevice(device As OpenSource.UPnP.UPnPDevice, URL As System.Uri)
        Me.HandleCreate(device, device.BaseURL)
    End Sub


    '// This is a callback that is called when a Forced Add FAILS
    Private Sub HandleForceAddFailed(sender As UPnPDeviceFactory, LocationUri As Uri, e As Exception, urn As String)
        Debug.Print("Force Add Failed")
    End Sub


    Protected Sub HandleRemovedDeviceEx(sender As UPnPSmartControlPoint, device As UPnPDevice)
        Dim TempList As ArrayList = New ArrayList()
        Dim en As IEnumerator = Me.UPnpRoot.Nodes.GetEnumerator()
        While en.MoveNext()
            Dim tn As TreeNode = CType(en.Current, TreeNode)
            If (CType(tn.Tag, UPnPDevice)).UniqueDeviceName = device.UniqueDeviceName Then
                TempList.Add(tn)
            End If
        End While
        For x As Integer = 0 To TempList.Count - 1
            Dim i As TreeNode = CType(TempList(x), TreeNode)
            Me.CleanTags(i)
            Me.UPnpRoot.Nodes.Remove(i)
        Next
    End Sub

    Protected Sub HandleCreate(device As UPnPDevice, URL As Uri)
        Dim TempList As SortedList = New SortedList()
        Dim Parent As TreeNode = New TreeNode(device.FriendlyName, 1, 1)
        Parent.Tag = device
        For cid As Integer = 0 To device.Services.Length - 1
            Dim Child As TreeNode = New TreeNode(device.Services(cid).ServiceURN, 2, 2)
            Child.Tag = device.Services(cid)
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
            Dim Child As TreeNode = Me.ProcessEmbeddedDevice(device.EmbeddedDevices(cid))
            Child.Tag = device.EmbeddedDevices(cid)
            Parent.Nodes.Add(Child)
        Next
        Dim args As Object() = New Object() {Parent}
        MyBase.Invoke(New UpdateTreeDelegate(AddressOf Me.HandleTreeUpdate), args)
    End Sub

    Protected Function ProcessEmbeddedDevice(device As UPnPDevice) As TreeNode
        Dim TempList As SortedList = New SortedList()
        Dim Parent As TreeNode = New TreeNode(device.FriendlyName, 1, 1)
        Parent.Tag = device
        For cid As Integer = 0 To device.Services.Length - 1
            Dim Child As TreeNode = New TreeNode(device.Services(cid).ServiceURN, 2, 2)
            Child.Tag = device.Services(cid)
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
            Dim Child As TreeNode = Me.ProcessEmbeddedDevice(device.EmbeddedDevices(cid))
            Child.Tag = device.EmbeddedDevices(cid)
            Parent.Nodes.Add(Child)
        Next
        Return Parent
    End Function

    Private Sub CleanTags(n As TreeNode)
        n.Tag = Nothing
        For Each sn As TreeNode In n.Nodes
            Me.CleanTags(sn)
        Next
    End Sub

    Protected Sub HandleTreeUpdate(node As TreeNode)
        If Me.UPnpRoot.Nodes.Count = 0 Then
            Me.UPnpRoot.Nodes.Add(node)
        Else
            For i As Integer = 0 To Me.UPnpRoot.Nodes.Count - 1
                If Me.UPnpRoot.Nodes(i).Text.CompareTo(node.Text) > 0 Then
                    Me.UPnpRoot.Nodes.Insert(i, node)
                    Exit For
                End If
                If i = Me.UPnpRoot.Nodes.Count - 1 Then
                    Me.UPnpRoot.Nodes.Add(node)
                    Exit For
                End If
            Next
        End If
        Me.UPnpRoot.Expand()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.SubscribeTime = 300
        Me.UPnpRoot = New TreeNode("Devices", 0, 0)
        Me.ForceDeviceList = New Hashtable()

        Me.deviceTree.Nodes.Add(Me.UPnpRoot)

        'Dim NetworkUri = New Uri("http://192.168.1.199:1400/xml/device_description.xml")

        'df = New UPnPDeviceFactory(NetworkUri, 1800, New UPnPDeviceFactory.UPnPDeviceHandler(AddressOf HandleForceAddDevice), New UPnPDeviceFactory.UPnPDeviceFailedHandler(AddressOf HandleForceAddFailed), System.Net.IPAddress.Parse("192.168.1.103"), "RINCON_000E58A72F9601400")

        'cp = New UPnPControlPoint()
        'AddHandler cp.OnCreateDevice, AddressOf HandleControlPointAddDevice
        'cp.CreateDeviceAsync(NetworkUri, 600)




        Me.scp = New UPnPSmartControlPoint(New UPnPSmartControlPoint.DeviceHandler(AddressOf Me.HandleAddedDevice), Nothing, {"urn:schemas-upnp-org:service:AVTransport:1", "urn:schemas-upnp-org:service:ConnectionManager:1", "urn:schemas-upnp-org:service:RenderingControl:1"})
        'df = New UPnPDeviceFactory(NetworkUri, 1800, New UPnPDeviceFactory.UPnPDeviceHandler(AddressOf HandleForceAddDevice), New UPnPDeviceFactory.UPnPDeviceFailedHandler(AddressOf HandleForceAddFailed), Nothing, Nothing)

        'Me.scp = New UPnPSmartControlPoint()
        'AddHandler Me.scp.OnAddedDevice, New UPnPSmartControlPoint.DeviceHandler(AddressOf Me.HandleAddedDevice)
        'AddHandler Me.scp.OnRemovedDevice, New UPnPSmartControlPoint.DeviceHandler(AddressOf Me.HandleRemovedDevice)

    End Sub

    Private Sub OnSubscribeService(sender As Object, e As EventArgs) Handles presPageMenuItem.Click, eventSubscribeMenuItem.Click
        If Me.deviceTree.SelectedNode IsNot Nothing Then
            If Me.deviceTree.SelectedNode.Tag IsNot Nothing Then
                Dim obj As Object = Me.deviceTree.SelectedNode.Tag
                If obj.[GetType]() Is GetType(UPnPService) Then
                    Dim service As UPnPService = DirectCast(obj, UPnPService)
                    If Not Me.deviceTree.SelectedNode.Checked Then
                        Me.splitter2.Visible = True
                        Me.eventListView.Visible = True
                        Me.menuItem3.Checked = True
                        Me.deviceTree.SelectedNode.ImageIndex = 3
                        Me.deviceTree.SelectedNode.SelectedImageIndex = 3
                        Me.deviceTree.SelectedNode.Checked = True
                        AddHandler service.OnSubscribe, New UPnPService.UPnPEventSubscribeHandler(AddressOf Me.HandleSubscribe)

                        Dim stateVariables As UPnPStateVariable() = (CType(service, UPnPService)).GetStateVariables()
                        For i As Integer = 0 To stateVariables.Length - 1
                            Dim V As UPnPStateVariable = stateVariables(i)
                            AddHandler V.OnModified, New UPnPStateVariable.ModifiedHandler(AddressOf Me.HandleEvents)
                        Next
                        service.Subscribe(Me.SubscribeTime, Nothing)
                    Else
                        Me.deviceTree.SelectedNode.ImageIndex = 2
                        Me.deviceTree.SelectedNode.SelectedImageIndex = 2
                        Me.deviceTree.SelectedNode.Checked = False
                        AddHandler service.OnSubscribe, New UPnPService.UPnPEventSubscribeHandler(AddressOf Me.HandleSubscribe)

                        Dim stateVariables As UPnPStateVariable() = (CType(service, UPnPService)).GetStateVariables()
                        For i As Integer = 0 To stateVariables.Length - 1
                            Dim V As UPnPStateVariable = stateVariables(i)
                            If V.SendEvent Then
                                AddHandler V.OnModified, New UPnPStateVariable.ModifiedHandler(AddressOf Me.HandleEvents)
                            End If
                        Next
                        service.UnSubscribe(Nothing)
                    End If
                End If
            End If
        End If
    End Sub

    Protected Sub HandleEvents(sender As UPnPStateVariable, EventValue As Object)
        If MyBase.InvokeRequired Then
            MyBase.Invoke(New UPnPStateVariable.ModifiedHandler(AddressOf Me.HandleEvents), New Object() {sender, EventValue})
        Else
            Dim eventSource As String = sender.OwningService.ParentDevice.FriendlyName + "/" + sender.OwningService.ServiceID
            Dim eventValue1 As String = UPnPService.SerializeObjectInstance(EventValue)
            If eventValue1 = "" Then
                eventValue1 = "(Empty)"
            End If
            Debug.Print(eventSource & "|" & EventValue)
            Dim now As DateTime = DateTime.Now
            Dim i As ListViewItem = New ListViewItem(New String() {now.ToShortTimeString(), eventSource, sender.Name, EventValue})
            i.Tag = now
            Me.eventListView.Items.Insert(0, i)
            If Me.deviceTree.SelectedNode IsNot Nothing Then
                If Me.deviceTree.SelectedNode.Tag.[GetType]() Is GetType(UPnPStateVariable) Then
                    If (CType(Me.deviceTree.SelectedNode.Tag, UPnPStateVariable)).SendEvent Then
                        If Me.deviceTree.SelectedNode.Tag.GetHashCode() = sender.GetHashCode() Then
                            Me.SetListInfo(Me.deviceTree.SelectedNode.Tag)
                        End If
                    End If
                End If
            End If
            Dim fNode As TreeNode = Me.deviceTree.Nodes(0).FirstNode
            While fNode IsNot Nothing
                Me.ScanDeviceNode(fNode, sender.OwningService)
                fNode = fNode.NextNode
            End While
        End If
    End Sub

    Protected Sub SetListInfo(infoObject As Object)
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
                        Me.listInfo.Sorting = SortOrder.None
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
        Me.listInfo.Sorting = SortOrder.Ascending
        Me.listInfo.Items.Clear()
        Me.listInfo.Items.AddRange(CType(Items.ToArray(GetType(ListViewItem)), ListViewItem()))
    End Sub

    Protected Sub HandleSubscribe(sender As UPnPService, succes As Boolean)
        If MyBase.InvokeRequired Then
            MyBase.Invoke(New UPnPService.UPnPEventSubscribeHandler(AddressOf Me.HandleSubscribe), New Object() {sender, succes})
        Else
            If Not succes Then
                Me.statusBar.Text = "FAILED Subscription - " + sender.ParentDevice.FriendlyName + ", " + sender.ServiceID
            Else
                Me.statusBar.Text = "Subscribed to " + sender.ParentDevice.FriendlyName + ", " + sender.ServiceID
            End If
        End If
    End Sub

    Private Sub ScanDeviceNode(dNode As TreeNode, s As UPnPService)
        Dim pNode As TreeNode = dNode.FirstNode
        While pNode IsNot Nothing
            If pNode.Tag.[GetType]() Is GetType(UPnPDevice) Then
                Me.ScanDeviceNode(pNode, s)
            Else
                If pNode.Tag.GetHashCode() = s.GetHashCode() Then
                    Dim vNode As TreeNode = pNode.FirstNode.FirstNode
                    While vNode IsNot Nothing
                        If (CType(vNode.Tag, UPnPStateVariable)).SendEvent Then
                            Dim stringValue As String = UPnPService.SerializeObjectInstance((CType(vNode.Tag, UPnPStateVariable)).Value)
                            If stringValue.Length > 10 Then
                                stringValue = stringValue.Substring(0, 10) + "..."
                            End If
                            If stringValue.Length > 0 Then
                                stringValue = " [" + stringValue + "]"
                            End If
                            vNode.Text = (CType(vNode.Tag, UPnPStateVariable)).Name + stringValue
                        End If
                        vNode = vNode.NextNode
                    End While
                    Exit Sub
                End If
            End If
            pNode = pNode.NextNode
        End While
    End Sub

    Private Sub presPageMenuItem_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub ValidateActionMenuItem_Click(sender As Object, e As EventArgs) Handles ValidateActionMenuItem.Click

    End Sub

    Private Sub deviceTree_MouseDown(sender As Object, e As MouseEventArgs) Handles deviceTree.MouseDown
        Me.eventSubscribeMenuItem.Visible = False
        Me.invokeActionMenuItem.Visible = False
        Me.presPageMenuItem.Visible = False
        Me.menuItem18.Visible = False
        Me.DeviceXMLmenuItem.Visible = False
        Me.ServiceXMLmenuItem.Visible = False
        Me.ValidateActionMenuItem.Visible = False
        Me.removeDeviceMenuItem.Visible = False
        Dim node As TreeNode = Me.deviceTree.GetNodeAt(e.X, e.Y)
        If node IsNot Nothing Then
            Me.deviceTree.SelectedNode = node
            Dim infoObject As Object = node.Tag
            If infoObject IsNot Nothing Then
                If infoObject.[GetType]() Is GetType(UPnPDevice) Then
                    If (CType(infoObject, UPnPDevice)).ParentDevice Is Nothing Then
                        Me.removeDeviceMenuItem.Visible = True
                    End If
                    If (CType(infoObject, UPnPDevice)).HasPresentation Then
                        Me.presPageMenuItem.Visible = True
                        Me.menuItem18.Visible = True
                    End If
                    If (CType(infoObject, UPnPDevice)).LocationURL <> Nothing Then
                        Me.DeviceXMLmenuItem.Visible = True
                        Me.menuItem18.Visible = True
                    End If
                End If
                If infoObject.[GetType]() Is GetType(UPnPService) Then
                    Me.ValidateActionMenuItem.Visible = True
                    Me.eventSubscribeMenuItem.Visible = True
                    Me.ServiceXMLmenuItem.Visible = True
                    Me.menuItem18.Visible = True
                    Me.eventSubscribeMenuItem.Checked = node.Checked
                End If
                If infoObject.[GetType]() Is GetType(UPnPAction) Then
                    Me.invokeActionMenuItem.Visible = True
                    Me.menuItem18.Visible = True
                End If
            End If
        End If
    End Sub
    Private Sub OnSelectedItem(sender As Object, e As TreeViewEventArgs) Handles deviceTree.AfterSelect
        Dim node As TreeNode = Me.deviceTree.SelectedNode
        Me.SetListInfo(node.Tag)
    End Sub

    Private Sub eventSubscribeMenuItem_Click(sender As Object, e As EventArgs)

    End Sub
End Class
