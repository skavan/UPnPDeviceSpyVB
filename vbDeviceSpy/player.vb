
Imports OpenSource.UPnP
Imports System.Collections.Generic
Imports System.Linq
Imports System.Threading
Imports System.Xml.Linq

Public Enum PlayerStatus
    Stopped
    Playing
    Paused
End Enum


Public Class Player
    Private m_device As UPnPDevice
    Private m_mediaRenderer As UPnPDevice
    Private m_avTransport As UPnPService
    Private m_mediaServer As UPnPDevice
    Private m_renderingControl As UPnPService
    Private m_contentDirectory As UPnPService
    Private m_currentState As New PlayerState
    Private positionTimer As Timer
    Private m_Name As String
    Private m_UUID As String
    Private m_ControlPoint As UPnPSmartControlPoint

    Public Event StateChanged As Action(Of Player)



    Private Sub SubscribeToEvents()

        If AVTransport Is Nothing Then Exit Sub
        AVTransport.Subscribe(600, AddressOf HandleOnServiceSubscribe)
    End Sub

#Region "Callbacks"


    Private Sub HandleOnServiceSubscribe(sender As UPnPService, success As Boolean)
        If Not success Then Exit Sub
        Dim lastChangeStateVariable = sender.GetStateVariableObject("LastChange")
        AddHandler lastChangeStateVariable.OnModified, AddressOf ChangeTriggered
    End Sub

    Private Sub ChangeTriggered(sender As UPnPStateVariable, value As Object)
        'Console.WriteLine("LastChange from {0}", UUID);
        Dim newState = sender.Value
        'Console.WriteLine(newState);
        ParseChangeXML(DirectCast(newState, String))
    End Sub

#End Region


    Private Sub ParseChangeXML(newState As String)
        Dim xEvent = XElement.Parse(newState)
        Dim ns As XNamespace = "urn:schemas-upnp-org:metadata-1-0/AVT/"
        Dim r As XNamespace = "urn:schemas-rinconnetworks-com:metadata-1-0/"


        Dim instance = xEvent.Element(ns + "InstanceID")

        ' We can receive other types of change events here.
        If instance.Element(ns + "TransportState") Is Nothing Then
            Return
        End If

        Dim preliminaryState = New PlayerState() With { _
            .TransportState = instance.Element(ns + "TransportState").Attribute("val").Value, _
            .NumberOfTracks = instance.Element(ns + "NumberOfTracks").Attribute("val").Value, _
            .CurrentTrack = instance.Element(ns + "CurrentTrack").Attribute("val").Value, _
            .CurrentTrackDuration = ParseDuration(instance.Element(ns + "CurrentTrackDuration").Attribute("val").Value), _
            .CurrentTrackMetaData = instance.Element(ns + "CurrentTrackMetaData").Attribute("val").Value, _
            .NextTrackMetaData = instance.Element(r + "NextTrackMetaData").Attribute("val").Value _
        }

        m_currentState = preliminaryState

        ' every time we have got a state change, do a PositionInfo
        Try
            Dim positionInfo = GetPositionInfo()
            CurrentState.RelTime = positionInfo.RelTime
            ' void
        Catch generatedExceptionName As Exception
        End Try

        CurrentState.LastStateChange = DateTime.Now

        RaiseEvent StateChanged(Me)

        'If StateChanged IsNot Nothing Then
        '    StateChanged.Invoke(Me)
        'End If
    End Sub

    Private Function ParseDuration(value As String) As TimeSpan
        If String.IsNullOrEmpty(value) Then
            Return TimeSpan.FromSeconds(0)
        End If
        Return TimeSpan.Parse(value)
    End Function

