using IRIS.Bluetooth.Common.Abstract;

namespace IRIS.Bluetooth.Common.Addressing
{
    /// <summary>
    ///     Represents a Bluetooth Low Energy device address that identifies devices by the presence of a specific service
    ///     UUID.
    /// </summary>
    /// <remarks>
    ///     This address type is used when you need to discover devices that provide a specific service.
    ///     The service UUID is used to match against the services provided by discovered devices.
    ///     UUIDs are compared case-insensitively and without dashes for consistent matching.
    /// </remarks>
    public readonly struct BluetoothLEServiceAddress(string serviceUUID) : IBluetoothLEAddress
    {
        /// <summary>
        ///     Gets the UUID of the service to match against.
        /// </summary>
        /// <remarks>
        ///     This UUID is used to identify the specific service that must be present on the device.
        ///     The UUID can be in any standard format (with or without dashes) as it will be normalized
        ///     during comparison.
        /// </remarks>
        public string ServiceUUID { get; init; } = serviceUUID;

        /// <summary>
        ///     Determines if a discovered device matches this address by checking if it provides the specified service.
        /// </summary>
        /// <param name="device">The device to validate</param>
        /// <returns>True if the device provides the specified service, false otherwise</returns>
        /// <remarks>
        ///     The method normalizes both the target service UUID and the device's service UUIDs by:
        ///     1. Converting to lowercase
        ///     2. Removing dashes
        ///     This ensures consistent matching regardless of the UUID format.
        /// </remarks>
        public bool IsDeviceValid(IBluetoothLEDevice device)
        {
            // Convert UUID to lowercase and remove dashes
            string serviceUUID = ServiceUUID.ToLowerInvariant().Replace("-", "");

            // Compare with device services
            return device.Services.Any(service => service.UUID.ToLowerInvariant().Replace("-", "") == serviceUUID);
        }

        /// <summary>
        ///     Returns a string representation of the service UUID used for device matching.
        /// </summary>
        /// <returns>A string containing the service UUID</returns>
        public override string ToString() => $"{ServiceUUID}";
    }
}