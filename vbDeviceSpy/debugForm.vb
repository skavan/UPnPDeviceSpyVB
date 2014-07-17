Imports OpenSource.Utilities
Imports System.Xml

Public Class debugForm
    Delegate Sub HandleOnEvent(LogType As EventLogEntryType, origin As Object, StackTrace As String, LogMessage As String)
    Dim dataTable As New DataTable
    Dim dataView As DataView
    Public ipFilter As String = "XXXX"

    Private Sub debugForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        EventLogger.Enabled = False
        RemoveHandler EventLogger.OnEvent, AddressOf eventLogger_OnEvent
    End Sub

    Private Sub debugForm_Load(sender As Object, e As EventArgs) Handles Me.Load
        EventLogger.Enabled = True
        AddHandler EventLogger.OnEvent, AddressOf eventLogger_OnEvent

        dataTable.Columns.Add("TimeStamp", GetType(Date))
        dataTable.Columns.Add("LogType", GetType(EventLogEntryType))
        dataTable.Columns.Add("Origin", GetType(String))
        dataTable.Columns.Add("LogMessage", GetType(String))
        dataTable.Columns.Add("StackTrace", GetType(String))
        dataView = dataTable.DefaultView
        DataGridView1.DataSource = dataView
        DataGridView1.Columns("TimeStamp").Visible = False
        DataGridView1.Columns("LogType").Width = 50
        DataGridView1.Columns("Origin").Width = 150
        DataGridView1.Columns("LogMessage").Width = 350
        DataGridView1.Columns("LogMessage").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        DataGridView1.Sort(DataGridView1.Columns("TimeStamp"), System.ComponentModel.ListSortDirection.Descending)
        DataGridView1.MultiSelect = False
        dataView.RowFilter = BuildFilter()
        'Dim results As DataRow() = dataTable.Select(BuildFilter)

        'DataGridView1.DataSource = dataTable.Select(BuildFilter)
    End Sub

    Private Sub eventLogger_OnEvent(LogType As EventLogEntryType, origin As Object, StackTrace As String, LogMessage As String)
        Me.Invoke(New HandleOnEvent(AddressOf OnEvent), {LogType, origin.ToString, StackTrace, LogMessage})

    End Sub

    Private Sub OnEvent(LogType As EventLogEntryType, origin As Object, StackTrace As String, LogMessage As String)
        dataTable.Rows.Add({Now, LogType, origin.ToString, LogMessage, StackTrace})
        DataGridView1.Rows(0).Selected = True
    End Sub

    Private Function BuildFilter() As String
        Dim base As String = "", prefix As String
        If btnErrors.Checked Then base += "LogType = 1"

        If base <> "" Then prefix = " OR " Else prefix = ""
        If btnWarnings.Checked Then base += prefix & "LogType = 2"

        If base <> "" Then prefix = " OR " Else prefix = ""
        If btnInfo.Checked Then base += prefix & "LogType = 4"

        If base <> "" Then prefix = " OR " Else prefix = ""
        If btnAudit.Checked Then base += prefix & "LogType = 8"

        If btnManaged.Checked Then
            base = "(" + base + ")"
            base += " OR (Origin = 'OpenSource.UPnP.UPnPControlPoint' AND LogMessage LIKE '%" & ipFilter & "%')"
        End If

        Return base
    End Function

    Private Sub CheckStateChanged(sender As Object, e As EventArgs) Handles btnErrors.CheckStateChanged, btnWarnings.CheckStateChanged, btnAudit.CheckStateChanged, btnInfo.CheckStateChanged
        If dataTable.Rows.Count > 0 Then
            'DataGridView1.DataSource = dataTable.Select(BuildFilter)
            dataView.RowFilter = BuildFilter()
        End If


    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If dataTable.Rows.Count > 0 Then
            Try
                Dim r As DataRow = DataGridView1.CurrentRow.DataBoundItem

                ShowDetail(r("LogMessage").ToString(), r("StackTrace").ToString())
                'ShowDetail(DataGridView1.SelectedRows(0).Cells("LogMessage").Value, DataGridView1.SelectedRows(0).Cells("StackTrace").Value)
            Catch ex As Exception
                Debug.Print("Something is wrong!")
            End Try


        End If

    End Sub

    Private Sub ShowDetail(log As String, trace As String)
        RichTextBox1.Clear()
        If trace Is Nothing Then trace = ""
        If log Is Nothing Then log = ""



        If log.Contains("<Event xmlns") Then
            Dim xml As String = log.Substring(log.IndexOf("<Event"), (log.LastIndexOf("/Event>") + 7) - log.IndexOf("<Event"))
            log = log.Remove(log.IndexOf("<Event"), (log.LastIndexOf("/Event>") + 7) - log.IndexOf("<Event"))
            If log <> "" Then
                RichTextBox1.AppendText(log & vbCrLf)
            End If
            LoadXMLtoBOX(xml)
            RichTextBox1.AppendText(vbCrLf & vbCrLf)
        Else
            RichTextBox1.Select(RichTextBox1.TextLength, 0)
            RichTextBox1.SelectionColor = Color.Blue
            RichTextBox1.AppendText(log & vbCrLf & vbCrLf)

        End If


        RichTextBox1.Select(RichTextBox1.TextLength, 0)
        RichTextBox1.SelectionColor = Color.Red
        RichTextBox1.AppendText(trace)
    End Sub

    Public Sub LoadXMLtoBOX(ByVal strXMLPath As String)

        Dim reader As New XmlTextReader(strXMLPath)
        While reader.Read()
            Select Case reader.NodeType
                Case XmlNodeType.Element
                    'The node is an element. 
                    Me.RichTextBox1.SelectionColor = Color.Blue
                    Me.RichTextBox1.AppendText("<")
                    Me.RichTextBox1.SelectionColor = Color.Brown
                    Me.RichTextBox1.AppendText(reader.Name)
                    Me.RichTextBox1.SelectionColor = Color.Blue
                    Me.RichTextBox1.AppendText(">")
                    Exit Select
                Case XmlNodeType.Text
                    'Display the text in each element. 
                    Me.RichTextBox1.SelectionColor = Color.Black
                    Me.RichTextBox1.AppendText(reader.Value)
                    Exit Select
                Case XmlNodeType.EndElement
                    'Display the end of the element. 
                    Me.RichTextBox1.SelectionColor = Color.Blue
                    Me.RichTextBox1.AppendText("</")
                    Me.RichTextBox1.SelectionColor = Color.Brown
                    Me.RichTextBox1.AppendText(reader.Name)
                    Me.RichTextBox1.SelectionColor = Color.Blue
                    Me.RichTextBox1.AppendText(">")
                    Me.RichTextBox1.AppendText(vbLf)
                    Exit Select
            End Select
        End While
        reader.Close()

    End Sub


    Private Sub btnPause_Click(sender As Object, e As EventArgs) Handles btnPause.Click

    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click

        Debug.Print(BuildFilter)
    End Sub

    Private Sub btnPause_MouseUp(sender As Object, e As MouseEventArgs) Handles btnPause.MouseUp

    End Sub
End Class