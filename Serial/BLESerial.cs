using System.Text;
using IRIS.Bluetooth.Common.Abstract;
using IRIS.Bluetooth.Common.Serial.Utility;
using IRIS.Bluetooth.Common.Utility;
using IRIS.Operations;
using IRIS.Operations.Abstract;
using IRIS.Operations.Data;

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
        /// <remarks>
        ///     This method:
        ///     1. Ensures the RX characteristic is subscribed for notifications
        ///     2. Waits for the serial connection to be ready
        ///     3. Transmits the data and waits for the response
        ///     4. Marks the connection as ready for the next operation
        /// </remarks>
        public async ValueTask<IDeviceOperationResult> ExchangeRawData(
            byte[] dataToTransmit,
            CancellationToken cancellationToken = default)
        {
            // Ensure RX characteristic is subscribed
            await rxCharacteristic.SubscribeAsync(cancellationToken);

            // Wait for serial to be ready to prevent multiple transmissions at same time
            await new WaitForSerialReady(this, cancellationToken);

            // Wait for response from device
            IDeviceOperationResult response = await new TransmitAndWaitForBluetoothNotification(
                txCharacteristic, rxCharacteristic, dataToTransmit, cancellationToken);
            
            // Check if response is success
            if (DeviceOperation.IsFailure(response, out IDeviceOperationResult proxyResult))
                return proxyResult;
            
            // Check if response has proper data
            if (response is not IDeviceOperationResult<byte[]>) return new DeviceReadFailedResult();

            // Serial is ready for next command
            IsReady = true;
            SerialReady?.Invoke();
            
            return response;
        }

        /// <summary>
        ///     Exchanges string messages with the connected Bluetooth Low Energy device.
        /// </summary>
        /// <param name="messageToTransmit">The string message to transmit.</param>
        /// <param name="encoding">The encoding to use for string conversion, ASCII if null.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <remarks>
        ///     This method converts the input string to bytes, exchanges the data using
        ///     <see cref="ExchangeRawData" />, and converts the response back to a string.
        ///     If encoding is not provided all string encoding/decoding will be performed using
        ///     ASCII encoding.
        /// </remarks>
        public async ValueTask<IDeviceOperationResult> ExchangeMessages(
            string messageToTransmit,
            Encoding? encoding = null,
            CancellationToken cancellationToken = default)
        {
            // Ensure encoding is not null
            encoding ??= Encoding.ASCII;
            
            // Convert message to bytes
            byte[] dataToTransmit = encoding.GetBytes(messageToTransmit);

            // Exchange data and get response
            IDeviceOperationResult dataReceived = await ExchangeRawData(dataToTransmit, cancellationToken);
            
            // Check for failure
            if (DeviceOperation.IsFailure(dataReceived, out IDeviceOperationResult proxyResult))
                return proxyResult;
            
            // Check if response has proper data
            if (dataReceived is not IDeviceOperationResult<byte[]> deviceOperationResult)
                return new DeviceReadFailedResult();
         
            // Convert response to string
            return new DeviceReadSuccessful<string>(encoding.GetString(deviceOperationResult.Data));
        }
    }
}