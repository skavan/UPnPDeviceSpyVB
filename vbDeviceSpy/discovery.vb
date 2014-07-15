Imports OpenSource.UPnP

Public Class discovery

#Region "Variable Declarations and Enums"
    Enum eDeviceDiscoveryEvent
        deviceAdded
        deviceRemoved
        deviceAdditionFailed
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

    Enum eDeviceSearchParameter
        LocationURL
        UDN
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

    '// Add a device to the Managed Device ArrayList
    Public Sub AddManagedDevice(device As UPnPDevice)
        Debug.Print("Adding " & device.FriendlyName)
        If CheckChildrenForService(device, CONTENTDIRECTORY) Then
            device.User = eManagedDeviceType.completeDevice
        Else
            device.User = eManagedDeviceType.avTranportDevice
        End If
        _AddManagedDevice(device)
    End Sub

    '// Add a compound (linked) device pair to the ManagedDevice List
    '// If we want to change the linked behaviour this is a good place to start
    Public Sub AddManagedDevice(device As UPnPDevice, childDevice As UPnPDevice)
        Debug.Print(String.Format("Adding {0}+{1}", device.FriendlyName, childDevice.FriendlyName))
        device.User = eManagedDeviceType.compoundDevice
        device.User2 = childDevice.UniqueDeviceName
        device.AddDevice(childDevice)
        _AddManagedDevice(device)
    End Sub

    '// Internal method to do the "adding"
    Private Sub _AddManagedDevice(device As UPnPDevice)
        If Not ManagedDevices.Contains(device) Then
            ManagedDevices.Add(device)
            RaiseEvent deviceDiscoveryEvent(device, eDeviceDiscoveryEvent.managedDeviceAdded)
        End If

    End Sub
#End Region
    '// remove a 'Managed' device.
    Public Sub RemoveManagedDevice(device As UPnPDevice)
        Dim deviceType As eManagedDeviceType = eManagedDeviceType.completeDevice
        If device.User IsNot Nothing Then deviceType = device.User
        Select Case deviceType
            Case eManagedDeviceType.completeDevice, eManagedDeviceType.avTranportDevice
                ManagedDevices.Remove(device)
            Case eManagedDeviceType.compoundDevice
                ManagedDevices.Remove(device)
                UnlinkCompoundDevice(device)
        End Select
        '// let all clients know!
        RaiseEvent deviceDiscoveryEvent(device, eDeviceDiscoveryEvent.managedDeviceRemoved)

    End Sub

#Region "Initialization, Settings and Cleanup Routines"

    Sub New()

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

    Sub SaveSettings()
        Dim savedDevices As New SavedDevices

        For Each device As UPnPDevice In ManagedDevices

            Dim savedDevice As SavedDevice = Nothing
            Dim managedDeviceType As discovery.eManagedDeviceType = eManagedDeviceType.completeDevice
            managedDeviceType = IfNotNothing(device.User, managedDeviceType)
            Select Case managedDeviceType
                Case eManagedDeviceType.completeDevice, eManagedDeviceType.avTranportDevice
                    savedDevice = New SavedDevice(device.FriendlyName, device.UniqueDeviceName, device.LocationURL, managedDeviceType)
                    savedDevices.Add(savedDevice)
                Case eManagedDeviceType.compoundDevice
                    '// lets get the child device (we can optimize this)
                    Dim childDevice As UPnPDevice = GetAvailableDevice(device.User2, eDeviceSearchParameter.UDN)                                                                                             '// get the child device by UDN
                    savedDevice = New SavedDevice(device.FriendlyName, device.UniqueDeviceName, device.LocationURL, managedDeviceType, childDevice.UniqueDeviceName, childDevice.LocationURL)
                    savedDevices.Add(savedDevice)
                    'Dim childSavedDevice = New SavedDevice(childDevice.FriendlyName, childDevice.DeviceURN, childDevice.LocationURL, eManagedDeviceType.compoundDevice)
                    'savedDevices.Add(childSavedDevice)
            End Select

        Next
        My.Settings.SavedDevices = savedDevices
    End Sub

    Sub LoadSettings()
        Dim savedDevices As SavedDevices = My.Settings.SavedDevices
        For Each savedDevice As SavedDevice In savedDevices
            Select Case savedDevice.ManagedDeviceType
                Case eManagedDeviceType.compoundDevice
                    ForceAddDevice(savedDevice.LocationURL, True)
                    ForceAddDevice(savedDevice.LinkedLocationURL, True)

                Case eManagedDeviceType.avTranportDevice, eManagedDeviceType.completeDevice
                    ForceAddDevice(savedDevice.LocationURL, True)
            End Select
        Next
    End Sub

