Imports OpenSource.UPnP




Public Class discovery

#Region "Variable Declarations and Enums"
    Enum eDeviceDiscoveryEvent
        deviceAdded
        deviceRemoved
        managedDeviceAdded
        managedDeviceRemoved
    End Enum

    Enum eManagedDeviceType
        completeDevice
        compoundDevice
        avTranportDevice
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
    Public ManagedDevices As New ArrayList
    Public Devices As New ArrayList

    Public Event deviceDiscoveryEvent(device As UPnPDevice, deviceChangeEvent As eDeviceDiscoveryEvent)
    Public Event serviceSubscriptionEvent(device As UPnPDevice, service As UPnPService, serviceChangeEvent As eServiceSubscriptionEvent)
    Public Event serviceDataEvent(service As UPnPService, sender As UPnPStateVariable, EventValue As Object)

    Public Const HAS_AVTRANSPORT As Integer = 1
    Public Const HAS_CONTENTDIRECTORY As Integer = 2
    Public Const AVTRANSPORT = "urn:upnp-org:serviceId:AVTransport"
    Public Const CONTENTDIRECTORY = "urn:upnp-org:serviceId:ContentDirectory"
#End Region

#Region "Exposed Properties and Methods"
    '// Look inside the ManagedDevices ArrayList and see if the specified device exists
    Public Function isManaged(device As UPnPDevice) As Boolean
        If ManagedDevices.Contains(device) Then
            Return True
        Else
            Return False
        End If
    End Function

    '// Add
    Public Sub AddManagedDevice(device As UPnPDevice)
        Debug.Print("Adding " & device.FriendlyName)
        If CheckChildrenForService(device, CONTENTDIRECTORY) Then
            device.User = eManagedDeviceType.completeDevice
        Else
            device.User = eManagedDeviceType.avTranportDevice
        End If
        _AddManagedDevice(device)
    End Sub

    Public Sub AddManagedDevice(device As UPnPDevice, childDevice As UPnPDevice)
        Debug.Print("Adding " & device.FriendlyName & "+" & childDevice.FriendlyName)
        device.User = eManagedDeviceType.compoundDevice
        device.AddDevice(childDevice)
        _AddManagedDevice(device)
        '_AddManagedDevice(childDevice)
    End Sub
#End Region


#Region "Initialization and Cleanup Routines"

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

#End Region

#Region "Callbacks"
    '// called by the SmartControlPoint when a devcie is added
    Protected Sub HandleAddedDevice(sender As UPnPSmartControlPoint, device As UPnPDevice)
        If Not Devices.Contains(device) Then Devices.Add(device)
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
        RemoveHandler sender.OnSubscriptionRemoved, New UPnPService.OnSubscriptionHandler(AddressOf Me.HandleOnServiceUnSubscribe)
        RemoveHandler sender.OnSubscribe, New UPnPService.UPnPEventSubscribeHandler(AddressOf Me.HandleOnServiceSubscribe)
        RaiseEvent serviceSubscriptionEvent(sender.ParentDevice, sender, eServiceSubscriptionEvent.serviceOnUnsubscribe)
    End Sub

    '// the main event handler of service data responses
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
 

        Dim stateVariables As UPnPStateVariable() = (CType(service, UPnPService)).GetStateVariables()
        For i As Integer = 0 To stateVariables.Length - 1
            Dim V As UPnPStateVariable = stateVariables(i)
            If V.SendEvent Then
                RemoveHandler V.OnModified, New UPnPStateVariable.ModifiedHandler(AddressOf Me.HandleEvents)
            End If
        Next
        service.UnSubscribe(Nothing)
        'Threading.Thread.Sleep(250)



    End Sub

#End Region

#Region "Device & Service Utility Functions"

    '// a utility routine that chacks for a serviceID within a device -- walking up and down the device tree looking at parents and children.
    Public Function CheckForService(device As UPnPDevice, serviceID As String) As Boolean
        If Not device.ParentDevice Is Nothing Then
            Return CheckForService(device.ParentDevice, serviceID)
        Else    '// now let's check for AVTransport somewhere in the tree
            For Each service As UPnPService In device.Services
                If service.ServiceID = serviceID Then
                    Return True
                End If
            Next
            For Each childDevice As UPnPDevice In device.EmbeddedDevices
                For Each service As UPnPService In childDevice.Services
                    If service.ServiceID = serviceID Then
                        Return True
                    End If
                Next
            Next

            Return False
        End If
    End Function

    '// A utility that just checks immediate children for a given ServiceID
    Public Function CheckChildrenForService(device As UPnPDevice, serviceID As String) As Boolean
        If FindChildDeviceWithService(device, serviceID) IsNot Nothing Then
            Return True
        Else
            Return False
        End If
    End Function

    '// Search the device and its embedded devices for the specified service. If found return that Service's parent.
    Public Function FindChildDeviceWithService(device As UPnPDevice, serviceID As String) As UPnPDevice
        For Each service As UPnPService In device.Services
            If service.ServiceID = serviceID Then
                Return device
            End If
        Next
        For Each childDevice As UPnPDevice In device.EmbeddedDevices
            For Each service As UPnPService In childDevice.Services
                If service.ServiceID = serviceID Then
                    Return childDevice
                End If
            Next
        Next
        Return Nothing
    End Function

    '// gets the top most parent device of the passed device
    Public Function FindTopMostDevice(device As UPnPDevice) As UPnPDevice
        If Not device.ParentDevice Is Nothing Then
            Return FindTopMostDevice(device.ParentDevice)
        Else
            Return device
        End If
    End Function

    '// given a device and a ServiceID, go find another device with the same ipaddress that has the target ServiceID in its tree.
    Public Function FindSiblingDevice(device As UPnPDevice, targetServiceID As String) As UPnPDevice
        For Each targetDevice As UPnPDevice In Devices
            If targetDevice.RemoteEndPoint.ToString = device.RemoteEndPoint.ToString Then
                If targetDevice.UniqueDeviceName <> device.UniqueDeviceName Then
                    If CheckForService(targetDevice, targetServiceID) Then
                        Return targetDevice
                        Exit For
                    Else
                        '// we've found ourself! skip to next.
                    End If
                End If
            End If
        Next
        Return Nothing
    End Function
#End Region
    Private Sub _AddManagedDevice(device As UPnPDevice)
        If Not ManagedDevices.Contains(device) Then
            ManagedDevices.Add(device)
            RaiseEvent deviceDiscoveryEvent(device, eDeviceDiscoveryEvent.managedDeviceAdded)
        End If

    End Sub
End Class
