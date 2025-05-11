using IRIS.Bluetooth.Common.Data;

namespace IRIS.Bluetooth.Common.Abstract
{
    /// <summary>
    ///     Represents bluetooth characteristic.
    /// </summary>
    public interface IBluetoothLECharacteristic
    {
        /// <summary>
        ///     Device that owns this characteristic and parent service
        /// </summary>
        public IBluetoothLEDevice Device => Service.Device;
        
        /// <summary>
        /// Service that owns this characteristic
        /// </summary>
        public IBluetoothLEService Service { get; }
        
        /// <summary>
        ///     Service UUID (in string format)
        /// </summary>
        public string UUID { get; }
        
        /// <summary>
        ///     True if characteristic can be read
        /// </summary>
        public bool IsRead { get; }
        
        /// <summary>
        ///     True if characteristic can be written
        /// </summary>
        public bool IsWrite { get; }
        
        /// <summary>
        ///     True if characteristic can be notified
        /// </summary>
        public bool IsNotify { get; }
        
        /// <summary>
        ///     Check if this characteristic is valid for specified flags
        /// </summary>
        /// <param name="flags">Flags to check</param>
        /// <returns>True if flags matches, false otherwise</returns>
        public bool IsValidForFlags(CharacteristicFlags flags)
        {
            if (flags.HasFlag(CharacteristicFlags.Read) && !IsRead) return false;
            if (flags.HasFlag(CharacteristicFlags.Write) && !IsWrite) return false;
            if (flags.HasFlag(CharacteristicFlags.Notify) && !IsNotify) return false;

            return true;
        }
        
        /// <summary>
        ///     Raised when the characteristic value changes.
        /// </summary>
        internal event CharacteristicValueChanged ValueChanged;
        
        /// <summary>
        ///     Read the characteristic value.
        /// </summary>
        /// <param name="cancellationToken">Token to cancel the operation</param>
        /// <returns>Value of the characteristic or null if failed</returns>
        public ValueTask<byte[]?> ReadAsync(CancellationToken cancellationToken = default);
        
        /// <summary>
        ///     Write the characteristic value.
        /// </summary>
        /// <param name="data">Data to write</param>
        /// <param name="cancellationToken">Token to cancel the operation</param>
        /// <returns>True if write was successful, false otherwise</returns>
        public ValueTask<bool> WriteAsync(byte[] data, CancellationToken cancellationToken = default);
     
        /// <summary>
        ///     Subscribe to characteristic value changes.
        /// </summary>
        /// <param name="cancellationToken">Token to cancel the operation</param>
        /// <returns>True if subscription was successful (or already subscribed), false otherwise</returns>
        public ValueTask<bool> SubscribeAsync(CancellationToken cancellationToken = default);
        
        /// <summary>
        ///     Unsubscribe from characteristic value changes.
        /// </summary>
        /// <param name="cancellationToken">Token to cancel the operation</param>
        /// <returns>True if un-subscription was successful (or already unsubscribed), false otherwise</returns>
        public ValueTask<bool> UnsubscribeAsync(CancellationToken cancellationToken = default);
    }
}