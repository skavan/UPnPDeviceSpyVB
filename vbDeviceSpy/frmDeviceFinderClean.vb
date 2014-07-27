Imports OpenSource.UPnP
Imports OpenSource.Utilities
Imports System.Drawing

Public Class frmDeviceFinderClean

    Public Delegate Sub UpdateTreeDelegate(device As UPnPDevice, node As TreeNode)
    Public Delegate Sub DeviceChangeHandler(device As UPnPDevice, isManagedTree As Boolean)
    Public Delegate Sub ServiceSubscribedHandler(device As UPnPDevice, service As UPnPService, bUnsubscribe As Boolean)
    Public Delegate Sub ServiceDataChangedHandler(service As UPnPService, sender As UPnPStateVariable, data As Object)
    Public Delegate Sub PlayerStateChangeHandler(player As UPNPPlayer)

    Public WithEvents disc As New discovery
    Public WithEvents player As New UPNPPlayer
    Protected UPnpRoot() As TreeNode '= New TreeNode("Media Devices", 0, 0)

    Dim myThread As Threading.Thread

    Private SubscribedList As New ArrayList

    Private Categories As New Dictionary(Of String, String)
    Private ManagedCategories As New Dictionary(Of String, String)

    Private RootNodes As New Dictionary(Of String, TreeNode)
    Private ManagedRootNodes As New Dictionary(Of String, TreeNode)

    Private Const COMPLETEDEVICE = "Complete Device: "
    Private Const AVTRANSPORTDEVICE = "AV Transport Device: "
    Private Const COMPOUNDDEVICE = "Compound Device: "
    Private WithEvents tt As ToolTip
    Private Const STARTUPMODE = 0   '// 0 is auto, 1 is manual
    Dim myDebugForm As debugForm 'splashform is the form you want to be seperate from main ui thread

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

        'debugForm.Show()
        ShowDebug()
        'Dim frm As debugForm = debugForm
        'Application.Run(debugForm)

        'InstanceTracker.Enabled = True
        'InstanceTracker.Display()

        'EventLogger.ShowAll = True
        AddHandler player.StateChanged, AddressOf player_OnStateChanged
        AutoLoad()
        GuiResizing()
    End Sub

    Private Sub AutoLoad()

        If My.Settings.SavedDevices IsNot Nothing Then
            '// we have some savedDevices
            If My.Settings.SavedDevices.Count = 0 Then
                '// but actually w2e don't - so do a network scan
                disc.BeginNetworkScan()
            Else
                '// otherwise process and auto-load
                disc.LoadSettings(True)
            End If
            'Else
        Else
            '// we have no SavedDevices - so do a network scan
            disc.BeginNetworkScan()
        End If
    End Sub


    Private Sub CleanUp()
        'InstanceTracker.Enabled = False
        'InstanceTracker.ActiveForm.Close()
        RemoveHandler player.StateChanged, AddressOf player_OnStateChanged
        player = Nothing
        Debug.Print("Exiting.....")
        disc.SaveSettings()
        disc.CleanUp()
        disc = Nothing
        KillDebug()

    End Sub

    Private Sub KillDebug()
        If myThread.IsAlive Then
            myDebugForm.ShutDown()
            myDebugForm = Nothing
        End If
    End Sub

    Private Sub ShowDebug()

        '// if its uninited, init it.
        If myDebugForm Is Nothing Then
            myDebugForm = New debugForm
        End If

        '// if the thread doesn't exist, make it else kill and restart.
        If myThread Is Nothing Then
            myThread = New Threading.Thread(AddressOf myDebugForm.ShowDialog)
        Else
            myThread.Abort()
            myThread = Nothing
            myThread = New Threading.Thread(AddressOf myDebugForm.ShowDialog)
        End If


        'Dim myThread As New Threading.Thread()
        myThread.Start()

    End Sub

    Private Function CheckIsDebugVisible() As Boolean
        If myDebugForm IsNot Nothing Then
            Return myDebugForm.Visible
        Else
            Return False
        End If
    End Function


    '// Form Closing
    Private Sub frmDeviceFinderClean_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        CleanUp()
    End Sub

    '// Form Loading
    Private Sub frmDeviceFinder_Load(sender As Object, e As EventArgs) Handles Me.Load
        Init()
    End Sub


