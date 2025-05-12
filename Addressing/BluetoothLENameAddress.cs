using System.Text.RegularExpressions;
using IRIS.Bluetooth.Common.Abstract;

namespace IRIS.Bluetooth.Common.Addressing
{
    /// <summary>
    ///     Represents a Bluetooth Low Energy device address that identifies devices by matching their broadcast names
    ///     against a regular expression pattern.
    /// </summary>
    /// <remarks>
    ///     This address type is used when you need to discover devices based on their broadcast names.
    ///     The regular expression pattern allows for flexible matching of device names, supporting both exact matches
    ///     and pattern-based discovery.
    /// </remarks>
    public readonly struct BluetoothLENameAddress(string deviceNameRegex) : IBluetoothLEAddress
    {
        /// <summary>
        ///     Gets the regular expression pattern used to match device names.
        /// </summary>
        /// <remarks>
        ///     This pattern is used to filter discovered devices based on their broadcast names.
        ///     The pattern should be a valid regular expression that can match the target device names.
        /// </remarks>
        public string NameRegex { get; } = deviceNameRegex;
        
        /// <summary>
        ///     Determines if a discovered device matches this address by comparing its name against the regular expression pattern.
        /// </summary>
        /// <param name="device">The device to validate</param>
        /// <returns>True if the device's name matches the regular expression pattern, false otherwise</returns>
        /// <remarks>
        ///     The method performs a null check on both the device name and the regular expression pattern
        ///     before attempting to match them.
        /// </remarks>
        public bool IsDeviceValid(IBluetoothLEDevice device)
        {
            return device.Name is { } name
                   && NameRegex is { } regex
                   && Regex.IsMatch(name, regex);
        }

        /// <summary>
        ///     Returns a string representation of the regular expression pattern used for device name matching.
        /// </summary>
        /// <returns>A string containing the regular expression pattern</returns>
        public override string ToString() => $"{NameRegex}";
    }
}