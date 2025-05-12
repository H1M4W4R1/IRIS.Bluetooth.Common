namespace IRIS.Bluetooth.Common.Data
{
    /// <summary>
    ///     Specifies the type of regular expression pattern used for Bluetooth Low Energy device discovery and filtering.
    /// </summary>
    public enum RegexType
    {
        /// <summary>
        ///     Regular expression pattern for matching device names.
        /// </summary>
        /// <remarks>
        ///     Used when searching for devices by their broadcast name.
        /// </remarks>
        Name,

        /// <summary>
        ///     Regular expression pattern for matching service UUIDs.
        /// </summary>
        /// <remarks>
        ///     Used when searching for devices that provide specific services.
        /// </remarks>
        Service
    }
}