#Region "public properties"

    Public Property Name() As String
        Get
            Return m_Name
        End Get
        Set(value As String)
            m_Name = value
        End Set
    End Property

    Public Property UUID() As String
        Get
            Return m_UUID
        End Get
        Set(value As String)
            m_UUID = value
        End Set
    End Property


    Public Property ControlPoint() As UPnPSmartControlPoint
        Get
            Return m_ControlPoint
        End Get
        Set(value As UPnPSmartControlPoint)
            m_ControlPoint = value
        End Set
    End Property


    Public Property Device() As UPnPDevice
        Get
            Return m_device
        End Get
        Set(value As UPnPDevice)
            m_device = value
        End Set
    End Property

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

    Public ReadOnly Property CurrentState() As PlayerState
        Get
            Return m_currentState
        End Get
    End Property

    Public ReadOnly Property CurrentStatus() As PlayerStatus
        Get
            Return GetPlayerStatus()
        End Get
    End Property

    Public ReadOnly Property PlayerInfo() As PlayerInfo
        Get
            Return GetPositionInfo()
        End Get
    End Property

    Public ReadOnly Property MediaInfo() As MediaInfo
        Get
            Return GetMediaInfo()
        End Get
    End Property

    Public ReadOnly Property CurrentTrack() As Track
        Get

            'Throw New NotImplementedException()
            'Return
        End Get
    End Property

    Public ReadOnly Property BaseUrl() As Uri
        Get
            Return Device.BaseURL
        End Get
    End Property

#End Region


    Public Sub SetDevice(playerDevice As UPnPDevice)
        Device = playerDevice
        ' Subscribe to LastChange event
        SubscribeToEvents()

        ' Start a timer that polls for PositionInfo
        'StartPolling();
    End Sub

    Public Sub StartPolling()
        If positionTimer IsNot Nothing Then
            Return
        End If

        If CurrentStatus <> PlayerStatus.Playing Then
            Return
        End If

        positionTimer = New Timer(AddressOf UpdateState, Nothing, TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(30))
    End Sub

    Private Sub UpdateState(state As Object)
        Dim positionInfo = GetPositionInfo()
        CurrentState.RelTime = positionInfo.RelTime
        CurrentState.LastStateChange = DateTime.Now

        RaiseEvent StateChanged(Me)
        Select Case m_currentState.TransportState
            Case "PLAYING"
            Case "PAUSED", "STOPPED"
                positionTimer.Dispose()
            Case Else
                positionTimer.Dispose()
        End Select
        'If StateChanged IsNot Nothing Then
        '    StateChanged.Invoke(Me)
        'End If
    End Sub

#Region "AVTransport Get Various Info (playerstatus, mediainfo, positioninfo"

    Public Function GetPlayerStatus() As PlayerStatus
        If AVTransport Is Nothing Then
            Return PlayerStatus.Stopped
        End If
        Dim arguments = New UPnPArgument(3) {}
        arguments(0) = New UPnPArgument("InstanceID", 0UI)
        arguments(1) = New UPnPArgument("CurrentTransportState", "")
        arguments(2) = New UPnPArgument("CurrentTransportStatus", "")
        arguments(3) = New UPnPArgument("CurrentSpeed", "")

        Try
            AVTransport.InvokeSync("GetTransportInfo", arguments)
        Catch ex As UPnPInvokeException
            Return PlayerStatus.Stopped
        End Try

        'Dim status As PlayerStatus

        Select Case DirectCast(arguments(1).DataValue, String)
            Case "PLAYING"
                Return PlayerStatus.Playing
                Exit Select
            Case "PAUSED"
                Return PlayerStatus.Paused
                Exit Select
            Case Else
                Return PlayerStatus.Stopped
        End Select
    End Function

    Public Function GetMediaInfo() As MediaInfo
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
        AVTransport.InvokeSync("GetMediaInfo", arguments)

        Return New MediaInfo() With { _
            .NrOfTracks = CUInt(arguments(1).DataValue) _
        }
    End Function

    Public Function GetPositionInfo() As PlayerInfo
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
        AVTransport.InvokeSync("GetPositionInfo", arguments)

        Dim trackDuration As TimeSpan
        Dim relTime As TimeSpan

        TimeSpan.TryParse(DirectCast(arguments(2).DataValue, String), trackDuration)
        TimeSpan.TryParse(DirectCast(arguments(5).DataValue, String), relTime)
        Return New PlayerInfo() With { _
            .TrackIndex = CUInt(arguments(1).DataValue), _
            .TrackMetaData = DirectCast(arguments(3).DataValue, String), _
            .TrackURI = DirectCast(arguments(4).DataValue, String), _
            .TrackDuration = trackDuration, _
            .RelTime = relTime _
        }
    End Function
