namespace IRIS.Bluetooth.Common.Serial.Utility
{
    /// <summary>
    ///     Provides a way to await a Bluetooth Low Energy serial connection becoming ready.
    /// </summary>
    /// <remarks>
    ///     This struct enables the use of the await keyword to wait for a BLESerial instance to become ready.
    ///     It wraps the BLESerialReadyAwaiter to provide a more convenient syntax for awaiting serial readiness.
    /// </remarks>
    public readonly struct WaitForSerialReady(BLESerial serial, CancellationToken cancellationToken)
    {
        /// <summary>
        ///     Gets an awaiter that can be used to await the serial connection becoming ready.
        /// </summary>
        /// <returns>A BLESerialReadyAwaiter instance that can be used with the await keyword</returns>
        /// <remarks>
        ///     This method is called by the compiler when using the await keyword on a WaitForSerialReady instance.
        ///     It creates a new BLESerialReadyAwaiter that will handle the actual waiting logic.
        /// </remarks>
        public BLESerialReadyAwaiter GetAwaiter() => new(serial, cancellationToken);
    }
}