#End Region

#Region "Callbacks"

    '// called when a discovery event occurs - either a device being added or removed as well as a device becoming "managed". Invokes AddDeviceToTree as wellas managedDeviceAdded
    Private Sub disc_OnDeviceDiscovery(device As UPnPDevice, deviceChangeEvent As discovery.eDeviceDiscoveryEvent) Handles disc.deviceDiscoveryEvent

        Select Case deviceChangeEvent
            Case discovery.eDeviceDiscoveryEvent.deviceAdded
                MyBase.Invoke(New DeviceChangeHandler(AddressOf HandlesDeviceDiscovery_AddDevice), {device, False})
                'Me.AddDeviceToTree(device)
            Case discovery.eDeviceDiscoveryEvent.deviceRemoved
                MyBase.Invoke(New DeviceChangeHandler(AddressOf Me.HandlesDeviceDiscovery_RemoveDevice), New Object() {device, False})
            Case discovery.eDeviceDiscoveryEvent.managedDeviceAdded
                EventLogger.Log(Me, EventLogEntryType.Information, device.FriendlyName & " " & deviceChangeEvent.ToString)
                MyBase.Invoke(New DeviceChangeHandler(AddressOf HandlesDeviceDiscovery_AddDevice), {device, True})
            Case discovery.eDeviceDiscoveryEvent.managedDeviceRemoved
                MyBase.Invoke(New DeviceChangeHandler(AddressOf Me.HandlesDeviceDiscovery_RemoveDevice), {device, True})
        End Select

    End Sub

    '// we subscribe to events in two places in this whole system. This subscription happens in the discovery module to allow debuging and monitoring.
    '// but it is better and more detailed to use the subscription under a "managed" player.

    '// this callback delivers data from the service - but then invokes a method to handle the data -- HandleServiceEvents
    Private Sub disc_OnServiceDataReceived(service As UPnPService, sender As UPnPStateVariable, EventValue As Object) Handles disc.serviceDataEvent
        MyBase.Invoke(New ServiceDataChangedHandler(AddressOf Me.HandlesServiceDataReceived), {service, sender, EventValue})
    End Sub

    '// this callback signals when a servcie is successfully subscribed to. It then invokes processing in the UpdateSubscribedService method.
    Private Sub disc_OnServiceSubscription(device As UPnPDevice, service As UPnPService, serviceChangeEvent As discovery.eServiceSubscriptionEvent) Handles disc.serviceSubscriptionEvent
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

    '// called when a player has a changed Event, whether its a result of polling or otherwise. This function then invokes processing of the data in the UpdatePlayerInfo Method.
    Private Sub player_OnStateChanged(obj As UPNPPlayer, eventType As UPNPPlayer.ePlayerStateChangeType)
        'Debug.Print("Player State Change")
        If eventType = vbDeviceSpy.UPNPPlayer.ePlayerStateChangeType.PollingEvent Then
            EventLogger.Log(Me, EventLogEntryType.FailureAudit, obj.CurrentTrackTime.ToString)
        End If

        MyBase.Invoke(New PlayerStateChangeHandler(AddressOf Handles_PlayerStateChanged), obj)
    End Sub

#End Region

