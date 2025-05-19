using System.Runtime.CompilerServices;
using IRIS.Bluetooth.Common.Abstract;

namespace IRIS.Bluetooth.Common.Utility
{
    /// <summary>
    ///     Provides an awaitable implementation for receiving notifications from a Bluetooth Low Energy characteristic.
    /// </summary>
    /// <remarks>
    ///     This class implements the INotifyCompletion interface to support the await pattern.
    ///     It allows asynchronous code to wait for and process notifications from a Bluetooth Low Energy characteristic.
    ///     The awaiter can be cancelled using a CancellationToken.
    /// </remarks>
    public sealed class BluetoothNotificationAwaiter(IBluetoothLECharacteristic characteristic,
        CancellationToken cancellationToken = default
    )
        : INotifyCompletion
    {
        /// <summary>
        ///     Indicates whether this awaiter has already handled the notification.
        /// </summary>
        private bool _handled;

        /// <summary>
        ///     The continuation action to be executed when the notification is received.
        /// </summary>
        private Action _continuation = null!;

        /// <summary>
        ///     The notification data received from the Bluetooth Low Energy characteristic.
        /// </summary>
        private byte[] _result = [];

        /// <summary>
        ///     Sets up the continuation and subscribes to the characteristic's ValueChanged event.
        /// </summary>
        /// <param name="continuation">The action to execute when the notification is received</param>
        /// <remarks>
        ///     This method:
        ///     1. Stores the continuation for later execution
        ///     2. Subscribes to the characteristic's ValueChanged event
        /// </remarks>
        public void OnCompleted(Action continuation)
        {
            _continuation = continuation;
            characteristic.ValueChanged += OnNotificationReceived;
        }

        /// <summary>
        ///     Gets the result of the notification operation and cleans up the event subscription.
        /// </summary>
        /// <returns>The notification data received from the characteristic</returns>
        /// <remarks>
        ///     This method unsubscribes from the ValueChanged event before returning the result.
        /// </remarks>
        public byte[] GetResult()
        {
            characteristic.ValueChanged -= OnNotificationReceived;
            return _result;
        }

        /// <summary>
        ///     Gets a value indicating whether the awaiter has completed.
        /// </summary>
        /// <remarks>
        ///     Returns true if either the notification has been handled or the cancellation token has been cancelled.
        /// </remarks>
        public bool IsCompleted => _handled || cancellationToken.IsCancellationRequested;

        /// <summary>
        ///     Handles the ValueChanged event from the Bluetooth Low Energy characteristic.
        /// </summary>
        /// <param name="bluetoothLECharacteristic">The characteristic that triggered the event</param>
        /// <param name="newValue">The new value received from the characteristic</param>
        /// <remarks>
        ///     This method:
        ///     1. Skips if already handled
        ///     2. Unsubscribes from the event
        ///     3. Stores the received value
        ///     4. Marks the state as handled
        ///     5. Invokes the continuation
        /// </remarks>
        private void OnNotificationReceived(IBluetoothLECharacteristic bluetoothLECharacteristic, byte[] newValue)
        {
            if (_handled) return;

            bluetoothLECharacteristic.ValueChanged -= OnNotificationReceived;

            _result = newValue;
            _handled = true;
            _continuation.Invoke();
        }
    }
}