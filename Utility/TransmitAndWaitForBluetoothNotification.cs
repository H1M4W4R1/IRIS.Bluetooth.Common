using IRIS.Bluetooth.Common.Abstract;

namespace IRIS.Bluetooth.Common.Utility
{
    /// <summary>
    ///     Provides an awaitable implementation for transmitting data and waiting for a response from a Bluetooth Low Energy
    ///     device.
    /// </summary>
    /// <remarks>
    ///     This struct wraps the BluetoothExchangeAwaiter to provide a clean awaitable interface
    ///     for exchanging data with a Bluetooth Low Energy device. It allows asynchronous code to:
    ///     1. Transmit data through a TX characteristic
    ///     2. Wait for and process the response from an RX characteristic
    ///     The operation can be cancelled using a CancellationToken.
    /// </remarks>
    public readonly struct TransmitAndWaitForBluetoothNotification(IBluetoothLECharacteristic txCharacteristic,
        IBluetoothLECharacteristic rxCharacteristic,
        byte[] dataToTransmit,
        CancellationToken cancellationToken = default,
        Predicate<byte[]>? validateReceivedData = null
    )
    {
        /// <summary>
        ///     Gets an awaiter for the data exchange operation.
        /// </summary>
        /// <returns>A BluetoothExchangeAwaiter that can be used to await the data exchange</returns>
        /// <remarks>
        ///     This method creates and returns a new BluetoothExchangeAwaiter instance that will
        ///     handle the asynchronous data exchange process.
        /// </remarks>
        public BluetoothExchangeAwaiter GetAwaiter()
            => new(txCharacteristic, rxCharacteristic, dataToTransmit, cancellationToken, validateReceivedData);
    }
}