#Region "Form and GUI Events (Discovery System & General)"

    '// Popup Menu and Associated menu Items for ManagedTree (RIGHT CLICK)
    Private Sub ManagedTree_MouseDown(sender As Object, e As MouseEventArgs) Handles ManagedTree.MouseDown
        ResetPopupMenuItems()
        ProcessNodePopupMenu(ManagedTree, e)
    End Sub

    '// Popup Menu and Associated menu Items for deviceTree (RIGHT CLICK)
    Private Sub deviceTree_MouseDown(sender As Object, e As MouseEventArgs) Handles deviceTree.MouseDown
        ResetPopupMenuItems()
        ProcessNodePopupMenu(deviceTree, e)
    End Sub

    '// Reset all popup menu items to default state before processing them
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
        Me.ActivatePlayerMenu.Visible = False
        Me.removeDeviceMenuItem.Visible = False
    End Sub

    '// the main method to do the hard work of setting up the popup menu (for both managed and unmanaged tree's)
    Private Sub ProcessNodePopupMenu(tree As TreeView, e As MouseEventArgs)
        ResetPopupMenuItems()
        Dim node As TreeNode = tree.GetNodeAt(e.X, e.Y)                                                                         '// get the node
        If node IsNot Nothing Then
            tree.SelectedNode = node
            Dim infoObject As Object = node.Tag
            If infoObject IsNot Nothing Then
                If infoObject.[GetType]() Is GetType(UPnPDevice) Then                                                           '// if the node is a device
                    Dim device As UPnPDevice = disc.GetTopMostDevice(infoObject)
                    If tree Is deviceTree Then                                                                                  '// if its the device tree then build the Add to Managed stuff
                        If (disc.CheckForService(device, discovery.AVTRANSPORT)) And (Not (disc.isManaged(device))) Then
                            Me.ManagedDeviceMenuItem.Visible = True
                            Me.ManagedDeviceSeperatorMenuItem.Visible = True
                            Me.AddManagedDeviceMenuItem.Visible = True

                            If disc.CheckForService(device, discovery.CONTENTDIRECTORY) Then '// It's a complete device
                                AddManagedDeviceMenuItem.Text = COMPLETEDEVICE & device.FriendlyName
                                Dim transportDevice As UPnPDevice = infoObject
                                If disc.CheckChildrenForService(transportDevice, discovery.AVTRANSPORT) Then
                                    transportDevice = disc.GetChildDeviceWithService(transportDevice, discovery.AVTRANSPORT)
                                    AddCompoundDeviceMenuItem.Text = AVTRANSPORTDEVICE & transportDevice.FriendlyName
                                    AddCompoundDeviceMenuItem.Tag = transportDevice
                                    AddCompoundDeviceMenuItem.Visible = True


                                End If
                            Else
                                '// let's find another device in the tree with a compatable IP address and matching service
                                Dim siblingDevice As UPnPDevice = disc.GetSiblingDevice(device, discovery.CONTENTDIRECTORY)
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
                    Else        '// set up the Remove from Managed Devices stuff. Need to find the topmost device of THIS TREE.
                        '// this is harder than it seems - as we may have added a child (AVtransport) or a Complete or a Compound.
                        '// in AVtransport case, we find the device with teh AVTransport and send that through.
                        '// in all other cases we goto the top parent.
                        Dim deviceType As discovery.eManagedDeviceType = discovery.eManagedDeviceType.completeDevice
                        Dim selectedDevice As UPnPDevice = infoObject
                        If selectedDevice.User IsNot Nothing Then deviceType = selectedDevice.User

                        Select Case deviceType
                            Case discovery.eManagedDeviceType.avTranportDevice
                                selectedDevice = disc.GetChildDeviceWithService(selectedDevice, discovery.AVTRANSPORT)
                            Case Else
                                selectedDevice = device             '//the parent
                        End Select
                        Dim topmostNode As TreeNode = node
                        For l = 1 To (node.Level - 1) Step 1
                            topmostNode = topmostNode.Parent
                        Next
                        Me.removeDeviceMenuItem.Visible = True
                        Me.removeDeviceMenuItem.Text = String.Format("Remove '{0}' from Managed Device Collection.", topmostNode.Text)
                        Me.removeDeviceMenuItem.Tag = selectedDevice

                        Me.ActivatePlayerMenu.Visible = True
                        Me.ActivatePlayerMenu.Text = String.Format("Activate Player {{{0}}}", disc.GetTopMostDevice(selectedDevice).FriendlyName)
                        Me.ActivatePlayerMenu.Tag = disc.GetTopMostDevice(selectedDevice)
                    End If

                    If (CType(infoObject, UPnPDevice)).ParentDevice Is Nothing Then
                        'Me.removeDeviceMenuItem.Visible = True
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
                    Me.ActivatePlayerMenu.Visible = True
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

    '// Menu Click - Add a device to Managed Device System
    Private Sub AddManagedDeviceMenuItem_Click(sender As Object, e As EventArgs) Handles AddManagedDeviceMenuItem.Click
        Dim menuItem As MenuItem = sender
        If menuItem.Text.Contains(COMPLETEDEVICE) Then
            disc.AddManagedDevice(menuItem.Tag)
        Else
            disc.AddManagedDevice(menuItem.Tag)
        End If
    End Sub

    '// Menu Click - Special case, Add a "COMPOUND" or "AVTRANSPORT" device to the ManagedDevice System
    Private Sub AddCompoundDeviceMenuItem_Click(sender As Object, e As EventArgs) Handles AddCompoundDeviceMenuItem.Click
        Dim menuItem As MenuItem = sender
        If menuItem.Text.Contains(AVTRANSPORTDEVICE) Then
            disc.AddManagedDevice(menuItem.Tag)
        Else
            disc.AddManagedDevice(menuItem.Tag(0), menuItem.Tag(1))
        End If
    End Sub

    '// Tree Click - Get detailed data on a Tree item
    Private Sub OnSelectedItem(sender As Object, e As TreeViewEventArgs) Handles deviceTree.AfterSelect, ManagedTree.AfterSelect
        Dim tree As TreeView = sender
        Dim node As TreeNode = tree.SelectedNode
        Dim Items As ArrayList = SetListInfo(node.Tag)
        Me.listInfo.Sorting = SortOrder.Ascending
        Me.listInfo.Items.Clear()
        Me.listInfo.Items.AddRange(CType(Items.ToArray(GetType(ListViewItem)), ListViewItem()))
    End Sub

    '// Exit the App
    Private Sub menuExit_Click(sender As Object, e As EventArgs) Handles menuExit.Click
        Me.Close()
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

    '// based on user action, subscribe to the highlighted service
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

    '// remove a device from the managed device tree
    Private Sub removeManagedDeviceMenuItem_Click(sender As Object, e As EventArgs) Handles removeDeviceMenuItem.Click
        Dim menuItem As MenuItem = sender
        If menuItem.GetContextMenu.SourceControl Is deviceTree Then

        Else
            '// it's a remove device event!
            disc.RemoveManagedDevice(menuItem.Tag)
        End If
    End Sub

    '// tab selection (tab managed selected) -- probably need to build this into a more general any tab change handling method
    Private Sub tabManaged_Selected(sender As Object, e As EventArgs) Handles tabControl1.Selected
        Debug.Print("TAB" & tabControl1.SelectedIndex)
        'If tabControl1.SelectedIndex = 1 Then
        '            ElseIf tabControl1.SelectedIndex = 0 Then
        '    '            ManagedTree = TreeView1
        'End If
        Select Case tabControl1.SelectedIndex
            Case 0
                splitter1_3.Panel2.Controls.Add(ManagedTree)
                'ManagedTree.Dock = DockStyle.Fill
                ManagedTree.BringToFront()
            Case 1
                splitter2_1.Panel1.Controls.Add(ManagedTree)
                'ManagedTree.Dock = DockStyle.Fill
                ManagedTree.BringToFront()
        End Select

    End Sub

    '// Kick off an Active Player System
    Private Sub ActivatePlayerMenu_Click(sender As Object, e As EventArgs) Handles ActivatePlayerMenu.Click
        Dim menuItem As MenuItem = sender
        player.SetDevice(menuItem.Tag)
        If debugForm IsNot Nothing Then
            debugForm.ipFilter = menuItem.Tag.RemoteEndpoint.Address.ToString
        End If


    End Sub



    Private Sub mnuGo_Click(sender As Object, e As EventArgs) Handles mnuGo.Click
        '// do a Network scan -- need to add some more thoughtful code. perhaps into the BeginNetworkScan code (ie to check if its already been run).
        disc.BeginNetworkScan()
    End Sub

