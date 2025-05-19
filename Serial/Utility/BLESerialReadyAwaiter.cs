using System.Runtime.CompilerServices;

namespace IRIS.Bluetooth.Common.Serial.Utility
{
    /// <summary>
    ///     Provides an awaitable implementation for waiting until a Bluetooth Low Energy serial connection is ready.
    /// </summary>
    /// <remarks>
    ///     This class implements the INotifyCompletion interface to support the await pattern.
    ///     It allows asynchronous code to wait for a BLESerial instance to become ready before proceeding.
    ///     The awaiter can be cancelled using a CancellationToken.
    /// </remarks>
    public sealed class BLESerialReadyAwaiter(BLESerial serial, CancellationToken cancellationToken = default)
        : INotifyCompletion
    {
        /// <summary>
        ///     The continuation action to be executed when the serial connection becomes ready.
        /// </summary>
        private Action _continuation = null!;

        /// <summary>
        ///     Indicates whether this awaiter has already handled the ready state.
        /// </summary>
        private bool _handled;

        /// <summary>
        ///     Gets a value indicating whether the awaiter has completed.
        /// </summary>
        /// <remarks>
        ///     Returns true if either the ready state has been handled or the cancellation token has been cancelled.
        /// </remarks>
        public bool IsCompleted => _handled || cancellationToken.IsCancellationRequested;

        /// <summary>
        ///     Cleans up the event subscription when the await operation completes.
        /// </summary>
        public void GetResult()
        {
            serial.SerialReady -= OnSerialReady;
        }

        /// <summary>
        ///     Handles the SerialReady event from the BLESerial instance.
        /// </summary>
        /// <remarks>
        ///     This method is called when the serial connection becomes ready. It:
        ///     1. Skips if already handled
        ///     2. Skips if serial is not ready anymore
        ///     3. Marks the serial as not ready (consumed)
        ///     4. Invokes the continuation and cleans up
        /// </remarks>
        private void OnSerialReady()
        {
            // Skip if already handled
            if (_handled) return;

            // Skip if serial is not ready anymore
            if (!serial.IsReady) return;

            // Serial won't be ready after this operation
            // as it was consumed
            serial.IsReady = false;

            // Handle this awaiter and continue
            _handled = true;
            _continuation.Invoke();

            serial.SerialReady -= OnSerialReady;
        }

        /// <summary>
        ///     Sets up the continuation to be executed when the serial connection becomes ready.
        /// </summary>
        /// <param name="continuation">The action to execute when the serial connection becomes ready</param>
        /// <remarks>
        ///     If the serial is already ready, executes the continuation immediately.
        ///     Otherwise, subscribes to the SerialReady event and stores the continuation for later execution.
        /// </remarks>
        public void OnCompleted(Action continuation)
        {
            if (serial.IsReady)
            {
                // Serial won't be ready after this operation
                serial.IsReady = false;

                // Handle this awaiter and continue
                _handled = true;
                continuation();
                return;
            }

            _continuation = continuation;
            serial.SerialReady += OnSerialReady;
        }
    }
}