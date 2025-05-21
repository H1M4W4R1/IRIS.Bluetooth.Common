using IRIS.Bluetooth.Common.Addressing;
using IRIS.Communication;

namespace IRIS.Bluetooth.Common.Abstract
{
    /// <summary>
    ///     Represents Bluetooth Communication interface
    /// </summary>
    /// <remarks>
    ///     New revision of BluetoothInterface is responsible only for finding devices (discovering them) and validating
    ///     if found devices have proper address. Multiple devices can be connected to same interface, so compared to previous
    ///     revisions all characteristic and service related methods were moved to <see cref="IBluetoothLEDevice" /> interface.
    ///     <br />--<br />
    ///     This reduces amount of mess in the code and makes it easier to maintain and understand.
    /// </remarks>
    public interface IBluetoothLEInterface : ICommunicationInterface<IBluetoothLEAddress>
    {
        public IReadOnlyList<IBluetoothLEDevice> DiscoveredDevices { get; }
        
        public IReadOnlyList<IBluetoothLEDevice> ConnectedDevices { get; }

        /// <summary>
        ///     Claim device to be used for communication.
        /// </summary>
        /// <param name="cancellationToken">Token to cancel the operation</param>
        /// <returns>Device that was claimed or null if failed</returns>
        public ValueTask<IBluetoothLEDevice?> ClaimDevice(CancellationToken cancellationToken = default);

        /// <summary>
        ///     Release device that was claimed for communication.
        /// </summary>
        /// <param name="device">Device to release</param>
        public ValueTask ReleaseDevice(IBluetoothLEDevice device);
        
        public event DeviceDiscoveredHandler OnBluetoothDeviceDiscovered;
        public event DeviceConnectedHandler OnBluetoothDeviceConnected;
        public event DeviceDisconnectedHandler OnBluetoothDeviceDisconnected;
        public event DeviceConnectionLostHandler OnBluetoothDeviceConnectionLost;
    }


}
