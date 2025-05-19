using IRIS.Bluetooth.Common.Abstract;

namespace IRIS.Bluetooth.Common.Utility
{
    public readonly struct WaitForBluetoothNotification(IBluetoothLECharacteristic characteristic,
        CancellationToken cancellationToken = default)
    {
        public BluetoothNotificationAwaiter GetAwaiter() => new(characteristic, cancellationToken);
        
    }
}