﻿Imports OpenSource.UPnP
Imports OpenSource.Utilities
Imports System.Collections.Generic
Imports System.Linq
Imports System.Threading
Imports System.Xml.Linq
'// To DO AMAZON CLOUD PLAYER On SoNOS - Artwork.

Public Class UPNPPlayer
    Inherits Player2

#Region "Variables & Events"
    Public Const NOT_IMPLEMENTED = "NOT_IMPLEMENTED"

    Private m_mediaRenderer As UPnPDevice
    Private m_avTransport As UPnPService
    Private m_mediaServer As UPnPDevice
    Private m_renderingControl As UPnPService
    Private m_contentDirectory As UPnPService

    Private positionTimer As Timer

    '// TO-DO
    Public Event StateChanged As Action(Of UPNPPlayer, ePlayerStateChangeType)

    Public Enum ePlayerStateChangeType
        SubscriptionEvent
        PollingEvent
    End Enum
    Private Shared _missingAlbumArtPath As String = "http://www.kavan.us/musicweb/images/No-album-art300x300.jpg"

#End Region

#Region "Initialize & Cleanup System"

    Public Sub SetDevice(playerDevice As UPnPDevice)
        Device = playerDevice
        ' Subscribe to LastChange event
        SubscribeToEvents()
    End Sub

    Private Sub SubscribeToEvents()
        If AVTransport Is Nothing Then Exit Sub
        AVTransport.Subscribe(600, AddressOf HandleOnServiceSubscribe)
    End Sub

    Protected Overrides Sub Finalize()

        If positionTimer IsNot Nothing Then positionTimer.Dispose()
        If AVTransport IsNot Nothing Then
            AVTransport.UnSubscribe(Nothing)
        End If

        MyBase.Finalize()
    End Sub

#End Region

#Region "Callbacks and Polling"

    Private Sub HandleOnServiceSubscribe(sender As UPnPService, success As Boolean)
        If Not success Then Exit Sub
        Dim lastChangeStateVariable = sender.GetStateVariableObject("LastChange")
        AddHandler lastChangeStateVariable.OnModified, AddressOf ChangeTriggered
    End Sub

    Private Sub ChangeTriggered(sender As UPnPStateVariable, value As Object)
        Dim newState As String = sender.Value
        EventLogger.Log(Me, EventLogEntryType.Warning, "Incoming Event: " & sender.Name & " | " & newState)
        ProcessEventChangeXML(newState)
    End Sub

    Public Sub StartPolling()
        If positionTimer IsNot Nothing Then
            positionTimer.Change(100, 1000)
            Exit Sub
        End If

        If TransportState <> eTransportState.Playing Then
            Return
        End If

        positionTimer = New Timer(AddressOf OnTimerUpdatePlayerState, Nothing, TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(1))
    End Sub

    Public Sub StopPolling()
        '// stop the timer
        If positionTimer IsNot Nothing Then positionTimer.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite)
    End Sub

    Private Sub OnTimerUpdatePlayerState(state As Object)
        PositionInfo = GetPositionInfo()
        TransportInfo = GetTransportInfo()
        UpdatePlayerStatusFromTransportInfo()
        RaiseEvent StateChanged(Me, ePlayerStateChangeType.PollingEvent)
        Debug.Print("pulse")
        If TransportInfo.CurrentTransportState <> eTransportState.Playing Then StopPolling()
    End Sub

#End Region