#End Region

#Region "Callbacks"
    '// called by the SmartControlPoint when a devcie is added
    Protected Sub HandleAddedDevice(sender As UPnPSmartControlPoint, device As UPnPDevice)
        If Not Devices.Contains(device) Then Devices.Add(device)
        RaiseEvent deviceDiscoveryEvent(device, eDeviceDiscoveryEvent.deviceAdded)
        AutoLoadManagedDevice(device)
    End Sub

    '// called by the SmartControlPoint when a devcie is removed
    Protected Sub HandleRemovedDevice(sender As UPnPSmartControlPoint, device As UPnPDevice)
        RaiseEvent deviceDiscoveryEvent(device, eDeviceDiscoveryEvent.deviceRemoved)
    End Sub

    '// This is a callback that is called when a Forced Add succeeds and a device is found
    Private Sub HandleForceAddDevice(sender As OpenSource.UPnP.UPnPDeviceFactory, device As OpenSource.UPnP.UPnPDevice, URL As System.Uri)
        If Not Devices.Contains(device) Then Devices.Add(device)
        RaiseEvent deviceDiscoveryEvent(device, eDeviceDiscoveryEvent.deviceAdded)
    End Sub

    '// This is a callback that is called when a Forced Add AT STARTUP succeeds and a device is found
    Private Sub HandleStartupAddDevice(sender As OpenSource.UPnP.UPnPDeviceFactory, device As OpenSource.UPnP.UPnPDevice, URL As System.Uri)
        If Not Devices.Contains(device) Then Devices.Add(device)
        RaiseEvent deviceDiscoveryEvent(device, eDeviceDiscoveryEvent.deviceAdded)
        AutoLoadManagedDevice(device)
    End Sub

    '// as devices arrive, put them in the managed device Array if we have all the required devices
    Private Sub AutoLoadManagedDevice(device As UPnPDevice)
        Dim savedDevices As SavedDevices = My.Settings.SavedDevices
        If savedDevices Is Nothing Then Exit Sub
        For Each savedDevice As SavedDevice In savedDevices
            If savedDevice.UniqueDeviceName = device.UniqueDeviceName Then
                '// we have found a device to auto manage
                device.User = savedDevice.ManagedDeviceType
                device.User2 = savedDevice.LinkedDeviceUDN
                If ManagedDevices.Contains(device) Then Exit Sub '// it's already there! abort
                Dim managedDeviceType As eManagedDeviceType = device.User

                If managedDeviceType = eManagedDeviceType.compoundDevice Then
                    Dim childDevice As UPnPDevice = GetAvailableDevice(savedDevice.LinkedDeviceUDN, eDeviceSearchParameter.UDN)
                    '// if it exists we can make the compound device
                    If childDevice IsNot Nothing Then
                        AddManagedDevice(device, childDevice)
                    Else    '// the child device isn't available. pick up the compound device when the child device arrives
                        '// do nothing. 
                        Exit Sub
                    End If
                Else
                    '// its a complete or AVTransport so add it to managed list.
                    AddManagedDevice(device)
                End If
            ElseIf savedDevice.LinkedDeviceUDN = device.UniqueDeviceName Then
                '// we have the child. Is the parent here?
                Dim parentDevice As UPnPDevice = GetAvailableDevice(savedDevice.UniqueDeviceName, eDeviceSearchParameter.UDN)
                If parentDevice IsNot Nothing Then
                    AddManagedDevice(parentDevice, device)
                    '//We have the parent - create the combo device
                Else
                    '// parent isn't available. get outta here.
                    Exit Sub
                End If
            End If
        Next
    End Sub


    '// This is a callback that is called when a Forced Add FAILS
    Private Sub HandleForceAddFailed(sender As UPnPDeviceFactory, LocationUri As Uri, e As Exception, urn As String)
        RaiseEvent deviceDiscoveryEvent(Nothing, eDeviceDiscoveryEvent.deviceAdditionFailed)
    End Sub

    '// triggered when we try and subscribe to a service.
    Protected Sub HandleOnServiceSubscribe(sender As UPnPService, success As Boolean)
        Debug.Print("SUBSCRIBED " & sender.ServiceURN)
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

    Protected Sub OnUnsubscribe(sender As UPnPService, seq As Long)
        Debug.Print("UNSUBSCRIBED " & sender.ServiceURN)
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

    '// Kick off a newwork scan for upnp devices. Point events to relevant handlers.
    Public Sub BeginNetworkScan()
        '// test 
        'Dim NetworkUri = New Uri("http://192.168.1.199:1400/xml/device_description.xml")
        'df = New UPnPDeviceFactory(NetworkUri, 1800, New UPnPDeviceFactory.UPnPDeviceHandler(AddressOf HandleForceAddDevice), New UPnPDeviceFactory.UPnPDeviceFailedHandler(AddressOf HandleForceAddFailed), System.Net.IPAddress.Parse("192.168.1.103"), "RINCON_000E58A72F9601400")

        'Me.scp = New UPnPSmartControlPoint(New UPnPSmartControlPoint.DeviceHandler(AddressOf Me.HandleAddedDevice), Nothing, {"urn:schemas-upnp-org:service:AVTransport:1", "urn:schemas-upnp-org:service:ConnectionManager:1", "urn:schemas-upnp-org:service:RenderingControl:1"})
        Me.scp = New UPnPSmartControlPoint(New UPnPSmartControlPoint.DeviceHandler(AddressOf Me.HandleAddedDevice))
        AddHandler Me.scp.OnRemovedDevice, New UPnPSmartControlPoint.DeviceHandler(AddressOf Me.HandleRemovedDevice)
    End Sub

    '// method to destroy linked devices and then reload them "clean"
    Private Sub UnlinkCompoundDevice(device As UPnPDevice)
        Dim childDevice As UPnPDevice
        Dim childURL As String = ""
        Dim parentURL As String = device.LocationURL                                            '// Get the location uri

        '// first lets deal with the child
        '// extract the child UDN from User2 on the parent device
        Dim childUDN As String = ""
        '// if we have something to search for...let go and get the child Device
        If device.User2 IsNot Nothing Then
            childUDN = device.User2
            childDevice = GetAvailableDevice(childUDN, eDeviceSearchParameter.UDN)                  '// get the child device by UDN
            If childDevice IsNot Nothing Then                                                       '// if it exists, remove it, kill it
                childURL = childDevice.LocationURL '// cache the URL - we'll need it in a sec
                Devices.Remove(childDevice)
                RaiseEvent deviceDiscoveryEvent(childDevice, eDeviceDiscoveryEvent.deviceRemoved)
                Try
                    scp.ForceDisposeDevice(childDevice)                                             '// Kill the childdevice
                Catch ex As Exception
                    Debug.Print("Couldn't force displose the child device")
                End Try
            End If
        End If

        '//now the parent
        '// if it exists - remove it.
        If GetAvailableDevice(device.UniqueDeviceName, eDeviceSearchParameter.UDN) IsNot Nothing Then Devices.Remove(device)
        RaiseEvent deviceDiscoveryEvent(device, eDeviceDiscoveryEvent.deviceRemoved)
        Try
            scp.ForceDisposeDevice(device)                                                  '//KIll the parent
        Catch ex As Exception
            Debug.Print("Couldn't force displose the parent device")
        End Try



        ForceAddDevice(parentURL, False)
        ForceAddDevice(childURL, False)


    End Sub

    Private Function IfNotNothing(source As Object, defaultValue As Object) As Object
        If source IsNot Nothing Then
            Return source
        Else
            Return defaultValue
        End If
    End Function


    '// Try and force add a known device
    Private Sub ForceAddDevice(deviceDescriptionURL As String, isStartup As Boolean)
        Try
            Dim NetworkUri = New Uri(deviceDescriptionURL)
            'Dim ipList As System.Net.IPAddress() = System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName()).AddressList
            'Dim myIP As System.Net.IPAddress = System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName()).AddressList(1)
            'myIP = LocalIP()
            Dim myIP As System.Net.IPAddress = GetFirstValidIPAddresses()

            Dim hostname As String = System.Net.Dns.GetHostName
            If isStartup Then
                '// is this efficient? Not sure. we use the HandleSTARTUP callback in this case
                Dim df As UPnPDeviceFactory = New UPnPDeviceFactory(NetworkUri, 1800, New UPnPDeviceFactory.UPnPDeviceHandler(AddressOf HandleStartupAddDevice), New UPnPDeviceFactory.UPnPDeviceFailedHandler(AddressOf HandleForceAddFailed), myIP, Nothing)

            Else
                '// is this efficient? Not sure. we use the HandleFORCEADD callback in this case
                Dim df As UPnPDeviceFactory = New UPnPDeviceFactory(NetworkUri, 1800, New UPnPDeviceFactory.UPnPDeviceHandler(AddressOf HandleForceAddDevice), New UPnPDeviceFactory.UPnPDeviceFailedHandler(AddressOf HandleForceAddFailed), myIP, Nothing)

            End If
            
        Catch ex_23 As Exception
            MessageBox.Show("Invalid URI!")
        End Try
    End Sub

    '// Lets subscribe to a service
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

    '// Unsubscribe from a service
    Public Sub UnSubscribe(service As UPnPService)


        Dim stateVariables As UPnPStateVariable() = (CType(service, UPnPService)).GetStateVariables()
        For i As Integer = 0 To stateVariables.Length - 1
            Dim V As UPnPStateVariable = stateVariables(i)
            If V.SendEvent Then
                RemoveHandler V.OnModified, New UPnPStateVariable.ModifiedHandler(AddressOf Me.HandleEvents)
            End If
        Next
        RemoveHandler service.OnSubscriptionRemoved, New UPnPService.OnSubscriptionHandler(AddressOf Me.HandleOnServiceUnSubscribe)
        RemoveHandler service.OnSubscribe, New UPnPService.UPnPEventSubscribeHandler(AddressOf Me.HandleOnServiceSubscribe)

        'AddHandler service.OnSubscriptionRemoved, New UPnPService.OnSubscriptionHandler(AddressOf Me.HandleOnServiceUnSubscribe)
        service.UnSubscribe(Nothing)
        'Threading.Thread.Sleep(250)
        RaiseEvent serviceSubscriptionEvent(service.ParentDevice, service, eServiceSubscriptionEvent.serviceOnUnsubscribe)


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
        If GetChildDeviceWithService(device, serviceID) IsNot Nothing Then
            Return True
        Else
            Return False
        End If
    End Function

    '// Search the device and its embedded devices for the specified service. If found return that Service's parent.
    Public Function GetChildDeviceWithService(device As UPnPDevice, serviceID As String) As UPnPDevice
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
    Public Function GetTopMostDevice(device As UPnPDevice) As UPnPDevice
        If Not device.ParentDevice Is Nothing Then
            Return GetTopMostDevice(device.ParentDevice)
        Else
            Return device
        End If
    End Function

    '// given a device and a ServiceID, go find another device with the same ipaddress that has the target ServiceID in its tree.
    Public Function GetSiblingDevice(device As UPnPDevice, targetServiceID As String) As UPnPDevice
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

    '// Search the Devices Arraylist for a matching device (based on LocationURL) and return it (or Nothing).
    Public Function GetAvailableDevice(searchTerm As String, searchType As eDeviceSearchParameter) As UPnPDevice
        For Each device As UPnPDevice In Devices
            Select Case searchType
                Case eDeviceSearchParameter.LocationURL
                    If device.LocationURL = searchTerm Then Return device
                Case eDeviceSearchParameter.UDN
                    If device.UniqueDeviceName = searchTerm Then Return device
            End Select
        Next
        Return Nothing
    End Function

#End Region




End Class