#End Region



    Public Sub SetAVTransportURI(track As Track)
        Dim arguments = New UPnPArgument(2) {}
        arguments(0) = New UPnPArgument("InstanceID", 0UI)
        arguments(1) = New UPnPArgument("CurrentURI", track.Uri)
        arguments(2) = New UPnPArgument("CurrentURIMetaData", track.MetaData)
        AVTransport.InvokeAsync("SetAVTransportURI", arguments)
    End Sub

#Region "RenderingControl Actions"

    Public Function GetMute() As Boolean
        Dim arguments = New UPnPArgument(2) {}
        arguments(0) = New UPnPArgument("InstanceID", 0UI)
        arguments(1) = New UPnPArgument("Channel", "Master")
        arguments(2) = New UPnPArgument("CurrentMute", Nothing)
        ' out
        RenderingControl.InvokeSync("GetMute", arguments)

        Dim result = arguments(2).DataValue.ToString()
        Return Boolean.Parse(result)
    End Function

    Public Sub SetMute(desiredMute As Boolean)
        Dim arguments = New UPnPArgument(2) {}
        arguments(0) = New UPnPArgument("InstanceID", 0UI)
        arguments(1) = New UPnPArgument("Channel", "Master")
        arguments(2) = New UPnPArgument("DesiredMute", desiredMute)
        RenderingControl.InvokeAsync("SetMute", arguments)
    End Sub

    Public Function GetVolume() As Integer
        Dim arguments = New UPnPArgument(2) {}
        arguments(0) = New UPnPArgument("InstanceID", 0UI)
        arguments(1) = New UPnPArgument("Channel", "Master")
        arguments(2) = New UPnPArgument("CurrentVolume", Nothing)
        ' out
        RenderingControl.InvokeSync("GetVolume", arguments)

        Dim result = arguments(2).DataValue.ToString()
        Return Int32.Parse(result)
    End Function

    Public Sub SetVolume(desiredVolume As UInt16)
        Dim arguments = New UPnPArgument(2) {}
        arguments(0) = New UPnPArgument("InstanceID", 0UI)
        arguments(1) = New UPnPArgument("Channel", "Master")
        arguments(2) = New UPnPArgument("DesiredVolume", desiredVolume)
        RenderingControl.InvokeSync("SetVolume", arguments)
    End Sub

#End Region

#Region "Transport Actions"

    Public Sub Play()
        Dim arguments = New UPnPArgument(1) {}
        arguments(0) = New UPnPArgument("InstanceID", 0UI)
        arguments(1) = New UPnPArgument("Speed", "1")
        AVTransport.InvokeAsync("Play", arguments)
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

#End Region

    Public Function Enqueue(track As Track, Optional asNext As Boolean = False) As UInteger
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

#Region "Library Functions"
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


End Class

Public Class SearchResult
    Public Property Result() As String
        Get
            Return m_Result
        End Get
        Set(value As String)
            m_Result = value
        End Set
    End Property
    Private m_Result As String
    Public Property StartingIndex() As UInteger
        Get
            Return m_StartingIndex
        End Get
        Set(value As UInteger)
            m_StartingIndex = value
        End Set
    End Property
    Private m_StartingIndex As UInteger
    Public Property NumberReturned() As UInteger
        Get
            Return m_NumberReturned
        End Get
        Set(value As UInteger)
            m_NumberReturned = value
        End Set
    End Property
    Private m_NumberReturned As UInteger
    Public Property TotalMatches() As UInteger
        Get
            Return m_TotalMatches
        End Get
        Set(value As UInteger)
            m_TotalMatches = value
        End Set
    End Property
    Private m_TotalMatches As UInteger
End Class

