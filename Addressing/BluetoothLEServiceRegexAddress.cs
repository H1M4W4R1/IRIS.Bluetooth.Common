using System.Text.RegularExpressions;
using IRIS.Bluetooth.Common.Abstract;

namespace IRIS.Bluetooth.Common.Addressing
{
    /// <summary>
    /// Represents a Bluetooth LE service address
    /// Can also match device name using regular expression
    /// </summary>
    public readonly struct BluetoothLEServiceRegexAddress(string regexPattern) : IBluetoothLEAddress
    {
        /// <summary>
        /// UUID of the service
        /// </summary>
        public string RegexPattern { get; init; } = regexPattern;

        /// <summary>
        /// Check if the device is valid for this service
        /// </summary>
        public bool IsDeviceValid(IBluetoothLEDevice device)
        {
            // Convert UUID to lowercase and remove dashes
            string regexPattern = RegexPattern.ToLowerInvariant().Replace("-", "");

            // Loop through all services of the device
            foreach (IBluetoothLEService service in device.Services)
            {
                string checkedUUID = service.UUID.ToLower().Replace("-", "");
                
                // Check regex pattern
                if (Regex.IsMatch(checkedUUID, regexPattern)) return true;
            }

            return false;
        }

        public override string ToString() => $"{RegexPattern}";
    }
}