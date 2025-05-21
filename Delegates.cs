using IRIS.Bluetooth.Common.Abstract;

namespace IRIS.Bluetooth.Common
{
    /// <summary>
    ///     Represents the method that handles characteristic value change notifications.
    /// </summary>
    /// <param name="characteristic">The characteristic whose value has changed</param>
    /// <param name="newValue">The new value of the characteristic</param>
    /// <remarks>
    ///     This delegate is used to handle notifications when a characteristic's value changes.
    ///     It is typically used in conjunction with the <see cref="IBluetoothLECharacteristic.SubscribeAsync" /> method.
    /// </remarks>
    public delegate void CharacteristicValueChangedHandler(
        IBluetoothLECharacteristic characteristic,
        byte[] newValue);

    /// <summary>
    ///     Represents the method that handles device discovery events.
    /// </summary>
    /// <param name="sender">The Bluetooth interface that discovered the device</param>
    /// <param name="device">The discovered Bluetooth device</param>
    /// <remarks>
    ///     This delegate is used to handle events when a new Bluetooth device is discovered during scanning.
    /// </remarks>
    public delegate void DeviceDiscoveredHandler(IBluetoothLEInterface sender, IBluetoothLEDevice device);

    /// <summary>
    ///     Represents the method that handles device connection events.
    /// </summary>
    /// <param name="sender">The Bluetooth interface that connected to the device</param>
    /// <param name="device">The connected Bluetooth device</param>
    /// <remarks>
    ///     This delegate is used to handle events when a connection to a Bluetooth device is successfully established.
    /// </remarks>
    public delegate void DeviceConnectedHandler(IBluetoothLEInterface sender, IBluetoothLEDevice device);

    /// <summary>
    ///     Represents the method that handles device disconnection events.
    /// </summary>
    /// <param name="sender">The Bluetooth interface that disconnected from the device</param>
    /// <param name="device">The disconnected Bluetooth device</param>
    /// <remarks>
    ///     This delegate is used to handle events when a connection to a Bluetooth device is intentionally terminated.
    /// </remarks>
    public delegate void DeviceDisconnectedHandler(IBluetoothLEInterface sender, IBluetoothLEDevice device);

    /// <summary>
    ///     Represents the method that handles device connection loss events.
    /// </summary>
    /// <param name="sender">The Bluetooth interface that lost connection to the device</param>
    /// <param name="device">The Bluetooth device that lost connection</param>
    /// <remarks>
    ///     This delegate is used to handle events when a connection to a Bluetooth device is unexpectedly lost,
    ///     such as when the device moves out of range or is powered off.
    /// </remarks>
    public delegate void DeviceConnectionLostHandler(IBluetoothLEInterface sender, IBluetoothLEDevice device);

    /// <summary>
    ///     Represents the method that handles device configuration completion events.
    /// </summary>
    /// <param name="device">The Bluetooth device that has been configured</param>
    /// <remarks>
    ///     This delegate is used to handle events when a Bluetooth device has completed configuration.
    ///     It can be used to perform any additional setup or initialization required after configuration.
    /// </remarks>
    public delegate void DeviceConfiguredHandler(IBluetoothLEDevice device);
}