#End Region

#Region "Form and GUI Events (Player System)"

    Private Sub btnPlay_Click(sender As Object, e As EventArgs) Handles btnPlay.Click
        player.Play()
    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        player.Next()
    End Sub

    Private Sub btnPrev_Click(sender As Object, e As EventArgs) Handles btnPrev.Click
        player.Previous()
    End Sub

    Private Sub btnStop_Click(sender As Object, e As EventArgs) Handles btnStop.Click
        player.Stop()
    End Sub

    Private Sub btnPause_Click(sender As Object, e As EventArgs) Handles btnPause.Click
        player.Pause()
    End Sub



#End Region

#Region "Tree and Other GUI Related Routines"

    Private Sub HighlightSubscribedNode(node As TreeNode, bHighlight As Boolean)
        If bHighlight Then
            Me.splitter1_2.Visible = True
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

    Private Sub UpdateTrackInfo(track As TrackInfoEx, lblTit As Label, lblArt As Label, lblAlb As Label, lblTrk As Label, pic As PictureBox)
        lblTit.Text = track.Title

        If track.AlbumArtist = "" Then
            lblArt.Text = track.Artist
        ElseIf track.Artist = "" Then
            lblArt.Text = track.Artist
        Else
            If track.Artist = track.AlbumArtist Then
                lblArt.Text = track.Artist
            Else
                lblArt.Text = track.Artist & "/" & track.AlbumArtist
            End If
        End If

        lblAlb.Text = track.Album
        lblTrk.Text = track.TrackNumber

        If pic.ImageLocation IsNot Nothing Then
            If pic.ImageLocation <> track.AlbumArtURI Then
                If track.AlbumArtURI <> "" Then pic.ImageLocation = track.AlbumArtURI


            End If
        Else
            If track.AlbumArtURI <> "" Then pic.ImageLocation = track.AlbumArtURI
        End If
        If track.AlbumArtURI <> "" Then TextBox1.Text = track.AlbumArtURI
        Debug.Print("TARGET URI [" & track.AlbumArtURI & "]")
        Dim tt As New ToolTip()
        tt.SetToolTip(pic, track.AlbumArtURI)


    End Sub