#Region "Public Properties"
    Public Property Device() As UPnPDevice

    Public Property PositionInfo As cPositionInfo = New cPositionInfo
    Public Property MediaInfo As cMediaInfo = New cMediaInfo
    Public Property TransportInfo As cTransportInfo = New cTransportInfo

    Public ReadOnly Property MediaRenderer() As UPnPDevice
        Get
            If m_mediaRenderer IsNot Nothing Then
                Return m_mediaRenderer
            End If
            If Device Is Nothing Then
                Return Nothing
            End If
            If Device.DeviceURN = "urn:schemas-upnp-org:device:MediaRenderer:1" Then
                '// the parent is a media renderer. return it.
                Return Device
            Else
                m_mediaRenderer = Device.EmbeddedDevices.FirstOrDefault(Function(d) d.DeviceURN = "urn:schemas-upnp-org:device:MediaRenderer:1")
                Return m_mediaRenderer
            End If

        End Get
    End Property

    Public ReadOnly Property MediaServer() As UPnPDevice
        Get
            If m_mediaServer IsNot Nothing Then
                Return m_mediaServer
            End If
            If Device Is Nothing Then
                Return Nothing
            End If
            m_mediaServer = Device.EmbeddedDevices.FirstOrDefault(Function(d) d.DeviceURN = "urn:schemas-upnp-org:device:MediaServer:1")
            Return m_mediaServer
        End Get
    End Property

    Public ReadOnly Property RenderingControl() As UPnPService
        Get
            If m_renderingControl IsNot Nothing Then
                Return m_renderingControl
            End If
            If MediaRenderer Is Nothing Then
                Return Nothing
            End If
            m_renderingControl = MediaRenderer.GetService("urn:upnp-org:serviceId:RenderingControl")
            Return m_renderingControl
        End Get
    End Property

    Public ReadOnly Property AVTransport() As UPnPService
        Get
            If m_avTransport IsNot Nothing Then
                Return m_avTransport
            End If
            If MediaRenderer Is Nothing Then
                Return Nothing
            End If
            m_avTransport = MediaRenderer.GetService("urn:upnp-org:serviceId:AVTransport")
            Return m_avTransport
        End Get
    End Property

    Public ReadOnly Property ContentDirectory() As UPnPService
        Get
            If m_contentDirectory IsNot Nothing Then
                Return m_contentDirectory
            End If
            If MediaServer Is Nothing Then
                Return Nothing
            End If
            m_contentDirectory = MediaServer.GetService("urn:upnp-org:serviceId:ContentDirectory")
            Return m_contentDirectory
        End Get
    End Property


#End Region

