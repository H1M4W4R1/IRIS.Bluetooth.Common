using IRIS.Bluetooth.Common.Abstract;

namespace IRIS.Bluetooth.Common.Addressing
{
    /// <summary>
    /// Bluetooth device address
    /// </summary>
    public interface IBluetoothLEAddress
    {
        /// <summary>
        ///     Check if the device is valid for this address
        /// </summary>
        public bool IsDeviceValid(IBluetoothLEDevice device);

    }
}