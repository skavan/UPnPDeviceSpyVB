
Imports OpenSource.UPnP

Public Class formForceAdd
    Public Delegate Sub UpdateTreeDelegate(node As TreeNode)
    Private SubscribeTime As Integer = 300
    Protected scp As UPnPSmartControlPoint

    Private ForceDeviceList As Hashtable = New Hashtable()
    Private Sub formForceAdd_HandleDestroyed(sender As Object, e As EventArgs) Handles Me.HandleDestroyed

    End Sub

    Private Sub formForceAdd_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.SubscribeTime = 300
        Me.ForceDeviceList = New Hashtable()

        'Me.scp = New UPnPSmartControlPoint(AddressOf Me.HandleAddedDevice)
        'Me.scp.ForceDeviceAddition("http://192.168.1.199:1400/xml/device_description.xml")
        ''Me.scp = New UPnPSmartControlPoint(New UPnPSmartControlPoint.DeviceHandler(AddressOf Me.HandleAddedDevice))
        'Me.scp = New UPnPSmartControlPoint(New UPnPSmartControlPoint.DeviceHandler(AddressOf Me.HandleAddedDevice), "http://192.168.1.199:1400/xml/device_description.xml")
        'AddHandler Me.scp.OnRemovedDevice, New UPnPSmartControlPoint.DeviceHandler(AddressOf Me.HandleRemovedDevice)

    End Sub
End Class