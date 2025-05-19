using System.Runtime.CompilerServices;
using IRIS.Bluetooth.Common.Abstract;

namespace IRIS.Bluetooth.Common.Utility
{
    /// <summary>
    /// Awaiter class for Bluetooth device discovery operations that implements the INotifyCompletion interface.
    /// This class allows for asynchronous waiting for Bluetooth device discovery events.
    /// </summary>
    public sealed class BluetoothDeviceDiscoveryAwaiter(
        IBluetoothLEInterface bluetoothInterface,
        CancellationToken cancellationToken = default
    ) : INotifyCompletion
    {
        /// <summary>
        /// The continuation action to be executed when a device is discovered.
        /// </summary>
        private Action _continuation = null!;

        /// <summary>
        /// The discovered Bluetooth device, if any.
        /// </summary>
        private IBluetoothLEDevice? _device;

        /// <summary>
        /// Flag indicating whether a device discovery has been handled.
        /// </summary>
        private bool _handled;

        /// <summary>
        /// Gets a value indicating whether the device discovery operation is completed.
        /// Returns true if a device has been discovered or if the operation has been cancelled.
        /// </summary>
        public bool IsCompleted => _device != null || cancellationToken.IsCancellationRequested;

        /// <summary>
        /// Gets the result of the device discovery operation.
        /// If the operation was cancelled, unsubscribes from the device discovery event and returns null.
        /// </summary>
        /// <returns>The discovered Bluetooth device, or null if the operation was cancelled.</returns>
        public IBluetoothLEDevice? GetResult()
        {
            if (!cancellationToken.IsCancellationRequested) return _device;
            
            _device = null;
            bluetoothInterface.OnBluetoothDeviceDiscovered -= OnDeviceDiscovered;
            return null;
        }

        /// <summary>
        /// Sets up the continuation action and subscribes to the device discovery event.
        /// </summary>
        /// <param name="continuation">The action to be executed when a device is discovered.</param>
        public void OnCompleted(Action continuation)
        {
            _continuation = continuation;
            bluetoothInterface.OnBluetoothDeviceDiscovered += OnDeviceDiscovered;
        }

        /// <summary>
        /// Handles the device discovery event.
        /// Sets the discovered device, unsubscribes from the event, and invokes the continuation action.
        /// </summary>
        /// <param name="sender">The sender of the device discovery event.</param>
        /// <param name="device">The discovered Bluetooth device.</param>
        private void OnDeviceDiscovered(IBluetoothLEInterface sender, IBluetoothLEDevice device)
        {
            if (_handled) return;
            _handled = true;
            
            _device = device;
            bluetoothInterface.OnBluetoothDeviceDiscovered -= OnDeviceDiscovered;
            _continuation.Invoke();
        }
    }
}