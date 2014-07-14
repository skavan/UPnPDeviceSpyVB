Imports OpenSource.UPnP

Public Class frmDeviceFinderClean

    Public Delegate Sub UpdateTreeDelegate(device As UPnPDevice, node As TreeNode)
    Public Delegate Sub DeviceChangeHandler(device As UPnPDevice, isManagedTree As Boolean)
    Public Delegate Sub ServiceSubscribedHandler(device As UPnPDevice, service As UPnPService, bUnsubscribe As Boolean)
    Public Delegate Sub ServiceDataChangedHandler(service As UPnPService, sender As UPnPStateVariable, data As Object)
    Public WithEvents disc As New discovery
    Protected UPnpRoot() As TreeNode '= New TreeNode("Media Devices", 0, 0)

    Private SubscribedList As New ArrayList

    Private Categories As New Dictionary(Of String, String)
    Private ManagedCategories As New Dictionary(Of String, String)

    Private RootNodes As New Dictionary(Of String, TreeNode)
    Private ManagedRootNodes As New Dictionary(Of String, TreeNode)

    Private Const COMPLETEDEVICE = "Complete Device: "
    Private Const AVTRANSPORTDEVICE = "AV Transport Device: "
    Private Const COMPOUNDDEVICE = "Compound Device: "
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

        Categories.Add("urn:schemas-upnp-org:device:MediaRenderer:1", "Media Renderers")
        Categories.Add("urn:schemas-upnp-org:device:MediaServer:1", "Media Servers")
        Categories.Add("OTHER", "Other")


        ManagedCategories.Add(discovery.eManagedDeviceType.completeDevice, "Complete Devices")
        ManagedCategories.Add(discovery.eManagedDeviceType.compoundDevice, "Compound Devices")
        ManagedCategories.Add(discovery.eManagedDeviceType.avTranportDevice, "AVTransport Only")
        'ManagedCategories.Add("OTHER", "Other")

        For Each item In Categories
            Dim root As New TreeNode(item.Value, 0, 0)
            root.Tag = item.Key
            RootNodes.Add(item.Key, root)
            Me.deviceTree.Nodes.Add(root)
        Next

        For Each item In ManagedCategories
            Dim root As New TreeNode(item.Value, 0, 0)
            root.Tag = item.Key
            ManagedRootNodes.Add(item.Key, root)
            Me.ManagedTree.Nodes.Add(root)
        Next

        disc.Init()
        GuiResizing()
    End Sub

    Private Sub CleanUp()
        disc.CleanUp()
        disc = Nothing
    End Sub
#End Region

#Region "Callbacks"

    Private Sub disc_deviceDiscoveryEvent(device As UPnPDevice, deviceChangeEvent As discovery.eDeviceDiscoveryEvent) Handles disc.deviceDiscoveryEvent
        Select Case deviceChangeEvent
            Case discovery.eDeviceDiscoveryEvent.deviceAdded
                MyBase.Invoke(New DeviceChangeHandler(AddressOf AddDeviceToTree), {device, False})
                'Me.AddDeviceToTree(device)
            Case discovery.eDeviceDiscoveryEvent.deviceRemoved
                MyBase.Invoke(New DeviceChangeHandler(AddressOf Me.RemovedDeviceFromTree), New Object() {device, False})
            Case discovery.eDeviceDiscoveryEvent.managedDeviceAdded
                MyBase.Invoke(New DeviceChangeHandler(AddressOf AddDeviceToTree), {device, True})
            Case discovery.eDeviceDiscoveryEvent.managedDeviceRemoved
                MyBase.Invoke(New DeviceChangeHandler(AddressOf Me.RemovedDeviceFromTree), {device, False})
        End Select

    End Sub

    Private Sub disc_serviceDataEvent(service As UPnPService, sender As UPnPStateVariable, EventValue As Object) Handles disc.serviceDataEvent
        MyBase.Invoke(New ServiceDataChangedHandler(AddressOf Me.HandleEvents), {service, sender, EventValue})
        Debug.Print("Service Data:" & service.ServiceURN & "-" & EventValue.ToString)
    End Sub

    Private Sub disc_serviceSubscriptionEvent(device As UPnPDevice, service As UPnPService, serviceChangeEvent As discovery.eServiceSubscriptionEvent) Handles disc.serviceSubscriptionEvent
        Select Case serviceChangeEvent
            Case discovery.eServiceSubscriptionEvent.serviceOnSubscribe
                Debug.Print("service subscribed:" & service.ServiceURN)
                MyBase.Invoke(New ServiceSubscribedHandler(AddressOf UpdateSubscribedService), {device, service, False})
                'UpdateSubscribedService(device, service)
            Case discovery.eServiceSubscriptionEvent.serviceOnUnsubscribe
                Debug.Print("service UNSUBSCRIBED:" & service.ServiceURN)
                'UpdateSubscribedService(device, service, True)
                MyBase.Invoke(New ServiceSubscribedHandler(AddressOf UpdateSubscribedService), {device, service, True})
        End Select


    End Sub

