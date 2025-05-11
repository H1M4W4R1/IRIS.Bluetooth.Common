using System.Text.RegularExpressions;
using IRIS.Bluetooth.Common.Abstract;

namespace IRIS.Bluetooth.Common.Addressing
{
    public readonly struct BluetoothLENameAddress(string deviceNameRegex) : IBluetoothLEAddress
    {
        /// <summary>
        /// Regular expression to match device name
        /// </summary>
        public string NameRegex { get; } = deviceNameRegex;
        
        /// <summary>
        /// Check if the device is valid for this address
        /// </summary>
        public bool IsDeviceValid(IBluetoothLEDevice device)
        {
            return device.Name is { } name
                   && NameRegex is { } regex
                   && Regex.IsMatch(name, regex);
        }

        public override string ToString() => $"{NameRegex}";
    }
}