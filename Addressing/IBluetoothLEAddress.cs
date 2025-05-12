using IRIS.Bluetooth.Common.Abstract;

namespace IRIS.Bluetooth.Common.Addressing
{
    /// <summary>
    ///     Represents a Bluetooth Low Energy device address that can be used to identify and validate devices.
    /// </summary>
    /// <remarks>
    ///     This interface provides a way to define different types of addresses (e.g., by name, by UUID, by MAC address)
    ///     and validate if a discovered device matches the address criteria.
    /// </remarks>
    public interface IBluetoothLEAddress
    {
        /// <summary>
        ///     Determines if a discovered device matches this address criteria.
        /// </summary>
        /// <param name="device">The device to validate</param>
        /// <returns>True if the device matches this address criteria, false otherwise</returns>
        /// <remarks>
        ///     Different implementations of this interface may use different criteria to validate devices,
        ///     such as matching device names, service UUIDs, or MAC addresses.
        /// </remarks>
        public bool IsDeviceValid(IBluetoothLEDevice device);
    }
}