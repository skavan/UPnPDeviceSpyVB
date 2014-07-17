﻿Imports OpenSource.Utilities
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
        EventLogger.ShowAll = True
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
        'If LogType = EventLogEntryType.Information Then MsgBox("HEY!")
        'DataGridView1.Rows(0).Selected = True
    End Sub

    Private Function BuildFilter() As String
        If btnAll.Checked Then Return ""

        Dim base As String = "", prefix As String
        If btnWarnings.Checked Then base += "(LogType = 1)"

        If base <> "" Then prefix = " OR " Else prefix = ""
        If btnInfo.Checked Then base += prefix & "(LogType = 2)"

        If base <> "" Then prefix = " OR " Else prefix = ""
        If btnInfo.Checked Then base += prefix & "(LogType = 4)"

        If base <> "" Then prefix = " OR " Else prefix = ""
        If btnAudit.Checked Then base += prefix & "(LogType = 8)"

        If btnManaged.Checked Then
            If base <> "" Then
                prefix = " OR "
                base = "(" + base + ")"
            Else
                prefix = ""
            End If
            base += prefix + "((Origin = 'OpenSource.UPnP.UPnPControlPoint') AND (LogMessage LIKE '%" & ipFilter & "%'))"
        End If
        Debug.Print(base)
        Return base
    End Function

    Private Sub CheckStateChanged(sender As Object, e As EventArgs) Handles btnErrors.CheckStateChanged, btnWarnings.CheckStateChanged, btnAudit.CheckStateChanged, btnInfo.CheckStateChanged, btnManaged.CheckStateChanged, btnAll.CheckStateChanged
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
                Dim r As DataRowView = DataGridView1.CurrentRow.DataBoundItem
                Debug.Print("COLUMN 1: " & r(1))
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

            Dim tmpDoc As New Xml.XmlDocument
            tmpDoc.LoadXml(xml)
            XML_Coloring(RichTextBox1, tmpDoc.ChildNodes, 0)
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

    Public Sub XML_Coloring(txtBox As RichTextBox, nodelist As XmlNodeList, indent As Integer)

        For Each xmlNodeType As XmlNode In nodelist

            With txtBox

                If Not xmlNodeType.NodeType = Xml.XmlNodeType.Element Then
                    .SelectionIndent = indent
                    .SelectionColor = Color.Black
                    .AppendText(xmlNodeType.OuterXml & vbNewLine)
                    Continue For
                End If

                Dim xmlNode As Xml.XmlElement = xmlNodeType

                ' Element node
                .SelectionIndent = indent
                .SelectionColor = Color.Blue
                .AppendText("<" & xmlNode.Name)

                ' Load attributes
                If xmlNode.HasAttributes Then
                    For Each xmlAttrib As XmlAttribute In xmlNode.Attributes
                        .SelectionColor = Color.Red
                        .AppendText(" " & xmlAttrib.Name)
                        .SelectionColor = Color.Black
                        .AppendText("=")
                        .SelectionColor = Color.Purple
                        .AppendText("""" & xmlAttrib.Value & """")
                    Next
                End If

                ' Load child nodes
                If xmlNode.HasChildNodes Then

                    .SelectionColor = Color.Blue
                    .AppendText(">")

                    If xmlNode.ChildNodes(0).GetType() Is GetType(XmlText) Then
                        ' Contains text
                        .SelectionColor = Color.Black
                        .AppendText(xmlNode.InnerText)
                    Else
                        ' Has child nodes
                        .AppendText(vbNewLine)
                        XML_Coloring(txtBox, xmlNode.ChildNodes, indent + 20)

                    End If

                    .SelectionIndent = indent
                    .SelectionColor = Color.Blue
                    .AppendText("</" & xmlNode.Name & ">" & vbNewLine)
                Else
                    .SelectionColor = Color.Blue
                    .AppendText(" />" & vbNewLine)

                End If

            End With

        Next

    End Sub


    Private Sub btnPause_Click(sender As Object, e As EventArgs) Handles btnPause.Click

    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        dataTable.Rows.Clear()
        Debug.Print(BuildFilter)
    End Sub

    Private Sub btnPause_MouseUp(sender As Object, e As MouseEventArgs) Handles btnPause.MouseUp

    End Sub
End Class