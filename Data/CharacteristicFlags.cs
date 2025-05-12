namespace IRIS.Bluetooth.Common.Data
{
    /// <summary>
    ///     Defines the possible operations that can be performed on a Bluetooth Low Energy characteristic.
    /// </summary>
    /// <remarks>
    ///     These flags are used to specify which operations a characteristic supports.
    ///     Multiple flags can be combined using the bitwise OR operator.
    /// </remarks>
    [Flags]
    public enum CharacteristicFlags
    {
        /// <summary>
        ///     Don't care about what operations wh
        /// </summary>
        None = 0x0,

        /// <summary>
        ///     The characteristic can be read.
        /// </summary>
        Read = 0x1,

        /// <summary>
        ///     The characteristic can be written to.
        /// </summary>
        Write = 0x2,

        /// <summary>
        ///     The characteristic supports notifications for value changes.
        /// </summary>
        Notify = 0x4
    }
}