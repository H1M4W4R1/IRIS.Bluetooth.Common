using IRIS.Bluetooth.Common.Abstract;

namespace IRIS.Bluetooth.Common
{
    public delegate void CharacteristicValueChanged(IBluetoothLECharacteristic characteristic, byte[] newValue);
    
    public delegate void DeviceDiscovered(IBluetoothLEInterface sender, IBluetoothLEDevice device);
    
    public delegate void DeviceConnected(IBluetoothLEInterface sender, IBluetoothLEDevice device);
    
    public delegate void DeviceDisconnected(IBluetoothLEInterface sender, IBluetoothLEDevice device);
    
    public delegate void DeviceConnectionLost(IBluetoothLEInterface sender, IBluetoothLEDevice device);
}