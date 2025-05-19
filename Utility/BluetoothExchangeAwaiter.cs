using System.Runtime.CompilerServices;
using IRIS.Bluetooth.Common.Abstract;

namespace IRIS.Bluetooth.Common.Utility
{
    /// <summary>
    ///     Provides an awaitable implementation for exchanging data with a Bluetooth Low Energy device.
    /// </summary>
    /// <remarks>
    ///     This class implements the INotifyCompletion interface to support the await pattern.
    ///     It handles the asynchronous exchange of data with a Bluetooth Low Energy device by:
    ///     1. Transmitting data through a TX characteristic
    ///     2. Waiting for and processing the response from an RX characteristic
    ///     The awaiter can be cancelled using a CancellationToken.
    /// </remarks>
    public sealed class BluetoothExchangeAwaiter(
        IBluetoothLECharacteristic txCharacteristic,
        IBluetoothLECharacteristic rxCharacteristic,
        byte[] dataToTransmit,
        CancellationToken cancellationToken = default) 
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
        ///     The response data received from the Bluetooth Low Energy device.
        /// </summary>
        private byte[] _result = [];
        
        /// <summary>
        ///     Sets up the continuation and initiates the data exchange.
        /// </summary>
        /// <param name="continuation">The action to execute when the notification is received</param>
        /// <remarks>
        ///     This method:
        ///     1. Stores the continuation for later execution
        ///     2. Subscribes to the RX characteristic's ValueChanged event
        ///     3. Transmits the data through the TX characteristic
        /// </remarks>
        public async void OnCompleted(Action continuation)
        {
            _continuation = continuation;
            rxCharacteristic.ValueChanged += OnNotificationReceived;

            // Transmit data
            await txCharacteristic.WriteAsync(dataToTransmit);
        }

        /// <summary>
        ///     Gets the result of the data exchange operation.
        /// </summary>
        /// <returns>The response data received from the device</returns>
        /// <remarks>
        ///     This method cleans up by unsubscribing from the ValueChanged event before returning the result.
        /// </remarks>
        public byte[] GetResult()
        {
            rxCharacteristic.ValueChanged -= OnNotificationReceived;
            return _result;
        }
        
        /// <summary>
        ///     Gets a value indicating whether the data exchange operation is completed.
        /// </summary>
        /// <remarks>
        ///     Returns true if either the notification has been handled or the cancellation token has been cancelled.
        /// </remarks>
        public bool IsCompleted => _handled || cancellationToken.IsCancellationRequested;
        
        /// <summary>
        ///     Handles the ValueChanged event from the RX characteristic.
        /// </summary>
        /// <param name="bluetoothLECharacteristic">The characteristic that triggered the event</param>
        /// <param name="newValue">The new value received from the device</param>
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