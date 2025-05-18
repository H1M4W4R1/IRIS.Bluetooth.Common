using System.Text;
using IRIS.Bluetooth.Common.Abstract;

namespace IRIS.Bluetooth.Common.Utility
{
    public readonly struct ExchangeBluetoothLEData(byte[] data,
        IBluetoothLECharacteristic txCharacteristic,
        IBluetoothLECharacteristic rxCharacteristic,
        Predicate<byte[]>? condition = null,
        CancellationToken cancellationToken = default
    )
    {
        public CharacteristicDataExchangeAwaiter GetAwaiter() => new(data,
            txCharacteristic,
            rxCharacteristic, condition, cancellationToken);

        public ExchangeBluetoothLEData(
            string command,
            IBluetoothLECharacteristic txCharacteristic,
            IBluetoothLECharacteristic rxCharacteristic,
            Predicate<byte[]>? condition = null,
            CancellationToken cancellationToken = default) : this(Encoding.UTF8.GetBytes(command),
            txCharacteristic, rxCharacteristic, condition, cancellationToken)
        {
            // Default
        }
    }
}