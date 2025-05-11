namespace IRIS.Bluetooth.Common.Data
{
    [Flags]
    public enum CharacteristicFlags
    {
        None = 0x0,
        Read = 0x1,
        Write = 0x2,
        Notify = 0x4
    }
}