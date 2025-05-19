using System.Text;
using IRIS.Bluetooth.Common.Abstract;
using IRIS.Bluetooth.Common.Serial.Utility;
using IRIS.Bluetooth.Common.Utility;

namespace IRIS.Bluetooth.Common.Serial
{
    /// <summary>
    ///     Provides a serial communication interface for Bluetooth Low Energy devices.
    /// </summary>
    /// <remarks>
    ///     This class implements a serial-like communication protocol over Bluetooth Low Energy,
    ///     using a pair of characteristics for transmission (TX) and reception (RX).
    ///     It ensures synchronized communication by preventing multiple simultaneous transmissions
    ///     and handling the asynchronous nature of BLE communication.
    /// </remarks>
    public sealed class BLESerial(
        IBluetoothLECharacteristic txCharacteristic,
        IBluetoothLECharacteristic rxCharacteristic
    )
    {
        /// <summary>
        ///     Gets or sets a value indicating whether the serial connection is ready for the next operation.
        /// </summary>
        /// <remarks>
        ///     This property is used to synchronize communication and prevent multiple simultaneous
        ///     transmissions. It is automatically managed by the class during data exchange operations.
        /// </remarks>
        public bool IsReady { get; internal set; } = true;

        /// <summary>
        ///     Occurs when the serial connection becomes ready for the next operation.
        /// </summary>
        /// <remarks>
        ///     This event is raised after a data exchange operation completes, indicating that
        ///     the connection is ready to handle the next transmission.
        /// </remarks>
        public event SerialReadyHandler? SerialReady;
        
        /// <summary>
        ///     Exchanges raw byte data with the connected Bluetooth Low Energy device.
        /// </summary>
        /// <param name="dataToTransmit">The byte array containing the data to transmit.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the response data as a byte array.</returns>
        /// <remarks>
        ///     This method:
        ///     1. Ensures the RX characteristic is subscribed for notifications
        ///     2. Waits for the serial connection to be ready
        ///     3. Transmits the data and waits for the response
        ///     4. Marks the connection as ready for the next operation
        /// </remarks>
        public async ValueTask<byte[]> ExchangeRawData(
            byte[] dataToTransmit,
            CancellationToken cancellationToken = default)
        {
            // Ensure RX characteristic is subscribed
            await rxCharacteristic.SubscribeAsync(cancellationToken);

            // Wait for serial to be ready to prevent multiple transmissions at same time
            await new WaitForSerialReady(this, cancellationToken);

            // Wait for response from device
            byte[] response = await new TransmitAndWaitForBluetoothNotification(
                txCharacteristic, rxCharacteristic, dataToTransmit, cancellationToken);

            // Serial is ready for next command
            IsReady = true;
            SerialReady?.Invoke();
            
            return response;
        }

        /// <summary>
        ///     Exchanges string messages with the connected Bluetooth Low Energy device.
        /// </summary>
        /// <param name="messageToTransmit">The string message to transmit.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the response as a string.</returns>
        /// <remarks>
        ///     This method converts the input string to ASCII bytes, exchanges the data using
        ///     <see cref="ExchangeRawData"/>, and converts the response back to a string.
        ///     All string encoding/decoding is performed using ASCII encoding.
        /// </remarks>
        public async ValueTask<string> ExchangeMessages(
            string messageToTransmit,
            CancellationToken cancellationToken = default)
        {
            // Convert message to bytes
            byte[] dataToTransmit = Encoding.ASCII.GetBytes(messageToTransmit);

            // Exchange data and get response
            byte[] dataReceived = await ExchangeRawData(dataToTransmit, cancellationToken);

            // Convert response to string
            return Encoding.ASCII.GetString(dataReceived);
        }
    }
}