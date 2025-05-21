using IRIS.Bluetooth.Common.Data;

namespace IRIS.Bluetooth.Common.Abstract
{
    /// <summary>
    ///     Represents a Bluetooth Low Energy (BLE) characteristic, which is a data point that can be read, written, or
    ///     monitored for changes.
    /// </summary>
    public interface IBluetoothLECharacteristic
    {
        /// <summary>
        ///     Gets the device that owns this characteristic and its parent service.
        /// </summary>
        /// <remarks>
        ///     This is a convenience property that returns the device from the parent service.
        /// </remarks>
        public IBluetoothLEDevice Device => Service.Device;

        /// <summary>
        ///     Gets the service that owns this characteristic.
        /// </summary>
        /// <remarks>
        ///     Every characteristic must belong to a service in the BLE protocol.
        /// </remarks>
        public IBluetoothLEService Service { get; }

        /// <summary>
        ///     Gets the UUID of this characteristic in string format.
        /// </summary>
        /// <remarks>
        ///     The UUID uniquely identifies the type of characteristic and its behavior.
        /// </remarks>
        public string UUID { get; }

        /// <summary>
        ///     Gets a value indicating whether this characteristic can be read.
        /// </summary>
        public bool IsRead { get; }

        /// <summary>
        ///     Gets a value indicating whether this characteristic can be written to.
        /// </summary>
        public bool IsWrite { get; }

        /// <summary>
        ///     Gets a value indicating whether this characteristic supports notifications.
        /// </summary>
        /// <remarks>
        ///     When notifications are enabled, the device will automatically send updates when the characteristic value changes.
        /// </remarks>
        public bool IsNotify { get; }

        /// <summary>
        ///     Checks if this characteristic supports the specified combination of operations.
        /// </summary>
        /// <param name="flags">The combination of operations to check for</param>
        /// <returns>True if the characteristic supports all specified operations, false otherwise</returns>
        public bool IsValidForFlags(CharacteristicFlags flags)
        {
            if (flags.HasFlag(CharacteristicFlags.Read) && !IsRead) return false;
            if (flags.HasFlag(CharacteristicFlags.Write) && !IsWrite) return false;
            if (flags.HasFlag(CharacteristicFlags.Notify) && !IsNotify) return false;

            return true;
        }

        /// <summary>
        ///     Event raised when the characteristic value changes.
        /// </summary>
        /// <remarks>
        ///     This event is internal and should only be used by the implementation.
        /// </remarks>
        internal event CharacteristicValueChangedHandler ValueChanged;

        /// <summary>
        ///     Reads the current value of the characteristic.
        /// </summary>
        /// <param name="cancellationToken">Token to cancel the operation</param>
        /// <returns>The characteristic value as a byte array, or null if the read operation failed</returns>
        public ValueTask<byte[]?> ReadAsync(CancellationToken cancellationToken = default);

        /// <summary>
        ///     Writes a new value to the characteristic.
        /// </summary>
        /// <param name="data">The data to write to the characteristic</param>
        /// <param name="cancellationToken">Token to cancel the operation</param>
        /// <returns>True if the write operation was successful, false otherwise</returns>
        public ValueTask<bool> WriteAsync(byte[] data, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Subscribes to notifications for this characteristic.
        /// </summary>
        /// <param name="cancellationToken">Token to cancel the operation</param>
        /// <returns>True if the subscription was successful or already active, false otherwise</returns>
        /// <remarks>
        ///     After subscribing, the device will automatically send updates when the characteristic value changes.
        /// </remarks>
        public ValueTask<bool> SubscribeAsync(CancellationToken cancellationToken = default);

        /// <summary>
        ///     Unsubscribes from notifications for this characteristic.
        /// </summary>
        /// <param name="cancellationToken">Token to cancel the operation</param>
        /// <returns>True if the unsubscription was successful or already inactive, false otherwise</returns>
        public ValueTask<bool> UnsubscribeAsync(CancellationToken cancellationToken = default);
    }
}