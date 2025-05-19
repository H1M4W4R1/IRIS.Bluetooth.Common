using System.Text;
using IRIS.Bluetooth.Common.Abstract;
using IRIS.Bluetooth.Common.Serial.Utility;
using IRIS.Bluetooth.Common.Utility;

namespace IRIS.Bluetooth.Common.Serial
{
    /// <summary>
    /// Bluetooth Low Energy Serial - class used to create a serial connection to a BLE device
    /// </summary>
    public sealed class BLESerial(
        IBluetoothLECharacteristic txCharacteristic,
        IBluetoothLECharacteristic rxCharacteristic
    )
    {
        /// <summary>
        /// Check if this device is ready for another operation
        /// </summary>
        public bool IsReady { get; internal set; } = true;

        public event SerialReadyHandler? SerialReady;
        
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