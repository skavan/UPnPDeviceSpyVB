Imports System.Configuration

'// A Small Class to hold the collection of saved devices
<SettingsSerializeAs(SettingsSerializeAs.Xml)>
Public Class SavedDevices
    Inherits System.Collections.ObjectModel.Collection(Of SavedDevice)
End Class

Public Class SavedDevice
    Implements IEquatable(Of SavedDevice)

    Public Property DisplayName As String
    Public Property UniqueDeviceName As String
    Public Property IsParentDevice As Boolean
    Public Property LinkedDeviceUDN As String
    Public Property LocationURL As String
    Public Property LinkedLocationURL As String
    Public Property DeviceCode As String
    Public Property SubscribedServices As ArrayList

    Sub New()
    End Sub

    Sub New(displayName As String, uniqueDeviceName As String, locationURL As String)
        Init(displayName, uniqueDeviceName, False, locationURL, "")
    End Sub

    Sub New(displayName As String, uniqueDeviceName As String, isLinkedDevice As Boolean, locationURL As String, linkedLocationURL As String, deviceCode As String)
        Me.DeviceCode = deviceCode
        Init(displayName, uniqueDeviceName, isLinkedDevice, locationURL, linkedLocationURL)
    End Sub

    Sub New(displayName As String, uniqueDeviceName As String, isLinkedDevice As Boolean, locationURL As String, linkedLocationURL As String)
        Init(displayName, uniqueDeviceName, isLinkedDevice, locationURL, linkedLocationURL)
    End Sub

    Sub Init(displayName As String, uniqueDeviceName As String, isLinkedDevice As Boolean, locationURL As String, linkedLocationURL As String)
        Me.DisplayName = displayName
        Me.UniqueDeviceName = uniqueDeviceName
        Me.IsParentDevice = isLinkedDevice
        Me.LocationURL = locationURL
        Me.LinkedLocationURL = linkedLocationURL
    End Sub


    'Private Sub Init(displayName As String, uniqueDeviceName As String, isLinkedDevice As Boolean, linkedDevice As String)
    '    Me.DisplayName = displayName
    '    Me.UniqueDeviceName = uniqueDeviceName
    '    Me.IsLinkedDevice = isLinkedDevice
    '    Me.LinkedDeviceName = linkedDevice
    'End Sub

    Public Function Equals1(other As SavedDevice) As Boolean Implements IEquatable(Of SavedDevice).Equals
        If Me.UniqueDeviceName = other.UniqueDeviceName Then Return True Else Return False
    End Function
End Class
