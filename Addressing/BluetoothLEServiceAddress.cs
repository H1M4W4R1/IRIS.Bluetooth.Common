using IRIS.Bluetooth.Common.Abstract;

namespace IRIS.Bluetooth.Common.Addressing
{
    /// <summary>
    /// Represents a Bluetooth LE service address
    /// Can also match device name using regular expression
    /// </summary>
    public readonly struct BluetoothLEServiceAddress(string serviceUUID) : IBluetoothLEAddress
    {
        /// <summary>
        /// UUID of the service
        /// </summary>
        public string ServiceUUID { get; init; } = serviceUUID;

        /// <summary>
        /// Check if the device is valid for this service
        /// </summary>
        public bool IsDeviceValid(IBluetoothLEDevice device)
        {
            // Convert UUID to lowercase and remove dashes
            string serviceUUID = ServiceUUID.ToLowerInvariant().Replace("-", "");

            // Compare with device services
            return device.Services.Any(service => service.UUID.ToLowerInvariant().Replace("-", "") == serviceUUID);
        }

        public override string ToString() => $"{ServiceUUID}";
    }
}