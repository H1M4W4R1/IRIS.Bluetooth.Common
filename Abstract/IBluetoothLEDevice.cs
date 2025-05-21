namespace IRIS.Bluetooth.Common.Abstract
{
    /// <summary>
    ///     Represents a Bluetooth Low Energy (BLE) device and provides access to its services and characteristics.
    /// </summary>
    public interface IBluetoothLEDevice
    {
        /// <summary>
        ///     Gets the name of the Bluetooth device.
        /// </summary>
        /// <remarks>
        ///     The name may be empty if the device doesn't broadcast its name.
        /// </remarks>
        public string Name { get; }

        /// <summary>
        ///     Gets the unique device address (MAC address) of the Bluetooth device.
        /// </summary>
        /// <remarks>
        ///     This is a 48-bit unsigned number. The address is provided by the adapter as a string,
        ///     which should be converted to ulong by removing ':' characters.
        /// </remarks>
        public ulong DeviceAddress { get; }

        /// <summary>
        ///     Checks if the device is currently configured.
        /// </summary>
        public bool IsConfigured { get; }

        /// <summary>
        ///     Gets all services available on this device.
        /// </summary>
        /// <remarks>
        ///     The list is read-only and contains all discovered services on the device.
        /// </remarks>
        public IReadOnlyList<IBluetoothLEService> Services { get; }

        /// <summary>
        ///     Gets all characteristics that match the specified UUID pattern.
        /// </summary>
        /// <param name="characteristicUUIDRegex">Regular expression pattern to match characteristic UUIDs</param>
        /// <returns>A read-only list of matching characteristics</returns>
        public IReadOnlyList<IBluetoothLECharacteristic> GetAllCharacteristicsForUUID(
            string characteristicUUIDRegex);

        /// <summary>
        ///     Gets all characteristics from services that match the specified UUID pattern.
        /// </summary>
        /// <param name="serviceUUIDRegex">Regular expression pattern to match service UUIDs</param>
        /// <returns>A read-only list of characteristics from matching services</returns>
        public IReadOnlyList<IBluetoothLECharacteristic> GetAllCharacteristicsForServices(
            string serviceUUIDRegex);

        /// <summary>
        ///     Gets all characteristics available on this device.
        /// </summary>
        /// <returns>A read-only list of all characteristics across all services</returns>
        public IReadOnlyList<IBluetoothLECharacteristic> GetAllCharacteristics();

        /// <summary>
        ///     Gets all services that match the specified UUID pattern.
        /// </summary>
        /// <param name="serviceUUIDRegex">Regular expression pattern to match service UUIDs</param>
        /// <returns>A read-only list of matching services</returns>
        public IReadOnlyList<IBluetoothLEService> GetAllServicesForUUID(string serviceUUIDRegex);

        /// <summary>
        ///     Event raised when device is configured
        /// </summary>
        public event DeviceConfiguredHandler DeviceConfigured;
    }
}
