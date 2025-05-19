using IRIS.Bluetooth.Common.Abstract;

namespace IRIS.Bluetooth.Common.Utility
{
    /// <summary>
    ///     Provides an awaitable implementation for waiting until a Bluetooth Low Energy device is configured.
    /// </summary>
    /// <remarks>
    ///     This struct wraps the BluetoothDeviceConfiguredAwaiter to provide a clean awaitable interface
    ///     for waiting until a Bluetooth Low Energy device is fully configured. It allows asynchronous code to wait for
    ///     the device to complete its configuration process. The operation can be cancelled using a CancellationToken.
    /// </remarks>
    public readonly struct WaitUntilBluetoothDeviceIsConfigured(IBluetoothLEDevice device,
        CancellationToken cancellationToken)
    {
        /// <summary>
        ///     Gets an awaiter for the device configuration operation.
        /// </summary>
        /// <returns>A BluetoothDeviceConfiguredAwaiter that can be used to await device configuration</returns>
        /// <remarks>
        ///     This method creates and returns a new BluetoothDeviceConfiguredAwaiter instance that will
        ///     handle the asynchronous device configuration process.
        /// </remarks>
        public BluetoothDeviceConfiguredAwaiter GetAwaiter() => new(device, cancellationToken);
    }
}