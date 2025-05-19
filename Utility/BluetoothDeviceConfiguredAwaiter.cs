using System.Runtime.CompilerServices;
using IRIS.Bluetooth.Common.Abstract;

namespace IRIS.Bluetooth.Common.Utility
{
    public sealed class BluetoothDeviceConfiguredAwaiter(IBluetoothLEDevice device,
        CancellationToken cancellationToken = default)
        : INotifyCompletion
    {
        private Action _continuation = null!;
        private bool _handled = device.IsConfigured;

        public bool IsCompleted => _handled || cancellationToken.IsCancellationRequested;

        public void GetResult()
        {
            device.DeviceConfigured -= OnDeviceConfigured;
        }

        public void OnCompleted(Action continuation)
        {
            _continuation = continuation;
            device.DeviceConfigured += OnDeviceConfigured;
        }

        private void OnDeviceConfigured(IBluetoothLEDevice bluetoothLEDevice)
        {
            if (_handled) return;
            _handled = true;

            device.DeviceConfigured -= OnDeviceConfigured;
            _continuation?.Invoke();
        }
    }
}