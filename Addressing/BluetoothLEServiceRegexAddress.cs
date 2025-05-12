using System.Text.RegularExpressions;
using IRIS.Bluetooth.Common.Abstract;

namespace IRIS.Bluetooth.Common.Addressing
{
    /// <summary>
    ///     Represents a Bluetooth Low Energy device address that identifies devices by matching their service UUIDs
    ///     against a regular expression pattern.
    /// </summary>
    /// <remarks>
    ///     This address type is used when you need to discover devices that provide services matching a specific pattern.
    ///     The regular expression pattern allows for flexible matching of service UUIDs, supporting both exact matches
    ///     and pattern-based discovery. UUIDs are normalized (lowercase, no dashes) before matching.
    /// </remarks>
    public readonly struct BluetoothLEServiceRegexAddress(string regexPattern) : IBluetoothLEAddress
    {
        /// <summary>
        ///     Gets the regular expression pattern used to match service UUIDs.
        /// </summary>
        /// <remarks>
        ///     This pattern is used to filter discovered devices based on their service UUIDs.
        ///     The pattern should be a valid regular expression that can match the target service UUIDs.
        ///     The pattern will be applied to normalized UUIDs (lowercase, no dashes).
        /// </remarks>
        public string RegexPattern { get; init; } = regexPattern;

        /// <summary>
        ///     Determines if a discovered device matches this address by checking if any of its services
        ///     match the regular expression pattern.
        /// </summary>
        /// <param name="device">The device to validate</param>
        /// <returns>True if any of the device's services match the regular expression pattern, false otherwise</returns>
        /// <remarks>
        ///     The method normalizes both the regular expression pattern and the device's service UUIDs by:
        ///     1. Converting to lowercase
        ///     2. Removing dashes
        ///     This ensures consistent matching regardless of the UUID format.
        /// </remarks>
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

        /// <summary>
        ///     Returns a string representation of the regular expression pattern used for service UUID matching.
        /// </summary>
        /// <returns>A string containing the regular expression pattern</returns>
        public override string ToString() => $"{RegexPattern}";
    }
}