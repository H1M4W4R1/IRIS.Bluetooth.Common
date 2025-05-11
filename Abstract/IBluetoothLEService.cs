namespace IRIS.Bluetooth.Common.Abstract
{
    /// <summary>
    ///     Represents a BluetoothLE service
    /// </summary>
    public interface IBluetoothLEService
    {
        /// <summary>
        ///     Device that this service is associated with
        /// </summary>
        public IBluetoothLEDevice Device { get; }
        
        /// <summary>
        ///     UUID of the service
        /// </summary>
        public string UUID { get; }
        
        /// <summary>
        ///     All characteristics that belong to this service
        /// </summary>
        public IReadOnlyList<IBluetoothLECharacteristic> Characteristics { get; }
        
        public IReadOnlyList<IBluetoothLECharacteristic> GetAllCharacteristicsForUUID(
            string characteristicUUIDRegex);
    }
}