#Region "UPNP AVTransport Information Actions"

    Public Function GetTransportInfo() As cTransportInfo
        Dim arguments = New UPnPArgument(3) {}
        arguments(0) = New UPnPArgument("InstanceID", 0UI)
        arguments(1) = New UPnPArgument("CurrentTransportState", "")
        arguments(2) = New UPnPArgument("CurrentTransportStatus", "")
        arguments(3) = New UPnPArgument("CurrentSpeed", "")

        Try
            AVTransport.InvokeSync("GetTransportInfo", arguments)
        Catch ex As UPnPInvokeException
            Return TransportInfo                                         '// on error - return existing TransportInfo
        End Try

        'Dim status As PlayerStatus
        Dim ti As New cTransportInfo
        With ti

            .CurrentTransportState = GetTransportState(arguments(1).DataValue.ToString)
            .CurrentTransportStatus = GetTransportStatus(arguments(2).DataValue.ToString)

        End With

        Return ti
    End Function

    Public Function GetMediaInfo() As cMediaInfo
        Dim arguments = New UPnPArgument(9) {}
        arguments(0) = New UPnPArgument("InstanceID", 0UI)
        arguments(1) = New UPnPArgument("NrTracks", 0UI)
        arguments(2) = New UPnPArgument("MediaDuration", Nothing)
        arguments(3) = New UPnPArgument("CurrentURI", Nothing)
        arguments(4) = New UPnPArgument("CurrentURIMetaData", Nothing)
        arguments(5) = New UPnPArgument("NextURI", Nothing)
        arguments(6) = New UPnPArgument("NextURIMetaData", Nothing)
        arguments(7) = New UPnPArgument("PlayMedium", Nothing)
        arguments(8) = New UPnPArgument("RecordMedium", Nothing)
        arguments(9) = New UPnPArgument("WriteStatus", Nothing)

        Try
            AVTransport.InvokeSync("GetMediaInfo", arguments)
        Catch ex As UPnPInvokeException
            Return MediaInfo                                        '// on error - return existing MediaInfo
        End Try

        '// do we need to return media info or could we just set the public property here? for now, let's optimzie for code friendliness
        Dim mi As New cMediaInfo
        With mi
            .NrTracks = CUInt(arguments(1).DataValue)
            TimeSpan.TryParse(arguments(2).DataValue, .MediaDuration)
            .CurrentURI = arguments(3).DataValue.ToString
            .CurrentURIMetaData = arguments(4).DataValue.ToString
            .NextURI = arguments(5).DataValue.ToString
            .NextURIMetaData = arguments(6).DataValue.ToString
            .PlayMedium = arguments(7).DataValue.ToString

        End With

        Return mi
    End Function

    Public Function GetPositionInfo() As cPositionInfo
        Dim arguments = New UPnPArgument(8) {}
        arguments(0) = New UPnPArgument("InstanceID", 0UI)
        arguments(1) = New UPnPArgument("Track", 0UI)
        arguments(2) = New UPnPArgument("TrackDuration", Nothing)
        arguments(3) = New UPnPArgument("TrackMetaData", Nothing)
        arguments(4) = New UPnPArgument("TrackURI", Nothing)
        arguments(5) = New UPnPArgument("RelTime", Nothing)
        arguments(6) = New UPnPArgument("AbsTime", Nothing)
        arguments(7) = New UPnPArgument("RelCount", 0)
        arguments(8) = New UPnPArgument("AbsCount", 0)

        Try
            AVTransport.InvokeSync("GetPositionInfo", arguments)
        Catch ex As Exception
            Return PositionInfo                                 '// on error - return existing PositionInfo
        End Try

        Dim pi As New cPositionInfo
        With pi
            .TrackIndex = CUInt(arguments(1).DataValue)
            TimeSpan.TryParse(DirectCast(arguments(2).DataValue, String), .TrackDuration)
            .TrackMetaData = arguments(3).DataValue.ToString
            .TrackURI = arguments(4).DataValue.ToString
            TimeSpan.TryParse(DirectCast(arguments(5).DataValue, String), .RelTime)
            TimeSpan.TryParse(DirectCast(arguments(6).DataValue, String), .AbsTime)
        End With

        Return pi
    End Function

#End Region

#Region "UPNP Transport Actions"

    Public Sub Play()
        Dim arguments = New UPnPArgument(1) {}
        arguments(0) = New UPnPArgument("InstanceID", 0UI)
        arguments(1) = New UPnPArgument("Speed", "1")
        If AVTransport IsNot Nothing Then
            AVTransport.InvokeAsync("Play", arguments)
        End If

        'positionTimer.Change(0, 1000)

        'StartPolling()
    End Sub

    Public Sub Seek(position As UInteger)
        Dim arguments = New UPnPArgument(2) {}
        arguments(0) = New UPnPArgument("InstanceID", 0UI)
        arguments(1) = New UPnPArgument("Unit", "TRACK_NR")
        arguments(2) = New UPnPArgument("Target", position.ToString())
        AVTransport.InvokeAsync("Seek", arguments)
    End Sub

    Public Sub Pause()
        Dim arguments = New UPnPArgument(0) {}
        arguments(0) = New UPnPArgument("InstanceID", 0UI)
        AVTransport.InvokeAsync("Pause", arguments)
    End Sub

    Public Sub [Stop]()
        Dim arguments = New UPnPArgument(0) {}
        arguments(0) = New UPnPArgument("InstanceID", 0UI)
        AVTransport.InvokeAsync("Stop", arguments)
    End Sub

    Public Sub [Next]()
        Dim arguments = New UPnPArgument(0) {}
        arguments(0) = New UPnPArgument("InstanceID", 0UI)
        AVTransport.InvokeAsync("Next", arguments)
    End Sub

    Public Sub Previous()
        Dim arguments = New UPnPArgument(0) {}
        arguments(0) = New UPnPArgument("InstanceID", 0UI)
        AVTransport.InvokeAsync("Previous", arguments)
    End Sub

