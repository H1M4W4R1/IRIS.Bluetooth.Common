using System.Runtime.CompilerServices;
using IRIS.Bluetooth.Common.Abstract;

namespace IRIS.Bluetooth.Common.Utility
{
    public sealed class BluetoothNotificationAwaiter(IBluetoothLECharacteristic characteristic,
        CancellationToken cancellationToken = default
    )
        : INotifyCompletion
    {
        private bool _handled;
        private Action _continuation = null!;
        private byte[] _result = [];

        public void OnCompleted(Action continuation)
        {
            _continuation = continuation;
            characteristic.ValueChanged += OnNotificationReceived;
        }

        public byte[] GetResult()
        {
            characteristic.ValueChanged -= OnNotificationReceived;
            return _result;
        }

        public bool IsCompleted => _handled || cancellationToken.IsCancellationRequested;

        private void OnNotificationReceived(IBluetoothLECharacteristic bluetoothLECharacteristic, byte[] newValue)
        {
            if (_handled) return;

            bluetoothLECharacteristic.ValueChanged -= OnNotificationReceived;

            _result = newValue;
            _handled = true;
            _continuation?.Invoke();
        }
    }
}