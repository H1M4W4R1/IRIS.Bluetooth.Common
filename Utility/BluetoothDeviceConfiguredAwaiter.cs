using System.Runtime.CompilerServices;
using IRIS.Bluetooth.Common.Abstract;

namespace IRIS.Bluetooth.Common.Utility
{
    /// <summary>
    ///     Provides an awaitable implementation for waiting until a Bluetooth Low Energy device is configured.
    /// </summary>
    /// <remarks>
    ///     This class implements the INotifyCompletion interface to support the await pattern.
    ///     It allows asynchronous code to wait for a Bluetooth Low Energy device to complete its configuration
    ///     before proceeding. The awaiter can be cancelled using a CancellationToken.
    /// </remarks>
    public sealed class BluetoothDeviceConfiguredAwaiter(IBluetoothLEDevice device,
        CancellationToken cancellationToken = default)
        : INotifyCompletion
    {
        /// <summary>
        ///     The continuation action to be executed when the device becomes configured.
        /// </summary>
        private Action _continuation = null!;

        /// <summary>
        ///     Indicates whether this awaiter has already handled the configured state.
        /// </summary>
        private bool _handled = device.IsConfigured;

        /// <summary>
        ///     Gets a value indicating whether the awaiter has completed.
        /// </summary>
        /// <remarks>
        ///     Returns true if either the configured state has been handled or the cancellation token has been cancelled.
        /// </remarks>
        public bool IsCompleted => _handled || cancellationToken.IsCancellationRequested;

        /// <summary>
        ///     Cleans up the event subscription when the await operation completes.
        /// </summary>
        public void GetResult()
        {
            device.DeviceConfigured -= OnDeviceConfigured;
        }

        /// <summary>
        ///     Sets up the continuation to be executed when the device becomes configured.
        /// </summary>
        /// <param name="continuation">The action to execute when the device becomes configured</param>
        /// <remarks>
        ///     Subscribes to the DeviceConfigured event and stores the continuation for later execution.
        /// </remarks>
        public void OnCompleted(Action continuation)
        {
            _continuation = continuation;
            device.DeviceConfigured += OnDeviceConfigured;
        }

        /// <summary>
        ///     Handles the DeviceConfigured event from the Bluetooth Low Energy device.
        /// </summary>
        /// <param name="bluetoothLEDevice">The device that triggered the event</param>
        /// <remarks>
        ///     This method is called when the device becomes configured. It:
        ///     1. Skips if already handled
        ///     2. Marks the state as handled
        ///     3. Unsubscribes from the event
        ///     4. Invokes the continuation
        /// </remarks>
        private void OnDeviceConfigured(IBluetoothLEDevice bluetoothLEDevice)
        {
            if (_handled) return;
            _handled = true;

            device.DeviceConfigured -= OnDeviceConfigured;
            _continuation?.Invoke();
        }
    }
}