#End Region

#Region "Methods invoked by delegate from Callbacks"

    '// this method handles incoming events from a service subscribed from the discovery system (as opposed to the player system)...
    Private Sub HandlesServiceDataReceived(service As UPnPService, sender As UPnPStateVariable, EventValue As Object)

        Dim eventSource As String = service.ParentDevice.FriendlyName + "/" + service.ServiceID
        Dim eventValue1 As String = UPnPService.SerializeObjectInstance(EventValue)
        If eventValue1 = "" Then
            eventValue1 = "(Empty)"
        End If

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

    '// this method adds a device to a visual tree.
    Protected Sub HandlesDeviceDiscovery_AddDevice(device As UPnPDevice, isManagedTree As Boolean)
        Dim parentNode As TreeNode
        If isManagedTree Then
            parentNode = CreateTreeNode(device, False)
        Else
            parentNode = CreateTreeNode(device, True)
        End If

        UpdateTree(device, parentNode, isManagedTree)
        'MyBase.Invoke(New UpdateTreeDelegate(AddressOf Me.UpdateTree), args)
    End Sub

    '// this method removes a device from a tree
    Protected Sub HandlesDeviceDiscovery_RemoveDevice(device As UPnPDevice, isManagedTree As Boolean)
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

    '// this method updates visual elements related to the player object
    Private Sub Handles_PlayerStateChanged(obj As UPNPPlayer)
        propGrid1.SelectedObject = obj

        UpdateTrackInfo(obj.CurrentTrack, lblTitle, lblArtist, lblAlbum, lblTrackNum, picBox)
        UpdateTrackInfo(obj.NextTrack, lblTitleNext, lblArtistNext, lblAlbumNext, lblTrackNumNext, picBoxNext)
        UpdateTrackInfo(obj.PreviousTrack, lblTitlePrev, lblArtistPrev, lblAlbumPrev, lblTrackNumPrev, picBoxPrev)

        lblQueueInfo.Text = "Queue " & obj.PositionInfo.TrackIndex & " of " & obj.MediaInfo.NrTracks

        lblDuration.Text = String.Format("{0}/{1}", obj.CurrentTrackTime, obj.CurrentTrackDuration)
        If obj.PositionInfo.TrackDuration.TotalSeconds > obj.CurrentTrackTime.TotalSeconds Then

            If obj.CurrentTrackDuration.TotalSeconds > 0 Then
                pbDuration.Value = (obj.CurrentTrackTime.TotalSeconds / obj.CurrentTrackDuration.TotalSeconds) * 100
            End If
        End If

        lblDevice.Text = player.Device.FriendlyName
    End Sub

