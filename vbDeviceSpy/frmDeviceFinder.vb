Imports OpenSource.UPnP

Public Class frmDeviceFinder

    Public Delegate Sub UpdateTreeDelegate(device As UPnPDevice, node As TreeNode)
    Private SubscribeTime As Integer = 300
    Protected scp As UPnPSmartControlPoint
    Private df As UPnPDeviceFactory
    Protected UPnpRoot() As TreeNode '= New TreeNode("Media Devices", 0, 0)
    Private ForceDeviceList As Hashtable = New Hashtable()
    Private SubscribedList As New ArrayList
    Private Categories As New Dictionary(Of String, String)
    Private RootNodes As New Dictionary(Of String, TreeNode)

#Region "GUI Management"

    '// manage Toolstrip resizing and "spring" functionality
    Private Sub GuiResizing()
        Me.cmbSearch.AutoSize = False
        Me.cmbSearch.Width = Me.ToolStrip1.ClientSize.Width - GetToolItemLeft(cmbSearch) - Me.ToolStrip1.OverflowButton.Width - Me.ToolStrip1.Padding.Left - Me.ToolStrip1.Margin.Left
    End Sub

    '// calculate available space for toolstrip item "spring"
    Private Function GetToolItemLeft(ByVal item As ToolStripItem) As Integer
        Dim left As Integer
        For Each search As ToolStripItem In Me.ToolStrip1.Items
            If search IsNot item Then
                left += search.Width
            End If
        Next

        Return left
    End Function
#End Region

#Region "Initialization & Cleanup"

    Private Sub Init()
        Me.SubscribeTime = 300
        '        Me.UPnpRoot = New TreeNode("Media Devices", 0, 0)

        Categories.Add("urn:schemas-upnp-org:device:MediaRenderer:1", "Media Renderers")
        Categories.Add("urn:schemas-upnp-org:device:MediaServer:1", "Media Servers")
        Categories.Add("OTHER", "Other")


        For Each item In Categories
            Dim root As New TreeNode(item.Value, 0, 0)
            root.Tag = item.Key
            RootNodes.Add(item.Key, root)
            Me.deviceTree.Nodes.Add(root)
        Next


        'Me.ForceDeviceList = New Hashtable()


        '// test 
        'Dim NetworkUri = New Uri("http://192.168.1.199:1400/xml/device_description.xml")
        'df = New UPnPDeviceFactory(NetworkUri, 1800, New UPnPDeviceFactory.UPnPDeviceHandler(AddressOf HandleForceAddDevice), New UPnPDeviceFactory.UPnPDeviceFailedHandler(AddressOf HandleForceAddFailed), System.Net.IPAddress.Parse("192.168.1.103"), "RINCON_000E58A72F9601400")

        'Me.scp = New UPnPSmartControlPoint(New UPnPSmartControlPoint.DeviceHandler(AddressOf Me.HandleAddedDevice), Nothing, {"urn:schemas-upnp-org:service:AVTransport:1", "urn:schemas-upnp-org:service:ConnectionManager:1", "urn:schemas-upnp-org:service:RenderingControl:1"})
        Me.scp = New UPnPSmartControlPoint(New UPnPSmartControlPoint.DeviceHandler(AddressOf Me.HandleAddedDevice))
        AddHandler Me.scp.OnRemovedDevice, New UPnPSmartControlPoint.DeviceHandler(AddressOf Me.HandleRemovedDevice)
        GuiResizing()
    End Sub

    Private Sub CleanUp()
        '// unsubscribe to all subscribed services!
        For Each service As UPnPService In SubscribedList
            service.UnSubscribe(Nothing)
        Next
        For Each device As UPnPDevice In scp.Devices
            Try
                scp.ForceDisposeDevice(device)
            Catch ex As Exception
                Debug.Print("Couldn't kill :" & device.FriendlyName)
            End Try
        Next
        scp.Devices.Clear()
        scp = Nothing
    End Sub

#End Region

#Region "Callbacks"
    Protected Sub HandleAddedDevice(sender As UPnPSmartControlPoint, device As UPnPDevice)
        Me.AddDeviceToTree(device, device.BaseURL)
    End Sub

    Protected Sub HandleRemovedDevice(sender As UPnPSmartControlPoint, device As UPnPDevice)
        MyBase.Invoke(New UPnPSmartControlPoint.DeviceHandler(AddressOf Me.RemovedDeviceFromTree), New Object() {sender, device})
    End Sub

    Protected Sub HandleSubscribe(sender As UPnPService, succes As Boolean)
        If MyBase.InvokeRequired Then
            MyBase.Invoke(New UPnPService.UPnPEventSubscribeHandler(AddressOf Me.HandleSubscribe), New Object() {sender, succes})
        Else
            If Not succes Then
                Me.lblStatus.Text = "FAILED Subscription - " + sender.ParentDevice.FriendlyName + ", " + sender.ServiceID
            Else
                Me.lblStatus.Text = "Subscribed to " + sender.ParentDevice.FriendlyName + ", " + sender.ServiceID
                If Not SubscribedList.Contains(sender) Then
                    SubscribedList.Add(sender)
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
                            SetListInfo(Me.deviceTree.SelectedNode.Tag)
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
#End Region

