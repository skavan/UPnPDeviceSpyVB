<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDeviceFinderClean
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDeviceFinderClean))
        Dim ListViewItem2 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem(New String() {""}, -1, System.Drawing.SystemColors.WindowText, System.Drawing.SystemColors.Window, New System.Drawing.Font("Microsoft Sans Serif", 8.25!))
        Me.mainMenu = New System.Windows.Forms.MainMenu(Me.components)
        Me.menuItem1 = New System.Windows.Forms.MenuItem()
        Me.manuallyAddDeviceMenuItem = New System.Windows.Forms.MenuItem()
        Me.menuItem15 = New System.Windows.Forms.MenuItem()
        Me.menuItem12 = New System.Windows.Forms.MenuItem()
        Me.menuItem9 = New System.Windows.Forms.MenuItem()
        Me.menuItem4 = New System.Windows.Forms.MenuItem()
        Me.menuItem13 = New System.Windows.Forms.MenuItem()
        Me.menuItem14 = New System.Windows.Forms.MenuItem()
        Me.menuItem2 = New System.Windows.Forms.MenuItem()
        Me.menuItem7 = New System.Windows.Forms.MenuItem()
        Me.rescanMenuItem = New System.Windows.Forms.MenuItem()
        Me.menuItem19 = New System.Windows.Forms.MenuItem()
        Me.expandAllMenuItem = New System.Windows.Forms.MenuItem()
        Me.collapseAllMenuItem = New System.Windows.Forms.MenuItem()
        Me.menuItem16 = New System.Windows.Forms.MenuItem()
        Me.menuItem3 = New System.Windows.Forms.MenuItem()
        Me.viewStatusbarMenuItem = New System.Windows.Forms.MenuItem()
        Me.menuItem5 = New System.Windows.Forms.MenuItem()
        Me.helpMenuItem = New System.Windows.Forms.MenuItem()
        Me.menuItem6 = New System.Windows.Forms.MenuItem()
        Me.menuItem10 = New System.Windows.Forms.MenuItem()
        Me.showDebugInfoMenuItem = New System.Windows.Forms.MenuItem()
        Me.listInfoContextMenu = New System.Windows.Forms.ContextMenu()
        Me.openWebPageMenuItem = New System.Windows.Forms.MenuItem()
        Me.copyValueCpMenuItem = New System.Windows.Forms.MenuItem()
        Me.copyTableCpMenuItem = New System.Windows.Forms.MenuItem()
        Me.treeImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.deviceContextMenu = New System.Windows.Forms.ContextMenu()
        Me.ManagedDeviceMenuItem = New System.Windows.Forms.MenuItem()
        Me.AddManagedDeviceMenuItem = New System.Windows.Forms.MenuItem()
        Me.AddCompoundDeviceMenuItem = New System.Windows.Forms.MenuItem()
        Me.ManagedDeviceSeperatorMenuItem = New System.Windows.Forms.MenuItem()
        Me.ActivatePlayerMenu = New System.Windows.Forms.MenuItem()
        Me.eventSubscribeMenuItem = New System.Windows.Forms.MenuItem()
        Me.removeDeviceMenuItem = New System.Windows.Forms.MenuItem()
        Me.presPageMenuItem = New System.Windows.Forms.MenuItem()
        Me.invokeActionMenuItem = New System.Windows.Forms.MenuItem()
        Me.DeviceXMLmenuItem = New System.Windows.Forms.MenuItem()
        Me.ServiceXMLmenuItem = New System.Windows.Forms.MenuItem()
        Me.menuItem18 = New System.Windows.Forms.MenuItem()
        Me.expandAllMenuItem2 = New System.Windows.Forms.MenuItem()
        Me.collapseAllMenuItem2 = New System.Windows.Forms.MenuItem()
        Me.menuItem21 = New System.Windows.Forms.MenuItem()
        Me.rescanNetworkMenuItem = New System.Windows.Forms.MenuItem()
        Me.eventListViewContextMenu = New System.Windows.Forms.ContextMenu()
        Me.copyEventCpMenuItem = New System.Windows.Forms.MenuItem()
        Me.copyEventLogCpMenuItem = New System.Windows.Forms.MenuItem()
        Me.menuItem11 = New System.Windows.Forms.MenuItem()
        Me.ClearEventLogMenuItem = New System.Windows.Forms.MenuItem()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.lblStatus = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tabControl1 = New System.Windows.Forms.TabControl()
        Me.tabAvailable = New System.Windows.Forms.TabPage()
        Me.splitter1_1 = New System.Windows.Forms.SplitContainer()
        Me.deviceTree = New System.Windows.Forms.TreeView()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.cmbSearch = New System.Windows.Forms.ToolStripComboBox()
        Me.btnScan = New System.Windows.Forms.ToolStripButton()
        Me.splitter1_2 = New System.Windows.Forms.SplitContainer()
        Me.splitter1_3 = New System.Windows.Forms.SplitContainer()
        Me.listInfo = New System.Windows.Forms.ListView()
        Me.columnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.columnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ManagedTree = New System.Windows.Forms.TreeView()
        Me.ToolStrip2 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripLabel1 = New System.Windows.Forms.ToolStripLabel()
        Me.eventListView = New System.Windows.Forms.ListView()
        Me.columnHeader6 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.columnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.columnHeader4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.columnHeader5 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.tabManaged = New System.Windows.Forms.TabPage()
        Me.splitter2_1 = New System.Windows.Forms.SplitContainer()
        Me.ToolStrip3 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripComboBox1 = New System.Windows.Forms.ToolStripComboBox()
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton()
        Me.Splitter2_2 = New System.Windows.Forms.SplitContainer()
        Me.Splitter2_3 = New System.Windows.Forms.SplitContainer()
        Me.propGrid1 = New System.Windows.Forms.PropertyGrid()
        Me.propGrid2 = New System.Windows.Forms.PropertyGrid()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Splitter3_1 = New System.Windows.Forms.SplitContainer()
        Me.ToolStrip4 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripComboBox2 = New System.Windows.Forms.ToolStripComboBox()
        Me.ToolStripButton2 = New System.Windows.Forms.ToolStripButton()
        Me.Splitter3_2 = New System.Windows.Forms.SplitContainer()
        Me.Splitter3_3 = New System.Windows.Forms.SplitContainer()
        Me.picBox = New System.Windows.Forms.PictureBox()
        Me.StatusStrip2 = New System.Windows.Forms.StatusStrip()
        Me.lblDuration = New System.Windows.Forms.ToolStripStatusLabel()
        Me.pbDuration = New System.Windows.Forms.ToolStripProgressBar()
        Me.lblTrackNum = New System.Windows.Forms.Label()
        Me.lblArtist2 = New System.Windows.Forms.Label()
        Me.lblArtist = New System.Windows.Forms.Label()
        Me.lblAlbum = New System.Windows.Forms.Label()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.lblDevice = New System.Windows.Forms.Label()
        Me.btnStop = New System.Windows.Forms.Button()
        Me.btnPrev = New System.Windows.Forms.Button()
        Me.btnNext = New System.Windows.Forms.Button()
        Me.btnPlay = New System.Windows.Forms.Button()
        Me.imgMediumIcons = New System.Windows.Forms.ImageList(Me.components)
        Me.StatusStrip1.SuspendLayout()
        Me.tabControl1.SuspendLayout()
        Me.tabAvailable.SuspendLayout()
        CType(Me.splitter1_1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.splitter1_1.Panel1.SuspendLayout()
        Me.splitter1_1.Panel2.SuspendLayout()
        Me.splitter1_1.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.splitter1_2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.splitter1_2.Panel1.SuspendLayout()
        Me.splitter1_2.Panel2.SuspendLayout()
        Me.splitter1_2.SuspendLayout()
        CType(Me.splitter1_3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.splitter1_3.Panel1.SuspendLayout()
        Me.splitter1_3.Panel2.SuspendLayout()
        Me.splitter1_3.SuspendLayout()
        Me.ToolStrip2.SuspendLayout()
        Me.tabManaged.SuspendLayout()
        CType(Me.splitter2_1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.splitter2_1.Panel1.SuspendLayout()
        Me.splitter2_1.Panel2.SuspendLayout()
        Me.splitter2_1.SuspendLayout()
        Me.ToolStrip3.SuspendLayout()
        CType(Me.Splitter2_2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Splitter2_2.Panel1.SuspendLayout()
        Me.Splitter2_2.SuspendLayout()
        CType(Me.Splitter2_3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Splitter2_3.Panel1.SuspendLayout()
        Me.Splitter2_3.Panel2.SuspendLayout()
        Me.Splitter2_3.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.Splitter3_1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Splitter3_1.Panel1.SuspendLayout()
        Me.Splitter3_1.Panel2.SuspendLayout()
        Me.Splitter3_1.SuspendLayout()
        Me.ToolStrip4.SuspendLayout()
        CType(Me.Splitter3_2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Splitter3_2.Panel1.SuspendLayout()
        Me.Splitter3_2.Panel2.SuspendLayout()
        Me.Splitter3_2.SuspendLayout()
        CType(Me.Splitter3_3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Splitter3_3.Panel1.SuspendLayout()
        Me.Splitter3_3.Panel2.SuspendLayout()
        Me.Splitter3_3.SuspendLayout()
        CType(Me.picBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.StatusStrip2.SuspendLayout()
        Me.SuspendLayout()
        '
        'mainMenu
        '
        Me.mainMenu.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.menuItem1, Me.menuItem7, Me.menuItem5})
        '
        'menuItem1
        '
        Me.menuItem1.Index = 0
        Me.menuItem1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.manuallyAddDeviceMenuItem, Me.menuItem15, Me.menuItem12, Me.menuItem9, Me.menuItem4, Me.menuItem13, Me.menuItem14, Me.menuItem2})
        Me.menuItem1.Text = "&File"
        '
        'manuallyAddDeviceMenuItem
        '
        Me.manuallyAddDeviceMenuItem.Index = 0
        Me.manuallyAddDeviceMenuItem.Text = "Manually Add Device"
        '
        'menuItem15
        '
        Me.menuItem15.Index = 1
        Me.menuItem15.Text = "-"
        '
        'menuItem12
        '
        Me.menuItem12.Index = 2
        Me.menuItem12.Text = "Copy &information table to clipboard"
        '
        'menuItem9
        '
        Me.menuItem9.Index = 3
        Me.menuItem9.Text = "Copy &event log to clipboard"
        '
        'menuItem4
        '
        Me.menuItem4.Index = 4
        Me.menuItem4.Text = "-"
        '
        'menuItem13
        '
        Me.menuItem13.Index = 5
        Me.menuItem13.Text = "&Clear Event Log"
        '
        'menuItem14
        '
        Me.menuItem14.Index = 6
        Me.menuItem14.Text = "-"
        '
        'menuItem2
        '
        Me.menuItem2.Index = 7
        Me.menuItem2.Text = "E&xit"
        '
        'menuItem7
        '
        Me.menuItem7.Index = 1
        Me.menuItem7.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.rescanMenuItem, Me.menuItem19, Me.expandAllMenuItem, Me.collapseAllMenuItem, Me.menuItem16, Me.menuItem3, Me.viewStatusbarMenuItem})
        Me.menuItem7.Text = "&View"
        '
        'rescanMenuItem
        '
        Me.rescanMenuItem.Index = 0
        Me.rescanMenuItem.Shortcut = System.Windows.Forms.Shortcut.F5
        Me.rescanMenuItem.Text = "Rescan network"
        '
        'menuItem19
        '
        Me.menuItem19.Index = 1
        Me.menuItem19.Text = "-"
        '
        'expandAllMenuItem
        '
        Me.expandAllMenuItem.Index = 2
        Me.expandAllMenuItem.Text = "&Expand all devices"
        '
        'collapseAllMenuItem
        '
        Me.collapseAllMenuItem.Index = 3
        Me.collapseAllMenuItem.Text = "&Collapse all devices"
        '
        'menuItem16
        '
        Me.menuItem16.Index = 4
        Me.menuItem16.Text = "-"
        '
        'menuItem3
        '
        Me.menuItem3.Index = 5
        Me.menuItem3.Text = "Event &log"
        '
        'viewStatusbarMenuItem
        '
        Me.viewStatusbarMenuItem.Checked = True
        Me.viewStatusbarMenuItem.Index = 6
        Me.viewStatusbarMenuItem.Text = "Status &bar"
        '
        'menuItem5
        '
        Me.menuItem5.Index = 2
        Me.menuItem5.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.helpMenuItem, Me.menuItem6, Me.menuItem10, Me.showDebugInfoMenuItem})
        Me.menuItem5.Text = "&Help"
        '
        'helpMenuItem
        '
        Me.helpMenuItem.Index = 0
        Me.helpMenuItem.Shortcut = System.Windows.Forms.Shortcut.F1
        Me.helpMenuItem.Text = "&Help Topics"
        '
        'menuItem6
        '
        Me.menuItem6.Index = 1
        Me.menuItem6.Text = "&Check for updates"
        '
        'menuItem10
        '
        Me.menuItem10.Index = 2
        Me.menuItem10.Text = "-"
        '
        'showDebugInfoMenuItem
        '
        Me.showDebugInfoMenuItem.Index = 3
        Me.showDebugInfoMenuItem.Text = "&Show Debug Information"
        '
        'listInfoContextMenu
        '
        Me.listInfoContextMenu.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.openWebPageMenuItem, Me.copyValueCpMenuItem, Me.copyTableCpMenuItem})
        '
        'openWebPageMenuItem
        '
        Me.openWebPageMenuItem.DefaultItem = True
        Me.openWebPageMenuItem.Index = 0
        Me.openWebPageMenuItem.Text = "&Open Web Page"
        '
        'copyValueCpMenuItem
        '
        Me.copyValueCpMenuItem.Index = 1
        Me.copyValueCpMenuItem.Text = "Copy &Value to Clipboard"
        '
        'copyTableCpMenuItem
        '
        Me.copyTableCpMenuItem.Index = 2
        Me.copyTableCpMenuItem.Text = "Copy &Table to Clipboard"
        '
        'treeImageList
        '
        Me.treeImageList.ImageStream = CType(resources.GetObject("treeImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.treeImageList.TransparentColor = System.Drawing.Color.Transparent
        Me.treeImageList.Images.SetKeyName(0, "")
        Me.treeImageList.Images.SetKeyName(1, "")
        Me.treeImageList.Images.SetKeyName(2, "")
        Me.treeImageList.Images.SetKeyName(3, "")
        Me.treeImageList.Images.SetKeyName(4, "")
        Me.treeImageList.Images.SetKeyName(5, "")
        Me.treeImageList.Images.SetKeyName(6, "")
        Me.treeImageList.Images.SetKeyName(7, "")
        '
        'deviceContextMenu
        '
        Me.deviceContextMenu.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.ManagedDeviceMenuItem, Me.ManagedDeviceSeperatorMenuItem, Me.ActivatePlayerMenu, Me.eventSubscribeMenuItem, Me.removeDeviceMenuItem, Me.presPageMenuItem, Me.invokeActionMenuItem, Me.DeviceXMLmenuItem, Me.ServiceXMLmenuItem, Me.menuItem18, Me.expandAllMenuItem2, Me.collapseAllMenuItem2, Me.menuItem21, Me.rescanNetworkMenuItem})
        '
        'ManagedDeviceMenuItem
        '
        Me.ManagedDeviceMenuItem.Index = 0
        Me.ManagedDeviceMenuItem.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.AddManagedDeviceMenuItem, Me.AddCompoundDeviceMenuItem})
        Me.ManagedDeviceMenuItem.Text = "Add to Managed Devices"
        '
        'AddManagedDeviceMenuItem
        '
        Me.AddManagedDeviceMenuItem.Index = 0
        Me.AddManagedDeviceMenuItem.Text = "Transport/Full"
        '
        'AddCompoundDeviceMenuItem
        '
        Me.AddCompoundDeviceMenuItem.Index = 1
        Me.AddCompoundDeviceMenuItem.Text = "Compound"
        '
        'ManagedDeviceSeperatorMenuItem
        '
        Me.ManagedDeviceSeperatorMenuItem.Index = 1
        Me.ManagedDeviceSeperatorMenuItem.Text = "-"
        '
        'ActivatePlayerMenu
        '
        Me.ActivatePlayerMenu.Index = 2
        Me.ActivatePlayerMenu.Text = "Activate Player"
        '
        'eventSubscribeMenuItem
        '
        Me.eventSubscribeMenuItem.DefaultItem = True
        Me.eventSubscribeMenuItem.Index = 3
        Me.eventSubscribeMenuItem.Text = "Subscribe to Events"
        '
        'removeDeviceMenuItem
        '
        Me.removeDeviceMenuItem.Index = 4
        Me.removeDeviceMenuItem.Text = "Remove Device"
        '
        'presPageMenuItem
        '
        Me.presPageMenuItem.DefaultItem = True
        Me.presPageMenuItem.Index = 5
        Me.presPageMenuItem.Text = "Display Presentation Page"
        '
        'invokeActionMenuItem
        '
        Me.invokeActionMenuItem.DefaultItem = True
        Me.invokeActionMenuItem.Index = 6
        Me.invokeActionMenuItem.Text = "Invoke Action"
        '
        'DeviceXMLmenuItem
        '
        Me.DeviceXMLmenuItem.Index = 7
        Me.DeviceXMLmenuItem.Text = "Get Device XML"
        '
        'ServiceXMLmenuItem
        '
        Me.ServiceXMLmenuItem.Index = 8
        Me.ServiceXMLmenuItem.Text = "Get Service XML"
        '
        'menuItem18
        '
        Me.menuItem18.Index = 9
        Me.menuItem18.Text = "-"
        '
        'expandAllMenuItem2
        '
        Me.expandAllMenuItem2.Index = 10
        Me.expandAllMenuItem2.Text = "&Expand all devices"
        '
        'collapseAllMenuItem2
        '
        Me.collapseAllMenuItem2.Index = 11
        Me.collapseAllMenuItem2.Text = "&Collapse all devices"
        '
        'menuItem21
        '
        Me.menuItem21.Index = 12
        Me.menuItem21.Text = "-"
        '
        'rescanNetworkMenuItem
        '
        Me.rescanNetworkMenuItem.Index = 13
        Me.rescanNetworkMenuItem.Text = "Rescan network"
        '
        'eventListViewContextMenu
        '
        Me.eventListViewContextMenu.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.copyEventCpMenuItem, Me.copyEventLogCpMenuItem, Me.menuItem11, Me.ClearEventLogMenuItem})
        '
        'copyEventCpMenuItem
        '
        Me.copyEventCpMenuItem.DefaultItem = True
        Me.copyEventCpMenuItem.Index = 0
        Me.copyEventCpMenuItem.Text = "Copy &Event to Clipboard"
        '
        'copyEventLogCpMenuItem
        '
        Me.copyEventLogCpMenuItem.Index = 1
        Me.copyEventLogCpMenuItem.Text = "Copy Event &Log to Clipboard"
        '
        'menuItem11
        '
        Me.menuItem11.Index = 2
        Me.menuItem11.Text = "-"
        '
        'ClearEventLogMenuItem
        '
        Me.ClearEventLogMenuItem.Index = 3
        Me.ClearEventLogMenuItem.Text = "&Clear Event Log"
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblStatus})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 764)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(1286, 30)
        Me.StatusStrip1.TabIndex = 0
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'lblStatus
        '
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(184, 25)
        Me.lblStatus.Text = "ToolStripStatusLabel1"
        '
        'tabControl1
        '
        Me.tabControl1.Controls.Add(Me.tabAvailable)
        Me.tabControl1.Controls.Add(Me.tabManaged)
        Me.tabControl1.Controls.Add(Me.TabPage1)
        Me.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tabControl1.Location = New System.Drawing.Point(0, 0)
        Me.tabControl1.Name = "tabControl1"
        Me.tabControl1.SelectedIndex = 0
        Me.tabControl1.Size = New System.Drawing.Size(1286, 764)
        Me.tabControl1.TabIndex = 1
        '
        'tabAvailable
        '
        Me.tabAvailable.BackColor = System.Drawing.SystemColors.Control
        Me.tabAvailable.Controls.Add(Me.splitter1_1)
        Me.tabAvailable.Location = New System.Drawing.Point(4, 37)
        Me.tabAvailable.Name = "tabAvailable"
        Me.tabAvailable.Padding = New System.Windows.Forms.Padding(3)
        Me.tabAvailable.Size = New System.Drawing.Size(1278, 723)
        Me.tabAvailable.TabIndex = 0
        Me.tabAvailable.Text = "Available UPnP Devices"
        '
        'splitter1_1
        '
        Me.splitter1_1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.splitter1_1.Location = New System.Drawing.Point(3, 3)
        Me.splitter1_1.Name = "splitter1_1"
        '
        'splitter1_1.Panel1
        '
        Me.splitter1_1.Panel1.Controls.Add(Me.deviceTree)
        Me.splitter1_1.Panel1.Controls.Add(Me.ToolStrip1)
        '
        'splitter1_1.Panel2
        '
        Me.splitter1_1.Panel2.Controls.Add(Me.splitter1_2)
        Me.splitter1_1.Size = New System.Drawing.Size(1272, 717)
        Me.splitter1_1.SplitterDistance = 394
        Me.splitter1_1.TabIndex = 1
        '
        'deviceTree
        '
        Me.deviceTree.BackColor = System.Drawing.Color.White
        Me.deviceTree.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.deviceTree.ContextMenu = Me.deviceContextMenu
        Me.deviceTree.Dock = System.Windows.Forms.DockStyle.Fill
        Me.deviceTree.ImageIndex = 0
        Me.deviceTree.ImageList = Me.treeImageList
        Me.deviceTree.Indent = 19
        Me.deviceTree.ItemHeight = 16
        Me.deviceTree.Location = New System.Drawing.Point(0, 30)
        Me.deviceTree.Name = "deviceTree"
        Me.deviceTree.SelectedImageIndex = 0
        Me.deviceTree.Size = New System.Drawing.Size(394, 687)
        Me.deviceTree.TabIndex = 14
        '
        'ToolStrip1
        '
        Me.ToolStrip1.AutoSize = False
        Me.ToolStrip1.CanOverflow = False
        Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmbSearch, Me.btnScan})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Padding = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.ToolStrip1.Size = New System.Drawing.Size(394, 30)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'cmbSearch
        '
        Me.cmbSearch.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.cmbSearch.AutoSize = False
        Me.cmbSearch.Name = "cmbSearch"
        Me.cmbSearch.Size = New System.Drawing.Size(200, 33)
        '
        'btnScan
        '
        Me.btnScan.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.btnScan.Image = CType(resources.GetObject("btnScan.Image"), System.Drawing.Image)
        Me.btnScan.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnScan.Name = "btnScan"
        Me.btnScan.Size = New System.Drawing.Size(146, 27)
        Me.btnScan.Text = "Scan for Devices"
        '
        'splitter1_2
        '
        Me.splitter1_2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.splitter1_2.Location = New System.Drawing.Point(0, 0)
        Me.splitter1_2.Name = "splitter1_2"
        Me.splitter1_2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'splitter1_2.Panel1
        '
        Me.splitter1_2.Panel1.Controls.Add(Me.splitter1_3)
        '
        'splitter1_2.Panel2
        '
        Me.splitter1_2.Panel2.Controls.Add(Me.eventListView)
        Me.splitter1_2.Size = New System.Drawing.Size(874, 717)
        Me.splitter1_2.SplitterDistance = 400
        Me.splitter1_2.TabIndex = 0
        '
        'splitter1_3
        '
        Me.splitter1_3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.splitter1_3.Location = New System.Drawing.Point(0, 0)
        Me.splitter1_3.Name = "splitter1_3"
        '
        'splitter1_3.Panel1
        '
        Me.splitter1_3.Panel1.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.splitter1_3.Panel1.Controls.Add(Me.listInfo)
        Me.splitter1_3.Panel1.Padding = New System.Windows.Forms.Padding(0, 6, 0, 0)
        '
        'splitter1_3.Panel2
        '
        Me.splitter1_3.Panel2.Controls.Add(Me.ManagedTree)
        Me.splitter1_3.Panel2.Controls.Add(Me.ToolStrip2)
        Me.splitter1_3.Size = New System.Drawing.Size(874, 400)
        Me.splitter1_3.SplitterDistance = 480
        Me.splitter1_3.TabIndex = 0
        '
        'listInfo
        '
        Me.listInfo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.listInfo.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.columnHeader1, Me.columnHeader2})
        Me.listInfo.ContextMenu = Me.listInfoContextMenu
        Me.listInfo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.listInfo.FullRowSelect = True
        Me.listInfo.Items.AddRange(New System.Windows.Forms.ListViewItem() {ListViewItem2})
        Me.listInfo.Location = New System.Drawing.Point(0, 6)
        Me.listInfo.Name = "listInfo"
        Me.listInfo.Size = New System.Drawing.Size(480, 394)
        Me.listInfo.TabIndex = 17
        Me.listInfo.UseCompatibleStateImageBehavior = False
        Me.listInfo.View = System.Windows.Forms.View.Details
        '
        'columnHeader1
        '
        Me.columnHeader1.Text = "Name"
        Me.columnHeader1.Width = 111
        '
        'columnHeader2
        '
        Me.columnHeader2.Text = "Value"
        Me.columnHeader2.Width = 350
        '
        'ManagedTree
        '
        Me.ManagedTree.BackColor = System.Drawing.Color.White
        Me.ManagedTree.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ManagedTree.ContextMenu = Me.deviceContextMenu
        Me.ManagedTree.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ManagedTree.ImageIndex = 0
        Me.ManagedTree.ImageList = Me.treeImageList
        Me.ManagedTree.Indent = 19
        Me.ManagedTree.ItemHeight = 16
        Me.ManagedTree.Location = New System.Drawing.Point(0, 30)
        Me.ManagedTree.Name = "ManagedTree"
        Me.ManagedTree.SelectedImageIndex = 0
        Me.ManagedTree.Size = New System.Drawing.Size(390, 370)
        Me.ManagedTree.TabIndex = 15
        '
        'ToolStrip2
        '
        Me.ToolStrip2.AutoSize = False
        Me.ToolStrip2.CanOverflow = False
        Me.ToolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripLabel1})
        Me.ToolStrip2.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip2.Name = "ToolStrip2"
        Me.ToolStrip2.Size = New System.Drawing.Size(390, 30)
        Me.ToolStrip2.TabIndex = 0
        Me.ToolStrip2.Text = "ToolStrip2"
        '
        'ToolStripLabel1
        '
        Me.ToolStripLabel1.Name = "ToolStripLabel1"
        Me.ToolStripLabel1.Size = New System.Drawing.Size(156, 27)
        Me.ToolStripLabel1.Text = "Managed Devices:"
        Me.ToolStripLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'eventListView
        '
        Me.eventListView.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.eventListView.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.columnHeader6, Me.columnHeader3, Me.columnHeader4, Me.columnHeader5})
        Me.eventListView.ContextMenu = Me.eventListViewContextMenu
        Me.eventListView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.eventListView.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.eventListView.FullRowSelect = True
        Me.eventListView.Location = New System.Drawing.Point(0, 0)
        Me.eventListView.Name = "eventListView"
        Me.eventListView.ShowItemToolTips = True
        Me.eventListView.Size = New System.Drawing.Size(874, 313)
        Me.eventListView.TabIndex = 18
        Me.eventListView.UseCompatibleStateImageBehavior = False
        Me.eventListView.View = System.Windows.Forms.View.Details
        '
        'columnHeader6
        '
        Me.columnHeader6.Text = "Time"
        Me.columnHeader6.Width = 54
        '
        'columnHeader3
        '
        Me.columnHeader3.Text = "Event Source"
        Me.columnHeader3.Width = 148
        '
        'columnHeader4
        '
        Me.columnHeader4.Text = "State Variable"
        Me.columnHeader4.Width = 126
        '
        'columnHeader5
        '
        Me.columnHeader5.Text = "Value"
        Me.columnHeader5.Width = 128
        '
        'tabManaged
        '
        Me.tabManaged.BackColor = System.Drawing.SystemColors.Control
        Me.tabManaged.Controls.Add(Me.splitter2_1)
        Me.tabManaged.Location = New System.Drawing.Point(4, 37)
        Me.tabManaged.Name = "tabManaged"
        Me.tabManaged.Padding = New System.Windows.Forms.Padding(3)
        Me.tabManaged.Size = New System.Drawing.Size(1278, 723)
        Me.tabManaged.TabIndex = 1
        Me.tabManaged.Text = "Managed Devices"
        '
        'splitter2_1
        '
        Me.splitter2_1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.splitter2_1.Location = New System.Drawing.Point(3, 3)
        Me.splitter2_1.Name = "splitter2_1"
        '
        'splitter2_1.Panel1
        '
        Me.splitter2_1.Panel1.Controls.Add(Me.ToolStrip3)
        '
        'splitter2_1.Panel2
        '
        Me.splitter2_1.Panel2.Controls.Add(Me.Splitter2_2)
        Me.splitter2_1.Size = New System.Drawing.Size(1272, 717)
        Me.splitter2_1.SplitterDistance = 394
        Me.splitter2_1.TabIndex = 2
        '
        'ToolStrip3
        '
        Me.ToolStrip3.AutoSize = False
        Me.ToolStrip3.CanOverflow = False
        Me.ToolStrip3.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip3.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripComboBox1, Me.ToolStripButton1})
        Me.ToolStrip3.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip3.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.ToolStrip3.Name = "ToolStrip3"
        Me.ToolStrip3.Padding = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.ToolStrip3.Size = New System.Drawing.Size(394, 30)
        Me.ToolStrip3.TabIndex = 0
        Me.ToolStrip3.Text = "ToolStrip3"
        '
        'ToolStripComboBox1
        '
        Me.ToolStripComboBox1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripComboBox1.AutoSize = False
        Me.ToolStripComboBox1.Name = "ToolStripComboBox1"
        Me.ToolStripComboBox1.Size = New System.Drawing.Size(200, 33)
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ToolStripButton1.Image = CType(resources.GetObject("ToolStripButton1.Image"), System.Drawing.Image)
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(146, 27)
        Me.ToolStripButton1.Text = "Scan for Devices"
        '
        'Splitter2_2
        '
        Me.Splitter2_2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Splitter2_2.Location = New System.Drawing.Point(0, 0)
        Me.Splitter2_2.Name = "Splitter2_2"
        Me.Splitter2_2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'Splitter2_2.Panel1
        '
        Me.Splitter2_2.Panel1.Controls.Add(Me.Splitter2_3)
        Me.Splitter2_2.Size = New System.Drawing.Size(874, 717)
        Me.Splitter2_2.SplitterDistance = 550
        Me.Splitter2_2.TabIndex = 0
        '
        'Splitter2_3
        '
        Me.Splitter2_3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Splitter2_3.Location = New System.Drawing.Point(0, 0)
        Me.Splitter2_3.Name = "Splitter2_3"
        '
        'Splitter2_3.Panel1
        '
        Me.Splitter2_3.Panel1.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Splitter2_3.Panel1.Controls.Add(Me.propGrid1)
        '
        'Splitter2_3.Panel2
        '
        Me.Splitter2_3.Panel2.Controls.Add(Me.propGrid2)
        Me.Splitter2_3.Size = New System.Drawing.Size(874, 550)
        Me.Splitter2_3.SplitterDistance = 480
        Me.Splitter2_3.TabIndex = 0
        '
        'propGrid1
        '
        Me.propGrid1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.propGrid1.Location = New System.Drawing.Point(0, 0)
        Me.propGrid1.Name = "propGrid1"
        Me.propGrid1.Size = New System.Drawing.Size(480, 550)
        Me.propGrid1.TabIndex = 0
        '
        'propGrid2
        '
        Me.propGrid2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.propGrid2.Location = New System.Drawing.Point(0, 0)
        Me.propGrid2.Name = "propGrid2"
        Me.propGrid2.Size = New System.Drawing.Size(390, 550)
        Me.propGrid2.TabIndex = 1
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.SystemColors.Control
        Me.TabPage1.Controls.Add(Me.Splitter3_1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 37)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(1278, 723)
        Me.TabPage1.TabIndex = 2
        Me.TabPage1.Text = "Player"
        '
        'Splitter3_1
        '
        Me.Splitter3_1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Splitter3_1.Location = New System.Drawing.Point(3, 3)
        Me.Splitter3_1.Name = "Splitter3_1"
        '
        'Splitter3_1.Panel1
        '
        Me.Splitter3_1.Panel1.Controls.Add(Me.ToolStrip4)
        '
        'Splitter3_1.Panel2
        '
        Me.Splitter3_1.Panel2.Controls.Add(Me.Splitter3_2)
        Me.Splitter3_1.Size = New System.Drawing.Size(1272, 717)
        Me.Splitter3_1.SplitterDistance = 394
        Me.Splitter3_1.TabIndex = 3
        '
        'ToolStrip4
        '
        Me.ToolStrip4.AutoSize = False
        Me.ToolStrip4.CanOverflow = False
        Me.ToolStrip4.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip4.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripComboBox2, Me.ToolStripButton2})
        Me.ToolStrip4.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip4.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.ToolStrip4.Name = "ToolStrip4"
        Me.ToolStrip4.Padding = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.ToolStrip4.Size = New System.Drawing.Size(394, 30)
        Me.ToolStrip4.TabIndex = 0
        Me.ToolStrip4.Text = "ToolStrip4"
        '
        'ToolStripComboBox2
        '
        Me.ToolStripComboBox2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripComboBox2.AutoSize = False
        Me.ToolStripComboBox2.Name = "ToolStripComboBox2"
        Me.ToolStripComboBox2.Size = New System.Drawing.Size(200, 33)
        '
        'ToolStripButton2
        '
        Me.ToolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ToolStripButton2.Image = CType(resources.GetObject("ToolStripButton2.Image"), System.Drawing.Image)
        Me.ToolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton2.Name = "ToolStripButton2"
        Me.ToolStripButton2.Size = New System.Drawing.Size(146, 27)
        Me.ToolStripButton2.Text = "Scan for Devices"
        '
        'Splitter3_2
        '
        Me.Splitter3_2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Splitter3_2.Location = New System.Drawing.Point(0, 0)
        Me.Splitter3_2.Name = "Splitter3_2"
        Me.Splitter3_2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'Splitter3_2.Panel1
        '
        Me.Splitter3_2.Panel1.Controls.Add(Me.Splitter3_3)
        '
        'Splitter3_2.Panel2
        '
        Me.Splitter3_2.Panel2.Controls.Add(Me.lblDevice)
        Me.Splitter3_2.Panel2.Controls.Add(Me.btnStop)
        Me.Splitter3_2.Panel2.Controls.Add(Me.btnPrev)
        Me.Splitter3_2.Panel2.Controls.Add(Me.btnNext)
        Me.Splitter3_2.Panel2.Controls.Add(Me.btnPlay)
        Me.Splitter3_2.Size = New System.Drawing.Size(874, 717)
        Me.Splitter3_2.SplitterDistance = 473
        Me.Splitter3_2.TabIndex = 0
        '
        'Splitter3_3
        '
        Me.Splitter3_3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Splitter3_3.Location = New System.Drawing.Point(0, 0)
        Me.Splitter3_3.Name = "Splitter3_3"
        '
        'Splitter3_3.Panel1
        '
        Me.Splitter3_3.Panel1.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Splitter3_3.Panel1.Controls.Add(Me.picBox)
        Me.Splitter3_3.Panel1.Controls.Add(Me.StatusStrip2)
        '
        'Splitter3_3.Panel2
        '
        Me.Splitter3_3.Panel2.Controls.Add(Me.lblTrackNum)
        Me.Splitter3_3.Panel2.Controls.Add(Me.lblArtist2)
        Me.Splitter3_3.Panel2.Controls.Add(Me.lblArtist)
        Me.Splitter3_3.Panel2.Controls.Add(Me.lblAlbum)
        Me.Splitter3_3.Panel2.Controls.Add(Me.lblTitle)
        Me.Splitter3_3.Size = New System.Drawing.Size(874, 473)
        Me.Splitter3_3.SplitterDistance = 480
        Me.Splitter3_3.TabIndex = 0
        '
        'picBox
        '
        Me.picBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.picBox.Location = New System.Drawing.Point(0, 0)
        Me.picBox.Name = "picBox"
        Me.picBox.Size = New System.Drawing.Size(480, 443)
        Me.picBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picBox.TabIndex = 2
        Me.picBox.TabStop = False
        '
        'StatusStrip2
        '
        Me.StatusStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblDuration, Me.pbDuration})
        Me.StatusStrip2.Location = New System.Drawing.Point(0, 443)
        Me.StatusStrip2.Name = "StatusStrip2"
        Me.StatusStrip2.Size = New System.Drawing.Size(480, 30)
        Me.StatusStrip2.SizingGrip = False
        Me.StatusStrip2.TabIndex = 1
        Me.StatusStrip2.Text = "StatusStrip2"
        '
        'lblDuration
        '
        Me.lblDuration.AutoSize = False
        Me.lblDuration.Name = "lblDuration"
        Me.lblDuration.Size = New System.Drawing.Size(163, 25)
        Me.lblDuration.Spring = True
        Me.lblDuration.Text = "ToolStripStatusLabel1"
        '
        'pbDuration
        '
        Me.pbDuration.Name = "pbDuration"
        Me.pbDuration.Size = New System.Drawing.Size(300, 24)
        Me.pbDuration.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        '
        'lblTrackNum
        '
        Me.lblTrackNum.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTrackNum.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTrackNum.Location = New System.Drawing.Point(5, 437)
        Me.lblTrackNum.Name = "lblTrackNum"
        Me.lblTrackNum.Size = New System.Drawing.Size(374, 36)
        Me.lblTrackNum.TabIndex = 4
        Me.lblTrackNum.Text = "Artist 2"
        Me.lblTrackNum.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblArtist2
        '
        Me.lblArtist2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblArtist2.Font = New System.Drawing.Font("Segoe UI", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblArtist2.Location = New System.Drawing.Point(3, 168)
        Me.lblArtist2.Name = "lblArtist2"
        Me.lblArtist2.Size = New System.Drawing.Size(382, 53)
        Me.lblArtist2.TabIndex = 3
        Me.lblArtist2.Text = "Artist 2"
        Me.lblArtist2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblArtist
        '
        Me.lblArtist.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblArtist.Font = New System.Drawing.Font("Segoe UI", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblArtist.Location = New System.Drawing.Point(3, 115)
        Me.lblArtist.Name = "lblArtist"
        Me.lblArtist.Size = New System.Drawing.Size(382, 53)
        Me.lblArtist.TabIndex = 2
        Me.lblArtist.Text = "Artist"
        Me.lblArtist.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblAlbum
        '
        Me.lblAlbum.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblAlbum.Font = New System.Drawing.Font("Segoe UI", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAlbum.Location = New System.Drawing.Point(3, 62)
        Me.lblAlbum.Name = "lblAlbum"
        Me.lblAlbum.Size = New System.Drawing.Size(382, 53)
        Me.lblAlbum.TabIndex = 1
        Me.lblAlbum.Text = "Album Title"
        Me.lblAlbum.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTitle
        '
        Me.lblTitle.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitle.Location = New System.Drawing.Point(3, 9)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(382, 53)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "Track Title"
        Me.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblDevice
        '
        Me.lblDevice.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblDevice.Font = New System.Drawing.Font("Segoe UI", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDevice.Location = New System.Drawing.Point(99, 154)
        Me.lblDevice.Name = "lblDevice"
        Me.lblDevice.Size = New System.Drawing.Size(671, 53)
        Me.lblDevice.TabIndex = 4
        Me.lblDevice.Text = "Artist 2"
        Me.lblDevice.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnStop
        '
        Me.btnStop.Location = New System.Drawing.Point(271, 16)
        Me.btnStop.Name = "btnStop"
        Me.btnStop.Size = New System.Drawing.Size(155, 90)
        Me.btnStop.TabIndex = 3
        Me.btnStop.Text = "Stop"
        Me.btnStop.UseVisualStyleBackColor = True
        '
        'btnPrev
        '
        Me.btnPrev.Location = New System.Drawing.Point(99, 16)
        Me.btnPrev.Name = "btnPrev"
        Me.btnPrev.Size = New System.Drawing.Size(155, 90)
        Me.btnPrev.TabIndex = 2
        Me.btnPrev.Text = "Previous"
        Me.btnPrev.UseVisualStyleBackColor = True
        '
        'btnNext
        '
        Me.btnNext.Location = New System.Drawing.Point(615, 16)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.Size = New System.Drawing.Size(155, 90)
        Me.btnNext.TabIndex = 1
        Me.btnNext.Text = "Next"
        Me.btnNext.UseVisualStyleBackColor = True
        '
        'btnPlay
        '
        Me.btnPlay.Location = New System.Drawing.Point(443, 16)
        Me.btnPlay.Name = "btnPlay"
        Me.btnPlay.Size = New System.Drawing.Size(155, 90)
        Me.btnPlay.TabIndex = 0
        Me.btnPlay.Text = "Play"
        Me.btnPlay.UseVisualStyleBackColor = True
        '
        'imgMediumIcons
        '
        Me.imgMediumIcons.ImageStream = CType(resources.GetObject("imgMediumIcons.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgMediumIcons.TransparentColor = System.Drawing.Color.Transparent
        Me.imgMediumIcons.Images.SetKeyName(0, "address-book-new-4.ico")
        Me.imgMediumIcons.Images.SetKeyName(1, "align-horizontal-top-out.ico")
        Me.imgMediumIcons.Images.SetKeyName(2, "bookmark-new-3.ico")
        Me.imgMediumIcons.Images.SetKeyName(3, "bookmark-new-list-2.ico")
        Me.imgMediumIcons.Images.SetKeyName(4, "contact-new-2.ico")
        '
        'frmDeviceFinderClean
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(11.0!, 28.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1286, 794)
        Me.Controls.Add(Me.tabControl1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Menu = Me.mainMenu
        Me.Name = "frmDeviceFinderClean"
        Me.Text = "frmDeviceFinder"
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.tabControl1.ResumeLayout(False)
        Me.tabAvailable.ResumeLayout(False)
        Me.splitter1_1.Panel1.ResumeLayout(False)
        Me.splitter1_1.Panel2.ResumeLayout(False)
        CType(Me.splitter1_1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splitter1_1.ResumeLayout(False)
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.splitter1_2.Panel1.ResumeLayout(False)
        Me.splitter1_2.Panel2.ResumeLayout(False)
        CType(Me.splitter1_2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splitter1_2.ResumeLayout(False)
        Me.splitter1_3.Panel1.ResumeLayout(False)
        Me.splitter1_3.Panel2.ResumeLayout(False)
        CType(Me.splitter1_3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splitter1_3.ResumeLayout(False)
        Me.ToolStrip2.ResumeLayout(False)
        Me.ToolStrip2.PerformLayout()
        Me.tabManaged.ResumeLayout(False)
        Me.splitter2_1.Panel1.ResumeLayout(False)
        Me.splitter2_1.Panel2.ResumeLayout(False)
        CType(Me.splitter2_1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splitter2_1.ResumeLayout(False)
        Me.ToolStrip3.ResumeLayout(False)
        Me.ToolStrip3.PerformLayout()
        Me.Splitter2_2.Panel1.ResumeLayout(False)
        CType(Me.Splitter2_2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Splitter2_2.ResumeLayout(False)
        Me.Splitter2_3.Panel1.ResumeLayout(False)
        Me.Splitter2_3.Panel2.ResumeLayout(False)
        CType(Me.Splitter2_3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Splitter2_3.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.Splitter3_1.Panel1.ResumeLayout(False)
        Me.Splitter3_1.Panel2.ResumeLayout(False)
        CType(Me.Splitter3_1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Splitter3_1.ResumeLayout(False)
        Me.ToolStrip4.ResumeLayout(False)
        Me.ToolStrip4.PerformLayout()
        Me.Splitter3_2.Panel1.ResumeLayout(False)
        Me.Splitter3_2.Panel2.ResumeLayout(False)
        CType(Me.Splitter3_2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Splitter3_2.ResumeLayout(False)
        Me.Splitter3_3.Panel1.ResumeLayout(False)
        Me.Splitter3_3.Panel1.PerformLayout()
        Me.Splitter3_3.Panel2.ResumeLayout(False)
        CType(Me.Splitter3_3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Splitter3_3.ResumeLayout(False)
        CType(Me.picBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.StatusStrip2.ResumeLayout(False)
        Me.StatusStrip2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents mainMenu As System.Windows.Forms.MainMenu
    Private WithEvents menuItem1 As System.Windows.Forms.MenuItem
    Private WithEvents manuallyAddDeviceMenuItem As System.Windows.Forms.MenuItem
    Private WithEvents menuItem15 As System.Windows.Forms.MenuItem
    Private WithEvents menuItem12 As System.Windows.Forms.MenuItem
    Private WithEvents menuItem9 As System.Windows.Forms.MenuItem
    Private WithEvents menuItem4 As System.Windows.Forms.MenuItem
    Private WithEvents menuItem13 As System.Windows.Forms.MenuItem
    Private WithEvents menuItem14 As System.Windows.Forms.MenuItem
    Private WithEvents menuItem2 As System.Windows.Forms.MenuItem
    Private WithEvents menuItem7 As System.Windows.Forms.MenuItem
    Private WithEvents rescanMenuItem As System.Windows.Forms.MenuItem
    Private WithEvents menuItem19 As System.Windows.Forms.MenuItem
    Private WithEvents expandAllMenuItem As System.Windows.Forms.MenuItem
    Private WithEvents collapseAllMenuItem As System.Windows.Forms.MenuItem
    Private WithEvents menuItem16 As System.Windows.Forms.MenuItem
    Private WithEvents menuItem3 As System.Windows.Forms.MenuItem
    Private WithEvents viewStatusbarMenuItem As System.Windows.Forms.MenuItem
    Private WithEvents menuItem5 As System.Windows.Forms.MenuItem
    Private WithEvents helpMenuItem As System.Windows.Forms.MenuItem
    Private WithEvents menuItem6 As System.Windows.Forms.MenuItem
    Private WithEvents menuItem10 As System.Windows.Forms.MenuItem
    Private WithEvents showDebugInfoMenuItem As System.Windows.Forms.MenuItem
    Private WithEvents listInfoContextMenu As System.Windows.Forms.ContextMenu
    Private WithEvents openWebPageMenuItem As System.Windows.Forms.MenuItem
    Private WithEvents copyValueCpMenuItem As System.Windows.Forms.MenuItem
    Private WithEvents copyTableCpMenuItem As System.Windows.Forms.MenuItem
    Private WithEvents treeImageList As System.Windows.Forms.ImageList
    Private WithEvents deviceContextMenu As System.Windows.Forms.ContextMenu
    Private WithEvents presPageMenuItem As System.Windows.Forms.MenuItem
    Private WithEvents eventSubscribeMenuItem As System.Windows.Forms.MenuItem
    Private WithEvents invokeActionMenuItem As System.Windows.Forms.MenuItem
    Private WithEvents ActivatePlayerMenu As System.Windows.Forms.MenuItem
    Private WithEvents DeviceXMLmenuItem As System.Windows.Forms.MenuItem
    Private WithEvents ServiceXMLmenuItem As System.Windows.Forms.MenuItem
    Private WithEvents removeDeviceMenuItem As System.Windows.Forms.MenuItem
    Private WithEvents menuItem18 As System.Windows.Forms.MenuItem
    Private WithEvents expandAllMenuItem2 As System.Windows.Forms.MenuItem
    Private WithEvents collapseAllMenuItem2 As System.Windows.Forms.MenuItem
    Private WithEvents menuItem21 As System.Windows.Forms.MenuItem
    Private WithEvents rescanNetworkMenuItem As System.Windows.Forms.MenuItem
    Private WithEvents eventListViewContextMenu As System.Windows.Forms.ContextMenu
    Private WithEvents copyEventCpMenuItem As System.Windows.Forms.MenuItem
    Private WithEvents copyEventLogCpMenuItem As System.Windows.Forms.MenuItem
    Private WithEvents menuItem11 As System.Windows.Forms.MenuItem
    Private WithEvents ClearEventLogMenuItem As System.Windows.Forms.MenuItem
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents tabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents tabAvailable As System.Windows.Forms.TabPage
    Friend WithEvents splitter1_1 As System.Windows.Forms.SplitContainer
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents cmbSearch As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents btnScan As System.Windows.Forms.ToolStripButton
    Friend WithEvents splitter1_2 As System.Windows.Forms.SplitContainer
    Friend WithEvents splitter1_3 As System.Windows.Forms.SplitContainer
    Friend WithEvents tabManaged As System.Windows.Forms.TabPage
    Private WithEvents deviceTree As System.Windows.Forms.TreeView
    Friend WithEvents ToolStrip2 As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripLabel1 As System.Windows.Forms.ToolStripLabel
    Private WithEvents eventListView As System.Windows.Forms.ListView
    Private WithEvents columnHeader6 As System.Windows.Forms.ColumnHeader
    Private WithEvents columnHeader3 As System.Windows.Forms.ColumnHeader
    Private WithEvents columnHeader4 As System.Windows.Forms.ColumnHeader
    Private WithEvents columnHeader5 As System.Windows.Forms.ColumnHeader
    Friend WithEvents imgMediumIcons As System.Windows.Forms.ImageList
    Private WithEvents listInfo As System.Windows.Forms.ListView
    Private WithEvents columnHeader1 As System.Windows.Forms.ColumnHeader
    Private WithEvents columnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents lblStatus As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ManagedDeviceMenuItem As System.Windows.Forms.MenuItem
    Friend WithEvents AddManagedDeviceMenuItem As System.Windows.Forms.MenuItem
    Friend WithEvents AddCompoundDeviceMenuItem As System.Windows.Forms.MenuItem
    Friend WithEvents ManagedDeviceSeperatorMenuItem As System.Windows.Forms.MenuItem
    Friend WithEvents splitter2_1 As System.Windows.Forms.SplitContainer
    Friend WithEvents ToolStrip3 As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripComboBox1 As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents ToolStripButton1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents Splitter2_2 As System.Windows.Forms.SplitContainer
    Friend WithEvents Splitter2_3 As System.Windows.Forms.SplitContainer
    Private WithEvents ManagedTree As System.Windows.Forms.TreeView
    Friend WithEvents propGrid1 As System.Windows.Forms.PropertyGrid
    Friend WithEvents propGrid2 As System.Windows.Forms.PropertyGrid
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents Splitter3_1 As System.Windows.Forms.SplitContainer
    Friend WithEvents ToolStrip4 As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripComboBox2 As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents ToolStripButton2 As System.Windows.Forms.ToolStripButton
    Friend WithEvents Splitter3_2 As System.Windows.Forms.SplitContainer
    Friend WithEvents Splitter3_3 As System.Windows.Forms.SplitContainer
    Friend WithEvents StatusStrip2 As System.Windows.Forms.StatusStrip
    Friend WithEvents lblDuration As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents pbDuration As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents lblArtist2 As System.Windows.Forms.Label
    Friend WithEvents lblArtist As System.Windows.Forms.Label
    Friend WithEvents lblAlbum As System.Windows.Forms.Label
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents lblTrackNum As System.Windows.Forms.Label
    Friend WithEvents lblDevice As System.Windows.Forms.Label
    Friend WithEvents btnStop As System.Windows.Forms.Button
    Friend WithEvents btnPrev As System.Windows.Forms.Button
    Friend WithEvents btnNext As System.Windows.Forms.Button
    Friend WithEvents btnPlay As System.Windows.Forms.Button
    Friend WithEvents picBox As System.Windows.Forms.PictureBox
End Class
