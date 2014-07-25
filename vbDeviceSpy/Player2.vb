Public Enum ePlayListType
    saved
    mixed
    album
    stream
End Enum

Public Enum eTransportState
    Stopped
    Playing
    Paused
    Recording
    NoMediaPresent
End Enum

Public Enum eTransportStatus
    OK
    [ERROR]
    UNKNOWN
End Enum

'// A simple class to hold everything one needs to know about a player.
'// Note we have a trackInfo class (the basic info about a track of music AND
'// A TrackInfoEx class that is only relevant as the currenttrack since it adds properties such
'// as currentTrcakTime

Public Class Player2
    Public Property Name() As String = ""
    Public Property ID() As String = ""
    Public Property UUID() As String = ""
    Public Property TransportState As eTransportState = eTransportState.Stopped
    Public Property TransportStatus As eTransportStatus = eTransportStatus.UNKNOWN


    Public Property PreviousTrack As TrackInfoEx = New TrackInfoEx
    Public Property CurrentTrack As TrackInfoEx = New TrackInfoEx
    Public Property NextTrack As TrackInfoEx = New TrackInfoEx
    Public Property BaseUrl() As Uri
    Public Property IsRepeat As Boolean
    Public Property IsShuffle As Boolean

    '// helper shortcut
    Public ReadOnly Property CurrentTrackTime As TimeSpan
        Get
            Return CurrentTrack.CurrentTrackTime
            'Return PositionInfo.RelTime
        End Get

    End Property

    '// helper shortcut
    Public ReadOnly Property CurrentTrackDuration As TimeSpan
        Get
            Return CurrentTrack.Duration
        End Get
    End Property

End Class

'Public Class xTrackInfoEx
'    Inherits TrackInfoEx
'    Public Property CurrentTrackTime As TimeSpan
'End Class

Public Class Queue
    Public Property Name() As String = ""
    Public Property ID() As String = ""
    Public Property TrackCount As Integer
    Public Property PlayListType As ePlayListType
    Public Property PlayListIndex As Integer

End Class

Public Class TrackInfoEx

    Public Property AlbumArtist As String
    Public Property Title As String
    Public Property Artist As String
    Public Property Album As String
    Public Property Duration As TimeSpan
    Public Property CurrentTrackTime As TimeSpan
    Public Property AlbumArtURI As String
    Public Property TrackNumber As String
    Public Property AlbumTrackCount As String
    Public Property Genre As String
    Public Property Year As String

    Public Property ItemClass As String
    Public Property StreamContent As String
    Public Property FileURL As String

    Public Property Uri As String
    Public Property Description As String

End Class

Public Class SearchResult
    Public Property Result() As String
    Public Property StartingIndex() As UInteger
    Public Property NumberReturned() As UInteger
    Public Property TotalMatches() As UInteger
End Class

Public Class TrackHolder
    Public Property Uri() As String
    Public Property MetaData() As String
End Class

Public Class SonosItem

    Public Overridable Property Track() As TrackHolder
    Public Overridable Property DIDL() As SonosDIDL

    Public Shared Function Parse(xmlString As String) As IList(Of SonosItem)

        Dim xml = XElement.Parse(xmlString)
        Dim ns As XNamespace = "urn:schemas-upnp-org:metadata-1-0/DIDL-Lite/"
        Dim dc As XNamespace = "http://purl.org/dc/elements/1.1/"
        Dim upnp As XNamespace = "urn:schemas-upnp-org:metadata-1-0/upnp/"
        Dim r As XNamespace = "urn:schemas-rinconnetworks-com:metadata-1-0/"

        Dim items = xml.Elements(ns + "item")

        Dim list = New List(Of SonosItem)()

        For Each item As XElement In items
            Dim track = New TrackHolder()

            'track.Uri = DirectCast(item.Element(ns + "res"), String)
            'track.MetaData = DirectCast(item.Element(r + "resMD"), String)

            track.Uri = item.Element(ns + "res").ToString
            track.MetaData = item.Element(r + "resMD").ToString

            ' fix didl if exist
            Dim didl = New SonosDIDL()

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

Public Class cPositionInfo
    Public Property TrackIndex() As UInteger = 0
    Public Property TrackDuration() As TimeSpan = New TimeSpan(0, 0, 0)
    Public Property TrackMetaData() As String = ""
    Public Property TrackURI() As String = ""
    Public Property RelTime() As TimeSpan = New TimeSpan(0, 0, 0)
    Public Property AbsTime() As TimeSpan = New TimeSpan(0, 0, 0)
End Class

Public Class cTransportInfo
    Public Property CurrentTransportState As eTransportState = eTransportState.NoMediaPresent
    Public Property CurrentTransportStatus As eTransportStatus = eTransportStatus.UNKNOWN
    Public Property CurrentSpeed As String = ""
End Class

Public Class cMediaInfo
    Public Property NrTracks As Integer = 0
    Public Property MediaDuration As TimeSpan = New TimeSpan(0, 0, 0)
    Public Property CurrentURI As String = ""
    Public Property CurrentURIMetaData As String = ""
    Public Property NextURI As String = ""
    Public Property NextURIMetaData As String = ""
    Public Property PlayMedium As String = ""
End Class

Public Class SonosDIDL

    Public Property AlbumArtURI() As String
    Public Property Title() As String
    Public Property Artist() As String
    Public Property Album() As String
    Public Property Uri() As String
    Public Property Description() As String


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