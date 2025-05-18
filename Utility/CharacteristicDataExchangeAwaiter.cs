using System.Diagnostics;
using System.Runtime.CompilerServices;
using IRIS.Bluetooth.Common.Abstract;

namespace IRIS.Bluetooth.Common.Utility
{
    public sealed class CharacteristicDataExchangeAwaiter(
        byte[] dataToTransmit,
        IBluetoothLECharacteristic txCharacteristic,
        IBluetoothLECharacteristic rxCharacteristic,
        Predicate<byte[]>? notificationCriteria = null,
        CancellationToken cancellationToken = default
    ) : INotifyCompletion
    {
        private byte[]? _receivedData;
        public bool IsCompleted { get; private set; }
        private Action _continuation = () => { };

        public byte[]? GetResult() => _receivedData;

        public async void OnCompleted(Action continuation)
        {
#if DEBUG
            Debug.Assert(characteristic.IsNotify);
#endif

            _continuation = continuation;

            // Ensure characteristic is subscribed
            await rxCharacteristic.SubscribeAsync(cancellationToken);
            rxCharacteristic.ValueChanged += OnCharacteristicValueChanged;

            // Transmit data
            await txCharacteristic.WriteAsync(dataToTransmit, cancellationToken);
        }

        private void OnCharacteristicValueChanged(
            IBluetoothLECharacteristic bluetoothLECharacteristic,
            byte[] newValue)
        {
            // Check if notification criteria is valid
            if (notificationCriteria != null && !notificationCriteria(newValue)) return;

            // Unsubscribe
            rxCharacteristic.ValueChanged -= OnCharacteristicValueChanged;

            // Handle data
            _receivedData = newValue;
            IsCompleted = true;
            _continuation();
        }
    }
}