Public Class PlayerState
    Public Property TransportState() As String
        Get
            Return m_TransportState
        End Get
        Set(value As String)
            m_TransportState = value
        End Set
    End Property
    Private m_TransportState As String
    Public Property NumberOfTracks() As String
        Get
            Return m_NumberOfTracks
        End Get
        Set(value As String)
            m_NumberOfTracks = value
        End Set
    End Property
    Private m_NumberOfTracks As String
    Public Property CurrentTrack() As String
        Get
            Return m_CurrentTrack
        End Get
        Set(value As String)
            m_CurrentTrack = value
        End Set
    End Property
    Private m_CurrentTrack As String
    Public Property CurrentTrackDuration() As TimeSpan
        Get
            Return m_CurrentTrackDuration
        End Get
        Set(value As TimeSpan)
            m_CurrentTrackDuration = value
        End Set
    End Property
    Private m_CurrentTrackDuration As TimeSpan
    Public Property CurrentTrackMetaData() As String
        Get
            Return m_CurrentTrackMetaData
        End Get
        Set(value As String)
            m_CurrentTrackMetaData = value
        End Set
    End Property
    Private m_CurrentTrackMetaData As String
    Public Property LastStateChange() As DateTime
        Get
            Return m_LastStateChange
        End Get
        Set(value As DateTime)
            m_LastStateChange = value
        End Set
    End Property
    Private m_LastStateChange As DateTime
    Public Property RelTime() As TimeSpan
        Get
            Return m_RelTime
        End Get
        Set(value As TimeSpan)
            m_RelTime = value
        End Set
    End Property
    Private m_RelTime As TimeSpan
    Public Property NextTrackMetaData() As String
        Get
            Return m_NextTrackMetaData
        End Get
        Set(value As String)
            m_NextTrackMetaData = value
        End Set
    End Property
    Private m_NextTrackMetaData As String
End Class

Public Class Track
    Public Property Uri() As String
        Get
            Return m_Uri
        End Get
        Set(value As String)
            m_Uri = value
        End Set
    End Property

    Private m_Uri As String

    Public Property MetaData() As String
        Get
            Return m_MetaData
        End Get
        Set(value As String)
            m_MetaData = value
        End Set
    End Property

    Private m_MetaData As String
End Class

Public Class SonosItem

    Public Overridable Property Track() As Track
        Get
            Return m_Track
        End Get
        Set(value As Track)
            m_Track = value
        End Set
    End Property
    Private m_Track As Track
    Public Overridable Property DIDL() As SonosDIDL
        Get
            Return m_DIDL
        End Get
        Set(value As SonosDIDL)
            m_DIDL = value
        End Set
    End Property
    Private m_DIDL As SonosDIDL

    Public Shared Function Parse(xmlString As String) As IList(Of SonosItem)
        Dim xml = XElement.Parse(xmlString)
        Dim ns As XNamespace = "urn:schemas-upnp-org:metadata-1-0/DIDL-Lite/"
        Dim dc As XNamespace = "http://purl.org/dc/elements/1.1/"
        Dim upnp As XNamespace = "urn:schemas-upnp-org:metadata-1-0/upnp/"
        Dim r As XNamespace = "urn:schemas-rinconnetworks-com:metadata-1-0/"

        Dim items = xml.Elements(ns + "item")

        Dim list = New List(Of SonosItem)()

        For Each item As XElement In items
            Dim track = New Track()

            'track.Uri = DirectCast(item.Element(ns + "res"), String)
            'track.MetaData = DirectCast(item.Element(r + "resMD"), String)

            track.Uri = item.Element(ns + "res").ToString
            track.MetaData = item.Element(r + "resMD").ToString


            ' fix didl if exist
            Dim didl = New SonosDIDL()
            'didl.AlbumArtURI = DirectCast(item.Element(upnp + "albumArtURI"), String)
            'didl.Artist = DirectCast(item.Element(dc + "creator"), String)
            'didl.Title = DirectCast(item.Element(dc + "title"), String)
            'didl.Description = DirectCast(item.Element(r + "description"), String)

            didl.AlbumArtURI = item.Element(upnp + "albumArtURI").ToString
            didl.Artist = item.Element(dc + "creator").ToString
            didl.Title = item.Element(dc + "title").ToString
            didl.Description = item.Element(r + "description").ToString


            list.Add(New SonosItem() With { _
                .Track = track, _
                .DIDL = didl _
            })
        Next

        Return list
    End Function
