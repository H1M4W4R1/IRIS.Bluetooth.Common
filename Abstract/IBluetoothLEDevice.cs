namespace IRIS.Bluetooth.Common.Abstract
{
    /// <summary>
    ///     Represents a Bluetooth device
    /// </summary>
    public interface IBluetoothLEDevice
    {
        /// <summary>
        ///     Name of the device
        /// </summary>
        public string Name { get; }
        
        /// <summary>
        /// NOTE: it might be provided by adapter as string, this is 48-bit unsigned number, remove ':' and convert to ulong
        ///       should be fine
        /// </summary>
        public ulong DeviceAddress { get; }
        
        /// <summary>
        ///     All services available on this device
        /// </summary>
        public IReadOnlyList<IBluetoothLEService> Services { get; }
        
        public IReadOnlyList<IBluetoothLECharacteristic> GetAllCharacteristicsForUUID(
            string characteristicUUIDRegex);

        public IReadOnlyList<IBluetoothLECharacteristic> GetAllCharacteristicsForServices(
            string serviceUUIDRegex);
        
        public IReadOnlyList<IBluetoothLECharacteristic> GetAllCharacteristics();
        
        public IReadOnlyList<IBluetoothLEService> GetAllServicesForUUID(string serviceUUIDRegex);
    }
}
