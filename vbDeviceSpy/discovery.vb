Imports OpenSource.UPnP




Public Class discovery

    Enum eDeviceDiscoveryEvent
        deviceAdded
        deviceRemoved
    End Enum

    Enum eServiceSubscriptionEvent
        serviceOnSubscribe
        serviceFailedToSubscribe
        serviceOnUnsubscribe
    End Enum

    Private SubscribeTime As Integer = 300
    Protected scp As UPnPSmartControlPoint
    Private df As UPnPDeviceFactory
    Private SubscribedList As New ArrayList
    Public Event deviceDiscoveryEvent(device As UPnPDevice, deviceChangeEvent As eDeviceDiscoveryEvent)
    Public Event serviceSubscriptionEvent(device As UPnPDevice, service As UPnPService, serviceChangeEvent As eServiceSubscriptionEvent)
    Public Event serviceDataEvent(service As UPnPService, sender As UPnPStateVariable, EventValue As Object)


    Sub New()

    End Sub

    Public Sub Init()
        '// test 
        'Dim NetworkUri = New Uri("http://192.168.1.199:1400/xml/device_description.xml")
        'df = New UPnPDeviceFactory(NetworkUri, 1800, New UPnPDeviceFactory.UPnPDeviceHandler(AddressOf HandleForceAddDevice), New UPnPDeviceFactory.UPnPDeviceFailedHandler(AddressOf HandleForceAddFailed), System.Net.IPAddress.Parse("192.168.1.103"), "RINCON_000E58A72F9601400")

        'Me.scp = New UPnPSmartControlPoint(New UPnPSmartControlPoint.DeviceHandler(AddressOf Me.HandleAddedDevice), Nothing, {"urn:schemas-upnp-org:service:AVTransport:1", "urn:schemas-upnp-org:service:ConnectionManager:1", "urn:schemas-upnp-org:service:RenderingControl:1"})
        Me.scp = New UPnPSmartControlPoint(New UPnPSmartControlPoint.DeviceHandler(AddressOf Me.HandleAddedDevice))
        AddHandler Me.scp.OnRemovedDevice, New UPnPSmartControlPoint.DeviceHandler(AddressOf Me.HandleRemovedDevice)
    End Sub

    Public Sub CleanUp()
        '// unsubscribe to all subscribed services!
        For Each service As UPnPService In SubscribedList
            service.UnSubscribe(Nothing)
        Next
        For Each device As UPnPDevice In scp.Devices
            Try
                scp.ForceDisposeDevice(device)
            Catch ex As Exception
                Debug.Print("Couldn't kill :" & device.FriendlyName)
            End Try
        Next
        scp.Devices.Clear()
        scp = Nothing
    End Sub

#Region "Callbacks"
    '// called by the SmartControlPoint when a devcie is added
    Protected Sub HandleAddedDevice(sender As UPnPSmartControlPoint, device As UPnPDevice)
        RaiseEvent deviceDiscoveryEvent(device, eDeviceDiscoveryEvent.deviceAdded)
    End Sub

    '// called by the SmartControlPoint when a devcie is removed
    Protected Sub HandleRemovedDevice(sender As UPnPSmartControlPoint, device As UPnPDevice)
        RaiseEvent deviceDiscoveryEvent(device, eDeviceDiscoveryEvent.deviceRemoved)
    End Sub

    '// triggered when we try and subscribe to a service.
    Protected Sub HandleOnServiceSubscribe(sender As UPnPService, success As Boolean)
        If success Then
            RaiseEvent serviceSubscriptionEvent(sender.ParentDevice, sender, eServiceSubscriptionEvent.serviceOnSubscribe)
        Else
            RaiseEvent serviceSubscriptionEvent(sender.ParentDevice, sender, eServiceSubscriptionEvent.serviceFailedToSubscribe)
        End If

    End Sub

    '// triggered when we unsubscribe from a service.
    Protected Sub HandleOnServiceUnSubscribe(sender As UPnPService)
        RaiseEvent serviceSubscriptionEvent(sender.ParentDevice, sender, eServiceSubscriptionEvent.serviceOnUnsubscribe)
    End Sub

    '// the main event!
    Protected Sub HandleEvents(sender As UPnPStateVariable, EventValue As Object)
        Dim device As UPnPDevice = sender.OwningService.ParentDevice
        Dim service As UPnPService = sender.OwningService
        Debug.Print("Event Fired!")
        RaiseEvent serviceDataEvent(service, sender, EventValue)
    End Sub
#End Region
#Region "UPNP Device & Service Actions"

    Public Sub Subscribe(service As UPnPService)
        AddHandler service.OnSubscribe, New UPnPService.UPnPEventSubscribeHandler(AddressOf Me.HandleOnServiceSubscribe)
        AddHandler service.OnSubscriptionRemoved, New UPnPService.OnSubscriptionHandler(AddressOf Me.HandleOnServiceUnSubscribe)

        Dim stateVariables As UPnPStateVariable() = (CType(service, UPnPService)).GetStateVariables()
        For i As Integer = 0 To stateVariables.Length - 1
            Dim V As UPnPStateVariable = stateVariables(i)
            AddHandler V.OnModified, New UPnPStateVariable.ModifiedHandler(AddressOf Me.HandleEvents)
        Next
        service.Subscribe(Me.SubscribeTime, Nothing)
    End Sub

    Public Sub UnSubscribe(service As UPnPService)
        RemoveHandler service.OnSubscribe, New UPnPService.UPnPEventSubscribeHandler(AddressOf Me.HandleOnServiceSubscribe)
        RemoveHandler service.OnSubscriptionRemoved, New UPnPService.OnSubscriptionHandler(AddressOf Me.HandleOnServiceUnSubscribe)

        Dim stateVariables As UPnPStateVariable() = (CType(service, UPnPService)).GetStateVariables()
        For i As Integer = 0 To stateVariables.Length - 1
            Dim V As UPnPStateVariable = stateVariables(i)
            If V.SendEvent Then
                RemoveHandler V.OnModified, New UPnPStateVariable.ModifiedHandler(AddressOf Me.HandleEvents)
            End If
        Next
        service.UnSubscribe(Nothing)
    End Sub

#End Region

End Class