#End Region


    Private Sub propGrid1_Click(sender As Object, e As EventArgs) Handles propGrid1.SelectedGridItemChanged
        Dim item As Object = propGrid1.SelectedGridItem.Value
        If item IsNot Nothing Then
            Try
                propGrid2.SelectedObject = item

            Catch ex As Exception
                Debug.Print("Invalid Property")
            End Try
        End If

    End Sub

    Private Sub propGrid2_Click(sender As Object, e As EventArgs) Handles propGrid2.SelectedGridItemChanged

        If propGrid2.SelectedGridItem.Tag IsNot Nothing Then
            If propGrid2.SelectedGridItem.Tag = propGrid2.SelectedGridItem.Value Then
                Exit Sub
            End If
        End If
        Dim item As Object = propGrid2.SelectedGridItem.Value
        propGrid2.SelectedGridItem.Tag = item
        If item IsNot Nothing Then
            Try
                XML_Coloring(RichTextBox1, item.ToString, 0)
            Catch ex As Exception

            End Try
        End If
    End Sub


    Private Sub lblDevice_Click(sender As Object, e As EventArgs) Handles lblDevice.Click
        player.StopPolling()
    End Sub

    Private Sub showDebugInfoMenuItem_Click(sender As Object, e As EventArgs) Handles showDebugInfoMenuItem.Click
        '// it is available, so kill it.
        If CheckIsDebugVisible() Then
            KillDebug()
            showDebugInfoMenuItem.Checked = False
        Else
            ShowDebug()
            showDebugInfoMenuItem.Checked = True
            '// it's not available - show it!
        End If

    End Sub

    Private Sub picBox_Click(sender As Object, e As EventArgs) Handles picBox.Click
        Clipboard.SetText(picBox.ImageLocation)
    End Sub

    Private Sub RichTextBox1_TextChanged(sender As Object, e As EventArgs) Handles RichTextBox1.Click
        Clipboard.SetText(RichTextBox1.Rtf, TextDataFormat.Rtf)
    End Sub

    Private Sub btnLoadURL_Click(sender As Object, e As EventArgs) Handles btnLoadURL.Click
        PictureBox1.ImageLocation = TextBox1.Text
    End Sub

    Private Sub btnUnencode_Click(sender As Object, e As EventArgs) Handles btnUnencode.Click
        Debug.Print(TextBox1.Text)
        TextBox2.Text = Uri.UnescapeDataString(TextBox1.Text)
        Debug.Print(TextBox2.Text)
    End Sub

    Private Sub btnLoadUnecoded_Click(sender As Object, e As EventArgs) Handles btnLoadUnecoded.Click
        PictureBox1.ImageLocation = TextBox2.Text
    End Sub

    Private Sub btnClearText_Click(sender As Object, e As EventArgs) Handles btnClearText.Click
        TextBox1.Text = ""
        TextBox2.Text = ""
    End Sub
End Class