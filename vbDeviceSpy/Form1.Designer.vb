<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Dim ListViewItem1 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem(New String() {""}, -1, System.Drawing.SystemColors.WindowText, System.Drawing.SystemColors.Window, New System.Drawing.Font("Microsoft Sans Serif", 8.25!))
        Me.menuItem9 = New System.Windows.Forms.MenuItem()
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
        Me.eventListView = New System.Windows.Forms.ListView()
        Me.columnHeader6 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.columnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.columnHeader4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.columnHeader5 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.eventListViewContextMenu = New System.Windows.Forms.ContextMenu()
        Me.copyEventCpMenuItem = New System.Windows.Forms.MenuItem()
        Me.copyEventLogCpMenuItem = New System.Windows.Forms.MenuItem()
        Me.menuItem11 = New System.Windows.Forms.MenuItem()
        Me.ClearEventLogMenuItem = New System.Windows.Forms.MenuItem()
        Me.menuItem4 = New System.Windows.Forms.MenuItem()
        Me.splitter2 = New System.Windows.Forms.Splitter()
        Me.menuItem12 = New System.Windows.Forms.MenuItem()
        Me.deviceTree = New System.Windows.Forms.TreeView()
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
        Me.treeImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.listInfo = New System.Windows.Forms.ListView()
        Me.columnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.columnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.listInfoContextMenu = New System.Windows.Forms.ContextMenu()
        Me.openWebPageMenuItem = New System.Windows.Forms.MenuItem()
        Me.copyValueCpMenuItem = New System.Windows.Forms.MenuItem()
        Me.copyTableCpMenuItem = New System.Windows.Forms.MenuItem()
        Me.splitter1 = New System.Windows.Forms.Splitter()
        Me.statusBar = New System.Windows.Forms.StatusBar()
        Me.mainMenu = New System.Windows.Forms.MainMenu(Me.components)
        Me.menuItem1 = New System.Windows.Forms.MenuItem()
        Me.manuallyAddDeviceMenuItem = New System.Windows.Forms.MenuItem()
        Me.menuItem15 = New System.Windows.Forms.MenuItem()
        Me.SuspendLayout()
        '
        'menuItem9
        '
        Me.menuItem9.Index = 3
        Me.menuItem9.Text = "Copy &event log to clipboard"
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
        'eventListView
        '
        Me.eventListView.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.eventListView.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.columnHeader6, Me.columnHeader3, Me.columnHeader4, Me.columnHeader5})
        Me.eventListView.ContextMenu = Me.eventListViewContextMenu
        Me.eventListView.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.eventListView.FullRowSelect = True
        Me.eventListView.Location = New System.Drawing.Point(480, 431)
        Me.eventListView.Name = "eventListView"
        Me.eventListView.Size = New System.Drawing.Size(1549, 234)
        Me.eventListView.TabIndex = 17
        Me.eventListView.UseCompatibleStateImageBehavior = False
        Me.eventListView.View = System.Windows.Forms.View.Details
        Me.eventListView.Visible = False
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
        'menuItem4
        '
        Me.menuItem4.Index = 4
        Me.menuItem4.Text = "-"
        '
        'splitter2
        '
        Me.splitter2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.splitter2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.splitter2.Location = New System.Drawing.Point(480, 665)
        Me.splitter2.Name = "splitter2"
        Me.splitter2.Size = New System.Drawing.Size(1549, 6)
        Me.splitter2.TabIndex = 18
        Me.splitter2.TabStop = False
        Me.splitter2.Visible = False
        '
        'menuItem12
        '
        Me.menuItem12.Index = 2
        Me.menuItem12.Text = "Copy &information table to clipboard"
        '
        'deviceTree
        '
        Me.deviceTree.BackColor = System.Drawing.Color.White
        Me.deviceTree.ContextMenu = Me.deviceContextMenu
        Me.deviceTree.Dock = System.Windows.Forms.DockStyle.Left
        Me.deviceTree.ImageIndex = 0
        Me.deviceTree.ImageList = Me.treeImageList
        Me.deviceTree.Indent = 19
        Me.deviceTree.ItemHeight = 16
        Me.deviceTree.Location = New System.Drawing.Point(6, 0)
        Me.deviceTree.Name = "deviceTree"
        Me.deviceTree.SelectedImageIndex = 0
        Me.deviceTree.Size = New System.Drawing.Size(474, 671)
        Me.deviceTree.TabIndex = 13
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
        'listInfo
        '
        Me.listInfo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.listInfo.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.columnHeader1, Me.columnHeader2})
        Me.listInfo.ContextMenu = Me.listInfoContextMenu
        Me.listInfo.FullRowSelect = True
        Me.listInfo.Items.AddRange(New System.Windows.Forms.ListViewItem() {ListViewItem1})
        Me.listInfo.Location = New System.Drawing.Point(480, 0)
        Me.listInfo.Name = "listInfo"
        Me.listInfo.Size = New System.Drawing.Size(1549, 425)
        Me.listInfo.TabIndex = 16
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
        'splitter1
        '
        Me.splitter1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.splitter1.Location = New System.Drawing.Point(0, 0)
        Me.splitter1.Name = "splitter1"
        Me.splitter1.Size = New System.Drawing.Size(6, 671)
        Me.splitter1.TabIndex = 15
        Me.splitter1.TabStop = False
        '
        'statusBar
        '
        Me.statusBar.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.statusBar.Location = New System.Drawing.Point(0, 671)
        Me.statusBar.Name = "statusBar"
        Me.statusBar.Size = New System.Drawing.Size(2029, 26)
        Me.statusBar.TabIndex = 14
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
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(2029, 697)
        Me.Controls.Add(Me.eventListView)
        Me.Controls.Add(Me.splitter2)
        Me.Controls.Add(Me.deviceTree)
        Me.Controls.Add(Me.listInfo)
        Me.Controls.Add(Me.splitter1)
        Me.Controls.Add(Me.statusBar)
        Me.Menu = Me.mainMenu
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents menuItem9 As System.Windows.Forms.MenuItem
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
    Private WithEvents eventListView As System.Windows.Forms.ListView
    Private WithEvents columnHeader6 As System.Windows.Forms.ColumnHeader
    Private WithEvents columnHeader3 As System.Windows.Forms.ColumnHeader
    Private WithEvents columnHeader4 As System.Windows.Forms.ColumnHeader
    Private WithEvents columnHeader5 As System.Windows.Forms.ColumnHeader
    Private WithEvents eventListViewContextMenu As System.Windows.Forms.ContextMenu
    Private WithEvents copyEventCpMenuItem As System.Windows.Forms.MenuItem
    Private WithEvents copyEventLogCpMenuItem As System.Windows.Forms.MenuItem
    Private WithEvents menuItem11 As System.Windows.Forms.MenuItem
    Private WithEvents ClearEventLogMenuItem As System.Windows.Forms.MenuItem
    Private WithEvents menuItem4 As System.Windows.Forms.MenuItem
    Private WithEvents splitter2 As System.Windows.Forms.Splitter
    Private WithEvents menuItem12 As System.Windows.Forms.MenuItem
    Private WithEvents deviceTree As System.Windows.Forms.TreeView
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
    Private WithEvents treeImageList As System.Windows.Forms.ImageList
    Private WithEvents listInfo As System.Windows.Forms.ListView
    Private WithEvents columnHeader1 As System.Windows.Forms.ColumnHeader
    Private WithEvents columnHeader2 As System.Windows.Forms.ColumnHeader
    Private WithEvents listInfoContextMenu As System.Windows.Forms.ContextMenu
    Private WithEvents openWebPageMenuItem As System.Windows.Forms.MenuItem
    Private WithEvents copyValueCpMenuItem As System.Windows.Forms.MenuItem
    Private WithEvents copyTableCpMenuItem As System.Windows.Forms.MenuItem
    Private WithEvents splitter1 As System.Windows.Forms.Splitter
    Private WithEvents statusBar As System.Windows.Forms.StatusBar
    Private WithEvents mainMenu As System.Windows.Forms.MainMenu
    Private WithEvents menuItem1 As System.Windows.Forms.MenuItem
    Private WithEvents manuallyAddDeviceMenuItem As System.Windows.Forms.MenuItem
    Private WithEvents menuItem15 As System.Windows.Forms.MenuItem

End Class