#End Region

#Region "UPNP Queue/Station Actions"

    Public Sub SetAVTransportURI(track As TrackHolder)
        Dim arguments = New UPnPArgument(2) {}
        arguments(0) = New UPnPArgument("InstanceID", 0UI)
        arguments(1) = New UPnPArgument("CurrentURI", track.Uri)
        arguments(2) = New UPnPArgument("CurrentURIMetaData", track.MetaData)
        AVTransport.InvokeAsync("SetAVTransportURI", arguments)
    End Sub

    Public Function Enqueue(track As TrackHolder, Optional asNext As Boolean = False) As UInteger
        Dim arguments = New UPnPArgument(7) {}
        arguments(0) = New UPnPArgument("InstanceID", 0UI)
        arguments(1) = New UPnPArgument("EnqueuedURI", track.Uri)
        arguments(2) = New UPnPArgument("EnqueuedURIMetaData", track.MetaData)
        arguments(3) = New UPnPArgument("DesiredFirstTrackNumberEnqueued", 0UI)
        arguments(4) = New UPnPArgument("EnqueueAsNext", asNext)
        arguments(5) = New UPnPArgument("FirstTrackNumberEnqueued", Nothing)
        arguments(6) = New UPnPArgument("NumTracksAdded", Nothing)
        arguments(7) = New UPnPArgument("NewQueueLength", Nothing)
        AVTransport.InvokeSync("AddURIToQueue", arguments)

        Return CUInt(arguments(5).DataValue)
    End Function

    Public Function GetQueue() As IList(Of SonosItem)
        Dim searchResult = Browse("Q:0")
        Return SonosItem.Parse(searchResult.Result)
    End Function

#End Region

#Region "UPNP Library Functions"
    Public Overridable Function GetFavorites() As IList(Of SonosItem)
        Dim searchResult = Browse("FV:2")
        Dim tracks = SonosItem.Parse(searchResult.Result)
        Return tracks
    End Function

    Public Overridable Function GetArtists(Optional objectId As String = Nothing, Optional startIndex As UInteger = 0, Optional requestedCount As UInteger = 100) As SearchResult
        If objectId Is Nothing Then
            objectId = "A:ARTIST"
        End If

        Dim searchResult = Browse(objectId, startIndex, requestedCount)
        Return searchResult
    End Function

    Private Function Browse(action As String, Optional startIndex As UInteger = 0UI, Optional requestedCount As UInteger = 100UI) As SearchResult
        Dim arguments = New UPnPArgument(9) {}
        arguments(0) = New UPnPArgument("ObjectID", action)
        arguments(1) = New UPnPArgument("BrowseFlag", "BrowseDirectChildren")
        arguments(2) = New UPnPArgument("Filter", "")
        arguments(3) = New UPnPArgument("StartingIndex", startIndex)
        arguments(4) = New UPnPArgument("RequestedCount", requestedCount)
        arguments(5) = New UPnPArgument("SortCriteria", "")
        arguments(6) = New UPnPArgument("Result", "")
        arguments(7) = New UPnPArgument("NumberReturned", 0UI)
        arguments(8) = New UPnPArgument("TotalMatches", 0UI)
        arguments(9) = New UPnPArgument("UpdateID", 0UI)

        ContentDirectory.InvokeSync("Browse", arguments)

        Dim result = TryCast(arguments(6).DataValue, String)
        Return New SearchResult() With { _
            .Result = result, _
            .StartingIndex = startIndex, _
            .NumberReturned = CUInt(arguments(7).DataValue), _
            .TotalMatches = CUInt(arguments(8).DataValue) _
        }
    End Function