#End Region

#Region "Form and GUI Events"

    Private Sub ManagedTree_MouseDown(sender As Object, e As MouseEventArgs) Handles ManagedTree.MouseDown
        ResetPopupMenuItems()
        ProcessNodePopupMenu(ManagedTree, e)
    End Sub

    Private Sub ResetPopupMenuItems()
        Me.eventSubscribeMenuItem.Visible = False
        Me.invokeActionMenuItem.Visible = False
        Me.presPageMenuItem.Visible = False
        Me.menuItem18.Visible = False
        Me.DeviceXMLmenuItem.Visible = False

        Me.ManagedDeviceMenuItem.Visible = False
        Me.ManagedDeviceSeperatorMenuItem.Visible = False
        Me.AddCompoundDeviceMenuItem.Visible = False

        Me.ServiceXMLmenuItem.Visible = False
        Me.ValidateActionMenuItem.Visible = False
        Me.removeDeviceMenuItem.Visible = False
    End Sub

    '// do the hard work of setting up the popup menu
    Private Sub ProcessNodePopupMenu(tree As TreeView, e As MouseEventArgs)
        ResetPopupMenuItems()
        Dim node As TreeNode = tree.GetNodeAt(e.X, e.Y)                                                                         '// get the node
        If node IsNot Nothing Then
            tree.SelectedNode = node
            Dim infoObject As Object = node.Tag
            If infoObject IsNot Nothing Then
                If infoObject.[GetType]() Is GetType(UPnPDevice) Then                                                           '// if the node is a device
                    Dim device As UPnPDevice = disc.FindTopMostDevice(infoObject)
                    If tree Is deviceTree Then                                                                                  '// if its the device tree then build the Add to Managed stuff
                        If (disc.CheckForService(device, discovery.AVTRANSPORT)) And (Not (disc.isManaged(device))) Then
                            Me.ManagedDeviceMenuItem.Visible = True
                            Me.ManagedDeviceSeperatorMenuItem.Visible = True
                            Me.AddManagedDeviceMenuItem.Visible = True

                            If disc.CheckForService(device, discovery.CONTENTDIRECTORY) Then '// It's a complete device
                                AddManagedDeviceMenuItem.Text = COMPLETEDEVICE & device.FriendlyName
                                Dim transportDevice As UPnPDevice = infoObject
                                If disc.CheckChildrenForService(transportDevice, discovery.AVTRANSPORT) Then
                                    transportDevice = disc.FindChildDeviceWithService(transportDevice, discovery.AVTRANSPORT)
                                    AddCompoundDeviceMenuItem.Text = AVTRANSPORTDEVICE & transportDevice.FriendlyName
                                    AddCompoundDeviceMenuItem.Tag = transportDevice
                                    AddCompoundDeviceMenuItem.Visible = True


                                End If
                            Else
                                '// let's find another device in the tree with a compatable IP address and matching service
                                Dim siblingDevice As UPnPDevice = disc.FindSiblingDevice(device, discovery.CONTENTDIRECTORY)
                                If siblingDevice Is Nothing Then
                                    '// AV Transport Only
                                    AddManagedDeviceMenuItem.Text = AVTRANSPORTDEVICE & device.FriendlyName

                                Else
                                    AddManagedDeviceMenuItem.Text = AVTRANSPORTDEVICE & device.FriendlyName
                                    AddCompoundDeviceMenuItem.Visible = True
                                    AddCompoundDeviceMenuItem.Text = String.Format("{0}{1}+{2}", COMPOUNDDEVICE, device.FriendlyName, siblingDevice.FriendlyName)
                                    AddCompoundDeviceMenuItem.Tag = {device, siblingDevice}

                                End If
                            End If
                            AddManagedDeviceMenuItem.Tag = device

                        End If
                    End If

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

    '// Popup Menu and Associated menu Items for deviceTree
    Private Sub deviceTree_MouseDown(sender As Object, e As MouseEventArgs) Handles deviceTree.MouseDown
        ResetPopupMenuItems()
        ProcessNodePopupMenu(deviceTree, e)

    End Sub

    Private Sub AddManagedDeviceMenuItem_Click(sender As Object, e As EventArgs) Handles AddManagedDeviceMenuItem.Click
        Dim menuItem As MenuItem = sender
        If menuItem.Text.Contains(COMPLETEDEVICE) Then
            disc.AddManagedDevice(menuItem.Tag)
        Else
            disc.AddManagedDevice(menuItem.Tag)
        End If
    End Sub

    Private Sub AddCompoundDeviceMenuItem_Click(sender As Object, e As EventArgs) Handles AddCompoundDeviceMenuItem.Click
        Dim menuItem As MenuItem = sender
        If menuItem.Text.Contains(AVTRANSPORTDEVICE) Then
            disc.AddManagedDevice(menuItem.Tag)
        Else
            disc.AddManagedDevice(menuItem.Tag(0), menuItem.Tag(1))
        End If
    End Sub

    '// Get detailed data on an item
    Private Sub OnSelectedItem(sender As Object, e As TreeViewEventArgs) Handles deviceTree.AfterSelect
        Dim node As TreeNode = Me.deviceTree.SelectedNode
        Dim Items As ArrayList = SetListInfo(node.Tag)
        Me.listInfo.Sorting = SortOrder.Ascending
        Me.listInfo.Items.Clear()
        Me.listInfo.Items.AddRange(CType(Items.ToArray(GetType(ListViewItem)), ListViewItem()))
    End Sub

    '// on load
    Private Sub frmDeviceFinder_Load(sender As Object, e As EventArgs) Handles Me.Load
        Init()
    End Sub

    '// on exit
    Private Sub Form1_HandleDestroyed(sender As Object, e As EventArgs) Handles Me.HandleDestroyed
        'Me.deviceTree.Nodes.Add(Me.UPnpRoot)
        CleanUp()
    End Sub

    '// on GUI resize
    Private Sub ToolStrip1_SizeChanged(sender As Object, e As EventArgs) Handles ToolStrip1.SizeChanged
        GuiResizing()
    End Sub

    '// menu item -- Subscribe/Unsubscribe to Events
    Private Sub eventSubscribeMenuItem_Click(sender As Object, e As EventArgs) Handles eventSubscribeMenuItem.Click
        Dim menuItem As MenuItem = sender
        If menuItem.GetContextMenu.SourceControl Is deviceTree Then
            SubscribeToService(deviceTree)
        Else
            Debug.Print("MANAGED TREE")
            SubscribeToService(ManagedTree)
        End If
    End Sub

    Private Sub SubscribeToService(tree As TreeView)
        If tree.SelectedNode IsNot Nothing Then
            If tree.SelectedNode.Tag IsNot Nothing Then
                Dim obj As Object = tree.SelectedNode.Tag
                If obj.[GetType]() Is GetType(UPnPService) Then
                    Dim service As UPnPService = DirectCast(obj, UPnPService)
                    If Not tree.SelectedNode.Checked Then
                        HighlightSubscribedNode(tree.SelectedNode, True)
                        disc.Subscribe(service)
                    Else
                        HighlightSubscribedNode(tree.SelectedNode, False)
                        disc.UnSubscribe(service)
                    End If
                End If
            End If
        End If
    End Sub

    '// copy data value to clipboard
    Private Sub copyEventCpMenuItem_Click(sender As Object, e As EventArgs) Handles copyEventCpMenuItem.Click
        If Me.eventListView.SelectedItems.Count <> 0 Then
            Clipboard.SetText(Me.eventListView.SelectedItems(0).SubItems(3).Text)
        End If
    End Sub

    '// expand tree
    Private Sub expandAllMenuItem2_Click(sender As Object, e As EventArgs) Handles expandAllMenuItem2.Click
        Dim menuItem As MenuItem = sender
        If menuItem.GetContextMenu.SourceControl Is deviceTree Then
            Debug.Print("DEVICE TREE")
            ExpandNode(deviceTree)
        Else
            Debug.Print("MANAGED TREE")
            ExpandNode(ManagedTree)
        End If


    End Sub


    '//collapse tree
    Private Sub collapseAllMenuItem2_Click(sender As Object, e As EventArgs) Handles collapseAllMenuItem2.Click
        Dim menuItem As MenuItem = sender
        If menuItem.GetContextMenu.SourceControl Is deviceTree Then
            CollapseNode(deviceTree)
        Else

            CollapseNode(ManagedTree)
        End If
    End Sub
