using IRIS.Bluetooth.Common.Abstract;

namespace IRIS.Bluetooth.Common.Addressing
{
    /// <summary>
    ///     Represents a Bluetooth Low Energy device address that identifies a device by its unique MAC address.
    /// </summary>
    /// <remarks>
    ///     This address type is used when you need to connect to a specific device using its MAC address.
    ///     The MAC address is a unique 48-bit identifier assigned to each Bluetooth device.
    /// </remarks>
    public readonly struct BluetoothLEDeviceIdentifierAddress(ulong deviceAddress)
        : IBluetoothLEAddress
    {
        /// <summary>
        ///     Gets the MAC address of the device.
        /// </summary>
        /// <remarks>
        ///     This is a 48-bit unsigned number that uniquely identifies the Bluetooth device.
        /// </remarks>
        public ulong DeviceAddress { get; private init; } = deviceAddress;

        /// <summary>
        ///     Determines if a discovered device matches this address by comparing its MAC address.
        /// </summary>
        /// <param name="device">The device to validate</param>
        /// <returns>True if the device's MAC address matches this address, false otherwise</returns>
        public bool IsDeviceValid(IBluetoothLEDevice device) => device.DeviceAddress == DeviceAddress;

        /// <summary>
        ///     Returns a string representation of the device address in hexadecimal format.
        /// </summary>
        /// <returns>A string containing the device address in hexadecimal format</returns>
        public override string ToString() => $"{DeviceAddress:X}";
    }
}