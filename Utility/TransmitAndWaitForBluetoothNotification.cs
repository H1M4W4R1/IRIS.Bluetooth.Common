using IRIS.Bluetooth.Common.Abstract;

namespace IRIS.Bluetooth.Common.Utility
{
    public readonly struct TransmitAndWaitForBluetoothNotification(
        IBluetoothLECharacteristic txCharacteristic,
        IBluetoothLECharacteristic rxCharacteristic,
        byte[] dataToTransmit,
        CancellationToken cancellationToken = default
    )
    {
        public BluetoothExchangeAwaiter GetAwaiter()
            => new(txCharacteristic, rxCharacteristic, dataToTransmit, cancellationToken);
    }
}