namespace IRIS.Bluetooth.Common.Serial.Utility
{
    /// <summary>
    ///     Represents a method that handles the event when a Bluetooth Low Energy serial connection becomes ready.
    /// </summary>
    /// <remarks>
    ///     This delegate is used to notify subscribers when a BLESerial instance has completed its initialization
    ///     and is ready for communication. The event is typically raised after all necessary services and
    ///     characteristics have been discovered and configured.
    /// </remarks>
    public delegate void SerialReadyHandler();
}