<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class debugForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(debugForm))
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnWarnings = New System.Windows.Forms.ToolStripButton()
        Me.btnErrors = New System.Windows.Forms.ToolStripButton()
        Me.btnInfo = New System.Windows.Forms.ToolStripButton()
        Me.btnAudit = New System.Windows.Forms.ToolStripButton()
        Me.btnPause = New System.Windows.Forms.ToolStripButton()
        Me.btnClear = New System.Windows.Forms.ToolStripButton()
        Me.btnManaged = New System.Windows.Forms.ToolStripButton()
        Me.btnAll = New System.Windows.Forms.ToolStripButton()
        Me.RichTextBox1 = New System.Windows.Forms.RichTextBox()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.DataGridView1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.ToolStrip1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.RichTextBox1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.StatusStrip1)
        Me.SplitContainer1.Size = New System.Drawing.Size(936, 734)
        Me.SplitContainer1.SplitterDistance = 464
        Me.SplitContainer1.SplitterWidth = 5
        Me.SplitContainer1.TabIndex = 0
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView1.Location = New System.Drawing.Point(0, 32)
        Me.DataGridView1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.RowHeadersVisible = False
        Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView1.Size = New System.Drawing.Size(936, 432)
        Me.DataGridView1.TabIndex = 1
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnWarnings, Me.btnErrors, Me.btnInfo, Me.btnAudit, Me.btnPause, Me.btnClear, Me.btnManaged, Me.btnAll})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.ToolStrip1.Size = New System.Drawing.Size(936, 32)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnWarnings
        '
        Me.btnWarnings.AutoSize = False
        Me.btnWarnings.Checked = True
        Me.btnWarnings.CheckOnClick = True
        Me.btnWarnings.CheckState = System.Windows.Forms.CheckState.Checked
        Me.btnWarnings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.btnWarnings.Image = CType(resources.GetObject("btnWarnings.Image"), System.Drawing.Image)
        Me.btnWarnings.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnWarnings.Name = "btnWarnings"
        Me.btnWarnings.Size = New System.Drawing.Size(60, 22)
        Me.btnWarnings.Text = "Errors"
        '
        'btnErrors
        '
        Me.btnErrors.AutoSize = False
        Me.btnErrors.Checked = True
        Me.btnErrors.CheckOnClick = True
        Me.btnErrors.CheckState = System.Windows.Forms.CheckState.Checked
        Me.btnErrors.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.btnErrors.Image = CType(resources.GetObject("btnErrors.Image"), System.Drawing.Image)
        Me.btnErrors.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnErrors.Name = "btnErrors"
        Me.btnErrors.Size = New System.Drawing.Size(60, 22)
        Me.btnErrors.Text = "Warnings"
        '
        'btnInfo
        '
        Me.btnInfo.AutoSize = False
        Me.btnInfo.Checked = True
        Me.btnInfo.CheckOnClick = True
        Me.btnInfo.CheckState = System.Windows.Forms.CheckState.Checked
        Me.btnInfo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.btnInfo.Image = CType(resources.GetObject("btnInfo.Image"), System.Drawing.Image)
        Me.btnInfo.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnInfo.Name = "btnInfo"
        Me.btnInfo.Size = New System.Drawing.Size(60, 22)
        Me.btnInfo.Text = "Info"
        '
        'btnAudit
        '
        Me.btnAudit.AutoSize = False
        Me.btnAudit.CheckOnClick = True
        Me.btnAudit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.btnAudit.Image = CType(resources.GetObject("btnAudit.Image"), System.Drawing.Image)
        Me.btnAudit.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnAudit.Name = "btnAudit"
        Me.btnAudit.Size = New System.Drawing.Size(60, 22)
        Me.btnAudit.Text = "Audit"
        '
        'btnPause
        '
        Me.btnPause.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.btnPause.AutoSize = False
        Me.btnPause.CheckOnClick = True
        Me.btnPause.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.btnPause.Image = CType(resources.GetObject("btnPause.Image"), System.Drawing.Image)
        Me.btnPause.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnPause.Name = "btnPause"
        Me.btnPause.Size = New System.Drawing.Size(60, 22)
        Me.btnPause.Text = "Pause"
        '
        'btnClear
        '
        Me.btnClear.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.btnClear.AutoSize = False
        Me.btnClear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.btnClear.Image = CType(resources.GetObject("btnClear.Image"), System.Drawing.Image)
        Me.btnClear.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(60, 22)
        Me.btnClear.Text = "Clear"
        '
        'btnManaged
        '
        Me.btnManaged.Checked = True
        Me.btnManaged.CheckOnClick = True
        Me.btnManaged.CheckState = System.Windows.Forms.CheckState.Checked
        Me.btnManaged.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.btnManaged.Image = CType(resources.GetObject("btnManaged.Image"), System.Drawing.Image)
        Me.btnManaged.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnManaged.Name = "btnManaged"
        Me.btnManaged.Size = New System.Drawing.Size(132, 29)
        Me.btnManaged.Text = "Managed Devices Only"
        '
        'btnAll
        '
        Me.btnAll.AutoSize = False
        Me.btnAll.CheckOnClick = True
        Me.btnAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.btnAll.Image = CType(resources.GetObject("btnAll.Image"), System.Drawing.Image)
        Me.btnAll.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnAll.Name = "btnAll"
        Me.btnAll.Size = New System.Drawing.Size(70, 29)
        Me.btnAll.Text = "All"
        '
        'RichTextBox1
        '
        Me.RichTextBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RichTextBox1.Location = New System.Drawing.Point(0, 0)
        Me.RichTextBox1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RichTextBox1.Name = "RichTextBox1"
        Me.RichTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical
        Me.RichTextBox1.Size = New System.Drawing.Size(936, 243)
        Me.RichTextBox1.TabIndex = 1
        Me.RichTextBox1.Text = ""
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 243)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Padding = New System.Windows.Forms.Padding(1, 0, 16, 0)
        Me.StatusStrip1.Size = New System.Drawing.Size(936, 22)
        Me.StatusStrip1.TabIndex = 0
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'debugForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(936, 734)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "debugForm"
        Me.Text = "debugForm"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents RichTextBox1 As System.Windows.Forms.RichTextBox
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents btnErrors As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnWarnings As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnInfo As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnAudit As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnClear As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnPause As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnManaged As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnAll As System.Windows.Forms.ToolStripButton
End Class
