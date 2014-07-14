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
    'Public Property IsParentDevice As Boolean
    Public Property ManagedDeviceType As discovery.eManagedDeviceType
    Public Property LocationURL As String

    Public Property LinkedDeviceUDN As String
    Public Property LinkedLocationURL As String

    'Public Property DeviceCode As String
    Public Property SubscribedServices As ArrayList

    Sub New()
    End Sub

    Sub New(displayName As String, uniqueDeviceName As String, locationURL As String, deviceType As discovery.eManagedDeviceType)
        Me.Init(displayName, uniqueDeviceName, locationURL, deviceType, "", "")
    End Sub


    Sub New(displayName As String, uniqueDeviceName As String, locationURL As String, deviceType As discovery.eManagedDeviceType, linkedDeviceUDN As String, linkedLocationURL As String)
        Me.Init(displayName, uniqueDeviceName, locationURL, deviceType, linkedDeviceUDN, linkedLocationURL)

    End Sub

    Sub Init(displayName As String, uniqueDeviceName As String, locationURL As String, deviceType As discovery.eManagedDeviceType, linkedDeviceUDN As String, linkedLocationURL As String)
        Me.DisplayName = displayName
        Me.UniqueDeviceName = uniqueDeviceName
        Me.ManagedDeviceType = deviceType
        Me.LocationURL = locationURL
        Me.LinkedDeviceUDN = LinkedDeviceUDN
        Me.LinkedLocationURL = LinkedLocationURL
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
