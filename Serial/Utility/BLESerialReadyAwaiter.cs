using System.Runtime.CompilerServices;

namespace IRIS.Bluetooth.Common.Serial.Utility
{
    public sealed class BLESerialReadyAwaiter(BLESerial serial, CancellationToken cancellationToken = default)
        : INotifyCompletion
    {
        private Action _continuation = null!;
        private bool _handled;

        public bool IsCompleted => _handled || cancellationToken.IsCancellationRequested;

        public void GetResult()
        {
            serial.SerialReady -= OnSerialReady;
        }

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
            _continuation?.Invoke();

            serial.SerialReady -= OnSerialReady;
        }

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