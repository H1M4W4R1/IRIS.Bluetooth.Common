using IRIS.Bluetooth.Common.Abstract;

namespace IRIS.Bluetooth.Common.Utility
{
    /// <summary>
    ///     Provides an awaitable implementation for waiting for notifications from a Bluetooth Low Energy characteristic.
    /// </summary>
    /// <remarks>
    ///     This struct wraps the BluetoothNotificationAwaiter to provide a clean awaitable interface
    ///     for receiving notifications from a Bluetooth Low Energy characteristic. It allows asynchronous code to wait for
    ///     and process notifications. The operation can be cancelled using a CancellationToken.
    /// </remarks>
    public readonly struct WaitForBluetoothNotification(IBluetoothLECharacteristic characteristic,
        CancellationToken cancellationToken = default)
    {
        /// <summary>
        ///     Gets an awaiter for the notification operation.
        /// </summary>
        /// <returns>A BluetoothNotificationAwaiter that can be used to await notifications</returns>
        /// <remarks>
        ///     This method creates and returns a new BluetoothNotificationAwaiter instance that will
        ///     handle the asynchronous notification process.
        /// </remarks>
        public BluetoothNotificationAwaiter GetAwaiter() => new(characteristic, cancellationToken);
    }
}