#End Region

#Region "Utility Functions"
    '// given the UPNP string, returns the enum value
    Function GetTransportState(strState As String) As eTransportState
        Select Case strState
            Case "STOPPED"
                Return eTransportState.Stopped
            Case "PLAYING"
                Return eTransportState.Playing
            Case "PAUSED", "PAUSED_PLAYBACK"
                Return eTransportState.Paused
            Case "NO_MEDIA_PRESENT"
                Return eTransportState.NoMediaPresent
            Case "TRANSITIONING"
                Return eTransportState.Paused
            Case ""
                Return eTransportState.Stopped
            Case Else
                EventLogger.Log(Me, EventLogEntryType.Error, "INVALID OR MISSING STATE")
                Return TransportState '// return last known value
        End Select

    End Function

    '// given the UPNP string, returns the enum value
    Function GetTransportStatus(strStatus As String) As eTransportStatus
        Select Case strStatus
            Case "OK"
                Return eTransportStatus.OK
            Case "ERROR_OCCURRED"
                Return eTransportStatus.ERROR
            Case ""
                Return eTransportStatus.UNKNOWN
            Case Else
                EventLogger.Log(Me, EventLogEntryType.Error, "INVALID OR MISSING STATE")
                Return TransportStatus            '// return last known value

        End Select

    End Function

#End Region

