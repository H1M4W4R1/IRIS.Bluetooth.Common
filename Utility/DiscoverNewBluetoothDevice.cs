using IRIS.Bluetooth.Common.Abstract;

namespace IRIS.Bluetooth.Common.Utility
{
    /// <summary>
    /// Helper struct to discover new bluetooth device
    /// </summary>
    public readonly struct DiscoverNewBluetoothDevice(IBluetoothLEInterface bluetoothLEInterface,
        CancellationToken cancellationToken)
    {
        public BluetoothDeviceDiscoveryAwaiter GetAwaiter() => new(bluetoothLEInterface, cancellationToken);
        
    }
}