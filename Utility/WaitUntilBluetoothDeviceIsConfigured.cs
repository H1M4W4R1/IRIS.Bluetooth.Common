using IRIS.Bluetooth.Common.Abstract;

namespace IRIS.Bluetooth.Common.Utility
{
    public readonly struct WaitUntilBluetoothDeviceIsConfigured(IBluetoothLEDevice device, CancellationToken cancellationToken)
    {
        public BluetoothDeviceConfiguredAwaiter GetAwaiter() => new(device, cancellationToken);
    }
}