#Region "Form and GUI Events"

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
        Dim Items As ArrayList = SetListInfo(node.Tag)
        Me.listInfo.Sorting = SortOrder.Ascending
        Me.listInfo.Items.Clear()
        Me.listInfo.Items.AddRange(CType(Items.ToArray(GetType(ListViewItem)), ListViewItem()))
    End Sub

    Private Sub frmDeviceFinder_Load(sender As Object, e As EventArgs) Handles Me.Load
        Init()
    End Sub

    Private Sub Form1_HandleDestroyed(sender As Object, e As EventArgs) Handles Me.HandleDestroyed
        'Me.deviceTree.Nodes.Add(Me.UPnpRoot)
        Cleanup()
    End Sub

    Private Sub ToolStrip1_SizeChanged(sender As Object, e As EventArgs) Handles ToolStrip1.SizeChanged
        GuiResizing()
    End Sub

    Private Sub eventSubscribeMenuItem_Click(sender As Object, e As EventArgs) Handles eventSubscribeMenuItem.Click
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

    Private Sub copyEventCpMenuItem_Click(sender As Object, e As EventArgs) Handles copyEventCpMenuItem.Click
        If Me.eventListView.SelectedItems.Count <> 0 Then
            Clipboard.SetText(Me.eventListView.SelectedItems(0).SubItems(3).Text)
        End If
    End Sub

    Private Sub expandAllMenuItem2_Click(sender As Object, e As EventArgs) Handles expandAllMenuItem2.Click

        If Me.deviceTree.SelectedNode IsNot Nothing Then
            Me.deviceTree.SelectedNode.Expand()
            Dim node As TreeNode = Me.deviceTree.SelectedNode
            For Each child As TreeNode In node.Nodes
                If child.Nodes.Count > 0 Then
                    'child.Expand()
                End If
            Next
        End If


    End Sub

    Private Sub collapseAllMenuItem2_Click(sender As Object, e As EventArgs) Handles collapseAllMenuItem2.Click
        If Me.deviceTree.SelectedNode IsNot Nothing Then
            Me.deviceTree.SelectedNode.Collapse()
            Dim node As TreeNode = Me.deviceTree.SelectedNode
            For Each child As TreeNode In node.Nodes
                If child.Nodes.Count > 0 Then
                    'child.Expand()
                End If
            Next
        End If
    End Sub
#End Region

#Region "Tree and ListInfo Routines"

    Protected Sub AddDeviceToTree(device As UPnPDevice, URL As Uri)
        'Dim parent As TreeNode = CreateTreeNode(device)
        'Dim args As Object() = New Object() {device, parent}
        'MyBase.Invoke(New UpdateTreeDelegate(AddressOf Me.UpdateTree), args)
    End Sub

    Function GetRootNode(device As UPnPDevice) As TreeNode

        If Categories.ContainsKey(device.DeviceURN) Then
            Return RootNodes(device.DeviceURN)
        Else
            For Each childDevice As UPnPDevice In device.EmbeddedDevices
                Return GetRootNode(childDevice)
            Next
        End If
        Return RootNodes("OTHER")

    End Function

    Protected Sub RemovedDeviceFromTree(sender As UPnPSmartControlPoint, device As UPnPDevice)
        Dim root As TreeNode = GetRootNode(device)
        Dim TempList As ArrayList = New ArrayList()
        Dim en As IEnumerator = root.Nodes.GetEnumerator()
        While en.MoveNext()
            Dim tn As TreeNode = CType(en.Current, TreeNode)
            If (CType(tn.Tag, UPnPDevice)).UniqueDeviceName = device.UniqueDeviceName Then
                TempList.Add(tn)
            End If
        End While
        For x As Integer = 0 To TempList.Count - 1
            Dim i As TreeNode = CType(TempList(x), TreeNode)
            Me.CleanTags(i)
            root.Nodes.Remove(i)
        Next
    End Sub

    Protected Sub UpdateTree(device As UPnPDevice, node As TreeNode)
        Dim root As TreeNode = GetRootNode(device)
        If root.Nodes.Count = 0 Then
            root.Nodes.Add(node)
        Else
            For i As Integer = 0 To root.Nodes.Count - 1
                If root.Nodes(i).Text.CompareTo(node.Text) > 0 Then
                    root.Nodes.Insert(i, node)
                    Exit For
                End If
                If i = root.Nodes.Count - 1 Then
                    root.Nodes.Add(node)
                    Exit For
                End If
            Next
        End If
        root.Expand()
    End Sub

    Private Sub CleanTags(n As TreeNode)
        n.Tag = Nothing
        For Each sn As TreeNode In n.Nodes
            Me.CleanTags(sn)
        Next
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
 
#End Region
 

End Class