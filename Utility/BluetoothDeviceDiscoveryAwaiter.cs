using System.Runtime.CompilerServices;
using IRIS.Bluetooth.Common.Abstract;

namespace IRIS.Bluetooth.Common.Utility
{
    public sealed class BluetoothDeviceDiscoveryAwaiter(
        IBluetoothLEInterface bluetoothInterface,
        CancellationToken cancellationToken
    ) : INotifyCompletion
    {
        private Action _continuation = null!;
        private IBluetoothLEDevice? _device;
        private bool _handled;

        public bool IsCompleted => _device != null || cancellationToken.IsCancellationRequested;

        public IBluetoothLEDevice? GetResult()
        {
            if (!cancellationToken.IsCancellationRequested) return _device;
            
            _device = null;
            bluetoothInterface.OnBluetoothDeviceDiscovered -= OnDeviceDiscovered;
            return null;
        }

        public void OnCompleted(Action continuation)
        {
            _continuation = continuation;
            bluetoothInterface.OnBluetoothDeviceDiscovered += OnDeviceDiscovered;
        }

        private void OnDeviceDiscovered(IBluetoothLEInterface sender, IBluetoothLEDevice device)
        {
            if (_handled) return;
            _handled = true;
            
            _device = device;
            bluetoothInterface.OnBluetoothDeviceDiscovered -= OnDeviceDiscovered;
            _continuation?.Invoke();
        }
    }
}