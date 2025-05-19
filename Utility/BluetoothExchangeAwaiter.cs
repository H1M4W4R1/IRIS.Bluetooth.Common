using System.Runtime.CompilerServices;
using IRIS.Bluetooth.Common.Abstract;

namespace IRIS.Bluetooth.Common.Utility
{
    public sealed class BluetoothExchangeAwaiter(
        IBluetoothLECharacteristic txCharacteristic,
        IBluetoothLECharacteristic rxCharacteristic,
        byte[] dataToTransmit,
        CancellationToken cancellationToken = default) 
        : INotifyCompletion
    {
        private bool _handled;
        private Action _continuation = null!;
        private byte[] _result = [];
        
        public async void OnCompleted(Action continuation)
        {
            _continuation = continuation;
            rxCharacteristic.ValueChanged += OnNotificationReceived;

            // Transmit data
            await txCharacteristic.WriteAsync(dataToTransmit);
        }

        public byte[] GetResult()
        {
            rxCharacteristic.ValueChanged -= OnNotificationReceived;
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