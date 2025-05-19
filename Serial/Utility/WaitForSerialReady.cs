namespace IRIS.Bluetooth.Common.Serial.Utility
{
    public readonly struct WaitForSerialReady(BLESerial serial, CancellationToken cancellationToken)
    {
        public BLESerialReadyAwaiter GetAwaiter() => new(serial, cancellationToken);
    }
}