#Region "XML Processing Methods"
    '// the main EventChange processor
    Private Sub ProcessEventChangeXML(newState As String)
        Dim xEvent As XElement = XElement.Parse(newState)
        Dim ns As XNamespace = "urn:schemas-upnp-org:metadata-1-0/AVT/"
        Dim r As XNamespace = "urn:schemas-rinconnetworks-com:metadata-1-0/"
        Dim instance As XElement = xEvent.Element(ns + "InstanceID")

        EventLogger.Log(Me, EventLogEntryType.Warning, "ProcessChangeEvent Begun: " & newState)

        ' We can receive other types of change events here. But not everyone has a TransportState - lets try Current Track Duration Instead
        'If instance.Element(ns + "CurrentTrackDuration") Is Nothing Then
        '    EventLogger.Log(Me, EventLogEntryType.Error, "ERROR in ParseChangeXML" & instance.Value)
        '    'Return
        'End If

        '// Start Processing the EventChanged Data

        TransportInfo.CurrentTransportState = GetTransportState(GetAttributeValue(instance, ns + "TransportState", "val"))
        If GetAttributeValue(instance, ns + "NumberOfTracks", "val") <> "" Then
            MediaInfo.NrTracks = GetAttributeValue(instance, ns + "NumberOfTracks", "val")
        End If

        If GetAttributeValue(instance, ns + "CurrentTrack", "val") <> "" Then
            PositionInfo.TrackIndex = GetAttributeValue(instance, ns + "CurrentTrack", "val")
        End If

        PositionInfo.TrackDuration = ParseDuration(GetAttributeValue(instance, ns + "CurrentTrackDuration", "val"))
        UpdatePlayerStatusFromTransportInfo()

        Dim lastCurrentTrack As TrackInfoEx = CurrentTrack
        CurrentTrack = Me.ParseTrackMetadataXML(GetAttributeValue(instance, ns + "CurrentTrackMetaData", "val"), Device.BaseURL.ToString)
        PositionInfo.TrackMetaData = GetAttributeValue(instance, ns + "CurrentTrackMetaData", "val")

        NextTrack = Me.ParseTrackMetadataXML(GetAttributeValue(instance, r + "NextTrackMetaData", "val"), Device.BaseURL.ToString)

        'EventLogger.Log(Me, EventLogEntryType.Warning, "Processed EventChange Data" & newState)

        Try
            '// the core request with all the track data
            PositionInfo = GetPositionInfo()
            '// now get the MediaInfo object (most usefule for #tracks)
            MediaInfo = GetMediaInfo()
            '// transport state (playing etc..) and status (OK etc...)
            TransportInfo = GetTransportInfo()


            EventLogger.Log(Me, EventLogEntryType.Warning, "TransportState: " & TransportInfo.CurrentTransportState)

            '// deal with Transport status stuff
            UpdatePlayerStatusFromTransportInfo()

            '// deal with TrackInfo stuff
            UpdateTrackInfoFromPositionInfo(lastCurrentTrack)

            'EventLogger.Log(Me, EventLogEntryType.Warning, "Ready to poll")
            If TransportInfo.CurrentTransportState = eTransportState.Playing Then
                StartPolling()
            Else
                StopPolling()
            End If

        Catch ex As Exception
            EventLogger.Log(Me, EventLogEntryType.Error, "A serious ParseChangeXML error has occurred: " & ex.StackTrace & vbCrLf & ex.Message)
        End Try
        EventLogger.Log(Me, EventLogEntryType.Warning, "Event Processed.")
        Dim p As Player2 = DirectCast(Me, Player2)
        RaiseEvent StateChanged(Me, ePlayerStateChangeType.SubscriptionEvent)
    End Sub

    Private Sub UpdateTrackInfoFromPositionInfo(lastCurrentTrack As TrackInfoEx)
        '// if the track has changed - push the current track into the prior slot.
        Dim tempTrack As TrackInfoEx = Me.ParseTrackMetadataXML(PositionInfo.TrackMetaData, Device.BaseURL.ToString)
        If lastCurrentTrack.Title <> tempTrack.Title Then
            '// we're playing a new track
            PreviousTrack = lastCurrentTrack
            CurrentTrack = tempTrack
        Else
            CurrentTrack = tempTrack        '// silly, since in theory they are the same...
        End If
        CurrentTrack.AlbumTrackCount = MediaInfo.NrTracks
        EventLogger.Log(Me, EventLogEntryType.Warning, "Num Tracks: " & MediaInfo.NrTracks)
        '// Let's try and get the next track info information. 1st choice is from the newly available media info object. 2nd choice is from the EventChange MetaData
        If MediaInfo.NextURIMetaData <> NOT_IMPLEMENTED Then
            If MediaInfo.NextURIMetaData <> "" Then
                NextTrack = Me.ParseTrackMetadataXML(MediaInfo.NextURIMetaData, Device.BaseURL.ToString)
            End If
            'ElseIf GetAttributeValue(instance, r + "NextTrackMetaData", "val") <> "NOT_IMPLEMENTED" Then
            '    NextTrack = Me.ParseTrackMetadataXML(GetAttributeValue(instance, r + "NextTrackMetaData", "val"), Device.BaseURL.ToString)
        End If

    End Sub

    Private Sub UpdatePlayerStatusFromTransportInfo()
        TransportState = TransportInfo.CurrentTransportState
        TransportStatus = TransportInfo.CurrentTransportStatus
        CurrentTrack.CurrentTrackTime = PositionInfo.RelTime
        CurrentTrack.Duration = PositionInfo.TrackDuration
    End Sub

    Private Function ParseDuration(value As String) As TimeSpan
        If String.IsNullOrEmpty(value) Then
            Return TimeSpan.FromSeconds(0)
        End If
        Return TimeSpan.Parse(value)
    End Function

    Private Function GetAttributeValue(instance As XElement, elementName As XName, attrName As String) As String
        Dim element As XElement = instance.Element(elementName)
        If element IsNot Nothing Then
            Dim attr As XAttribute = element.Attribute(attrName)
            If attr IsNot Nothing Then
                Return attr.Value
            Else
                Debug.Print("ATTRIBUTE NOT FOUND: " & elementName.ToString & "-" & attrName)
            End If
        Else
            Debug.Print("ELEMENT not found:" & elementName.ToString)
        End If

        Return ""
    End Function

    Public Function ParseTrackMetadataXML(xml As String, documentURL As String) As TrackInfoEx
        If xml = "" Then Return New TrackInfoEx
        Dim didl = XElement.Parse(xml)
        Return ParseTrackMetaDataXML(didl, documentURL)
    End Function

    Public Function ParseTrackMetaDataXML(didl As XElement, documentURL As String) As TrackInfoEx
        Dim ns As XNamespace = "urn:schemas-upnp-org:metadata-1-0/DIDL-Lite/"
        Dim dc As XNamespace = "http://purl.org/dc/elements/1.1/"
        Dim upnp As XNamespace = "urn:schemas-upnp-org:metadata-1-0/upnp/"
        Dim r As XNamespace = "urn:schemas-rinconnetworks-com:metadata-1-0/"
        Dim dlna As XNamespace = "urn:schemas-dlna-org:metadata-1-0"

        Dim items = didl.Elements(ns + "item")


        'Dim list = New List(Of SonosDIDL)()
        Dim response = New TrackInfoEx
        If items Is Nothing Then
            Return response
        End If

        For Each item As XElement In items

            response.AlbumArtURI = GetElementValue(item, upnp + "albumArtURI")
            response.Artist = GetElementValue(item, dc + "creator")
            response.Title = GetElementValue(item, dc + "title")
            response.Year = GetElementValue(item, dc + "date")
            response.Album = GetElementValue(item, upnp + "album")
            response.AlbumArtist = GetElementValue(item, r + "albumArtist")
            response.TrackNumber = GetElementValue(item, upnp + "originalTrackNumber")
            response.Genre = GetElementValue(item, upnp + "genre")
            response.ItemClass = GetElementValue(item, upnp + "class")
            response.StreamContent = GetElementValue(item, r + "streamContent")
            'response.MetaData = didl.ToString

            response.Artist = GetElementValue(item, dc + "creator")
            SetElementFilteredValue(response.AlbumArtist, item, upnp + "artist", "role", "AlbumArtist")
            SetElementFilteredValue(response.Artist, item, upnp + "artist", "role", "Performer")
            SetElementFilteredValue(response.AlbumArtURI, item, upnp + "albumArtURI", dlna + "profileID", "JPEG_MED")
            response.AlbumArtURI = FixAlbumArtURI(response.AlbumArtURI, documentURL)
        Next

        Return response
    End Function

    Private Shared Function GetElementValue(instance As XElement, elementName As XName) As String
        Dim element As XElement = instance.Element(elementName)
        If element IsNot Nothing Then
            Return element.Value
        Else
            Debug.Print("ELEMENT not found:" & elementName.ToString)
            Return ""
        End If
    End Function

    Private Shared Sub SetElementFilteredValue(ByRef Value As String, instance As XElement, elementName As XName, attributeName As XName, attributeValue As String)
        For Each element As XElement In instance.Elements(elementName)
            If element.Attribute(attributeName) IsNot Nothing Then
                If element.Attribute(attributeName).Value = attributeValue Then
                    Value = element.Value
                End If
            End If

        Next

    End Sub

    '// Fix AlbumArt URI issues
    Private Shared Function FixAlbumArtURI(rawAlbumArtURI As String, documentURL As String) As String

        If rawAlbumArtURI = "" Then
            Return _missingAlbumArtPath
        Else

            If rawAlbumArtURI.StartsWith("http") Then
                Return rawAlbumArtURI
            End If
            Dim art As String = rawAlbumArtURI

            Dim baseUri As Uri = New Uri(documentURL)
            Dim path As String = art.Substring(0, art.IndexOf("?"c))
            Dim qs As String = art.Substring(art.IndexOf("?"c))
            Dim builder As New UriBuilder(baseUri.Scheme, baseUri.Host, baseUri.Port, path, qs)
            Return builder.Uri.OriginalString
        End If

    End Function
#End Region


End Class







