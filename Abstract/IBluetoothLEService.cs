using IRIS.Operations.Abstract;

namespace IRIS.Bluetooth.Common.Abstract
{
    /// <summary>
    ///     Represents a Bluetooth Low Energy (BLE) service, which is a collection of related characteristics.
    /// </summary>
    /// <remarks>
    ///     Services in BLE are used to group related functionality together. Each service has a unique UUID
    ///     and contains one or more characteristics that provide specific functionality.
    /// </remarks>
    public interface IBluetoothLEService
    {
        /// <summary>
        ///     Gets the device that this service is associated with.
        /// </summary>
        /// <remarks>
        ///     Every service must belong to a device in the BLE protocol.
        /// </remarks>
        public IBluetoothLEDevice Device { get; }

        /// <summary>
        ///     Gets the UUID of the service.
        /// </summary>
        /// <remarks>
        ///     The UUID uniquely identifies the type of service and its functionality.
        ///     Standard BLE services have predefined UUIDs, while custom services use vendor-specific UUIDs.
        /// </remarks>
        public string UUID { get; }

        /// <summary>
        ///     Gets all characteristics that belong to this service.
        /// </summary>
        /// <remarks>
        ///     The list is read-only and contains all discovered characteristics for this service.
        /// </remarks>
        public IReadOnlyList<IBluetoothLECharacteristic> Characteristics { get; }

        /// <summary>
        ///     Gets all characteristics that match the specified UUID pattern.
        /// </summary>
        /// <param name="characteristicUUIDRegex">Regular expression pattern to match characteristic UUIDs</param>
        /// <remarks>
        ///     This method allows filtering characteristics by their UUID using a regular expression pattern.
        /// </remarks>
        public IDeviceOperationResult GetAllCharacteristicsForUUID(string characteristicUUIDRegex);
    }
}
