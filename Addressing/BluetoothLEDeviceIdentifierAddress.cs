using IRIS.Bluetooth.Common.Abstract;

namespace IRIS.Bluetooth.Common.Addressing
{
    public readonly struct BluetoothLEDeviceIdentifierAddress(ulong deviceAddress)
        : IBluetoothLEAddress
    {
        /// <summary>
        /// Address of the device
        /// </summary>
        public ulong DeviceAddress { get; private init; } = deviceAddress;

        /// <summary>
        /// Check if the device is valid for this address
        /// </summary>
        /// TODO: Rework to use IBluetoothLEDevice and reimplement
        public bool IsDeviceValid(IBluetoothLEDevice device) => device.DeviceAddress == DeviceAddress;

        public override string ToString() => $"{DeviceAddress:X}";
    }
}