#End Region

#Region "Tree and ListInfo Routines"

    Private Sub HighlightSubscribedNode(node As TreeNode, bHighlight As Boolean)
        If bHighlight Then
            Me.splitter2.Visible = True
            Me.eventListView.Visible = True
            Me.menuItem3.Checked = True
            node.ImageIndex = 3
            node.SelectedImageIndex = 3
            node.Checked = True
            node.ForeColor = Color.Yellow
            node.BackColor = Color.Red
        Else
            node.ImageIndex = 2
            node.SelectedImageIndex = 2
            node.Checked = False
            node.ForeColor = Color.Black
            node.BackColor = Color.White

        End If
    End Sub

    Private Sub UpdateSubscribedService(device As UPnPDevice, service As UPnPService, Optional bUnsubscribe As Boolean = False)
        Dim nodes As TreeNode()
        nodes = deviceTree.Nodes.Find(device.UniqueDeviceName, True)
        HighlightNode(device, service, nodes, bUnsubscribe)

        nodes = ManagedTree.Nodes.Find(device.UniqueDeviceName, True)
        HighlightNode(device, service, nodes, bUnsubscribe)
    End Sub

    Private Sub HighlightNode(device As UPnPDevice, service As UPnPService, nodes As TreeNode(), Optional bUnsubscribe As Boolean = False)
        For Each node As TreeNode In nodes
            If node.Tag Is device Then
                Debug.Print("FOUND DEVICE")
                Dim sNodes As TreeNode() = node.Nodes.Find(service.ServiceID, True)
                For Each sNode As TreeNode In sNodes
                    If sNode.Tag Is service Then
                        '// EUREKA - WE HAVE FOUND THE NODE.
                        HighlightSubscribedNode(sNode, Not bUnsubscribe)
                    End If
                Next
            End If
        Next
    End Sub

    '// Private routine to do the work
    Private Sub ExpandNode(tree As TreeView)
        If tree.SelectedNode IsNot Nothing Then
            tree.SelectedNode.Expand()
            Dim node As TreeNode = tree.SelectedNode
            For Each child As TreeNode In node.Nodes
                If child.ImageIndex = 1 Then
                    '// this means its a device
                    child.Expand()
                End If
            Next
        End If
    End Sub

    '// Private routine to do the work
    Private Sub CollapseNode(tree As TreeView)
        If tree.SelectedNode IsNot Nothing Then
            tree.SelectedNode.Collapse()
            Dim node As TreeNode = tree.SelectedNode
            For Each child As TreeNode In node.Nodes
                If child.Nodes.Count > 0 Then
                    'child.Expand()
                End If
            Next
        End If
    End Sub

    Protected Sub AddDeviceToTree(device As UPnPDevice, isManagedTree As Boolean)
        Dim parentNode As TreeNode
        If isManagedTree Then
            parentNode = CreateTreeNode(device, False)
        Else
            parentNode = CreateTreeNode(device, True)
        End If

        UpdateTree(device, parentNode, isManagedTree)
        'MyBase.Invoke(New UpdateTreeDelegate(AddressOf Me.UpdateTree), args)
    End Sub

    Protected Sub RemovedDeviceFromTree(device As UPnPDevice, isManagedTree As Boolean)
        Dim root As TreeNode
        If isManagedTree Then
            root = GetManagedCategoryNode(ManagedRootNodes, ManagedCategories, device, "OTHER")
        Else
            root = GetCategoryNode(RootNodes, Categories, device, "OTHER")
        End If
        'Dim root As TreeNode = GetCategoryNode(RootNodes, Categories, device)
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

    Protected Sub UpdateTree(device As UPnPDevice, node As TreeNode, isManagedTree As Boolean)
        Dim root As TreeNode
        If isManagedTree Then
            root = GetManagedCategoryNode(ManagedRootNodes, ManagedCategories, device, "OTHER")
        Else
            root = GetCategoryNode(RootNodes, Categories, device, "OTHER")
        End If
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

    Private Sub HandleEvents(service As UPnPService, sender As UPnPStateVariable, EventValue As Object)

        Dim eventSource As String = service.ParentDevice.FriendlyName + "/" + service.ServiceID
        Dim eventValue1 As String = UPnPService.SerializeObjectInstance(EventValue)
        If eventValue1 = "" Then
            eventValue1 = "(Empty)"
        End If

        'Debug.Print(eventSource & "|" & EventValue)
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

    End Sub

#End Region



End Class