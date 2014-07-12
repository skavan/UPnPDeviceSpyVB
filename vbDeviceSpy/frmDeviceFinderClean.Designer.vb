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
        Dim ListViewItem1 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem(New String() {""}, -1, System.Drawing.SystemColors.WindowText, System.Drawing.SystemColors.Window, New System.Drawing.Font("Microsoft Sans Serif", 8.25!))
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
        Me.presPageMenuItem = New System.Windows.Forms.MenuItem()
        Me.eventSubscribeMenuItem = New System.Windows.Forms.MenuItem()
        Me.invokeActionMenuItem = New System.Windows.Forms.MenuItem()
        Me.ValidateActionMenuItem = New System.Windows.Forms.MenuItem()
        Me.DeviceXMLmenuItem = New System.Windows.Forms.MenuItem()
        Me.ServiceXMLmenuItem = New System.Windows.Forms.MenuItem()
        Me.removeDeviceMenuItem = New System.Windows.Forms.MenuItem()
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
        Me.splitter1 = New System.Windows.Forms.SplitContainer()
        Me.deviceTree = New System.Windows.Forms.TreeView()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.cmbSearch = New System.Windows.Forms.ToolStripComboBox()
        Me.btnScan = New System.Windows.Forms.ToolStripButton()
        Me.splitter2 = New System.Windows.Forms.SplitContainer()
        Me.splitter3 = New System.Windows.Forms.SplitContainer()
        Me.listInfo = New System.Windows.Forms.ListView()
        Me.columnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.columnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.ToolStrip2 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripLabel1 = New System.Windows.Forms.ToolStripLabel()
        Me.eventListView = New System.Windows.Forms.ListView()
        Me.columnHeader6 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.columnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.columnHeader4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.columnHeader5 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.tabManaged = New System.Windows.Forms.TabPage()
        Me.imgMediumIcons = New System.Windows.Forms.ImageList(Me.components)
        Me.StatusStrip1.SuspendLayout()
        Me.tabControl1.SuspendLayout()
        Me.tabAvailable.SuspendLayout()
        CType(Me.splitter1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.splitter1.Panel1.SuspendLayout()
        Me.splitter1.Panel2.SuspendLayout()
        Me.splitter1.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.splitter2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.splitter2.Panel1.SuspendLayout()
        Me.splitter2.Panel2.SuspendLayout()
        Me.splitter2.SuspendLayout()
        CType(Me.splitter3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.splitter3.Panel1.SuspendLayout()
        Me.splitter3.Panel2.SuspendLayout()
        Me.splitter3.SuspendLayout()
        Me.ToolStrip2.SuspendLayout()
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
        Me.deviceContextMenu.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.presPageMenuItem, Me.eventSubscribeMenuItem, Me.invokeActionMenuItem, Me.ValidateActionMenuItem, Me.DeviceXMLmenuItem, Me.ServiceXMLmenuItem, Me.removeDeviceMenuItem, Me.menuItem18, Me.expandAllMenuItem2, Me.collapseAllMenuItem2, Me.menuItem21, Me.rescanNetworkMenuItem})
        '
        'presPageMenuItem
        '
        Me.presPageMenuItem.DefaultItem = True
        Me.presPageMenuItem.Index = 0
        Me.presPageMenuItem.Text = "Display Presentation Page"
        '
        'eventSubscribeMenuItem
        '
        Me.eventSubscribeMenuItem.DefaultItem = True
        Me.eventSubscribeMenuItem.Index = 1
        Me.eventSubscribeMenuItem.Text = "Subscribe to Events"
        '
        'invokeActionMenuItem
        '
        Me.invokeActionMenuItem.DefaultItem = True
        Me.invokeActionMenuItem.Index = 2
        Me.invokeActionMenuItem.Text = "Invoke Action"
        '
        'ValidateActionMenuItem
        '
        Me.ValidateActionMenuItem.Index = 3
        Me.ValidateActionMenuItem.Text = "Validate Actions"
        '
        'DeviceXMLmenuItem
        '
        Me.DeviceXMLmenuItem.Index = 4
        Me.DeviceXMLmenuItem.Text = "Get Device XML"
        '
        'ServiceXMLmenuItem
        '
        Me.ServiceXMLmenuItem.Index = 5
        Me.ServiceXMLmenuItem.Text = "Get Service XML"
        '
        'removeDeviceMenuItem
        '
        Me.removeDeviceMenuItem.Index = 6
        Me.removeDeviceMenuItem.Text = "Remove Device"
        '
        'menuItem18
        '
        Me.menuItem18.Index = 7
        Me.menuItem18.Text = "-"
        '
        'expandAllMenuItem2
        '
        Me.expandAllMenuItem2.Index = 8
        Me.expandAllMenuItem2.Text = "&Expand all devices"
        '
        'collapseAllMenuItem2
        '
        Me.collapseAllMenuItem2.Index = 9
        Me.collapseAllMenuItem2.Text = "&Collapse all devices"
        '
        'menuItem21
        '
        Me.menuItem21.Index = 10
        Me.menuItem21.Text = "-"
        '
        'rescanNetworkMenuItem
        '
        Me.rescanNetworkMenuItem.Index = 11
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
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 772)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(1286, 22)
        Me.StatusStrip1.TabIndex = 0
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'lblStatus
        '
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(121, 17)
        Me.lblStatus.Text = "ToolStripStatusLabel1"
        '
        'tabControl1
        '
        Me.tabControl1.Controls.Add(Me.tabAvailable)
        Me.tabControl1.Controls.Add(Me.tabManaged)
        Me.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tabControl1.Location = New System.Drawing.Point(0, 0)
        Me.tabControl1.Name = "tabControl1"
        Me.tabControl1.SelectedIndex = 0
        Me.tabControl1.Size = New System.Drawing.Size(1286, 772)
        Me.tabControl1.TabIndex = 1
        '
        'tabAvailable
        '
        Me.tabAvailable.BackColor = System.Drawing.SystemColors.Control
        Me.tabAvailable.Controls.Add(Me.splitter1)
        Me.tabAvailable.Location = New System.Drawing.Point(4, 26)
        Me.tabAvailable.Name = "tabAvailable"
        Me.tabAvailable.Padding = New System.Windows.Forms.Padding(3)
        Me.tabAvailable.Size = New System.Drawing.Size(1278, 742)
        Me.tabAvailable.TabIndex = 0
        Me.tabAvailable.Text = "Available UPnP Devices"
        '
        'splitter1
        '
        Me.splitter1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.splitter1.Location = New System.Drawing.Point(3, 3)
        Me.splitter1.Name = "splitter1"
        '
        'splitter1.Panel1
        '
        Me.splitter1.Panel1.Controls.Add(Me.deviceTree)
        Me.splitter1.Panel1.Controls.Add(Me.ToolStrip1)
        '
        'splitter1.Panel2
        '
        Me.splitter1.Panel2.Controls.Add(Me.splitter2)
        Me.splitter1.Size = New System.Drawing.Size(1272, 736)
        Me.splitter1.SplitterDistance = 394
        Me.splitter1.TabIndex = 1
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
        Me.deviceTree.Size = New System.Drawing.Size(394, 706)
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
        Me.cmbSearch.Size = New System.Drawing.Size(200, 23)
        '
        'btnScan
        '
        Me.btnScan.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.btnScan.Image = CType(resources.GetObject("btnScan.Image"), System.Drawing.Image)
        Me.btnScan.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnScan.Name = "btnScan"
        Me.btnScan.Size = New System.Drawing.Size(97, 27)
        Me.btnScan.Text = "Scan for Devices"
        '
        'splitter2
        '
        Me.splitter2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.splitter2.Location = New System.Drawing.Point(0, 0)
        Me.splitter2.Name = "splitter2"
        Me.splitter2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'splitter2.Panel1
        '
        Me.splitter2.Panel1.Controls.Add(Me.splitter3)
        '
        'splitter2.Panel2
        '
        Me.splitter2.Panel2.Controls.Add(Me.eventListView)
        Me.splitter2.Size = New System.Drawing.Size(874, 736)
        Me.splitter2.SplitterDistance = 412
        Me.splitter2.TabIndex = 0
        '
        'splitter3
        '
        Me.splitter3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.splitter3.Location = New System.Drawing.Point(0, 0)
        Me.splitter3.Name = "splitter3"
        '
        'splitter3.Panel1
        '
        Me.splitter3.Panel1.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.splitter3.Panel1.Controls.Add(Me.listInfo)
        Me.splitter3.Panel1.Padding = New System.Windows.Forms.Padding(0, 6, 0, 0)
        '
        'splitter3.Panel2
        '
        Me.splitter3.Panel2.Controls.Add(Me.ListBox1)
        Me.splitter3.Panel2.Controls.Add(Me.ToolStrip2)
        Me.splitter3.Size = New System.Drawing.Size(874, 412)
        Me.splitter3.SplitterDistance = 575
        Me.splitter3.TabIndex = 0
        '
        'listInfo
        '
        Me.listInfo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.listInfo.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.columnHeader1, Me.columnHeader2})
        Me.listInfo.ContextMenu = Me.listInfoContextMenu
        Me.listInfo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.listInfo.FullRowSelect = True
        Me.listInfo.Items.AddRange(New System.Windows.Forms.ListViewItem() {ListViewItem1})
        Me.listInfo.Location = New System.Drawing.Point(0, 6)
        Me.listInfo.Name = "listInfo"
        Me.listInfo.Size = New System.Drawing.Size(575, 406)
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
        'ListBox1
        '
        Me.ListBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ListBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.IntegralHeight = False
        Me.ListBox1.ItemHeight = 17
        Me.ListBox1.Location = New System.Drawing.Point(0, 30)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(295, 382)
        Me.ListBox1.TabIndex = 1
        '
        'ToolStrip2
        '
        Me.ToolStrip2.AutoSize = False
        Me.ToolStrip2.CanOverflow = False
        Me.ToolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripLabel1})
        Me.ToolStrip2.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip2.Name = "ToolStrip2"
        Me.ToolStrip2.Size = New System.Drawing.Size(295, 30)
        Me.ToolStrip2.TabIndex = 0
        Me.ToolStrip2.Text = "ToolStrip2"
        '
        'ToolStripLabel1
        '
        Me.ToolStripLabel1.Name = "ToolStripLabel1"
        Me.ToolStripLabel1.Size = New System.Drawing.Size(103, 27)
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
        Me.eventListView.Size = New System.Drawing.Size(874, 320)
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
        Me.tabManaged.Location = New System.Drawing.Point(4, 26)
        Me.tabManaged.Name = "tabManaged"
        Me.tabManaged.Padding = New System.Windows.Forms.Padding(3)
        Me.tabManaged.Size = New System.Drawing.Size(1278, 742)
        Me.tabManaged.TabIndex = 1
        Me.tabManaged.Text = "Managed Devices"
        Me.tabManaged.UseVisualStyleBackColor = True
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
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
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
        Me.splitter1.Panel1.ResumeLayout(False)
        Me.splitter1.Panel2.ResumeLayout(False)
        CType(Me.splitter1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splitter1.ResumeLayout(False)
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.splitter2.Panel1.ResumeLayout(False)
        Me.splitter2.Panel2.ResumeLayout(False)
        CType(Me.splitter2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splitter2.ResumeLayout(False)
        Me.splitter3.Panel1.ResumeLayout(False)
        Me.splitter3.Panel2.ResumeLayout(False)
        CType(Me.splitter3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splitter3.ResumeLayout(False)
        Me.ToolStrip2.ResumeLayout(False)
        Me.ToolStrip2.PerformLayout()
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
    Private WithEvents ValidateActionMenuItem As System.Windows.Forms.MenuItem
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
    Friend WithEvents splitter1 As System.Windows.Forms.SplitContainer
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents cmbSearch As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents btnScan As System.Windows.Forms.ToolStripButton
    Friend WithEvents splitter2 As System.Windows.Forms.SplitContainer
    Friend WithEvents splitter3 As System.Windows.Forms.SplitContainer
    Friend WithEvents tabManaged As System.Windows.Forms.TabPage
    Private WithEvents deviceTree As System.Windows.Forms.TreeView
    Friend WithEvents ListBox1 As System.Windows.Forms.ListBox
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
End Class
