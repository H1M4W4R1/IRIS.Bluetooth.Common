using IRIS.Bluetooth.Common.Abstract;

namespace IRIS.Bluetooth.Common.Utility
{
    /// <summary>
    ///     Provides an awaitable implementation for discovering new Bluetooth Low Energy devices.
    /// </summary>
    /// <remarks>
    ///     This struct wraps the BluetoothDeviceDiscoveryAwaiter to provide a clean awaitable interface
    ///     for discovering new Bluetooth Low Energy devices. It allows asynchronous code to wait for
    ///     device discovery events. The discovery operation can be cancelled using a CancellationToken.
    /// </remarks>
    public readonly struct DiscoverNewBluetoothDevice(IBluetoothLEInterface bluetoothLEInterface,
        CancellationToken cancellationToken)
    {
        /// <summary>
        ///     Gets an awaiter for the device discovery operation.
        /// </summary>
        /// <returns>A BluetoothDeviceDiscoveryAwaiter that can be used to await device discovery</returns>
        /// <remarks>
        ///     This method creates and returns a new BluetoothDeviceDiscoveryAwaiter instance that will
        ///     handle the asynchronous device discovery process.
        /// </remarks>
        public BluetoothDeviceDiscoveryAwaiter GetAwaiter() => new(bluetoothLEInterface, cancellationToken);
    }
}