End Class


Public Class PlayerInfo
    Public Property TrackURI() As String
        Get
            Return m_TrackURI
        End Get
        Set(value As String)
            m_TrackURI = Value
        End Set
    End Property
    Private m_TrackURI As String
    Public Property TrackIndex() As UInteger
        Get
            Return m_TrackIndex
        End Get
        Set(value As UInteger)
            m_TrackIndex = Value
        End Set
    End Property
    Private m_TrackIndex As UInteger
    Public Property TrackMetaData() As String
        Get
            Return m_TrackMetaData
        End Get
        Set(value As String)
            m_TrackMetaData = Value
        End Set
    End Property
    Private m_TrackMetaData As String
    Public Property RelTime() As TimeSpan
        Get
            Return m_RelTime
        End Get
        Set(value As TimeSpan)
            m_RelTime = Value
        End Set
    End Property
    Private m_RelTime As TimeSpan
    Public Property TrackDuration() As TimeSpan
        Get
            Return m_TrackDuration
        End Get
        Set(value As TimeSpan)
            m_TrackDuration = Value
        End Set
    End Property
    Private m_TrackDuration As TimeSpan
End Class

Public Class SonosDIDL
    Public Property AlbumArtURI() As String
        Get
            Return m_AlbumArtURI
        End Get
        Set(value As String)
            m_AlbumArtURI = value
        End Set
    End Property
    Private m_AlbumArtURI As String
    Public Property Title() As String
        Get
            Return m_Title
        End Get
        Set(value As String)
            m_Title = value
        End Set
    End Property
    Private m_Title As String
    Public Property Artist() As String
        Get
            Return m_Artist
        End Get
        Set(value As String)
            m_Artist = value
        End Set
    End Property
    Private m_Artist As String
    Public Property Album() As String
        Get
            Return m_Album
        End Get
        Set(value As String)
            m_Album = value
        End Set
    End Property
    Private m_Album As String
    Public Property Uri() As String
        Get
            Return m_Uri
        End Get
        Set(value As String)
            m_Uri = value
        End Set
    End Property
    Private m_Uri As String
    Public Property Description() As String
        Get
            Return m_Description
        End Get
        Set(value As String)
            m_Description = value
        End Set
    End Property
    Private m_Description As String

    Public Shared Function Parse(xml As String) As IList(Of SonosDIDL)
        Dim didl = XElement.Parse(xml)
        Return Parse(didl)
    End Function

    Public Shared Function Parse(didl As XElement) As IList(Of SonosDIDL)
        Dim ns As XNamespace = "urn:schemas-upnp-org:metadata-1-0/DIDL-Lite/"
        Dim dc As XNamespace = "http://purl.org/dc/elements/1.1/"
        Dim upnp As XNamespace = "urn:schemas-upnp-org:metadata-1-0/upnp/"
        Dim r As XNamespace = "urn:schemas-rinconnetworks-com:metadata-1-0/"

        Dim items = didl.Elements(ns + "item")

        Dim list = New List(Of SonosDIDL)()

        For Each item As XElement In items
            Dim response = New SonosDIDL()
            response.AlbumArtURI = item.Element(upnp + "albumArtURI").Value
            response.Artist = item.Element(dc + "creator").Value
            response.Title = item.Element(dc + "title").Value
            list.Add(response)
        Next

        Return list
    End Function
End Class

Public Class MediaInfo
    Public Property NrOfTracks() As UInteger
        Get
            Return m_NrOfTracks
        End Get
        Set(value As UInteger)
            m_NrOfTracks = Value
        End Set
    End Property
    Private m_NrOfTracks As UInteger
End Class

